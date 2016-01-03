using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using ModelLayer;
using BusinessLayer;
using DataAccessLayer;
using BusinessLayer.BasePage;

public partial class bankaHesaplari : BasePageAdmin
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
            if (Request.Params["islem"] == "durum")
            {
                aktifPasif();
            }

            if (Request.Params["islem"] == "sil")//Silme İşlemi 
            {
                KayitSil(Convert.ToInt32(Request.Params["id"]));
            }

            BankaHesapBilgiListele();

            if (Request.Params["islem"] == "duzenle")
            {
                btnBankaHesapEkle.Visible = false;
                btnHesapDuzenle.Visible = true;
                hesapDuzenleBilgiGetir();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "tabAcBanka", "tabAc()", true);

            }   
        }
    }


    #region Banka Kayıt Silme İşlemi
    private void KayitSil(int id)
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "banka_HesapSil";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(id);
            komutver.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Kayit Silme Hatası" , ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region Hesap Durum İşlemi
    private void aktifPasif()
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "banka_HesapDurum";
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

    #region Banka Hesap Bilgisi  Listele
    private void BankaHesapBilgiListele()
    {
        try
        {
            gvwBankaHesap.DataSource = BankaHesaplariDB.HesapListe("admin");
            gvwBankaHesap.DataBind();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Listeleme Hatası", ex);
        }
    }
    #endregion

    #region Güncelleme için Bilgi Getirme
    private void hesapDuzenleBilgiGetir()
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "banka_HesapGetir";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@bankaAdi", SqlDbType.NVarChar);
            komutver.Parameters["@bankaAdi"].Value = Request.Params["bankaAdi"];

            adp = new SqlDataAdapter(komutver);
            ds = new DataSet();
            adp.Fill(ds);

            txtBankaAdi.Text = ds.Tables[0].Rows[0]["bankaAdi"].ToString();
            txtSube.Text = ds.Tables[0].Rows[0]["sube"].ToString();
            txSubeKodu.Text = ds.Tables[0].Rows[0]["subeKod"].ToString();
            txtIban.Text = ds.Tables[0].Rows[0]["iban"].ToString();
            txtHesapNo.Text = ds.Tables[0].Rows[0]["hesapNo"].ToString();
            txtHesapAdi.Text = ds.Tables[0].Rows[0]["hesapAdi"].ToString();
            ckbDurum.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["durum"]);
        
            ddlHesapTipi.ClearSelection();

            string hesapTipi = ds.Tables[0].Rows[0]["hesapTipi"].ToString();

            for (int i = 0; i < ddlHesapTipi.Items.Count; i++)
            {
                if (ddlHesapTipi.Items[i].Value == hesapTipi.ToString())
                {
                    ddlHesapTipi.Items[i].Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis( "GELEN VERİ HATASI:" , ex);
        }
        finally
        {
          baglan.Close();    
        }
    }
    #endregion

    #region Banka Hesabı Ekleme İşlemi
    protected void btnBankaHesapEkle_Click(object sender, EventArgs e)
    {
        try
        {
            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "banka_HesapEkle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@bankaAdi", SqlDbType.NVarChar);
            komutver.Parameters["@bankaAdi"].Value = txtBankaAdi.Text.ToString().Trim();
            komutver.Parameters.Add("@sube", SqlDbType.NVarChar);
            komutver.Parameters["@sube"].Value = txtSube.Text.ToString();
            komutver.Parameters.Add("@subeKod", SqlDbType.NVarChar);
            komutver.Parameters["@subeKod"].Value = txSubeKodu.Text.ToString();
            komutver.Parameters.Add("@iban", SqlDbType.NVarChar);
            komutver.Parameters["@iban"].Value = txtIban.Text.ToString();
            komutver.Parameters.Add("@hesapAdi", SqlDbType.NVarChar);
            komutver.Parameters["@hesapAdi"].Value = txtHesapAdi.Text.ToString();
            komutver.Parameters.Add("@hesapTipi", SqlDbType.NVarChar);
            komutver.Parameters["@hesapTipi"].Value = ddlHesapTipi.SelectedValue.ToString();
            komutver.Parameters.Add("@hesapNo ", SqlDbType.NVarChar);
            komutver.Parameters["@hesapNo "].Value = txtHesapNo.Text.ToString();
            komutver.Parameters.Add("@durum", SqlDbType.Bit);
            komutver.Parameters["@durum"].Value = Convert.ToBoolean(ckbDurum.Checked);
            komutver.ExecuteNonQuery();

            mesajGosterOk("Banka Hesap Bilgisi Başarı ile Kaydedildi.");
            BankaHesapBilgiListele();
          

            txtBankaAdi.Text = " ";
            txtSube.Text = " ";
            txSubeKodu.Text = " ";
            txtIban.Text = " ";
            txtHesapAdi.Text = " ";
            txtHesapNo.Text = " ";

        }
        catch (Exception ex)
        {
            mesajGosterSis("Banka Hesap  hatası:", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region Hesap Bilgi Düzenle İşlemi
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "banka_HesapGuncelle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(Request.Params["id"]);
            komutver.Parameters.Add("@bankaAdi", SqlDbType.NVarChar);
            komutver.Parameters["@bankaAdi"].Value = txtBankaAdi.Text.ToString().Trim();
            komutver.Parameters.Add("@sube", SqlDbType.NVarChar);
            komutver.Parameters["@sube"].Value = txtSube.Text.ToString();
            komutver.Parameters.Add("@subeKod", SqlDbType.NVarChar);
            komutver.Parameters["@subeKod"].Value = txSubeKodu.Text.ToString();
            komutver.Parameters.Add("@iban", SqlDbType.NVarChar);
            komutver.Parameters["@iban"].Value = txtIban.Text.ToString();
            komutver.Parameters.Add("@hesapAdi", SqlDbType.NVarChar);
            komutver.Parameters["@hesapAdi"].Value = txtHesapAdi.Text.ToString();
            komutver.Parameters.Add("@hesapTipi", SqlDbType.NVarChar);
            komutver.Parameters["@hesapTipi"].Value = ddlHesapTipi.SelectedValue.ToString();
            komutver.Parameters.Add("@hesapNo ", SqlDbType.NVarChar);
            komutver.Parameters["@hesapNo "].Value = txtHesapNo.Text.ToString();
            komutver.Parameters.Add("@durum", SqlDbType.Bit);
            komutver.Parameters["@durum"].Value = Convert.ToBoolean(ckbDurum.Checked);
            komutver.ExecuteNonQuery();

            mesajGosterOk("Banka Hesap Bilgisi Başarı ile Güncellendi.");
            BankaHesapBilgiListele();
            btnBankaHesapEkle.Visible = true;
            btnHesapDuzenle.Visible = false;

            txtBankaAdi.Text = " ";
            txtSube.Text = " ";
            txSubeKodu.Text = " ";
            txtIban.Text = " ";
            txtHesapAdi.Text = " ";
            txtHesapNo.Text = " ";

        }
        catch (Exception ex)
        {
            mesajGosterSis("Banka Hesap Güncelleme  hatası:", ex);
        }
        finally
        {
            baglan.Close();
        }

    }
    #endregion
}
