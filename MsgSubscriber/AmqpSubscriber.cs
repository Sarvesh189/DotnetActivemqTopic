//using System;
//using System.Threading;
//using Apache.NMS;
//using Apache.NMS.AMQP;
//using Apache.NMS.AMQP.Message;
//using Apache.NMS.Policies;

//namespace MsgSubscriber
//{
//    public class Subscriber
//    {
//        private static string brokerUrl = "amqp://137.135.73.173:5672?transport.prefetch=1";
//        private static string topicName = "eurofins.ecommerce.dop.orderStatusUpdaterTopic";
//        public static void ReceiveMsg()
//        {
//            Apache.NMS.IConnection con = null;
//            try
//            {
//                var factory = new NmsConnectionFactory(brokerUrl);
//                factory.UserName = "admin";
//                factory.Password = "admin";
//                factory.ClientId = "MsgApp-Receiver1";
//                factory.SendTimeout = 300000; //5 mins

//                var rpolicy = new RedeliveryPolicy();
//                rpolicy.InitialRedeliveryDelay = 500;
//                rpolicy.MaximumRedeliveries = 5;
//                rpolicy.BackOffMultiplier = 2;
              
//            //    rpolicy.RedeliveryDelay(5000);

//                rpolicy.UseExponentialBackOff = true;

//              // factory.RedeliveryPolicy = rpolicy;

                

//                using (con = factory.CreateConnection())
//                {
//                    (con as NmsConnection).RedeliveryPolicy = rpolicy;
                   
//                    //con.AcknowledgementMode = AcknowledgementMode.ClientAcknowledge;
//                    using (var session = con.CreateSession(AcknowledgementMode.ClientAcknowledge))
//                    {
//                        var dest = session.GetTopic(topicName);
                        
//                        var durableConsumer =  session.CreateDurableConsumer(dest, "durable-eol-orderMsgSubscriber1", null, false);
                        
//                        con.Start();

//                        durableConsumer.Listener += (msg) =>
//                        {
                            
//                           Console.WriteLine("durable-eol-orderMsgSubscriber1: "+((NmsTextMessage)msg).Text);
//                            if ((msg as NmsTextMessage).Text.StartsWith("0"))
//                            { 
//                                msg.Acknowledge(); 
//                            }
//                            else if ((msg as NmsTextMessage).NMSRedelivered)
//                            {
//                                Console.WriteLine("redelivered msg");

//                               // msg.Acknowledge();
//                            }
//                            else 
//                            {                        
//                                session.Recover(); 
//                            }
                            

//                        };
//                        Console.ReadKey();
//                        durableConsumer.Close();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                string emsg = ex.Message;

//            }
//            finally
//            {
//                con.Dispose();
//            }
//        }
//        public static void ReceiveMsg2()
//        {
//            Apache.NMS.IConnection con = null;
//            try
//            {
//                var factory = new NmsConnectionFactory(brokerUrl);
//                factory.UserName = "admin";
//                factory.Password = "admin";
//                factory.ClientId = "MsgApp-Receiver2";
//                factory.SendTimeout = 300000; //5 mins
//                using (con = factory.CreateConnection())
//                {
//                    using (var session = con.CreateSession(Apache.NMS.AcknowledgementMode.ClientAcknowledge))
//                    {
//                        var dest = session.GetTopic(topicName);
                        
//                        var durableConsumer = session.CreateDurableConsumer(dest, "durable-eol-orderMsgSubscriber2", null, false);
                     
//                        con.Start();

               
//                        durableConsumer.Listener += (msg) =>
//                        {
//                            Console.WriteLine("durable - eol - orderMsgSubscriber2: "+((ITextMessage)msg).Text);
//                            msg.Acknowledge();
                           
//                        };
//                        Console.ReadKey();
//                        durableConsumer.Close();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                string emsg = ex.Message;

//            }
//            finally
//            {
//                con.Dispose();
//            }
//        }


//    }
//}
