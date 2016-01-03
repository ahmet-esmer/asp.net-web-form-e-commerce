using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;
using DataAccessLayer;
using BusinessLayer;
using BusinessLayer.BasePage;
using BusinessLayer.PagingLink;


public partial class Admin_Siparisler: BasePageAdmin
{
    private int sayfaNo, Baslangic, Bitis;
    private int sayfaGosterim = 25;
    private int toplamKayit = 0;
    private List<Siparis> siparisler;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            lblSayfaBaslik.Text = "Siparişler";

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

           

            if (Request.QueryString["SDurum"] != null)
            {
                KayitSayisi(Request.QueryString["SDurum"]);
                siparisListele(Baslangic, Bitis, Request.QueryString["SDurum"]);
            }
            else if (Request.QueryString["tarih1"] != null)
            {
                TarihArama();
            }
            else
            {
                KayitSayisi("butunSiparisler");
                siparisListele(Baslangic, Bitis, "butunSiparisler");
            }
        }
    }


    private void TarihArama()
    {
        try
        {
            SiparisArama prm = new SiparisArama();
            prm.Baslangic = Baslangic;
            prm.Bitis = Bitis;
            prm.BaslangicTarih = Convert.ToDateTime(Request.QueryString["tarih1"]);
            prm.BitisTarih = Convert.ToDateTime(Request.QueryString["tarih2"] + " 23:59:59");
            prm.SiparisDurumu = Request.QueryString["durum"];

            grwSiparisler.DataSource = SiparisDB.TarihListe(prm);
            grwSiparisler.DataBind();

            prm = SiparisDB.TarihSayfaNo(prm);

            ltlSayfalama.Text = PagingLink.GetHtmlCode(Request.QueryString, sayfaGosterim, prm.SayfaToplam);

            if (prm.SayfaToplam == 0)
            {
                mesajGizleOk();
                mesajGosterNo(string.Format("{0} ile  {1} Tarihleri aralığında sipariş bulunamadı.", DateFormat.TarihSaat(prm.BaslangicTarih), DateFormat.TarihSaat(prm.BitisTarih) ));
            }
            else
            {
                mesajGizleNo();
mesajGosterOk(string.Format("{0} ile  {1} Tarihleri aralığında toplam<b> {2}</b> adet sipariş bulundu.</br> Toplam Fiyat: <b>{3}</b>",DateFormat.TarihSaat(prm.BaslangicTarih), DateFormat.TarihSaat(prm.BitisTarih), prm.SayfaToplam, prm.GenelToplami.ToString("C") ));
            }

        }
        catch (Exception ex)
        {
             mesajGosterSis("Sipariş Listeleme Hatası", ex);
        }
    }

    #region Toplam Kayıt Sayısı Almak
    private void KayitSayisi(string parameter)
    {
        SqlParameter parametre = new SqlParameter("@parametre", parameter);
        toplamKayit = (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "siparis_KayitSayafaNo", parametre);

        ltlSayfalama.Text = PagingLink.GetHtmlCode( Request.QueryString, sayfaGosterim, toplamKayit);

    }
    #endregion

    #region Sipariş Listeleme İşlemi
    private void siparisListele(int Baslangic, int Bitis, string sorguParam)
    {
        try
        {
            List<Siparis> sipariTablo = new List<Siparis>();

            SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@Baslangic", Baslangic),
                    new SqlParameter("@Bitis", Bitis),
                    new SqlParameter("@parametre", sorguParam.ToString())
                };

            using (SqlDataReader dr = SqlHelper.ExecuteReader("siparis_KayitListele", parametre))
            {
                while (dr.Read())
                {
                    sipariTablo.Add(new Siparis
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("id")),
                        SiparisNo = dr.GetString(dr.GetOrdinal("siparisNo")),
                        AdiSoyadi = dr.GetString(dr.GetOrdinal("adiSoyadi")),
                        KullaniciId = dr.GetInt32(dr.GetOrdinal("kullaniciId")),
                        OdemeTipi = Fonksiyonlar.OdemeTuru(dr.GetInt32(dr.GetOrdinal("odemeTipi"))),
                        SiparisDurumu = Fonksiyonlar.SiparisDurum(dr.GetInt32(dr.GetOrdinal("siparisDurumu"))),
                        SiparisTarihi =  dr.GetDateTime(dr.GetOrdinal("siparisTarihi")),
                        TaksitMiktari = dr.GetInt32(dr.GetOrdinal("taksitMiktari")),
                        TaksitliGenelToplami = dr.GetDecimal(dr.GetOrdinal("TaksitliGenelToplami"))
                    });
                }  
            }
            grwSiparisler.DataSource = sipariTablo;
            grwSiparisler.DataBind();
        }
        catch (Exception ex) 
        {
            mesajGosterSis("Sipariş Listeleme Hatası", ex);
        }
    }
    #endregion

    protected void txtSiparisArama_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ltlSayfalama.Visible = false;
            siparisler = SiparisDB.Listele(txtSiparisArama.Text.Trim(), "siparisID");
            grwSiparisler.DataSource = siparisler;
            grwSiparisler.DataBind();

            if (siparisler.Count == 0)
            {
                mesajGosterNo(string.Format("<b>{0}</b> nolu  sipariş bulunamadı.", txtSiparisArama.Text));
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Arama Hatası:", ex);
        }
        
    }

    protected void txt_UyeAdi_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ltlSayfalama.Visible = false;
            siparisler = SiparisDB.Listele(txt_UyeAdi.Text.Trim(), "adArama");
            grwSiparisler.DataSource = siparisler;
            grwSiparisler.DataBind();

            if (siparisler.Count == 0)
            {
                mesajGizlenfo();
                mesajGosterNo(string.Format("<b>{0}</b> kullanıcısına ait sipariş bulunamadı.", txt_UyeAdi.Text));
            }
            else
            {
                mesajGizleNo();
                mesajGosterInfo(string.Format("<b>{0}</b> kullanıcısına ait toplam <b>{1}</b> sipariş bulundu.", txt_UyeAdi.Text, siparisler.Count));
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Arama Hatası:", ex);
        }
    }

    protected void txtSiparisFiyat_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ltlSayfalama.Visible = false;
            decimal fiyat = Convert.ToDecimal(txtSiparisFiyat.Text.Trim());
            siparisler = SiparisDB.Listele(" ","fiyat",fiyat);

            grwSiparisler.DataSource = siparisler;
            grwSiparisler.DataBind();

            if (siparisler.Count == 0)
            {
                mesajGizlenfo();
                mesajGosterNo(string.Format("<b>{0} TL</b> Degerinde sipariş bulunamadı.", txtSiparisFiyat.Text));
            }
            else
            {
                mesajGizleNo();
                mesajGosterInfo(string.Format("<b>{0}TL</b> Degerinde toplam <b>{1}</b> sipariş bulundu.", txtSiparisFiyat.Text, siparisler.Count));
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Arama Hatası:", ex);
        }
    }

    protected void btnSipTarihAra_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtTarih_1.Text) == true && ddlSiparisDurum.SelectedValue == "-1" )
        {
            mesajGosterInfo("Lütfen arama kıriteri giriniz.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtTarih_1.Text) == true || string.IsNullOrWhiteSpace(txtTarih_2.Text) == true  )
        {
            Response.Redirect("~/Admin/siparisler.aspx?SDurum=" + ddlSiparisDurum.SelectedValue);
        }
        else
        {
            Response.Redirect(string.Format("~/Admin/siparisler.aspx?durum={0}&tarih1={1}&tarih2={2}",
                ddlSiparisDurum.SelectedValue, txtTarih_1.Text, txtTarih_2.Text));

        }
    }
}
