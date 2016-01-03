using System.Collections.Generic;
using System.Data.SqlClient;
using ModelLayer;

namespace DataAccessLayer
{
    public class BankaHesaplariDB :BaseDB
    {
        public static BankaHesaplari GetName(string bankaAdi)
        {
            SqlParameter param = new SqlParameter("@bankaAdi", bankaAdi);
            BankaHesaplari hesap = new BankaHesaplari();
            using (SqlDataReader dr = SqlHelper.ExecuteReader("banka_HesapGetir", param))
            {
                while (dr.Read())
                {
                    hesap = new BankaHesaplari
                    {
                        BankaAdi = dr.GetString(dr.GetOrdinal("bankaAdi")),
                        HesapAdi = dr.GetString(dr.GetOrdinal("hesapAdi")),
                        Sube = dr.GetString(dr.GetOrdinal("sube")),
                        SubeKod = dr.GetString(dr.GetOrdinal("subeKod")),
                        HesapNo = dr.GetString(dr.GetOrdinal("hesapNo")),
                        Iban = dr.GetString(dr.GetOrdinal("iban")),
                        HesapTipi = dr.GetString(dr.GetOrdinal("hesapTipi"))
                    };
                }
            }

            return hesap;
        }

        public static List<BankaHesaplari> HesapListe(string parametre)
        {
            List<BankaHesaplari> bankaHesaplari = new List<BankaHesaplari>();

            SqlParameter prm = new SqlParameter("@parametre", parametre);

            using (SqlDataReader dr = SqlHelper.ExecuteReader("banka_HesapListele", prm))
            {
                while (dr.Read())
                {

                    //bankaHesaplari.Add(new BankaHesaplari(
                    //   dr.GetInt32(dr.GetOrdinal("id")),
                    //   dr.GetString(dr.GetOrdinal("bankaAdi")),
                    //   dr.GetString(dr.GetOrdinal("sube")),
                    //   dr.GetString(dr.GetOrdinal("subeKod")),
                    //   dr.GetString(dr.GetOrdinal("hesapNo")),
                    //   dr.GetString(dr.GetOrdinal("iban")),
                    //   dr.GetString(dr.GetOrdinal("hesapAdi")),
                    //   dr.GetString(dr.GetOrdinal("hesapTipi")),
                    //   dr.GetBoolean(dr.GetOrdinal("durum"))));


                    bankaHesaplari.Add(new BankaHesaplari
                    {
                        BankaAdi = dr.GetString(dr.GetOrdinal("bankaAdi")),
                        Durum = dr.GetBoolean(dr.GetOrdinal("durum")),
                        HesapAdi = dr.GetString(dr.GetOrdinal("hesapAdi")),
                        HesapNo = dr.GetString(dr.GetOrdinal("hesapNo")),
                        HesapTipi = dr.GetString(dr.GetOrdinal("hesapTipi")),
                        Iban = dr.GetString(dr.GetOrdinal("iban")),
                        Id = dr.GetInt32(dr.GetOrdinal("id")),
                        Sube = dr.GetString(dr.GetOrdinal("sube")),
                        SubeKod = dr.GetString(dr.GetOrdinal("subeKod"))

                    });

                }
            }

            return bankaHesaplari;
        }
    }
}
