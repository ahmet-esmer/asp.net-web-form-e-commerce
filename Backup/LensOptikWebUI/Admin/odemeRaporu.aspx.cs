using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer;
using DataAccessLayer;
using ModelLayer;
using BusinessLayer.BasePage;
using BusinessLayer.PagingLink;


public partial class Siparisler: BasePageAdmin
{
    private int sayfaNo, Baslangic, Bitis;
    private int sayfaGosterim = 25;
    private int toplamKayit = 0;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
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

            KayitSayisi("butunSiparisler");
            siparisOdemeListele(Baslangic, Bitis, "butunSiparisler");
        }
    }

    #region Toplam Kayıt Sayısı Almak
    private void KayitSayisi(string parameter)
    {
        toplamKayit = (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "siparis_OdemeSayafaNo");

        ltlSayfalama.Text = PagingLink.GetHtmlCode(Request.QueryString, sayfaGosterim, toplamKayit);

    }
    #endregion

    #region Sipariş Listeleme İşlemi
    private void siparisOdemeListele(int Baslangic, int Bitis, string sorguParam)
    {
        try
        {
            List<KrediKartOdeme> sipariTablo = new List<KrediKartOdeme>();

            SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@Baslangic", Baslangic),
                    new SqlParameter("@Bitis", Bitis)
                };

            using (SqlDataReader dr = SqlHelper.ExecuteReader("siparis_OdemeListele", parametre))
            {
                while (dr.Read())
                {
                    sipariTablo.Add(new KrediKartOdeme
                    {
                        SiparisId = dr.GetInt32(dr.GetOrdinal("id")),
                        SiparisNo = dr.GetString(dr.GetOrdinal("siparisNo")),
                        UyeAdi = dr.GetString(dr.GetOrdinal("adiSoyadi")),
                        SiparisDurumu = Fonksiyonlar.SiparisDurum(dr.GetInt32(dr.GetOrdinal("siparisDurumu"))),
                        SiparisTarihi = dr.GetDateTime(dr.GetOrdinal("siparisTarihi")),
                        TaksitMiktari = dr.GetInt32(dr.GetOrdinal("taksitMiktari")),
                        TaksitliGenelToplami = dr.GetDecimal(dr.GetOrdinal("TaksitliGenelToplami")),
                        KartSahibi = dr.GetString(dr.GetOrdinal("kartAd")),
                        KartNo = dr.GetString(dr.GetOrdinal("kartNo")),
                        OnayKodu = dr.GetString(dr.GetOrdinal("onayKodu")),
                        ReferansNo = dr.GetString(dr.GetOrdinal("referansNo")),
                        BankaAdi = dr.GetString(dr.GetOrdinal("bankaAdi"))
                    });
                }  
            }
            grwSiparisOdeme.DataSource = sipariTablo;
            grwSiparisOdeme.DataBind();
        }
        catch (Exception ex) 
        {
            mesajGosterSis("Listeleme Hatası", ex);
        }

      
    }
    #endregion

    protected void txtKartSahibi_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(txtKartSahibi.Text))
            {
                OdemeArama("kartAd", txtKartSahibi.Text.Trim());
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Listeleme Hatası", ex);
        }
    }

    protected void txtIslemNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(txtIslemNo.Text))
            {
                OdemeArama("islem", txtIslemNo.Text.Trim());
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Listeleme Hatası", ex);
        }
    }

    protected void OdemeArama(string parametre, string value)
    {
        try
        {
            ltlSayfalama.Visible = false;
            List<KrediKartOdeme> sipariTablo = new List<KrediKartOdeme>();

            SqlParameter[] prm = new SqlParameter[] 
            { 
               new SqlParameter("@parametre", parametre),
               new SqlParameter("@value", value)
             };

            using (SqlDataReader dr = SqlHelper.ExecuteReader("siparis_Odeme_Arama", prm))
            {
                while (dr.Read())
                {
                    sipariTablo.Add(new KrediKartOdeme
                    {
                        SiparisId = dr.GetInt32(dr.GetOrdinal("id")),
                        SiparisNo = dr.GetString(dr.GetOrdinal("siparisNo")),
                        UyeAdi = dr.GetString(dr.GetOrdinal("adiSoyadi")),
                        SiparisDurumu = Fonksiyonlar.SiparisDurum(dr.GetInt32(dr.GetOrdinal("siparisDurumu"))),
                        SiparisTarihi = dr.GetDateTime(dr.GetOrdinal("siparisTarihi")),
                        TaksitMiktari = dr.GetInt32(dr.GetOrdinal("taksitMiktari")),
                        TaksitliGenelToplami = dr.GetDecimal(dr.GetOrdinal("TaksitliGenelToplami")),
                        KartSahibi = dr.GetString(dr.GetOrdinal("kartAd")),
                        KartNo = dr.GetString(dr.GetOrdinal("kartNo")),
                        OnayKodu = dr.GetString(dr.GetOrdinal("onayKodu")),
                        ReferansNo = dr.GetString(dr.GetOrdinal("referansNo")),
                        BankaAdi = dr.GetString(dr.GetOrdinal("bankaAdi"))
                    });
                }
            }
            grwSiparisOdeme.DataSource = sipariTablo;
            grwSiparisOdeme.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

}
