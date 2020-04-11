using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore
{
    class SubscriptionModel
    {
        public int SubID { get; set; }
        public string SubDescr { get; set; }
        public string SubName { get; set; }
        public int SubPeriodDay { get; set; }
        public double SubPrice { get; set; }
    }
}
