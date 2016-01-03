using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;

namespace DataAccessLayer
{

    public class KullaniciAdresDB : BaseDB
    {

        public static int AdresVarmi(int kullniciId)
        {
            SqlParameter prm = new SqlParameter("@id", kullniciId);
            int adr = (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "kullanici_AdresVarmi", prm);
            return adr;
        }

        public static void Sil(int uyeId, int adresId)
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@uyeId", SqlDbType.Int);
            parameter[0].Value = uyeId;
            parameter[1] = new SqlParameter("@adresId", SqlDbType.NVarChar);
            parameter[1].Value = adresId;

            SqlHelper.ExecuteNonQuery("kullanici_AdresSil", parameter);
       
        }

        public static int Kaydet(KullaniciAdres adres)
        {
            int geriDonus = 0;
            try
            {
                SqlParameter[] parameter = new SqlParameter[6];
                parameter[0] = new SqlParameter("@uyeId", SqlDbType.Int);
                parameter[0].Value = adres.UyeId;
                parameter[1] = new SqlParameter("@adres", SqlDbType.NVarChar);
                parameter[1].Value =  adres.Adres ;
                parameter[2] = new SqlParameter("@teslimAlan", SqlDbType.NVarChar);
                parameter[2].Value = adres.TeslimAlan ;
                parameter[3] = new SqlParameter("@telefon", SqlDbType.NVarChar);
                parameter[3].Value = adres.Telefon ;
                parameter[4] = new SqlParameter("@sehirId", SqlDbType.Int);
                parameter[4].Value = adres.SehirId;
                parameter[5] = new SqlParameter("@deger_dondur", SqlDbType.Int);
                parameter[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery("kullanici_AdresEkle", parameter);

                geriDonus = Convert.ToInt32(parameter[5].Value);

            }
            catch (Exception )
            {
                throw;
            }

            return geriDonus;
        }

        public static List<KullaniciAdres> Liste(int uyeId)
        {
            List<KullaniciAdres> adresTablo = new List<KullaniciAdres>();

            try
            {
                SqlParameter parametre = new SqlParameter("@id", uyeId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("kullanici_AdresListele", parametre))
                {
                    while (dr.Read())
                    {
                     adresTablo.Add(new KullaniciAdres { 
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            Adres = dr.GetString(dr.GetOrdinal("adres")), 
                            TeslimAlan =  dr.GetString(dr.GetOrdinal("teslimAlan")),
                            Telefon = dr.GetString(dr.GetOrdinal("telefon")),
                            Sehir = KullaniciSehirIslem(dr.GetInt32(dr.GetOrdinal("sehirId")),
                                                       dr.GetString(dr.GetOrdinal("sehir")))

                        });
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }

            return adresTablo;
        }


        public static string KullaniciSehirIslem(int sehirId, string sehir)
        {

            if (sehirId == 0)
            {
                return "<span style='color:#d20740;' >Adres düzenleme kısmından şehir alanını güncelleyiniz.</span>";
            }
            else
            {
                return sehir;
            }
        }

        public static KullaniciAdres Getir(int adresId)
        {
            KullaniciAdres adres = new KullaniciAdres();

            try
            {
                SqlParameter parametre = new SqlParameter("@id", adresId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("kullanici_AdresGetir", parametre))
                {
                    while (dr.Read())
                    {
                        adres.Id = dr.GetInt32(dr.GetOrdinal("id"));
                        adres.Adres = dr.GetString(dr.GetOrdinal("adres"));
                        adres.Telefon = dr.GetString(dr.GetOrdinal("telefon"));
                        adres.TeslimAlan = dr.GetString(dr.GetOrdinal("teslimAlan"));
                        adres.SehirId = dr.GetInt32(dr.GetOrdinal("sehirId"));
                        adres.Sehir = dr.GetString(dr.GetOrdinal("sehir"));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return adres;
        }

        public static void Duzenle(KullaniciAdres adres)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[5];
                parameter[0] = new SqlParameter("@id", SqlDbType.Int);
                parameter[0].Value = adres.Id;
                parameter[1] = new SqlParameter("@adres", SqlDbType.NVarChar);
                parameter[1].Value = adres.Adres;
                parameter[2] = new SqlParameter("@teslimAlan", SqlDbType.NVarChar);
                parameter[2].Value = adres.TeslimAlan;
                parameter[3] = new SqlParameter("@telefon", SqlDbType.NVarChar);
                parameter[3].Value = adres.Telefon;

                parameter[4] = new SqlParameter("@sehirId", SqlDbType.Int);
                parameter[4].Value = adres.SehirId;

                SqlHelper.ExecuteNonQuery("kullanici_AdresGuncelle", parameter);

            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
