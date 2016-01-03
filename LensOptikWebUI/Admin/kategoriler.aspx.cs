using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using ImageLibrary;
using ModelLayer;
using BusinessLayer;
using BusinessLayer.BasePage;

public partial class AdminKategoriler : BasePageAdmin
{
    private int kategoriId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            kategoriListele();
            
            if (Request.Params["islem"] == "durum")
            {
                KategoriDurum();
            }

             if (Request.Params["islem"] == "sil")//Silme İşlemi 
            {
                 string flaSerial = Request.Params["flaSerial"];
                 if (flaSerial.Length < 4)
                 {
                     katgoriFlashSil(Convert.ToInt32(Request.Params["id"]));
                 }

                 kategoriSil(Convert.ToInt32(Request.Params["id"]));
            }

             if (Request.Params["islem"] == "duzenle")
             {
                 btnKategoriKaydet.Visible = false;
                 btnKategoriGuncelle.Visible = true;
                 ddlkategoriler.ClearSelection();
                 KayitBilgiGetir(Convert.ToInt32(Request.Params["id"]));
                 ScriptManager.RegisterStartupScript(Page, typeof(Page), "kategori", "tabAc()", true);
             }
             kategriKayitListele();
        }
    }


    #region Kategori ve Resimleri Silme İşlemi
    private void kategoriSil(int id)
    {
        try
        {
            foreach (var item in KategoriDB.ResimAdiListeleSilme(id))
            {
                Images.BigImage.Delete(item.resimAdi);
                Images.SmallImage.Delete(item.resimAdi);
                Images.LittleImage.Delete(item.resimAdi);
            }

            KategoriDB.Delete(id);

        }
        catch (Exception ex)
        {
            mesajGosterSis("Kategori Silme Hatası:", ex);
        }
    }
    #endregion

    #region anakategori flash silme
    private void katgoriFlashSil(int katId)
    {
        Kategori kat = KategoriDB.getir(katId);

        if (File.Exists(Server.MapPath("~/Products/Flash/" + kat.resimAdi)))
        {
            File.Delete(Server.MapPath("~/Products/Flash/" + kat.resimAdi));
            KategoriDB.flashSil(kat.resimAdi);
        }
    }
    #endregion 

    #region Kategori Durum İşlemi
    private void KategoriDurum()
    {
        try
        {
            KategoriDB.durum(Request.Params["id"], Request.Params["durum"]);
        }
        catch (Exception ex)
        {
            mesajGosterSis("Durum Degiştirme Hatası:" , ex);
        }
    }
    #endregion

    #region Kategori Kayıt Listeleme İşlemi
    private void kategriKayitListele()
    {
        try
        {
            GridView1.DataSource = KategoriDB.listeleme();
            GridView1.DataBind();
        }

        catch (Exception ex)
        {
            mesajGosterSis("Hata Oluştu:<br>" , ex);
        }
    }

    #endregion

    #region Kategori Sıralama İşlemi
    protected void kategoriSiraGuncelleme(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = (TextBox)sender;
            int katId = Convert.ToInt32(txt.ToolTip);
            string katSira = txt.Text.ToString();

            if (katSira == "")
                return;

            IcerikDB.GenelSiralama(katId, katSira, "kategori");
        }
        catch (Exception ex)
        {
            mesajGosterSis("Kategori Sira Güncelleme", ex);
        }
    }
    #endregion

    // kayıt Güncelleme işlemi

    #region  DropDown'a Kategori Listelem İşlemi
    private void kategoriListele()
    {
        ddlkategoriler.DataSource = KategoriDB.dropDownListele();
        ddlkategoriler.DataTextField = "kategoriadi";
        ddlkategoriler.DataValueField = "serial";
        ddlkategoriler.DataBind();

        ListItem item = new ListItem();
        item.Text = "-- Kategori Şeçiniz --";
        item.Value = "0";
        item.Selected = true;
        ddlkategoriler.Items.Add(item);
    }

    #endregion

    #region Kategori Kaydetme
    protected void btnKategoriKaydet_Click(object sender, EventArgs e)
    {

        int kayit_sinirla =  KategoriDB.kaydet(ddlkategoriler.SelectedValue.ToString(), "0", txtkategoriadi.Text, cbkategoridurum.Checked.ToString(), txtTitle.Text, txtDescription.Text, txtTitle.Text);

        ddlkategoriler.ClearSelection();
        txtkategoriadi.Text = "";
        txtTitle.Text = "";
        txtDescription.Text = "";
        txtKeyword.Text = "";
        kategoriListele();


        if (kayit_sinirla > 0)
        {
            mesajGosterNo("Artık alt kategori ekleyemezsiniz!...");
        }
        else
        {
            mesajGosterOk("Kayıt Başarı ile oluşturuldu.");
        }

        kategriKayitListele();
    }
    #endregion

    #region kategori Güncellemek İçin Verinin Getirilmesi İşlemi
    private void KayitBilgiGetir(int katId)
    {
        try
        {
            Kategori kat = KategoriDB.getir(katId);
            txtkategoriadi.Text = kat.kategoriadi;
            txtTitle.Text = kat.title;
            txtDescription.Text = kat.description;
            txtKeyword.Text = kat.keywords;
            cbkategoridurum.Checked =  kat.durum;
            string  anakategoriSerial = kat.serial;


            int karekterSay = anakategoriSerial.Length - 3;
            int katSerial = Convert.ToInt32(anakategoriSerial.Substring(0, 3));
            anakategoriSerial = anakategoriSerial.Substring(0, karekterSay);

            ddlkategoriler.ClearSelection();

            if (anakategoriSerial == "")
            {
                ListItem item = new ListItem();
                item.Text = "-- Kategori Şeçiniz --";
                item.Value = "0";
                item.Selected = true;
                ddlkategoriler.Items.Add(item);

                rowFlash.Attributes["Style"] = "display:''";

                if (kat.resimAdi != "ResimYok")
                {
                hddResimAd.Value = kat.resimAdi;
                ltlFlashKategori.Text = 
                    Flash.swfTool(ResolveUrl("~/Products/Flash/" + kat.resimAdi) , 150, 60);
                }
            }
            else
            {
                ddlkategoriler.SelectedValue = anakategoriSerial.ToString();
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("KATEGORİ GÜNCELLEME HATASI:", ex);
        }
    }
    #endregion

    #region  Kategori Güncelleme İşlemi
    protected void btnKategoriGuncelle_Click(object sender, EventArgs e)
    {
      
        kategoriId = Convert.ToInt32(Request.Params["id"]);
        string dosyaAdi = string.Empty;
        string[] dosya;
        string dosyaUzanti = string.Empty;
        string orginalFileName = string.Empty;
        string yeniDosyaAdi = string.Empty;

        if (txtresim.PostedFile.FileName != "" && txtresim.PostedFile.ContentLength > 0 )
        {
            if (txtresim.PostedFile.ContentType != "")
            {
                if (hddResimAd.Value != "")
                {
                    string hddResimAdi = hddResimAd.Value.ToString();

                    if (File.Exists(Server.MapPath("~/Products/Flash/") + hddResimAdi.ToString()))
                    {
                        File.Delete(Server.MapPath("~/Products/Flash/") + hddResimAdi.ToString());
                    }
                    KategoriDB.flashSil(hddResimAdi);
                }

                char slash = (char)92;
                orginalFileName = txtresim.PostedFile.FileName.Substring(Convert.ToInt32(txtresim.PostedFile.FileName.LastIndexOf(slash)) + 1);
                dosya = txtresim.PostedFile.FileName.Substring(Convert.ToInt32(txtresim.PostedFile.FileName.LastIndexOf(slash)) + 1).Split('.');

                dosyaAdi = dosya[0].ToString();
                dosyaUzanti = dosya[1].ToString();
                yeniDosyaAdi = resim.dosyaadi(dosyaUzanti.ToString());
                txtresim.PostedFile.SaveAs(Server.MapPath("~/Products/Flash/") + yeniDosyaAdi);
            }
        }

        if (yeniDosyaAdi != "")
        {
            ResimDB.Kaydet(kategoriId, yeniDosyaAdi, "kategori");
        }

        int kayit_sinirla = KategoriDB.kaydet(ddlkategoriler.SelectedValue.ToString(), kategoriId.ToString(), txtkategoriadi.Text, cbkategoridurum.Checked.ToString(), txtTitle.Text, txtDescription.Text, txtKeyword.Text);

  
        if (kayit_sinirla > 0)
        {
            mesajGosterNo("Artık alt kategori ekleyemezsiniz!...");
        }
        else
        {
            mesajGosterOk("Kaydınız Başarı ile Güncellendi");
            btnKategoriGuncelle.Visible = false;
            btnKategoriKaydet.Visible = true;
            txtDescription.Text = "";
            txtkategoriadi.Text = "";
            txtKeyword.Text = "";
            txtTitle.Text = "";
            ddlkategoriler.SelectedValue = "0";
            kategriKayitListele();
        }
    }
    #endregion

}
