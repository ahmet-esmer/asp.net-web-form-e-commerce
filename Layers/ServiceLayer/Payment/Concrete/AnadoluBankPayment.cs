using System;
using ePayment;
using ModelLayer;
using ServiceLayer.Payment.Abstract;

namespace ServiceLayer.Payment.Concrete
{
    public class AnadoluBankPayment : PaymentServiceBase
    {

        public AnadoluBankPayment(BankaApi api)
        {
            base.bankaApi = api;
        }

        public override PaymentMessage PeymentCore(BankRequest bankRequest)
        {
            PaymentMessage mesaj = new PaymentMessage();
            try
            {
                cc5payment mycc5pay = new cc5payment();
                mycc5pay.host = bankaApi.HostName;
                mycc5pay.name = bankaApi.ApiName;
                mycc5pay.password = bankaApi.ApiPassword;
                mycc5pay.clientid = bankaApi.ClientId;
                mycc5pay.orderresult = 0;

                if (bankRequest.Taksit > 1)
                {
                    mycc5pay.taksit = bankRequest.Taksit.ToString();
                }

                mycc5pay.bname = bankRequest.KrediKart.AdSoyad;
                mycc5pay.sname = bankRequest.KrediKart.AdSoyad;

                mycc5pay.cardnumber = bankRequest.KrediKart.No;
                mycc5pay.cv2 = bankRequest.KrediKart.CV2;
                mycc5pay.expmonth = bankRequest.KrediKart.Ay;
                mycc5pay.expyear = bankRequest.KrediKart.Yil.Substring(2);

                mycc5pay.subtotal = bankRequest.TaksitToplam.ToString("N");
                mycc5pay.oid = bankRequest.SiparisNo;
                mycc5pay.currency = "949";
                mycc5pay.chargetype = "Auth";

                string result = mycc5pay.processorder();

                if (result == "1") // banka ile bağlantı sağlandı
                {
                    if (mycc5pay.appr == "Approved")
                    {
                        mesaj.Success = true;
                        mesaj.OnayKodu = mycc5pay.code;
                        mesaj.ReferansNo = mycc5pay.refno;
                    }
                    else
                    {
                        mesaj.RedMesaj = mycc5pay.errmsg;
                        mesaj.RedMesajKodu = mycc5pay.err;
                    }
                }
                else
                {
                    mesaj.RedMesaj = "Banka ile Bağlantı Kurulamadı, Lütfen tekrar deneyiniz.";
                    mesaj.RedMesajKodu = "anadoluBaglanti";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mesaj;
        }
    }
}
