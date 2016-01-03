using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public static class SiparisOperasyon
    {

        public static string BirimToplam(decimal fiyat, int miktar)
        {
            decimal toplam = 0;
            toplam = fiyat * miktar;
            return toplam.ToString("c");
        }

        public static string KDVDahilFiyat(decimal fiyat, int urunKDV)
        {
            decimal toplam = 0;
            toplam = (fiyat * urunKDV / 100) + fiyat;
            return toplam.ToString("c");
        }

    }
}
