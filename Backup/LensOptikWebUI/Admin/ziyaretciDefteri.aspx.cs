using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessLayer.BasePage;
using BusinessLayer.PagingLink;
using DataAccessLayer;
using ModelLayer;

public partial class AdminZiyaretciDefteri :BasePageAdmin
{
    private int sayfaNo, Baslangic, Bitis;
    private int sayfaGosterim = 5;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (! IsPostBack)
        {
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

            if (Request.Params["tumu"] != null)
            {
                ltlSayfalama.Text = PagingLink.GetHtmlCode(Request.QueryString, sayfaGosterim, ZiyaretciDefteriDB.ItemCount());
                ziyaretciDefteriListele();
            }

            if (Request.Params["tarih_1"] != null)
            {
                tarihArama();
            }

        }
    }

    protected void btnYorumTarihAra_Click(object sender, EventArgs e)
    {
        Response.Redirect("ziyaretciDefteri.aspx?tarih_1=" + txtTarih_1.Text.ToString() + "&tarih_2=" + txtTarih_2.Text.ToString());
    }


    #region Ziyaretçi Yorum Güncelleme İşlemi İçin Getirme
    private void ziyaretciDefteriGetir( int id)
    {
        using (SqlConnection baglan = new SqlConnection(ConnectionString.Get))
        {
            baglan.Open();
            using (SqlCommand komutver = new SqlCommand())
            {
                komutver.Connection = baglan;
                komutver.CommandText = "ziyaretci_DefteriGetir";
                komutver.CommandType = CommandType.StoredProcedure;
                komutver.Parameters.Add("@id", SqlDbType.Int);
                komutver.Parameters["@id"].Value = id ;

                SqlDataAdapter adp = new SqlDataAdapter(komutver);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                hdfId.Value = id.ToString();
                lblIlce.Text = ds.Tables[0].Rows[0]["ilceAd"].ToString();
                lbSehir.Text = ds.Tables[0].Rows[0]["sehirAd"].ToString();
                txtZiyaretciAd.Text = ds.Tables[0].Rows[0]["adSoyad"].ToString();
                txtCevap.Text = GenelFonksiyonlar.BRDonusturEnter( ds.Tables[0].Rows[0]["yorumCevap"].ToString());

                txtZiyaretciYorum.Text = GenelFonksiyonlar.BRDonusturEnter(ds.Tables[0].Rows[0]["yorum"].ToString());
                ckbDurum.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["durum"]);
                
            }
        }
    }
    #endregion

    #region Ziyaretçi Yorum Durum İşlemi
    private void aktifPasif()
    {
        try
        {
            using (SqlConnection baglan = new SqlConnection(ConnectionString.Get))
            {
                SqlCommand komutver = new SqlCommand();
                baglan.Open();
                komutver.Connection = baglan;
                komutver.CommandText = "ziyaretci_DefteriDurum";
                komutver.CommandType = CommandType.StoredProcedure;
                komutver.Parameters.Add("@id", SqlDbType.Int);
                komutver.Parameters["@id"].Value = Convert.ToInt32(Request.Params["id"]);
                komutver.Parameters.Add("@durum", SqlDbType.Int);
                komutver.Parameters["@durum"].Value = Convert.ToInt32(Request.Params["durum"]);
                komutver.ExecuteNonQuery();
                
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>Durum Degiştirme Hatası: </b>", ex);
        }
    }
    #endregion

    #region Ziyaretçi Yorum Silme İşlemi
    private void kayitSil(int id)
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@id",id);
            SqlHelper.ExecuteNonQuery("ziyaretci_DefteriSil",parametre);
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>Silme Hatası:</b>  ", ex);
        }
    }

    #endregion

    #region Ziyaretçi Defteri Listeleme İşlemi
    private void ziyaretciDefteriListele()
    {
        try
        {
            SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@Baslangic", Baslangic),
                    new SqlParameter("@Bitis", Bitis),
                    new SqlParameter("@parametre", "admin")
                };

            using (SqlDataReader dr = SqlHelper.ExecuteReader("ziyaretci_DefteriListele", parametre))
	         { List<ZiyaretciDefteri> ziyaretciler = new List<ZiyaretciDefteri>();
             string yorumCevap = string.Empty;
                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("yorumCevap")))
                    {
                        yorumCevap = dr.GetString(dr.GetOrdinal("yorumCevap"));
                    }

                    ZiyaretciDefteri info = new ZiyaretciDefteri(
                        dr.GetInt32(dr.GetOrdinal("id")),
                        dr.GetString(dr.GetOrdinal("adSoyad")),
                        dr.GetString(dr.GetOrdinal("ePosta")),
                        dr.GetString(dr.GetOrdinal("yorum")),
                        dr.GetDateTime(dr.GetOrdinal("eklenmeTarihi")),
                        dr.GetBoolean(dr.GetOrdinal("durum")),
                        dr.GetString(dr.GetOrdinal("sehirAd")),
                        dr.GetString(dr.GetOrdinal("ilceAd")),
                        yorumCevap);

                    ziyaretciler.Add(info);
              
                }

                GridView1.DataSource = ziyaretciler;
                GridView1.DataBind();
	        }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Listeleme Hatası", ex);
        }
    }
    #endregion

    #region Ziyaretçi Yorum Güncelleme İşlemi
    protected void btnZiyaretciYorumGuncelle_Click(object sender, EventArgs e)
    {
        try
        {
            using(SqlConnection baglan = new SqlConnection(ConnectionString.Get))
	        {
                baglan.Open();
                using (SqlCommand komutver = new SqlCommand())
                {
                    komutver.Connection = baglan;
                    komutver.CommandText = "ziyaretci_DefteriGuncelle";
                    komutver.CommandType = CommandType.StoredProcedure;
                    komutver.Parameters.Add("@id", SqlDbType.Int);
                    komutver.Parameters["@id"].Value = Convert.ToInt32(hdfId.Value);
                    komutver.Parameters.Add("@adSoyad", SqlDbType.NVarChar);
                    komutver.Parameters["@adSoyad"].Value = txtZiyaretciAd.Text.ToString();
                    komutver.Parameters.Add("@yorum", SqlDbType.NVarChar);
                    komutver.Parameters["@yorum"].Value = GenelFonksiyonlar.EnterDonusturBr(txtZiyaretciYorum.Text.ToString());

                    komutver.Parameters.Add("@yorumCevap", SqlDbType.NVarChar);
                    komutver.Parameters["@yorumCevap"].Value = GenelFonksiyonlar.EnterDonusturBr(txtCevap.Text.ToString());
                    komutver.Parameters.Add("@durum", SqlDbType.Bit);
                    komutver.Parameters["@durum"].Value = Convert.ToBoolean(ckbDurum.Checked);

                    komutver.ExecuteNonQuery();

                    txtZiyaretciAd.Text = "";
                    txtZiyaretciYorum.Text = "";
                  
                    pnlYorumArama.Visible = true;
                    mesajGosterOk("Kayıt Başarı ile Güncellendi.");
                }
	        }
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>YORUM GÖNDERME HATAS: </b> ", ex);
        }
    }
    #endregion

    #region Tarih Aralığı Yorum Arama işlemi
    private void tarihArama()
    {
        using (SqlConnection baglan = new SqlConnection(ConnectionString.Get))
        {
            baglan.Open();
            SqlCommand komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "ziyaretci_DefteriTarihArama";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@baslangicTarih", SqlDbType.DateTime);
            komutver.Parameters["@baslangicTarih"].Value = Request.Params["tarih_1"].ToString();
            komutver.Parameters.Add("@bitisTarih", SqlDbType.DateTime);
            string tarih = Request.Params["tarih_2"].ToString() + " 23:59:59";

            komutver.Parameters["@bitisTarih"].Value = Convert.ToDateTime(tarih);

            SqlDataReader dr = komutver.ExecuteReader(CommandBehavior.CloseConnection);
            List<ZiyaretciDefteri> ziyaretciler = new List<ZiyaretciDefteri>();
            int count = 0;
            string yorumCevap = string.Empty;
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("yorumCevap")))
                {
                    yorumCevap = dr.GetString(dr.GetOrdinal("yorumCevap"));
                }

                ZiyaretciDefteri info = new ZiyaretciDefteri(
                    dr.GetInt32(dr.GetOrdinal("id")),
                    dr.GetString(dr.GetOrdinal("adSoyad")),
                    dr.GetString(dr.GetOrdinal("ePosta")),
                    dr.GetString(dr.GetOrdinal("yorum")),
                    dr.GetDateTime(dr.GetOrdinal("eklenmeTarihi")),
                    dr.GetBoolean(dr.GetOrdinal("durum")),
                    dr.GetString(dr.GetOrdinal("sehirAd")),
                    dr.GetString(dr.GetOrdinal("ilceAd")),
                    yorumCevap);

                ziyaretciler.Add(info);
                count = count + 1;
            }

            string birlestir = null;
            if (count == 0)
            {
                birlestir = "<b>" + Request.Params["tarih_1"].ToString() + " " + Request.Params["tarih_2"].ToString() + "</b> Tarih Aralığına Ait Soru Bulunamadı.";
                mesajGosterNo(birlestir.ToString());

            }
            else
            {
                birlestir = "<b>" + Request.Params["tarih_1"].ToString() + " " + Request.Params["tarih_2"].ToString() + "</b> Tarih Aralığına Ait Toplam <b>" + count + "</b> Soru Bulundu.";
                mesajGosterInfo(birlestir.ToString());
            }

            GridView1.DataSource = ziyaretciler;
            GridView1.DataBind();
            
        }
    }
    #endregion

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnGuncell")
        {
            ziyaretciDefteriGetir(Convert.ToInt32(e.CommandArgument));
            ModalPopupExtender.Show();
        }
    }
}
