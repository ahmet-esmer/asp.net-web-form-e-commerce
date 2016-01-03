using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;

namespace DataAccessLayer
{

    public class KullaniciFaturaDB : BaseDB
    {

        public static void Sil(int uyeId, int faturaId)
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@uyeId", SqlDbType.Int);
            parameter[0].Value = uyeId;
            parameter[1] = new SqlParameter("@faturaId", SqlDbType.NVarChar);
            parameter[1].Value = faturaId;

            SqlHelper.ExecuteNonQuery("kullanici_FaturaSil", parameter);

        }

        public static int kaydet(KullaniciFatura fatura)
        {
            int geriDonus = 0;
            try
            {
                SqlParameter[] parameter = new SqlParameter[9];
                parameter[0] = new SqlParameter("@uyeId", SqlDbType.Int);
                parameter[0].Value = fatura.UyeId;
                parameter[1] = new SqlParameter("@unvan", SqlDbType.NVarChar);
                parameter[1].Value =  fatura.Unvan;
                parameter[2] = new SqlParameter("@vergiNo", SqlDbType.NVarChar);
                parameter[2].Value = fatura.VergiNo;
                parameter[3] = new SqlParameter("@vergiDairesi", SqlDbType.NVarChar);
                parameter[3].Value =  fatura.VergiDairesi;
                parameter[4] = new SqlParameter("@faturaAdresi", SqlDbType.NVarChar);
                parameter[4].Value = fatura.FaturaAdresi;
                parameter[5] = new SqlParameter("@adSoyad", SqlDbType.NVarChar);
                parameter[5].Value =  fatura.AdSoyad;
                parameter[6] = new SqlParameter("@tcNo", SqlDbType.NVarChar);
                parameter[6].Value = fatura.TCNo;
                parameter[7] = new SqlParameter("@faturaCinsi", SqlDbType.Bit);
                parameter[7].Value = fatura.FaturaCinsi;

                parameter[8] = new SqlParameter("@deger_dondur", SqlDbType.Int);
                parameter[8].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery("kullanici_FaturaEkle", parameter);

                geriDonus = Convert.ToInt32(parameter[8].Value);
            }
            catch (Exception)
            {
                throw;
            }

            return geriDonus;
        }

        public static void Duzenle(KullaniciFatura fatura)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[9];
                parameter[0] = new SqlParameter("@id", SqlDbType.Int);
                parameter[0].Value = fatura.Id;
                parameter[1] = new SqlParameter("@unvan", SqlDbType.NVarChar);
                parameter[1].Value = fatura.Unvan;
                parameter[2] = new SqlParameter("@vergiNo", SqlDbType.NVarChar);
                parameter[2].Value = fatura.VergiNo;
                parameter[3] = new SqlParameter("@vergiDairesi", SqlDbType.NVarChar);
                parameter[3].Value = fatura.VergiDairesi;
                parameter[4] = new SqlParameter("@faturaAdresi", SqlDbType.NVarChar);
                parameter[4].Value = fatura.FaturaAdresi;
                parameter[5] = new SqlParameter("@adSoyad", SqlDbType.NVarChar);
                parameter[5].Value = fatura.AdSoyad;
                parameter[6] = new SqlParameter("@tcNo", SqlDbType.NVarChar);
                parameter[6].Value = fatura.TCNo;
                parameter[7] = new SqlParameter("@faturaCinsi", SqlDbType.Bit);
                parameter[7].Value = fatura.FaturaCinsi;

                SqlHelper.ExecuteNonQuery("kullanici_FaturaGuncelle", parameter);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static KullaniciFatura Getir(int faturaId)
        {
            KullaniciFatura f = new KullaniciFatura();

            try
            {
                SqlParameter parametre = new SqlParameter("@id", faturaId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("kullanici_FaturaBilgiGetir", parametre))
                {
                    while (dr.Read())
                    {
                        f.Id = dr.GetInt32(dr.GetOrdinal("id"));
                        f.FaturaCinsi = dr.GetBoolean(dr.GetOrdinal("faturaCinsi"));
                        f.FaturaAdresi = dr.GetString(dr.GetOrdinal("faturaAdresi"));

                        if (!dr.IsDBNull(dr.GetOrdinal("unvan")))
                            f.Unvan = dr.GetString(dr.GetOrdinal("unvan"));

                        if (!dr.IsDBNull(dr.GetOrdinal("tcNo")))
                        f.TCNo = dr.GetString(dr.GetOrdinal("tcNo"));

                        if (!dr.IsDBNull(dr.GetOrdinal("adSoyad")))
                        f.AdSoyad = dr.GetString(dr.GetOrdinal("adSoyad"));

                        if (!dr.IsDBNull(dr.GetOrdinal("vergiDairesi")))
                        f.VergiDairesi = dr.GetString(dr.GetOrdinal("vergiDairesi"));

                        if (!dr.IsDBNull(dr.GetOrdinal("vergiNo")))
                        f.VergiNo = dr.GetString(dr.GetOrdinal("vergiNo"));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return f;
        }


        public static List<KullaniciFatura> Liste(int uyeId)
        {
            List<KullaniciFatura> faturaTablo = new List<KullaniciFatura>();

            try
            {
                SqlParameter parametre = new SqlParameter("@id", uyeId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("kullanici_FaturaListele", parametre))
                {
                    while (dr.Read())
                    {
                        faturaTablo.Add(new KullaniciFatura
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            FaturaCinsi = dr.GetBoolean(dr.GetOrdinal("faturaCinsi")),
                            FaturaAdresi = dr.GetString(dr.GetOrdinal("faturaAdresi")),

                            Unvan = dr.IsDBNull(dr.GetOrdinal("unvan")) ? " " : dr.GetString(dr.GetOrdinal("unvan")),
                            TCNo = dr.IsDBNull(dr.GetOrdinal("tcNo")) ? " " : dr.GetString(dr.GetOrdinal("tcNo")),
                            AdSoyad = dr.IsDBNull(dr.GetOrdinal("adSoyad")) ? " " : dr.GetString(dr.GetOrdinal("adSoyad")),
                            VergiDairesi = dr.IsDBNull(dr.GetOrdinal("vergiDairesi")) ? " " : dr.GetString(dr.GetOrdinal("vergiDairesi")),
                            VergiNo = dr.IsDBNull(dr.GetOrdinal("vergiNo")) ? " " : dr.GetString(dr.GetOrdinal("vergiNo"))

                        });
                    }
                }   
            }
            catch (Exception)
            {
                throw;
            }

            return faturaTablo;
        }
    }
}
