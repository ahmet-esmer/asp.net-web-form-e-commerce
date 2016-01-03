<%@ WebService Language="C#" Class="UrunService" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]



public class UrunService  : System.Web.Services.WebService {

    [WebMethod]
    public string FavoriEkle(int urunId)
    {
        string retVal = "işlem tamam";
        try
        {
            System.Data.SqlClient.SqlParameter[] sqlFavori = new SqlParameter[3];
            sqlFavori[0] = new SqlParameter("@uyeId", BusinessLayer.KullaniciOperasyon.GetId());
            sqlFavori[1] = new SqlParameter("@urunId", urunId);
            sqlFavori[2] = new SqlParameter("@deger_dondur", SqlDbType.Int);
            sqlFavori[2].Direction = ParameterDirection.Output;

            DataAccessLayer.SqlHelper.ExecuteNonQuery("kullanici_FavoriUrunEkle", sqlFavori);

            if ((int)sqlFavori[2].Value == 0)
            {
                retVal = "Ürün Favorilerinize eklenmiştir..";
            }
            else
            {
                retVal = "Ürün Favorilerim listeside Mevcut.. ";
            }
        }
        catch (Exception hata)
        {
            LoggerLibrary.LogManager.SqlDB.Write("Kullanıcı Favori Ürün Ekleme Hatası", hata);
            retVal = "Hata Oluştu";
        }

        return retVal;
    }


    [WebMethod]
    public string UrunIzleme(int urunId)
    {
        string retVal = null;
        try
        {
            SqlParameter[] sqlIzm = new SqlParameter[4];
            sqlIzm[0] = new SqlParameter("@uyeId", BusinessLayer.KullaniciOperasyon.GetId());
            sqlIzm[1] = new SqlParameter("@urunId", urunId);
            sqlIzm[2] = new SqlParameter("@deger_dondur", SqlDbType.Int);
            sqlIzm[2].Direction = ParameterDirection.Output;

            DataAccessLayer.SqlHelper.ExecuteNonQuery("kullanici_IndirimBildirEkle", sqlIzm);

            if ((int)sqlIzm[2].Value == 0)
            {
                retVal = "Ürün fiyat indriminde e-posta ile  bildirilecektir..";
            }
            else
            {
                retVal = "Bu ürün takip listesinde mevcut";
            }
        }
        catch (Exception hata)
        {
            LoggerLibrary.LogManager.SqlDB.Write("Kullanıcı ürün fiyat kakip", hata);
            retVal = "Hata Oluştu";
        }

        return retVal;
    }
    
}