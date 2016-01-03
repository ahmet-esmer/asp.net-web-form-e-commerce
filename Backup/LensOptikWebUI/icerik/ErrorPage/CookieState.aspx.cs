using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using LoggerLibrary;

public partial class Icerik_Cookies_State : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpBrowserCapabilities browser = Request.Browser;

            StringBuilder sp = new StringBuilder();
            sp.Append("BrowserTuru: " + browser.Browser);
            sp.Append(" BrowserAdi: " + browser.Type);
            sp.Append(" BrowserVersiyonu: " + browser.Version);


            if (browser.Browser == "IE" && browser.Version == "8.0")
            {
                pnlCookieIE8.Visible = true;
            }
            else if (browser.Browser == "IE" && browser.Version == "7.0")
            {
                pnlCookieIE8.Visible = true;
            }
            else if (browser.Browser == "IE" && browser.Version == "9.0")
            {
                pnlCookieIE9.Visible = true;
            }


            LogManager.Event.Write("Cookies desteklemiyor", sp.ToString());

            StringBuilder sbMesaj = new StringBuilder();
            sbMesaj.Append("Tarayıcınızın çerez fonksiyonu kapalı.<br/>");
            sbMesaj.Append("Lütfen bu fonksiyonu açınız.<br/>");
            sbMesaj.Append("<a href='../../Default.aspx' class='link'>");
            sbMesaj.Append("Ana sayfaya dönmek için tıklayın.");
            sbMesaj.Append("</a><br/>");
            sbMesaj.Append("<p>&nbsp; </p>");

            Mesaj.ErrorSis(sbMesaj.ToString());
            
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Coocies Durum", ex);
        }
    }
}