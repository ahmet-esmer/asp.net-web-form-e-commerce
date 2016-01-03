using System;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer;
using BusinessLayer.BasePage;

public partial class adminGenelAyarlar : BasePageAdmin
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
            siteIletisimBilgiGetir();
            mailServerBilgiGetir();
            SMSBilgiGetir();

            if (Request.Params["mailDuzenle"] == "ok")
            {
            mesajGosterOk("Server Mail Bilgisi Başarı İle Düzenlendi.");
            }

            if (Request.Params["smsDuzenle"] == "ok")
            {
                mesajGosterOk("SMS Bilgisi Başarı İle Düzenlendi.");
            }
        }
    }

    #region İletişim  Bilgi Getirme Güncelleme için
    private void siteIletisimBilgiGetir()
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "SELECT * FROM tbl_site_iletisim WHERE id=1";
            komutver.CommandType = CommandType.Text;

            adp = new SqlDataAdapter(komutver);
            ds = new DataSet();
            adp.Fill(ds);

            txtFirmaAdi.Text = ds.Tables[0].Rows[0]["fimaAdi"].ToString();
            txtYetkili.Text = ds.Tables[0].Rows[0]["yetkiliKisi"].ToString();
            txtTelefon.Text = ds.Tables[0].Rows[0]["telefon"].ToString();
            txtFaks.Text = ds.Tables[0].Rows[0]["faks"].ToString();
            txtVergiDairesi.Text = ds.Tables[0].Rows[0]["vergiDairesi"].ToString();
            txtVergiNo.Text = ds.Tables[0].Rows[0]["vergiNo"].ToString();
            txtEposta.Text = ds.Tables[0].Rows[0]["ePosta"].ToString();
            txtAdres.Text = GenelFonksiyonlar.BRDonusturEnter( ds.Tables[0].Rows[0]["adres"].ToString());
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

    #region Mail Server  Bilgi Getirme
    private void mailServerBilgiGetir()
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "SELECT * FROM tbl_site_iletisim WHERE id=2";
            komutver.CommandType = CommandType.Text;

            adp = new SqlDataAdapter(komutver);
            ds = new DataSet();
            adp.Fill(ds);

            txtMailServer.Text = ds.Tables[0].Rows[0]["fimaAdi"].ToString();
            txtkullaniciAdi.Text = ds.Tables[0].Rows[0]["yetkiliKisi"].ToString();
            txtGonderenMail.Text = ds.Tables[0].Rows[0]["telefon"].ToString();
            txtMailSifre.Text = ds.Tables[0].Rows[0]["faks"].ToString();

        }
        catch (Exception ex)
        {
            mesajGosterSis("MAİL SERVER GELEN VERİ HATASI:", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region İletişim Güncelleme İşlemi
    protected void btnIletisimBilgisiDuzenle_Click(object sender, EventArgs e)
    {
        try
        {
            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "site_iletisimBilgiDuzenle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@fimaAdi", SqlDbType.NVarChar);
            komutver.Parameters["@fimaAdi"].Value = txtFirmaAdi.Text.ToString();
            komutver.Parameters.Add("@yetkiliKisi", SqlDbType.NVarChar);
            komutver.Parameters["@yetkiliKisi"].Value = txtYetkili.Text.ToString();
            komutver.Parameters.Add("@telefon", SqlDbType.NVarChar);
            komutver.Parameters["@telefon"].Value = txtTelefon.Text.ToString();
            komutver.Parameters.Add("@faks", SqlDbType.NVarChar);
            komutver.Parameters["@faks"].Value = txtFaks.Text.ToString();
            komutver.Parameters.Add("@vergiDairesi", SqlDbType.NVarChar);
            komutver.Parameters["@vergiDairesi"].Value = txtVergiDairesi.Text.ToString();
            komutver.Parameters.Add("@vergiNo", SqlDbType.NVarChar);
            komutver.Parameters["@vergiNo"].Value = txtVergiNo.Text.ToString();
            komutver.Parameters.Add("@ePosta", SqlDbType.NVarChar);
            komutver.Parameters["@ePosta"].Value = txtEposta.Text.ToString();
            komutver.Parameters.Add("@adres", SqlDbType.NVarChar);
            komutver.Parameters["@adres"].Value = GenelFonksiyonlar.EnterDonusturBr( txtAdres.Text.ToString());
       
            komutver.ExecuteNonQuery();

            mesajGosterOk("Site İletişim Bilgisi Başarı İle Düzenlendi.");
        }
        catch (Exception ex)
        {
            mesajGosterSis("Site İletişim güncelleme hatası:", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    # region Mail Server Düzenleme İşlemi 
    protected void btnMailServer_Click(object sender, EventArgs e)
    {
        try
        {
            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "site_MailServerDuzenle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@mailServer", SqlDbType.NVarChar);
            komutver.Parameters["@mailServer"].Value = txtMailServer.Text.ToString();
            komutver.Parameters.Add("@kullaniciAdi", SqlDbType.NVarChar);
            komutver.Parameters["@kullaniciAdi"].Value = txtkullaniciAdi.Text.ToString();
            komutver.Parameters.Add("@gonderenMail", SqlDbType.NVarChar);
            komutver.Parameters["@gonderenMail"].Value = txtGonderenMail.Text.ToString();
            komutver.Parameters.Add("@sifre", SqlDbType.NVarChar);
            komutver.Parameters["@sifre"].Value = txtMailSifre.Text.ToString();
            komutver.ExecuteNonQuery();

            Response.Redirect("genel_ayarlar.aspx?mailDuzenle=ok");
        }
        catch (Exception ex)
        {
            mesajGosterSis("Server Mail  güncelleme hatası:", ex);
        }
        finally
        {
            baglan.Close();
        }

    }
    #endregion

    #region SMS  Bilgi Getirme
    private void SMSBilgiGetir()
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "SELECT fimaAdi,yetkiliKisi,adres,telefon FROM tbl_site_iletisim WHERE id=3";
            komutver.CommandType = CommandType.Text;

            adp = new SqlDataAdapter(komutver);
            ds = new DataSet();
            adp.Fill(ds);
            txtSmsKullanici.Text = ds.Tables[0].Rows[0]["fimaAdi"].ToString();
            txtSmsSifre.Attributes.Add("value", ds.Tables[0].Rows[0]["yetkiliKisi"].ToString());
            txtSistemSmsTelefon.Text = ds.Tables[0].Rows[0]["telefon"].ToString();
            txtOriginator.Text = ds.Tables[0].Rows[0]["adres"].ToString();

        }
        catch (Exception ex)
        {
            mesajGosterSis("SMS GELEN VERİ HATASI:", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region SMS Düzenleme İşlemi
    protected void btnSMSkayit_Click(object sender, EventArgs e)
    {
        try
        {
            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "site_SmsBilgiDuzenle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@smsKullaniciAdi", SqlDbType.NVarChar);
            komutver.Parameters["@smsKullaniciAdi"].Value = txtSmsKullanici.Text.ToString();
            komutver.Parameters.Add("@smsSifre", SqlDbType.NVarChar);
            komutver.Parameters["@smsSifre"].Value = txtSmsSifre.Text.ToString();
            komutver.Parameters.Add("@smsNo", SqlDbType.NVarChar);
            komutver.Parameters["@smsNo"].Value = txtSistemSmsTelefon.Text.ToString();
            komutver.Parameters.Add("@smsOriginator", SqlDbType.NVarChar);
            komutver.Parameters["@smsOriginator"].Value = txtOriginator.Text.ToString();
            komutver.ExecuteNonQuery();
          
            Response.Redirect("genel_ayarlar.aspx?smsDuzenle=ok");
        }
        catch (Exception ex)
        {
            mesajGosterSis("SMS  güncelleme hatası:", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion
}
