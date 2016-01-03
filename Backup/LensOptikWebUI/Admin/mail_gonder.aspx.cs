using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DataAccessLayer;
using MailLibrary;
using BusinessLayer;
using BusinessLayer.BasePage;

public partial class uyeMailGonder :  BasePageAdmin
{

    private SqlDataReader dr;
    private int SayfaNo = 0;
    private int sayfaGosterim = 25;
    private int toplamKayit = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (Request.Params["ePosta"] != null && Request.Params["Ad"] != null)
            {
                lblAlici.Visible = true;
                ddlMailGonder.Visible = false;
                lblAlici.Text = Request.Params["Ad"].ToString();
                btnMailGonder.Visible = true;
                btnTopluMail.Visible = false;
            }
        }
    }

    protected void  ePostaGonder(object sender, EventArgs e)
    {
           try
           {
               string konu = txtKonu.Text;
               string mailMesaj = FCKeditor1.Value.ToString();
               string bolge = ddlMailGonder.SelectedValue;

               SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@parametre" , bolge),
                    new SqlParameter("@Baslangic", 1),
                    new SqlParameter("@Bitis", sayfaGosterim )
                };

               if (bolge.ToString() != "mailList")
               {
                    bolge = "siteUyeleri";
               }

               dr = SqlHelper.ExecuteReader("mail_KayitListeleSirali", parametre);
      
               int count = Convert.ToInt32(hdfNo.Value);
               StringBuilder sb = new StringBuilder();

               Boolean sonuc = true;
               string ePosta = null;
               
               while (dr.Read())
               {
                   count += +1;
                   ePosta = dr.GetString(dr.GetOrdinal("ePosta"));

                   sonuc = MailManager.User.Bulk(ePosta, konu, mailMesaj);

                   MailDB.TarihKaydet(ePosta, bolge);
                   
                   sb.Append("<div class='mail1' >" + count.ToString() + " : </div> ");
                   sb.Append("<div class='mail2' >" + dr.GetString(dr.GetOrdinal("adiSoyadi")) + "</div> ");
                   sb.Append("<div class='mail3' >" + ePosta.ToString() + "</div> ");
                   sb.Append("<div class='mail4' >" + GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(sonuc)) + "</div>"); 
               }
             
               lblTamami.Text = sb.ToString();
               hdfNo.Value = Convert.ToString(count);

               SayfaNo = Convert.ToInt32(hdfSayfaNo.Value);
               hdfSayfaNo.Value = Convert.ToString(SayfaNo += +1);

               int sonSayfa =  Convert.ToInt32(hdfTolamSayfa.Value);
               if ( SayfaNo >= sonSayfa + 1)
               {
                   tmEposta.Enabled = false;
               }

           }
           catch (Exception hata)
           {
               lblHata.Text = "E-Posta Gönderi Hatası:" + hata.ToString();
           }
    }

    // Toplu Mail Gönderme İşlemi Timer Tetikleme
    protected void btnTopluMail_Click(object sender, EventArgs e)
    {
        tmEposta.Enabled = true;
        KayitSayisi();
    }

    #region Toplam Kayıt Sayısı Almak
    private void KayitSayisi()
    {
        SqlParameter parametre = new SqlParameter("@parametre", ddlMailGonder.SelectedValue.ToString());
 
        toplamKayit = (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "mail_KayitListeleSiraNo", parametre);
        int ToplamSayfaSayisi = 0;
        if (toplamKayit % sayfaGosterim == 0)
        {
            ToplamSayfaSayisi = toplamKayit / sayfaGosterim;
        }
        else
        {
            ToplamSayfaSayisi = toplamKayit / sayfaGosterim + 1;
        }

        hdfTolamSayfa.Value = ToplamSayfaSayisi.ToString();

    }
    #endregion

    #region Manuel Mail Gönderme İşlemi
    protected void btnManuelGonder_Click(object sender, EventArgs e)
    {
        try
        {
            string konu = txtKonu.Text.ToString();
            string Alici = txtMailManuel.Text.ToString();
            string mailMesaj = FCKeditor1.Value.ToString();

            MailManager.User.Send(Alici, konu, mailMesaj);

            mesajGosterInfo(txtMailManuel.Text + " adresine Mail gönderildi");
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>E-Posta Gönderi Hatası:</b>" , ex);

        }

    }
    #endregion

    #region Üye Listeleme Bölümünden Gelen Tek Mail Gönderme İşlemi
    protected void btnMailGonder_Click(object sender, EventArgs e)
    {
        try
        {
               string konu = txtKonu.Text.ToString();
               string Alici = Request.Params["ePosta"].ToString();
               string mailMesaj = FCKeditor1.Value.ToString();

               MailManager.User.Send(Alici, konu, mailMesaj);

               mesajGosterInfo(Alici + " adresine Mail gönderildi");

        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>E-Posta Gönderi Hatası:</b>" , ex);
        }
    }
    #endregion

    #region DropDown List e Mail Gönderim Bilgisi Getirme İşlemi
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ( ddlMailGonder.SelectedValue.ToString() != Convert.ToString(0))
        {
            SqlParameter parametre = new SqlParameter("@parametre" , ddlMailGonder.SelectedValue.ToString());

            toplamKayit = (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "mail_KayitListeleSiraNo", parametre);

            string gosterimAd = ddlMailGonder.SelectedValue.ToString();
            switch (gosterimAd)
            {
                case "siteUyeleri": gosterimAd = "Aişveriş Yapmayan Site Üyeleri "; break;
                case "SiteMusteri": gosterimAd = "Alişveriş Yapan Müşteriler "; break;
                case "butunUyeler": gosterimAd = "Üyeler ve Müşteriler Tamamı "; break;
                case "enZiyaret": gosterimAd = "Siteyi En Çok Ziyaret Eden Müşteriler ve Üyeler "; break;
                case "mailList": gosterimAd = "Mail Listesi "; break;
            }

            mesajGosterInfo("<b>" + gosterimAd.ToString() + "</b> Mail Gönderilecek Toplam Kişi Sayısı: <b>" + toplamKayit.ToString());
        }
    }
    #endregion

    #region Mail Gönderme Formunu Manuel Haline Getirme
    protected void btnmManuelAc_Click(object sender, EventArgs e)
    {
        ddlMailGonder.Visible = false;
        txtMailManuel.Visible = true;
        btnTopluMail.Visible = false;
        btnmManuelAc.Visible = false;
        btnTopluAc.Visible = true;
        btnManuelGonder.Visible = true;
        lblAlici.Visible = false;
        btnMailGonder.Visible = false;
 
    }
    #endregion

    #region Mail Gönderme Formunu Toplu Gönderim Haline Getirme
    protected void btnTopluAc_Click(object sender, EventArgs e)
    {
        ddlMailGonder.Visible = true;
        txtMailManuel.Visible = false;
        btnTopluMail.Visible = true;
        btnmManuelAc.Visible = true;
        btnTopluAc.Visible = false;
        btnManuelGonder.Visible = false;
        lblAlici.Visible = false;
        btnMailGonder.Visible = false;

    }
    #endregion

}




