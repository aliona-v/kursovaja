using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace ChatServer.Classes
{
    public static class Server
    {
        //Лист структур FileD
        public static List<FileD> Files = new List<FileD>();
        //Структура содержащая данные
        public struct FileD
        {
            public int ID;
            public string FileName;
            public string From;
            public int FileSize;
            public byte[] fileBuffer;
        }
        public static int CountUsers = 0;
        public delegate void UserEvent(string Name);
        //Событие конента пользователя
        public static event UserEvent UserConnected = (Username) =>
        {
            Console.WriteLine($"User {Username} connected.");
            CountUsers++;
            SendGlobalMessage($"Пользователь {Username} подключился к чату.", "Black");
            SendUserList();
        };
        //Событие расконекта пользователя
        public static event UserEvent UserDisconnected = (Username) =>
        {
            Console.WriteLine($"User {Username} disconnected.");
            CountUsers--;
            SendGlobalMessage($"Пользователь {Username} отключился от чата.", "Black");
            SendUserList();
        };
        //Лист пользователей
        public static List<User> UserList = new List<User>();
        //Обьявление сокета сервера
        public static Socket ServerSocket;
        //Неизменяемая переменная для хранения адреса сервера
        public const string Host = "127.0.0.1";
        //Неизменяемая переменная для хранения адреса порта
        public const int Port = 2222;
        //Статичная переменная состояния работы
        public static bool Work = true;

        //Процедура получения файла по ID
        public static FileD GetFileByID(int ID)
        {
            int countFiles = Files.Count;
            for (int i = 0; i < countFiles; i++)
            {
                if (Files[i].ID == ID)
                    return Files[i];
            }
            return new FileD() { ID = 0 };
        }
        //Процедура добавления нового пользователя
        public static void NewUser(User usr)
        {
            if (UserList.Contains(usr))
                return;
            UserList.Add(usr);
            UserConnected(usr.Username);
        }
        //Процедура удаления пользователя
        public static void EndUser(User usr)
        {
            if (!UserList.Contains(usr))
                return;
            UserList.Remove(usr);
            usr.End();
            UserDisconnected(usr.Username);

        }
        //Процедура получения имени юзера
        public static User GetUser(string Name)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                if (UserList[i].Username == Name)
                    return UserList[i];
            }
            return null;
        }
        //Процедура добавления юзера в лист
        public static void SendUserList()
        {
            string userList = "#userlist|";

            for (int i = 0; i < CountUsers; i++)
            {
                userList += UserList[i].Username + ",";
            }

            SendAllUsers(userList);
        }
        //Процедура поссылки глобального сообщения
        public static void SendGlobalMessage(string content, string clr)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                UserList[i].SendMessage(content, clr);
            }
        }
        //Процедура поссылки сообщения всем юзерам, тип входных данных массив байт
        public static void SendAllUsers(byte[] data)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                UserList[i].Send(data);
            }
        }
        //Процедура поссылки сообщения всем юзерам, тип входных данных строка
        public static void SendAllUsers(string data)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                UserList[i].Send(data);
            }
        }


    }
}