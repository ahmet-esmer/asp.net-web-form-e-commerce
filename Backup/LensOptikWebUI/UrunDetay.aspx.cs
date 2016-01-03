using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataAccessLayer;
using LoggerLibrary;
using MailLibrary;
using ModelLayer;
using ServiceLayer.ExtensionMethods;


public partial class urun_detay : Page
{
    private int urunId = 0;
    private decimal fiyatToplam = 0;
    private int uyeId = 0;
    private string uyeAdi = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoginKontrol();

        if (!IsPostBack)
        {
            urunId = ServiceLayer.RouteDataGet.Value<int>("urunId");

            if (urunId > 0)
            {
                hdfUrunId.Value = urunId.ToString();
                UrunResimleriGetir();
                UrunGoruntuleme();
                urunGetir();
                UrunSecenekleri();
                urunTavsiyeListele();
                KategoriMevcutSayfa();
                MarkaListeleme();
                HediyeUrun();
                QueryStringMesajlari();
                KampanyaBilgiListe();
                IndirimTablo();
            }
            else
            {
                Response.Redirect("~/");
            }
        }
    }

    private void QueryStringMesajlari()
    {
        if (Request.QueryString["kayit"] == "1")
        {
            ucMesaj.Successful(" Üyelik İşleminiz Başarı ile Gerçekleşti.");
        }
        else if (Request.QueryString["hata"] != null)
        {
            ucMesaj.ErrorSis("<b>Hata Oluştu</b>: Lütfen Daha Sonra Tekrar Deneyiniz..");
        }
        else if (Request.QueryString["hataResim"] != null)
        {
            ucMesaj.ErrorSis("Lütfen Tekrar Deneyiniz..");
        }
        else if (Request.QueryString["tavsiye"] != null)
        {
            ucMesaj.Successful("Ürün tavsiyeniz başarı ile gönderildi");
        }
        else if (Request.QueryString["yorum"] != null)
        {
            if (Request.QueryString["yorum"] == "0")
                ucMesaj.Successful("Ürün yorumunuz yöneticiye iletilmiştir yöneticinin onayından sonra yayınlanacaktır");
            else
                ucMesaj.Alert("Bu Yorumu Daha Önce Gönderdiniz");
        }
    }

    private void LoginKontrol()
    {
        if (KullaniciOperasyon.LoginKontrol())
        {
            uyeId = KullaniciOperasyon.GetId();
            uyeAdi = KullaniciOperasyon.GetName();
            hdfUyeId.Value = "1"; 
            pnlUrunYorumFormu.Visible = true;
        }
    }

    private void UrunSecenekleri()
    {
        string[] secenekDeger;

        List<string> Aks = new List<string>();
        List<string> Bc = new List<string>();
        List<string> Dioptri = new List<string>();
        List<string> Renk = new List<string>();
        List<string> Silindirik = new List<string>();
        List<string> Dia = new List<string>();

        ListItem ddlSec = new ListItem();
        ddlSec.Text = "Seçiniz";
        ddlSec.Value = "0";
        ddlSec.Selected = true;

        string[] secenekler = UrunDB.urunSecenekGetir(urunId);

        if (secenekler != null && secenekler.Length > 1 )
        {
            foreach (var item in secenekler)
            {
                secenekDeger = item.Split(':');

                if (secenekDeger[0] == "Aks")
                {
                    Aks.Add(secenekDeger[1]);
                }
                else if (secenekDeger[0] == "Bc")
                {
                    Bc.Add(secenekDeger[1]);
                }
                else if (secenekDeger[0] == "Dioptri")
                {
                    Dioptri.Add(secenekDeger[1]);
                }
                else if (secenekDeger[0] == "Renk")
                {
                    Renk.Add(secenekDeger[1]);
                }
                else if (secenekDeger[0] == "Silindirik")
                {
                    Silindirik.Add(secenekDeger[1]);
                }
                else if (secenekDeger[0] == "Dia")
                {
                    Dia.Add(secenekDeger[1]);
                }
            }

            if (Aks.Count() > 0)
            {
                lblAks.Visible = true;
                ddlAks.DataSource = Aks;
                ddlAks.DataBind();
                ddlAks.Visible = true;
                ddlAks.Items.Add(ddlSec);

                ddlAks1.DataSource = Aks;
                ddlAks1.DataBind();
                ddlAks1.Visible = true;
                ddlAks1.Items.Add(ddlSec);
            }

            if (Bc.Count() > 0)
            {
                lblBc.Visible = true;
                ddlBc.DataSource = Bc;
                ddlBc.DataBind();
                ddlBc.Visible = true;
                ddlBc.Items.Add(ddlSec);

                ddlBc1.DataSource = Bc;
                ddlBc1.DataBind();
                ddlBc1.Visible = true;
                ddlBc1.Items.Add(ddlSec);
            }

            if (Dioptri.Count() > 0)
            {
                lblDioptri.Visible = true;
                ddlDioptri.DataSource = Dioptri;
                ddlDioptri.DataBind();
                ddlDioptri.Visible = true;
                ddlDioptri.Items.Add(ddlSec);

                ddlDioptri1.DataSource = Dioptri;
                ddlDioptri1.DataBind();
                ddlDioptri1.Visible = true;
                ddlDioptri1.Items.Add(ddlSec);
            }

            if (Renk.Count() > 0)
            {
                lblRenk.Visible = true;
                ddlRenk.DataSource = Renk;
                ddlRenk.DataBind();
                ddlRenk.Visible = true;
                ddlRenk.Items.Add(ddlSec);

                ddlRenk1.DataSource = Renk;
                ddlRenk1.DataBind();
                ddlRenk1.Visible = true;
                ddlRenk1.Items.Add(ddlSec);
            }

            if (Silindirik.Count() > 0)
            {
                lblSilindirik.Visible = true;
                ddlSilindirik.DataSource = Silindirik;
                ddlSilindirik.DataBind();
                ddlSilindirik.Visible = true;
                ddlSilindirik.Items.Add(ddlSec);

                ddlSilindirik1.DataSource = Silindirik;
                ddlSilindirik1.DataBind();
                ddlSilindirik1.Visible = true;
                ddlSilindirik1.Items.Add(ddlSec);

            }

            if (Dia.Count() > 0)
            {
                lblDia.Visible = true;
                ddlDia.DataSource = Dia;
                ddlDia.DataBind();
                ddlDia.Visible = true;
                ddlDia.Items.Add(ddlSec);

                ddlDia1.DataSource = Dia;
                ddlDia1.DataBind();
                ddlDia1.Visible = true;
                ddlDia1.Items.Add(ddlSec);
            }
        }
        else
        {
            ddlAdet1.Visible = false;
            lblSolText.Visible = false;
            lblSagText.Text = "Ürün Adet:";
            lblAdet.Visible = false;
            trSenekInfo.Visible = false;
        }
    }

    #region Ürün hit işlemi
    public void UrunGoruntuleme()
    {
        try
        {
            DataTable dt = new DataTable();

            if (Session["hit"] != null)
            {
                dt = (DataTable)Session["hit"];
            }
            else
            {
                dt.Columns.Add("id");
            }

            bool varmi = Kontrol(urunId.ToString());

            if (varmi == false)
            {
                DataRow drow = dt.NewRow();
                drow["id"] = urunId.ToString();
                dt.Rows.Add(drow);
                SqlHelper.ExecuteNonQuery("urun_Goruntuleme",  new SqlParameter("@id", urunId));
            }

            HttpContext.Current.Session["hit"] = dt;
        }
        catch (Exception hata)
        {
            LoggerLibrary.LogManager.SqlDB.Write("Ürün puanlama hatası", hata);
        }
    }
    #endregion

    #region Ürün hit kontrol
    private bool Kontrol(string id)
    {
        bool r = false;
        DataTable dt = new DataTable();

        if (HttpContext.Current.Session["hit"] != null)
        {
            dt = (DataTable)HttpContext.Current.Session["hit"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["id"].ToString() == id)
                {
                    r = true;
                    break;
                }
            }
        }
        return r;
    }
    #endregion

    #region Ürün resimleri getirme
    private void UrunResimleriGetir()
    {
        try
        {
            SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@parametre", "urun"),
                    new SqlParameter("@id", urunId ),
                };

            using (SqlDataReader drResim = SqlHelper.ExecuteReader("resim_Bilgi_Getir", parametre))
            {
                List<Banner> urunResimTablo = new List<Banner>();
       
                while (drResim.Read())
                {
                    Banner resim = new Banner
                    {
                        ResimAdi = drResim.GetString(drResim.GetOrdinal("resim_adi")),
                        Sira = drResim.GetInt32(drResim.GetOrdinal("sira")),
                        ResimBaslik = drResim.GetString(drResim.GetOrdinal("resim_baslik"))
                    };

                    if (resim.Sira == 1)
                    {
                        imgResimOrta.ImageUrl = "~/Products/Small/" + resim.ResimAdi;
                        imgResimOrta.AlternateText = resim.ResimBaslik;
                        hlResimBuyuk.NavigateUrl = "~/Products/Big/" + resim.ResimAdi;
                    }

                    urunResimTablo.Add(resim);
                }


                if (!drResim.HasRows)
                {
                    resimOrtaDiv.Visible = false;
                    imgResimYok.Visible = true;
                    imgResimYok.ImageUrl = "Products/Small/ResimYok.gif";
                }

                rptUrunResimleri.DataSource = urunResimTablo;
                rptUrunResimleri.DataBind();
            }
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Ürün Resimleri Listeleme Hatası", hata);
        }
    }
    #endregion

    #region Ürün listele işlemi
    private void urunGetir()
    {
        try
        {
            Urun urun = UrunDB.GetById(urunId);

            if (urun.id < 1)
                Response.Redirect("~/Default.aspx", false);

            Title = urun.title;
            MetaDescription = urun.description;
            MetaKeywords = urun.keywords;

            lblUrunAdi.InnerText = urun.urunAdi;

            lblMarka.Text = urun.markaAdi;
            lblGoruntu.Text = urun.hit.ToString();
            ltUrunDetay.Text = urun.urunOzellik;
            lblHavale.Text = "% " + urun.havaleIndirim.ToString();

            if (urun.kisaAciklama != "")
            {
                lblAciklama.Text = urun.kisaAciklama;
                trKisaAciklama.Visible = true;
            }

            lblStok.Text = GenelFonksiyonlar.StokDurumUrunDetay(urun.kiritikStok, urun.urunStok);

            lblFiyat.Text = AritmetikIslemler.UrunFiyatIndirim(urun.urunFiyat, urun.uIndirimFiyat, urun.doviz,urun.kdv);
            lblFiyatIndirim.Text = AritmetikIslemler.UrunFiyatIndirimVarmi(urun.urunFiyat, urun.uIndirimFiyat, urun.doviz, urun.kdv);

            lblHavaleile.Text = AritmetikIslemler.UrunKDVDahilHavaleFiyat(urun.urunFiyat, urun.uIndirimFiyat, urun.kdv, urun.havaleIndirim);
            
            
            fiyatToplam = AritmetikIslemler.UrunKDVDahilFiyat(urun.urunFiyat, urun.uIndirimFiyat, urun.kdv);
            // Taksitler İçin
            hdfUrunFiyat.Value = fiyatToplam.ToString();
            lblFiyatToplam.Text = fiyatToplam.ToString("C") + " (kdv Dahil)";

            UrunAdet(urun.stokCins);
            List<string> ozelikler = UrunDB.urunOzellikleri(urunId);

            foreach (string ozellik in ozelikler)
            {
                
                if (ozellik == "bauschLomb" || ozellik == "solotica" || 
                    ozellik == "rainbow" || ozellik == "freshLook" ||
                    ozellik == "sophistic" || ozellik == "starColor" ||
                    ozellik == "soloticaNaCo" || ozellik == "soloticaHidCo")
                {
                    Image img = new Image();
                    img.ImageUrl = "~/Images/buttonlar/renkSecenegi.gif";
                    img.Attributes.Add("onClick", "Javascript: renkPopUp('" + ResolveUrl("~/Products/RenkliLens/" + ozellik + ".htm');"));
                    this.pnlRenkliLens.Controls.Add(img);
                }
            }

            //if (urun.desiMiktari == 0)
            //{
            //    imgKargoUcretsiz.Visible = true;
            //}

            if (urun.uIndirimFiyat == 0)
            {
                trIndirim.Visible = false;
            }

            if (urun.havaleIndirim == 0)
            {
                trHavaleIndirim.Visible = false;
            }

            if (urun.kdv == 0)
            {
                trKDVToplamFiyat.Visible = false;
            }
        }
        catch (Exception hata)
        {
            ucMesaj.ErrorSis("Hata oluştu lütfen tekrar deneyiniz.");
            LogManager.SqlDB.Write("Ürün Detay Listeleme Hatası", hata);
        }
    }
    #endregion

    #region DropDown urun adet ekleme
    protected void UrunAdet(string adet)
    {
        for (int i = 12; i >= 0; i--)
        {
            ddlAdet.Items.Insert(0, new ListItem(i.ToString() + " " + adet, i.ToString()));
            ddlAdet1.Items.Insert(0, new ListItem(i.ToString() + " " + adet, i.ToString()));
        }
    }
    #endregion

    #region Ürün tavsiye listeleme
    private void urunTavsiyeListele()
    {
        try
        {
            List<UrunTavsiye> urunTavsiye = UrunTavsiyeDB.Liste(urunId);

            if (urunTavsiye.Count > 0)
            {
                rptUrunTavsiye.Visible = true;
                rptUrunTavsiye.DataSource = urunTavsiye;
                rptUrunTavsiye.DataBind();
            }
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Ürün Tavsiye Listeleme Hatası", hata);
        }
    }
    #endregion

    #region Ürün yorum ekleme
    protected void btnYorumGonder_Click(object sender, EventArgs e)
    {
        try
        {
            #region Kulanıcı giriş doğrulama
            StringBuilder denetim = new StringBuilder();
            if (ddlDegerlendirme.SelectedValue.ToString() == "0")
                denetim.Append("* Lütfen Ürün Degerlendirme Kırıteri Seçiniz..<br/>");
            if (string.IsNullOrWhiteSpace(txtMesaj.Text))
                denetim.Append("* Lütfen Ürün Yorumun Yazınız.<br/>");


            if (denetim.Length > 10)
            {
                ucMesaj.Alert(denetim.ToString());
                return;
            }
            #endregion

            UrunYorumlari yorum = new UrunYorumlari
            {
                UyeId = Convert.ToInt32(uyeId),
                AdiSoyadi = uyeAdi,
                UrunId = Convert.ToInt32(RouteData.Values["urunId"]),
                DegerKiriteri = Convert.ToInt32(ddlDegerlendirme.SelectedItem.Value),
                Yorum = txtMesaj.Text
            };

           int donendeger = UrunYorumlariDB.Kaydet(yorum);

           StringBuilder sb = new StringBuilder();
           sb.Append("Ürün yorumunun görüntülene bilmesi için yonetiçi panelinde ürün yorumuna görüntülenme onayı veriniz..<br/><br/>");
           sb.Append("<a href='http://www.lensoptik.com/Admin/Default.aspx'> Admin Panel Girişi</a>");

           MailManager.Admin.Send("Ürüne Yorum Eklendi", sb.ToString(), ProcessType.Async);

           Response.Redirect(string.Format("{0}?yorum={1}", Request.Url.AbsolutePath, donendeger), false);
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Ürün Yorumu Gönderme Hatası", ex);
            Response.Redirect(Request.Url.AbsolutePath + "?hata=0", false);
        }
    }
    #endregion

    #region Arkadaşına ürün gönderme işlemi
    protected void btnArkasinaGonder_Click(object sender, EventArgs e)
    {
        if (Session["randomStr"] == null)
        {
            Response.Redirect(Request.Url.AbsolutePath + "?hataResim=0", false);
            return;
        }

        if (Page.IsValid && (txtGuvenlikKodu.Text.ToString() == Session["randomStr"].ToString()))
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<h1>lensoptik.com</h1>");
                sb.Append("Sayın: <b>" + txtAlicAdi.Text+ "</b><br>");
                sb.Append(txtGoAdi.Text);
                sb.Append(" Tarafından tavsiye ürün gönderildi altaki linkten ilgili ürüne ulaşabilrsiniz.<br/>");
                sb.Append("<b>Not: </b>" + txtTavsiyeMesaj.Text + "<br>");
                sb.Append("<a href='" + Request.Url.AbsoluteUri + "' target='_blank' >Tavsiye Edilen Ürün</a>");

                MailManager.User.Send(txtAliciAdres.Text,
                    "Arkadaşınız tarafından ürün tavsiyesi gönderildi.", sb.ToString());

                Response.Redirect(Request.Url.AbsolutePath + "?tavsiye=0", false);
            }
            catch (Exception hata)
            {
                LogManager.SqlDB.Write("Ürün tavsiye gönderim hatası.", hata);
                Response.Redirect(Request.Url.AbsolutePath + "?hata=0", false);
            }
        }
        else
        {
            ucMesaj.InfoHide();
            ucMesaj.Alert("Lütfen Güvenlik Kodunu Giriniz.");
            txtGuvenlikKodu.Text = "";
        }
    }
    #endregion


    #region Kategori mevcut sayfa bilgisi işlemi
    protected void KategoriMevcutSayfa()
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@urunId", urunId );
            using (SqlDataReader dr = SqlHelper.ExecuteReader("urunDetay_KategoriListele", parametre))
            {
                List<Kategori> katTablo = new List<Kategori>();

                while (dr.Read())
                {
                    katTablo.Add(new Kategori
                    {
                        kategoriadi = dr.GetString(dr.GetOrdinal("kategoriadi")),
                        serial = dr.GetString(dr.GetOrdinal("serial")),
                        title = dr.GetString(dr.GetOrdinal("title"))
                    });
                }


                if (katTablo.Count > 0)
                {
                    if (katTablo[0].kategoriadi == "Lens Kabı ve Aksesuarı")
                    {
                        lblSagText.Text = "Ürün Adet: ";
                        lblSolText.Visible = false;
                        ddlAdet1.Visible = false;
                        ddlRenk1.Visible = false;
                        trSenekInfo.Visible = false;
                      
                    }
                }

                rptMevcutLink.DataSource = katTablo;
                rptMevcutLink.DataBind();
            }
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Ürün Detay Sitemap Hatası: ", hata);
        }
    }
    #endregion

    #region İlgili kategoriye ait markalar
    protected void MarkaListeleme()
    {
        try
        {
            rptMarkalar.DataSource = MarkaDB.UrunDetayMarkaLink(urunId);
            rptMarkalar.DataBind();
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Marka Listeleme", hata);
        }
    }
    #endregion

    #region Ürüne ait hediye ürün bilgisi
    private void HediyeUrun()
    {
        try
        {
            UrunHediyeTek hediyeUrun = UrunDB.HediyeUrun(urunId);
            if (hediyeUrun.UrunAdi != null)
            {
                imgKamResim.ImageUrl = "~/Products/Small/" + hediyeUrun.ResimAdi;
                lblKamFiyat.Text = hediyeUrun.UrunFiyat.ToString("N") + hediyeUrun.Doviz;
                lblKamUrun.Text = hediyeUrun.UrunAdi;

                ltHediye.Text = hediyeUrun.UrunAdi;
            }
            else
            {
                trHediye.Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Hediye Ürün", ex);
        }
    }
    #endregion


    #region Kampanya bilgi Listeleme
    protected void KampanyaBilgiListe()
    {
        try
        {
            rptKampanyaBilgi.DataSource = UrunKampanyaDB.BilgiListe(urunId);
            rptKampanyaBilgi.DataBind();
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Kampanya Bilgi", ex);
        }
    }
    #endregion

    #region Kampanya bilgi Listeleme
    protected void IndirimTablo()
    {
        try
        {
            List<UrunIndirim> liste = UrunIndirimDB.UrunDetayListe(urunId);
            
            if (liste.Count > 0)
            {
                rptTopluListe.DataSource = liste.ConvertToIndirimResponse();
                rptTopluListe.DataBind(); 
            }
            else
            {
                rptTopluListe.Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Kampanya Bilgi", ex);
        }
    }
    #endregion

}