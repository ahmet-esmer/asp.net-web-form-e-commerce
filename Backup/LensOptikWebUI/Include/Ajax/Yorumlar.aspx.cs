using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BusinessLayer;
using BusinessLayer.PagingLink;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class Include_Ajax_Yorum : System.Web.UI.Page
{
    private static int sayfaGosterim = 5;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethod]
    public static string Sayfalama(int urunId, int sayfaNo)
    {
        return AjaxPaging.List(sayfaGosterim, sayfaNo, UrunYorumlariDB.ItemCountForProduct(urunId));
    }


    [System.Web.Services.WebMethod]
    public static List<UrunYorumlari> Liste(int urunId, int sayfaNo)
    {
        int Baslangic, Bitis;

        Baslangic = (sayfaNo * sayfaGosterim) + 1;
        Bitis = Baslangic + sayfaGosterim - 1;

        List<UrunYorumlari> urunYorumlari = new List<UrunYorumlari>();

        try
        {
            SqlParameter[] parametre = new SqlParameter[] 
             { 
                 new SqlParameter("@Baslangic", Baslangic),
                 new SqlParameter("@Bitis", Bitis),
                 new SqlParameter("@urunId", urunId)
             };

            using (SqlDataReader drYr = SqlHelper.ExecuteReader("urun_YorumListeleWeb", parametre))
            {
                while (drYr.Read())
                {
                    urunYorumlari.Add(new UrunYorumlari
                    {
                        Id = drYr.GetInt32(drYr.GetOrdinal("id")),
                        AdiSoyadi = GenelFonksiyonlar.ToTitleCase(drYr.GetString(drYr.GetOrdinal("adiSoyadi"))),
                        DegerKiriteri = drYr.GetInt32(drYr.GetOrdinal("degerKiriteri")),
                        Yorum = drYr.GetString(drYr.GetOrdinal("yorum")),
                        UrunAdi = DateFormat.Tarih(drYr.GetDateTime(drYr.GetOrdinal("tarih")).ToString())
                    });
                }
            }
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Ürün Detay Ürün yorumlari", hata);
        }

        return urunYorumlari;
    }


}