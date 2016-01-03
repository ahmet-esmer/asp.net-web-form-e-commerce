using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceLayer.Payment.Abstract;
using ModelLayer;
using _PosnetDotNetModule;

namespace ServiceLayer.Payment.Concrete
{
    public class YapiKrediPayment : PaymentServiceBase
    {

        public YapiKrediPayment(BankaApi api)
        {
            base.bankaApi = api;
        }

        public override PaymentMessage PeymentCore(ModelLayer.BankRequest bankRequest)
        {
            PaymentMessage mesaj = new PaymentMessage();

            C_Posnet posnetObj = new C_Posnet();
            posnetObj.SetMid(bankaApi.ClientId);
            posnetObj.SetTid(bankaApi.ApiName);
            posnetObj.SetURL(bankaApi.HostName);

            // Yapı Kredi Bilgi Formatı
            string tarihYil = bankRequest.KrediKart.Yil.Substring(2) + bankRequest.KrediKart.Ay;

            // Yapı Kredi Toplam Fiyat İşlemi
            string ykbtaksitToplam = bankRequest.TaksitToplam.ToString("c");
            ykbtaksitToplam = ykbtaksitToplam.Replace("TL", "");
            ykbtaksitToplam = ykbtaksitToplam.Replace(",", "");
            ykbtaksitToplam = ykbtaksitToplam.Replace(".", "");

            string taksit = null;
            if (bankRequest.Taksit.ToString() == "01")
            {
                taksit = "00";
            }
            else
            {
                taksit = bankRequest.Taksit.ToString();
            }

            bankRequest.KrediKart.AdSoyad = string.Format("{0}-", BusinessLayer.UrlTR.Replace(bankRequest.KrediKart.AdSoyad));

            //Ad Soyad için kalan alan
            int nNameLastIndex = (23 - bankRequest.SiparisNo.Length);

            for (int i = bankRequest.KrediKart.AdSoyad.Length; i < nNameLastIndex; i++)
            {
                bankRequest.KrediKart.AdSoyad += "0"; 
            }

            if (bankRequest.KrediKart.AdSoyad.Length > nNameLastIndex)
            {
                bankRequest.KrediKart.AdSoyad = bankRequest.KrediKart.AdSoyad.Substring(0, nNameLastIndex);
            }

            bankRequest.SiparisNo = string.Format("{0}-{1}", bankRequest.KrediKart.AdSoyad, bankRequest.SiparisNo);

            Boolean baglanti = posnetObj.DoSaleTran(bankRequest.KrediKart.No, tarihYil, bankRequest.KrediKart.CV2, bankRequest.SiparisNo, ykbtaksitToplam, "YT", taksit, "", "");

            if (baglanti == false)
            {
                mesaj.RedMesaj = "Banka ile Bağlantı Kurulamadı, Lütfen tekrar deneyiniz.";
                mesaj.RedMesajKodu = "yapikerediBaglanti";
            }
            if (posnetObj.GetApprovedCode() == "1")
            {
                if (posnetObj.GetAuthcode().ToString().Trim() != "" && posnetObj.GetHostlogkey().ToString().Trim() != "")
                {
                    mesaj.Success = true;
                    mesaj.OnayKodu = posnetObj.GetAuthcode();
                    mesaj.ReferansNo = posnetObj.GetHostlogkey();
                }
            }
            else if (posnetObj.GetApprovedCode() == "2")
            {
                mesaj.RedMesaj = "Kredi kartı ile ödeme  işlemini daha önce yapıldı. <br/> Resptext: " + posnetObj.GetResponseText();
                mesaj.RedMesajKodu = posnetObj.GetResponseCode();
            }
            else
            {
                #region Yapı Kredi Hata Mesajları
                string hataKodu = posnetObj.GetResponseCode();
                string hataMesaji = posnetObj.GetResponseText();
                mesaj.RedMesajKodu = posnetObj.GetResponseCode();

                if (hataKodu.Trim() == "0095")
                {
                    mesaj.RedMesaj = "* Hata Mesajı: " + posnetObj.GetResponseText() + "<br/>* Kart bilgilerinden ( KK No, SKT, CVV) biri yada birkaçı hatalı girilmiş veya Worldcard'lar için bankaca tanımlanmış günlük limitler aşılmış olabilir.";
                }
                else if (hataKodu.Trim() == "150")
                {
                    mesaj.RedMesaj = "* Yanlış CVC no hatası.";
                }
                else if (hataKodu.Trim() == "0213")
                {
                    mesaj.RedMesaj = "* Hata Mesajı: " + hataMesaji + "<br/>* Kartın bakiyesi yetersiz. Kartı veren bankayı arayın.";
                }
                else if (hataKodu.Trim() == "0220")
                {
                    mesaj.RedMesaj = "* Hata Mesajı: " + hataMesaji + "<br/>* Taksitli işlemler ancak 1 YTL'nin üstündeki tutarlarla yapılabilirler.";
                }
                else if (hataKodu.Trim() == "0225")
                {
                    mesaj.RedMesaj = "* Hata Mesajı: " + hataMesaji + "<br/>* Kart no hatalı";
                }
                else if (hataKodu.Trim() == "0400")
                {
                    mesaj.RedMesaj = "* Hata Mesajı: " + hataMesaji + "<br/>* Posnet sunucu teknik bir sorun yaşıyor. Lütfen tekrar deneyin.";
                }
                else if (hataKodu.Trim() == "0551")
                {
                    mesaj.RedMesaj = "* Hata Mesajı: " + hataMesaji + "<br/>* Numara bir kredi kartına ait değil.";
                }
                else if (hataKodu.Trim() == "131")
                {
                    mesaj.RedMesaj = "* Hata Mesajı: " + hataMesaji + "<br/>* Kart no bilgisi hiçbir boşluk içermeden 4912065000461139 şeklinde gönderilmelidir.";
                }
                else if (hataKodu.Trim() == "139")
                {
                    mesaj.RedMesaj = "* Hata Mesajı: " + hataMesaji + "<br/>* Kredi kartı güvenlik numarası (CVC) parametre hatalı.";
                }
                else if (hataKodu.Trim() == "0800")
                {
                    mesaj.RedMesaj = "* Resptext: " + hataMesaji + "<br/>* işlemde kullanılan kredi kartının YKB provizyon sisteminde tutulan çalıntı kredi kartı listesinde bulunmasıdır.";
                }
                else if (hataKodu.Trim() == "0876")
                {
                    mesaj.RedMesaj = "* Hata Mesajı: " + hataMesaji + "<br/>* Kart bilgilerinden ( KK No, SKT, CVV) biri yada birkaçı hatalı girilmiş veya Worldcard'lar için bankaca tanımlanmış günlük limitler aşılmış olabilir.";
                }
                else if (hataKodu.Trim() == "0877")
                {
                    mesaj.RedMesaj = "* CVC no girilmeli";
                }
                else if (hataKodu.Trim() == "0995")
                {
                    mesaj.RedMesaj = "* Hata Mesajı: " + hataMesaji + "<br/>* Kartı veren  banka ile iletişimde zaman aşımı oldu (bankadan zamanında yanıt alınamadı). Lütfen Tekrar deneyin.";
                }
                else if (hataKodu.Trim() == "0100")
                {
                    mesaj.RedMesaj = "* Resptext: " + hataMesaji + "<br/>* Banka sistemlerimizde anlık sorunlar olduğundan. Lütfen tekrar deneyin.";

                }
                else if (hataKodu.Trim() == "0173")
                {
                    mesaj.RedMesaj = "* Hata Mesajı: " + hataMesaji + "<br/>*  işlemde kullanılan kredi kartının internetten işlem yapma yetkisi bulunmamaktadır. <br/> Kredi kartını aldığı bankanın kredi kartları servisiyle görüşüp kredi kartını e-ticarette kullanamadığını belirtiniz.";
                }
                else
                {
                    mesaj.RedMesaj =  "* Ödeme İşlemi Yapılırken Bankadan Hata Mesajı Döndü.<br/> Hata Mesajı: " + hataMesaji + "<br/>Hata kodu: " + hataKodu;
                }
                #endregion
            }
            return mesaj;
        }
    }
}
