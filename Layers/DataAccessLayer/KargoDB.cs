using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLayer;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class KargoDB :BaseDB
    {
        public static List<Kargo> Liste(string parametre)
        {
            List<Kargo> kargoTablo = new List<Kargo>();

            SqlParameter sqlParamResim = new SqlParameter("@parametre", parametre);

            using (SqlDataReader dr = SqlHelper.ExecuteReader("kargo_KayitListele", sqlParamResim))
            {
                while (dr.Read())
                {
                    Kargo info = new Kargo(
                        dr.GetInt32(dr.GetOrdinal("id")),
                        dr.GetString(dr.GetOrdinal("kargoAdi")),
                        dr.GetDecimal(dr.GetOrdinal("desi_1_3")),
                        dr.GetDecimal(dr.GetOrdinal("desi_4_10")),
                        dr.GetDecimal(dr.GetOrdinal("desi_11_20")),
                        dr.GetDecimal(dr.GetOrdinal("desi_21_30")),
                        dr.GetDecimal(dr.GetOrdinal("desi_31_40")),
                        dr.GetDecimal(dr.GetOrdinal("desi_41_50")),
                        dr.GetDecimal(dr.GetOrdinal("desi_50")),
                        dr.GetBoolean(dr.GetOrdinal("durum")),
                        dr.GetBoolean(dr.GetOrdinal("kapidaOdeme")),
                        dr.GetDecimal(dr.GetOrdinal("kapidaOdemeFark")));

                    kargoTablo.Add(info);
                }
            }

            return kargoTablo;
        }


        #region  Kargo Bilgisi  Listele
        public static decimal KargoFiyat(int uyeId)
        {
            try
            {
                SqlParameter prm = new SqlParameter("@uyeId", uyeId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("kargo_KayitGetir_1", prm))
                {
                    int desi = 0;
                    decimal kargoFiyat = 0;
                    decimal KargoKDV = 0;

                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(dr.GetOrdinal("desiMiktari")))
                        {
                            desi = dr.GetInt32(dr.GetOrdinal("desiMiktari"));
                        }

                        if (desi == 0)
                        {
                            return kargoFiyat;
                        }
                        else if (desi >= 1 && desi <= 3)
                        {
                            kargoFiyat = dr.GetDecimal(dr.GetOrdinal("desi_1_3"));
                        }
                        else if (desi >= 4 && desi <= 10)
                        {
                            kargoFiyat = dr.GetDecimal(dr.GetOrdinal("desi_4_10"));
                        }
                        else if (desi >= 11 && desi <= 20)
                        {
                            kargoFiyat = dr.GetDecimal(dr.GetOrdinal("desi_11_20"));
                        }
                        else if (desi >= 21 && desi <= 30)
                        {
                            kargoFiyat = dr.GetDecimal(dr.GetOrdinal("desi_21_30"));
                        }
                        else if (desi >= 31 && desi <= 40)
                        {
                            kargoFiyat = dr.GetDecimal(dr.GetOrdinal("desi_31_40"));
                        }
                        else if (desi >= 41 && desi <= 50)
                        {
                            kargoFiyat = dr.GetDecimal(dr.GetOrdinal("desi_41_50"));
                        }
                        else if (desi >= 51)
                        {
                            kargoFiyat = dr.GetDecimal(dr.GetOrdinal("desi_50"));
                        }

                        // Kargo KDV Hesaplama
                        KargoKDV = kargoFiyat * 18 / 100;
                        kargoFiyat = kargoFiyat + KargoKDV;
                    }

                    return kargoFiyat;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
