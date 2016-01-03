using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BusinessLayer
{

    public static class LinkBulding
    {
       
        public static string Icerik(string sayfaLink, int id, string kategoriAdi)
        {
            if (sayfaLink == "")
            {
                return sayfaLink = string.Format("~/icerik/{0}/{1}", id.ToString(), UrlTR.Replace(kategoriAdi.ToLower()));
            }
            else
            {
                if (sayfaLink.Contains("default"))
                    return "~/";

               return sayfaLink += "/" + id.ToString();
            }
        }

        public static string Urun(string kategori, int id, string urunAdi)
        {
            return string.Format("~/{0}/{1}/{2}", UrlTR.Replace(kategori), id, UrlTR.Replace(urunAdi));
        }

        public static string Kategori(object title, object kategori, object serial)
        {
            return string.Format("~/{0}/lens-{1}/{2}", UrlTR.Replace(kategori.ToString()),
                serial, UrlTR.Replace(title.ToString()));
        }

        public static string Marka(object id, object marka)
        {
            return string.Format("~/Markalar/{0}/{1}", id, UrlTR.Replace(marka.ToString()));
        }

        public static string MarkaAndParam(object title, object kategori, object serial, object markaId)
        {
            return string.Format("~/{0}/lens-{1}/{2}?markaId={3}", 
                 UrlTR.Replace(kategori.ToString()), serial, UrlTR.Replace(title.ToString()), markaId );
        }


        public static string Hediye(int hediyeId, string title)
        {
            return String.Format("{0}/kampanya-{1}", UrlTR.Replace(title), hediyeId);
        }
    }
}