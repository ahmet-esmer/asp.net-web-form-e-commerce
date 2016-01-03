using System;
using System.Web.UI;
using ModelLayer;
using DataAccessLayer;
using ServiceLayer;

public partial class Icerik_Sayfa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (RouteData.Values["sayfaId"] != null)
        {
            sayfaIcerikListele();
        }
    }


    #region Sayfa İçerik  Listele İşlemi
    private void sayfaIcerikListele()
    {
        try
        {
            Icerik Icerik = IcerikDB.IcerikGetir("web", RouteDataGet.Value<int>("sayfaId"));
            Page.Title = Icerik.title;
            Page.MetaDescription = Icerik.description;
            Page.MetaKeywords = Icerik.keywords;

            ltlBaslik.Text = Icerik.sayfaBaslik;
            ltlSayfaIcerik.Text = Icerik.icerik;

        }
        catch (Exception hata)
        {
            LoggerLibrary.LogManager.SqlDB.Write("Sayfa Gösterim Listeleme Hatası", hata);
        }
    }
    #endregion
}