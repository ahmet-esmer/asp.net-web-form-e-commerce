using System;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public class KullaniciDB:BaseDB
    {

        public static int kaydet(Kullanici kulanici)
        {
            int geriDonus = 0;
            try
            {
                SqlParameter[] parameter = new SqlParameter[12];
                parameter[0] = new SqlParameter("@uyeAdiSoyadi", SqlDbType.NVarChar);
                parameter[0].Value = kulanici.AdiSoyadi;
                parameter[1] = new SqlParameter("@uyeGsm", SqlDbType.NVarChar);
                parameter[1].Value = kulanici.Gsm;
                parameter[2] = new SqlParameter("@uyeCinsiyet", SqlDbType.Bit);
                parameter[2].Value = kulanici.Cinsiyet;
                parameter[3] = new SqlParameter("@uyeSehir", SqlDbType.Int);
                parameter[3].Value = kulanici.Sehir;
                parameter[4] = new SqlParameter("@uyeEposta", SqlDbType.NVarChar);
                parameter[4].Value = kulanici.EPosta;
                parameter[5] = new SqlParameter("@uyeSifre", SqlDbType.NVarChar);
                parameter[5].Value = kulanici.Sifre;
                parameter[6] = new SqlParameter("@uyeDogumTarihi", SqlDbType.SmallDateTime);
                parameter[6].Value = kulanici.DogumTarihi;
                parameter[7] = new SqlParameter("@kulaniciTip", SqlDbType.NVarChar);
                parameter[7].Value = kulanici.KullaniciTipi;
                parameter[8] = new SqlParameter("@deger_dondur", SqlDbType.Int);
                parameter[8].Direction = ParameterDirection.Output;


                if (kulanici.Id > 0)// Kulanıcı Güncelleme
                {
                    parameter[9] = new SqlParameter("@smsGonder", SqlDbType.Bit);
                    parameter[9].Value = kulanici.SMSGonder;
                    parameter[10] = new SqlParameter("@epostaGonder", SqlDbType.Bit);
                    parameter[10].Value = kulanici.PostaGonder;
                    parameter[11] = new SqlParameter("@id", SqlDbType.NVarChar);
                    parameter[11].Value = kulanici.Id;

                    SqlHelper.ExecuteNonQuery("kullanici_KayitGuncelle", parameter);
                    geriDonus = Convert.ToInt32(parameter[8].Value);
                }
                else
                {
                    SqlHelper.ExecuteNonQuery("kullanici_KayitEkle", parameter);
                    geriDonus = Convert.ToInt32(parameter[8].Value);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return geriDonus;
        }

        public static Kullanici Getir(int kulaniciId)
        {

            Kullanici k = null;
            SqlParameter prm = new SqlParameter("@id",kulaniciId);

            using (SqlDataReader dr = SqlHelper.ExecuteReader("kullanici_Getir", prm))
            {
                while (dr.Read())
                {
                    k = new Kullanici
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("id")),
                        AdiSoyadi = dr.GetString(dr.GetOrdinal("adiSoyadi")),
                        Gsm = dr.GetString(dr.GetOrdinal("Gsm")),
                        DogumTarihi = dr.GetDateTime(dr.GetOrdinal("dogumTarihi")),
                        EPosta = dr.GetString(dr.GetOrdinal("ePosta")),
                        Durum = dr.GetBoolean(dr.GetOrdinal("durum")),
                        KayitTarihi = dr.GetDateTime(dr.GetOrdinal("kayitTarihi")),
                        GirisSayisi = dr.GetInt32(dr.GetOrdinal("girisSayisi")),
                        Cinsiyet = dr.GetBoolean(dr.GetOrdinal("cinsiyet")).ToString(),
                        Sehir = dr.GetString(dr.GetOrdinal("sehir")),
                        KullaniciTipi = dr.GetString(dr.GetOrdinal("kullaniciTipi")),
                        Sifre = dr.GetString(dr.GetOrdinal("sifre"))
                    };  
                }
            }

            return k;
        }

        public static Kullanici Login(string eposta, string sifre)
        {
            Kullanici info = null;
            try
            {
                SqlParameter[] prm = new SqlParameter[] 
                { 
                    new SqlParameter("@eposta", eposta),
                    new SqlParameter("@sifre", sifre)
                };

                using (SqlDataReader dr = SqlHelper.ExecuteReader("kullanici_GirisKontrol", prm))
                {
                    while (dr.Read())
                    {
                        info = new Kullanici(
                        dr.GetInt32(dr.GetOrdinal("id")),
                        dr.GetString(dr.GetOrdinal("adiSoyadi")),
                        dr.GetString(dr.GetOrdinal("ePosta")),
                        dr.GetString(dr.GetOrdinal("kullaniciTipi")),
                        dr.GetInt32(dr.GetOrdinal("siparisAdet")));
                    }
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Kullanici AdminSifreHatirlatma(string eposta)
        {
            Kullanici info = null;
            try
            {
                SqlParameter prm = new SqlParameter("@eposta", eposta);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("kullanici_AdminSifre", prm))
                {
                    while (dr.Read())
                    {
                        info = new Kullanici
                        {
                            EPosta = dr.GetString(dr.GetOrdinal("ePosta")),
                            AdiSoyadi = dr.GetString(dr.GetOrdinal("adiSoyadi")),
                            Sifre = dr.GetString(dr.GetOrdinal("sifre"))
                        };
                    }
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Kullanici> Arama(string parametre, string bolge)
        { 
            List<Kullanici> kullanici = new List<Kullanici>();
            try
            {
                SqlParameter[] prm = new SqlParameter[] 
                { 
                    new SqlParameter("@parametre", parametre),
                    new SqlParameter("@bolge", bolge)
                };

                using (SqlDataReader dr = SqlHelper.ExecuteReader("kullanici_KayitListele", prm))
                {
                    while (dr.Read())
                    {
                        kullanici.Add(new Kullanici
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            AdiSoyadi = dr.GetString(dr.GetOrdinal("adiSoyadi")),
                            Gsm = dr.GetString(dr.GetOrdinal("Gsm")),
                            DogumTarihi = dr.GetDateTime(dr.GetOrdinal("dogumTarihi")),
                            EPosta = dr.GetString(dr.GetOrdinal("ePosta")),
                            Durum = dr.GetBoolean(dr.GetOrdinal("durum")),
                            KayitTarihi = dr.GetDateTime(dr.GetOrdinal("kayitTarihi")),
                            GirisSayisi = dr.GetInt32(dr.GetOrdinal("girisSayisi")),
                            Cinsiyet = dr.GetString(dr.GetOrdinal("cinsiyet")),
                            Sehir = dr.GetString(dr.GetOrdinal("sehir")),
                            KullaniciTipi = dr.GetString(dr.GetOrdinal("kullaniciTipi")),
                            Sifre = dr.GetString(dr.GetOrdinal("sifre"))
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
           
           return kullanici;
        }

        public static List<Kullanici> TarihArama(string baslangicTarih, string bitisTarih)
        {
            List<Kullanici> kullanici = new List<Kullanici>();
            try
            {
                SqlParameter[] prm = new SqlParameter[2];
                prm[0] = new SqlParameter("@baslangicTarih", SqlDbType.DateTime);
                prm[0].Value = Convert.ToDateTime(baslangicTarih);
                prm[1] = new SqlParameter("@bitisTarih", SqlDbType.DateTime);
                prm[1].Value = Convert.ToDateTime(bitisTarih + " 23:59:59");

                using (SqlDataReader dr = SqlHelper.ExecuteReader("kullanici_TarihArama", prm))
                {
                    while (dr.Read())
                    {
                        kullanici.Add(new Kullanici
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            AdiSoyadi = dr.GetString(dr.GetOrdinal("adiSoyadi")),
                            Gsm = dr.GetString(dr.GetOrdinal("Gsm")),
                            DogumTarihi = dr.GetDateTime(dr.GetOrdinal("dogumTarihi")),
                            EPosta = dr.GetString(dr.GetOrdinal("ePosta")),
                            Durum = dr.GetBoolean(dr.GetOrdinal("durum")),
                            KayitTarihi = dr.GetDateTime(dr.GetOrdinal("kayitTarihi")),
                            GirisSayisi = dr.GetInt32(dr.GetOrdinal("girisSayisi")),
                            Cinsiyet = dr.GetString(dr.GetOrdinal("cinsiyet")),
                            Sehir = dr.GetString(dr.GetOrdinal("sehir")),
                            KullaniciTipi = dr.GetString(dr.GetOrdinal("kullaniciTipi")),
                            Sifre = dr.GetString(dr.GetOrdinal("sifre"))
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return kullanici;
        }

        public static void Durum(int id, int durum)
        {
            try
            {
               SqlParameter[] prm = new SqlParameter[2];
               prm[0] = new SqlParameter("@id", SqlDbType.Int);
               prm[0].Value = id;
               prm[1] = new SqlParameter("@durum", SqlDbType.Int);
               prm[1].Value = durum;

               SqlHelper.ExecuteNonQuery("kullanici_Durum", prm);      
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Sil(int id)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[2];
                prm[0] = new SqlParameter("@id", SqlDbType.Int);
                prm[0].Value = id;

                SqlHelper.ExecuteNonQuery("kullanici_KayitSil", prm);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int ItemCount()
        {
            try
            {
                string query = "SELECT COUNT(id) FROM tbl_kullanicilar";
                return (int)SqlHelper.ExecuteScalar(CommandType.Text, query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
