using Apache.NMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsgSubscriber
{
    class OrderStatusUpdaterRedeliveryPolicy : IRedeliveryPolicy
    {
        public int CollisionAvoidancePercent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseCollisionAvoidance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int InitialRedeliveryDelay { get ; set ; }
        public int MaximumRedeliveries { get; set; }
        public bool UseExponentialBackOff { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int BackOffMultiplier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public int RedeliveryDelay(int redeliveredCounter)
        {
            throw new NotImplementedException();
        }
    }
}
