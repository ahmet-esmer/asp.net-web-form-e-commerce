using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class Kullanici_Siparisler : System.Web.UI.Page
{
    public int uyeId = 0;
    private int scount = 0;
    private int reCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoginKontrol();

        Page.Title = KullaniciOperasyon.GetName() + " | Şiparişler";

        if (!IsPostBack)
        {
            KulaniciSiparisListele(uyeId);
        }

        if (Request.QueryString["reOrder"] != null)
        {
            SiparisDetayGetir(Convert.ToInt32(Request.QueryString["reOrder"]));
        }
    }


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


    #region Kulanıcı Sipariş Listeleme İşlemi
    private void KulaniciSiparisListele(int uyeId)
    {
        try
        {
            gvwListe.DataSource = SiparisDB.Listele(uyeId.ToString(), "kulaniciId");
            gvwListe.DataBind();
        }
        catch (Exception)
        {
            Mesaj.ErrorSis("Sipariş Lisrteleme Hatası");
        }
    }
    #endregion


    protected void gvwListe_Paging(object sender, GridViewPageEventArgs e)
    {
        gvwListe.PageIndex = e.NewPageIndex;
        KulaniciSiparisListele(uyeId);
    }

    // Reorder işlemleri
    private void SiparisDetayGetir(int siparisId)
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@siparisId", siparisId);
            List<SiparisDetay> siparisDetay = new List<SiparisDetay>();

            using (SqlDataReader dr = SqlHelper.ExecuteReader("siparis_DetayGetir", parametre))
            {

                while (dr.Read())
                {
                    sepetUrunekle(new SiparisDetay
                    {
                        urunId = dr.GetInt32(dr.GetOrdinal("urunId")),
                        sagAdet = dr.GetInt32(dr.GetOrdinal("sagAdet")),
                        solAdet = dr.GetInt32(dr.GetOrdinal("solAdet")),
                        sagBilgi = dr.GetString(dr.GetOrdinal("sagBilgi")),
                        solBilgi = dr.GetString(dr.GetOrdinal("solBilgi")),
                    });

                    ++ reCount;
                }

                // Ürün stokta yok ise
                if (reCount == scount)
                    return;
            }

            SiparisAdresBul(siparisId);
            Session["adresMenu"] = "True";
            Response.Redirect("~/Market/IslemOnay.aspx");
        }
        catch (Exception hata)
        {
            Mesaj.ErrorSis("Sipariş Detay Ürün  Listeleme hatası..", hata.ToString());
        }
    }

    #region Sepete Ürün Ekleme
    private void sepetUrunekle(SiparisDetay siparis)
    {
        try
        {
            SqlParameter[] parametre = new SqlParameter[9];
            parametre[0] = new SqlParameter("@uyeId", uyeId);
            parametre[1] = new SqlParameter("@urunId", siparis.urunId );
            parametre[2] = new SqlParameter("@sagAdet", siparis.sagAdet);
            parametre[3] = new SqlParameter("@solAdet", siparis.solAdet);
            parametre[4] = new SqlParameter("@sagBilgi", siparis.sagBilgi);
            parametre[5] = new SqlParameter("@solBilgi", siparis.solBilgi);

            parametre[6] = new SqlParameter("@hediyeId", "0");
            parametre[7] = new SqlParameter("@hediyeBilgi","");

            parametre[8] = new SqlParameter("@deger_dondur", SqlDbType.Int);
            parametre[8].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery("Sepet_UrunEkle", parametre);

            int geriDonus = (int)parametre[8].Value;


            if (geriDonus >= 0)
            {
                Mesaj.Alert("Talep etiginiz ürün stoklarımızda <b> " + geriDonus.ToString() + " </b> adet bulunmaktadır.<br /> Lütfen işleminizi manuel gerçekleştiriniz.");

               ++ scount;
            }
        }
        catch (Exception ex)
        {
            Mesaj.ErrorSis("İşlem hata ile Sonuçlandı..");
            LogManager.Mail.Write("Sepete Ürün Eklerken Hata Oluştu", ex);
        }
    }
    #endregion

    private void SiparisAdresBul(int siparisId)
    {
        SqlParameter parametre = new SqlParameter("@siparisId", siparisId);

        using (SqlDataReader dr = SqlHelper.ExecuteReader("siparis_ReorderAdres", parametre))
        {
            while (dr.Read())
            {
               Session["adresId"]  = dr.GetInt32(dr.GetOrdinal("adresId"));
               Session["faturaId"]  = dr.GetInt32(dr.GetOrdinal("faturaId")); 
            }
        }
    }

}