using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessLayer;
using BusinessLayer.BasePage;

public partial class adminIcerikEkleme :  BasePageAdmin
{
    SqlConnection baglan;
    SqlCommand komutver;
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        baglan = new SqlConnection(ConnectionString.Get);
        if (!IsPostBack)
        {
            ddlIcerikListeleme();

            if (Request.Params["islem"] == "icerik_getir")
            {
                Icerik_Getir(Convert.ToInt32(Request.Params["id"]));
            }
           
            if (Request.Params["sayfa"] == "yeni")
            {
                mesajGosterOk("Sayfa Başlığı Başarı ile Oluştururdu.");

            }
          
            if (Request.Params["msg"] == "ok")
            {
                mesajGosterOk("Sayfa İçerigi Başarı ile Güncellendi.");
            }

            if (Request.Params["msg"] == "no")
            {
                mesajGosterNo("Sayfa Güncelleme Hatası Oluştu!");
            }

            if (Request.Params["resimId"] != null)
            {

                 urunResimSil(Convert.ToString(Request.Params["resimadi"]));
            }
        }
    }

    #region İÇERİK RESİM SİLME İŞLEMİ
    private void urunResimSil(string resimadi)
    {
        try
        {
            if (File.Exists(Server.MapPath("~/Products/Sayfa_Resim/") + resimadi.ToString()))
            {
                File.Delete(Server.MapPath("~/Products/Sayfa_Resim/") + resimadi.ToString());
            }

            IcerikDB.SayafaResimSil(Convert.ToInt32(Request.Params["resimId"]));

            Response.Redirect("icerik_ekle.aspx?id=" + Convert.ToInt32(Request.Params["id"]) + "&islem=icerik_getir");
        }
        catch (Exception ex)
        {
       
            mesajGosterSis("HATA OLUŞTU: ", ex);
        }
    }
    #endregion

    #region BÜTÜN İÇERİK BAŞLIKLARININ LİSTELENMESİ.
    private void ddlIcerikListeleme()
    {
     
        SqlParameter parametre = new SqlParameter("@bolge", "admin");
        SqlDataReader dr = SqlHelper.ExecuteReader("icerik_baslikadiListele", parametre);

        string serial = null;
        while (dr.Read())
        {
            serial = dr.GetString(dr.GetOrdinal("serial"));
            ddlIcerikler.Items.Insert(0, new ListItem(GenelFonksiyonlar.KategoriCizgiDropdown(serial) + dr.GetString(dr.GetOrdinal("kategoriadi")), dr.GetInt32(dr.GetOrdinal("id")).ToString()));
        }

        ListItem item = new ListItem();
        item.Text = "-- Sayfa Başlığı Şeçiniz --";
        item.Value = "0";
        item.Selected = true;
        ddlIcerikler.Items.Add(item);
    }



    protected void ddlIcerikler_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("icerik_ekle.aspx?id=" + ddlIcerikler.SelectedItem.Value + "&islem=icerik_getir");
        //Icerik_Getir(Convert.ToInt32(ddlIcerikler.SelectedItem.Value));
    }
    #endregion

    #region SAYFA İÇERİGİNİN GETİRİLMESİ İŞLEMEİ
    private void Icerik_Getir(int id)
    {
        try
        {
            ds = IcerikDB.GetById(id);

            if (Convert.ToInt32(ds.Tables[0].Rows.Count) >= 1)
            {
                txtSayfaBaslik.Text = ds.Tables[0].Rows[0]["sayfaBaslik"].ToString();
                txtKeyword.Text = ds.Tables[0].Rows[0]["keywords"].ToString();
                txtTitle.Text = ds.Tables[0].Rows[0]["title"].ToString();
                hdf_Sayfa_id.Value = ds.Tables[0].Rows[0]["id"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["description"].ToString();
                ckb_durum.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["durum"]);
                FCKeditor1.Value = ds.Tables[0].Rows[0]["icerik"].ToString();
                string Anasayfa = ds.Tables[0].Rows[0]["kategori_ad"].ToString();
                ddlIcerikler.ClearSelection();


                string kategori_id = ds.Tables[0].Rows[0]["kategori_id"].ToString();
                ddlIcerikler.SelectedValue = kategori_id;
               
                btnIcerikEkle.Visible = false;
                btnIcerikGuncelle.Visible = true;

                int deger = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                ds = new DataSet();

                ds = IcerikDB.SayafaResimAdiListeleAdmin(deger);

                if (Convert.ToInt32(ds.Tables[0].Rows.Count) >= 1)
                {
                    string Resim_Adi = ds.Tables[0].Rows[0]["resim_adi"].ToString();
                    string Resim_id = ds.Tables[0].Rows[0]["id"].ToString();
                    Hdd_Resim_Ad.Value = Resim_Adi.ToString();
                    Hdd_Resim_id.Value = Resim_id.ToString();
                    img_Ana_Resim.Visible = true;
                    img_Ana_Resim.ImageUrl = "../Products/Sayfa_Resim/" + Resim_Adi;
                    hypLinkSil.Visible = true;
                    hypLinkSil.NavigateUrl = "icerik_ekle.aspx?id=" + kategori_id + "&islem=duzenle&resimId=" + Resim_id + "&resimadi=" + Resim_Adi;
                }
            }
            else
            {
                ddlIcerikler.ClearSelection();

                for (int i = 0; i < ddlIcerikler.Items.Count; i++)
                {
                    if (ddlIcerikler.Items[i].Value == Request.Params["id"].ToString())
                    {
                        ddlIcerikler.Items[i].Selected = true;

                    }
                }

                txtDescription.Text = "";
                txtKeyword.Text = "";
                txtTitle.Text = "";
                FCKeditor1.Value = "";
                btnIcerikEkle.Visible = true;
                btnIcerikGuncelle.Visible = false;
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("İçerik Başlık Listelenme Hatası: " , ex);
        }
        finally
        {
            baglan.Close();
        }
    }


    #endregion

    #region  İÇERİK( SAYFA OLUŞTURMA ) EKLEME ALANI
    protected void btnIcerikEkle_Click(object sender, EventArgs e)
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

                txtresim.PostedFile.SaveAs(Server.MapPath("~/Products/Sayfa_Resim/") + yeniDosyaAdi);

            }
        }
        try
        {
            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "icerik_ekle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@kategori_id", SqlDbType.Int);
            komutver.Parameters["@kategori_id"].Value = ddlIcerikler.SelectedItem.Value;
            komutver.Parameters.Add("@sayfaBaslik", SqlDbType.NVarChar);
            komutver.Parameters["@sayfaBaslik"].Value = txtSayfaBaslik.Text.ToString();
            komutver.Parameters.Add("@title", SqlDbType.NVarChar);
            komutver.Parameters["@title"].Value = txtTitle.Text.ToString();
            komutver.Parameters.Add("@description", SqlDbType.NVarChar);
            komutver.Parameters["@description"].Value = txtDescription.Text.ToString();
            komutver.Parameters.Add("@keywords", SqlDbType.NVarChar);
            komutver.Parameters["@keywords"].Value = txtKeyword.Text.ToString();
            komutver.Parameters.Add("@icerik", SqlDbType.NText);
            komutver.Parameters["@icerik"].Value = GenelFonksiyonlar.FjkEditorKarekter(FCKeditor1.Value.ToString());
            komutver.Parameters.Add("@durum", SqlDbType.Bit);
            komutver.Parameters["@durum"].Value = Convert.ToInt32(ckb_durum.Checked);
            komutver.Parameters.Add("@resim_adi", SqlDbType.NVarChar);
            komutver.Parameters["@resim_adi"].Value = yeniDosyaAdi.ToString();
            komutver.Parameters.Add("@deger_dondur", SqlDbType.Int);
            komutver.Parameters["@deger_dondur"].Direction = ParameterDirection.Output;
            komutver.ExecuteNonQuery();
            int donendeger = Convert.ToInt32(komutver.Parameters["@deger_dondur"].Value);

            if (donendeger == 0)
            {
                mesajGosterOk("Sayfa İçerigi Başarı ile Oluştururdu.");
                txtDescription.Text = "";
                txtSayfaBaslik.Text = "";
                txtKeyword.Text = "";
                txtTitle.Text = "";
                FCKeditor1.Value = "";
                ddlIcerikler.SelectedValue = "0";
            }
            else
            {
                mesajGosterNo( "Aynı Sayfa Başlığı  Altında 2  İçerik Oluşturamazsız.");
                if (File.Exists(resim.BigImagePath.ToString() + yeniDosyaAdi.ToString()))
                {
                    File.Delete(resim.BigImagePath.ToString() + yeniDosyaAdi.ToString());
                }
            }

        }
        catch (Exception ex)
        {
            mesajGosterSis("HATA OLUŞTU:" , ex);
            if (File.Exists(Server.MapPath("~/Products/Sayfa_Resim/") + yeniDosyaAdi.ToString()))
            {
                File.Delete(Server.MapPath("~/Products/Sayfa_Resim/") + yeniDosyaAdi.ToString());
            }
        }
        finally
        {
            baglan.Close();
        }
    }

#endregion

    #region İÇERİK GÜNCELLEME ALANI
    protected void btnIcerikGuncelle_Click(object sender, EventArgs e)
    {
        string dosyaAdi = string.Empty;
        string[] dosya;
        string dosyaUzanti = string.Empty;
        string orginalFileName = string.Empty;
        string yeniDosyaAdi = string.Empty;



        if (txtresim.PostedFile.FileName != "" && txtresim.PostedFile.ContentLength > 0 && Hdd_Resim_Ad.Value != "")
        {
            if (txtresim.PostedFile.ContentType != "")
            {
                string hddResimAdi = Hdd_Resim_Ad.Value.ToString();
                int hddResimId = Convert.ToInt32(Hdd_Resim_id.Value);

                if (File.Exists(Server.MapPath("~/Products/Sayfa_Resim/") + hddResimAdi.ToString()))
                {
                    File.Delete(Server.MapPath("~/Products/Sayfa_Resim/") + hddResimAdi.ToString());
                }

                IcerikDB.SayafaResimSil(Convert.ToInt32(hddResimId));

             
                char slash = (char)92;
                orginalFileName = txtresim.PostedFile.FileName.Substring(Convert.ToInt32(txtresim.PostedFile.FileName.LastIndexOf(slash)) + 1);
                dosya = txtresim.PostedFile.FileName.Substring(Convert.ToInt32(txtresim.PostedFile.FileName.LastIndexOf(slash)) + 1).Split('.');

                dosyaAdi = dosya[0].ToString();
                dosyaUzanti = dosya[1].ToString();
                yeniDosyaAdi = resim.dosyaadi(dosyaUzanti.ToString());

                txtresim.PostedFile.SaveAs(Server.MapPath("~/Products/Sayfa_Resim/") + yeniDosyaAdi);

            }

        }

        else if (txtresim.PostedFile.FileName != "" || txtresim.PostedFile.ContentLength > 0)
        {
            if (txtresim.PostedFile.ContentType != "")
            {

                char slash = (char)92;
                orginalFileName = txtresim.PostedFile.FileName.Substring(Convert.ToInt32(txtresim.PostedFile.FileName.LastIndexOf(slash)) + 1);
                dosya = txtresim.PostedFile.FileName.Substring(Convert.ToInt32(txtresim.PostedFile.FileName.LastIndexOf(slash)) + 1).Split('.');

                dosyaAdi = dosya[0].ToString();
                dosyaUzanti = dosya[1].ToString();
                yeniDosyaAdi = resim.dosyaadi(dosyaUzanti.ToString());

                txtresim.PostedFile.SaveAs(Server.MapPath("~/Products/Sayfa_Resim/") + yeniDosyaAdi);

            }

        }

        try
        {
            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "icerik_guncelle";
            komutver.CommandType = CommandType.StoredProcedure;

            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = hdf_Sayfa_id.Value;
            komutver.Parameters.Add("@sayfaBaslik", SqlDbType.NVarChar);
            komutver.Parameters["@sayfaBaslik"].Value = txtSayfaBaslik.Text.ToString();
            komutver.Parameters.Add("@title", SqlDbType.NVarChar);
            komutver.Parameters["@title"].Value = txtTitle.Text.ToString();
            komutver.Parameters.Add("@description", SqlDbType.NVarChar);
            komutver.Parameters["@description"].Value = txtDescription.Text.ToString();
            komutver.Parameters.Add("@keywords", SqlDbType.NVarChar);
            komutver.Parameters["@keywords"].Value = txtKeyword.Text.ToString();
            komutver.Parameters.Add("@icerik", SqlDbType.NText);
            komutver.Parameters["@icerik"].Value = GenelFonksiyonlar.FjkEditorKarekter(FCKeditor1.Value.ToString());
            komutver.Parameters.Add("@durum", SqlDbType.Bit);
            komutver.Parameters["@durum"].Value = Convert.ToInt32(ckb_durum.Checked);

            komutver.Parameters.Add("@resim_adi", SqlDbType.NVarChar);
            komutver.Parameters["@resim_adi"].Value = yeniDosyaAdi.ToString();

            komutver.Parameters.Add("@deger_dondur", SqlDbType.Int);
            komutver.Parameters["@deger_dondur"].Direction = ParameterDirection.Output;
            komutver.ExecuteNonQuery();
            int degerdondur = Convert.ToInt32(komutver.Parameters["@deger_dondur"].Value);

            if (degerdondur == 0)
            {
                Response.Redirect("icerik_ekle.aspx?id=" + ddlIcerikler.SelectedItem.Value + "&islem=icerik_getir&msg=ok");
            }
            else
            {
                Response.Redirect("icerik_ekle.aspx?id=" + ddlIcerikler.SelectedItem.Value + "&islem=icerik_getir&msg=no");
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("GÜNCELLEME HATASI:" , ex);
        }
        finally
        {
            baglan.Close();
        }
    }
#endregion

}




