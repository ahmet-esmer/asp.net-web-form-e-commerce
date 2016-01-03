using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using BusinessLayer.BasePage;
using BusinessLayer.PagingLink;
using DataAccessLayer;
using ModelLayer;

public partial class Admin_Uyeler : BasePageAdmin
{
    private List<Kullanici> kullanici;
    private int sayfaNo, Baslangic, Bitis;
    private int sayfaGosterim = 15;


    protected void Page_Load(object sender, EventArgs e)
    {
        CreateLink();

        if (!IsPostBack)
        {
            if (Request.QueryString["Sayfa"] != null)
            {
                sayfaNo = Convert.ToInt32(Request.QueryString["Sayfa"]);
            }
            else
            {
                sayfaNo = 0;
            }

            Baslangic = (sayfaNo * sayfaGosterim) + 1;
            Bitis = Baslangic + sayfaGosterim - 1;

            if (Request.Params["islem"] == "genel")
            {
                uyeListele();
            }

            if (Request.Params["myLetter"] != null)
            {
                AlfabeListele();
            }

            if (Request.Params["islem"] == "sil")
            {
                uyeSil(Convert.ToInt32(Request.Params["id"]));
                Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString());
            }
     
            if (Request.Params["islem"] == "durum")
            {
                aktifPasif();
                Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString());
            }

            if (Request.QueryString["kullaniciId"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["kullaniciId"]);
                UyeGetirId(id);
            }
        }
    }
    private void UyeGetirId(int id)
    {

        ltlSayfalama.Visible = false;

        kullanici = new List<Kullanici>();
        kullanici.Add(KullaniciDB.Getir(id));

        grwUyeler.DataSource = kullanici;
        grwUyeler.DataBind();
    }

    #region Üye Arama Ada Göre
    protected void txt_UyeAdi_TextChanged(object sender, EventArgs e)
    {
        ltlSayfalama.Visible = false;
        kullanici = KullaniciDB.Arama(txt_UyeAdi.Text, "uyeArama");

        grwUyeler.DataSource = kullanici;
        grwUyeler.DataBind();

        if (kullanici.Count == 0)
        {
            mesajGosterNo(string.Format("<b>{0}</b> aramasına ait üye bulunamadı.", txt_UyeAdi.Text));
        }
        else
        {
            mesajGosterInfo(string.Format("<b>{0}</b> aramasına ait toplam <b>{1}</b> üye bulundu.", txt_UyeAdi.Text, kullanici.Count));
        }
    }
    #endregion

    #region Tarih Aralığı Üye Arama işlemi
    protected void btnUyeTarihAra_Click(object sender, EventArgs e)
    {
        try
        {
            ltlSayfalama.Visible = false;
            kullanici = KullaniciDB.TarihArama(txtTarih_1.Text, txtTarih_2.Text);

            grwUyeler.DataSource = kullanici;
            grwUyeler.DataBind();

            if (kullanici.Count == 0)
            {
                mesajGosterNo(string.Format("<b>{0}  {1}</b> Tarih aralığına ait kayıt bulunamadı.", txtTarih_1.Text, txtTarih_2.Text));
            }
            else
            {
                mesajGosterInfo(string.Format("<b>{0}  {1}</b> Tarih aralığınada kayıt olan toplam <b> {2} </b> üye bulundu.", txtTarih_1.Text, txtTarih_2.Text, kullanici.Count));
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Arama Hatası:", ex);
        }
    }
    #endregion

    #region Üye Alfa Listeleme
    protected void CreateLink()
    {
        string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K","L", "M", "N", "O", "P", "R", "S", "T", "U", "V", "Y", "Z"};
        for (int i = 0; i < letters.Length; i++)
        {
            HyperLink myLetter = new HyperLink();
            myLetter.ID = myLetter + i.ToString();
            myLetter.NavigateUrl = "?myLetter=" + letters[i].ToString();
            myLetter.CssClass = "letters";
            myLetter.Text = letters[i].ToString() + " ";

            PagingPanel.Controls.Add(myLetter);
        }
    }
    #endregion

    #region Üye Listeleme Bütün Üyeler 
    private void uyeListele()
    {
        SqlParameter[] prm = new SqlParameter[] 
          { 
             new SqlParameter("@Baslangic", Baslangic),
             new SqlParameter("@Bitis", Bitis)
          };

        ltlSayfalama.Text = PagingLink.GetHtmlCode(Request.QueryString, sayfaGosterim, KullaniciDB.ItemCount());

        using (SqlDataReader dr = SqlHelper.ExecuteReader("kullanici_KayitListeleAdmin", prm))
        {
            kullanici = new List<Kullanici>();
            while (dr.Read())
            {
                kullanici.Add(new Kullanici
                {
                    Id = dr.GetInt32(dr.GetOrdinal("id")),
                    AdiSoyadi = dr.GetString(dr.GetOrdinal("adiSoyadi")),
                    Gsm = dr.GetString(dr.GetOrdinal("Gsm")),
                    DogumTarihi = dr.GetDateTime(dr.GetOrdinal("dogumTarihi")),
                    EPosta = dr.GetString(dr.GetOrdinal("ePosta")),
                    Durum = dr.GetBoolean(dr.GetOrdinal("durum")),
                    KayitTarihi = dr.GetDateTime(dr.GetOrdinal("kayitTarihi")),
                    GirisSayisi = dr.GetInt32(dr.GetOrdinal("girisSayisi")),
                    Cinsiyet = dr.GetString(dr.GetOrdinal("cinsiyet")),
                    Sehir = dr.GetString(dr.GetOrdinal("sehir")),
                    KullaniciTipi = dr.GetString(dr.GetOrdinal("kullaniciTipi")),
                    Sifre = dr.GetString(dr.GetOrdinal("sifre"))
                });
            }

            grwUyeler.DataSource = kullanici;
            grwUyeler.DataBind();
        }
    }
    #endregion

    #region Üye Listeleme  Alfabeye Göre
    private void AlfabeListele()
    {
        try
        {
            ltlSayfalama.Visible = false;
            kullanici = KullaniciDB.Arama(Request.Params["myLetter"], "Letter");

            grwUyeler.DataSource = kullanici;
            grwUyeler.DataBind();

            if (kullanici.Count == 0)
            {
                mesajGosterNo(string.Format("<b>{0}</b> Aramasına Ait Üye Bulunamadı.", txt_UyeAdi.Text));
            }
            else
            {
                mesajGosterInfo(string.Format("<b>{0}</b> Harfi ile başlayan toplam <b>{1}</b> Üye Bulundu.", Request.Params["myLetter"], kullanici.Count));
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Arama Hatası:", ex);
        }
    }
    #endregion

    #region Üye Durum İşlemleri
    private void aktifPasif()
    {
        try
        {
            KullaniciDB.Durum(Convert.ToInt32(Request.Params["id"]), Convert.ToInt32(Request.Params["durum"]));
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>Durum Degiştirme Hatası:</b>", ex);
        }
    }
    #endregion

    #region Üye Silme İşlemi
    private void uyeSil(int id)
    {
        try
        {
            KullaniciDB.Sil(id);
        }
        catch (Exception ex)
        {
            mesajGosterSis("Üye Silme Hatası:", ex);
        }
    }

    #endregion

}
