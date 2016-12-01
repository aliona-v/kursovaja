using ChatClient.Classes;
using System;
using System.Windows.Forms;

namespace ChatClient.Forms
{
    public partial class LogIn : Form
    {
        public Connector connect;
        public LogIn(Connector connect)
        {
            InitializeComponent();
            this.connect = connect;
        }

        //Кнопка "Выход"
        private void bCloseProgram_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Кнопка "Вход"
        private void bOkay_Click(object sender, EventArgs e)
        {
            string sQueryIsEmpry;

            //Проверка на админку
            if(tbUserName.Text == "admin")
            {
                //Если администратор, то открытие формы для работы с таблицами и настройками
                sQueryIsEmpry = "SELECT * FROM USERS WHERE vName = 'admin'";
                //Проверка на существование в базе админмистратора
                if (connect.QueryIsEmpty(sQueryIsEmpry) == true)
                {
                    //Проверка на ввод пароля
                    if (tbUserPass.Text.Equals(""))
                    {
                        MessageBox.Show("Вы не ввели все данные!!!", "Внимание!!!");
                    }
                    else
                    {
                        //Строка проверки
                        sQueryIsEmpry = "SELECT * FROM USERS WHERE vName = 'admin' AND vPassword = '" + tbUserPass.Text + "';";
                        //Проверка на существование аккаунта
                        if (connect.QueryIsEmpty(sQueryIsEmpry) == true)
                        {
                            Main main = new Main(connect);
                            main.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Пароль не верен, повторите попытку!!!", "Внимание!!!");
                        }
                    }
                }
                else
                {
                    //Предложение создать администратора с паролем по умолчанию
                    if (MessageBox.Show("Аккаунта администратора не существует!\nСоздать по умолчанию?\nИмя: admin\nПароль:admin",
                       "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        sQueryIsEmpry = "INSERT INTO USERS (idUsers, vName, vPassword) VALUES (0, 'admin', 'admin');";
                        //Внос значения в базу данных
                        connect.QueryIsEmpty(sQueryIsEmpry);
                    }

                    else
                    {
                        Application.Exit();
                    }
                }
            }
            else //user
            {
                //Если пользователь, то открытие формы чата
                //Предварительное включение сервера обязательно!!!

                //Проверка не ввод данных
                if (tbUserName.Text.Equals("") || tbUserPass.Text.Equals(""))
                {
                    MessageBox.Show("Вы не ввели все данные!!!", "Внимание!!!");
                }
                else
                {
                    //Строка проверки
                    sQueryIsEmpry = "SELECT * FROM USERS WHERE vName = '" + tbUserName.Text + "' AND vPassword = '" + tbUserPass.Text + "';";
                    //Проверка на существование аккаунта
                    if (connect.QueryIsEmpty(sQueryIsEmpry) == true)
                    {
                        string sIdUser = connect.GetData("SELECT iIdUsers FROM USERS WHERE vName = '"
                            + tbUserName.Text + "' AND vPassword = '" + tbUserPass.Text + "';");

                        //Открытие формы чата с передачей никнейма в форму входа

                        ChatClient chatClient = new ChatClient(connect);
                        //Передача никнейма
                        chatClient.nickName = tbUserName.Text;
                        //Открываем форму
                        chatClient.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Пароль не верен, повторите попытку!!!", "Внимание!!!");
                    }
                }
            }
            
        }//end
    }
}