using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceLayer.Messages.Sepet;
using BusinessLayer;
using ModelLayer;
using DataAccessLayer;
using ServiceLayer.Messages;

namespace ServiceLayer.ExtensionMethods
{
    public static class IndirimExtensionMethod
    {
        public static List<UrunIndirimResponse> ConvertToIndirimResponse(this List<UrunIndirim> urunIndirim)
        {

            List<UrunIndirimResponse> indirimListe = new List<UrunIndirimResponse>();
            decimal indirim = 0;
            decimal kdv = 0;
            decimal fiyat = 0;

            foreach (UrunIndirim spt in urunIndirim)
            {
                UrunIndirimResponse response = new UrunIndirimResponse();

                response.Adet = string.Format("{0} {1}", spt.Adet, spt.StokCinsi);

                fiyat = spt.Fiyat;
                indirim = fiyat * spt.Oran / 100;
                fiyat -= indirim;

                kdv = fiyat * spt.KDV / 100;
                fiyat += kdv;

                response.Islem = string.Format("{0:N} x {1}", fiyat, spt.Adet);

                fiyat = spt.Fiyat * spt.Adet;
                indirim = fiyat * spt.Oran / 100;
                fiyat -= indirim;

                response.Indirim = indirim.ToString("C");

                kdv = fiyat * spt.KDV / 100;
                fiyat += kdv;

                response.Toplam = fiyat.ToString("C");

                indirimListe.Add(response);
            }

            return indirimListe;
        }
    }
}
