using System;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer;
using BusinessLayer.BasePage;
using BusinessLayer.PagingLink;
using DataAccessLayer;
using ModelLayer;


public partial class Admin_Urun_Yorumlari: BasePageAdmin
{
    SqlConnection baglan;
    SqlDataAdapter adp;
    SqlCommand komutver;

    private int Baslangic, Bitis;
    private int sayfaGosterim = 14;
    private int sayfaNo = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        baglan = new SqlConnection(ConnectionString.Get);
        
        if(!IsPostBack)
        {
            if (Request.Params["islem"] == "sil")
            {
                kayitSil(Convert.ToInt32(Request.Params["id"]));
                Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString());
            }

            if (Request.Params["islem"] == "durum")
            {
                aktifPasif();
                Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString());
            }

            if (Request.Params["islem"] == "duzenle")
            {
                string id = Request.QueryString["id"];
                urunYorumGetime(Convert.ToInt32(id));
                ModalPopupExtender.Show();
            }

            if (Request.QueryString["Sayfa"] != null)
            {
                sayfaNo = Convert.ToInt32(Request.QueryString["Sayfa"]);
            }



            Baslangic = (sayfaNo * sayfaGosterim) + 1;
            Bitis = Baslangic + sayfaGosterim - 1;

            ltlSayfalama.Text = PagingLink.GetHtmlCode(Request.QueryString, sayfaGosterim, UrunYorumlariDB.ItemCount());
            urunYorumlari();
        
            if (Request.Params["yorumKiriter"] != null)
            {
                yorumArama(Request.Params["yorumKiriter"].ToString(), "kiriter"); 
            }
        
            if (Request.Params["tarih_1"] != null)
            {
                tarihArama();
            }
        }
    }

    protected void txt_urunAdi_TextChanged(object sender, EventArgs e)
    {
        yorumArama(txt_urunId.Text.ToString() , "urunId"); 
    }

    protected void ddlArama_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("urun_yorumlari.aspx?yorumKiriter=" +  ddlArama.SelectedValue.ToString());
    }

    protected void btnYorumTarihAra_Click(object sender, EventArgs e)
    {
        Response.Redirect("urun_yorumlari.aspx?tarih_1=" + txtTarih_1.Text.ToString() + "&tarih_2=" + txtTarih_2.Text.ToString());
    }

    #region  Yorum Arama 
    private void yorumArama( string deger , string parametre)
    {
        baglan.Open();
        komutver = new SqlCommand();
        komutver.Connection = baglan;
        komutver.CommandText = "urun_YorumArama";
        komutver.CommandType = CommandType.StoredProcedure;
        komutver.Parameters.Add("@deger", SqlDbType.NVarChar);
        komutver.Parameters["@deger"].Value = deger.ToString();
        komutver.Parameters.Add("@parametre", SqlDbType.NVarChar);
        komutver.Parameters["@parametre"].Value =  parametre.ToString();
        adp = new SqlDataAdapter(komutver);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        string birlestir = null;
        string Kiriter = null;

        if(Request.Params["yorumKiriter"] != null)
        {
         Kiriter = Request.Params["yorumKiriter"].ToString();
        }
            switch (Kiriter)
            {
                case "5": Kiriter = "Mükemmel"; break;
                case "4": Kiriter = "Çok İyi"; break;
                case "3": Kiriter = "İyi"; break;
                case "2": Kiriter = "Fena Degil"; break;
                case "1": Kiriter = "Çok Kötü"; break;
            }

            if (parametre == "urunId")
        {
            if (dt.Rows.Count == 0)
            {
                birlestir = "<b>" + deger + "</b> Id li  Ürüne Ait Yorum Bulunamadı.";
                mesajGosterNo(birlestir.ToString());
            }
            else
            {
                birlestir = "<b>" + deger + "</b> Id li Ürüne Ait Toplam <b>" + dt.Rows.Count + "</b> Yorum Bulundu.";
                mesajGosterInfo(birlestir.ToString());
            }
        }
        else if (Request.Params["yorumKiriter"] != null)
        {
            if (dt.Rows.Count == 0)
            {
                birlestir = "<b>" + Kiriter + "</b> Kıriterine Ait Yorum Bulunamadı.";
                mesajGosterNo(birlestir.ToString());

            }
            else
            {
                birlestir = "<b>"+ Kiriter + "</b> Kıriterine Ait Toplam <b>" + dt.Rows.Count + "</b> Yorum Bulundu.";
                mesajGosterInfo(birlestir.ToString());
            }
        }

        grwUrunYorumlari.DataSource = dt.DefaultView;
        grwUrunYorumlari.DataBind();
    }
    #endregion

    #region Yorum Güncelleme İşlemi İçin Getirme
    private void urunYorumGetime(int id)
    {
        UrunYorumlari urunYorum = UrunYorumlariDB.Get(id ,"admin");

        lblYorumEkleyen.Text = urunYorum.AdiSoyadi;
        lblUrunAdi.Text = urunYorum.UrunAdi;
        txtUrunYorum.Text = GenelFonksiyonlar.BRDonusturEnter(urunYorum.Yorum);
        hdYorumId.Value = urunYorum.Id.ToString();

        ddlDegerlendirme.ClearSelection();

        for (int i = 0; i < ddlDegerlendirme.Items.Count; i++)
        {
            if (ddlDegerlendirme.Items[i].Value == urunYorum.DegerKiriteri.ToString())
            {
                ddlDegerlendirme.Items[i].Selected = true;
            }
        }
    }
    #endregion

    #region Ürün Durum İşlemi
    private void aktifPasif()
    {
        try
        {
            int durum = Convert.ToInt32(Request.Params["durum"]);
            int yorumId = Convert.ToInt32(Request.Params["id"]);
            int uyeId =   Convert.ToInt32(Request.QueryString["uyeId"]);

            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "urun_YorumDurum";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = yorumId;
            komutver.Parameters.Add("@durum", SqlDbType.Int);
            komutver.Parameters["@durum"].Value = durum ;
            komutver.ExecuteNonQuery();


            KullaniciPuan puan = new KullaniciPuan
            {
                Aciklama = "Ürün yorumu ile kazanılan puan.",
                PuanKod = "urunYorum",
                UyeId = uyeId,
                GenelId = yorumId
            };


            if (durum == 1)
            {
                KullaniciPuanDB.Kaydet(puan);
            }
            else
	        {
                KullaniciPuanDB.Sil(puan);
	        }
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>Durum Degiştirme Hatası: </b>" , ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region Ürün Yorumu Silme İşlemi
    private void kayitSil(int id)
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "urun_YorumtSil";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(id);
            komutver.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>Yorum Silme Hatası:</b>  " , ex);
        }
    }

    #endregion

    #region Ürün Yorumları
    private void urunYorumlari()
    {
        try
        {
            grwUrunYorumlari.DataSource = UrunYorumlariDB.Liste(Baslangic, Bitis);
            grwUrunYorumlari.DataBind();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Ürün Yorumları Listeleme Hatası", ex);
        }
    }
    #endregion

    #region Ürün Yorum Güncelleme İşlemi
    protected void btnYorumGuncelle_Click(object sender, EventArgs e)
    {
        try
        {
            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "urun_YorumGuncelle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(hdYorumId.Value);
            komutver.Parameters.Add("@degerKiriteri", SqlDbType.NVarChar);
            komutver.Parameters["@degerKiriteri"].Value = ddlDegerlendirme.SelectedValue.ToString();
            komutver.Parameters.Add("@yorum", SqlDbType.NVarChar);
            komutver.Parameters["@yorum"].Value = GenelFonksiyonlar.EnterDonusturBr( txtUrunYorum.Text.ToString());
            komutver.Parameters.Add("@deger_dondur", SqlDbType.Int);
            komutver.Parameters["@deger_dondur"].Direction = ParameterDirection.Output;
            komutver.ExecuteNonQuery();
            int donendeger = Convert.ToInt32(komutver.Parameters["@deger_dondur"].Value);

            if (donendeger == 0)
            {
                pnlYorumArama.Visible = true;
                mesajGizlenfo();
                mesajGosterOk("Ürün Yorumu Başarı ile Güncellendi.");
            }
            else
            {
                mesajGosterNo("Aynı İçerige Sahip Bir Yorum Bulunmakta");
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>YORUM GÖNDERME HATAS: </b> ", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region Tarih Aralığı Yorum Arama işlemi
    private void tarihArama()
    {
        baglan.Open();
        komutver = new SqlCommand();
        komutver.Connection = baglan;
        komutver.CommandText = "urun_YorumTarihArama";
        komutver.CommandType = CommandType.StoredProcedure;
        komutver.Parameters.Add("@baslangicTarih", SqlDbType.DateTime);
        komutver.Parameters["@baslangicTarih"].Value = Request.Params["tarih_1"].ToString();
        komutver.Parameters.Add("@bitisTarih", SqlDbType.DateTime);
        string tarih = Request.Params["tarih_2"].ToString() + " 23:59:59";
       
        komutver.Parameters["@bitisTarih"].Value = Convert.ToDateTime(tarih);

        adp = new SqlDataAdapter(komutver);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        string birlestir = null;

            if (dt.Rows.Count == 0)
            {
                birlestir = "<b>" + Request.Params["tarih_1"].ToString() +" "+ Request.Params["tarih_2"].ToString() +"</b> Tarih Aralığına Ait Yorum Bulunamadı.";
                mesajGosterNo(birlestir.ToString());

            }
            else
            {
                birlestir = "<b>"+ Request.Params["tarih_1"].ToString() +" "+ Request.Params["tarih_2"].ToString() + "</b> Tarih Aralığına Ait Toplam <b>" + dt.Rows.Count + "</b> Yorum Bulundu.";
                mesajGosterInfo(birlestir.ToString());
            }


        grwUrunYorumlari.DataSource = dt.DefaultView;
        grwUrunYorumlari.DataBind();
    }
    #endregion

}
