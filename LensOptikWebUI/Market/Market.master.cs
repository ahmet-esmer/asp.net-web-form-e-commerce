using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BusinessLayer;
using DataAccessLayer;
using ModelLayer;
using ServiceLayer.Messages.Sepet;
using ServiceLayer.ExtensionMethods;
using BusinessLayer.Security;
using System.Configuration;

public partial class Market : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (ConfigurationManager.AppSettings["sslSitePath"].Contains("htts"))
        //{
            SslHelper sslHelper = new SslHelper();
            sslHelper.EnsureHTTPS();  
        //}


        if (!IsPostBack)
        {
            KulanicHosgeldinYazisi();  
        }
    }

    #region Kulanıcı Hoşgeldin Yazısı
    private void KulanicHosgeldinYazisi()
    {
        btnCikis.Visible = true;
        hlKulaniciAd.Text = "Hoş Geldiniz <b> " + KullaniciOperasyon.GetName() + "</b>";
        sepetBilgilendirme(KullaniciOperasyon.GetId());
    }
    #endregion

    #region Login Sepet bilgilendirme
    private void sepetBilgilendirme(int uyeId)
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
        {
            Response.Cookies["LensOptikLogin"].Expires = DateTime.Now.AddDays(-1);
        }

        Response.Redirect("~/");
    }
    #endregion

}
