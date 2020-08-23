using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.Policies;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsgApp
{
    class TcpProducer
    {
      
        public void PublishMsgToTopic(string brokerUrl, string topicName)
        {
            Apache.NMS.IConnection con = null;
            ITextMessage msg = null;
            try
            {
                var factory = new ConnectionFactory(brokerUrl);
//                factory.PrefetchPolicy = new PrefetchPolicy() { DurableTopicPrefetch = 1, TopicPrefetch = 1 };        
                  
                con = factory.CreateConnection("admin", "admin");

               

                con.ClientId = "OrderMsgProducer-1";
                con.RequestTimeout = TimeSpan.FromMinutes(5);

                con.Start();

                using (var session = con.CreateSession(Apache.NMS.AcknowledgementMode.ClientAcknowledge))
                {

                    var dest = (IDestination)session.GetTopic(topicName);

                    var prod = session.CreateProducer(dest);
                    prod.DeliveryMode = MsgDeliveryMode.NonPersistent;
                    prod.TimeToLive = TimeSpan.FromMinutes(20);
                    do
                    {
                        Console.WriteLine("Please enter msg");
                        var textMsg = Console.ReadLine();
                        msg = prod.CreateTextMessage(textMsg);
                        msg.Properties.SetLong("x-opt-delivery-delay", 10000);
                        prod.Send(msg);
                        msg.ClearBody();
                        Console.WriteLine("Please press esc key to exit.");

                    } while (Console.ReadKey().Key != ConsoleKey.Escape);
                    prod.Close();
                }

            }
            catch (Exception ex)
            {
                string emsg = ex.Message;

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
    }
}
