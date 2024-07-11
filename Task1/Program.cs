/// Домашнее задание к Семинару 1
/// Попробуйте переработать приложение, добавив подтверждение об отправке сообщений как в сервер, так и в клиент.

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Chat.UdpReceiver();

            }
            else
            {
                Chat.UdpSender(args[0]);
            }
        }
    }
}
