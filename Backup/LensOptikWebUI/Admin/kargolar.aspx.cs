using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using BusinessLayer;
using DataAccessLayer;
using BusinessLayer.BasePage;

public partial class AdminKargolar : BasePageAdmin
{
    SqlConnection baglan;
    SqlDataAdapter adp;
    SqlCommand komutver;
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        baglan = new SqlConnection(ConnectionString.Get);

        if (!IsPostBack)
        {
            lblSayfaBaslik.Text = "Kargolar";

            if (Request.Params["islem"] == "durum")
            {
                aktifPasif();
            }

            if (Request.Params["islem"] == "sil")//Silme İşlemi 
            {
                mesajGosterInfo("Kargo Silemezsiniz Var olan kargo bilgisini düzenleyiniz.");
               // KayitSil(Convert.ToInt32(Request.Params["id"]));
            }

            kargolarListele();

            if (Request.Params["islem"] == "duzenle")
            {
                btnKargoEkle.Visible = false;
                btnKargoDuzenle.Visible = true;
                kargoDuzenleBilgiGetir();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "tabAc1", "tabAc()", true);
            }     
        }
    }


    #region Kargo Kayıt Silme İşlemi
    private void KayitSil(int id)
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "kargo_KayitSil";
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

    #region Kargo Durum İşlemi
    private void aktifPasif()
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "kargo_Durum";
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

    #region Kargo  Ekleme İşlemi 
    protected void btnKargoEkle_Click(object sender, EventArgs e)
    {
        mesajGosterInfo("Alişveriş sistemi tek kargo işlemi üzerine kurulmuştur, yeni kargo ekleyemezsiniz.");
        return;

        //try
        //{

        //    baglan.Open();
        //    komutver = new SqlCommand();
        //    komutver.Connection = baglan;
        //    komutver.CommandText = "kargo_KayitEkle";
        //    komutver.CommandType = CommandType.StoredProcedure;
        //    komutver.Parameters.Add("@kargoAdi", SqlDbType.NVarChar);
        //    komutver.Parameters["@kargoAdi"].Value = txtKargoAdi.Text.ToString();
        //    komutver.Parameters.Add("@kapidaOdeme", SqlDbType.NVarChar);
        //    komutver.Parameters["@kapidaOdeme"].Value = ddlKapidaOdeme.SelectedValue.ToString();

        //    komutver.Parameters.Add("@kapidaOdemeFark", SqlDbType.Money);
        //    komutver.Parameters["@kapidaOdemeFark"].Value = Convert.ToDecimal(txtOdemeFarki.Text); 

        //    komutver.Parameters.Add("@desi_1_3", SqlDbType.Money);
        //    komutver.Parameters["@desi_1_3"].Value = txtDesi_1_3.Text.ToString();
        //    komutver.Parameters.Add("@desi_4_10", SqlDbType.Money);
        //    komutver.Parameters["@desi_4_10"].Value = txtDesi_4_10.Text.ToString();
        //    komutver.Parameters.Add("@desi_11_20", SqlDbType.Money);
        //    komutver.Parameters["@desi_11_20"].Value = txtDesi_11_20.Text.ToString();
        //    komutver.Parameters.Add("@desi_21_30", SqlDbType.Money);
        //    komutver.Parameters["@desi_21_30"].Value = txtDesi_21_30.Text.ToString();
        //    komutver.Parameters.Add("@desi_31_40", SqlDbType.Money);
        //    komutver.Parameters["@desi_31_40"].Value = txtDesi_31_40.Text.ToString();
        //    komutver.Parameters.Add("@desi_41_50", SqlDbType.Money);
        //    komutver.Parameters["@desi_41_50"].Value = txtDesi_41_50.Text.ToString();
        //    komutver.Parameters.Add("@desi_50", SqlDbType.Money);
        //    komutver.Parameters["@desi_50"].Value = txtDesi_50.Text.ToString();
        //    komutver.Parameters.Add("@durum", SqlDbType.Bit);
        //    komutver.Parameters["@durum"].Value = Convert.ToBoolean(ckbDurum.Checked);
        //    komutver.Parameters.Add("@deger_dondur", SqlDbType.Int);
        //    komutver.Parameters["@deger_dondur"].Direction = ParameterDirection.Output;
        //    komutver.ExecuteNonQuery();


        //    int donendeger = Convert.ToInt32(komutver.Parameters["@deger_dondur"].Value);

        //    if (donendeger == 0)
        //    {
        //        mesajGosterOk("Kargo Kaydı Başarı ile Yapıldı.");
            

        //        txtKargoAdi.Text = " ";
        //        txtDesi_1_3.Text = " ";
        //        txtDesi_4_10.Text = " ";
        //        txtDesi_11_20.Text = " "; 
        //        txtDesi_21_30.Text = " ";
        //        txtDesi_31_40.Text = " ";
        //        txtDesi_41_50.Text = " ";
        //        txtDesi_50.Text = " ";
        //        kargolarListele();
        //    }
        //    else
        //    {
        //        mesajGosterNo("Bu Ad ile  Kargo  Bulunmakta.");
        //    }
        //}
        //catch (Exception hata)
        //{
        //    mesajGosterSis("Kargo hatası:", hata.ToString());
        //}
        //finally
        //{
        //    baglan.Close();
        //}
    }
    #endregion

    #region  Kargo Bilgisi  Listele
    private void kargolarListele()
    {
        try
        {
            gvwKargolar.DataSource = KargoDB.Liste("admin");
            gvwKargolar.DataBind();   
        }
        catch (Exception ex)
        {
            mesajGosterSis("Listeleme Hatası", ex);
        }
    }
    #endregion

    #region Güncelleme için Bilgi Getirme
    private void kargoDuzenleBilgiGetir()
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "kargo_KayitGetir";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(Request.Params["id"]);

            adp = new SqlDataAdapter(komutver);
            ds = new DataSet();
            adp.Fill(ds);

            txtKargoAdi.Text =   ds.Tables[0].Rows[0]["kargoAdi"].ToString();
            txtDesi_1_3.Text =   Convert.ToDecimal(ds.Tables[0].Rows[0]["desi_1_3"]).ToString("N");
            txtDesi_4_10.Text =  Convert.ToDecimal(ds.Tables[0].Rows[0]["desi_4_10"]).ToString("N");   
            txtDesi_11_20.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["desi_11_20"]).ToString("N");
            txtDesi_21_30.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["desi_21_30"]).ToString("N");
            txtDesi_31_40.Text = Convert.ToDecimal( ds.Tables[0].Rows[0]["desi_31_40"]).ToString("N");
            txtDesi_41_50.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["desi_41_50"]).ToString("N");
            txtDesi_50.Text =    Convert.ToDecimal(ds.Tables[0].Rows[0]["desi_50"]).ToString("N");
            txtOdemeFarki.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["kapidaOdemeFark"]).ToString("N");

            

            string kapidaOdeme = ds.Tables[0].Rows[0]["kapidaOdeme"].ToString();

            for (int i = 0; i < ddlKapidaOdeme.Items.Count; i++)
            {

                if (ddlKapidaOdeme.Items[i].Value == kapidaOdeme.ToString())
                {
                    ddlKapidaOdeme.Items[i].Selected = true;
                }
            }

            ckbDurum.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["durum"]);
        }
        catch (Exception ex)
        {
            mesajGosterSis("GELEN VERİ HATASI:",  ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region Kargo Kayıt Düzenle İşlemi
    protected void btnKargoDuzenle_Click(object sender, EventArgs e)
    {
        try
        {
            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "kargo_KayitGuncelle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(Request.Params["id"]);
            komutver.Parameters.Add("@kargoAdi", SqlDbType.NVarChar);
            komutver.Parameters["@kargoAdi"].Value = txtKargoAdi.Text.ToString();
            komutver.Parameters.Add("@kapidaOdeme", SqlDbType.NVarChar);
            komutver.Parameters["@kapidaOdeme"].Value = ddlKapidaOdeme.SelectedValue.ToString();
            komutver.Parameters.Add("@kapidaOdemeFark", SqlDbType.Money);
            komutver.Parameters["@kapidaOdemeFark"].Value = Convert.ToDecimal(txtOdemeFarki.Text); 
            komutver.Parameters.Add("@desi_1_3", SqlDbType.Money);
            komutver.Parameters["@desi_1_3"].Value = txtDesi_1_3.Text.ToString();
            komutver.Parameters.Add("@desi_4_10", SqlDbType.Money);
            komutver.Parameters["@desi_4_10"].Value = txtDesi_4_10.Text.ToString();
            komutver.Parameters.Add("@desi_11_20", SqlDbType.Money);
            komutver.Parameters["@desi_11_20"].Value = txtDesi_11_20.Text.ToString();
            komutver.Parameters.Add("@desi_21_30", SqlDbType.Money);
            komutver.Parameters["@desi_21_30"].Value = txtDesi_21_30.Text.ToString();
            komutver.Parameters.Add("@desi_31_40", SqlDbType.Money);
            komutver.Parameters["@desi_31_40"].Value = txtDesi_31_40.Text.ToString();
            komutver.Parameters.Add("@desi_41_50", SqlDbType.Money);
            komutver.Parameters["@desi_41_50"].Value = txtDesi_41_50.Text.ToString();
            komutver.Parameters.Add("@desi_50", SqlDbType.Money);
            komutver.Parameters["@desi_50"].Value = txtDesi_50.Text.ToString();
            komutver.Parameters.Add("@durum", SqlDbType.Bit);
            komutver.Parameters["@durum"].Value = Convert.ToBoolean(ckbDurum.Checked);
            komutver.Parameters.Add("@deger_dondur", SqlDbType.Int);
            komutver.Parameters["@deger_dondur"].Direction = ParameterDirection.Output;
            komutver.ExecuteNonQuery();

            mesajGosterOk("Kargo kaydı başarı ile güncellendi.");

            btnKargoDuzenle.Visible = false;
            btnKargoEkle.Visible = true;
          
            txtKargoAdi.Text = " ";
            txtDesi_1_3.Text = " ";
            txtDesi_4_10.Text = " ";
            txtDesi_11_20.Text = " ";
            txtDesi_21_30.Text = " ";
            txtDesi_31_40.Text = " ";
            txtDesi_41_50.Text = " ";
            txtDesi_50.Text = " ";
            txtOdemeFarki.Text = "";
            kargolarListele();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Kargo Kayıt Güncelleme  hatası:", ex);
        }
        finally
        {
            baglan.Close();
        }

    }
    #endregion
 
}
