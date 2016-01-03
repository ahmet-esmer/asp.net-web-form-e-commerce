using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;

namespace DataAccessLayer
{

    public class UrunKampanyaDB : BaseDB
    {

        public static int TopluKaydet(string serial, int kamUrunId)
        {
            int geriDonus = 0;
            try
            {
                SqlParameter[] parameter = new SqlParameter[3];

                parameter[0] = new SqlParameter("@kamUrunId", SqlDbType.Int);
                parameter[0].Value = Convert.ToInt32(kamUrunId);

                parameter[1] = new SqlParameter("@serial", SqlDbType.NVarChar);
                parameter[1].Value = serial;
                parameter[2] = new SqlParameter("@deger_dondur", SqlDbType.Int);
                parameter[2].Direction = ParameterDirection.Output;

             SqlHelper.ExecuteNonQuery("urun_HediyeTopluEkle", parameter);

             geriDonus = Convert.ToInt32(parameter[2].Value);
            }
            catch (Exception)
            {
                throw;
            }

            return geriDonus;
        }

        public static int Kaydet(string kamAnaUrunId, string kamUrunId)
        {
            int geriDonus = 0;
            try
            {
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@kamAnaUrunId", SqlDbType.Int);
                parameter[0].Value = Convert.ToInt32(kamAnaUrunId);
                parameter[1] = new SqlParameter("@kamUrunId", SqlDbType.Int);
                parameter[1].Value = Convert.ToInt32(kamUrunId);

                parameter[2] = new SqlParameter("@bolge", SqlDbType.Int);
                parameter[2].Value = Convert.ToInt32(Kampanya.HediyeUrun);

                parameter[3] = new SqlParameter("@deger_dondur", SqlDbType.Int);
                parameter[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery("urunKampanya_Ekle", parameter);
                geriDonus = Convert.ToInt32(parameter[3].Value);

            }
            catch (Exception)
            {
                throw;
            }

            return geriDonus;
        }

        public static void HediyeKampanyaKaydet(string kamAnaUrunId, string kamUrunId)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[3];
                parameter[0] = new SqlParameter("@kamAnaUrunId", SqlDbType.Int);
                parameter[0].Value = Convert.ToInt32(kamAnaUrunId);
                parameter[1] = new SqlParameter("@kamUrunId", SqlDbType.Int);
                parameter[1].Value = Convert.ToInt32(kamUrunId);
                parameter[2] = new SqlParameter("@bolge", SqlDbType.Int);
                parameter[2].Value = Convert.ToInt32(Kampanya.HediyeKampanya);

                SqlHelper.ExecuteNonQuery("urun_HediyeKampanya_Ekle", parameter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<UrunKampanya> Liste(int urunId)
        {
            List<UrunKampanya> kampanya = new List<UrunKampanya>();

            try
            {
                SqlParameter parametre = new SqlParameter("@id", urunId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("urunKampanya_Listele", parametre))
                {

                    while (dr.Read())
                    {
                        kampanya.Add(new UrunKampanya
                        {
                            id = dr.GetInt32(dr.GetOrdinal("id")),
                            kamUrunId = dr.GetInt32(dr.GetOrdinal("kamUrunId")),
                            urunFiyat = dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                            uIndirimFiyat = dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat")),
                            urunAdi = dr.GetString(dr.GetOrdinal("urunAdi")),
                            doviz = dr.GetString(dr.GetOrdinal("doviz"))
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return kampanya;
        }

        public static void UrunBilgiKaydet(UrunKampanyaBilgi bilgi)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[3];
                parameter[0] = new SqlParameter("@sira", SqlDbType.Int);
                parameter[0].Value = bilgi.Sira;
                parameter[1] = new SqlParameter("@urunId", SqlDbType.Int);
                parameter[1].Value = bilgi.UrunId;

                parameter[2] = new SqlParameter("@bilgi", SqlDbType.NVarChar);
                parameter[2].Value = bilgi.Bilgi;


                SqlHelper.ExecuteNonQuery("urun_KampanyaBilgi_Ekle", parameter);


            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<UrunKampanyaBilgi> BilgiListe(int urunId)
        {
            List<UrunKampanyaBilgi> kampanya = new List<UrunKampanyaBilgi>();

            try
            {
                SqlParameter parametre = new SqlParameter("@urunId", urunId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_KampanyaBilgi_Listele", parametre))
                {

                    while (dr.Read())
                    {
                        kampanya.Add(new UrunKampanyaBilgi
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            //UrunId = dr.GetInt32(dr.GetOrdinal("urunId")),
                            Sira = dr.GetInt32(dr.GetOrdinal("sira")),
                            Bilgi = dr.GetString(dr.GetOrdinal("bilgi")),

                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return kampanya;
        }

        public static void UrunBilgiSil(int id)
        {
            try
            {
                SqlParameter parameter = new SqlParameter("@id", id);

                SqlHelper.ExecuteNonQuery("urun_KampanyaBilgi_Sil", parameter);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
