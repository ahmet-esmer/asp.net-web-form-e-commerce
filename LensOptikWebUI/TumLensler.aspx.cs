using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessLayer.Cashing;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class tum_lensler : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EnCokSatilanlar();
        KategoriKayitListele();
        MarkaListeleme();
        SayfaIcerikListele();
    }


    #region Sayfa İçerik  Listele İşlemi
    private void SayfaIcerikListele()
    {
        try
        {
            int sayfaId;
            if ((sayfaId = ServiceLayer.RouteDataGet.Value<int>("sayfaId")) > 0)
            {
                Icerik Icerik = IcerikDB.IcerikGetir("web", sayfaId);
                Page.Title = Icerik.title;
                Page.MetaDescription = Icerik.description;
                Page.MetaKeywords = Icerik.keywords;
                ltlIcerik.Text = Icerik.icerik;
            }   
        }
        catch (Exception hata)
        {
            LoggerLibrary.LogManager.SqlDB.Write("Sayfa Gösterim Listeleme Hatası", hata);
        }
    }
    #endregion

    private void EnCokSatilanlar()
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@parametre", "butunUrunler");
            using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_Listele", parametre))
            {
                List<Urun> SatanilanUrunler = new List<Urun>();

                while (dr.Read())
                {
                    SatanilanUrunler.Add(new Urun
                    {
                        urunAdi = dr.GetString(dr.GetOrdinal("urunAdi")),
                        link = LinkBulding.Urun(
                        dr.GetString(dr.GetOrdinal("kategoriadi")),
                        dr.GetInt32(dr.GetOrdinal("id")),
                        dr.GetString(dr.GetOrdinal("urunAdi")))
                    });
                }

                rptSatilanlar.DataSource = SatanilanUrunler;
                rptSatilanlar.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Ürün en çok satanlar Listeleme Hatası", ex);
        }
    }

    private void KategoriKayitListele()
    {
        try
        {
            if (CacheStorage.Exists(CacheKeys.Category))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.Category))
                    {
                        CacheStorage.Store(CacheKeys.Category, KategoriDB.Liste(), "tbl_kategori");
                    }
                }
            }

            rptAnakategoriler.DataSource = Cache[CacheKeys.Category] as List<Kategori>;
            rptAnakategoriler.DataBind();

        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write(" Sol Panel kategori ", ex);
        }
    }

    #region Altkategori Listeleme İşlemi
    protected void rptAnakategoriler_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rptAltKategori = (Repeater)e.Item.FindControl("rptAltKategori");
            Label lblKat = (Label)e.Item.FindControl("lblKategori");
            HiddenField serial = (HiddenField)e.Item.FindControl("hdfSerial");

            try
            {
                SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@serial", serial.Value.ToString()),
                    new SqlParameter("@bolge", "anaKat"),
                };

                using (SqlDataReader dr = SqlHelper.ExecuteReader("kategori_altKategoriListele", parametre))
                {
                    List<Kategori> altKategori = new List<Kategori>();
                    while (dr.Read())
                    {
                        if (dr.GetString(dr.GetOrdinal("serial")) != serial.Value.ToString())
                        {
                            altKategori.Add(new Kategori
                            {
                                id = dr.GetInt32(dr.GetOrdinal("id")),
                                kategoriadi = dr.GetString(dr.GetOrdinal("kategoriadi")),
                                serial = dr.GetString(dr.GetOrdinal("serial")),
                                title = dr.GetString(dr.GetOrdinal("title"))
                            });
                        }
                    }

                    rptAltKategori.DataSource = altKategori;
                    rptAltKategori.DataBind();

                    if (!dr.HasRows)
                    {
                        rptAltKategori.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.SqlDB.Write(" Sol Panel Alt kategori ", ex);
            }
        }
    }
    #endregion

    #region Marka Listeleme İşlemi
    protected void MarkaListeleme()
    {
        try
        {
            rptMarkalar.DataSource = MarkaDB.Liste("solPanel");
            rptMarkalar.DataBind();
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("marka  SolPanel Listeleme", hata);
        }
    }
    #endregion

}