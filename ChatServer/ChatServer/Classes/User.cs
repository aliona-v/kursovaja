using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace ChatServer.Classes
{
    public class User
    {
        //Поток для пользователей
        private Thread _userThread;
        //Переменная хранящая название переменной
        private string _userName;
        //Переменаня для аутентификации успеха
        private bool AuthSuccess = false;
        //Процедура получения имено пользователя
        public string Username
        {
            get { return _userName; }
        }
        //Обьявление сокета дял пользователя
        private Socket _userHandle;
        //Процедура получения данных для сокета
        public User(Socket handle)
        {
            _userHandle = handle;
            //Передаем уже созданному эксепляру класса Thread новые значения
            _userThread = new Thread(listner);
            //Делаем поток фоновым
            _userThread.IsBackground = true;
            //Открываем поток
            _userThread.Start();
        }
        //Процедура чтения данных
        private void listner()
        {
            try
            {
                //До тех, пор, пока соединение открыто
                while (_userHandle.Connected)
                {
                    byte[] buffer = new byte[2048];
                    //Получение данных от соединенное сокета
                    int bytesReceive = _userHandle.Receive(buffer);
                    handleCommand(Encoding.Unicode.GetString(buffer, 0, bytesReceive));
                }
            }
            catch { Server.EndUser(this); }
        }
        //Процедура (сетер) установка имени пользователя
        private bool setName(string Name)
        {
            //Тут можно добавить различные проверки
            _userName = Name;
            Server.NewUser(this);
            AuthSuccess = true;
            return true;
        }
        //Процедура обработки команд
        private void handleCommand(string cmd)
        {
            try
            {
                //Метод возвращающий массив string с присутствующими в данном экземпляре подстроками внутри
                string[] commands = cmd.Split('#');
                //Определение длинны строки
                int countCommands = commands.Length;
                //Массив ограниченный длинной строки
                for (int i = 0; i < countCommands; i++)
                {
                    //Текущая i-команда
                    string currentCommand = commands[i];
                    //Если соддержит эту строку, не пустая и не содержит null
                    if (string.IsNullOrEmpty(currentCommand))
                    {
                        continue;
                    }                        
                    //Если успешно, смена в тру
                    if (!AuthSuccess)
                    {
                        //Проверка, содержит ли строка подстроку 'setname'
                        if (currentCommand.Contains("setname"))
                        {
                            if (setName(currentCommand.Split('|')[1]))
                                Send("#setnamesuccess");
                            else
                                Send("#setnamefailed");
                        }
                        continue;
                    }
                    //Проверка, содержит ли строка подстроку 'yy'
                    if (currentCommand.Contains("yy"))
                    {
                        string id = currentCommand.Split('|')[1];
                        Server.FileD file = Server.GetFileByID(int.Parse(id));
                        if (file.ID == 0)
                        {
                            SendMessage("Ошибка при передаче файла...", "1");
                            continue;
                        }
                        Send(file.fileBuffer);
                        Server.Files.Remove(file);
                    }
                    //Проверка, содержит ли строка подстроку 'message'
                    if (currentCommand.Contains("message"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        Server.SendGlobalMessage($"[{_userName}]: {Arguments[1]}", "Black");

                        continue;
                    }//sENDfile
                    //...
                    //Проверка, содержит ли строка подстроку 'endsession'
                    if (currentCommand.Contains("endsession"))
                    {
                        Server.EndUser(this);
                        return;
                    }
                    //Проверка, содержит ли строка подстроку 'sendfileto'
                    if (currentCommand.Contains("sendfileto"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        string TargetName = Arguments[1];
                        int FileSize = int.Parse(Arguments[2]);
                        string FileName = Arguments[3];
                        byte[] fileBuffer = new byte[FileSize];
                        _userHandle.Receive(fileBuffer);
                        User targetUser = Server.GetUser(TargetName);
                        if (targetUser == null)
                        {
                            SendMessage($"Пользователь {FileName} не найден!", "Black");
                            continue;
                        }
                        Server.FileD newFile = new Server.FileD()
                        {
                            ID = Server.Files.Count + 1,
                            FileName = FileName,
                            From = Username,
                            fileBuffer = fileBuffer,
                            FileSize = fileBuffer.Length
                        };
                        Server.Files.Add(newFile);
                        targetUser.SendFile(newFile, targetUser);
                    }
                    //Проверка, содержит ли строка подстроку 'private'
                    if (currentCommand.Contains("private"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        string TargetName = Arguments[1];
                        string Content = Arguments[2];
                        User targetUser = Server.GetUser(TargetName);
                        if (targetUser == null)
                        {
                            SendMessage($"Пользователь {TargetName} не найден!", "Red");
                            continue;
                        }
                        SendMessage($"-[Отправлено][{TargetName}]: {Content}", "Black");
                        targetUser.SendMessage($"-[Получено][{Username}]: {Content}", "Black");
                        continue;
                    }

                }//Конец for

            }
            catch (Exception exp) { Console.WriteLine("Error with handleCommand: " + exp.Message); }
        }
        //Процедура посылки файла
        public void SendFile(Server.FileD fd, User To)
        {
            byte[] answerBuffer = new byte[48];
            Console.WriteLine($"Sending {fd.FileName} from {fd.From} to {To.Username}");
            To.Send($"#gfile|{fd.FileName}|{fd.From}|{fd.fileBuffer.Length}|{fd.ID}");
        }
        //Процедура поссылки сообщения
        public void SendMessage(string content, string clr)
        {
            Send($"#msg|{content}|{clr}");
        }
        //Поссылка данных сокету, тип входные данных массив байт
        public void Send(byte[] buffer)
        {
            try
            {
                _userHandle.Send(buffer);
            }
            catch { }
        }
        //Поссылка данных сокету, тип данных строка
        public void Send(string Buffer)
        {
            try
            {
                _userHandle.Send(Encoding.Unicode.GetBytes(Buffer));
            }
            catch { }
        }
        //Процедура закрытия соединения
        public void End()
        {
            try
            {
                _userHandle.Close();
            }
            catch { }

        }

    }//Конец класса Server
}