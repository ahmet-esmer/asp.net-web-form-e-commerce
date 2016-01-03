using System;
using System.Web;
using System.Web.Script.Serialization;
using DataAccessLayer;
using ModelLayer.JSON;
using SecureCookie;
using ServiceLayer.ExtensionMethods;
using BusinessLayer;

namespace LensOptikWebUI.Include.Ajax
{

    public class BankaTaksitleri : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "application/json";

                int bankaId = int.Parse(context.Request.Params["bankaId"]);
                decimal fiyat = 0;

  
                HttpCookie siparisBilgi = context.Request.Cookies["lensOptik"];
                siparisBilgi = HttpSecureCookie.Decode(siparisBilgi);
                fiyat = Convert.ToDecimal(siparisBilgi["FiyatToplam"]);

                TaksitJson taksit = BankaTaksitDB.List(bankaId, fiyat);
                JavaScriptSerializer oSerializer = new JavaScriptSerializer();
                context.Response.Write(oSerializer.Serialize(taksit));
            }
            catch(NullReferenceException ex)
            {
                LoggerLibrary.LogManager.SqlDB.Write("Zaman Aşımı Cookies Null", ex);
                context.Response.Write("hata");
            }
            catch (Exception ex)
            {
                LoggerLibrary.LogManager.SqlDB.Write("Ödeme sayfası taksit listeleme hatası", ex);
                context.Response.Write("hata");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

     
    }
}