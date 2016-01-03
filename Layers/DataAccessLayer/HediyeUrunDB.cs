using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;

namespace DataAccessLayer
{
    public class HediyeUrunDB :BaseDB
    {

        public static void TitleUpdate(UrunHediyeBaslik baslik)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[5];
                prm[0] = new SqlParameter("@id", SqlDbType.NVarChar);
                prm[0].Value = baslik.Id;
                prm[1] = new SqlParameter("@title", SqlDbType.NVarChar);
                prm[1].Value = baslik.Title;
                prm[2] = new SqlParameter("@limit", SqlDbType.Int);
                prm[2].Value = baslik.Limit;
                prm[3] = new SqlParameter("@resim", SqlDbType.NVarChar);
                prm[3].Value = baslik.Resim;
                prm[4] = new SqlParameter("@durum", SqlDbType.Bit);
                prm[4].Value = baslik.Durum;

                SqlHelper.ExecuteNonQuery("urun_HediyeDuzenle", prm);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static UrunHediyeBaslik GetTitle(int id)
        {
            try
            {
                UrunHediyeBaslik baslik = new UrunHediyeBaslik();
                SqlParameter prm = new SqlParameter("@id", id);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_HediyeGetir", prm))
                {
                    while (dr.Read())
                    {
                        baslik.Id = dr.GetInt32(dr.GetOrdinal("id"));
                        baslik.Title = dr.GetString(dr.GetOrdinal("title"));
                        baslik.Limit = dr.GetInt32(dr.GetOrdinal("limit"));
                        baslik.Durum = dr.GetBoolean(dr.GetOrdinal("durum"));
                        baslik.Resim = dr.GetString(dr.GetOrdinal("resim")); 
                    }
                }

                return baslik;
            }
            catch (Exception)
            {

                throw;
            }
        
        
        }

        public static IList<UrunHediyeBaslik> TitleListFor(string urunId)
        {
            try
            {
                IList<UrunHediyeBaslik> baslik = new List<UrunHediyeBaslik>();

                SqlParameter prm = new SqlParameter("@urunId", urunId);
                using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_HediyeGetirUrunIle", prm))
                {

                    while (dr.Read())
                    {
                        baslik.Add(new UrunHediyeBaslik
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            Title = dr.GetString(dr.GetOrdinal("title")),
                            Limit = dr.GetInt32(dr.GetOrdinal("limit"))
                        });
                    }
                }

                return baslik;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IList<UrunHediyeBaslik> TitleList(string bolge)
        {
            try
            {
                string query;

                if (bolge == "web")
                {
                    query = "SELECT id, title, limit,resim, durum  FROM tbl_urunHediye WHERE durum='1'";  
                }
                else
                {
                    query = "SELECT id, title, limit,resim, durum FROM tbl_urunHediye";  
                }

                IList<UrunHediyeBaslik> baslik = new List<UrunHediyeBaslik>();

                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, query ))
                {

                    while (dr.Read())
                    {
                        baslik.Add(new UrunHediyeBaslik
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            Title = dr.GetString(dr.GetOrdinal("title")),
                            Limit = dr.GetInt32(dr.GetOrdinal("limit")),
                            Durum = dr.GetBoolean(dr.GetOrdinal("durum")),
                            Resim = dr.GetString(dr.GetOrdinal("resim")),
                            Link = BusinessLayer.LinkBulding.Hediye(dr.GetInt32(dr.GetOrdinal("id")),
                             dr.GetString(dr.GetOrdinal("title")))
                        });
                    }
                }

                return baslik;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int TitleSave(UrunHediyeBaslik hediye)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[4];
                prm[0] = new SqlParameter("@title", SqlDbType.NVarChar);
                prm[0].Value = hediye.Title;
                prm[1] = new SqlParameter("@limit", SqlDbType.Int);
                prm[1].Value = hediye.Limit;
                prm[2] = new SqlParameter("@resim", SqlDbType.NVarChar);
                prm[2].Value = hediye.Resim;
                prm[3] = new SqlParameter("@retValue", SqlDbType.Int);
                prm[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery("urun_HediyeEkle", prm);

                return Convert.ToInt32(prm[3].Value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Ürün detay kısmında ajax tarafında çağrılacak metod
        /// </summary>
        /// <param name="urunId">Ürüne ait hediye kampanyası varmı </param>
        /// <param name="adet"> seçilen adet miktarına göre ilgili kampanyanı getirilmesi</param>
        /// <returns></returns>
        public static IList<UrunHediye> GiftList(int urunId, int adet, int uyeId)
        {
            try
            {
                IList<UrunHediye> hediye = new List<UrunHediye>();

                SqlParameter[] prm = new SqlParameter[3];
                prm[0] = new SqlParameter("@urunId", SqlDbType.NVarChar);
                prm[0].Value = urunId;
                prm[1] = new SqlParameter("@adet", SqlDbType.Int);
                prm[1].Value = adet;
                prm[2] = new SqlParameter("@uyeId", SqlDbType.Int);
                prm[2].Value = uyeId;

                using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_HediyeGetirUrunWeb", prm))
                {
                    while (dr.Read())
                    {
                        hediye.Add(new UrunHediye
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            BaslikAdi = dr.GetString(dr.GetOrdinal("title")),
                            UrunAdi = dr.GetString(dr.GetOrdinal("urunAdi")),
                            Resim = dr.GetString(dr.GetOrdinal("resim")),
                            Marka = dr.GetString(dr.GetOrdinal("marka")),
                            Adet = dr.GetInt32(dr.GetOrdinal("adet")),
                            Secenekler = GetSecenekList(dr.GetString(dr.GetOrdinal("secenek")))
                        });
                    }
                }

                return hediye;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Secenek> GetSecenekList(string secen)
        {
            List<Secenek> _secenekler = new List<Secenek>();

            string[] secenekler = null;
            string[] secenekDeger = null;

            if (secen != "")
            {
                secenekler = secen.Split('|');
            }

            if (secenekler != null)
            {
                foreach (var item in secenekler)
                {
                    if (item != null)
                    {
                        secenekDeger = item.Split(':');

                        _secenekler.Add(new Secenek
                        {
                            Value = item,
                            Name = secenekDeger[1]

                        });
                    }
                }
            }

            return _secenekler;
              
        }


        public static IList<UrunHediye> GiftList(int baslikId)
        {
            try
            {
                IList<UrunHediye> hediye = new List<UrunHediye>();

                SqlParameter prm = new SqlParameter("@baslikId", baslikId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_HediyeDetayListe", prm))
                {
                    while (dr.Read())
                    {
                        hediye.Add(new UrunHediye
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            BaslikId = dr.GetInt32(dr.GetOrdinal("baslikId")),
                            UrunAdi = dr.GetString(dr.GetOrdinal("urunAdi")),
                            Resim = dr.GetString(dr.GetOrdinal("resim")),
                            Marka = dr.GetString(dr.GetOrdinal("marka")),
                            Adet = dr.GetInt32(dr.GetOrdinal("adet")),
                            Durum = dr.GetBoolean(dr.GetOrdinal("durum"))
                        });
                    }
                }

                return hediye;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static UrunHediye GetGift(string baslikId)
        {
            try
            {
                UrunHediye hediye = new UrunHediye();

                SqlParameter prm = new SqlParameter("@id", baslikId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_HediyeDetayGetir", prm))
                {
                    while (dr.Read())
                    {
                        hediye.Id = dr.GetInt32(dr.GetOrdinal("id"));
                        hediye.BaslikId = dr.GetInt32(dr.GetOrdinal("baslikId"));
                        hediye.UrunAdi = dr.GetString(dr.GetOrdinal("urunAdi"));
                        hediye.Resim = dr.GetString(dr.GetOrdinal("resim"));
                        hediye.Marka = dr.GetString(dr.GetOrdinal("marka"));
                        hediye.Secenek = dr.GetString(dr.GetOrdinal("secenek"));
                        hediye.Adet = dr.GetInt32(dr.GetOrdinal("adet"));
                        hediye.Durum = dr.GetBoolean(dr.GetOrdinal("durum"));
                    }
                }

                return hediye;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public static void GiftSave(UrunHediye hediye)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[7];
                prm[0] = new SqlParameter("@urunAdi", SqlDbType.NVarChar);
                prm[0].Value = hediye.UrunAdi;
                prm[1] = new SqlParameter("@baslikId", SqlDbType.Int);
                prm[1].Value = hediye.BaslikId;
                prm[2] = new SqlParameter("@adet", SqlDbType.Int);
                prm[2].Value = hediye.Adet;
                prm[3] = new SqlParameter("@resim", SqlDbType.NVarChar);
                prm[3].Value = hediye.Resim;
                prm[4] = new SqlParameter("@marka", SqlDbType.NVarChar);
                prm[4].Value = hediye.Marka;
                prm[5] = new SqlParameter("@secenek", SqlDbType.NVarChar);
                prm[5].Value = hediye.Secenek;
                prm[6] = new SqlParameter("@durum", SqlDbType.Bit);
                prm[6].Value = hediye.Durum;

                SqlHelper.ExecuteNonQuery("urun_HediyeDetayEkle", prm);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void GiftUpdate(UrunHediye hediye)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[8];

                prm[0] = new SqlParameter("@id", SqlDbType.Int);
                prm[0].Value = hediye.Id;
                prm[1] = new SqlParameter("@urunAdi", SqlDbType.NVarChar);
                prm[1].Value = hediye.UrunAdi;
                prm[2] = new SqlParameter("@adet", SqlDbType.Int);
                prm[2].Value = hediye.Adet;
                prm[3] = new SqlParameter("@resim", SqlDbType.NVarChar);
                prm[3].Value = hediye.Resim;
                prm[4] = new SqlParameter("@marka", SqlDbType.NVarChar);
                prm[4].Value = hediye.Marka;
                prm[5] = new SqlParameter("@secenek", SqlDbType.NVarChar);
                prm[5].Value = hediye.Secenek;
                prm[6] = new SqlParameter("@durum", SqlDbType.Bit);
                prm[6].Value = hediye.Durum;

                SqlHelper.ExecuteNonQuery("urun_HediyeDetayDuzenle", prm);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void GiftDelete(string id)
        {
            try
            {
                string query = "DELETE FROM tbl_urunHediyeDetay WHERE id="+ id;

                SqlHelper.ExecuteNonQuery(CommandType.Text,query);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void TitleDelete(int baslikId)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[1];
                prm[0] = new SqlParameter("@id", SqlDbType.Int);
                prm[0].Value = baslikId;

                SqlHelper.ExecuteNonQuery("urun_HediyeSil", prm);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void TitleDeleteFor(int urunId, int kampanyaId)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[2];
                prm[0] = new SqlParameter("@urunId", SqlDbType.Int);
                prm[0].Value = urunId;
                prm[1] = new SqlParameter("@kampanyaId", SqlDbType.Int);
                prm[1].Value = kampanyaId;
                
                SqlHelper.ExecuteNonQuery("urun_HediyeSilUrunIle", prm);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
