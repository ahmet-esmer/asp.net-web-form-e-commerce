using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public  class Fonksiyonlar
    {

        public static string OdemeTuru(int odemeTipi)
        {
            string siparisTuru = "";
            switch (odemeTipi)
            {
                case 1: siparisTuru = "Kredi Kartı"; break;
                case 2: siparisTuru = "Havale"; break;
                case 3: siparisTuru = "Kapıda Ödeme"; break;
            }

            return siparisTuru;
        }

        public static string SiparisDurum(int siparisDurum)
        {
            string strReturn = "";
            switch (siparisDurum)
            {
                case 0: strReturn = "Onay Bekliyor"; break;
                case 1: strReturn = "Sipariş hazırlanıyor"; break;
                case 2: strReturn = "Kargoya Verildi"; break;
                case 3: strReturn = "İptal edildi"; break;
                case 4: strReturn = "Ödeme onayı Bekleniyor"; break;
                case 5: strReturn = "Tedarik edilemedi"; break;
                case 6: strReturn = "İptal edilecek"; break;
                case 7: strReturn = "Tedarik sürecinde"; break;
                case 8: strReturn = "Tedarik edilemiyor"; break;
            }
            return strReturn;
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

        public static string KategoriCizgiDrop(string serial)
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
    }
}
