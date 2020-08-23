using System;
using System.Threading;

namespace MsgSubscriber
{
    class Program
    {
       
        static void Main(string[] args)
        { 
            Console.WriteLine("***********************************");
            Console.WriteLine("******Message Subscribers************");
            Console.WriteLine("***********************************");

            Thread t1 = new Thread(new ThreadStart(TcpSubscriber.ReceiveMsg));
            t1.IsBackground = true;
            Thread t2 = new Thread(new ThreadStart(TcpSubscriber.ReceiveMsg2));
            t2.IsBackground = true;
            t1.Start();
            t2.Start();

            Console.WriteLine("Press enter to exit");

            Console.ReadLine();

        }
    }
}
