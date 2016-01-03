using System;
using System.Data.SqlClient;
using System.Web.UI;
using BusinessLayer;
using BusinessLayer.Security;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using System.Data;
using System.Web.UI.WebControls;

public partial class Market_TeslimatBilgisi : System.Web.UI.Page
{
    private int uyeId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        Session["adresMenu"] = "True";
        LoginKontrol();

        if (!IsPostBack)
        {
            Sehirler();
            uyeBilgiGetir();

            if (KullaniciAdresDB.AdresVarmi(uyeId) > 0)
            {
                Response.Redirect("~/Market/TeslimatBilgisi.aspx");
            }
        }
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
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }


    #region Kullanıcı Ön  Bilgi Getir
    private void uyeBilgiGetir()
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@id", uyeId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader("kullanici_Getir", parametre))
            {
                while (dr.Read())
                {
                    txtTelefon.Text = dr.GetString(dr.GetOrdinal("Gsm"));
                    txtTeslimAlan.Text = dr.GetString(dr.GetOrdinal("adiSoyadi"));
                }
            }
        }
        catch (Exception ex)
        {
            Mesaj.Alert("Adres Listeleme hatası..");
            LogManager.Mail.Write("Teslimat adres listeleme hatası..", ex);
        }
    }
    #endregion

    protected void imBtnSatinAl_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            KullaniciFatura f = new KullaniciFatura();
            f.UyeId = uyeId;

            string teslimAlan = txtTeslimAlan.Text;
            string fmAdres = txtAdres.Text;

            KullaniciAdres adres = new KullaniciAdres
            {
                UyeId = uyeId,
                Adres = fmAdres,
                Telefon = txtTelefon.Text,
                TeslimAlan = teslimAlan,
                SehirId = Convert.ToInt32(ddlSehirler.SelectedValue)
            };

            int adresId = KullaniciAdresDB.Kaydet(adres);

            // Fatura Form
            if (ckbAdres.Checked)
            {
                if (fatBireysel.Checked)// Bireysel Fatura
                {
                   f.FaturaCinsi = true;
                   f.AdSoyad = txtBirAdsoyad.Text;
                   f.TCNo = txtBirTcNo.Text;
                   f.FaturaAdresi = txtBirAdres.Text;                    
                }
                else//Kurumsal Fatura
                {
                    f.FaturaAdresi = txtKurAdres.Text;
                    f.Unvan = txtKurUnvan.Text;
                    f.VergiNo = txtKurVergiNo.Text;
                    f.VergiDairesi = txtKurVergiDairesi.Text;
                }
            }// Adres Form
            else
            {
                if (adrBireysel.Checked)// Bireysel
                {
                    f.AdSoyad = teslimAlan;
                    f.FaturaAdresi = fmAdres + " " + ddlSehirler.SelectedItem;
                    f.FaturaCinsi = true;
                    f.TCNo = txtAdrTCno.Text;
                }
                else// adres Kurumsal
                {
                    f.Unvan = teslimAlan;
                    f.FaturaAdresi = fmAdres + " " + ddlSehirler.SelectedItem;
                    f.VergiDairesi = txtAdrVergiDaire.Text;
                    f.VergiNo = txtAdrVergiNo.Text  ;
                }
            }

            int faturaId = KullaniciFaturaDB.kaydet(f);

            Session["adresId"] = adresId.ToString();
            Session["faturaId"] = faturaId.ToString();

            Response.Redirect("~/Market/IslemOnay.aspx", false);

        }
        catch (Exception ex)
        {
            Mesaj.ErrorSis("Kullanıcı adres ekleme hatası..", ex.ToString());
            LogManager.Mail.Write("Kullanıcı adres ekleme hatası", ex);
        }
    }
}