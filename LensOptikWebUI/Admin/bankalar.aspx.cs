using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessLayer;
using BusinessLayer.BasePage;

public partial class AdminBankalar : BasePageAdmin
{
    SqlConnection baglan;
    SqlCommand komutver;

    protected void Page_Load(object sender, EventArgs e)
    {
        baglan = new SqlConnection(ConnectionString.Get);

        if (!IsPostBack)
        {
            BankaListele();
       
            if (Request.Params["islem"] == "durum")
            {
                aktifPasif();
            }

            if (Request.Params["islem"] == "sil")//Silme İşlemi 
            {
                KayitSil(Convert.ToInt32(Request.Params["id"]));

                string resimAdi = Request.Params["resim"].ToString();

                if (File.Exists(Server.MapPath("~/Products/Sayfa_Resim/") + resimAdi.ToString()))
                {
                    File.Delete(Server.MapPath("~/Products/Sayfa_Resim/") + resimAdi.ToString());
                }
            }

            if (Request.Params["taksit"] == "sil")//Silme İşlemi 
            {
                TaksitSil(Convert.ToInt32(Request.Params["id"]));
                Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString());
            }


            if (Request.Params["islem"] == "duzenle")
            {
                pnlBankaEkle.Visible = false;
                pnlBankaDuzenle.Visible = true;
          
                string bankaId = Request.Params["id"];
                string resim = Request.Params["resim"].ToString();

                hdResimAdi.Value = resim.ToString();
                hdfBankaId.Value = bankaId.ToString();

                txtBankaDuzenle.Text = Request.Params["ad"].ToString();
                imgBankaBaslik.ImageUrl = "~/Products/Sayfa_Resim/"+ resim;

                TaksitListele(Convert.ToInt32(bankaId));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "tabAcPos1", "tabAc()", true);
        
            }

            if (Request.Params["islem"] == "sanalpos")
            {
                lblBankaAdi.Text = Request.Params["ad"].ToString();
                SanalposListele(Convert.ToInt32(Request.Params["id"]));
                ModalPopupExtender.Show();
            } 
        }
    }


    #region Banka Kayıt Listele
    private void BankaListele()
    {
        try
        {
            GridView1.DataSource = BankaDB.Liste("admin");
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Listeleme Hatası", ex);
        }
    }
    #endregion

    #region Banka Kayıt Silme İşlemi
    private void KayitSil(int id)
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "banka_KayitSil";
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

    #region Banka Adi ve Başlık Resmi Güncelleme İşlemi
     protected void btnBankaGuncelle_Click(object sender, EventArgs e)
    {
        try
        {
            string dosyaAdi = string.Empty;
            string[] dosya;
            string dosyaUzanti = string.Empty;
            string orginalFileName = string.Empty;
            string yeniDosyaAdi = string.Empty;

            if (txtResimDuz.PostedFile.FileName != "" || txtResimDuz.PostedFile.ContentLength > 0)
            {
                if (txtResimDuz.PostedFile.ContentType != "")
                {
                    char slash = (char)92;
                    orginalFileName = txtResimDuz.PostedFile.FileName.Substring(Convert.ToInt32(txtResimDuz.PostedFile.FileName.LastIndexOf(slash)) + 1);
                    dosya = txtResimDuz.PostedFile.FileName.Substring(Convert.ToInt32(txtResimDuz.PostedFile.FileName.LastIndexOf(slash)) + 1).Split('.');

                    dosyaAdi = dosya[0].ToString();
                    dosyaUzanti = dosya[1].ToString();
                    yeniDosyaAdi = resim.dosyaadi(dosyaUzanti.ToString());

                    txtResimDuz.PostedFile.SaveAs(Server.MapPath("~/Products/Sayfa_Resim/") + yeniDosyaAdi);


                    if (File.Exists(Server.MapPath("~/Products/Sayfa_Resim/") + hdResimAdi.Value.ToString()))
                    {
                        File.Delete(Server.MapPath("~/Products/Sayfa_Resim/") + hdResimAdi.Value.ToString());
                    }
                } 
            }
            else 
            {
                yeniDosyaAdi = hdResimAdi.Value.ToString();
            }


            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "banka_AdiGuncelle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(hdfBankaId.Value);
            komutver.Parameters.Add("@bankaAdi", SqlDbType.NVarChar);
            komutver.Parameters["@bankaAdi"].Value = txtBankaDuzenle.Text.Trim();
            komutver.Parameters.Add("@baslikResmi", SqlDbType.NVarChar);
            komutver.Parameters["@baslikResmi"].Value = yeniDosyaAdi.ToString();
            komutver.ExecuteNonQuery();

            imgBankaBaslik.ImageUrl = "~/Products/Sayfa_Resim/" + yeniDosyaAdi.ToString();
            hdResimAdi.Value = yeniDosyaAdi.ToString();
            BankaListele();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "tabAcPos2", "tabAc()", true);
        }
        catch (Exception ex)
        {
            mesajGosterSis("Banka Güncelleme  Hatası:", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region Banka Adı Ekle
    protected void btnBankaAdiEkle_Click(object sender, EventArgs e)
    {
        try
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
            

            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "banka_KayitEkle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@bankaAdi", SqlDbType.NVarChar);
            komutver.Parameters["@bankaAdi"].Value = txtAnketAdi.Text.Trim();
            komutver.Parameters.Add("@baslikResmi", SqlDbType.NVarChar);
            komutver.Parameters["@baslikResmi"].Value = yeniDosyaAdi.ToString();
            komutver.Parameters.Add("@deger_dondur", SqlDbType.Int);
            komutver.Parameters["@deger_dondur"].Direction = ParameterDirection.Output;
            komutver.ExecuteNonQuery();


            int donenDeger = Convert.ToInt32(komutver.Parameters["@deger_dondur"].Value);
            if (donenDeger == -1)
            {
                mesajGosterNo("Bu Banka Adında Bir Kayıt Mevcut.");
            }
            else
            {
                pnlBankaEkle.Visible = false;
                pnlBankaDuzenle.Visible = true;
                txtBankaDuzenle.Text = txtAnketAdi.Text.ToString();
                hdfBankaId.Value = donenDeger.ToString();
                hdResimAdi.Value = yeniDosyaAdi.ToString();
                imgBankaBaslik.ImageUrl = "~/Products/Sayfa_Resim/"+ yeniDosyaAdi.ToString();

                BankaListele();

                mesajGosterInfo("<span class='satirYukseklik'>1. Banka Adı Başarı ile Oluşturuldu. <br/> 2. Banka İçin Taksitleri Oluşturunuz.</br>3. Sanalpos Ayarlarını Oluşturunuz.</span> ");
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "tabAcPos2", "tabAc()", true);
        
            }

        }
        catch (Exception ex)
        {
            mesajGosterSis("Banka Ekleme   Hatası:", ex);
        }
        finally
        {
            baglan.Close();
        }

    }
    #endregion

    #region Banka  Durum İşlemi
    private void aktifPasif()
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "banka_Durum";
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


    // Banka Taksit  İşlemleri 

    #region Banka Taksit Ekleme İşlemi
    protected void btnTaksitEkle_Click(object sender, EventArgs e)
    {
        try
        {
            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "bankaTaksit_Ekle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@bankaId", SqlDbType.Int);
            komutver.Parameters["@bankaId"].Value = Convert.ToInt32(hdfBankaId.Value);
            komutver.Parameters.Add("@taksit", SqlDbType.Int);
            komutver.Parameters["@taksit"].Value = Convert.ToInt32( txtTaksit.Text);
            komutver.Parameters.Add("@vadeFarki", SqlDbType.Float);
            komutver.Parameters["@vadeFarki"].Value = txtVadeFarki.Text;
            komutver.ExecuteNonQuery();


            ((HtmlGenericControl)Master.FindControl("Mesaj_Info")).Visible = false;

            TaksitListele(Convert.ToInt32(hdfBankaId.Value));
            txtTaksit.Text = "";
            txtVadeFarki.Text = "";
        }
        catch (Exception ex)
        {
            mesajGosterSis("Taksit Ekleme Hatası:", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region  Taksit Güncelleme İşlemi
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "taksitKaydet")
        {
            string index = e.CommandArgument.ToString();
 
            int taksitId =0;
            string taksit = null;
            string taksitVade = null;

            foreach( GridViewRow item in GridView2.Rows)
            {
                if(item.RowType == DataControlRowType.DataRow)
                {
                    if (GridView2.DataKeys[item.RowIndex].Value.ToString() == index.ToString())
                    {
                        taksitId = Convert.ToInt32(e.CommandArgument);
                        taksit = (item.FindControl("txtTaksitDuzenle") as TextBox).Text;
                        taksitVade = (item.FindControl("txtVadeFarki") as TextBox).Text;
                    }
                }
            }



            if(taksitId != 0 && taksit != null && taksitVade != null)
            {
                try
                {
                    baglan.Open();
                    komutver = new SqlCommand();
                    komutver.Connection = baglan;
                    komutver.CommandText = "bankaTaksit_Guncelle";
                    komutver.CommandType = CommandType.StoredProcedure;
                    komutver.Parameters.Add("@id", SqlDbType.Int);
                    komutver.Parameters["@id"].Value = Convert.ToInt32(taksitId);
                    komutver.Parameters.Add("@taksit", SqlDbType.NVarChar);
                    komutver.Parameters["@taksit"].Value = taksit.ToString();
                    komutver.Parameters.Add("@vadeFarki", SqlDbType.Float);
                    komutver.Parameters["@vadeFarki"].Value = Convert.ToDouble(taksitVade);
                    komutver.ExecuteNonQuery();
                    BankaListele();
            
                }
                catch (Exception ex)
                {
                    mesajGosterSis("Taksit Düzenleme Hatası:", ex);
                }
                finally
                {
                    baglan.Close();
                }
               
            }

        }

    }
    #endregion

    #region Taksit Silme İşlemi
    private void TaksitSil(int id)
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "bankaTaksit_Sil";
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

    #region Taksit  Kayıt Listele
    private void TaksitListele(int id)
    {
        try
        {
            SqlParameter parametre = new SqlParameter();
            parametre.ParameterName = "@id";
            parametre.Value = Convert.ToInt32(id);

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "bankaTaksit_Getir", parametre);
        

            GridView2.DataSource = ds;
            GridView2.DataBind();          
        }
        catch (Exception ex)
        {
            mesajGosterSis("Listeleme Hatası : ", ex);
        }
       
    }
    #endregion

    #region Banka Gösterim Sırası
    protected void bankaSira(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;

        int bankaId = Convert.ToInt32(txt.ToolTip);
        int sira = Convert.ToInt32(txt.Text);

        if (!(bankaId == 0 && sira == 0))
        {
            try
            {
                SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@id",  bankaId),
                     new SqlParameter("@sira", sira),
                };

                SqlHelper.ExecuteNonQuery("banka_KayitSiraGuncelle", parametre);

                BankaListele();


            }
            catch (Exception ex)
            {
                mesajGosterSis(" Sıralama Hatası", ex);
            }
        }
    }
    #endregion

    #region Sanalpos Listele
    private void SanalposListele(int id)
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@id",id);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "bankaSanalPos_Getir", parametre);

            string bankakod = ds.Tables[0].Rows[0]["bankaKod"].ToString();

            if (!string.IsNullOrWhiteSpace(bankakod))
            {
                txtBankaKod.Enabled = false; 
            }

            txtBankaKod.Text = bankakod;
            txtMagaza.Text =     ds.Tables[0].Rows[0]["magazaAdi"].ToString();
            txtApiKulanici.Text = ds.Tables[0].Rows[0]["apiKullanici"].ToString();
            txtApiSifre.Text = ds.Tables[0].Rows[0]["apiSifre"].ToString();
            txtHedfSunucu.Text = ds.Tables[0].Rows[0]["sunucu"].ToString();

            ddlSanalposTipi.ClearSelection();

            string stokCinsi = ds.Tables[0].Rows[0]["tip"].ToString();

            for (int i = 0; i < ddlSanalposTipi.Items.Count; i++)
            {
                if (ddlSanalposTipi.Items[i].Value == stokCinsi.ToString())
                {
                    ddlSanalposTipi.Items[i].Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Listeleme Hatası : ", ex);
        }
    }
    #endregion

    #region Sanalpos Düzenle
    protected void btnSanalPos_Click(object sender, EventArgs e)
    {
        try
        {
            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "bankaSanalpos_Guncelle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(Request.Params["id"]);
            komutver.Parameters.Add("@magazaAdi", SqlDbType.NVarChar);
            komutver.Parameters["@magazaAdi"].Value = txtMagaza.Text.ToString();
            komutver.Parameters.Add("@apiKullanici", SqlDbType.NVarChar);
            komutver.Parameters["@apiKullanici"].Value =  txtApiKulanici.Text.ToString();
            komutver.Parameters.Add("@apiSifre", SqlDbType.NVarChar);
            komutver.Parameters["@apiSifre"].Value = txtApiSifre.Text.ToString();
            komutver.Parameters.Add("@tip", SqlDbType.NVarChar);
            komutver.Parameters["@tip"].Value = ddlSanalposTipi.SelectedValue.ToString();
            komutver.Parameters.Add("@bankaKod", SqlDbType.NVarChar);
            komutver.Parameters["@bankaKod"].Value = txtBankaKod.Text;
            komutver.Parameters.Add("@sunucu", SqlDbType.NVarChar);
            komutver.Parameters["@sunucu"].Value = txtHedfSunucu.Text.ToString();
            komutver.ExecuteNonQuery();

            mesajGizlenfo();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Sanalpos Düzenleme Hatası:", ex);
        }
        finally
        {
            baglan.Close();
        }

    }
    #endregion

    #region varsayılan banka  düzenle
    protected void dlVarsayilanBanka(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        Boolean varsayilanBanka = Convert.ToBoolean(list.SelectedValue);
        int bankaId = Convert.ToInt32(list.ToolTip);

        if (!(bankaId == 0))
        {
            try
            {
                SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("bankaId",  bankaId),
                     new SqlParameter("@varsayilanBanka", varsayilanBanka),
                };

                SqlHelper.ExecuteNonQuery("banka_KayitVarsayilan", parametre);

                BankaListele();
            }
            catch (Exception ex)
            {
                mesajGosterSis("Varsayılan Banka düzenlenirken hata oluştu.", ex);
            }
        }
    }
    #endregion
   
}
