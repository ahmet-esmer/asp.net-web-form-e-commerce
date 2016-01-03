using System;
using DataAccessLayer;
using ModelLayer;
using PaymentService.Concrete;

namespace PaymentService
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
                else
	            {
                    throw new ApplicationException("Payment Service:Desteklenmeyen bankakodu "+ api.BankaKodu );
	            }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
