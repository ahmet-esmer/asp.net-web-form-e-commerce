using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public static class SepetOperasyon
    {
        //Sipariş detay Kayıt Ürün indirm fiyatı bulamk
        public static decimal UrunFiyat(decimal urunFiyat, decimal uIndirimFiyat)
        {
            decimal deger = 0;

            if (uIndirimFiyat == 0)
            {
                deger = urunFiyat;
            }
            else
            {
                deger = uIndirimFiyat;
            }

            return deger;
        }


        public static string SagBilgiHtml(string sagBilgi)
        {
            return "<div class='goz'>Sağ</div><div class='goz1'>Goz:</div>" + sagBilgi;
        }

        public static string SolBilgiHtml(string solBilgi)
        {
            return "<div class='goz'>Sol</div><div class='goz1'>Goz:</div>" + solBilgi;
        }


        public static string FiyatVeParaBirimi(decimal urunFiyat,string doviz)
        {
            string deger = null;
            string gecici = null;

            switch (doviz)
            {
                case "TL": doviz = "TL"; break;
                case "EURO": doviz = "€"; break;
                case "USD": doviz = "$"; break;
            }

            gecici = string.Format("{0:C}", urunFiyat);

            deger = gecici.Replace("TL", doviz);
            return deger.ToString();
        }


    


    }
}
