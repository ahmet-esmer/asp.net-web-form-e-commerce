using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer;
using ModelLayer;

namespace DataAccessLayer
{
    public class GunuUrunuDB: BaseDB
    {
        public static List<GununUrunu> Listele(string bolge)
        {
            List<GununUrunu> gununUrunleri = new List<GununUrunu>();
            SqlParameter sqlp = new SqlParameter("@bolge", bolge );

            using (SqlDataReader dr = SqlHelper.ExecuteReader("gununUrunu_Listele", sqlp))
            {
                while (dr.Read())
                {
                    gununUrunleri.Add(new GununUrunu
                    {
                        ResimAdi = dr.GetString(dr.GetOrdinal("resimAdi")),
                        UrunAdi = dr.GetString(dr.GetOrdinal("urunAdi")),
                        KategoriAdi = dr.GetString(dr.GetOrdinal("kategoriadi")),
                        Id = dr.GetInt32(dr.GetOrdinal("id")),
                        Durum = dr.GetBoolean(dr.GetOrdinal("durum")),
                        UrunStok = dr.GetInt32(dr.GetOrdinal("urunStok")),
                        KiritikStok = dr.GetInt32(dr.GetOrdinal("kiritikStok")),
                        StokCins = dr.GetString(dr.GetOrdinal("stokCins")),
                        UrunFiyat = dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                        Tarih = dr.GetDateTime(dr.GetOrdinal("tarih")),
                        GunId = dr.GetInt32(dr.GetOrdinal("gunId")),
                        Doviz = dr.GetString(dr.GetOrdinal("doviz")),
                        KDV  = dr.GetInt32(dr.GetOrdinal("urunKDV")),
                        SatisFiyat = dr.GetDecimal(dr.GetOrdinal("satisFiyat")),
                        Aciklama = dr.GetString(dr.GetOrdinal("aciklama")),
                        IndirimYuzde = dr.GetInt32(dr.GetOrdinal("indirimYuzde")),
                        Link = LinkBulding.Urun(
                          dr.GetString(dr.GetOrdinal("kategoriadi")),
                          dr.GetInt32(dr.GetOrdinal("id")),
                          dr.GetString(dr.GetOrdinal("urunAdi")))
                    });
                }
            }

            return gununUrunleri;
        }

        public static GununUrunu Get()
        {
            GununUrunu urun = new GununUrunu();

            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "gununUrunu_Getir"))
            {
                while (dr.Read())
                {
                    urun.ResimAdi = dr.GetString(dr.GetOrdinal("resimAdi"));
                    urun.UrunAdi = dr.GetString(dr.GetOrdinal("urunAdi"));
                    urun.Id = dr.GetInt32(dr.GetOrdinal("id"));
                    urun.UrunFiyat = dr.GetDecimal(dr.GetOrdinal("urunFiyat"));
                    urun.UIndirimFiyat = dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat"));
                    urun.IndirimYuzde = dr.GetInt32(dr.GetOrdinal("indirimYuzde"));
                    urun.Doviz = dr.GetString(dr.GetOrdinal("doviz"));
                    urun.KategoriAdi = dr.GetString(dr.GetOrdinal("kategoriadi"));
                    urun.KDV = dr.GetInt32(dr.GetOrdinal("urunKDV"));
                }
            }

            return urun;
        }

    }
}
