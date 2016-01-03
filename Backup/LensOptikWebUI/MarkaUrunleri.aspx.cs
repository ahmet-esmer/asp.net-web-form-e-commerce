using System;
using BusinessLayer.PagingLink;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using ServiceLayer;

public partial class marka_urunleri : System.Web.UI.Page
{

    private int markaId = 0;
    private Paging paging;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (RouteData.Values["MarkaId"] != null)
        {
            markaId = Convert.ToInt32(RouteData.Values["MarkaId"]); 

            UrunKayitSayisi();
            kategoriMevcutSayfa();
            UrunListele();
        }     
    }

    #region Kategori Mevcut Sayfa Bilgisi İşlemi
    protected void kategoriMevcutSayfa()
    {
        try
        {
            string marka = MarkaDB.GetName(markaId);
            hlMarka.Text = marka;
            Page.Title = marka;
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Kategori Mevcut Sayfa", ex);
        }
    }
    #endregion

    #region Toplam Kayıt Sayısı Almak
    private void UrunKayitSayisi()
    {
        try
        {
            paging = new Paging();
            paging.CurentPage = QueryString.Value<int>("Sayfa");
            paging.TotolItem = UrunDB.UrunToplamKayit("", "markaTek", markaId);
            paging.QueryString = Request.QueryString;
            ucProductList.PagingLinkToDisplay = PagingLinkWeb.GetHtmlCode(paging);
            lbllistelemeBilgisi.Text = paging.Mesaj;
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Marka Ürün Listeleme Sayfa No Hatası", ex);
        }
    }
    #endregion

    #region Ürün Listele İşlemi
    private void UrunListele()
    {
        try
        {
            ucProductList.ProductsToDisplay =
                UrunDB.Urunler(paging.StartItem, paging.EndItem, "", "markaTek", markaId);
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Marka Ürün Listeleme Hatası", ex);
        }
    }
    #endregion

}