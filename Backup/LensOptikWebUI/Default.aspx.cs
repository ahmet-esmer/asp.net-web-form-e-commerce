using System;
using System.Collections.Generic;
using System.Web.UI;
using BusinessLayer.Cashing;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class _default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        UrunListele();
        SayfaMetaTag();

        if (Request.QueryString["yeniKayit"] != null)
        {
            YeniUyeKaydiAlert();
        }
    }

    private void UrunListele()
    {
        try
        {
            if (CacheStorage.Exists(CacheKeys.MainProducts))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.MainProducts))
                        CacheStorage.Store(CacheKeys.MainProducts, UrunDB.Urunler("anaSayfa"), "tbl_urunler");
                }
            }

            ucProductList.ProductsToDisplay = Cache[CacheKeys.MainProducts] as List<Urun>;
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Anasayfa ürün listeleme hatası", ex);
        }
    }

    private void SayfaMetaTag()
    {
        try
        {
            if (CacheStorage.Exists(CacheKeys.MainMetaTag))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.MainMetaTag))
                        CacheStorage.Store(CacheKeys.MainMetaTag,IcerikDB.IcerikGetir("ANASAYFA"), "sayfa_icerik");
                }
            }

            Icerik icerik = Cache[CacheKeys.MainMetaTag] as Icerik;
            Page.Title = icerik.title;
            Page.MetaDescription = icerik.description;
            Page.MetaKeywords = icerik.keywords;
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Meta Tag:", ex);
        }
    }

    private void YeniUyeKaydiAlert()
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "yeniKayit", " alert('Üyelik işlemi başarı ile gerçekleşmiştir')", true);
    }
}