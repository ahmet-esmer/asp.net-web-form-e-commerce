using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLayer;
using ModelLayer.BankService;

namespace PaymentService
{
    public class PaymentManager
    {
        private PaymentServiceBase paymentService;
       
        public PaymentMessage Process(BankRequest bankRequest, KrediKart krediKart)
        {
            paymentService = PeymentServiceFactory.GetPaymentServiceFrom(bankRequest.BankaId, bankRequest.Taksit);
            return paymentService.PeymentCore(bankRequest, krediKart);
        }

    }
}
