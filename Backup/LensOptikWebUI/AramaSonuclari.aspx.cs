using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BusinessLayer;
using BusinessLayer.PagingLink;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using ServiceLayer;

public partial class arama_sonuclari : System.Web.UI.Page
{
    private string keyword = "";
    private Paging paging;    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["keyword"] != null)
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["keyword"]))
            {
                keyword = Request.QueryString["keyword"].Replace("-", " ");
                KayitSayisi(keyword, -1);
                urunListele(keyword);
            }
            else
            {
                lbllistelemeBilgisi.Text = "Lütfen arama kiriteri giriniz.";
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    #region Toplam Kayıt Sayısı Almak
    private void KayitSayisi(string keyword, int markaId)
    {
        try
        {
            paging = new Paging();
            paging.CurentPage = QueryString.Value<int>("Sayfa");
            paging.TotolItem = UrunDB.UrunToplamKayit("arama", keyword, markaId);
            paging.QueryString = Request.QueryString;
            lbllistelemeBilgisi.Text = paging.Mesaj;

            ucProductList.PagingLinkToDisplay = PagingLinkWeb.GetHtmlCode(paging);
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Arama Sonucu Sayfa No Hatası:", hata);
        }
    }
    #endregion

    #region Ürün Listele İşlemi
    private void urunListele(string keyword)
    {
        try
        {
            SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@Baslangic", paging.StartItem),
                    new SqlParameter("@Bitis", paging.EndItem),
                    new SqlParameter("@keyword", keyword)
                };

            using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_KayitAraWeb", parametre))
            {
                List<Urun> urunler = new List<Urun>();

                while (dr.Read())
                {
                    urunler.Add(new Urun(dr.GetString(dr.GetOrdinal("resimAdi")),
                       dr.GetInt32(dr.GetOrdinal("id")),
                       dr.GetString(dr.GetOrdinal("urunAdi")),
                       dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                       dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat")),
                       dr.GetString(dr.GetOrdinal("doviz")),
                       dr.GetInt32(dr.GetOrdinal("urunKDV")),
                        LinkBulding.Urun(
                       dr.GetString(dr.GetOrdinal("kategoriadi")),
                       dr.GetInt32(dr.GetOrdinal("id")),
                       dr.GetString(dr.GetOrdinal("urunAdi")))
                       ));
                }

                ucProductList.ProductsToDisplay = urunler;
            }
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Arama Sonucu Listeleme Hatası", hata);
        }
    }
    #endregion
}