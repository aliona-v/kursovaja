using ChatClient.Classes;
using System;
using System.Windows.Forms;

namespace ChatClient.Forms
{
    public partial class AddEdit : Form
    {
        //Переменные для работы с таблицами
        public string sNameOfTable, sNameOfFriend;
        public bool bAdd = true;
        public int iMaxIdTable = 0, idIdEditTable = 0, iIdFriendsEdit = 0;

        public Connector connect;
        public AddEdit(Connector connect)
        {
            InitializeComponent();
            this.connect = connect;
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

        //Загрузка формы
        private void AddEdit_Load(object sender, EventArgs e)
        {
            //Загрузка таблицы в зависимости от того, какая кнопка в главном меню была нажата
            if (sNameOfTable == "USERS")
            {
                //Дача имени лейблам
                lb1.Text = "Введите имя пользователя:";
                lb2.Text = "Введите пароль пользователя:";

                //Если добавляем
                if (bAdd == true)
                {
                    //Дача имени форме
                    this.Text = "Добавление нового пользователя";
                }
                else
                {
                    //Дача имени форме
                    Text = "Редактирование существующего пользователя";
                }
            }
            else
            {
                //Дача имени лейблам
                lb1.Text = "Выберите имя пользователя:";
                lb2.Text = "Выберите имя друга:";
                tB1.Visible = false;
                comboB1.Visible = true;
                tB2.Visible = false;
                comboB2.Visible = true;

                //Если добавляем
                if (bAdd == true)
                {
                    //Дача имени форме
                    this.Text = "Добавление нового друга";
                    //Прогрузка комбобоксов данными из таблицы
                    connect.ComboBoxLoad("SELECT vName FROM USERS", comboB1, "vName");
                    connect.ComboBoxLoad("SELECT vName FROM USERS", comboB2, "vName");
                }
                else
                {
                    //Дача имени форме
                    Text = "Редактирование существующего друга";
                    connect.ComboBoxLoad("SELECT vName FROM USERS WHERE iIdUsers = " + iIdFriendsEdit, comboB1, "vName");
                    connect.ComboBoxLoad("SELECT vName FROM USERS", comboB2, "vName");

                    comboB2.Text = sNameOfFriend;
                }
            }
        }

        //Кнопка "Отмена"
        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Кнопка "Подтвердить"
        private void bOkay_Click(object sender, EventArgs e)
        {
            //Загрузка таблицы в зависимости от того, какая кнопка в главном меню была нажата
            if (sNameOfTable == "USERS")
            {
                //Проверка на пустоту вводимых полей
                if (tB1.Text.Equals("") || tB2.Text.Equals(""))
                {
                    MessageBox.Show("Не все данные были введены!!!", "Внимание!!!");
                }
                else
                {
                    //Если true, то добавляем
                    if (bAdd == true)
                    {
                        //Вызов процедуры получения максимального ID из таблицы, актоинкремент
                        iMaxIdTable = iMaxIdInTable("SELECT * FROM USERS", "SELECT MAX(iIdUsers) FROM USERS");

                        string sQueryAdd = "INSERT INTO USERS(iIdUsers, vName, vPassword) VALUES("
                            + iMaxIdTable + ", '" + tB1.Text + "','" + tB2.Text + "');";

                        connect.QueryIsEmpty(sQueryAdd);
                    }
                    //Редактируем
                    else
                    {
                        string sQueryEdit = "UPDATE USERS SET vName = '" + tB1.Text + "', vPassword = '" + tB2.Text
                            + "' WHERE iIdUsers = " + idIdEditTable + ";";

                        connect.QueryIsEmpty(sQueryEdit);

                        //Закрываем форму
                        this.Close();
                    }
                }
            }
            else
            {
                //Проверка на пустоту вводимых полей
                if (comboB1.Text.Equals("") || comboB2.Text.Equals(""))
                {
                    MessageBox.Show("Не все данные были введены!!!", "Внимание!!!");
                }
                else
                {
                    //Если true, то добавляем
                    if (bAdd == true)
                    {
                        //Проверка на дружбу с самим собой
                        if(comboB1.Text == comboB2.Text)
                        {
                            MessageBox.Show("Нельзя дружить с самим собой\nМы же не психи, правда? :)","Сообщение!");

                            return;
                        }
                        else
                        {
                            string sIdUsers = connect.GetData("SELECT iIdUsers FROM USERS WHERE vName = '" + comboB2.Text + "'; ");

                            //Вызов процедуры получения максимального ID из таблицы, актоинкремент
                            iMaxIdTable = iMaxIdInTable("SELECT * FROM FRIENDS", "SELECT MAX(iIdFriends) FROM FRIENDS");

                            //Строка проверки
                            string sCheckQuery = "SELECT * FROM FRIENDS WHERE vName = '" + comboB1.Text + "' AND fk_iIdUsers = " + sIdUsers;

                            //Проверка, есть ли у данного пользователя уже этот друг
                            if (connect.QueryIsEmpty(sCheckQuery) == true)
                            {
                                //Сообщение, предупреждение
                                MessageBox.Show(comboB2.Text + ", уже есть в друзьях у " + comboB1.Text + " !", "Сообщение");
                                //Выходим из процедуры нажатия на кнопку
                                return;
                            }
                            else
                            {
                                string sQueryAdd = "INSERT INTO FRIENDS(iIdFriends, vName, fk_iIdUsers) VALUES("
                               + iMaxIdTable + ", '" + comboB1.Text + "','" + sIdUsers + "');";

                                connect.QueryIsEmpty(sQueryAdd);
                            }
                        }                                             
                    }
                    //Редактируем
                    else
                    {
                        string sIdUsers = connect.GetData("SELECT iIdUsers FROM USERS WHERE vName = '" + comboB2.Text + "'; ");

                        string sQueryEdit = "UPDATE FRIENDS SET vName = '" + comboB1.Text + "', fk_iIdUsers = '" + sIdUsers
                            + "' WHERE iIdFriends = " + idIdEditTable + ";";

                        connect.QueryIsEmpty(sQueryEdit);

                        //Закрываем форму
                        this.Close();
                    }
                }
            }
        }
    }
}