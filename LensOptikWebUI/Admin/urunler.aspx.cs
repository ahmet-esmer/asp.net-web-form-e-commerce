using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using BusinessLayer.BasePage;
using BusinessLayer.PagingLink;
using DataAccessLayer;
using ModelLayer;

public partial class AdminUrunler : BasePageAdmin
{
    string birlestir = null;
    int count = 0;
    string gosterimAd = null;

    private int sayfaNo, Baslangic, Bitis;
    private int sayfaGosterim = 12;
    private int toplamKayit = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            kategoriListele();
            markaListele();

            if (Request.QueryString["Sayfa"] != null)
             {
               sayfaNo = Convert.ToInt32(Request.QueryString["Sayfa"]);
             }
            else
             {
               sayfaNo = 0;
             }

            Baslangic = (sayfaNo * sayfaGosterim) + 1;
            Bitis = Baslangic + sayfaGosterim - 1;


            if (Request.Params["islem"] == "genelUrun")
            {
               KayitSayisi("bos", "admin", 0);
               urunListele(Baslangic, Bitis);
            }


            #region Kategori Arama
            if (Request.Params["kategori"] != null)
            {
                string serial = Request.Params["kategori"].ToString();
                SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@Baslangic", Baslangic),
                    new SqlParameter("@Bitis", Bitis),
                    new SqlParameter("@serial", serial ),
                    new SqlParameter("@parametre", "kategoriAdmin"),
                    new SqlParameter("@markaId", 0)
                };

                SqlDataReader dr = SqlHelper.ExecuteReader("urun_KayitListele", parametre);

                gosterimAd = Request.Params["katAdi"].ToString();
                KayitSayisi(serial.ToString(), "kategoriAdmin", 0);
                urunAramaMetodu(dr, gosterimAd);
            }
            #endregion

            #region Marka Arama
            if (Request.Params["markalar"] != null)
            {
                int marka = Convert.ToInt32(Request.Params["markalar"]);
                SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@Baslangic", Baslangic),
                    new SqlParameter("@Bitis", Bitis),
                    new SqlParameter("@serial", "" ),
                    new SqlParameter("@parametre", "markaAdmin"),
                    new SqlParameter("@markaId", marka)
                };

                SqlDataReader dr = SqlHelper.ExecuteReader("urun_KayitListele", parametre);

                gosterimAd = Request.Params["markaAd"];
                KayitSayisi("", "markaAdmin", marka);
                urunAramaMetodu(dr, gosterimAd);
            }
            #endregion

            #region Ürün Adı ile Arama
            if (Request.Params["urunArama"] != null)
            {
                SqlDataReader dr = UrunDB.AramaAdmin(-2, Request.Params["urunArama"].ToString());

                gosterimAd = Request.Params["urunArama"].ToString();
                urunAramaMetodu(dr, gosterimAd);
            }
            #endregion

            #region Ürün Etiket Paramatleri Arama
            if (Request.Params["urunParemetre"] != null)
            {
                SqlDataReader dr = UrunDB.AramaAdmin(1, Request.Params["urunParemetre"].ToString());

                gosterimAd = Request.Params["urunParemetre"].ToString();
                switch (gosterimAd)
                {
                    case "anasayfa": gosterimAd = "Anasayfa "; break;
                    case "satanlar": gosterimAd = "En Çok Satanlar"; break;
                    case "yeniUrun": gosterimAd = "Yeni Ürünler"; break;
                    case "kampanya": gosterimAd = "Kapanyalı Ürünler "; break;
                    case "kiritikStok": gosterimAd = "kiritik Stok "; break;
                    case "stokYok": gosterimAd = "Stokta Olmayanlar "; break;
                    case "aktif": gosterimAd = "Aktif Ürünler "; break;
                    case "pasif": gosterimAd = "Pasif Ürünler "; break;
                }

                urunAramaMetodu(dr, gosterimAd);
            }
            #endregion

        }
    }
  
    #region Genel Ürün Arama Metodu 
    private void urunAramaMetodu(SqlDataReader dr, string gosterimAd)
    {
        List<Urun> urunler = new List<Urun>();
        while (dr.Read())
        {
            Urun Info = new Urun(
               dr.GetString(dr.GetOrdinal("resimAdi")),
               dr.GetString(dr.GetOrdinal("kategoriadi")),
               dr.GetString(dr.GetOrdinal("markaAdi")),
               dr.GetString(dr.GetOrdinal("urunAdi")),
               dr.GetInt32(dr.GetOrdinal("id")),
               dr.GetBoolean(dr.GetOrdinal("durum")),
               dr.GetInt32(dr.GetOrdinal("sira")),
               dr.GetInt32(dr.GetOrdinal("urunStok")),
               dr.GetString(dr.GetOrdinal("stokCins")),
               dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
               dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat")),
               dr.GetInt32(dr.GetOrdinal("kiritikStok")),
               dr.GetString(dr.GetOrdinal("doviz")));
            urunler.Add(Info);

            count = count + 1;
        }
        dr.Close();

        if (count == 0)
        {
            birlestir = "<b>\"" + gosterimAd.ToString() + "\"</b>  Ürün Bulunamadı.";
            mesajGosterNo(birlestir.ToString());
        }

        else if (Request.Params["markalar"] != null || Request.Params["kategori"] != null || Request.Params["tedarikciKod"] != null)
        {
            birlestir = "<b>\"" + gosterimAd.ToString() + "\"</b>   Toplam <b>" + toplamKayit.ToString() + "</b> Ürün Bulundu.";
            mesajGosterInfo(birlestir.ToString());

        }
        else
        {
            birlestir = "<b>\"" + gosterimAd.ToString() + "\"</b>   Toplam <b>" + count.ToString() + "</b> Ürün Bulundu.";
            mesajGosterInfo(birlestir.ToString());
        }



        GridView1.DataSource = urunler;
        GridView1.DataBind();
    }
    #endregion

    protected void ddlMarkalar_SelectedIndexChanged(object sender, EventArgs e)
    {
        string markaAdi = ddlMarkalar.SelectedItem.ToString().Replace("-", "");
        markaAdi = markaAdi.Replace("&", "");

        Response.Redirect("urunler.aspx?markalar=" + ddlMarkalar.SelectedItem.Value +"&markaAd=" + markaAdi);
    }

    protected void ddlkategoriler_SelectedIndexChanged(object sender, EventArgs e)
    {
        string katAdi = ddlkategoriler.SelectedItem.ToString().Replace("-", "");
        katAdi = katAdi.Replace("&", "");
        Response.Redirect("urunler.aspx?kategori=" + ddlkategoriler.SelectedValue+"&katAdi="+katAdi.Trim());
    }

    protected void txt_urunAdi_TextChanged(object sender, EventArgs e)
    {
        Response.Redirect("urunler.aspx?urunArama=" + txt_urunAdi.Text.ToString());
    }

    protected void ddlGosterim_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("urunler.aspx?urunParemetre=" + ddlGosterim.SelectedItem.Value.ToString());
    }

    protected void ddlStokDurum_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("urunler.aspx?urunParemetre=" + ddlStokDurum.SelectedItem.Value.ToString());
    }
   
    #region Toplam Kayıt ve Sayfalama
    public void KayitSayisi(string serial, string parameter, int marka)
    {
        SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@serial", serial.ToString()),
                    new SqlParameter("@parametre", parameter.ToString()),
                    new SqlParameter("@markaId ",  Convert.ToInt32(marka))
                };

        toplamKayit = (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "urun_SayafaNo", parametre);
        ltlSayfalama.Text = PagingLink.GetHtmlCode(Request.QueryString, sayfaGosterim, toplamKayit);

    }
    #endregion

    #region DropDouwn Kategori Listeleme
    private void kategoriListele()
    {
        ddlkategoriler.DataSource = KategoriDB.dropDownListele();
        ddlkategoriler.DataTextField = "kategoriadi";
        ddlkategoriler.DataValueField = "serial";
        ddlkategoriler.DataBind();

        ListItem item = new ListItem();
        item.Text = "Ürün Kategorisine Göre Ara";
        item.Value = "0";
        item.Selected = true;
        ddlkategoriler.Items.Add(item);
    }

    #endregion

    #region DropDouwn Marka Listeleme
    private void markaListele()
    {
        try
        {
            ddlMarkalar.DataSource = MarkaDB.dropDownListele();
            ddlMarkalar.DataTextField = "marka_adi";
            ddlMarkalar.DataValueField = "id";
            ddlMarkalar.DataBind();

            ListItem item = new ListItem();
            item.Text = "Ürün Markasına Göre Ara";
            item.Value = "0";
            item.Selected = true;
            ddlMarkalar.Items.Add(item);
        }
        catch (Exception ex)
        {
            mesajGosterSis("Marka Listeleme Hatası:", ex);
        }
    }
    #endregion

    #region Ürün Listele İşlemi
    private void urunListele(int Baslangic, int Bitis)
    {
        try
        {
            SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@Baslangic", Baslangic),
                    new SqlParameter("@Bitis", Bitis)
                };

            SqlDataReader dr = SqlHelper.ExecuteReader("urun_AdminListele", parametre);
            List<Urun> urunler = new List<Urun>();

            while (dr.Read())
            {
                Urun Info = new Urun(
                    dr.GetString(dr.GetOrdinal("resimAdi")),
                    dr.GetString(dr.GetOrdinal("kategoriadi")),
                    dr.GetString(dr.GetOrdinal("markaAdi")),
                    dr.GetString(dr.GetOrdinal("urunAdi")),
                    dr.GetInt32(dr.GetOrdinal("id")),
                    dr.GetBoolean(dr.GetOrdinal("durum")),
                    dr.GetInt32(dr.GetOrdinal("sira")),
                    dr.GetInt32(dr.GetOrdinal("urunStok")),
                    dr.GetString(dr.GetOrdinal("stokCins")),
                    dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                    dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat")),
                    dr.GetInt32(dr.GetOrdinal("kiritikStok")),
                    dr.GetString(dr.GetOrdinal("doviz")));
                urunler.Add(Info);
            }
            dr.Close();

            GridView1.DataSource = urunler;
            GridView1.DataBind();


        }
        catch (Exception ex)
        {
            mesajGosterSis("Ürün Listeleme Hatası", ex);
        }
    }
    #endregion

    protected void btnYoonetiEkle_Click(object sender, EventArgs e)
    {
        Response.Redirect("urun_ekle.aspx?islem=yeni");
    }

    #region Id Arama Alanı
    protected void btnIdArama_Click(object sender, EventArgs e)
    {
        SqlDataReader dr = UrunDB.AramaAdmin(Convert.ToInt32(txtIdArama.Text.Trim()), "urunId");
        urunAramaMetodu(dr, txtIdArama.Text.ToString()+ " Id"  );
    }
    #endregion

    #region Ürün Sıralama İşlemi
    protected void urunSiraGuncelleme(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = (TextBox)sender;
            int katId = Convert.ToInt32(txt.ToolTip);
            string katSira = txt.Text.ToString();

            if (katSira == "")
                return;
            
            IcerikDB.GenelSiralama(katId, katSira, "urunler");
        }
        catch (Exception ex)
        {
            mesajGosterSis("Kategori Sira Güncelleme", ex);
        }
    }
    #endregion

}
