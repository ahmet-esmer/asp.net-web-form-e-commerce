using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public class AritmetikIslemler
    {

        // Üründe İndirim Varsa Gerçek Fiyat Üstü Çizilecek
        public static string UrunFiyatIndirim(decimal urunFiyat, decimal uIndirimFiyat, string doviz)
        {
            string deger = null;
            //string gecici = null;

            switch (doviz)
            {
                case "TL": doviz = "TL"; break;
                case "EURO": doviz = "€"; break;
                case "USD": doviz = "$"; break;
            }


            if (Convert.ToDecimal(uIndirimFiyat) == 0)
            {
                deger = "<span  class='fiyat' >" + urunFiyat.ToString("N") + " " + doviz + " </span>";
            }
            else
            {
                deger = "<span  class='fiyatIndirim' >" + urunFiyat.ToString("N") + " " + doviz + " </span>";
            }

            return deger.ToString();
        }

        // Üründe İndirim Varsa Gerçek Fiyat Üstü Çizilecek kdv bilgisi
        public static string UrunFiyatIndirim(decimal urunFiyat, decimal uIndirimFiyat, string doviz, int kdv)
        {
            string deger = null;
            string strKDV = null;

            switch (doviz)
            {
                case "TL": doviz = "TL"; break;
                case "EURO": doviz = "€"; break;
                case "USD": doviz = "$"; break;
            }

            if (kdv == 0)
                strKDV = "  KDV Dahil ";
            else
                strKDV = " +  KDV ";


            if (Convert.ToDecimal(uIndirimFiyat) == 0)
            {
                deger = "<span  class='fiyat' >" + urunFiyat.ToString("N") + " " + doviz + strKDV + " </span>";
            }
            else
            {
                deger = "<span  class='fiyatIndirim' >" + urunFiyat.ToString("N") + " " + doviz + strKDV + " </span>";
            }

            return deger.ToString();
        }

        // Ürün İndirimi Varsa Görüntülenecek Yoksa Gizlenecek
        public static string UrunFiyatIndirimVarmi(decimal urunFiyat, decimal uIndirimFiyat, string doviz)
        {
            string deger = null;

            switch (doviz)
            {
                case "TL": doviz = "TL"; break;
                case "EURO": doviz = "€"; break;
                case "USD": doviz = "$"; break;
            }

            if (Convert.ToDecimal(uIndirimFiyat) == 0)
            {
                deger = " ";
            }
            else
            {
                deger = "<span class='fiyat' > " + uIndirimFiyat.ToString("N") + " " + doviz + " </span>";
            }
            return deger.ToString();
        }

        // Ürün İndirimi Varsa Görüntülenecek Yoksa Gizlenecek
        public static string UrunFiyatIndirimVarmi(decimal urunFiyat, decimal uIndirimFiyat, string doviz, int kdv)
        {
            string deger = null;
            string strKDV = null;
            switch (doviz)
            {
                case "TL": doviz = "TL"; break;
                case "EURO": doviz = "€"; break;
                case "USD": doviz = "$"; break;
            }

            if (kdv == 0)
                strKDV = "  KDV Dahil ";
            else
                strKDV = " +  KDV ";

            if (Convert.ToDecimal(uIndirimFiyat) == 0)
            {
                deger = " ";
            }
            else
            {
                deger = "<span class='fiyat' > " + uIndirimFiyat.ToString("N") + " " + doviz + strKDV + " </span>";
            }
            return deger.ToString();
        }


        // Üründe İndirim Varsa Gerçek Fiyat Üstü Çizilecek 2. metod
        // Ürün gösterim sayfasında ürün gerçek fiyatı, indirimli fiyatı, doviz kur fiyatı
        public static string UrunFiyatlari(string urunFiyat, string uIndirimFiyat, string doviz, string kdv)
        {
            StringBuilder sb = new StringBuilder();
            decimal kurFiyati = 0;
            decimal UrunFiyat = Convert.ToDecimal(urunFiyat);
            decimal UIndirimFiyat = Convert.ToDecimal(uIndirimFiyat);
            string dovizHam = doviz;

            switch (doviz)
            {
                case "TL": doviz = "TL"; break;
                case "EURO": doviz = "€"; break;
                case "USD": doviz = "$"; break;
            }

            if (kdv == "0")
                kdv = "  KDV Dahil ";
            else
                kdv = " +  KDV ";


            if (UIndirimFiyat == 0)
            {
                sb.Append("<div  class='fiyatAsil1' >Fiyatı:</div>");
                sb.Append("<div  class='fiyatAsil2' >" + UrunFiyat.ToString("N") + " " + doviz + kdv + " </div>");
            }
            else
            {
                sb.Append("<div class='fiyatIndirim1' >Fiyatı:</div>");
                sb.Append("<div class='fiyatIndirim2' >" + UrunFiyat.ToString("N") + " " + doviz + kdv + " </div>");

                sb.Append("<div class='urunFiyatIndirim1' >İndirmli:</div>");
                sb.Append("<div class='urunFiyatIndirim2' >");
                sb.Append(UIndirimFiyat.ToString("N") + " " + doviz + kdv);
                sb.Append("</div>");
            }


            if (dovizHam != "TL")
            {
                kurFiyati = urunKurFiyati(UrunFiyat, UIndirimFiyat, dovizHam);
                sb.Append("<div  class='dovizTl' >" + kurFiyati.ToString("C") + " </div>");
            }


            return sb.ToString();
        }


        // Kdv Durumu
        public static string UrunKDV(int urunKDV)
        {
            string deger = null;

            if (urunKDV == 0)
            {
                deger = "  KDV Dahil ";
            }
            else
            {
                deger = " +  KDV ";
            }
            return deger.ToString();
        }

        // Sepet İçin Kdv Miktarını yazma
        public static string sepetKDV(int urunKDV)
        {
            string deger = null;

            if (urunKDV == 0)
            {
                deger = "<span  class=''>  KDV Dahil </span>";
            }
            else
            {
                deger = "<span  class=''> % " + urunKDV + "</span>";
            }
            return deger.ToString();
        }


        // Ürün Taksit Seçenekleri için Toplam Fiyat  Decimal Haliyle dönüş 
        public static decimal UrunKDVDahilFiyat(decimal urunFiyat, decimal uIndirimFiyat, int urunKDV)
        {
            decimal deger = 0;
            decimal kdv = 0;
            decimal toplam = 0;

            if (Convert.ToDecimal(uIndirimFiyat) == 0)
            {
                kdv = urunFiyat * urunKDV / 100;
                toplam = urunFiyat + kdv;
                deger = toplam;
            }
            else
            {
                kdv = uIndirimFiyat * urunKDV / 100;
                toplam = uIndirimFiyat + kdv;
                deger = toplam;
            }

            return deger;
        }

        //public static decimal AdminUrunKDVDahilFiyat(decimal urunFiyat, int adet, int urunKDV, string kampanya)
        //{
        //    decimal deger = 0;
        //    decimal kdv = 0;
        //    decimal toplam = 0;

        //    // Gerçek Fiyat
        //    adet = kamapanyaMiktar(Convert.ToInt32(kampanya), adet);

        //    kdv = urunFiyat * urunKDV / 100;
        //    toplam = urunFiyat + kdv;
        //    deger = toplam * adet;

        //    return deger;
        //}

       

        // Ürün Taksit Seçenekleri Hesaplama

        public static decimal[] urunTaksitHesaplama(decimal fiyat, double taksitVade, int taksitMiktari)
        {
            decimal[] taksitDizi = new decimal[2];

            decimal taksitFarki = 0;
            decimal toplam = 0;
            decimal taksit = 0;

            taksitFarki = fiyat * Convert.ToDecimal(taksitVade) / 100;
            toplam = fiyat + taksitFarki;
            taksit = toplam / taksitMiktari;

            taksitDizi[0] = taksit;
            taksitDizi[1] = toplam;

            return taksitDizi;
        }


        // Havale Fiyatı Hesaplama
        public static string UrunKDVDahilHavaleFiyat(decimal urunFiyat, decimal uIndirimFiyat, int urunKDV, int havaleIndirim)
        {
            string deger = null;
            decimal kdv = 0;
            decimal toplam = 0;
            decimal havaleToplam = 0;
            decimal havale = 0;

            if (Convert.ToDecimal(uIndirimFiyat) == 0)
            {
                kdv = urunFiyat * urunKDV / 100;
                toplam = urunFiyat + kdv;

                havale = toplam * havaleIndirim / 100;
                havaleToplam = toplam - havale;

                deger = "<span  class=''  > " + string.Format("{0:C}", havaleToplam) + " (kdv Dahil) </span>";
            }
            else
            {
                kdv = uIndirimFiyat * urunKDV / 100;
                toplam = uIndirimFiyat + kdv;

                havale = toplam * havaleIndirim / 100;
                havaleToplam = toplam - havale;


                deger = "<span  class=''  > " + string.Format("{0:C}", havaleToplam) + " (kdv Dahil) </span>";
            }
            return deger.ToString();
        }


        // En yeni Ürün Listeleme Fiyat Bulma CSS İçin Farklı Fonksiyon oluşturudu.
        public static string panelFiyat(decimal urunFiyat, decimal uIndirimFiyat, string doviz)
        {
            string deger = null;
            string gecici = null;

            switch (doviz)
            {
                case "TL": doviz = "TL"; break;
                case "EURO": doviz = "€"; break;
                case "USD": doviz = "$"; break;
            }

            if (Convert.ToDecimal(uIndirimFiyat) == 0)
            {
                gecici = string.Format("{0:C}", urunFiyat);
            }
            else
            {
                gecici = string.Format("{0:C}", uIndirimFiyat);
            }

            deger = gecici.Replace("TL", doviz);
            return deger.ToString();
        }

        // Döviz Kur Fiyatı hesaplama
        public static decimal urunKurFiyati(decimal urunFiyat, decimal uIndirimFiyat, string doviz)
        {
            decimal DovizKur = 0;
            decimal donenDeger = 0;

            //SqlParameter[] dovizParam = new SqlParameter[1];

            //if (doviz == "USD")
            //{
            //    dovizParam[0] = new SqlParameter("@paremetre", "USD");
            //    DovizKur = (decimal)SqlHelper.ExecuteScalar(ConnectionString.Get, CommandType.StoredProcedure, "dovizKur_Getir", dovizParam);
            //}
            //else if (doviz == "EURO")
            //{
            //    dovizParam[0] = new SqlParameter("@paremetre", "EURO");
            //    DovizKur = (decimal)SqlHelper.ExecuteScalar(ConnectionString.Get, CommandType.StoredProcedure, "dovizKur_Getir", dovizParam);
            //}


            if (Convert.ToDecimal(uIndirimFiyat) == 0)
            {
                donenDeger = urunFiyat * DovizKur;
            }
            else
            {
                donenDeger = uIndirimFiyat * DovizKur;
            }

            if (doviz == "TL")
            {
                if (Convert.ToDecimal(uIndirimFiyat) == 0)
                    donenDeger = urunFiyat;
                else
                    donenDeger = uIndirimFiyat;
            }

            return donenDeger;
        }


        // Urun KDV Dahih Fiyat ve KDV  Sepet İÇin 
        public static decimal KDVDahilFiyatSepet(decimal urunFiyat, decimal uIndirimFiyat, int urunKDV, int miktar, string doviz, int kampanya)
        {
            decimal kdv = 0;
            decimal kdvToplam = 0;
            decimal toplam = 0;

            decimal gelenFiyat = urunKurFiyati(urunFiyat, uIndirimFiyat, doviz);

            miktar = kamapanyaMiktar(kampanya, miktar);

            kdv = gelenFiyat * urunKDV / 100;
            kdvToplam = kdv * miktar;

            toplam = (gelenFiyat * miktar) + kdvToplam;

            return toplam;
        }

        // Kamapany İşlemi
        public static int kamapanyaMiktar(int kampanya, int miktar)
        {
            int miktarRe = 0;

            if (kampanya == 0)
            {
                return miktar;
            }

            if (kampanya == 21)
            {
                if (miktar == 3)
                    miktarRe = 2;
                else if (miktar == 6)
                    miktarRe = 4;
                else
                    miktarRe = miktar;
            }
            else if (kampanya == 22)
            {
                if (miktar == 4)
                    miktarRe = 2;
                else if (miktar == 8)
                    miktarRe = 4;
                else
                    miktarRe = miktar;
            }
            else if (kampanya == 31)
            {
                if (miktar == 4)
                    miktarRe = 3;
                else if (miktar == 8)
                    miktarRe = 6;
                else
                    miktarRe = miktar;
            }

            return miktarRe;
        }

        // Sepet Birim Toplam
        public static decimal sepetBirimToplam(decimal urunFiyat, decimal uIndirimFiyat, int miktar, string doviz, int kampanya)
        {

            decimal toplam = 0;
            decimal gelenFiyat = urunKurFiyati(urunFiyat, uIndirimFiyat, doviz);

            miktar = kamapanyaMiktar(kampanya, miktar);

            toplam = gelenFiyat * miktar;

            return toplam;
        }

        public static decimal KDVOranıBulmaSepet(decimal urunFiyat, decimal uIndirimFiyat, int urunKDV, int miktar, string doviz, int kampanya)
        {
            decimal kdv = 0;
            decimal kdvToplam = 0;

            decimal gelenFiyat = urunKurFiyati(urunFiyat, uIndirimFiyat, doviz);

            miktar = kamapanyaMiktar(kampanya, miktar);

            kdv = gelenFiyat * urunKDV / 100;
            kdvToplam = kdv * miktar;

            return kdvToplam;
        }

        // Sepet İçin Ürün Fiyatınıbulma İndirim Varmı yokmu
        public static decimal UrunFiyatBulSepet(decimal urunFiyat, decimal uIndirimFiyat)
        {
            decimal deger = 0;

            if (Convert.ToDecimal(uIndirimFiyat) == 0)
            {
                deger = urunFiyat;
            }
            else
            {
                deger = uIndirimFiyat;
            }
            return deger;
        }

        public static int decimalConvertInt(decimal argument)
        {
            object Int32Value;
            object UInt32Value;

            // Convert the argument to an int value.
            try
            {
                Int32Value = decimal.ToInt32(argument);
            }
            catch (Exception ex)
            {
                Int32Value = GetExceptionType(ex);
            }

            // Convert the argument to a uint value.
            try
            {
                UInt32Value = decimal.ToUInt32(argument);
            }
            catch (Exception ex)
            {
                UInt32Value = GetExceptionType(ex);
            }

            return Convert.ToInt32(Int32Value);
        }

        public static string GetExceptionType(Exception ex)
        {
            string exceptionType = ex.GetType().ToString();
            return exceptionType.Substring(
                exceptionType.LastIndexOf('.') + 1);
        }

    }
}
