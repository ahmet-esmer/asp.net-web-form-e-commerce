using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer;
using ModelLayer;

namespace DataAccessLayer
{

    public  class UrunIndirimDB :BaseDB
    {

        public static void Kaydet(UrunIndirim indirim)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[3];
                parameter[0] = new SqlParameter("@urunId", SqlDbType.Int);
                parameter[0].Value = indirim.UrunId;

                parameter[1] = new SqlParameter("@adet", SqlDbType.Int);
                parameter[1].Value = indirim.Adet;

                parameter[2] = new SqlParameter("@oran", SqlDbType.Int);
                parameter[2].Value = indirim.Oran;

                SqlHelper.ExecuteNonQuery("urunIndirim_Ekle", parameter);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<UrunIndirim> UrunDetayListe(int urunId)
        {
            try
            {
                List<UrunIndirim> indirmler = new List<UrunIndirim>();

                SqlParameter prm = new SqlParameter("@urunId", urunId);
                using (SqlDataReader dr = SqlHelper.ExecuteReader("urunIndirim_ListeWeb", prm))
                {

                    while (dr.Read())
                    {
                        indirmler.Add(new UrunIndirim
                        {
                            Adet = dr.GetInt32(dr.GetOrdinal("adet")),
                            Oran = dr.GetInt32(dr.GetOrdinal("oran")),
                            Fiyat = SepetOperasyon.UrunFiyat(dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                                                  dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat"))),
                            StokCinsi = dr.GetString(dr.GetOrdinal("stokCins")),
                            KDV = dr.GetInt32(dr.GetOrdinal("urunKDV")),
                        });
                    }
                }

                return indirmler;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IList<UrunIndirim> Liste(string urunId)
        {
            try
            {
                IList<UrunIndirim> indirmler = new List<UrunIndirim>();


                SqlParameter prm = new SqlParameter("@urunId", urunId);
                using (SqlDataReader dr = SqlHelper.ExecuteReader("urunIndirim_Liste", prm))
                {

                    while (dr.Read())
                    {
                        indirmler.Add(new UrunIndirim
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            UrunId = dr.GetInt32(dr.GetOrdinal("urunId")),
                            Adet = dr.GetInt32(dr.GetOrdinal("adet")),
                            Oran = dr.GetInt32(dr.GetOrdinal("oran"))
                        });
                    }
                }

                return indirmler;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static UrunIndirim UrunKampanyaGetir(int urunId, int miktar)
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@urunId", SqlDbType.Int);
            parameter[0].Value = urunId;

            parameter[1] = new SqlParameter("@adet", SqlDbType.Int);
            parameter[1].Value = miktar;

            UrunIndirim indirim = new UrunIndirim();

            SqlParameter prm = new SqlParameter("@urunId", urunId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader("urunIndirim_Getir", parameter))
            {
                while (dr.Read())
                {
                    indirim.Oran = dr.GetInt32(dr.GetOrdinal("oran"));
                    indirim.Adet = dr.GetInt32(dr.GetOrdinal("adet"));
                }
            }

            return indirim;

          
        }

        public static void Delete(int urunId, int indirimId)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[2];
                prm[0] = new SqlParameter("@urunId", SqlDbType.Int);
                prm[0].Value = urunId;
                prm[1] = new SqlParameter("@indirimId", SqlDbType.Int);
                prm[1].Value = indirimId;

                SqlHelper.ExecuteNonQuery("urunIndirim_Sil", prm);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
