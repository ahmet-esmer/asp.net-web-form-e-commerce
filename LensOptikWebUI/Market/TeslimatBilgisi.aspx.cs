using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessLayer.Security;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using System.Data;

public partial class Market_Teslimat_Bilgisi : System.Web.UI.Page
{
    private int uyeId = 0;
    private string uyeAdi = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoginKontrol();

        if (!IsPostBack)
        {
            Sehirler();
            adresListele();
            faturaBilgisiListele();
        }

        Session["adresMenu"] = "True";
    }



    public void Sehirler()
    {
        DataTable dt = SehirDB.Sehirler();

        foreach (DataRow dataRow in dt.Rows)
        {
            ddlSehirler.Items.Add(new ListItem(dataRow["Ad"].ToString(), dataRow["IlID"].ToString()));
        }

        ListItem item = new ListItem();
        item.Text = "-- Şehir Seçiniz --";
        item.Value = "0";
        item.Selected = true;
        ddlSehirler.Items.Add(item);
    }

    private void LoginKontrol()
    {
        if (KullaniciOperasyon.LoginKontrol())
        {
            uyeId = KullaniciOperasyon.GetId();
            uyeAdi = KullaniciOperasyon.GetName();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }


    #region Adres Listele
    private void adresListele()
    {
        try
        {
            List<KullaniciAdres>  list = KullaniciAdresDB.Liste(uyeId);

            rptAdresler.DataSource = list;
            rptAdresler.DataBind();

            if (list.Count >= 2)
            {
                foreach (RepeaterItem item in rptAdresler.Items)
                {
                    Button yeniAdres = (Button)item.FindControl("btnYeniAdres");
                    yeniAdres.Visible = false;
                }
            }

            if (list.Count == 0)
            {
                btnYeniAdres.Visible = true;
                hdfSecAdres.Value = "0";
            }
            else
            {
                btnYeniAdres.Visible = false;
                hdfSecAdres.Value = list[0].Id.ToString();
            }
        }
        catch (Exception ex)
        {
            Mesaj.ErrorSis("Adres Listeleme hatası..");
            LogManager.SqlDB.Write("Adres Listeleme hatası..", ex);
        }
    }
    #endregion

    #region Adres Düzenleme Bilgi Getir
    private void AdresDuzenleBilgiGetir(int id)
    {
        try
        {
            ddlSehirler.ClearSelection();

            mpeAdresForm.Show();
            btnAdresKayit.Visible = false;
            btnAdresDuzenle.Visible = true;

            KullaniciAdres adres = KullaniciAdresDB.Getir(id);

            txtAdres.Text = adres.Adres;
            txtTeslimAlan.Text = adres.TeslimAlan;
            txtTelefon.Text = adres.Telefon;
            hdAdresId.Value = adres.Id.ToString();
            ddlSehirler.SelectedValue = adres.SehirId.ToString();

        }
        catch (Exception ex)
        {
            Mesaj.ErrorSis("Adres Düzenleme Hatası");
            LogManager.SqlDB.Write("Adres Düzenleme Hatası", ex);
        }
    }
    #endregion

    protected void btnYeniAdres_Click(object sender, ImageClickEventArgs e)
    {
        mpeAdresForm.Show();
    }

    #region Adres Reapeter Command
    protected void rptAdresler_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "adresSil")
            {
                KullaniciAdresDB.Sil(uyeId, Convert.ToInt32(e.CommandArgument));
                adresListele();
            }
            else if (e.CommandName == "adresDuzenle")
            {
                AdresDuzenleBilgiGetir(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "adresEkle")
            {
                mpeAdresForm.Show();
            }
        }
        catch (Exception ex)
        {
            Mesaj.Alert("İşlem hata ile sonuçlandı.");
            LogManager.SqlDB.Write("Adres Reapeter Command Hata Oluştu", ex);
        }
    }
    #endregion 

    #region Adres kaydı
    protected void btnAdresKayit_Click(object sender, EventArgs e)
    {
        try
        {
            KullaniciAdresDB.Kaydet(new KullaniciAdres
            {
                UyeId = uyeId,
                Adres = txtAdres.Text,
                TeslimAlan = txtTeslimAlan.Text,
                Telefon = txtTelefon.Text,
                SehirId = Convert.ToInt32(ddlSehirler.SelectedValue)
            });

            txtAdres.Text = "";
            txtTelefon.Text = "";
            txtTeslimAlan.Text = "";

            adresListele();
        }
        catch (Exception ex)
        {
            Mesaj.ErrorSis("Kullanıcı adres ekleme hatası..");
            LogManager.SqlDB.Write("Kullanıcı adres ekleme hatası", ex);
        }
    }
    #endregion

    #region Adres Güncelleme
    protected void btnAdresDuzenle_Click(object sender, EventArgs e)
    {
        try
        {
            KullaniciAdresDB.Duzenle(new KullaniciAdres
            {
                Id = Convert.ToInt32(hdAdresId.Value),
                Adres = txtAdres.Text,
                TeslimAlan = txtTeslimAlan.Text,
                Telefon = txtTelefon.Text,
                SehirId = Convert.ToInt32(ddlSehirler.SelectedValue)
            });

            Mesaj.Successful("Adres bilgileri başarı ile güncellendi.");

            adreslerBilgi.Visible = true;
            txtAdres.Text = "";
            txtTelefon.Text = "";
            txtTeslimAlan.Text = "";
            adresListele();

            btnAdresKayit.Visible = true;
            btnAdresDuzenle.Visible = false;
        }
        catch (Exception ex)
        {
            Mesaj.ErrorSis("Kullanıcı adres guncelleme hatası..");
            LogManager.SqlDB.Write("Kullanıcı adres Düzenleme hatası", ex);
        }
    }
    #endregion


    // Fatura İşlemleri
    #region Fatura listeleme
    private void faturaBilgisiListele()
    {
        try
        {
            List<KullaniciFatura>  faturList = KullaniciFaturaDB.Liste(uyeId);

            rptFaturaBilgi.DataSource = faturList;
            rptFaturaBilgi.DataBind();


            if (faturList.Count >= 2)
            {
                foreach (RepeaterItem item in rptFaturaBilgi.Items)
                {
                    Button yeniFatura = (Button)item.FindControl("btnYeniFatura");
                    yeniFatura.Visible = false;
                }
            }

            if (faturList.Count == 0)
            {
                btnYeniFaturaAc.Visible = true;
                hdfSecFatura.Value = "0";
            }
            else
            {
                btnYeniFaturaAc.Visible = false;
                hdfSecFatura.Value = faturList[0].Id.ToString();
            }
        }
        catch (Exception ex)
        {
            Mesaj.ErrorSis("Fatura Bilgisi Listeleme hatası..");
            LogManager.Mail.Write("Fatura Bilgisi Listeleme hatası", ex);
        }
    }
    #endregion

    #region Fatura Düzenle Bilgi Getirme
    private void FaturaDuzenleBilgiGetir(int id)
    {
        try
        {
            KullaniciFatura fatura = KullaniciFaturaDB.Getir(id);

            hdFaturaId.Value = fatura.Id.ToString();

            if (fatura.FaturaCinsi)
            {
                txtBirAdsoyad.Text = fatura.AdSoyad;
                txtBirTcNo.Text = fatura.TCNo;
                txtBirAdres.Text = fatura.FaturaAdresi;
                fatBireysel.Checked = true;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "fn1", "fnBireysel()", true); 
            }
            else
            {
                txtKurUnvan.Text = fatura.Unvan;
                txtKurVergiNo.Text = fatura.VergiNo;
                txtKurVergiDairesi.Text = fatura.VergiDairesi;
                txtKurAdres.Text = fatura.FaturaAdresi;
                fatBireysel.Checked = true;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "fn2", "fnKurumsal()", true); 
            }

            btnFaturaKaydet.Visible = false;
            btnFaturaDuzenle.Visible = true;

            mpeFaturaForm.Show();

        }
        catch (Exception ex)
        {
            Mesaj.Alert("Adres Düzenleme Hatası.");
            LogManager.SqlDB.Write("Adres Düzenleme Hatası", ex);
        }
    }
    #endregion

    protected void btnYeniFaturaAc_Click(object sender, ImageClickEventArgs e)
    {
        fatBireysel.Checked = true;
        fatKurumsal.Checked = false;
        btnFaturaDuzenle.Visible = false;
        btnFaturaKaydet.Visible = true;

        mpeFaturaForm.Show();
    }

    #region Fatura reapeter command
    protected void rptFaturaBilgi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "faturaSil")
            {
                KullaniciFaturaDB.Sil(uyeId, Convert.ToInt32(e.CommandArgument));
                faturaBilgisiListele();

            }
            else if (e.CommandName == "faturaDuzenle")
            {
                FaturaDuzenleBilgiGetir(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "yeniFatura")
            {
                fatBireysel.Checked = true;
                fatKurumsal.Checked = false;
                btnFaturaDuzenle.Visible = false;
                btnFaturaKaydet.Visible = true;

                mpeFaturaForm.Show();
            }

        }
        catch (Exception ex)
        {
            Mesaj.ErrorSis("Fatura Bilgisi Silinirken Hata Oluştu");
            LogManager.SqlDB.Write("Fatura Bilgisi Silinirken Hata Oluştu", ex);
        }
    }
    #endregion

    #region Fatura kaydet
    protected void btnFaturaKaydet_Click(object sender, EventArgs e)
    {
        try
        {
            KullaniciFatura f = new KullaniciFatura();
            f.UyeId = uyeId;

            if (fatBireysel.Checked)// Bireysel Fatura
            {
                f.FaturaCinsi = true;
                f.AdSoyad = GuvenlikIslemleri.hackKontrol(txtBirAdsoyad.Text);
                f.TCNo = GuvenlikIslemleri.hackKontrol(txtBirTcNo.Text);
                f.FaturaAdresi = GuvenlikIslemleri.hackKontrol(txtBirAdres.Text);
                txtBirAdsoyad.Text = "";
                txtBirTcNo.Text = "";
                txtBirAdres.Text = "";
            }
            else//Kurumsal Fatura
            {
                f.FaturaCinsi = false;
                f.FaturaAdresi = GuvenlikIslemleri.hackKontrol(txtKurAdres.Text);
                f.Unvan = GuvenlikIslemleri.hackKontrol(txtKurUnvan.Text);
                f.VergiNo = GuvenlikIslemleri.hackKontrol(txtKurVergiNo.Text);
                f.VergiDairesi = GuvenlikIslemleri.hackKontrol(txtKurVergiDairesi.Text);
                txtKurUnvan.Text = "";
                txtKurVergiDairesi.Text = "";
                txtKurVergiNo.Text = "";
                txtKurAdres.Text = "";
            }

 
            KullaniciFaturaDB.kaydet(f);

            faturaBilgisiListele();
        }
        catch (Exception ex)
        {
            Mesaj.Alert("Kullanıcı adres ekleme hatası..");
            LogManager.SqlDB.Write("Kullanıcı adres ekleme hatası", ex);
        }
    }
    #endregion

    #region  Fatura düzenle kaydet
    protected void btnFaturaDuzenle_Click(object sender, EventArgs e)
    {
        try
        {
            KullaniciFatura f = new KullaniciFatura();
            f.Id = Convert.ToInt32(hdFaturaId.Value);

            if (fatBireysel.Checked)// Bireysel Fatura
            {
                f.FaturaCinsi = true;
                f.AdSoyad = GuvenlikIslemleri.hackKontrol(txtBirAdsoyad.Text);
                f.TCNo = GuvenlikIslemleri.hackKontrol(txtBirTcNo.Text);
                f.FaturaAdresi = GuvenlikIslemleri.hackKontrol(txtBirAdres.Text);
                txtBirAdsoyad.Text = "";
                txtBirTcNo.Text = "";
                txtBirAdres.Text = "";
            }
            else//Kurumsal Fatura
            {
                f.FaturaAdresi = GuvenlikIslemleri.hackKontrol(txtKurAdres.Text);
                f.Unvan = GuvenlikIslemleri.hackKontrol(txtKurUnvan.Text);
                f.VergiNo = GuvenlikIslemleri.hackKontrol(txtKurVergiNo.Text);
                f.VergiDairesi = GuvenlikIslemleri.hackKontrol(txtKurVergiDairesi.Text);
                txtKurUnvan.Text = "";
                txtKurVergiDairesi.Text = "";
                txtKurVergiNo.Text = "";
                txtKurAdres.Text = "";
            }

            KullaniciFaturaDB.Duzenle(f);

            faturaBilgisiListele();
        }
        catch (Exception ex)
        {
            Mesaj.Alert("Adres düzenleme hatası..");
            LogManager.Mail.Write("Kullanıcı adres ekleme hatası", ex);
        }
    }
    #endregion


    protected void btnOnay_Click(object sender, ImageClickEventArgs e)
    {
        string adresId = hdfSecAdres.Value;
        string faturaId = hdfSecFatura.Value;

        if (adresId == "0" && faturaId == "0")
        {
            Mesaj.Alert("Lütfen Siparişinizin Gönderilecegi Adresi Seçiniz <br/> Lütfen Fatura Bilgisi Seçiniz");
            return;
        }
        if (adresId == "0")
        {
            Mesaj.Alert("Lütfen Siparişinizin Gönderilecegi Adresi Seçiniz");
            return;
        }
        if (faturaId == "0")
        {
            Mesaj.Alert("Lütfen Fatura Bilgisi Seçiniz");
            return;
        }

        Session["adresId"] = adresId;
        Session["faturaId"] = faturaId;

        Response.Redirect("~/Market/IslemOnay.aspx");


    }
}