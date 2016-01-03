using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLayer;
using ModelLayer.BankService;

namespace PaymentService
{
    public abstract class PaymentServiceBase
    {
        public virtual BankaApi bankaApi { get; set; }

        public abstract PaymentMessage PeymentCore(BankRequest bankRequest, KrediKart kart);
    }
}
