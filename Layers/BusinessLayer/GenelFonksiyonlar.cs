using System;
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    public class GenelFonksiyonlar
    {

        public static bool IsNumber(string text)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(text);
        }

        public static string StokDurum(int kiritkStok, int id, string stokCins)
        {
            string deger = null;

            if (Convert.ToInt32(id) <= Convert.ToInt32(kiritkStok))
            {
                deger = "<span style='color:#d31213;'  >" + id.ToString() + " " + stokCins.ToString() + " </span>";
            }
            else
            {

                deger = "<span style='color:#11be24;'  > " + id.ToString() + " " + stokCins.ToString() + " </span>";
            }

            return deger.ToString();
        }

        public static string StokDurumUrunDetay(int kiritkStok, int urunStok)
        {
            string deger = null;

            if (Convert.ToInt32(urunStok) == 0)
            {
                deger = "<span style='color:#d2063f;' >Stok Bilgisi Alınız</span>";
            }
            else if (urunStok <= kiritkStok)
            {
                deger = "<span style='color:#d2063f;' title='Bu Ürün Kritik Stokta' >Stoklarımızda Mevcuttur</span>";
            }
            else
            {
                deger = "<span>Stoklarımızda Mevcuttur</span>";
            }

            return deger.ToString();
        }

        public static string UrunAdiKesme(string urunAdi)
        {
            string urunAdi_1 = null;

            int uzunluk = urunAdi.Length;


            if (uzunluk <= 22)
            {
                urunAdi_1 = "<span >" + urunAdi + " </span>";

            }
            else
            {
                urunAdi_1 = "<span >" + urunAdi.Substring(0, 22) + " ... </span>";
            }


            return ToTitleCase(urunAdi_1);
        }

        public static string KategoriLinkKesme(string katAdi)
        {
            string urunAdi_1 = null;

            int uzunluk = katAdi.Length;


            if (uzunluk <= 24)
            {
                urunAdi_1 = katAdi;

            }
            else
            {
                urunAdi_1 = katAdi.Substring(0, 24) + "..";
            }


            return urunAdi_1;
        }

        public static string UrunAdiSergiKesme(string urunAdi)
        {
            string urunAdi_1 = null;

            int uzunluk = urunAdi.Length;


            if (uzunluk <= 32)
            {
                urunAdi_1 = urunAdi;

            }
            else
            {
                urunAdi_1 = "<span >" + urunAdi.Substring(0, 32) + "... </span>";
            }


            return urunAdi_1;
        }

        public static string ZiyaretciDefteriKes(string yorum)
        {
            if (yorum.Length <= 60)
            {
                return yorum;
            }
            else
            {
                return yorum.Substring(0, 60) + "...<br/> <span class='devam'> yazının devamı</span>";
            }
        }

        public static string SSSorularKes(string yorum)
        {
            if (yorum.Length <= 60)
            {
                return yorum;
            }
            else
            {
                return yorum.Substring(0, 60) + "...";
            }
        }

        public static string ToTitleCase(string inputString)
        {
            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(inputString.ToLower());
        }

        public static string SiraDurum(int durum)
        {
            string sonuc = "<img src='images/upArrow.gif' border='0' />";
            if (durum <= 1)
            {
                sonuc = " ";
            }
            return sonuc.ToString();
        }

        public static int AnketDivGenislik(decimal uzunluk, decimal toplamOy, decimal anketOy)
        {
            decimal sonuc = (uzunluk / toplamOy) * anketOy;

            return Convert.ToInt32(sonuc);
        }

        public static decimal AnketOylanma(decimal yuzde, decimal toplamOy, decimal anketOy)
        {

            decimal sonuc = (yuzde / toplamOy) * anketOy;
            sonuc = Math.Round(sonuc, 1);
            return sonuc;
        }

        public static string DurumKontrol(int durum)
        {
            string sonuc = "<img src='images/pasif.gif' border='0' />";
            if (durum == 1)
            {
                sonuc = "<img src='images/aktiv.gif' border='0' />";
            }
            return sonuc.ToString();
        }

        public static string DurumYazi(int durum)
        {
            string sonuc = "Aktif Yap";
            if (durum == 1)
            {
                sonuc = "Pasif Yap";
            }
            return sonuc.ToString();
        }

        public static string EnterDonusturBr(string str)
        {
            return str.Replace("\r\n", "<br />");

        }

        public static string BRDonusturEnter(string str)
        {
            return str.Replace("<br />", "\r\n");

        }

        public static string BRDonusturEnter1(string str)
        {
            return str.Replace("<br />", "");

        }

        public static string FjkEditorKarekter(string deger)
        {
            string str_1;

            str_1 = deger.Replace("&ccedil;", "ç");
            str_1 = str_1.Replace("&Ccedil;", "Ç");
            str_1 = str_1.Replace("&uuml;", "ü");
            str_1 = str_1.Replace("&Uuml;", "Ü");
            str_1 = str_1.Replace("&ouml;", "ö");
            str_1 = str_1.Replace("&Ouml;", "Ö");
            return str_1;
        }

        public static string SearchKeyword(string deger)
        {
            string key;
            key = deger.Replace("ÅŸ", "ş");
            key = key.Replace("Å", "Ş");
            key = key.Replace("Ş ", "Ş");
            key = key.Replace("Þ", "Ş");
            key = key.Replace("Ã¼", "ü");
            key = key.Replace("Ãœ", "Ü");
            key = key.Replace("ÄŸ", "ğ");
            key = key.Replace("Ä", "Ğ");
            key = key.Replace("Ğ ", "Ğ");
            key = key.Replace("Ä±", "ı");
            key = key.Replace("Ä°", "İ");
            key = key.Replace("Ğ±", "ı");
            key = key.Replace("Ğ°", "İ");
            key = key.Replace("Ã§", "ç");
            key = key.Replace("Ã‡", "Ç");
            key = key.Replace("Ã¶", "ö");
            key = key.Replace("Ã–", "Ö");
            key = key.Replace("==", "");
            key = key.Replace("/", " / ");
            key = key.Replace("+", " + ");
            key = key.Replace("ý", "ı");
            key = key.Replace("Ý", "İ");
            return key;
        }

        public static string KategoriCizgi(string serial)
        {
            string sonuc = string.Empty;

            if (Convert.ToInt32(serial.ToString().Length) == 3)
            {
                sonuc = "<b>";
            }
            else if (Convert.ToInt32(serial.ToString().Length) == 6)
            {
                sonuc = "-- ";
            }
            else if (Convert.ToInt32(serial.ToString().Length) == 9)
            {
                sonuc = "---- ";
            }
            else if (Convert.ToInt32(serial.ToString().Length) == 12)
            {
                sonuc = "------ ";
            }
            else if (Convert.ToInt32(serial.ToString().Length) == 15)
            {
                sonuc = "-------- ";
            }
            else if (Convert.ToInt32(serial.ToString().Length) == 18)
            {
                sonuc = "---------- ";
            }
            else if (Convert.ToInt32(serial.ToString().Length) == 21)
            {
                sonuc = "------------ ";
            }

            return sonuc.ToString();
        }

        public static string KategoriCizgiDropdown(string serial)
        {
            string sonuc = string.Empty;

            if (Convert.ToInt32(serial.ToString().Length) == 3)
            {
                sonuc = "";
            }
            else if (Convert.ToInt32(serial.ToString().Length) == 6)
            {
                sonuc = "-- ";
            }
            else if (Convert.ToInt32(serial.ToString().Length) == 9)
            {
                sonuc = "---- ";
            }
            else if (Convert.ToInt32(serial.ToString().Length) == 12)
            {
                sonuc = "------ ";
            }
            else if (Convert.ToInt32(serial.ToString().Length) == 15)
            {
                sonuc = "-------- ";
            }
            else if (Convert.ToInt32(serial.ToString().Length) == 18)
            {
                sonuc = "---------- ";
            }
            else if (Convert.ToInt32(serial.ToString().Length) == 21)
            {
                sonuc = "------------ ";
            }

            return sonuc.ToString();
        }

        public static Boolean GecerliMailAdresi(string s)
        {
            try
            {
                Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                return regex.IsMatch(s);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
