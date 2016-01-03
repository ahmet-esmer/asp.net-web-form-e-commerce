using System;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using BusinessLayer.PagingLink;
using ServiceLayer;

public partial class yeni_urunler : System.Web.UI.Page
{
    private Paging paging;

    protected void Page_Load(object sender, EventArgs e)
    {
        KayitSayisi();
        UrunListele();
        PageMetaTag();     
    }

    #region Toplam kayıt sayısı almak
    private void KayitSayisi()
    {
        try
        {
            paging = new Paging();
            paging.CurentPage = QueryString.Value<int>("Sayfa");
            paging.TotolItem = UrunDB.YeniUrunlerToplamKayit();
            paging.QueryString = Request.QueryString;
            lbllistelemeBilgisi.Text = paging.Mesaj;

            ucProductList.PagingLinkToDisplay = PagingLinkWeb.GetHtmlCode(paging);
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("yeni Ürün Listeleme Sayfa No Hatası", ex);
        }
    }
    #endregion

    #region Ürün listele işlemi
    private void UrunListele()
    {
        try
        {
            ucProductList.ProductsToDisplay = UrunDB.YeniUrunler(paging.StartItem, paging.EndItem); 
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Anasfa Ürün Listeleme Hatası", ex);
        }
    }
    #endregion

    #region Sayfa metatağ getirme işlemi
    public void PageMetaTag()
    {
        try
        {
            if (RouteData.Values["sayfaId"] != null)
            {
                MetaTag metaTag = IcerikDB.MetaTag(RouteDataGet.Value<int>("sayfaId"));
                Page.Title = metaTag.Title;
                Page.MetaDescription = metaTag.Description;
                Page.MetaKeywords = metaTag.Keywords;
            }
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Kategori Metatag:", hata);
        }
    }
    #endregion
}