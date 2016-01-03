using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using BusinessLayer;
using DataAccessLayer;
using ModelLayer;
using BusinessLayer.BasePage;


public partial class admin_Paneller : BasePageAdmin
{
    SqlConnection baglan;
    SqlCommand komutver;

    protected void Page_Load(object sender, EventArgs e)
    {
     
        baglan = new SqlConnection(ConnectionString.Get);

        if (!IsPostBack)
        {
            if (Request.Params["resimId"] != null)
            {
                banner_Icerik_ResimSil(Request.Params["resimadi"]);
            }

            if (Request.Params["islem"] == "durum")
            {
                aktifPasif();
            }

            if (Request.Params["islem"] == "duzenle")
            {
                btnBannerEkle.Visible = false;
                btnBannerGuncelle.Visible = true;
                bannerBilgiGetir();
            }

            BannerListele();
        }
    }

    #region Günelleme İçin Banner Bilgisi Getirme
    private void bannerBilgiGetir()
    {
        //try
        //{
        //    string baglantiCumle = ConfigurationManager.ConnectionStrings["SQLBaglanti"].ConnectionString;
        //    SqlConnection baglan = new SqlConnection(baglantiCumle);
        //    komutver = new SqlCommand();
        //    baglan.Open();
        //    komutver.Connection = baglan;
        //    komutver.CommandText = "resim_Bilgi_Getir";
        //    komutver.CommandType = CommandType.StoredProcedure;
        //    komutver.Parameters.Add("@id", SqlDbType.Int);
        //    komutver.Parameters["@id"].Value = Convert.ToInt32(Request.Params["id"]);
        //    komutver.Parameters.Add("@parametre", SqlDbType.NVarChar);
        //    komutver.Parameters["@parametre"].Value = "reklam";


        //    adp = new SqlDataAdapter(komutver);
        //    ds = new DataSet();
        //    adp.Fill(ds);

        //    txtBannerBaslik.Text = ds.Tables[0].Rows[0]["resim_baslik"].ToString();
        //    ckb_ReklamDurum.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["durum"]);

        //    string Resim_Adi = ds.Tables[0].Rows[0]["resim_adi"].ToString();
        //    Hdd_Resim_Ad.Value = Resim_Adi.ToString();
        //    Hdd_Resim_id.Value = ds.Tables[0].Rows[0]["id"].ToString();
        //    //img_Ana_Resim.Visible = true;
        //    //img_Ana_Resim.ImageUrl = "../Products/Big/" + Resim_Adi;

        //    dpl_ResimNo.ClearSelection();
        //    for (int i = 50; i >= 1; i--)
        //    {
        //        if (dpl_ResimNo.Items[i].Value == ds.Tables[0].Rows[0]["sira"].ToString())
        //        {
        //            dpl_ResimNo.Items[i].Selected = true;
        //        }
        //    }


        //    baglan.Close();

        //}
        //catch (Exception hata)
        //{
        //   mesajGosterSis("Banner Bilgi Getirme Hatası:" , hata.ToString());
        //}

    }
    #endregion

    #region Banner Aktif veya Pasif Yapmak
    private void aktifPasif()
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "reklam_Durum";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(Request.Params["id"]);
            komutver.Parameters.Add("@durum", SqlDbType.Int);
            komutver.Parameters["@durum"].Value = Convert.ToInt32(Request.Params["durum"]);
            komutver.ExecuteNonQuery();


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

    #region Banner İçerigi ve Resmini Silme İşlemi
    private void banner_Icerik_ResimSil(string resimadi)
    {
        try
        {

            if (File.Exists(Server.MapPath("~/Products/Flash/") + resimadi.ToString()))
            {
                File.Delete(Server.MapPath("~/Products/Flash/") + resimadi.ToString());
            }
          
            ResimDB.ResimSil(Convert.ToInt32(Request.Params["resimId"]));
            mesajGosterOk("Reklam Silme İşlemi Yapıldı.");

        }
        catch (Exception ex)
        {
            mesajGosterSis("Banner Silme Hatası" , ex);
        }
    }
    #endregion


    #region  Banner Listeleme   
    private void BannerListele()
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@paremetre", "flash");

            using (SqlDataReader dr = SqlHelper.ExecuteReader("resim_Listele", parametre))
            {
                List<Pannel> bannerTablo = new List<Pannel>();
                string gosterimAd = null;
                while (dr.Read())
                {
                    gosterimAd = dr.GetString(dr.GetOrdinal("parametre"));

                    switch (gosterimAd)
                    {
                        case "solPanel": gosterimAd = "Sol Panel Flash "; break;
                        case "popUp": gosterimAd = "Anasayfa Pop Up "; break;
                        case "sagKargo": gosterimAd = "Sağ Kargo Alanı"; break;
                        case "sagAlt": gosterimAd = "Sağ Alt Resim Alanı"; break;
                        case "sagFlash": gosterimAd = "Sağ Panel Flash"; break;
                        case "hediye": gosterimAd = "Hediye Kapanya Resim"; break;
                    }

                    Pannel info = new Pannel(
                        dr.GetString(dr.GetOrdinal("resim_adi")),
                        dr.GetInt32(dr.GetOrdinal("id")),
                        dr.GetString(dr.GetOrdinal("resim_baslik")),
                        dr.GetBoolean(dr.GetOrdinal("durum")),
                        gosterimAd);

                    bannerTablo.Add(info);
                }

                gvwPaneller.DataSource = bannerTablo;
                gvwPaneller.DataBind();
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Banner Listeleme Hatası", ex);
        }
    }
 #endregion


    //private void dpl_Resim_No_Listeleme()
    //{
    //    dpl_ResimNo.ClearSelection();
    //    ListItem item = new ListItem();
    //    item.Text = "-- banner Gösterim Sırası --";
    //    item.Value = "0";
    //    item.Selected = true;
    //    dpl_ResimNo.Items.Add(item);

    //    for (int i = 50; i >= 1; i--)
    //    {
    //        dpl_ResimNo.Items.Insert(1, new ListItem(i.ToString(), i.ToString()));
    //    }
    //}


    #region Banner Yeni Bilgi Ekleme 
    protected void btnBannerEkle_Click(object sender, EventArgs e)
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

                txtresim.PostedFile.SaveAs(Server.MapPath("~/Products/Flash/") + yeniDosyaAdi);

            }
        }


        baglan.Open();
        komutver = new SqlCommand();
        komutver.Connection = baglan;
        komutver.CommandText = "ResimEkle";
        komutver.CommandType = CommandType.StoredProcedure;
        komutver.Parameters.Add("@parametre", SqlDbType.NVarChar);
        komutver.Parameters["@parametre"].Value = ddlFlashGosterimAlani.SelectedItem.Value;
        komutver.Parameters.Add("@resim_adi", SqlDbType.NVarChar);
        komutver.Parameters["@resim_adi"].Value = yeniDosyaAdi.ToString();
        komutver.Parameters.Add("@resim_baslik", SqlDbType.NVarChar);
        komutver.Parameters["@resim_baslik"].Value = txtBannerBaslik.Text.ToString();
        komutver.Parameters.Add("@sira", SqlDbType.NVarChar);
        komutver.Parameters["@sira"].Value = "1";
        komutver.Parameters.Add("@durum", SqlDbType.Bit);
        komutver.Parameters["@durum"].Value = Convert.ToInt32(ckb_ReklamDurum.Checked);

        komutver.ExecuteNonQuery();
        baglan.Close();

        mesajGosterOk("banner İçerigi Başarı ile Oluştururdu.");

        txtBannerBaslik.Text = "";

        BannerListele();
    }
    #endregion

    #region Banner Güncelleme İşlemi
    protected void btnBannerGuncelle_Click(object sender, EventArgs e)
    {
        //string dosyaAdi = string.Empty;
        //string[] dosya;
        //string dosyaUzanti = string.Empty;
        //string orginalFileName = string.Empty;
        //string yeniDosyaAdi = Hdd_Resim_Ad.Value.ToString();
        //string hddResimGunAdi = Hdd_Resim_Ad.Value.ToString();
        //int hddResimGunId = Convert.ToInt32(Hdd_Resim_id.Value);

        //try
        //{
        //    if (txtresim.PostedFile.FileName != "" && txtresim.PostedFile.ContentLength > 0)
        //    {

        //        if (File.Exists("~/Products/Flash/" + hddResimGunAdi.ToString()))
        //        {
        //            File.Delete("~/Products/Flash/" + hddResimGunAdi.ToString());
        //        }

        //        if (txtresim.PostedFile.ContentType != "")
        //        {

        //            char slash = (char)92;
        //            orginalFileName = txtresim.PostedFile.FileName.Substring(Convert.ToInt32(txtresim.PostedFile.FileName.LastIndexOf(slash)) + 1);
        //            dosya = txtresim.PostedFile.FileName.Substring(Convert.ToInt32(txtresim.PostedFile.FileName.LastIndexOf(slash)) + 1).Split('.');

        //            dosyaAdi = dosya[0].ToString();
        //            dosyaUzanti = dosya[1].ToString();
        //            yeniDosyaAdi = resim.dosyaadi(dosyaUzanti.ToString());
        //            txtresim.PostedFile.SaveAs(resim.imagePath.ToString() + orginalFileName);

        //            txtresim.PostedFile.SaveAs(Server.MapPath("~/Products/Flash/") + yeniDosyaAdi);
        //        }
        //    }

        //    string baglantiCumle = ConfigurationManager.ConnectionStrings["SQLBaglanti"].ConnectionString;
        //    SqlConnection baglan = new SqlConnection(baglantiCumle);

        //    baglan.Open();
        //    komutver = new SqlCommand();
        //    komutver.Connection = baglan;
        //    komutver.CommandText = "reklam_Guncelle";
        //    komutver.CommandType = CommandType.StoredProcedure;
        //    komutver.Parameters.Add("@id", SqlDbType.Int);
        //    komutver.Parameters["@id"].Value = Convert.ToInt32(hddResimGunId);
        //    komutver.Parameters.Add("@resim_adi", SqlDbType.NVarChar);
        //    komutver.Parameters["@resim_adi"].Value = yeniDosyaAdi.ToString();
        //    komutver.Parameters.Add("@resim_baslik", SqlDbType.NVarChar);
        //    komutver.Parameters["@resim_baslik"].Value = txtBannerBaslik.Text.ToString();
        //    komutver.Parameters.Add("@sira", SqlDbType.Int);
        //    komutver.Parameters["@sira"].Value = Convert.ToInt32(dpl_ResimNo.SelectedItem.Value);
        //    komutver.Parameters.Add("@deger_dondur", SqlDbType.Int);
        //    komutver.Parameters["@deger_dondur"].Direction = ParameterDirection.Output;
        //    komutver.ExecuteNonQuery();

        //    int donenDeger = Convert.ToInt32(komutver.Parameters["@deger_dondur"].Value);


        //    if (donenDeger == 0)
        //    {
        //        mesajGosterOk("Banner İçerigi Başarı Güncellendi.");
        //        masterPageBaslik("Bannerlar");
        //        pnl_Banner.Visible = false;
        //        btnYeni_Banner_Ekle.Visible = true;

        //        bannerListele();
        //    }

        //}
        //catch (Exception hata)
        //{
        //    if (File.Exists("~/Products/Flash/" + yeniDosyaAdi.ToString()))
        //    {
        //        File.Delete("~/Products/Flash/" + yeniDosyaAdi.ToString());
        //    }
        //    mesajGosterSis("Güncelleme Hatası" , hata.ToString());
        //}
        //finally
        //{
        //    baglan.Close();
        //}
    }
    #endregion

}