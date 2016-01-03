using System;
using System.Data;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataAccessLayer;
using LoggerLibrary;
using MailLibrary;
using ModelLayer;
using SecureCookie;
using System.Globalization;
using BusinessLayer.Security;
using System.Configuration;

public partial class Kulanici_Kayit : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            KulanicHatirlama();
            yil();
            Gun();
            Sehirler();

            if (Request.Params["yorum"] == "0")
            {
                Mesaj.Info("* Ürün yorumu eklemek için önce üye girişi yapmalısınız.<br/>"+
                           "* Sitemize  üye  değilseniz lütfen üye olunuz.<br/>"+ 
                           "* <a href='SifreHatirlatma.aspx' >Şifrenizi mi unuttunuz?</a>");
            }

            if (Request.Params["izleme"] == "0")
            {
                Mesaj.Info("* Ürün Fiyat Takip eklemek için önce üye girişi yapmalısınız.<br/>"+
                           "* Sitemize  üye  değilseniz lütfen üye olunuz.<br/>"+
                           "* <a href='SifreHatirlatma.aspx' >Şifrenizi mi unuttunuz?</a>");
            }

            if (Request.Params["favori"] == "0")
            {
                Mesaj.Info("* Ürün Favorilere eklemek için önce üye girişi yapmalısınız.<br/>"+
                           "* Sitemize  üye  değilseniz lütfen üye olunuz.<br/>"+
                           "* <a href='SifreHatirlatma.aspx' >Şifrenizi mi unuttunuz?</a>");

            }

            if (Request.Params["sepet"] == "0")
            {
                Mesaj.Info("* Sepettinizi görüntülemek için önce üye girişi yapmalısınız.<br/>"+ 
                           "* Sitemize  üye  değilseniz lütfen üye olunuz.<br/>"+
                           "* <a href='SifreHatirlatma.aspx' > Şifrenizi mi unuttunuz?</a>");
            }

            if (Request.Params["sepeteEkle"] == "0")
            {
                ViewState.Add("urunId", Request.Form["urunId"]);
                ViewState.Add("sagAdet", Request.Form["sagAdet"]);
                ViewState.Add("solAdet", Request.Form["solAdet"]);
                ViewState.Add("sagBilgi", Request.Form["sagBilgi"]);
                ViewState.Add("solBilgi", Request.Form["solBilgi"]);
                ViewState.Add("hediyeId", Request.Form["hediyeId"]);
                ViewState.Add("hediyeBilgi", Request.Form["hediyeBilgi"]);

                Mesaj.Info("* Ürün sepete eklemek için önce üye girişi yapmalısınız.<br/>"+
                           "* Sitemize  üye  değilseniz lütfen üye olunuz.<br/>"+
                           "* <a href='SifreHatirlatma.aspx' >Şifrenizi mi unuttunuz?</a>");
            }
        }
    }

    #region Doğum Tarihi Gün Listeleme
    protected void Gun()
    {
        ddlGun.ClearSelection();
        ListItem item = new ListItem();
        item.Text = "Gün";
        item.Value = "0";
        item.Selected = true;
        ddlGun.Items.Add(item);

        for (int i = 31; i >= 1; i--)
        {
            if (i < 10)
            {
                ddlGun.Items.Insert(1, new ListItem("0" + i.ToString(), "0" + i.ToString()));
            }
            else
            {
                ddlGun.Items.Insert(1, new ListItem(i.ToString(), i.ToString()));
            }
        }
    }
    #endregion

    #region Doğum Tarihi Yıl Listeleme
    protected void yil()
    {
        ddlYil.ClearSelection();
        ListItem item = new ListItem();
        item.Text = "Yıl";
        item.Value = "0";
        item.Selected = true;
        ddlYil.Items.Add(item);

        for (int i = 1930; i <= 2000; i++)
        {
            ddlYil.Items.Insert(1, new ListItem(i.ToString(), i.ToString()));
        }
    }
    #endregion

    public  void Sehirler()
    {
        DataTable dt = SehirDB.Sehirler();

        foreach (DataRow dataRow in dt.Rows)
        {
            ddlSehirler.Items.Add( new ListItem(dataRow["Ad"].ToString(), dataRow["IlID"].ToString()));
        }

        ListItem item = new ListItem();
        item.Text = "-- Şehir Seçiniz --";
        item.Value = "0";
        item.Selected = true;
        ddlSehirler.Items.Add(item);
    }

    #region Üye Kayıt İşlemi
    protected void btnUyeKayit_Click(object sender, EventArgs e)
    {
        Mesaj.SuccessfulHide();
        Mesaj.AlertHide();

        try
        {
            if (Session["randomStr"] == null)
            {
                txtGuvenlik.Text = "";
                Mesaj.Alert("Lütfen güvenlik kodunu doldurup yeniden deneyiniz...");
                return;
            }


            if (Page.IsValid && (txtGuvenlik.Text.ToString() == Session["randomStr"].ToString()))
            {
                
                #region KULANICI GİRİŞ DOĞRULAMA
                StringBuilder denetim = new StringBuilder();
                if (string.IsNullOrWhiteSpace(txtAdSoyad.Text))
                    denetim.Append("* Lütfen Adınızı Yazınız.<br/>");
                if (string.IsNullOrWhiteSpace(txtTelefon.Text))
                    denetim.Append("* Lütfen Telefon Yazınız.<br/>");
                if (string.IsNullOrWhiteSpace(rdbCinsiyet.SelectedValue))
                    denetim.Append("* Lütfen Cinsiyetinizi Seçiniz.<br/>");
                if (string.IsNullOrWhiteSpace(ddlSehirler.SelectedValue))
                    denetim.Append("* Lütfen Şehir Seçiniz.<br/>");
                if (string.IsNullOrWhiteSpace(txtEposta.Text))
                    denetim.Append("* Lütfen E-Posta Alanını doldurunuz.<br/>");
                if (!GenelFonksiyonlar.GecerliMailAdresi(txtEposta.Text))
                    denetim.Append("* Lütfen geçerli mail adresi giriniz.");
                if (string.IsNullOrWhiteSpace(txtSifre.Text))
                    denetim.Append("* Lütfen Şifre Alanını doldurunuz.<br/>");
                if (ddlGun.SelectedValue == "0")
                    denetim.Append("* Lütfen Gün Seçiniz.<br/>");
                if (ddlAy.SelectedValue == "0")
                    denetim.Append("* Lütfen Ay Seçiniz.<br/>");
                if (ddlYil.SelectedValue == "0")
                    denetim.Append("* Lütfen Yıl Seçiniz.<br/>");

                if (denetim.Length > 10)
                {
                    Mesaj.Alert(denetim.ToString());
                    txtGuvenlik.Text = "";
                    return;
                }


                #endregion

                Kullanici kulanici = new Kullanici();
                kulanici.AdiSoyadi = GenelFonksiyonlar.ToTitleCase(txtAdSoyad.Text);
                kulanici.Gsm = txtTelefon.Text;
                kulanici.Cinsiyet = rdbCinsiyet.SelectedValue;
                kulanici.Sehir = ddlSehirler.SelectedValue;
                kulanici.EPosta = txtEposta.Text.ToLower();
                kulanici.Sifre = txtSifre.Text;
                kulanici.KullaniciTipi = "standart";

                string dogumTarihi = string.Format("{0}/{1}/{2}", ddlGun.SelectedValue, ddlAy.SelectedValue, ddlYil.SelectedValue);
                CultureInfo ci = new CultureInfo("en-US");
                kulanici.DogumTarihi = DateTime.ParseExact(dogumTarihi, "dd/MM/yyyy", ci);
                
                int donenKayitId = KullaniciDB.kaydet(kulanici);

                    if (donenKayitId != 0)
                    {
                        #region Kulanıcı Oturum Açma
                        HttpCookie kullanici = new HttpCookie("LensOptikLogin");
                        kullanici.Values.Add("kullaniciId", donenKayitId.ToString());
                        kullanici.Values.Add("kullaniciIsim", txtAdSoyad.Text);
                        Response.Cookies.Add(HttpSecureCookie.Encode(kullanici));
                        #endregion

                        #region Hoşgeldin Maili
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<table cellpadding='0' cellspacing='0' style='width:100%;border:1px solid #d9dada;background-color:#fafafa;margin:20px 0px; font-family:Verdana;' >");
                        sb.Append("<tr>");

                        sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;border-bottom:1px solid #fff;padding-left:15px; padding-top:20px;' >");
                        sb.Append("<p><b>Sayın,&nbsp;" + txtAdSoyad.Text + "</b><br /></p> <p>lensoptik.com.tr Ailesine hoşgeldiniz, Üyeliginiz başarı ile gerçekleştirilmiştir, İyi alışverişler dileriz.</p>");
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;padding-left:15px;' >");
                        sb.Append("<b>Kulanıcı Bilgileriniz </b>");
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;padding-left:15px;' >");
                        sb.Append("<b>Eposta Adresi &nbsp;: </b>" + txtEposta.Text);
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:40px;padding-left:15px;' >");
                        sb.Append("<b>Şifre &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;: </b>" + txtSifre.Text );
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style='vertical-align:middle;font-size:12px;color:#069;height:60px;padding-left:15px;text-decoration:underline;' >");
                        sb.Append("<a style='font-size:12px;color:#069;'  href='http://www.lensoptik.com.tr' target='_blank'> www.lensoptik.com.tr </a>");
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("</table>");

                        MailManager.User.Send(txtEposta.Text, "Kayıt İşleminiz Yapıldı", sb.ToString(), ProcessType.Async);

                        #endregion

                        if (Request.Params["returnUrl"] != null)
                        {
                            if (Request.Params["yorum"] == "0")
                            {
                                Response.Redirect(Request.Params["returnUrl"] + "?kayit=1&yorum=1", false);
                            }
                            else if (Request.Params["izleme"] == "0")
                            {
                                Response.Redirect(Request.Params["returnUrl"] + "?kayit=1&izleme=1", false);
                            }
                            else if (Request.Params["favori"] == "0")
                            {
                                Response.Redirect(Request.Params["returnUrl"] + "?kayit=1&favori=1", false);
                            }
                            else
                            {
                                Response.Redirect("~/Default.aspx?yeniKayit=ok", false);
                            }
                        }
                        else if (Request.QueryString["sepeteEkle"]  != null)
	                    {
                            SepeteUrunEkleSayfayaGit();
	                    }
                        else
                        {
                            Response.Redirect("~/Default.aspx?yeniKayit=ok", false);
                        }
                    }
                    else
                    {
                        Mesaj.Alert("Bu E-Posta Adresi İle Kayıt Mevcut.");
                    }
            }
            else
            {
                Mesaj.Alert("Lütfen Güvenlik Kodunu Giriniz...");
                txtGuvenlik.Text = "";
            }
        }
        catch (ThreadAbortException){}
        catch (Exception hata)
        {
            Mesaj.ErrorSis("<b>Üyelik Kayıt Hatası </b> Lütfen Daha Sonra Tekrar Deneyiniz..");

            HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            StringBuilder sp = new StringBuilder();
            sp.Append("<h6>Clint Info</h6>");
            sp.Append(" BrowserTuru: " + browser.Browser + "<br/>");
            sp.Append(" BrowserAdi: " + browser.Type + "<br/>");
            sp.Append(" BrowserVersiyonu: " + browser.Version + "<br/>");
            sp.Append(" Hata: " + hata.ToString());

            LogManager.SqlDB.Write("Üye Kayıt Hatası", sp.ToString());
        }
    }
    #endregion

    // Kulanıcı Login İşlemleri
    #region Kulanic Giriş Bilgisi Hatırlatma
    public void KulanicHatirlama()
    {
        try
        {
            if (Request.Cookies["_LensOptikHatirla"] != null)
            {
                HttpCookie hatirlaYaz = Request.Cookies["_LensOptikHatirla"];
                txtEpostaGiris.Text = hatirlaYaz["kullaniciPosta"];
            }
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Kulanıcı Hatırlama:", hata);

            if (Request.Cookies["_LensOptikHatirla"] != null)
                Response.Cookies["_LensOptikHatirla"].Expires = DateTime.Now.AddDays(-1);
        }
    }
    #endregion

    #region  Kulanici Oturum Açma
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            Kullanici k;
            k = KullaniciDB.Login(txtEpostaGiris.Text.Trim(), txtSifreGiris.Text.Trim());

            if (k != null)
            {
               
                if (k.KullaniciTipi == "admin")
                {
                    HttpCookie yonetici = new HttpCookie("LensOptikAdminGiris");
                    yonetici.Values.Add("yoneticiId", k.Id.ToString());
                    yonetici.Values.Add("yoneticiIsim", k.AdiSoyadi);
                    Response.Cookies.Add(HttpSecureCookie.Encode(yonetici));
                }

                if (rdbHatirla.SelectedValue == "hatirla")
                {
                    HttpCookie hatirla = new HttpCookie("_LensOptikHatirla");
                    hatirla.Values.Add("kullaniciPosta", k.EPosta);
                    hatirla.Expires = DateTime.Now.AddDays(60);
                    Response.Cookies.Add(hatirla);
                }
                else if (rdbHatirla.SelectedValue == "unut")
                {
                    if (Request.Cookies["_LensOptikHatirla"] != null)
                        Response.Cookies["_LensOptikHatirla"].Expires = DateTime.Now.AddDays(-1);
                }

                HttpCookie kullanici = new HttpCookie("LensOptikLogin");
                kullanici.Values.Add("kullaniciId", k.Id.ToString());
                kullanici.Values.Add("kullaniciIsim", k.AdiSoyadi);
                Response.Cookies.Add(HttpSecureCookie.Encode(kullanici));

                //LogManager.Event.Write("Giriş Tamam:",
                //    " Mail: " + txtEpostaGiris.Text + " ||  Şifre : " + txtSifreGiris.Text);

                if (k.SiparisAdet > 0)
                {
                    HttpCookie popUp = new HttpCookie("SiparisAdetPopUp");
                    popUp.Values.Add("UserId", k.Id.ToString());
                    Response.Cookies.Add(popUp);
                }

                if (Request.Params["returnUrl"] != null)
                {
                    Response.Redirect(Request.Params["returnUrl"] + "?user=ok", false);
                }
                else if (Request.Params["sepet"] == "0")
                {
                    Response.Redirect(string.Format("{0}/Market/Sepet.aspx?user=ok", ConfigurationManager.AppSettings["sslSitePath"]), false);
                    
                }
                else if (Request.Params["sepeteEkle"] == "0")
                {
                    SepeteUrunEkleSayfayaGit();
                }
                else
                {
                    Response.Redirect("~/default?user=ok", false);
                }
            }
            else
            {
                if (Request.Cookies["_LensOptikHatirla"] != null)
                {
                    Response.Cookies["_LensOptikHatirla"].Expires = DateTime.Now.AddDays(-1);
                }

                //LogManager.Event.Write("Hatalı Giriş:",
                //    " Mail: " + txtEpostaGiris.Text + " ||  Şifre : " + txtSifreGiris.Text);

                ScriptManager.RegisterStartupScript(Page, typeof(Page),
                    "kulaniciHata", " alert('* Lütfen Bilgilerinizi Kontrol Ediniz');", true);

            }
        }
        catch (ThreadAbortException){ }
        catch (Exception ex)
        {
            LoggerLibrary.LogManager.SqlDB.Write("Kullanıcı Giriş:", ex);
            Mesaj.Alert("Hata oluştu Lütfen tekrar deneyiniz.");
        }
    }
    #endregion

    private void SepeteUrunEkleSayfayaGit()
    {
        RemotePost urlPost = new RemotePost();
        urlPost.Url = ResolveUrl( string.Format("{0}/Market/Sepet.aspx?user=ok", ConfigurationManager.AppSettings["sslSitePath"]));
        urlPost.Add("urunId", ViewState["urunId"]);
        urlPost.Add("sagAdet", ViewState["sagAdet"]);
        urlPost.Add("solAdet", ViewState["solAdet"]);
        urlPost.Add("sagBilgi", ViewState["sagBilgi"]);
        urlPost.Add("solBilgi", ViewState["solBilgi"]);
        urlPost.Add("hediyeId", ViewState["hediyeId"]);
        urlPost.Add("hediyeBilgi", ViewState["hediyeBilgi"]);

        urlPost.Post();
    }
}