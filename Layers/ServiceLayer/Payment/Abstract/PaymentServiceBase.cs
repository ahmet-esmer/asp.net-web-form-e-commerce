using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceLayer.Messages;
using ModelLayer;

namespace ServiceLayer.Payment.Abstract
{
    public abstract class PaymentServiceBase
    {
        public virtual BankaApi bankaApi { get; set; }

        public abstract PaymentMessage PeymentCore(BankRequest bankRequest);
    }
}
