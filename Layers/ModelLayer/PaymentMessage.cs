using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelLayer
{
    public class PaymentMessage
    {
        public bool Success { get; set; }
        public string OnayKodu { get; set; }
        public string ReferansNo { get; set; }
        public string RedMesaj { get; set; }
        public string RedMesajKodu { get; set; }
    }
}
