using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Task1
{
    public class Chat
    {
        public static void UdpReceiver()
        {
            IPEndPoint receiverEndPoint = new IPEndPoint(IPAddress.Any, 0);
            UdpClient udpClient = new UdpClient(12345);
            Console.WriteLine("Receiver is waiting for messages...");

            while (true)
            {
                try
                {
                    byte[] bytes = udpClient.Receive(ref receiverEndPoint);
                    string message = Encoding.UTF8.GetString(bytes);
                    Message? newMessage = Message.GetMessage(message);

                    if (newMessage != null)
                    {
                        Console.WriteLine(newMessage);

                        // Отправка подтверждения получения сообщения
                        Message acceptMessage = new Message("Сервер", "Сообщение получено!");
                        string jsonMsg = acceptMessage.GetJson();
                        byte[] respondBytes = Encoding.UTF8.GetBytes(jsonMsg);
                        udpClient.Send(respondBytes, receiverEndPoint);
                    }
                    else
                        Console.WriteLine("Somthing went wrong with message!");

                }
                catch (Exception e) 
                {
                    Console.WriteLine(e.Message);
                }
            }
            
        }

        public static void UdpSender(string name)
        {
            IPEndPoint senderEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
            UdpClient udpClient = new UdpClient();            

            while (true)
            {
                Console.WriteLine("Введите сообщение:");
                string? messageText = Console.ReadLine();

                if (String.IsNullOrEmpty(messageText)) break;
                
                Message newMessage = new Message(name, messageText);
                string jsonMsg = newMessage.GetJson();

                byte[] bytes = Encoding.UTF8.GetBytes(jsonMsg);
                udpClient.Send(bytes, senderEndPoint);

                // Получение подтверждения получения сообщения
                byte[] acceptBytes = udpClient.Receive(ref senderEndPoint);
                string message = Encoding.UTF8.GetString(acceptBytes);
                Message? acceptMessage = Message.GetMessage(message);
                Console.WriteLine(acceptMessage);                
            }
        }
    }
}
