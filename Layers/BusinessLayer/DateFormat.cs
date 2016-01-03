using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public static class DateFormat
    {
        public static string TarihSaat(string zaman)
        {
            DateTime tarih = DateTime.Parse(zaman);
            string zamanSon = tarih.ToString("dd MMMM yyyy ddd HH:mm");
            return zamanSon;
        }

        public static string TarihSaatSani(string zaman)
        {
            DateTime tarih = DateTime.Parse(zaman);
            string zamanSon = tarih.ToString("dd MMMM yyyy ddd HH:mm:ss");
            return zamanSon;
        }

        public static string TarihSaat(DateTime zaman)
        {
            return zaman.ToString("dd MMMM yyyy HH:mm");
        }

        public static string TarihSaatSiparis(string zaman)
        {
            DateTime tarih = DateTime.Parse(zaman);
            string zamanSon = tarih.ToString("dd MMMM yyyy HH:mm");
            return zamanSon;
        }

        public static string TarihSaatSiparis(DateTime zaman)
        {
            return zaman.ToString("dd MMMM yyyy HH:mm");
        }

        public static string TarihGun(DateTime zaman)
        {
            return zaman.ToString("dd MMMM yyyy ddddd");
        }

        public static string TarihGun(string zaman)
        {
            DateTime tarih = DateTime.Parse(zaman);
            string zamanSon = tarih.ToString("dd MMMM yyyy ddddd");
            return zamanSon;
        }

        public static string Tarih(string zaman)
        {
            DateTime tarih = DateTime.Parse(zaman);
            string zamanSon = tarih.ToString("dd MMMM yyyy");
            return zamanSon;
        }
    }
}
