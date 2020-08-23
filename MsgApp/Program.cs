using System;

namespace MsgApp
{
    class Program
    {
        private static string brokerUrl = "amqp://137.135.73.173:5672?transport.prefetch=1";
        private static string topicName = "eurofins.ecommerce.dop.orderStatusUpdaterTopic";

        private static string brokerUrl1 = "activemq:tcp://137.135.73.173:61616";

        static void Main(string[] args)
        {
            Console.WriteLine("***********************************");
            Console.WriteLine("******Message Producer!************");
            Console.WriteLine("***********************************");
            var producer = new TcpProducer();
            producer.PublishMsgToTopic(brokerUrl1, topicName);
        }
    }
}
