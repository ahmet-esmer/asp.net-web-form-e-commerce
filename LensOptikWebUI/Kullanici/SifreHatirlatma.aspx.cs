using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DataAccessLayer;
using LoggerLibrary;
using MailLibrary;
using BusinessLayer;

public partial class Kulanici_SifreGonder : System.Web.UI.Page
{
  
  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["post"] != null)
            {
                txtSifreGonder.Text = Request.Params["post"].ToString();
            }
        }
    }

    protected void btnSifreGonder_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["randomStr"] == null)
            {
                txtGuvenlik.Text = "";
                Mesaj.Alert("Lütfen güvenlik kodunu doldurup yeniden deneyiniz...");
                return;
            }


            string AdSoyad = "";
            string sifre = "";
            string eposta = txtSifreGonder.Text.Trim();

            if (!GenelFonksiyonlar.GecerliMailAdresi(eposta))
            {
                Mesaj.Alert("Lütfen geçerli mail adresi giriniz.");
                return;
            }

            SqlParameter prm = new SqlParameter("@eposta", eposta);

            if (txtGuvenlik.Text.ToString() == Session["randomStr"].ToString())
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader("kullanici_WebSifre", prm))
                {
                    while (dr.Read())
                    {
                        eposta = dr.GetString(dr.GetOrdinal("ePosta")).ToString();
                        AdSoyad = dr.GetString(dr.GetOrdinal("adiSoyadi")).ToString();
                        sifre = dr.GetString(dr.GetOrdinal("sifre")).ToString();
                    }

                    if (dr.HasRows)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append("<table cellpadding='0' cellspacing='0' style='width:100%;border:1px solid #d9dada;background-color:#fafafa;margin:20px 0px; font-family:Verdana;' >");

                        sb.Append("<tr>");
                        sb.Append("<td colspan='2' style='vertical-align:middle;font-size:12px;color:#292929;height:40px;border-bottom:1px solid #fff;padding-left:15px; padding-top:20px;' >");
                        sb.Append("<h1> lensoptik.com.tr Kulanıcı Bilgileri</h1>");
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;border-bottom:1px solid #fff;padding-left:15px; padding-top:20px;' >");
                        sb.Append("İsim Soyisim ");
                        sb.Append("</td>");
                        sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;border-bottom:1px solid #fff;padding-left:15px; padding-top:20px;' >");
                        sb.Append(AdSoyad);
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;border-bottom:1px solid #fff;padding-left:15px; padding-top:20px;' >");
                        sb.Append("E-Posta Adresi");
                        sb.Append("</td>");
                        sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;border-bottom:1px solid #fff;padding-left:15px; padding-top:20px;' >");
                        sb.Append(eposta);
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;border-bottom:1px solid #fff;padding-left:15px; padding-top:20px;' >");
                        sb.Append("Şifre");
                        sb.Append("</td>");
                        sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;border-bottom:1px solid #fff;padding-left:15px; padding-top:20px;' >");
                        sb.Append(sifre);
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td colspan='2' style='vertical-align:middle;font-size:12px;color:#069;height:60px;padding-left:15px;text-decoration:underline;' >");
                        sb.Append("<a style='font-size:12px;color:#069;'  href='http://www.lensoptik.com.tr' target='_blank'> www.lensoptik.com.tr </a>");
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("</table>");
                        MailManager.User.Send(eposta, "Şifre Hatırlatma", sb.ToString());
                        Mesaj.AlertHide();
                        Mesaj.Successful(" Şifreniz E-Posta Adresine Gönderildi.");
                    }
                    else
                    {
                        Mesaj.Alert("Sisteme Kayıtlı E-Posta Adresi Bulunamadı.");
                        txtGuvenlik.Text = "";
                    }
                }
            }
            else
            {
                Mesaj.Alert("Lütfen Güvenlik Kodunu Giriniz...");
                txtGuvenlik.Text = "";
            }
        }
        catch (Exception hata)
        {
            Mesaj.Alert("Şifre Hatırlatma Hatası..");
            LogManager.SqlDB.Write("Şifre Hatırlatma Hatası", hata);
        }
    }
}