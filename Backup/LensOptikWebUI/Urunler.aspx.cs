using System;
using System.Data.SqlClient;
using System.Web.UI;
using BusinessLayer;
using BusinessLayer.PagingLink;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using ServiceLayer;

public partial class urunler : Page
{
    private Paging paging;
    private string serial = "";
    private int markaId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        serial = RouteDataGet.Value<string>("katId");

        if ((markaId = QueryString.Value<int>("markaId")) > 0)
        {
            KayitSayisi("marka", markaId);
            UrunListele("marka", markaId);
            MevcutMarkaGetir(markaId);  
        }
        else if (serial != null)
        {
            KayitSayisi("kategori", 0);
            UrunListele("kategori", 0);
        }

        PageMetaTag();
        KategoriMevcutSayfa();
    }

    #region Mevcut sayfa marka bilgisi
    protected void MevcutMarkaGetir(int markaId)
    {
        try
        {
            hlMarka.Visible = true;
            hlMarka.Text = MarkaDB.GetName(markaId);
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Marka Listeleme", ex);
        }
    }
    #endregion

    #region Kategori mevcut sayfa bilgisi işlemi
    protected void KategoriMevcutSayfa()
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@serial", serial);
            using (SqlDataReader dr = SqlHelper.ExecuteReader("kategori_altKategoriLink", parametre))
            {
                while (dr.Read())
                {
                    string katAdi = dr.GetString(dr.GetOrdinal("kategoriadi"));
                    string katSeri = dr.GetString(dr.GetOrdinal("serial"));
                    string title = dr.GetString(dr.GetOrdinal("title"));

                    if (katSeri.Length == 3)
                    {
                        hlkat3.Visible = true;
                        hlkat3.Text = katAdi;
                        hlkat3.NavigateUrl = LinkBulding.Kategori(title, katAdi, serial);
                    }
                    else if (katSeri.Length == 6)
                    {
                        hlkat6.Visible = true;
                        hlkat6.Text = katAdi;
                        hlkat6.NavigateUrl = LinkBulding.Kategori(title, katAdi, serial);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("kategori Mevcut Sayfa", ex);
        }
    }
    #endregion

    #region Toplam kayıt sayısı almak
    private void KayitSayisi(string parameter, int markaId)
    {
        try
        {
            paging = new Paging();
            paging.CurentPage = QueryString.Value<int>("Sayfa");
            paging.TotolItem = UrunDB.UrunToplamKayit(serial, parameter, markaId);
            paging.QueryString = Request.QueryString;
            lbllistelemeBilgisi.Text = paging.Mesaj;

            ucProductList.PagingLinkToDisplay = PagingLinkWeb.GetHtmlCode(paging);
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Ürün Listeleme Sayfa No Hatası", ex);
        }
    }
    #endregion

    #region Ürün listele işlemi
    private void UrunListele(string parameter, int markaId)
    {
        try
        {
         ucProductList.ProductsToDisplay =
             UrunDB.Urunler(paging.StartItem, paging.EndItem, serial, parameter, markaId);
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Ürün Listeleme Hatası", ex);
        }
    }
    #endregion

    #region Sayfa meta tağ getirme işlemi
    public void PageMetaTag()
    {
        try
        {
            MetaTag metaTag = KategoriDB.MetaTag(serial);
            Page.Title = metaTag.Title;
            Page.MetaDescription = metaTag.Description;
            Page.MetaKeywords = metaTag.Keywords;
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Kategori Metatag:", hata);
        }
    }
    #endregion
}