using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using LoggerLibrary;
using SecureCookie;

namespace BusinessLayer
{
    public class KullaniciOperasyon
    {
        public static Boolean LoginKontrol()
        {
            Boolean durum = false;
            try
            {
                if (HttpContext.Current.Request["LensOptikLogin"] != null)
                    durum = true;
            }
            catch (Exception ex)
            {
                LogManager.SqlDB.Write("Login Kontrol Hatası", ex);
                throw;
            }

            return durum;
        }

        public static int GetId()
        {
         int Id = 0;

         try
         {
             if (HttpContext.Current.Request["LensOptikLogin"] != null)
             {
                 HttpCookie kullanici = HttpContext.Current.Request.Cookies["LensOptikLogin"];
                 kullanici = HttpSecureCookie.Decode(kullanici);
                 Id = Convert.ToInt32(kullanici["kullaniciId"]);
             }
         }
         catch (Exception)
         {
             throw;
         }

         return Id;
        }

        public static string GetName()
        {
            string name = " ";
            try
            {
                if (HttpContext.Current.Request["LensOptikLogin"] != null)
                {
                    HttpCookie kullanici = HttpContext.Current.Request.Cookies["LensOptikLogin"];
                    kullanici = HttpSecureCookie.Decode(kullanici);
                    name = Fonksiyonlar.SearchKeyword(kullanici["kullaniciIsim"]);
                }
            }
            catch (Exception ex)
            {
                LogManager.SqlDB.Write("Cookies kullanici adi hatası: "+ GetId().ToString(), ex);
            }

            return name;
        }
    }
}
