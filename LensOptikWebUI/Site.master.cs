using System;
using System.Web;
using BusinessLayer;
using BusinessLayer.Security;
using DataAccessLayer;
using ServiceLayer.ExtensionMethods;
using ServiceLayer.Messages.Sepet;
using LoggerLibrary;

public partial class master_page_site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
     
        Page.Header.DataBind();
        string sLocalPath = Request.Url.LocalPath;

        if (HttpContext.Current.Request.IsSecureConnection &&
            !sLocalPath.Contains("/Kullanici/Kayit") && !sLocalPath.Contains("/Market/Sepet"))
        {
            SslHelper sslHelper = new SslHelper();
            sslHelper.EnsureHTTP();
        }


        if (!IsPostBack)
        {
            KulanicHosgeldinYazisi();

            if (Request.QueryString["user"] == "ok")
            {
                CookieBrowserKontlor();
            }
        }
    }

    private void CookieBrowserKontlor()
    {
        if (Request.Cookies["LensOptikLogin"] == null)
        {
            Response.Redirect("~/icerik/ErrorPage/CookieState.aspx");
        }
    }

    #region Kulanıcı Hoşgeldin Yazısı
    private void KulanicHosgeldinYazisi()
    {
        if (KullaniciOperasyon.LoginKontrol())
        {
            btnCikis.Visible = true;
            hlKulaniciAd.Text = "Hoş Geldiniz <b>" + KullaniciOperasyon.GetName() + "</b>";
            SepetBilgilendirme(KullaniciOperasyon.GetId());
        } 
    }
    #endregion

    #region Login Sepet bilgilendirme
    private void SepetBilgilendirme(int uyeId)
    {
        try
        {
            SepetOzetResponse sepet = SepetDB.SepetOzet(uyeId).ConvertToSepetOzetResponse();

            hlBilgiSay.Text = "Sepetinizde<span class='sepetSayi'><b> "
                + sepet.Adet.ToString() + " </b> Adet Ürün</span> Var.";

            if (sepet.FiyatToplam != 0)
            {
                hlToplam.Text = "Toplam: <b>" + sepet.FiyatToplam.ToString("c") + "</b>";
            }
        }
        catch (Exception hata)
        {
            LoggerLibrary.LogManager.SqlDB.Write("login Sepet Bilgilendirme hatası", hata);
        }
    }
    #endregion

    #region Kullanici Çıkış
    protected void btnCikis_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["LensOptikLogin"] != null)
            Response.Cookies["LensOptikLogin"].Expires = DateTime.Now.AddDays(-1);

        if (Request.Cookies["LensOptikAdminGiris"] != null)
            Response.Cookies["LensOptikAdminGiris"].Expires = DateTime.Now.AddDays(-1);

        Response.Redirect("~/");
    }
    #endregion

    protected void btnArama_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        string arama = txtAramaForm.Text.Replace(" ", "-");
        Response.Redirect("~/AramaSonuclari.aspx?keyword=" + GuvenlikIslemleri.hackKontrolArama(arama));
    }
}
