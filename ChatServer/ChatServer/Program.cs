using ChatServer.Classes;
using System;
using System.Net;
using System.Net.Sockets;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse(Server.Host);
            //Создание сокета
            Server.ServerSocket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //Связывает сокет с локальной конечной точкой для ожидания входящих запросов на соединение
            Server.ServerSocket.Bind(new IPEndPoint(address, Server.Port));
            //Помещает сокет в режим прослушивания (ожидания). Этот метод предназначен только для серверных приложений
            Server.ServerSocket.Listen(100);
            //Вывод сообщения на консоль
            Console.WriteLine($"Server has been started on {Server.Host}:{Server.Port}");
            //Вывод сообщения на консоль
            Console.WriteLine("Waiting connections...");
            //До тех пор пока сервер работает
            while (Server.Work)
            {
                //Создает новый сокет для обработки входящего запроса на соединение
                Socket handle = Server.ServerSocket.Accept();
                Console.WriteLine($"New connection: {handle.RemoteEndPoint.ToString()}");
                new User(handle);

            }
            //Вывод сообщения на консоль
            Console.WriteLine("Server closeing...");
        }
    }
}