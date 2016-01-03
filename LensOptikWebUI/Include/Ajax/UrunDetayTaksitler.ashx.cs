using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelLayer.JSON;
using System.Web.Script.Serialization;
using DataAccessLayer;

namespace LensOptikWebUI.Include.Ajax
{
    /// <summary>
    /// Summary description for UrunDetayTaksitler
    /// </summary>
    public class UrunDetayTaksitler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
               context.Response.ContentType = "application/json";

               List<TaksitJson> taksitler = 
                   BankaTaksitDB.UrunDetayListe(Convert.ToDecimal(context.Request.Params["fiyat"]));

                JavaScriptSerializer oSerializer = new JavaScriptSerializer();
                context.Response.Write(oSerializer.Serialize(taksitler));
            }
            catch (Exception ex)
            {
                context.Response.Write("hata");
                LoggerLibrary.LogManager.Mail.Write("Taksit listeleme hatası", ex);
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