using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLayer;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class ZiyaretciDefteriDB :BaseDB
    {
        public static int ItemCount()
        {
            try
            {
                string query = "SELECT COUNT(id) FROM tbl_ziyaretciDefteri";
                return (int)SqlHelper.ExecuteScalar(CommandType.Text, query);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int YorumKaydet(ZiyaretciDefteri mesaj)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[6];
                parameter[0] = new SqlParameter("@adSoyad", SqlDbType.NVarChar);
                parameter[0].Value = mesaj.adSoyad;
                parameter[1] = new SqlParameter("@ePosta", SqlDbType.NVarChar);
                parameter[1].Value = mesaj.ePosta;
                parameter[2] = new SqlParameter("@yorum", SqlDbType.NVarChar);
                parameter[2].Value = mesaj.yorum;
                parameter[3] = new SqlParameter("@sehir", SqlDbType.NVarChar );
                parameter[3].Value = mesaj.sehirAd;
                parameter[4] = new SqlParameter("@ilce", SqlDbType.NVarChar);
                parameter[4].Value = mesaj.ilceAd;
                parameter[5] = new SqlParameter("@deger_dondur", SqlDbType.Int);
                parameter[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery("ziyaretci_DefteriEkle", parameter);

                return Convert.ToInt32(parameter[5].Value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<ZiyaretciDefteri> Get()
        {
            List<ZiyaretciDefteri> ZiyaretciTablo = new List<ZiyaretciDefteri>();

            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ziyaretci_DefteriPanel"))
            {
                while (dr.Read())
                {
                    ZiyaretciTablo.Add(new ZiyaretciDefteri(
                        dr.GetInt32(dr.GetOrdinal("id")),
                        dr.GetString(dr.GetOrdinal("yorum"))));
                }
            }

            return ZiyaretciTablo;
        }

        public static List<ZiyaretciDefteri> Liste(int baslangic, int bitis, string parametre )
        {

            List<ZiyaretciDefteri> ZiyaretciTablo = new List<ZiyaretciDefteri>();

            SqlParameter[] sqlParametre = new SqlParameter[] 
                { 
                    new SqlParameter("@Baslangic", baslangic),
                    new SqlParameter("@Bitis", bitis),
                    new SqlParameter("@parametre", parametre)
                };

            using (SqlDataReader dr = SqlHelper.ExecuteReader("ziyaretci_DefteriListele", sqlParametre))
            {
                while (dr.Read())
                {
                    string ilceAd = " ";
                    if (!dr.IsDBNull(dr.GetOrdinal("ilceAd")))
                    {
                        ilceAd = dr.GetString(dr.GetOrdinal("ilceAd"));
                    }

                    ZiyaretciTablo.Add(new ZiyaretciDefteri(
                        dr.GetInt32(dr.GetOrdinal("id")),
                        dr.GetString(dr.GetOrdinal("adSoyad")),
                        dr.GetString(dr.GetOrdinal("yorum")),
                        dr.GetDateTime(dr.GetOrdinal("eklenmeTarihi")),
                        dr.GetString(dr.GetOrdinal("sehirAd")),
                        ilceAd,
                        dr.GetString(dr.GetOrdinal("yorumCevap"))));
                }
            }
            return ZiyaretciTablo;

        }

        public static int ItemCount(string parametre)
        {
            try
            {
                SqlParameter sqlNo = new SqlParameter("@parametre", parametre);
                return (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "ziyaretci_DefteriSayfaNo", sqlNo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
