using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using BusinessLayer;
using DataAccessLayer;
using BusinessLayer.BasePage;

public partial class DuyuruEkle : BasePageAdmin
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
            img_Ana_Resim.Visible = false;

            if (Request.Params["islem"] == "duzenle")
            {
                lblSayfaBaslik.Text = "Duyuru Düzenle";
                DuyuruBilgiGetir();
                Button1.Visible = false;
                btnDuyuruGuncelle.Visible = true;
                img_Ana_Resim.Visible = true;
            }
        }
    }

    #region Duyuru Ekleme İşlemi
    protected void Button1_Click(object sender, EventArgs e)
    {
        string dosyaAdi = string.Empty;
        string[] dosya;
        string dosyaUzanti = string.Empty;
        string orginalFileName = string.Empty;
        string yeniDosyaAdi = string.Empty;

        if (txtresim.PostedFile.FileName != "" || txtresim.PostedFile.ContentLength > 0)
        {
            if (txtresim.PostedFile.ContentType != "")
            {
                char slash = (char)92;
                orginalFileName = txtresim.PostedFile.FileName.Substring(Convert.ToInt32(txtresim.PostedFile.FileName.LastIndexOf(slash)) + 1);
                dosya = txtresim.PostedFile.FileName.Substring(Convert.ToInt32(txtresim.PostedFile.FileName.LastIndexOf(slash)) + 1).Split('.');

                dosyaAdi = dosya[0].ToString();
                dosyaUzanti = dosya[1].ToString();
                yeniDosyaAdi = resim.dosyaadi(dosyaUzanti.ToString());
                txtresim.PostedFile.SaveAs(resim.imagePath.ToString() + orginalFileName);
                resim.resimThumb(orginalFileName, yeniDosyaAdi, 690, 170, resim.BigImagePath.ToString(), "b");
            }
        }


        baglan.Open();
        komutver = new SqlCommand();
        komutver.Connection = baglan;
        komutver.CommandText = "duyuru_Ekle";
        komutver.CommandType = CommandType.StoredProcedure;
        komutver.Parameters.Add("@duyuru_adi", SqlDbType.NVarChar);
        komutver.Parameters["@duyuru_adi"].Value = txt_duyurBaslik.Text.ToString();
        komutver.Parameters.Add("@durum", SqlDbType.Bit);
        komutver.Parameters["@durum"].Value = ckb_durum.Checked.ToString();
        komutver.Parameters.Add("@duyuru_icerik", SqlDbType.NVarChar);
        komutver.Parameters["@duyuru_icerik"].Value = fck_duyuruIcerik.Value.ToString();
        komutver.Parameters.Add("@resim_adi", SqlDbType.NVarChar);
        komutver.Parameters["@resim_adi"].Value = yeniDosyaAdi.ToString();
        komutver.Parameters.Add("@deger_dondur", SqlDbType.Int);
        komutver.Parameters["@deger_dondur"].Direction = ParameterDirection.Output;
        komutver.ExecuteNonQuery();


        int donenDeger = Convert.ToInt32(komutver.Parameters["@deger_dondur"].Value);
        if (donenDeger == 0)
        {
            txt_duyurBaslik.Text = "";
            fck_duyuruIcerik.Value = "";
      
            mesajGosterOk("Duyuru Kaydı Başarı ile Yapıldı");
        }
        else
        {
            if (File.Exists(resim.BigImagePath.ToString() + yeniDosyaAdi.ToString()))
            {
                File.Delete(resim.BigImagePath.ToString() + yeniDosyaAdi.ToString());
            }
            mesajGosterNo("Bu Dyuru Kaydı Mevcuttur");
        }

        baglan.Close();
    }
    #endregion

    #region Güncelleme İşlemi İçin Duyuru Bilgi Getir
    private void DuyuruBilgiGetir()
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "duyuru_Getir";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(Request.Params["id"]);
            komutver.Parameters.Add("@paremetre", SqlDbType.NVarChar);
            komutver.Parameters["@paremetre"].Value = "admin";


            adp = new SqlDataAdapter(komutver);
            ds = new DataSet();
            adp.Fill(ds);

            txt_duyurBaslik.Text = ds.Tables[0].Rows[0]["duyuru_adi"].ToString();
            fck_duyuruIcerik.Value = ds.Tables[0].Rows[0]["duyuru_icerik"].ToString();
            ckb_durum.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["durum"]);
           


            int deger = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
            ds = new DataSet();

            ds = DuyuruDB.ResimAdiListele(deger);

            if (Convert.ToInt32(ds.Tables[0].Rows.Count) >= 1)
            {
                string Resim_Adi = ds.Tables[0].Rows[0]["resim_adi"].ToString();
                Hdd_Resim_Ad.Value = Resim_Adi.ToString();
                Hdd_Resim_id.Value = ds.Tables[0].Rows[0]["id"].ToString();
                img_Ana_Resim.ImageUrl = "../Products/Big/" + Resim_Adi;
            }

            baglan.Close();

        }
        catch (Exception ex)
        {
            mesajGosterSis("Duyuru Listeleme Hatası:", ex);
        }
    }

    #endregion

    #region Duyuru Güncelleme İşlemi
    protected void btnDuyuruGuncelle_Click(object sender, EventArgs e)
    {
        string dosyaAdi = string.Empty;
        string[] dosya;
        string dosyaUzanti = string.Empty;
        string orginalFileName = string.Empty;
        string yeniDosyaAdi = string.Empty;

        if (txtresim.PostedFile.FileName != "" || txtresim.PostedFile.ContentLength > 0)
        {
            if (txtresim.PostedFile.ContentType != "")
            {

                if (Hdd_Resim_Ad.Value != "")
                {
                    string hddResimAdi = Hdd_Resim_Ad.Value.ToString();
                    int hddResimId = Convert.ToInt32(Hdd_Resim_id.Value);

                    if (File.Exists(resim.BigImagePath.ToString() + hddResimAdi.ToString()))
                    {
                        File.Delete(resim.BigImagePath.ToString() + hddResimAdi.ToString());
                    }
                }


                char slash = (char)92;
                orginalFileName = txtresim.PostedFile.FileName.Substring(Convert.ToInt32(txtresim.PostedFile.FileName.LastIndexOf(slash)) + 1);
                dosya = txtresim.PostedFile.FileName.Substring(Convert.ToInt32(txtresim.PostedFile.FileName.LastIndexOf(slash)) + 1).Split('.');
                dosyaAdi = dosya[0].ToString();
                dosyaUzanti = dosya[1].ToString();
                yeniDosyaAdi = resim.dosyaadi(dosyaUzanti.ToString());
                txtresim.PostedFile.SaveAs(resim.imagePath.ToString() + orginalFileName);
                resim.resimThumb(orginalFileName, yeniDosyaAdi, 200, 150, resim.BigImagePath.ToString(), "b");
            }
        }


        baglan.Open();
        komutver = new SqlCommand();
        komutver.Connection = baglan;
        komutver.CommandText = "duyuru_Guncelle";
        komutver.CommandType = CommandType.StoredProcedure;
        komutver.Parameters.Add("@id", SqlDbType.Int);
        komutver.Parameters["@id"].Value = Convert.ToInt32(Request.Params["id"]);
        komutver.Parameters.Add("@duyuru_adi", SqlDbType.NVarChar);
        komutver.Parameters["@duyuru_adi"].Value = txt_duyurBaslik.Text.ToString();
        komutver.Parameters.Add("@durum", SqlDbType.Bit);
        komutver.Parameters["@durum"].Value = ckb_durum.Checked.ToString();
        komutver.Parameters.Add("@duyuru_icerik", SqlDbType.NVarChar);
        komutver.Parameters["@duyuru_icerik"].Value = fck_duyuruIcerik.Value.ToString();
        komutver.Parameters.Add("@resim_adi", SqlDbType.NVarChar);
        komutver.Parameters["@resim_adi"].Value = yeniDosyaAdi.ToString();
        komutver.Parameters.Add("@deger_dondur", SqlDbType.Int);
        komutver.Parameters["@deger_dondur"].Direction = ParameterDirection.Output;
        komutver.ExecuteNonQuery();
        baglan.Close();

        int donenDeger = Convert.ToInt32(komutver.Parameters["@deger_dondur"].Value);
        if (donenDeger == 0)
        {
           
            mesajGosterOk("Duyuru Güncelleme İşlemi ile Yapıldı");
        }
    }
    #endregion
}
