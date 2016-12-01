using ChatClient.Classes;
using System;
using System.IO;
using System.Windows.Forms;

namespace ChatClient.Forms
{
    public partial class LoadScreen : Form
    {
        public Connector connect;
        public LoadScreen(Connector connect)
        {
            InitializeComponent();
            this.connect = connect;
        }

        //Загрузка формы
        private void LoadScreen_Load(object sender, EventArgs e)
        {
            ConLabel.Text = "Загрузка приложения...";
            timer1.Interval = 1000;
            timer1.Start();
        }

        //Загрузка таймера
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Если существует файл настроек подключения
            if (File.Exists(@"Data\ChatDBConfig.cfg"))
            {
                StreamReader sreader = new StreamReader(@"Data\ChatDBConfig.cfg");

                string ConnectionString = sreader.ReadLine(); //читаем шифрованные данные о подключении в переменную 

                sreader.Close();
                sreader.Dispose();
                //Если возврат true
                if (connect.OpenConnection(ConnectionString) == true)
                {
                    timer1.Stop();
                    LogIn logIn = new LogIn(connect);
                    logIn.Show();
                    this.Hide();
                }
                else
                {
                    ConLabel.Text = "Ошибка подключения...";
                    timer1.Stop();
                    if (MessageBox.Show("Не удалось подключиться к базе данных, сервер выключен или неправильно указаны данные соединения.\nНастроить подключение?",
                        "Ошибка", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Settings Stngs = new Settings(connect);
                        Stngs.Show();
                        //Return to Main
                        Stngs.bReturnToMain.Enabled = false;
                        //Cancel
                        Stngs.bCancel.Enabled = true;
                        this.Hide();
                    }
                    else
                    {
                        Application.Exit();
                        connect.CloseConnection();
                    }
                }
            }
            else
            {
                StreamWriter wreader = new StreamWriter(@"Data\ChatDBConfig.cfg");
                wreader.Close();
            }
        }
    }
}