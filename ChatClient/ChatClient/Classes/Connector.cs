using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ChatClient.Classes
{
    public class Connector
    {
        //Строка подключения
        public SqlConnection conn;

        public bool CheckException;

        public DataTable dataTable;
        public SqlDataAdapter adap;
        public DataSet ds;
        public SqlCommandBuilder commBuild;

        //Бинсорсы для таблиц
        public BindingSource binSourceUsers = new BindingSource();
        public BindingSource binSourceFriends = new BindingSource();

        //Процедура коннекта к базе
        public bool OpenConnection(string Connection)
        {
            try
            {
                conn = new SqlConnection(Connection);
                conn.Open();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Процедура закрытия коннекта
        public bool CloseConnection()
        {
            conn.Close();
            return true;
        }

        //Процедура загрузки таблицы
        public void LoadTable(string Name_Table, string queryString, BindingSource binSource, DataGridView dataGrid, BindingNavigator Navigator)
        {
            try
            {
                adap = new SqlDataAdapter(queryString, conn);
                ds = new DataSet();
                adap.Fill(ds, Name_Table);
                binSource.DataSource = ds.Tables[0];
                Navigator.BindingSource = binSource;
                dataGrid.DataSource = binSource;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Вернет true, если есть записи
        public bool QueryIsEmpty(string queryString)
        {
            dataTable = new DataTable();
            SqlCommand com;
            SqlDataReader dataReader;
            com = new SqlCommand(queryString, conn);
            try
            {
                dataReader = com.ExecuteReader();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                CheckException = true;
                return false;
            }
            if (dataReader.HasRows)
            {
                dataTable.Load(dataReader);

                dataReader.Close();
                com.Dispose();
                return true;
            }
            dataReader.Close();
            com.Dispose();
            CheckException = false;
            return false;
        }

        //Процедура вывода в комбо бокса столбца таблицы
        public bool ComboBoxLoad(string queryString, ComboBox comboBox, string Name_Column)
        {
            dataTable = new DataTable();
            SqlCommand com;
            SqlDataReader dataReader;
            com = new SqlCommand(queryString, conn);
            try
            {
                dataReader = com.ExecuteReader();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return false;
            }
            if (dataReader.HasRows)
            {
                dataTable.Load(dataReader);
                comboBox.DataSource = dataTable;
                comboBox.DisplayMember = Name_Column;

                dataReader.Close();
                com.Dispose();
                return true;
            }
            dataReader.Close();
            com.Dispose();
            return false;
        }

        //Процедура вывода в лист столбца таблицы
        public bool ListBoxLoad(string queryString, ListBox listBox, string Name_Column)
        {
            dataTable = new DataTable();
            SqlCommand com;
            SqlDataReader dataReader;
            com = new SqlCommand(queryString, conn);
            try
            {
                dataReader = com.ExecuteReader();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return false;
            }

            if (dataReader.HasRows)
            {
                dataTable.Load(dataReader);

                listBox.DataSource = dataTable;
                listBox.ValueMember = Name_Column;

                dataReader.Close();
                com.Dispose();
                return true;
            }
            dataReader.Close();
            com.Dispose();

            return false;
        }

        //Процедура для агрегатных запросов
        public string GetData(string queryString)
        {
            int iResultQuery = 0;
            string resultQuery = "";
            SqlCommand com;
            SqlDataReader dataReader;
            com = new SqlCommand(queryString, conn);
            try
            {
                dataReader = com.ExecuteReader();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return resultQuery;
            }

            dataReader.Read();

            //Пытаемся читать значение которое пришло, обычно это стринг, если нет, то ловим исключение
            try
            {
                resultQuery = dataReader.GetString(0);
            }
            //Поймали исключение и читаем не текст, а число
            catch (Exception)
            {
                //Необработанное исключение типа "System.InvalidOperationException" в System.Data.dll
                iResultQuery = dataReader.GetInt32(0);
                resultQuery = Convert.ToString(iResultQuery);
            }

            dataReader.Close();
            com.Dispose();
            return resultQuery;
        }
    }
}