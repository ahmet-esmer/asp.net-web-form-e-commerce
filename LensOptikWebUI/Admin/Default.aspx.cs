using System;
using System.Text;
using System.Web;
using BusinessLayer.BasePage;
using DataAccessLayer;
using LoggerLibrary;
using MailLibrary;
using ModelLayer;
using SecureCookie;
using BusinessLayer;


public partial class AdminGiris : BasePageAdmin
{
    private Kullanici admin;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            YoneticiHatirlama();
        }
    }

    private void YoneticiHatirlama()
    {
        try
        {
            if (Request.Cookies["LensOptikAdminBilgi"] != null)
            {
                HttpCookie hatirla = Request.Cookies["LensOptikAdminBilgi"];
                hatirla = HttpSecureCookie.Decode(hatirla);
                txtEposta.Text = hatirla["posta"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Request.Cookies["LensOptikAdminBilgi"] != null)
            {
                Response.Cookies["LensOptikAdminBilgi"].Expires = DateTime.Now.AddDays(-1);
            }

            lblMesaj.Visible = true;
            lblMesaj.Text = "* Lütfen Bilgilerinizi Kontrol Ediniz"+ ex.ToString();
        }
    }

    protected void btnOturumAc_Click(object sender, EventArgs e)
    {
       
        admin = KullaniciDB.Login(txtEposta.Text, txtsifre.Text);
        
        if (admin != null)
        {
            if (admin.KullaniciTipi != "admin")
            {
                lblMesaj.Visible = true;
                lblMesaj.Text = "* Yalnızca Admin kullanıcısı giriş yapabilir.";
                return;
            }

            if (rbtHatirla.SelectedValue == "hatirla")
            {
                HttpCookie hatirla = new HttpCookie("LensOptikAdminBilgi");
                hatirla.Values.Add("posta", admin.EPosta);
                hatirla.Expires = DateTime.Now.AddDays(120);
                Response.Cookies.Add(HttpSecureCookie.Encode(hatirla));
            }
            else if (rbtHatirla.SelectedValue == "unut")
            {
                if (Request.Cookies["LensOptikAdminBilgi"] != null)
                    Response.Cookies["LensOptikAdminBilgi"].Expires = DateTime.Now.AddDays(-1);
            }

            HttpCookie kullanici = new HttpCookie("LensOptikLogin");
            kullanici.Values.Add("kullaniciId", admin.Id.ToString());
            kullanici.Values.Add("kullaniciIsim", admin.AdiSoyadi);
            Response.Cookies.Add(HttpSecureCookie.Encode(kullanici));

            HttpCookie yonetici = new HttpCookie("LensOptikAdminGiris");
            yonetici.Values.Add("yoneticiId", admin.Id.ToString());
            yonetici.Values.Add("yoneticiIsim", admin.AdiSoyadi);
            Response.Cookies.Add(HttpSecureCookie.Encode(yonetici));
            Response.Redirect("yonetici_arayuz.aspx");

        }
        else
        {
            if (Request.Cookies["LensOptikAdminBilgi"] != null)
                Response.Cookies["LensOptikAdminBilgi"].Expires = DateTime.Now.AddDays(-1);

            LogManager.Event.Write("Admin Hatalı Giriş:",
                    " Kullanıcı Adı: " + txtEposta.Text + " ||  Şifre : " + txtsifre.Text);

            lblMesaj.Visible = true;
            lblMesaj.Text = "* Lütfen Bilgilerinizi Kontrol Ediniz";
        }
    }

    #region Admin Şifre Hatırlatma İşlemi
    protected void btnSifreGonder_Click(object sender, EventArgs e)
    {
        try
        {
            string mail = txtSifreGonder.Text;

            if (!GenelFonksiyonlar.GecerliMailAdresi(mail))
            {
                lblMesaj.Visible = true;
                lblMesaj.Text = "* Lütfen geçerli mail adresi giriniz";
                return;
            }

            admin = KullaniciDB.AdminSifreHatirlatma(mail);

            if (admin != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<h3>lensoptik.com.tr  Yönetici Bilgileri</h3>");
                sb.Append("<b>İsim Soyisim   : </b>" + admin.AdiSoyadi + "<br>");
                sb.Append("<b>E-Posta Adresi : </b>" + admin.EPosta + "<br>");
                sb.Append("<b>Şifre          : </b>" + admin.Sifre + "<br>");
                sb.Append("<a href='http://www.lensoptik.com.tr/Admin/' target='_blank' >lensoptik.com.tr</a>");

                MailManager.User.Send(admin.EPosta, "Yönetici Şifre Hatırlatma", sb.ToString());

                lblMesaj.Visible = true;
                lblMesaj.Text = "* Şifreniz E-Posta Adresine Gönderildi.";
            }
            else
            {
                lblMesaj.Visible = true;
                lblMesaj.Text = "* Sisteme Kayıtlı E-Posta Adresi Bulunamadı.";
                LogManager.Text.Write("Yönetici paneli sisteme kayıtlı E-Posta adresi bulunamadı.", mail);
            }   
        }
        catch (Exception ex)
        {
            lblMesaj.Visible = true;
            lblMesaj.Text = "Hata oluştu lütfen daha sonra tekrar deneyiniz.";
            LogManager.SqlDB.Write("Yönetici Paneli şifre hatırlatma" , ex);
        }
    }
    #endregion
}
