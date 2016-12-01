using ChatClient.Classes;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace ChatClient.Forms
{
    public partial class ChatClient : Form
    {
        public bool bCloseApi = true;

        private delegate void ChatEvent(string content, string clr);
        private ChatEvent _addMessage;
        private Socket _serverSocket;
        private Thread listenThread;
        private string _host = "127.0.0.1";
        private int _port = 2222;

        //Переменная хранящая никнейм пользователя
        public string nickName;
        public Connector connect;
        public ChatClient(Connector connect)
        {
            InitializeComponent();
            this.connect = connect;

            _addMessage = new ChatEvent(AddMessage);
            userMenu = new ContextMenuStrip();

            //Описываем новый элемент в меню на ПКМ
            ToolStripMenuItem PrivateMessageItem = new ToolStripMenuItem();
            PrivateMessageItem.Text = "Личное сообщение";
            PrivateMessageItem.Click += delegate
            {
                if (userList.SelectedItems.Count > 0)
                {
                    messageData.Text = $"\"{userList.SelectedItem} ";
                }
            };
            //Добавляем к списку
            userMenu.Items.Add(PrivateMessageItem);

            //Описываем новый элемент в меню на ПКМ
            ToolStripMenuItem SendFileItem = new ToolStripMenuItem()
            {
                Text = "Отправить файл"
            };
            SendFileItem.Click += delegate
            {
                if (userList.SelectedItems.Count == 0)
                {
                    return;
                }
                OpenFileDialog ofp = new OpenFileDialog();
                ofp.ShowDialog();
                if (!File.Exists(ofp.FileName))
                {
                    MessageBox.Show($"Файл {ofp.FileName} не найден!");
                    return;
                }
                FileInfo fi = new FileInfo(ofp.FileName);
                byte[] buffer = File.ReadAllBytes(ofp.FileName);
                Send($"#sendfileto|{userList.SelectedItem}|{buffer.Length}|{fi.Name}");//g
                Send(buffer);


            };
            //Добавляем к списку
            userMenu.Items.Add(SendFileItem);

            //Описываем новый элемент в меню на ПКМ
            ToolStripMenuItem AddNewFriendItem = new ToolStripMenuItem()
            {
                Text = "Добавить в друзья"
            };
            AddNewFriendItem.Click += delegate
            {
                if (userList.SelectedItems.Count == 0)
                {
                    return;
                }

                int iMaxIdTable = 0;

                //Получаем имя друга для добавления
                string sNameOfFriend = userList.SelectedItem.ToString();

                string sIdUsers = connect.GetData("SELECT iIdUsers FROM USERS WHERE vName = '" + sNameOfFriend + "'; ");

                //Проверка на добавления самого себя в друзья
                if(sNameOfFriend == nicknameData.Text)
                {
                    MessageBox.Show("Нельзя дружить с самим собой\nМы же не психи, правда? :)", "Сообщение!");
                }                
                else
                {
                    //Вызов процедуры получения максимального ID из таблицы, актоинкремент
                    iMaxIdTable = iMaxIdInTable("SELECT * FROM FRIENDS", "SELECT MAX(iIdFriends) FROM FRIENDS");

                    //Строка проверки
                    string sCheckQuery = "SELECT * FROM FRIENDS WHERE vName = '" + nicknameData.Text + "' AND fk_iIdUsers = " + sIdUsers;

                    //Проверка, есть ли у данного пользователя уже этот друг
                    if (connect.QueryIsEmpty(sCheckQuery) == true)
                    {
                        //Сообщение, предупреждение
                        MessageBox.Show(sNameOfFriend + ", уже есть в друзьях у " + nicknameData.Text + " !", "Сообщение");
                        //Выходим из процедуры нажатия на кнопку
                        return;
                    }
                    else
                    {
                        string sQueryAdd = "INSERT INTO FRIENDS(iIdFriends, vName, fk_iIdUsers) VALUES("
                       + iMaxIdTable + ", '" + nicknameData.Text + "','" + sIdUsers + "');";

                        connect.QueryIsEmpty(sQueryAdd);

                        MessageBox.Show(sNameOfFriend + ", добавлен в друзья к " + nicknameData.Text + " !", "Сообщение");
                    }
                }                
            };
            //Добавляем к списку
            userMenu.Items.Add(AddNewFriendItem);

            //Присваиваем листу юзеров на форме созданный ранее лист меню
            userList.ContextMenuStrip = userMenu;
        }

        //Процедура вызова максимального ID из таблица
        public int iMaxIdInTable(string sSqlQueryIsEmpty, string sSqlQueryMaxId)
        {
            int iMaxIdTableLocal = -1;

            //Если сушествуют данные в таблице, а это возврат true
            if (connect.QueryIsEmpty(sSqlQueryIsEmpty) == true)
            {
                //То делаем запрос на max id в таблицу
                string sID = connect.GetData(sSqlQueryMaxId);
                try
                {
                    iMaxIdTableLocal = Convert.ToInt32(sID);
                    iMaxIdTableLocal++;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else
            {
                iMaxIdTableLocal = 1;
            }

            return iMaxIdTableLocal;
        }

        //Процедура вывода сообщения с предупреждением
        private void AddMessage(string Content, string Color = "Black")
        {
            if (InvokeRequired)
            {
                Invoke(_addMessage, Content, Color);
                return;
            }
            chatBox.SelectionStart = chatBox.TextLength;
            chatBox.SelectionLength = Content.Length;
            chatBox.SelectionColor = getColor(Color);
            chatBox.AppendText(Content + Environment.NewLine);
        }

        //Процедура перекращивания сообщения в чате
        private Color getColor(string text)
        {
            //Задаем цвет
            if (Color.Red.Name.Contains(text))
                return Color.Red;
            return Color.Black;

        }

        //Процедура отправки данных сокету, тип данных массив байт
        public void Send(byte[] buffer)
        {
            try
            {
                //Отправка данных сокету
                _serverSocket.Send(buffer);
            }
            catch { }
        }

        //Процедура отправки данных сокету, тип данных строка
        public void Send(string Buffer)
        {
            try
            {
                //Отправка данных сокету
                _serverSocket.Send(Encoding.Unicode.GetBytes(Buffer));
            }
            catch { }
        }

        //Процедура считывания данных
        public void listner()
        {
            try
            {
                //До тех пора, пока сокет держит коннект
                while (_serverSocket.Connected)
                {
                    byte[] buffer = new byte[2048];
                    //Получение данных от сокеа
                    int bytesReceive = _serverSocket.Receive(buffer);

                    string result = System.Text.Encoding.UTF8.GetString(buffer);

                    //Отправка команды
                    handleCommand(Encoding.Unicode.GetString(buffer, 0, bytesReceive));
                }
            }
            catch
            {
                MessageBox.Show("Связь с сервером прервана");
                Application.Exit();
            }
        }

        //Процедура обработки команд
        public void handleCommand(string cmd)
        {
            //Метод возвращающий массив string с присутствующими в данном экземпляре подстроками внутри
            string[] commands = cmd.Split('#');
            //Определение длинны строки
            int countCommands = commands.Length;
            //Массив ограниченный длинной строки
            for (int i = 0; i < countCommands; i++)
            {
                try
                {
                    //Текущая i-команда
                    string currentCommand = commands[i];
                    //Если соддержит эту строку, не пустая и не содержит null
                    if (string.IsNullOrEmpty(currentCommand))
                    {
                        continue;
                    }
                    if (currentCommand.Contains("setnamesuccess"))
                    {


                        //Из-за того что программа пыталась получить доступ к контролам из другого потока вылетал эксепщен и поля не разблокировались

                        Invoke((MethodInvoker)delegate
                        {
                            AddMessage($"Добро пожаловать, {nicknameData.Text}");
                            nameData.Text = nicknameData.Text;
                            chatBox.Enabled = true;
                            messageData.Enabled = true;
                            userList.Enabled = true;
                            nicknameData.Enabled = false;
                            enterChat.Enabled = false;
                        });
                        continue;
                    }
                    if (currentCommand.Contains("setnamefailed"))
                    {
                        AddMessage("Неверный ник!");
                        continue;
                    }
                    if (currentCommand.Contains("msg"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        AddMessage(Arguments[1], Arguments[2]);
                        continue;
                    }

                    if (currentCommand.Contains("userlist"))
                    {
                        string[] Users = currentCommand.Split('|')[1].Split(',');
                        int countUsers = Users.Length;
                        userList.Invoke((MethodInvoker)delegate { userList.Items.Clear(); });
                        for (int j = 0; j < countUsers; j++)
                        {
                            userList.Invoke((MethodInvoker)delegate { userList.Items.Add(Users[j]); });
                        }
                        continue;

                    }
                    if (currentCommand.Contains("gfile"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        string fileName = Arguments[1];
                        string FromName = Arguments[2];
                        string FileSize = Arguments[3];
                        string idFile = Arguments[4];
                        DialogResult Result = MessageBox.Show($"Вы хотите принять файл {fileName} размером {FileSize} от {FromName}", "Файл", MessageBoxButtons.YesNo);
                        if (Result == DialogResult.Yes)
                        {
                            Thread.Sleep(1000);
                            Send("#yy|" + idFile);
                            byte[] fileBuffer = new byte[int.Parse(FileSize)];
                            _serverSocket.Receive(fileBuffer);
                            File.WriteAllBytes(fileName, fileBuffer);
                            MessageBox.Show($"Файл {fileName} принят.");
                        }
                        else
                            Send("nn");
                        continue;
                    }

                }
                catch (Exception exp) { Console.WriteLine("Error with handleCommand: " + exp.Message); }

            }


        }

        //Загрузка формы чата
        private void ChatClient_Load(object sender, EventArgs e)
        {
            //Настройка справочной системы
            helpProviderChat.HelpNamespace = @"Data\Help\ChatHelp.chm";
            helpProviderChat.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProviderChat.SetShowHelp(this, true);

            //Сокрытие списка друзей
            tabPageFriends.Parent = null;
            //Установка никнейма
            nicknameData.Text = nickName;
            //Задаем IP адрес
            IPAddress temp = IPAddress.Parse(_host);
            //Создаем новый сокет и задаем ему условия (адрес,тип, протокол и т.д.)
            _serverSocket = new Socket(temp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //Конектимся к сокету
            _serverSocket.Connect(new IPEndPoint(temp, _port));
            if (_serverSocket.Connected)
            {
                //Разблокируем кнопку подключения
                enterChat.Enabled = true;
                //Разблокируем кнопку ввода никнейма
                nicknameData.Enabled = true;
                //Посылка сообщения об успешном установлении соединения
                AddMessage("Связь с сервером установлена.");
                //Создаем новый поток
                listenThread = new Thread(listner);
                //Делаем фоновым
                listenThread.IsBackground = true;
                //Запускаем поток
                listenThread.Start();

            }
            else
            {
                AddMessage("Связь с сервером не установлена.");
            }

            /*helpProviderChat.HelpNamespace = @"Data\Help\ChatHelp.chm";
            helpProviderChat.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProviderChat.SetShowHelp(this, true);*/
        }

        //Кнопка входа в чат
        private void enterChat_Click(object sender, EventArgs e)
        {
            //Получение никнейма пользователя (передается автоматически)
            string nickName = nicknameData.Text;
            //Если никнейм не нуловый
            if (string.IsNullOrEmpty(nickName))
            {
                return;
            }
            else
            {
                Send($"#setname|{nickName}");
                //Возвращаем вкладку на место
                tabPageFriends.Parent = tabControlChat;
            }
        }

        //Обработка события причины закрытия формы, во время закрытия формы
        private void ChatClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_serverSocket.Connected)
            {
                Send("#endsession");
            }

            if (bCloseApi == true)
            {
                //Закрываем соединение
                connect.CloseConnection();
                //Закрываем программу
                Application.Exit();
            }
        }

        //Происходит в момент отпускания клавишы
        private void messageData_KeyUp(object sender, KeyEventArgs e)
        {
            //Если была нажата кнопка "Enter"
            if (e.KeyData == Keys.Enter)
            {
                string msgData = messageData.Text;
                if (string.IsNullOrEmpty(msgData))
                {
                    return;
                }                    
                if (msgData[0] == '"')
                {
                    string temp = msgData.Split(' ')[0];
                    string content = msgData.Substring(temp.Length + 1);
                    temp = temp.Replace("\"", string.Empty);

                    Send($"#private|{temp}|{content}");
                }
                else
                {              
                    Send($"#message|{msgData}");
                }
                    
                messageData.Text = string.Empty;
            }
        }

        //Вкладка "Друзья", обновление списка друзей
        private void bUpdateFriends_Click(object sender, EventArgs e)
        {
            //Проверка есть ли вообще друзья у данного пользователя
            if(connect.QueryIsEmpty("SELECT U.vName FROM FRIENDS AS F, USERS AS U WHERE F.fk_iIdUsers = U.iIdUsers AND F.vName = '" 
                + nicknameData.Text + "'") == true)
            {
                //Загрузка списка друзей
                connect.ListBoxLoad("SELECT U.vName AS Spisok FROM FRIENDS AS F, USERS AS U WHERE F.fk_iIdUsers = U.iIdUsers AND F.vName = '" 
                    + nicknameData.Text + "'", friendList, "Spisok");
            }
            else
            {
                MessageBox.Show("У Вас нет друзей, forever alone :c", "Сообщение");
            }
        }

        //Вкладка "Друзья," удаление друга из списка
        private void bDeleteFriend_Click(object sender, EventArgs e)
        {
            //Проверка на пустоту
            if(nicknameDelete.Text.Equals(""))
            {
                MessageBox.Show("Не все данные введены!!!", "Сообщение");
            }
            else
            {                
                //Проверка на правильность вводимого никнейма
                if (connect.QueryIsEmpty("SELECT iIdUsers FROM USERS WHERE vName = '" + nicknameDelete.Text + "'") == false)
                {
                    MessageBox.Show("Пользователя с никнеймом: " + nicknameDelete.Text + ", не существует!!!","Сообщение");

                    //Убераем последний элемент из списка
                    friendList.Items.Clear();
                    return;
                }

                //Получение ID друга
                string sIdFriend = connect.GetData("SELECT iIdUsers FROM USERS WHERE vName = '" + nicknameDelete.Text + "'");

                if (MessageBox.Show("Вы действительно хотите удалить друга из списка?",
                          "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Удаление строки
                    connect.QueryIsEmpty("DELETE FROM FRIENDS WHERE vName = '" + nicknameData.Text + "' AND fk_iIdUsers = " + sIdFriend);

                    MessageBox.Show(nicknameDelete.Text + " был успешно удален из списка ваших друзей!", "Сообщение!");
                    nicknameDelete.Clear();

                    //Вызов кода нажатия кнопки
                    bUpdateFriends_Click(null, null);                    
                }
            }            
        }

        //Происходит при смене вкладки
        private void tabControlChat_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //Проверка какая страница была выбрана
            if(tabControlChat.SelectedTab == tabPageFriends)
            {
                //Проверка есть ли вообще друзья у данного пользователя
                if (connect.QueryIsEmpty("SELECT U.vName FROM FRIENDS AS F, USERS AS U WHERE F.fk_iIdUsers = U.iIdUsers AND F.vName = '"
                    + nicknameData.Text + "'") == true)
                {
                    //Загрузка списка друзей
                    connect.ListBoxLoad("SELECT U.vName AS Spisok FROM FRIENDS AS F, USERS AS U WHERE F.fk_iIdUsers = U.iIdUsers AND F.vName = '"
                        + nicknameData.Text + "'", friendList, "Spisok");

                    //Строка для получения количества друзей
                    string sCountOfFriends = "SELECT COUNT(*) FROM FRIENDS WHERE vName = '" + nicknameData.Text + "'";

                    tabPageFriends.Text = "Друзья(" + connect.GetData(sCountOfFriends)  + ")";
                }
                else
                {
                    MessageBox.Show("У Вас нет друзей, forever alone :c", "Сообщение");

                    tabPageFriends.Text = "Друзья(0)";
                }
            }            
        }

        //Кнопка "Авторизация"
        private void bLogIng_Click(object sender, EventArgs e)
        {
            bCloseApi = false;

            LogIn logIn = new LogIn(connect);
            logIn.Show();
            Close();
        }

        //Кнопка вызова справки (F1)
        private void bHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProviderChat.HelpNamespace);
        }
    }
}