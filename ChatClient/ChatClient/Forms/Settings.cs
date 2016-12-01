using ChatClient.Classes;
using System;
using System.IO;
using System.Windows.Forms;

namespace ChatClient.Forms
{
    public partial class Settings : Form
    {
        public Connector connect;
        public Settings(Connector connect)
        {
            InitializeComponent();
            this.connect = connect;
        }

        //Кнопка "Настроить подключение"
        private void bAddConnection_Click(object sender, EventArgs e)
        {
            if (tbDatabase.Text.Equals("") || (tbServer.Text.Equals("")))
            {
                MessageBox.Show("Вы не ввели все данные!", "Предупреждение!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string sConnectionString;
                sConnectionString = "Database=" + tbDatabase.Text + ";Server=" + tbServer.Text +
                    ";Trusted_Connection=True;MultipleActiveResultSets=True";

                StreamWriter writer = new StreamWriter(@"Data\ChatDBConfig.cfg");
                writer.WriteLine(sConnectionString);
                writer.Close();
                writer.Dispose();
                MessageBox.Show("Данные были сохранены!", "Созданно!");
                LoadScreen loadSreen = new LoadScreen(connect);
                loadSreen.Show();
                this.Close();
            }
        }

        //Кнопка "Отмена"
        private void bCancel_Click(object sender, EventArgs e)
        {
            LoadScreen loadSreen = new LoadScreen(connect);
            loadSreen.Show();
            Close();
        }

        //Кнопка "В главное меню"
        private void bReturnToMain_Click(object sender, EventArgs e)
        {
            Close();
            Main main = new Main(connect);
            main.Show();
        }
    }
}