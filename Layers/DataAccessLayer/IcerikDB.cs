using System;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;

namespace DataAccessLayer
{
    public  class IcerikDB :BaseDB
    {
        #region Listeleme Web Id ye Göre
        public static Icerik IcerikGetir(string bolge, int id)
        {
            Icerik Icerik = new Icerik();
            try
            {
                SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@paremetre", bolge),
                    new SqlParameter("@id", Convert.ToInt32(id))
                };

                using (SqlDataReader dr = SqlHelper.ExecuteReader("icerik_Getir", parametre))
                {
                    while (dr.Read())
                    {
                        Icerik = new Icerik
                        {
                            title = dr.GetString(dr.GetOrdinal("title")),
                            description = dr.GetString(dr.GetOrdinal("description")),
                            keywords = dr.GetString(dr.GetOrdinal("keywords")),
                            icerik = dr.GetString(dr.GetOrdinal("icerik")),
                            sayfaBaslik = dr.GetString(dr.GetOrdinal("kategoriadi"))
                        };
                    }
                }

                return Icerik;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Sayfa Meta Tağ 
        public static MetaTag MetaTag(int id)
        {
            MetaTag metaTag = new MetaTag();
            try
            {
                SqlParameter parametre = new SqlParameter("@id", id);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("icerik_MeteTag", parametre))
                {
                    while (dr.Read())
                    {
                        metaTag = new MetaTag
                        {
                            Title = dr.GetString(dr.GetOrdinal("title")),
                            Description = dr.GetString(dr.GetOrdinal("description")),
                            Keywords = dr.GetString(dr.GetOrdinal("keywords"))
                        };
                    }
                }

                return metaTag;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Listeleme Web Sayfa Adına Göre
        public static Icerik IcerikGetir(string bolge)
        {
            Icerik Icerik = new Icerik();
            try
            {
                SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@paremetre", bolge),
                    new SqlParameter("@id", 1)
                };

                using (SqlDataReader dr = SqlHelper.ExecuteReader("icerik_Getir", parametre))
                {
                    while (dr.Read())
                    {
                        Icerik = new Icerik
                        {
                            title = dr.GetString(dr.GetOrdinal("title")),
                            description = dr.GetString(dr.GetOrdinal("description")),
                            keywords = dr.GetString(dr.GetOrdinal("keywords")),
                            icerik = dr.GetString(dr.GetOrdinal("icerik")),
                            sayfaBaslik = dr.GetString(dr.GetOrdinal("kategoriadi"))// Makale Sayfa Başlık
                        };
                    }
                }

                return Icerik;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public static DataSet GetById(int id)
        {
            SqlParameter[] parametre = new SqlParameter[] 
            { 
               new SqlParameter("@paremetre", "admin"),
               new SqlParameter("@id", id)
            };

            return SqlHelper.ExecuteDataset("icerik_Getir", parametre);
        }

        public static DataSet SayafaResimAdiListeleAdmin(int id)
        {
            SqlParameter[] parametre = new SqlParameter[] 
            { 
               new SqlParameter("@id", id)
            };

            return SqlHelper.ExecuteDataset("sayfa_Resim_Admin", parametre);

        }

        public static void SayafaResimSil(int id)
        {

            SqlParameter[] parametre = new SqlParameter[] 
            { 
               new SqlParameter("@id", id)
            };

            SqlHelper.ExecuteNonQuery("resim_Sil", parametre);

        }

        public static DataSet SayafaResimAdiListeleSilme(int id)
        {
            SqlParameter[] parametre = new SqlParameter[] 
            { 
               new SqlParameter("@id", id)
            };

            return SqlHelper.ExecuteDataset("icerik_Resim_Adi_Listele_Sil", parametre);
        }

        public static void GenelSiralama(int id, string prm, string tablo)
        {
            SqlParameter[] parametre = new SqlParameter[] 
            { 
                new SqlParameter("@id",id),
                new SqlParameter("@parametre", prm),
                new SqlParameter("@tablo", tablo),
            };

            SqlHelper.ExecuteNonQuery("genel_Siralama", parametre);
        }

    }
}
