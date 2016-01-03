using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessLayer.BasePage;
using DataAccessLayer;
using ModelLayer;

public partial class adminAnket : BasePageAdmin
{
    SqlConnection baglan;
    SqlCommand komutver;

    protected void Page_Load(object sender, EventArgs e)
    {
        baglan = new SqlConnection(ConnectionString.Get);

        if (!IsPostBack)
        {
            if (Request.Params["islem"] == "durum")
            {
                aktifPasif();
            }

            if (Request.Params["islem"] == "anketSoru")
            {
                anketSoruDurum();
                Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString());
            }

            if (Request.Params["islem"] == "sil")//Silme İşlemi 
            {
                KayitSil(Convert.ToInt32(Request.Params["id"]));
            }

            if (Request.Params["anketSoru"] == "sil")//Silme İşlemi 
            {
                AnketSoruSil(Convert.ToInt32(Request.Params["id"]));
                Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString());
            }

            AnketListele();

            if (Request.Params["islem"] == "duzenle")
            {
                pnlAnketDuzenle.Visible = true;
                pnlAnketEkle.Visible = false;
                txtAnketDuzenle.Text = Request.Params["ad"].ToString();
                hdfAnketId.Value = Request.Params["id"].ToString();
                AnketSoruListele(Convert.ToInt32(Request.Params["id"]));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "anket1", "tabAc()", true);
            }       
        }
    }

    // Anket İşlemleri
    #region Anket Kayıt Listele
    private void AnketListele()
    {
        try
        {
            gvwAnketler.DataSource = AnketDB.GetByParam("admin");
            gvwAnketler.DataBind();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Listeleme Hatası", ex);
        }
    }
    #endregion

    #region Anket Kayıt Silme İşlemi
    private void KayitSil(int id)
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "anket_KayitSil";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(id);
            komutver.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

            mesajGosterSis("Kayit Silme Hatası", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region Anket Başlık Güncelleme İşlemi
    protected void txtAnketDuzenle_TextChanged(object sender, EventArgs e)
    {
        try
        {
            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "anket_KayitGuncelle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(hdfAnketId.Value);
            komutver.Parameters.Add("@anketBaslik", SqlDbType.NVarChar);
            komutver.Parameters["@anketBaslik"].Value = txtAnketDuzenle.Text.ToString();
            komutver.ExecuteNonQuery();
            AnketListele();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Anket Ekleme   Hatası:", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region Anket Başlık Ekle
    protected void btnAnketBaslikEkle_Click(object sender, EventArgs e)
    {
        try
        {
            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "anket_KayitEkle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@anketBaslik", SqlDbType.NVarChar);
            komutver.Parameters["@anketBaslik"].Value = txtAnketAdi.Text.ToString();
            komutver.Parameters.Add("@deger_dondur", SqlDbType.Int);
            komutver.Parameters["@deger_dondur"].Direction = ParameterDirection.Output;
            komutver.ExecuteNonQuery();


            int donenDeger = Convert.ToInt32(komutver.Parameters["@deger_dondur"].Value);
            if (donenDeger == 1)
            {
                mesajGosterNo("Bu Başlık Adı İle Anken  Mevcut.");
            }
            else
            {

                pnlAnketEkle.Visible = false;
                pnlAnketDuzenle.Visible = true;
                txtAnketDuzenle.Text = txtAnketAdi.Text.ToString();
                hdfAnketId.Value = donenDeger.ToString();
                AnketListele();

                mesajGosterInfo("<span class='satirYukseklik'>1. Anket Başlık Kaydı Başarı ile Oluşturuldu. <br/> 2. Anket Başlığı İçin Soru Oluşturunuz.</span> ");
              
            }

        }
        catch (Exception ex)
        {
            mesajGosterSis("Anket Ekleme   Hatası:", ex);
        }
        finally
        {
            baglan.Close();
        }

    }
    #endregion

    #region Anket Başlık  Durum İşlemi
    private void aktifPasif()
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "anket_Durum";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(Request.Params["id"]);
            komutver.Parameters.Add("@durum", SqlDbType.Int);
            komutver.Parameters["@durum"].Value = Convert.ToInt32(Request.Params["durum"]);
            komutver.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>Durum Degiştirme Hatası: </b>", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    // Anket Soru İşlemleri 
    #region Anket Soru Ekleme İşlemi
    protected void btnAnketSoruEkle_Click(object sender, EventArgs e)
    {
        try
        {
            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "anketSorular_KayitEkle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(hdfAnketId.Value);
            komutver.Parameters.Add("@soru", SqlDbType.NVarChar);
            komutver.Parameters["@soru"].Value = txtAnketSoru.Text.ToString();
            komutver.ExecuteNonQuery();


            AnketSoruListele(Convert.ToInt32(hdfAnketId.Value));
            txtAnketSoru.Text = "";
        }
        catch (Exception ex)
        {
            mesajGosterSis("Anket Ekleme   Hatası:", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region  Anket Soru Güncelleme İşlemi
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SoruKaydet")
        {
            string index = e.CommandArgument.ToString();
 
            int soruId =0;
            string anketSoruAd = null;
            string anketOylama = null;

            foreach (GridViewRow item in GridView2.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    if (GridView2.DataKeys[item.RowIndex].Value.ToString() == index.ToString())
                    {
                        soruId = Convert.ToInt32(e.CommandArgument);
                        anketSoruAd = (item.FindControl("txtAnketDuzenle") as TextBox).Text;
                        anketOylama = (item.FindControl("txtAnketOylama") as TextBox).Text;
                    }
                }
            }


            if(soruId != 0 && anketSoruAd != null && anketOylama != null)
            {

                try
                {
                    baglan.Open();
                    komutver = new SqlCommand();
                    komutver.Connection = baglan;
                    komutver.CommandText = "anketSoru_Guncelle";
                    komutver.CommandType = CommandType.StoredProcedure;
                    komutver.Parameters.Add("@id", SqlDbType.Int);
                    komutver.Parameters["@id"].Value = Convert.ToInt32(soruId);
                    komutver.Parameters.Add("@soru", SqlDbType.NVarChar);
                    komutver.Parameters["@soru"].Value = anketSoruAd.ToString();
                    komutver.Parameters.Add("anketOy", SqlDbType.Int);
                    komutver.Parameters["anketOy"].Value = Convert.ToInt32(anketOylama);
                    komutver.ExecuteNonQuery();
                    AnketListele();
                }
                catch (Exception ex)
                {
                    mesajGosterSis("Anket Ekleme   Hatası:", ex);
                }
                finally
                {
                    baglan.Close();
                }
            }
        }
    }
    #endregion

    #region Anket Soru Silme İşlemi
    private void AnketSoruSil(int id)
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "anketSorulari_KayitSil";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(id);
            komutver.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            mesajGosterSis("Kayit Silme Hatası", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region Anket Soru  Kayıt Listele
    private void AnketSoruListele(int id)
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@id",id);
            List<Anket> anketSoruGoster = new List<Anket>();


            using (SqlDataReader dr = SqlHelper.ExecuteReader("anket_KayitGetir", parametre))
            {
               
                while (dr.Read())
                {
                    Anket info = new Anket(
                        dr.GetInt32(dr.GetOrdinal("soruId")),
                        dr.GetInt32(dr.GetOrdinal("anketOy")),
                        dr.GetString(dr.GetOrdinal("soru")),
                        dr.GetBoolean(dr.GetOrdinal("durum")));

                    anketSoruGoster.Add(info);
                } 
            }

            GridView2.DataSource = anketSoruGoster;
            GridView2.DataBind();          
        }
        catch (Exception ex)
        {
            mesajGosterSis("Listeleme Hatası : ", ex);
        }
    }
    #endregion

    #region Anket Soru  Durum İşlemi
    private void anketSoruDurum()
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "anketSoru_Durum";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(Request.Params["id"]);
            komutver.Parameters.Add("@durum", SqlDbType.Int);
            komutver.Parameters["@durum"].Value = Convert.ToInt32(Request.Params["durum"]);
            komutver.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>Durum Degiştirme Hatası: </b>", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

}
