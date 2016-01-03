<%@ WebHandler Language="C#" Class="EPostaContlor" %>

using System;
using System.Web;
using DataAccessLayer;
using System.Data;

public class EPostaContlor : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        try
        {
            int count = 0;
            if (context.Request["EPosta"] != null)
	        {
                
                string eposta = context.Request["EPosta"];
                if (BusinessLayer.GenelFonksiyonlar.GecerliMailAdresi(eposta))
                {
                    string sqlText = "SELECT COUNT(ePosta) FROM dbo.tbl_kullanicilar WHERE eposta='" + eposta + "'";

                    count = (int)SqlHelper.ExecuteScalar(CommandType.Text, sqlText);

                }
	        }
            
            context.Response.Write(count.ToString());
        }
        catch (Exception hata)
        {
            LoggerLibrary.LogManager.SqlDB.Write("Ajax E-Posta  Sorgulama Hatası", hata);
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