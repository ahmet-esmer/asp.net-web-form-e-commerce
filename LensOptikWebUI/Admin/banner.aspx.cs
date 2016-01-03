using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using ImageLibrary;
using ModelLayer;
using BusinessLayer.BasePage;


public partial class Admin_Banner : BasePageAdmin
{

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            dpl_Resim_No_Listeleme();
            BannerListele();

            if (Request.Params["resimId"] != null)
            {
                BannerSil(Request.Params["resimadi"]);
            }

            if (Request.Params["islem"] == "durum")
            {
                BannerDurum();
            }

            if (Request.Params["islem"] == "duzenle")
            {
                dpl_Resim_No_Listeleme();
                btnReklamEkle.Visible = false;
                btnReklamGuncelle.Visible = true;
                BannerBilgiGetir();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "reklam", "tabAc()", true);
            }
        }
    }

    #region Banner Güncelleme İçin Bilgi Getirme İşlemi
    private void BannerBilgiGetir()
    {
        try
        {
            Banner banner = BannerDB.Get(Convert.ToInt32(Request.Params["id"]), "reklam");

            txtResimBaslik.Text = banner.ResimBaslik; ;
            ckb_ReklamDurum.Checked = banner.Durum;

            Hdd_Resim_Ad.Value = banner.ResimAdi;
            Hdd_Resim_id.Value = banner.Id.ToString();
            img_Ana_Resim.Visible = true;
            img_Ana_Resim.ImageUrl = "../Products/Big/" + banner.ResimAdi;

            dpl_ResimNo.ClearSelection();

            for (int i = 0; i <= 50; i++)
            {
                if (dpl_ResimNo.Items[i].Value == banner.Sira.ToString())
                {
                    dpl_ResimNo.Items[i].Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Reklam Listeleme Hatası:", ex);
        }
    }
    #endregion

    #region Banner Aktif Pasif Yapma İşlemi
    private void BannerDurum()
    {
        try
        {
            string durum = Request.Params["durum"];
            switch (durum)
            {
                case "0": durum = "False"; break;
                case "1": durum = "True"; break;  
            }

            BannerDB.Status(new Banner { 
                Id = Convert.ToInt32(Request.Params["id"]),
                Durum = Convert.ToBoolean(durum)
            });

            BannerListele();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Durum Degiştirme Hatası:" , ex);
        }
    }
    #endregion

    #region Banner Silme İşlemi
    private void BannerSil(string resimadi)
    {
        try
        {
            Images.BigImage.Delete(resimadi);
            Images.LittleImage.Delete(resimadi);

            ResimDB.ResimSil(Convert.ToInt32(Request.Params["resimId"]));

            mesajGosterOk("Reklam Silme İşlemi Yapıldı.");
            BannerListele();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Reklam Silme Hatası" , ex);
        }
    }
    #endregion

    #region Banner Listeleme İşlemi
    private void BannerListele()
    {
        try
        {
            gvwBanner.DataSource = BannerDB.BannerList();
            gvwBanner.DataBind();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Banner Listeleme Hatası" , ex);
        }
    }
    #endregion

    #region Banner Sıra No Listeleme Döngisi
    private void dpl_Resim_No_Listeleme()
    {

        dpl_ResimNo.ClearSelection();

        ListItem item = new ListItem();
        item.Text = "-- Reklam Gösterim Sırası --";
        item.Value = "0";
        item.Selected = true;
        dpl_ResimNo.Items.Add(item);

        for (int i = 50; i >= 1; i--)
        {
            dpl_ResimNo.Items.Insert(1, new ListItem(i.ToString(), i.ToString()));
        }

    }
    #endregion

    #region Banner Ekleme Alanı
    protected void btnReklamEkle_Click(object sender, EventArgs e)
    {
        string dosyaUzanti = string.Empty;
        string resimAdi = string.Empty;

        if (txtResimEk.PostedFile.FileName != "" || txtResimEk.PostedFile.ContentLength > 0)
        {
            resimAdi = Images.GetImageName(txtResimEk.PostedFile.FileName);
   
            txtResimEk.PostedFile.SaveAs(Images.GetPathBig(resimAdi));
        }

        Banner banner  = new Banner
        {
            Parametre = "reklam",
            ResimAdi = resimAdi,  
            ResimBaslik = txtResimBaslik.Text ,
            Durum = Convert.ToBoolean(ckb_ReklamDurum.Checked),
            Sira = Convert.ToInt16(dpl_ResimNo.SelectedItem.Value)
        };

        BannerDB.Save(banner);

       
        mesajGizleNo();
        mesajGosterOk("Reklam İçerigi Başarı ile Oluştururdu.");


        txtResimBaslik.Text = "";
        dpl_ResimNo.ClearSelection();
        BannerListele();
    }
    #endregion

    #region Banner Güncelleme İşlemi
    protected void btnReklamGuncelle_Click(object sender, EventArgs e)
    {
        string dosyaUzanti = string.Empty;
        string yeniDosyaAdi = Hdd_Resim_Ad.Value;
        string hddResimGunAdi = Hdd_Resim_Ad.Value;

        try
        {
            if (txtResimEk.PostedFile.FileName != "" && txtResimEk.PostedFile.ContentLength > 0)
            {
                Images.BigImage.Delete(hddResimGunAdi);
                if (txtResimEk.PostedFile.ContentType != "")
                {
                    yeniDosyaAdi = Images.GetImageName(txtResimEk.PostedFile.FileName);
                    txtResimEk.PostedFile.SaveAs(Images.GetPathBig(yeniDosyaAdi));
                }
            }

            Banner banner = new Banner
            {
                Id = Convert.ToInt32(Hdd_Resim_id.Value),
                Parametre = "reklam",
                ResimAdi = yeniDosyaAdi,
                ResimBaslik = txtResimBaslik.Text,
                Durum = Convert.ToBoolean(ckb_ReklamDurum.Checked),
                Sira = Convert.ToInt16(dpl_ResimNo.SelectedItem.Value)
            };

            BannerDB.Save(banner);

            mesajGosterOk("Reklam İçerigi Başarı Güncellendi.");
            btnReklamEkle.Visible = true;
            btnReklamGuncelle.Visible = false;
            txtResimBaslik.Text = "";
            BannerListele();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Güncelleme Hatası", ex);
        }
    }
    #endregion
}