using ChatClient.Classes;
using ChatClient.Forms;
using System;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Main : Form
    {
        public Connector connect;
        public Main(Connector connect)
        {
            InitializeComponent();
            this.connect = connect;
        }

        //Бинсорсы для навигации по таблицам
        public BindingSource binSourceUsers = new BindingSource();       
        public BindingSource binSourceFriends = new BindingSource();

        //Процедура настройки дата грида
        public void settingOfDGV(DataGridView dGV)
        {
            dGV.ReadOnly = true;
            dGV.AllowUserToAddRows = false;

            dGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dGV.MultiSelect = false;
        }

        //Загрузка формы
        private void Main_Load(object sender, EventArgs e)
        {
            //Настройка справочной системы
            helpProviderChat.HelpNamespace = @"Data\Help\ChatHelp.chm";
            helpProviderChat.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProviderChat.SetShowHelp(this, true);            

            //Выбор активной вкладки, вкладки "Пользователи"
            this.tabControlChat.SelectedTab = tabPageUsers;

            //Прогрузка таблицы
            connect.LoadTable("USERS", "SELECT * FROM USERS", binSourceUsers, dGVUsers, bNavigatorChat);

            try
            {
                dGVUsers.Columns[0].Visible = false;
                dGVUsers.Columns[1].HeaderText = "Логин";
                dGVUsers.Columns[2].HeaderText = "Пароль";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            settingOfDGV(dGVUsers);
        }

        //Кнопка меню "Информация/О программе"
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программное средство обмена сообщениями\nБезопасный обмен сообщениями между пользователями.\n"
                + "Авторизация.Управление списком контактов.Обмен сообщениями с добавленным контактом.\n"
                + "Передача файлов\n"
                + "Шифрование данных\n©2016", "О программе");
        }

        //Кнопка меню "Информация/Справка(F1)"
        private void справкаF1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProviderChat.HelpNamespace);
        }

        //Кнопка меню "Файл/Авторизация"
        private void авторизацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogIn logIn = new LogIn(connect);
            logIn.Show();
            Close();
        }

        //Кнопка меню "Файл/Выход"
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Кнопка меню "Настройки"
        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings Stngs = new Settings(connect);
            Stngs.Show();
            //Return to Main
            Stngs.bReturnToMain.Enabled = true;
            //Cancel
            Stngs.bCancel.Enabled = false;
            Hide();
        }

        //Кнопка навигатора "Добавить"
        private void bAdd_Click(object sender, EventArgs e)
        {
            //Определяем какая вкладка была выбрана
            if (tabControlChat.SelectedTab == tabPageUsers)
            {
                AddEdit addEdit = new AddEdit(connect);
                addEdit.sNameOfTable = "USERS";
                addEdit.Show();
            }
            else
            {
                AddEdit addEdit = new AddEdit(connect);
                addEdit.sNameOfTable = "FRIENDS";
                addEdit.Show();
            }
        }

        //Кнопка навигатора "Изменить"
        private void bEdit_Click(object sender, EventArgs e)
        {
            //Определяем какая вкладка была выбрана
            if (tabControlChat.SelectedTab == tabPageUsers)
            {
                //Перенос данных на форму для редактирования
                if (connect.QueryIsEmpty("SELECT * FROM USERS") == true)
                {
                    AddEdit addEdit = new AddEdit(connect);
                    addEdit.bAdd = false;
                    addEdit.sNameOfTable = "USERS";

                    int x = 0;

                    try
                    {
                        x = dGVUsers.CurrentRow.Index;
                    }
                    catch
                    {
                        MessageBox.Show("Вы не выбрали строку для редактирования!", "Ошибка!");
                        return;
                    }

                    //Забираем значение ячейки
                    addEdit.idIdEditTable = Convert.ToInt32(dGVUsers[0, x].Value);
                    addEdit.tB1.Text = Convert.ToString(dGVUsers[1, x].Value);
                    addEdit.tB2.Text = Convert.ToString(dGVUsers[2, x].Value);

                    addEdit.Show();
                }
                else
                {
                    MessageBox.Show("Нету данных для изменения!", "Ошибка!");
                }
            }
            else
            {
                //Перенос данных на форму для редактирования
                if (connect.QueryIsEmpty("SELECT * FROM FRIENDS") == true)
                {
                    AddEdit addEdit = new AddEdit(connect);
                    addEdit.bAdd = false;
                    addEdit.sNameOfTable = "FRIENDS";

                    int x = 0;

                    try
                    {
                        x = dGVFriends.CurrentRow.Index;
                    }
                    catch
                    {
                        MessageBox.Show("Вы не выбрали строку для редактирования!", "Ошибка!");
                        return;
                    }

                    //Забираем значение ячейки
                    addEdit.idIdEditTable = Convert.ToInt32(dGVFriends[0, x].Value);

                    //Строка получения ID
                    string sGetiIdFriends = "SELECT iIdUsers FROM USERS WHERE vName = '" + dGVFriends[1, x].Value + "'";
                    //Получение ID друга
                    int iIdFriend = Convert.ToInt32(connect.GetData(sGetiIdFriends));
                    //Передача данных на форму
                    addEdit.iIdFriendsEdit = iIdFriend;

                    string sNameOfFriend = Convert.ToString(dGVFriends[2, x].Value);
                    addEdit.sNameOfFriend = sNameOfFriend;

                    addEdit.Show();
                }
                else
                {
                    MessageBox.Show("Нету данных для изменения!", "Ошибка!");
                }
            }            
        }

        //Кнопка навигатора "Обновить"
        private void bUpdate_Click(object sender, EventArgs e)
        {
            //Определяем какая вкладка была выбрана
            if (tabControlChat.SelectedTab == tabPageUsers)
            {
                //Прогрузка таблицы
                connect.LoadTable("USERS","SELECT * FROM USERS", binSourceUsers, dGVUsers, bNavigatorChat);

                try
                {
                    dGVUsers.Columns[0].Visible = false;
                    dGVUsers.Columns[1].HeaderText = "Логин";
                    dGVUsers.Columns[2].HeaderText = "Пароль";
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

                settingOfDGV(dGVUsers);
            }
            else
            {
                //Прогрузка таблицы
                connect.LoadTable("FRIENDS", "SELECT F.iIdFriends, F.vName, U.vName FROM FRIENDS AS F, USERS AS U WHERE F.fk_iIdUsers = U.iIdUsers",
                    binSourceFriends, dGVFriends, bNavigatorChat);

                try
                {
                    dGVFriends.Columns[0].Visible = false;
                    dGVFriends.Columns[1].HeaderText = "Логин";
                    dGVFriends.Columns[2].HeaderText = "Кто друг";
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

                settingOfDGV(dGVFriends);
            }
        }

        //Кнопка навигатора "Удалить"
        private void bDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?",
                       "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Определяем какая вкладка была выбрана
                if (tabControlChat.SelectedTab == tabPageUsers)
                {
                    if (connect.QueryIsEmpty("SELECT * FROM USERS") == false)
                    {
                        MessageBox.Show("Все строки были удалены из базы", "Ошибка удаления!");
                    }
                    else
                    {
                        int iIdNow = 0;
                        //Определяем индекс выбранной строки
                        int i = dGVUsers.CurrentRow.Index;
                        iIdNow = Convert.ToInt32(dGVUsers[0, i].Value);
                        //Удаление строки
                        connect.QueryIsEmpty("DELETE FROM USERS WHERE iIdUsers = " + iIdNow);
                        //Зачем здесь эта строка? Во славу Сатане конечно :3
                        binSourceUsers.RemoveAt(i);
                        //Прогрузка таблицы
                        connect.LoadTable("USERS", "SELECT * FROM USERS", binSourceUsers, dGVUsers, bNavigatorChat);
                    }
                }
                else
                {
                    if (connect.QueryIsEmpty("SELECT * FROM FRIENDS") == false)
                    {
                        MessageBox.Show("Все строки были удалены из базы", "Ошибка удаления!");
                    }
                    else
                    {
                        int iIdNow = 0;
                        //Определяем индекс выбранной строки
                        int i = dGVFriends.CurrentRow.Index;
                        iIdNow = Convert.ToInt32(dGVFriends[0, i].Value);
                        //Удаление строки
                        connect.QueryIsEmpty("DELETE FROM FRIENDS WHERE iIdFriends = " + iIdNow);
                        //Зачем здесь эта строка? Во славу Сатане конечно :3
                        binSourceFriends.RemoveAt(i);
                        //Прогрузка таблицы
                        connect.LoadTable("FRIENDS", "SELECT F.iIdFriends, F.vName, U.vName FROM FRIENDS AS F, USERS AS U WHERE F.fk_iIdUsers = U.iIdUsers",
                            binSourceFriends, dGVFriends, bNavigatorChat);
                    }
                }
            }
        }

        //Происходит при смене вкладки
        private void tabControlChat_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //Определяем какая вкладка была выбрана
            if (tabControlChat.SelectedTab == tabPageUsers)
            {
                //Прогрузка таблицы
                connect.LoadTable("USERS", "SELECT * FROM USERS", binSourceUsers, dGVUsers, bNavigatorChat);

                try
                {
                    dGVUsers.Columns[0].Visible = false;
                    dGVUsers.Columns[1].HeaderText = "Логин";
                    dGVUsers.Columns[2].HeaderText = "Пароль";
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

                settingOfDGV(dGVUsers);
            }
            else
            {
                //Прогрузка таблицы
                connect.LoadTable("FRIENDS", "SELECT F.iIdFriends, F.vName, U.vName FROM FRIENDS AS F, USERS AS U WHERE F.fk_iIdUsers = U.iIdUsers", 
                    binSourceFriends, dGVFriends, bNavigatorChat);

                try
                {
                    dGVFriends.Columns[0].Visible = false;
                    dGVFriends.Columns[1].HeaderText = "Логин";
                    dGVFriends.Columns[2].HeaderText = "Кто друг";
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

                settingOfDGV(dGVFriends);
            }
        }
    }
}