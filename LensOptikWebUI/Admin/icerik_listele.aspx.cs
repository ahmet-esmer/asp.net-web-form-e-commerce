using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelLayer;
using DataAccessLayer;
using BusinessLayer;
using BusinessLayer.BasePage;

public partial class adminIcerikListeleme : BasePageAdmin
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

            if (Request.Params["islem"] == "siraAlt")
            {
                IcerikDB.GenelSiralama(Convert.ToInt32(Request.Params["id"]), "asagi", "icerik");
            }

            if (Request.Params["islem"] == "siraUst")
            {
                IcerikDB.GenelSiralama(Convert.ToInt32(Request.Params["id"]), "yukari", "icerik");
            }

            if (Request.Params["islem"] == "sil")//Silme İşlemi 
            {
                 icerikSil(Convert.ToInt32(Request.Params["id"]));
            }

            if (Request.Params["msg"] == "ok")
            {
                mesajGosterOk("Güncelleme İşlemi Başarı İle Yapıldı.");
            }

            if (Request.Params["islem"] == "duzenle")
            {
                btnIcerikBaslikEkle.Visible = false;
                btbIcerikBaslikGun.Visible = true;
                icerikBaslikBilgiGetir();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "sayfa1", "tabAc()", true);
            }

            if (Request.Params["islem"] == "durum")
            {
                aktifPasif();
            }

            icerikBasliklariListeleme();
     
        }
    }


    #region İçerik Başlıklari Listeleme
    private void icerikBasliklariListeleme()
    {
        try
        {
            GridView1.DataSource = IcerikBaslikDB.listeleme("admin");
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            mesajGosterSis("İçerik Başlık Listelenme Hatası" ,  ex);
        }
    }

    #endregion

    #region İçerik Silme İşlemi
    private void icerikSil(int id)
    {
        try
        {
            ds = new DataSet();
            ds = IcerikDB.SayafaResimAdiListeleSilme(id);

            for (int i = 0; i < Convert.ToInt32(ds.Tables[0].Rows.Count); i++)
            {
                if (File.Exists(resim.SmallImagePath.ToString() + ds.Tables[0].Rows[i]["resim_adi"].ToString()))
                {
                    File.Delete(resim.SmallImagePath.ToString() + ds.Tables[0].Rows[i]["resim_adi"].ToString());
                }

                if (File.Exists(resim.BigImagePath.ToString() + ds.Tables[0].Rows[i]["resim_adi"].ToString()))
                {
                    File.Delete(resim.BigImagePath.ToString() + ds.Tables[0].Rows[i]["resim_adi"].ToString());
                }
            }

            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "icerik_baslikadiSil";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(id);
            komutver.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            mesajGosterSis("İçerik Silme Hatası:" , ex);
        }
        finally
        {
            baglan.Close();
        }
    }

#endregion

    #region İçerik Aktif Pasif Yapma İşlemleri
    private void aktifPasif()
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "icerik_Baslık_Ad_AktifPasif";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(Request.Params["id"]);
            komutver.Parameters.Add("@durum", SqlDbType.Int);
            komutver.Parameters["@durum"].Value = Convert.ToInt32(Request.Params["durum"]);
            komutver.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>Durum Degiştirme Hatası:</b>", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region İçerik Başlık Ekleme İçin DropDouwn Listeleme 
    private void ddlIcerikListeleme()
    {

        SqlParameter parametre = new SqlParameter("@bolge","admin");
        SqlDataReader dr = SqlHelper.ExecuteReader("icerik_baslikadiListele", parametre);


        string serial = null;
        while (dr.Read())
        {
            serial = dr.GetString(dr.GetOrdinal("serial"));
            ddlIcerikler.Items.Insert(0,new ListItem(GenelFonksiyonlar.KategoriCizgiDropdown(serial) + dr.GetString(dr.GetOrdinal("kategoriadi")),serial));
        }

        ListItem item = new ListItem();
        item.Text = "-- Sayfa Başlığı Şeçiniz --";
        item.Value = "0";
        item.Selected = true;
        ddlIcerikler.Items.Add(item);
    }
    #endregion

    #region İçerik Başlık Bilgi Getir
    private void icerikBaslikBilgiGetir()
    {
        try
        {
            IcerikBaslik Icerik = IcerikBaslikDB.getir(Convert.ToInt32(Request.Params["id"]));
            txt_icerkBaslik.Text = Icerik.kategoriadi;
            string anakategoriSerial = Icerik.serial;
            int karekterSay = anakategoriSerial.Length - 3;
            anakategoriSerial = anakategoriSerial.Substring(0, karekterSay);

            ddlIcerikler.ClearSelection();
            ddlIcerikler.SelectedValue = anakategoriSerial;

            if (anakategoriSerial == "")
            {
                ddlIcerikler.SelectedValue = "0";
            }
            else
            {
                ddlIcerikler.SelectedValue = anakategoriSerial;
            }

            ddlGosterimAlani.SelectedValue = Icerik.gosterimAlani;
            ckb_durum.Checked = Icerik.durum;

            txtLink.Text = Icerik.link;

        }
        catch (Exception ex)
        {
            mesajGosterSis("İÇERİK GETİRME HATASI:", ex);
        }
    }
    #endregion

    #region İçerik Başlığı Guncelle
    protected void icerik_Baslik_Guncelle_Click(object sender, EventArgs e)
    {
        string anakategori_serial = null;
        if (string.IsNullOrEmpty(ddlIcerikler.SelectedValue))
        {
            anakategori_serial = "0";
        }
        else
        {
            anakategori_serial = ddlIcerikler.SelectedValue;
        }


        int kayit_sinirla = IcerikBaslikDB.kaydet(anakategori_serial,
            Request.Params["id"],
            txt_icerkBaslik.Text.ToString(),
            Convert.ToBoolean(ckb_durum.Checked),
            ddlGosterimAlani.SelectedValue.ToString(),
            txtLink.Text);

        if (kayit_sinirla > 0)
        {
            mesajGosterNo("Artık alt kategori ekleyemezsiniz!...");
        }
        else
        {
            Response.Redirect("icerik_listele.aspx?msg=ok");
        }
    }
    #endregion

    #region İçerik Başlığı Eklme İşlemi
    protected void btnIcerikBaslikEkle_Click(object sender, EventArgs e)
    {
        try
        {
            string anakategori_serial = "0";
            if (string.IsNullOrEmpty(ddlIcerikler.SelectedValue))
            {
                anakategori_serial = "0";
            }
            else
            {
                anakategori_serial = ddlIcerikler.SelectedValue;
            }


            int kayit_sinirla = IcerikBaslikDB.kaydet(anakategori_serial,
                "0",
                txt_icerkBaslik.Text.ToString(),
                Convert.ToBoolean(ckb_durum.Checked),
                ddlGosterimAlani.SelectedValue.ToString(),
                txtLink.Text);
            
            if (kayit_sinirla < 0)
            {
               mesajGosterNo("Artık alt kategori ekleyemezsiniz!...");
            }
            else
            {
                Response.Redirect("icerik_ekle.aspx?id=" + kayit_sinirla.ToString() + "&islem=icerik_getir&sayfa=yeni");
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>HATA OLUŞTU: </b>" , ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion
}