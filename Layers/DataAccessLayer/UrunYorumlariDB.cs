using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ModelLayer;
using System.Data;

namespace DataAccessLayer
{

    public class UrunYorumlariDB :BaseDB
    {
        public static int ItemCount()
        {
            try
            {
                string query = "SELECT COUNT(id) FROM tbl_urunYorumlari";
                return (int)SqlHelper.ExecuteScalar(CommandType.Text, query);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int ItemCountForProduct(int id)
        {
            try
            {
                SqlParameter prm = new SqlParameter("@id", id);

                string query = "SELECT COUNT(*) id FROM tbl_urunYorumlari WHERE durum='1' AND urun_Id=@id";
                return (int)SqlHelper.ExecuteScalar(CommandType.Text, query,prm);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Kaydet(UrunYorumlari yorum)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[6];
                parameter[0] = new SqlParameter("@uye_Id", SqlDbType.Int);
                parameter[0].Value = yorum.UyeId;
                parameter[1] = new SqlParameter("@uyeAdi", SqlDbType.NVarChar);
                parameter[1].Value = yorum.AdiSoyadi;
                parameter[2] = new SqlParameter("@urun_Id", SqlDbType.Int);
                parameter[2].Value = yorum.UrunId;
                parameter[3] = new SqlParameter("@degerKiriteri", SqlDbType.Int);
                parameter[3].Value = yorum.DegerKiriteri;
                parameter[4] = new SqlParameter("@yorum", SqlDbType.NVarChar);
                parameter[4].Value = yorum.Yorum;
                parameter[5] = new SqlParameter("@deger_dondur", SqlDbType.Int);
                parameter[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery("urun_YorumEkle", parameter);

                return Convert.ToInt32(parameter[5].Value);     
            }
            catch (Exception hata)
            {
                throw hata;
            }
        }

        public static UrunYorumlari Get(int yorumId, string parametre)
        {

            UrunYorumlari urunYorum = new UrunYorumlari();
            try
            {
                SqlParameter[] sqlPrm = new SqlParameter[] 
                { 
                    new SqlParameter("@parametre", parametre),
                    new SqlParameter("@id", yorumId)
                };

                using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_YorumGetir", sqlPrm))
                {
                    while (dr.Read())
                    {
                        urunYorum = new UrunYorumlari
                        {
                            AdiSoyadi = dr.GetString(dr.GetOrdinal("uyeAdi")),
                            UrunAdi = dr.GetString(dr.GetOrdinal("urunAdi")),
                            Yorum = dr.GetString(dr.GetOrdinal("yorum")),
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            DegerKiriteri = dr.GetInt32(dr.GetOrdinal("degerKiriteri"))
                        };
                    }
                }

                return urunYorum;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<UrunYorumlari> Liste(int baslangic, int bitis)
        {
            try
            {
            List<UrunYorumlari> urunYorumlari = new List<UrunYorumlari>();

            SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@Baslangic", baslangic),
                    new SqlParameter("@Bitis", bitis)
                };


            using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_YorumListele", parametre))
            {
                while (dr.Read())
                {
                    urunYorumlari.Add(new UrunYorumlari
                    {
                          Id =  dr.GetInt32(dr.GetOrdinal("id")),
                          AdiSoyadi =  dr.GetString(dr.GetOrdinal("adiSoyadi")),
                          UyeId = dr.GetInt32(dr.GetOrdinal("UyeId")),
                          UrunAdi =  dr.GetString(dr.GetOrdinal("urunAdi")),
                          DegerKiriteri = dr.GetInt32(dr.GetOrdinal("degerKiriteri")),
                          Yorum = dr.GetString(dr.GetOrdinal("yorum")),
                          Sehir = dr.GetString(dr.GetOrdinal("sehir")),
                          Tarih = dr.GetDateTime(dr.GetOrdinal("tarih")),
                          IsimGoster =  dr.GetBoolean(dr.GetOrdinal("isimGoster")),
                          Durum =  dr.GetBoolean(dr.GetOrdinal("durum"))
                    });
                }
            }

                return urunYorumlari;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }

}
