using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using BusinessLayer;
using DataAccessLayer;
using ModelLayer;

namespace LensOptikWebUI.Include.Ajax
{

    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //System.Web.SessionState.IReadOnlySessionState

    public class HediyeUrunContlor : IHttpHandler 
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                int adet = 0;
                int urunId = 0;

                context.Response.ContentType = "application/json";

                if (context.Request.Params["adet"] != null)
                    adet = int.Parse(context.Request.Params["adet"]);
              


                if (context.Request.Params["urunId"] != null)
                    urunId = int.Parse(context.Request.Params["urunId"]);  
                

                IList<UrunHediye> hediye = HediyeUrunDB.GiftList(urunId, adet, KullaniciOperasyon.GetId());
                JavaScriptSerializer oSerializer = new JavaScriptSerializer();
               
                context.Response.Write(oSerializer.Serialize(hediye));
            }
            catch (Exception ex)
            {
                LoggerLibrary.LogManager.SqlDB.Write("Hata: ", ex);
                context.Response.Write("Hata Oluştu");
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