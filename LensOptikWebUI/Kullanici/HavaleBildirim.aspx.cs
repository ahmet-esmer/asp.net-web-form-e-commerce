using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using LoggerLibrary;
using MailLibrary;
using ModelLayer;
using DataAccessLayer;


public partial class Kulanici_HavaleBildirim : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BankaHesapAdiListele();
        }
    }


    #region Havale Bildirim Formu
    protected void btnHavaleBildirim_Click(object sender, EventArgs e)
    {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<table cellpadding='0' cellspacing='0' style='width:100%;border:1px solid #d9dada;background-color:#fafafa;margin:20px 0px; font-family:Verdana;' >");

                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='vertical-align:middle;font-size:12px;color:#292929;height:40px;border-bottom:1px solid #fff;padding-left:15px; padding-top:20px;border-bottom:1px solid #d9dada;' >");
                sb.Append("<h1>Havale Bildirim Formu</h1>");
                sb.Append("</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='width:200px;vertical-align:middle;font-size:12px;color:#292929;height:40px;padding-left:15px;' >");
                sb.Append("<b>Gönderen</b>");
                sb.Append("</td>");
                sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;padding-left:15px;' >");
                sb.Append(": " + txtAdSoyad.Text.ToString());
                sb.Append("</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;padding-left:15px;' >");
                sb.Append("<b>E- Posta</b>");
                sb.Append("</td>");
                sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;padding-left:15px;' >");
                sb.Append(": " + txtEposta.Text.ToString());
                sb.Append("</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;padding-left:15px;' >");
                sb.Append("<b>Havale Yaptığınız Miktar</b>");
                sb.Append("</td>");
                sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;padding-left:15px;' >");
                sb.Append(": " + txtHavaleMiktari.Text.ToString());
                sb.Append("</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;padding-left:15px;' >");
                sb.Append("<b>Sipariş No</b>");
                sb.Append("</td>");
                sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;padding-left:15px;' >");
                sb.Append(": " + txtSiparisNo.Text.ToString());
                sb.Append("</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;padding-left:15px;' >");
                sb.Append("<b>Havalenin Yapıldığı Banka</b>");
                sb.Append("</td>");
                sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;padding-left:15px;' >");
                sb.Append(": " + rdbBankaHavale.SelectedItem.ToString());
                sb.Append("</td>");
                sb.Append("</tr>");


                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='vertical-align:middle;font-size:12px;color:#069;height:60px;padding-left:15px;text-decoration:underline;' >");
                sb.Append("<a style='font-size:12px;color:#069;'  href='http://www.lensoptik.com.tr' target='_blank'> www.lensoptik.com.tr </a>");
                sb.Append("</td>");
                sb.Append("</tr>");

                sb.Append("</table>");

                MailManager.Admin.Send("Havale Formu", sb.ToString());

                Mesaj.Successful("Havale Birildiriminiz Yöneticiye Gönderilmiştir.");

                txtAdSoyad.Text = "";
                txtEposta.Text = "";
                txtSiparisNo.Text = "";
                txtHavaleMiktari.Text = "";

            }
            catch (Exception hata)
            {
                Mesaj.ErrorSis("Havale Formu Gönderme Hatası.");
                LogManager.SqlDB.Write("Havale Formu Gönderme Hatası.", hata);
            }
       
    }
    #endregion

    #region Banka Hesap Bilgisi  Listele
    private void BankaHesapAdiListele()
    {
        try
        {
            rdbBankaHavale.DataSource = BankaHesaplariDB.HesapListe("web");
            rdbBankaHavale.DataTextField = "BankaAdi";
            rdbBankaHavale.DataValueField = "Id";
            rdbBankaHavale.DataBind();
        }
        catch (Exception hata)
        {
            Mesaj.ErrorSis("Listeleme Hatası");
            LogManager.SqlDB.Write("Banka Hesaplari Listelerken hata oluştu.", hata);
        }
    }
    #endregion

}