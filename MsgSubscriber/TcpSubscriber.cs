using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS.Policies;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsgSubscriber
{
    class TcpSubscriber
    {
        private static string brokerUrl = "activemq:tcp://137.135.73.173:61616";
        private static string topicName = "eurofins.ecommerce.dop.orderStatusUpdaterTopic";
        public static void ReceiveMsg()
        {
            Apache.NMS.IConnection con = null;
            try
            {
                var factory = new ConnectionFactory(brokerUrl);
                factory.UserName = "admin";
                factory.Password = "admin";
                factory.ClientId = "MsgApp-Receiver1";
                factory.PrefetchPolicy = new PrefetchPolicy() { DurableTopicPrefetch = 1, TopicPrefetch = 1 };

                var rpolicy = new RedeliveryPolicy();
                rpolicy.InitialRedeliveryDelay = 5000;
                rpolicy.MaximumRedeliveries = 5;
                rpolicy.UseExponentialBackOff = false;

                using (con = factory.CreateConnection("admin","admin"))
                {
                    con.RedeliveryPolicy = rpolicy;


                    //con.AcknowledgementMode = AcknowledgementMode.ClientAcknowledge;
                    using (var session = con.CreateSession(AcknowledgementMode.ClientAcknowledge))
                    {
                        var dest = session.GetTopic(topicName);

                        var durableConsumer = session.CreateDurableConsumer(dest, "durable-eol-orderMsgSubscriber1", null, false);

                        con.Start();
                        

                        durableConsumer.Listener += (msg) =>
                        {

                            Console.WriteLine("durable-eol-orderMsgSubscriber-1: " + ((ActiveMQTextMessage)msg).Text);
                            if ((msg as ActiveMQTextMessage).Text.StartsWith("0"))
                            {
                                msg.Acknowledge();
                            }
                           else
                            {
                                session.Recover();
                            }


                        };
                        Console.ReadKey();
                        durableConsumer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string emsg = ex.Message;

            }
            finally
            {
                con.Dispose();
            }
        }
        public static void ReceiveMsg2()
        {
            Apache.NMS.IConnection con = null;
            try
            {
                var factory = new ConnectionFactory(brokerUrl);
                factory.UserName = "admin";
                factory.Password = "admin";
                factory.ClientId = "MsgApp-Receiver2";
              //  factory.SendTimeout = 300000; //5 mins
                using (con = factory.CreateConnection())
                {
                    using (var session = con.CreateSession(Apache.NMS.AcknowledgementMode.ClientAcknowledge))
                    {
                        var dest = session.GetTopic(topicName);

                        var durableConsumer = session.CreateDurableConsumer(dest, "durable-eol-orderMsgSubscriber2", null, false);

                        con.Start();


                        durableConsumer.Listener += (msg) =>
                        {
                            Console.WriteLine("durable-eol-orderMsgSubscriber-2: " + ((ITextMessage)msg).Text);
                            msg.Acknowledge();

                        };
                        Console.ReadKey();
                        durableConsumer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string emsg = ex.Message;

            }
            finally
            {
                con.Dispose();
            }
        }


    }

}
