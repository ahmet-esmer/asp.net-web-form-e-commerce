using System;
using System.Web;
using SecureCookie;
using BusinessLayer;


public partial class adminMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "Lens Optik Yönetici Paneli";

        if (Request.Cookies["LensOptikAdminGiris"] == null)
            Response.Redirect("Default.aspx");

        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies["LensOptikAdminGiris"];
            cookie = HttpSecureCookie.Decode(cookie);
   
            hlYonetici.Text = GenelFonksiyonlar.SearchKeyword(cookie["yoneticiIsim"]);
            hlYonetici.NavigateUrl = "yoneticiler.aspx?islem=duzenle&id=" + cookie["yoneticiId"]; 
        }
    }
}
