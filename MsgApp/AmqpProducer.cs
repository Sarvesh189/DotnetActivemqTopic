//using Amqp;
//using Apache.NMS;
////using Apache.NMS.AMQP;
//using Apache.NMS.Policies;
//using Apache.NMS.ActiveMQ;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Apache.NMS.AMQP;
//using Apache.NMS.AMQP.Message;

//namespace MsgApp
//{
//   public class AmqpProducer
//    {
//        public void PublishMsgToTopic(string brokerUrl, string topicName)
//        {
//            Apache.NMS.IConnection con = null;
//            ITextMessage msg = null;
//            try
//            {
//                   var factory =new  NmsConnectionFactory(brokerUrl);
//                //  factory.UserName = "admin";
//                //   factory.Password = "admin";
//                //    factory.ClientId = "MsgAppProducer-1";
//                //    factory.SendTimeout = 300000; //5 mins
//             //  var factory = NMSConnectionFactory.CreateConnectionFactory(new Uri(brokerUrl), null);
//                 con = factory.CreateConnection();
//              //  var rpolicy = new RedeliveryPolicy();
//              //  rpolicy.InitialRedeliveryDelay = 1000;
//               // rpolicy.MaximumRedeliveries = 3;
//              //  rpolicy.RedeliveryDelay(2000);
//               // rpolicy.UseExponentialBackOff = false;
//                con.ClientId = "OrderMsgProducer-1";
//                con.RequestTimeout = TimeSpan.FromMinutes(1);

//                //                factory.RedeliveryPolicy = rpolicy;
//               // con.RedeliveryPolicy = rpolicy;
//                con.Start();

//                using (var session = con.CreateSession(Apache.NMS.AcknowledgementMode.ClientAcknowledge))
//                {
                    
//                    var dest = (IDestination)session.GetTopic(topicName);
              
//                    var prod = session.CreateProducer(dest);
//                    prod.DeliveryMode = MsgDeliveryMode.NonPersistent;
//                    prod.TimeToLive = TimeSpan.FromMinutes(20);
//                    do
//                    {
//                        Console.WriteLine("Please enter msg");
//                        var textMsg = Console.ReadLine();
//                        msg = prod.CreateTextMessage(textMsg);
//                        msg.Properties.SetLong("x-opt-delivery-delay", 10000);
//                        prod.Send(msg);
//                        msg.ClearBody();                        
//                        Console.WriteLine("Please press esc key to exit.");

//                    } while (Console.ReadKey().Key != ConsoleKey.Escape);
//                    prod.Close();
//                }
               
//            }
//            catch (Exception ex)
//            {
//                string emsg = ex.Message;

//            }
//            finally
//            {
//                con.Close();
//                con.Dispose();
//            }
//        }
//    }
//}
