using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;

namespace DataAccessLayer
{
    public class KullaniciPuanDB : BaseDB
    {

        public static void Kaydet(KullaniciPuan puan)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@uyeId", SqlDbType.Int);
                parameter[0].Value = puan.UyeId;
                parameter[1] = new SqlParameter("@puanKod", SqlDbType.NVarChar);
                parameter[1].Value = puan.PuanKod;
                parameter[2] = new SqlParameter("@aciklama", SqlDbType.NVarChar);
                parameter[2].Value = puan.Aciklama;
                parameter[3] = new SqlParameter("@genelId", SqlDbType.Int);
                parameter[3].Value = puan.GenelId;

                SqlHelper.ExecuteNonQuery("kullaniciPuan_Ekle", parameter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Sil(KullaniciPuan puan)
        {
                SqlParameter[] parameter = new SqlParameter[3];
                parameter[0] = new SqlParameter("@uyeId", SqlDbType.Int);
                parameter[0].Value = puan.UyeId;
                parameter[1] = new SqlParameter("@puanKod", SqlDbType.NVarChar);
                parameter[1].Value = puan.PuanKod;
                parameter[2] = new SqlParameter("@genelId", SqlDbType.Int);
                parameter[2].Value = puan.GenelId;

                SqlHelper.ExecuteNonQuery("kullaniciPuan_Sil", parameter);
        }

        public static decimal KullaniciPuan(int uyeId)
        {
            SqlParameter parameter = new SqlParameter("@uyeId", uyeId);

            return (decimal)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "kullaniciPuan_Topla", parameter);
        }

        public static List<KullaniciPuan> Liste(int uyeId)
        {
            List<KullaniciPuan> puan = new List<KullaniciPuan>();

            try
            {
                SqlParameter parametre = new SqlParameter("@uyeId", uyeId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("kullaniciPuan_Listele", parametre))
                {
                    while (dr.Read())
                    {
                        puan.Add(new KullaniciPuan
                        {
                            Aciklama = dr.GetString(dr.GetOrdinal("aciklama")),
                            PuanTL = dr.GetDecimal(dr.GetOrdinal("puanTL")),
                            KazanmaTarih = dr.GetDateTime(dr.GetOrdinal("kazanmaTarih"))
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return puan;
        }
    }
}
