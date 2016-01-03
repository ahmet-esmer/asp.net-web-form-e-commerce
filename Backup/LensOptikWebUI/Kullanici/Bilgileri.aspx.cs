using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessLayer.Security;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using SecureCookie;

public partial class Kullanici_Bilgi : System.Web.UI.Page
{
    public int uyeId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoginKontrol();

        if (!IsPostBack)
        {
            yil();
            Gun();
            Sehirler();
            kulaniciBilgiGetir(uyeId);
        }
    }

    #region Login Kontrol
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
    #endregion

    #region Güncelleme İçin Bilgi Getirme
    protected void kulaniciBilgiGetir(int uyeId)
    {
        try
        {
            Kullanici k = KullaniciDB.Getir(uyeId);
            txtAdSoyad.Text = k.AdiSoyadi;
            txtEposta.Text = k.EPosta;
            txtSifre.Attributes.Add("value", k.Sifre);
            txtTelefon.Text = k.Gsm;
            ddlSehirler.SelectedValue = k.Sehir;
            ckbEposta.Checked = k.PostaGonder;
            ckbSms.Checked = k.SMSGonder;

            for (int i = 0; i < 2; i++)
            {
                if (rdbCinsiyet.Items[i].Value == k.Cinsiyet.ToString())
                {
                    rdbCinsiyet.Items[i].Selected = true;
                }
            }

            string gun = null;
            if (k.DogumTarihi.Month < 10)
            {
                gun = "0" + k.DogumTarihi.Day.ToString();
            }
            else
            {
                gun = k.DogumTarihi.Day.ToString();
            }

            for (int i = 0; i < ddlGun.Items.Count; i++)
            {
                if (ddlGun.Items[i].Value == gun)
                {
                    ddlGun.Items[i].Selected = true;
                }
            }

            string ay = null;
            if (k.DogumTarihi.Month < 10)
            {
                ay = "0" + k.DogumTarihi.Month.ToString();
            }
            else
            {
                ay = k.DogumTarihi.Month.ToString();
            }

            for (int i = 0; i < ddlAy.Items.Count; i++)
            {
                if (ddlAy.Items[i].Value == ay)
                {
                    ddlAy.Items[i].Selected = true;

                }
            }

            for (int i = 0; i < ddlYil.Items.Count; i++)
            {
                if (ddlYil.Items[i].Value == k.DogumTarihi.Year.ToString())
                {
                    ddlYil.Items[i].Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            Mesaj.ErrorSis("Hata oluştu lütfen daha sonra tekrar deneyiniz.");
            LogManager.SqlDB.Write("Güncelleme İçin Üyelik Bigisi Getirme", ex);
        }
    }
    #endregion

    #region Doğum Tarihi Gün Listeleme
    protected void Gun()
    {
        ListItem item = new ListItem();
        for (int i = 31; i >= 1; i--)
        {
            if (i < 10)
            {
                ddlGun.Items.Insert(0, new ListItem("0" + i.ToString(), "0" + i.ToString()));
            }
            else
            {
                ddlGun.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
            }
        }
    }
    #endregion

    #region Doğum Tarihi Yıl Listeleme
    protected void yil()
    {
        ListItem item = new ListItem();
        ddlYil.Items.Add(item);

        for (int i = 1930; i <= 2011; i++)
        {
            ddlYil.Items.Insert(1, new ListItem(i.ToString(), i.ToString()));
        }
    }
    #endregion

    #region Şehirler
    public void Sehirler()
    {
        DataTable dt = SehirDB.Sehirler();

        foreach (DataRow dataRow in dt.Rows)
        {
            ddlSehirler.Items.Add(new ListItem(dataRow["Ad"].ToString(), dataRow["IlID"].ToString()));
        }
    }
    #endregion

    #region Üye Bilgilerini Güncelle
    protected void btnUyeBilgiGuncelle_Click(object sender, EventArgs e)
    {
        try
        {
            string uyeAdSoyad = txtAdSoyad.Text;
            string ePosta = txtEposta.Text;
    
            if (string.IsNullOrWhiteSpace(ddlYil.SelectedValue))
            {
                Mesaj.Alert("Lütfen Yıl seçiniz.");
                return;
            }

            Kullanici kulanici = new Kullanici();
            kulanici.Id = uyeId;
            kulanici.AdiSoyadi = uyeAdSoyad;
            kulanici.Gsm = GuvenlikIslemleri.hackKontrol(txtTelefon.Text);
            kulanici.Cinsiyet = rdbCinsiyet.SelectedValue;
            kulanici.Sehir = ddlSehirler.SelectedValue;
            kulanici.EPosta = ePosta;
            kulanici.Sifre = txtSifre.Text;
            kulanici.DogumTarihi = Convert.ToDateTime(string.Format("{0}.{1}.{2}", ddlGun.SelectedValue, ddlAy.SelectedValue, ddlYil.SelectedValue));
            kulanici.PostaGonder = ckbEposta.Checked;
            kulanici.SMSGonder = ckbSms.Checked;

            int donendeger = KullaniciDB.kaydet(kulanici);

            if (donendeger == 0)
            {
                if (Request.Cookies["LensOptikHatirla"] != null)
                {
                    HttpCookie kullaniciBilgi = new HttpCookie("LensOptikHatirla");
                    kullaniciBilgi.Values.Add("kullaniciPosta", ePosta);
                    kullaniciBilgi.Expires = DateTime.Now.AddDays(60);
                    Response.Cookies.Add(HttpSecureCookie.Encode(kullaniciBilgi));
                }

                HttpCookie kullanici = new HttpCookie("LensOptikLogin");
                kullanici.Values.Add("kullaniciId", uyeId.ToString());
                kullanici.Values.Add("kullaniciIsim", uyeAdSoyad);
                Response.Cookies.Add(HttpSecureCookie.Encode(kullanici));

                Mesaj.Successful(" Degişiklikler başarı ile kaydedildi.");
            }
            else
            {
                Mesaj.Alert("* Bu E-posta adresi daha önce bir kulanıcı hesabı oluşturulmuş.<br/>* Lütfen farklı bir E-Posata adresi giriniz. <br/> * Eger <b>" + ePosta + "</b> adresi size aitse şifre hatırlatma talabinde bulununuz.  ");
            }
        }
        catch (Exception ex)
        {
            Mesaj.ErrorSis("<b>Üyelik Bilgileri Güncelleme Hatası </b> Lütfen Daha Sonra Tekrar Deneyiniz..");
            LogManager.SqlDB.Write("Üyelik Bilgileri Güncelleme Hatası", ex);
        }
    }
    #endregion
}