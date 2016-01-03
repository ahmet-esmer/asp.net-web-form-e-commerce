using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceLayer.Payment.Abstract;
using ServiceLayer.Messages;
using ModelLayer;

namespace ServiceLayer.Payment
{
    public class PaymentManager
    {
        private PaymentServiceBase paymentService;

        public PaymentMessage Process(BankRequest bankRequest)
        {
            paymentService = PeymentServiceFactory.GetPaymentServiceFrom(bankRequest.BankaId, bankRequest.Taksit);
            return paymentService.PeymentCore(bankRequest);
        }

    }
}
