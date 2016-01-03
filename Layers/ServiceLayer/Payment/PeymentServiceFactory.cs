using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceLayer.Payment.Abstract;
using ModelLayer;
using DataAccessLayer;
using ServiceLayer.Payment.Concrete;

namespace ServiceLayer.Payment
{
    public class PeymentServiceFactory
    {
        public static PaymentServiceBase GetPaymentServiceFrom(int bankId, int taksit)
        {
            try
            {
                BankaApi api = BankaApiDB.SanalPosBilgileri(bankId, taksit);

                if (api.BankaKodu == "AnadoluBank")
                {
                    return new AnadoluBankPayment(api);
                }
                else if (api.BankaKodu == "GarantiBank")
                {
                    return new GarantiBankPayment(api);
                }
                else if (api.BankaKodu == "YapiKredi")
                {
                    return new YapiKrediPayment(api);
                }
                else
                {
                    throw new ApplicationException("Payment Service:Desteklenmeyen bankakodu " + api.BankaKodu);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
