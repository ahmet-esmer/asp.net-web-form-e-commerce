using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web.UI.WebControls;
using BusinessLayer.BasePage;
using DataAccessLayer;
using MailLibrary;
using ModelLayer;
using LoggerLibrary;

public partial class Admin_SiparisDetay: BasePageAdmin
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if(!IsPostBack)
        {
            if (Request.QueryString["siparisId"] != null)
            {
                int siparisId = Convert.ToInt32(Request.QueryString["siparisId"]);
                string url;
                if (Request.QueryString["retSayfa"] != null)
                {
                   url = string.Format("siparisler.aspx?Sayfa={0}&goto={1}", 
                         Request.QueryString["retSayfa"], siparisId);
                }
                else
                {
                    url  = string.Format("siparisler.aspx?goto={0}", siparisId);
                }

                hlSiparisler.NavigateUrl = url;
                ((HyperLink)Master.FindControl("hlMSiparis")).NavigateUrl = url;

                siparisDetayListeleme(siparisId);
                siparisDetayUrunListeleme(siparisId);
            }
        }
    }


    #region Sipariş Durum Güncelleme
    protected void btnSiparisGuncelle_Click(object sender, EventArgs e)
    {
        try
        {
            string siparisDurumu = ddlSiparisDurum.SelectedValue;
            
            SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@siparisId", Convert.ToInt32(Request.Params["siparisId"])),
                    new SqlParameter("@siparisDurumu", siparisDurumu),
                };

            SqlHelper.ExecuteNonQuery("siparis_KayitDurumDuzenle", parametre);

            if (ckbUyePosta.Checked)
            {
                string uyeAdi = hdfUyeAdi.Value; ;
                StringBuilder sb = new StringBuilder();

                sb.Append("<table cellpadding='0' cellspacing='0' style='width:100%;border:1px solid #d9dada;background-color:#fafafa;margin:20px 0px; font-family:Verdana;' >");
                sb.Append("<tr><td  style='background-color:#f2f3f4;height:25px;padding-top:10px;padding-left:15px;font-size:15px;fontweight:700;color:#525252;border-bottom:1px solid #d9dada;' > <b>Sipariş No:" + hdfSiparisNo.Value + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr><td style='vertical-align:middle;font-size:12px;color:#292929;height:130px;border-bottom:1px solid #d9dada;padding:25px 15px;vertical-align:text-top;' ><b>Sayın " + uyeAdi + " </b><br/> <br/>  lensoptik.com dan vermiş olduğunuz siparişiniz " + ddlSiparisDurum.SelectedItem.ToString() + ". </td> </tr>");
                sb.Append("<tr><td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;border-bottom:1px solid #fff;padding-left:15px;' > Online alışverişinizde bizi tercih ettiğiniz için teşekkür ederiz. </td> </tr>");
                sb.Append("<tr><td style='vertical-align:middle;font-size:12px;color:#069;height:40px;padding-left:15px;text-decoration:underline;'> <a style='font-size:12px;color:#069;'  href='http://www.lensoptik.com.tr' target='_blank'>www.lensoptik.com.tr </a> </td> </tr>");
                sb.Append("</table>");

                try
                {
                    MailManager.User.Send(hdfUyeEposta.Value, "Sipariş Durumu", sb.ToString());
                    mesajGosterOk("Kulanıcıya Durum E-Postayla Bildirildi.");
                }
                catch (Exception)
                {
                    mesajGosterNo("Kulanıcıya E-Postayla Gönderilmedi.");
                }
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Sipariş Güncelleme Hatası", ex);
        }
    }
    #endregion

    #region Sipariş Detay
    private void siparisDetayListeleme(int siparisId)
    {
        try
        {
            SiparisResponse siparis = SiparisDB.DetayAdmin(siparisId);

            hdfUyeEposta.Value = siparis.Mail;
            lblTarih.Text = siparis.Tarih;
            lblodemeTipi.Text = siparis.OdemeTipi;
            lblToplamFiyat.Text = siparis.FiyatToplam;

            lblSipariNotu.Text = siparis.Mesaj;

            lblAlici.Text = siparis.Adress.TeslimAlan;
            lblTelefon.Text = siparis.Adress.Telefon;
            lblAdres.Text = siparis.Adress.Adres;

            if (siparis.Adress.Sehir != "Şehir Seçiniz")
            {
                lblSehir.Text = siparis.Adress.Sehir;
            }
            else
            {
                lblSehir.ForeColor = Color.Red;
                lblSehir.Text = "Eski adres kaydı şehir seçilmedi";
            }

            lblTaksitTutari.Text = siparis.HavaleVeKapi;
            lblBankaAdi.Text = siparis.BankaAdi;

            if (siparis.Fatura.FaturaCinsi)
            {
                bireyselFatura.Visible = true;
                lblBirAdSoyad.Text = siparis.Fatura.AdSoyad;
                lblBirTC.Text = siparis.Fatura.TCNo;
                lblBirAdres.Text = siparis.Fatura.FaturaAdresi;
            }
            else
            {
                kurumsalFatura.Visible = true;
                lblKurUnvan.Text = siparis.Fatura.Unvan;
                lblKurVergiNo.Text = siparis.Fatura.VergiNo;
                lblKurVergiDairesi.Text = siparis.Fatura.VergiDairesi;
                lblKurAdres.Text = siparis.Fatura.FaturaAdresi;
            }

            lblBirimToplam.Text = siparis.BirimFiyat;
            lblKdvToplam.Text = siparis.KDVToplam; 
           
            lblToplam.Text = siparis.FiyatToplam;



            hdfUyeAdi.Value = siparis.UyeAdi;
            lblUyeAdi.Text = siparis.UyeAdi;
            lblEpostaAdi.Text = siparis.UyeAdi;

            lblEPosta.Text = siparis.Mail;
            hdfSiparisNo.Value = siparis.SiparisNo;
            lblSiparisNO1.Text = siparis.SiparisNo;



            if (siparis.KargoFiyat != "0,00 TL")
            {
                trKargo.Visible = true;
                lblKorgoUcreti.Text = siparis.KargoFiyat;
            }

            if (siparis.Taksit > 1)
            {
                lblKTaksit.Text = ": Taksitli (" + siparis.Taksit.ToString() + ")";
                lblTaksitSayisi.Text = siparis.Taksit.ToString();
            }
            else
            {
                lblTaksitSayisi.Text = "Tek Çekim";
                lblKTaksit.Text = ": Tek Çekim";
            }

            if (siparis.OdemeTipi == "Kredi Kartı")
            {
                Taksit1.Visible = true;
                Taksit2.Visible = true;
                Taksit3.Visible = true;
                lblKTaksit.Visible = true;
            }
            else if (siparis.OdemeTipi == "Havale")
            {
                havaleVeKapiOdeme.Visible = true;
                lblKapidaVeHavale1.Text = "Havale İndirimi";
                lblKapidaVeHavale2.Text = "- " + siparis.HavaleVeKapi;
                HavaleSatir.Visible = true;
                havaleBanka.Text = siparis.BankaAdi;
            }
            else
            {
                havaleVeKapiOdeme.Visible = true;
                lblKapidaVeHavale1.Text = "Kapıda ödeme Farkı";
                lblKapidaVeHavale2.Text = "+ " + siparis.HavaleVeKapi;
                HavaleSatir.Visible = true;
            }

            for (int i = 0; i < ddlSiparisDurum.Items.Count; i++)
            {
                if (ddlSiparisDurum.Items[i].Value == siparis.Durum)
                    ddlSiparisDurum.Items[i].Selected = true;
            }

            if (siparis.KullanilanPara > 0)
            {
                trKulPara.Visible = true;
                lblKulPara.Text = siparis.KullanilanPara.ToString("C");
            }

            if (siparis.Indirim > 0)
            {
                trIndirim.Visible = true;
                lblIndirim.Text = "- " + siparis.Indirim.ToString("C");
            }  
        }
        catch (Exception ex)
        {
            mesajGosterSis("Sipariş Detay Listeleme hatası.." , ex);
        }
    }
    #endregion

    #region Sipariş Detay Ürün Listelme
    private void siparisDetayUrunListeleme(int siparisId)
    {
        try
        {
            rptSiparisDetay.DataSource = SiparisDB.DetayUrunListe(siparisId);
            rptSiparisDetay.DataBind();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Sipariş Detay Ürün  Listeleme hatası..", ex);
        }
    }
    #endregion

    #region Sipariş Silme İşlemi
    protected void btnSiparisSil_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@siparisId", Convert.ToInt32(Request.Params["siparisId"])); 
            SqlHelper.ExecuteNonQuery("siparis_KayitSil", parametre);

            Response.Redirect("siparisler.aspx");
        }
        catch (Exception ex)
        {
            mesajGosterSis("Sipariş Silme  hatası..", ex);
        }
    }
    #endregion

    #region Kulanici E Posta gönder
    protected void btnEpostaGonder_Click(object sender, EventArgs e)
    {
        try
        {
            MailManager.User.Send(hdfUyeEposta.Value, txtMailBaslik.Text, fckSiparisMail.Value);
            mesajGosterOk("E-Posta Başarı ile Gönderildi");
        }
        catch (Exception ex)
        {
            mesajGosterSis("Kulanıcıya E-Postayla Gönderilmedi.", ex);
        }
    }
    #endregion

    protected void hlOdemeDetayShow_Click(object sender, EventArgs e)
    {
        using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, "SELECT siparisId,kartNo,kartAd,onayKodu,referansNo FROM tbl_siparisOdeme WHERE siparisId ='" + Request.Params["siparisId"] + "'"))
        {
            while (dr.Read())
            {
                lblOdemeYapan.Text = dr.GetString(dr.GetOrdinal("kartAd"));
                lblKartNo.Text = dr.GetString(dr.GetOrdinal("kartNo"));
                lblReferansNo.Text = dr.GetString(dr.GetOrdinal("referansNo"));
                lblOnayKodu.Text = dr.GetString(dr.GetOrdinal("onayKodu"));
            }

            lblSiparisNo.Text = hdfSiparisNo.Value;
            ModalPopupExtender.Show();
        }
    }
}
