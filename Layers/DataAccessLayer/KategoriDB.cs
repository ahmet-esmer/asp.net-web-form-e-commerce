using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;

namespace DataAccessLayer
{
    public class KategoriDB:BaseDB
    {

         #region Kaydet
         public static int kaydet(string anakategori_serial, string kayitId, string kategoriAdi, string durum, string title, string description, string keywords)
         {
             int geriDonus = 0;
             try
             {

                 SqlParameter[] parameter = new SqlParameter[8];
                 parameter[0] = new SqlParameter("@anakategori_serial", SqlDbType.NVarChar);
                 parameter[0].Value = anakategori_serial;
                 parameter[1] = new SqlParameter("@kayitId", SqlDbType.Int);
                 parameter[1].Value = Convert.ToInt32(kayitId);
                 parameter[2] = new SqlParameter("@kategoriAdi", SqlDbType.NVarChar);
                 parameter[2].Value = kategoriAdi;
                 parameter[3] = new SqlParameter("@durum", SqlDbType.Int);
                 parameter[3].Value = Convert.ToBoolean(durum);
                 parameter[4] = new SqlParameter("@title", SqlDbType.NVarChar);
                 parameter[4].Value = title;
                 parameter[5] = new SqlParameter("@description", SqlDbType.NVarChar);
                 parameter[5].Value = description;
                 parameter[6] = new SqlParameter("@keywords", SqlDbType.NVarChar);
                 parameter[6].Value = keywords;
                 parameter[7] = new SqlParameter("@kayit_sinirla", SqlDbType.Int);
                 parameter[7].Direction = ParameterDirection.Output;


                 SqlHelper.ExecuteNonQuery("KategoriKaydet", parameter);

                 geriDonus = int.Parse(parameter[7].Value.ToString());
             }
             catch (Exception )
             {
                 throw ;
             }

             return geriDonus;
         }
         #endregion

         #region Listeleme Web UI
         public static List<Kategori> Liste()
         {
             try
             {
                 SqlParameter parametre = new SqlParameter("@bolge", "anaKategori");
                 List<Kategori> kategoriListe = new List<Kategori>();

                 using (SqlDataReader dr = SqlHelper.ExecuteReader("kategori_KayitListele", parametre))
                 {
                     while (dr.Read())
                     {
                         kategoriListe.Add(new Kategori
                         {
                             id = dr.GetInt32(dr.GetOrdinal("id")),
                             kategoriadi = dr.GetString(dr.GetOrdinal("kategoriadi")),
                             serial = dr.GetString(dr.GetOrdinal("serial")),
                             title = dr.GetString(dr.GetOrdinal("title"))
                         });
                     }
                 }

                 return kategoriListe;
             }
             catch (Exception)
             {
                 throw;
             }
         }
         #endregion

         #region Listeleme
         public static List<Kategori> listeleme()
         {
             try
             {
                 SqlParameter parametre = new SqlParameter("@bolge", "admin");
                 List<Kategori> KategoriTablo = new List<Kategori>();
                 using (SqlDataReader dr = SqlHelper.ExecuteReader("kategori_KayitListele", parametre))
                 {

                     while (dr.Read())
                     {
                         Kategori info = new Kategori(
                             dr.GetInt32(dr.GetOrdinal("id")),
                             dr.GetInt32(dr.GetOrdinal("anaId")),
                             dr.GetString(dr.GetOrdinal("kategoriadi")),
                             dr.GetBoolean(dr.GetOrdinal("durum")),
                             dr.GetInt32(dr.GetOrdinal("sira")),
                             dr.GetString(dr.GetOrdinal("serial")));
                         KategoriTablo.Add(info);
                     }
                 }

                 return KategoriTablo;
             }
             catch (Exception)
             {
                 throw;
             }
         }
         #endregion

         #region Listeleme
         public static List<Kategori> dropDownListele()
         {
             try
             {
                 SqlParameter parametre = new SqlParameter("@bolge", "AdminDropDown");
                 using (SqlDataReader dr = SqlHelper.ExecuteReader("kategori_KayitListele", parametre))
                 {
                     string serial = null;
                     List<Kategori> KategoriTablo = new List<Kategori>();

                     while (dr.Read())
                     {
                         serial = dr.GetString(dr.GetOrdinal("serial"));

                         Kategori info = new Kategori(dr.GetInt32(dr.GetOrdinal("id")), BusinessLayer.Fonksiyonlar.KategoriCizgiDrop(serial)
                         + dr.GetString(dr.GetOrdinal("kategoriadi")), serial);
                         KategoriTablo.Add(info);
                     }

                     return KategoriTablo;
                 }
             }
             catch (Exception)
             {
                 throw;
             }
         }
         #endregion

         #region Getirme
         public static Kategori getir(int id)
         {
             Kategori kategoriTablo = new Kategori();
             try
             {
                SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@parametre", "admin"),
                    new SqlParameter("@id", id),
                };

                using (SqlDataReader dr = SqlHelper.ExecuteReader("kategori_KayitGetir", parametre))
                {
                    while (dr.Read())
                    {
                        kategoriTablo = new Kategori(
                           dr.GetInt32(dr.GetOrdinal("id")),
                           dr.GetInt32(dr.GetOrdinal("anaId")),
                           dr.GetString(dr.GetOrdinal("kategoriadi")),
                           dr.GetBoolean(dr.GetOrdinal("durum")),
                           dr.GetInt32(dr.GetOrdinal("sira")),
                           dr.GetString(dr.GetOrdinal("serial")),
                           dr.GetString(dr.GetOrdinal("resimAdi")),
                           dr.GetString(dr.GetOrdinal("title")),
                           dr.GetString(dr.GetOrdinal("description")),
                           dr.GetString(dr.GetOrdinal("keywords")));
                    }
                }

                return kategoriTablo;
             }
             catch (Exception)
             {
                 throw;
             }
         }
         #endregion

         #region Kategori Flash Silme
         public static void flashSil(string resimAdi)
         {
             try
             {
                string query = "DELETE FROM dbo.tbl_Resimler WHERE resim_adi='"+resimAdi+"'";
                SqlHelper.ExecuteNonQuery(CommandType.Text, query);
             }
             catch (Exception)
             {
                 throw;
             }
         }
        #endregion

         #region Durum
         public static void durum(string id, string durum)
         {
             try
             {
                SqlParameter[] parametre = new SqlParameter[]
                {
                    new SqlParameter("@id", id),
                    new SqlParameter("@durum", durum)
                };
                SqlHelper.ExecuteNonQuery("Kategori_Durum", parametre);
             }
             catch (Exception)
             {
                 throw;
             }
         }
         #endregion

         public static List<Resim> ResimAdiListeleSilme(int id)
         {
             try
             {
                SqlParameter sqlParamResim = new SqlParameter("@id", id);

                List<Resim> resimListe = new List<Resim>();

                using (SqlDataReader dr = SqlHelper.ExecuteReader("kategori_Resim_Sil", sqlParamResim))
                {
                    while (dr.Read())
                    {
                        resimListe.Add(new Resim
                        {
                            resimAdi = dr.GetString(dr.GetOrdinal("resim_adi"))
                        });
                    }  
                }
              
                return resimListe;
             }
             catch (Exception)
             {
                 throw;
             }
         }

         public static void Delete(int id)
         {
             try
             {
                 SqlParameter parametre = new SqlParameter("@id", id);
                 SqlHelper.ExecuteNonQuery("kategori_Sil", parametre);
             }
             catch (Exception)
             {
                 throw;
             }
         }

         #region Kategori Metağ
         public static MetaTag MetaTag(string serial)
         {
             try
             {
                 MetaTag metaTag = new MetaTag();
                 SqlParameter parametre = new SqlParameter("@serial", serial);

                 using (SqlDataReader dr = SqlHelper.ExecuteReader("kategori_MetaGetir", parametre))
                 {
                     while (dr.Read())
                     {
                         metaTag.Title = dr.GetString(dr.GetOrdinal("title"));
                         metaTag.Description = dr.GetString(dr.GetOrdinal("description"));
                         metaTag.Keywords = dr.GetString(dr.GetOrdinal("keywords"));
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

         #region Altkategori Listeleme İşlemi
         public static List<Kategori> AltLKategoriler(string serial)
         {

             List<Kategori> altKategori = new List<Kategori>();
             SqlParameter[] parametre = new SqlParameter[] 
             { 
                    new SqlParameter("@serial", serial),
                    new SqlParameter("@bolge", "anaKat"),
             };

             using (SqlDataReader dr = SqlHelper.ExecuteReader("kategori_altKategoriListele", parametre))
             {
                 
                 while (dr.Read())
                 {
                     if (dr.GetString(dr.GetOrdinal("serial")) != serial)
                     {
                         altKategori.Add(new Kategori
                         {
                             id = dr.GetInt32(dr.GetOrdinal("id")),
                             kategoriadi = dr.GetString(dr.GetOrdinal("kategoriadi")),
                             serial = dr.GetString(dr.GetOrdinal("serial")),
                             title = dr.GetString(dr.GetOrdinal("title"))
                         });
                     }
                 }
             }

             return altKategori;
         }
         #endregion
    }
}
