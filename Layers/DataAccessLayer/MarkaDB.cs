using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;
using BusinessLayer;


namespace DataAccessLayer
{
   public class MarkaDB: BaseDB
    {
       public static void Guncelle(int markaId, string markaAdi, Boolean durum, Boolean disbrutor)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[4];
                prm[0] = new SqlParameter("@id", SqlDbType.Int);
                prm[0].Value = markaId ;
                prm[1] = new SqlParameter("@marka_adi", SqlDbType.NVarChar);
                prm[1].Value = markaAdi;
                prm[2] = new SqlParameter("@disbrutor", SqlDbType.NVarChar);
                prm[2].Value = disbrutor;
                prm[3] = new SqlParameter("@durum", SqlDbType.NVarChar);
                prm[3].Value = durum;

                SqlHelper.ExecuteNonQuery("marka_Guncelle", prm);
            }
            catch (Exception)
            {
                throw;
            }
        }

       public static int Kaydet(string markaAdi, Boolean durum, Boolean disbrutor)
        {
            int geriDonus = 1;
            try
            {
                SqlParameter[] prm = new SqlParameter[4];
                prm[0] = new SqlParameter("@marka_adi", SqlDbType.NVarChar);
                prm[0].Value = markaAdi;
                prm[1] = new SqlParameter("@durum", SqlDbType.NVarChar);
                prm[1].Value = durum;
                prm[2] = new SqlParameter("@disbrutor", SqlDbType.NVarChar);
                prm[2].Value = disbrutor;
                prm[3] = new SqlParameter("@deger_dondur", SqlDbType.Int);
                prm[3].Direction = ParameterDirection.Output;

                
                SqlHelper.ExecuteNonQuery("marka_KayitEkle", prm);
                geriDonus = int.Parse(prm[3].Value.ToString());
            }
            catch (Exception)
            {
                throw;
            }


            return geriDonus;
        }

       public static void Durum(int markaId, string durum)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[2];
                prm[0] = new SqlParameter("@id", SqlDbType.Int);
                prm[0].Value = markaId;
                prm[1] = new SqlParameter("@durum", SqlDbType.NVarChar);
                prm[1].Value = durum;

                SqlHelper.ExecuteNonQuery("markalar_Durum", prm);
            }
            catch (Exception)
            {
                throw;
            }
        }

       public static MarkaList Liste(string param, string serial="")
        {
            try
            {
                SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@serial", serial),
                    new SqlParameter("@parametre ", param),
                };

                MarkaList markalar = new MarkaList();

                using (SqlDataReader dr = SqlHelper.ExecuteReader("marka_SolPanelListeleme", parametre))
                {
                    while (dr.Read())
                    {
                        markalar.Add(new Markalar(
                           dr.GetInt32(dr.GetOrdinal("id")),
                           dr.GetString(dr.GetOrdinal("marka_adi")).Trim()));
                    }
                }
                return markalar;
            }
            catch (Exception)
            {
                throw;
            }
        }

       public static string GetName(int markaId)
        {
            try
            {
               SqlParameter parametre = new SqlParameter("@markaId", markaId);
               string sqlText = "SELECT marka_adi FROM dbo.tbl_markalar WHERE id=@markaId";
               return (string)SqlHelper.ExecuteScalar(CommandType.Text, sqlText, parametre);
            }
            catch (Exception)
            {
                throw;
            }
        }

       public static MarkaList dropDownListele()
        {
            try
            {
                SqlParameter parametre = new SqlParameter("@parametre","AdminDropDown");

                using (SqlDataReader dr = SqlHelper.ExecuteReader("marka_Listele", parametre))
                {
                    MarkaList markaTablo = new MarkaList();
                    while (dr.Read())
                    {
                        Markalar info = new Markalar(
                          dr.GetInt32(dr.GetOrdinal("id")),
                          dr.GetString(dr.GetOrdinal("marka_adi")));
                        markaTablo.Add(info);
                    }

                    return markaTablo;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

       public static List<Resim> ResimAdiListeleSilme(int id)
        {
            try
            {
                SqlParameter sqlParamResim = new SqlParameter("@id", id);

                List<Resim> resimListe = new List<Resim>();

                using (SqlDataReader dr = SqlHelper.ExecuteReader("marka_Resim_Sil", sqlParamResim))
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

       public static void Delete(int markaId)
        {
            try
            {
                SqlParameter parametre = new SqlParameter("@id", markaId);
                SqlHelper.ExecuteNonQuery("marka_Sil", parametre);
            }
            catch (Exception)
            {
                throw;
            }
        }

       public static List<MarkaLink> UrunDetayMarkaLink(int urunId)
       {
           try
           {
               SqlParameter parametre = new SqlParameter("@urunId", urunId);
               using (SqlDataReader dr = SqlHelper.ExecuteReader("marka_UrunDetayListeleme", parametre))
               {
                   List<MarkaLink> markLink = new List<MarkaLink>();
                   Dictionary<string, string> _demo = new Dictionary<string, string>();

                   while (dr.Read())
                   {
                       MarkaLink markaLink = new MarkaLink();
                       markaLink.MarkaAdi = dr.GetString(dr.GetOrdinal("marka_adi"));
                       markaLink.Adet = dr.GetInt32(dr.GetOrdinal("adet"));
                       markaLink.Link = LinkBulding.MarkaAndParam(
                                           dr.GetString(dr.GetOrdinal("title")),
                                           markaLink.MarkaAdi,
                                           dr.GetString(dr.GetOrdinal("serial")),
                                           dr.GetInt32(dr.GetOrdinal("id")));

                       if (!_demo.ContainsKey(markaLink.MarkaAdi))
                       {
                           _demo.Add(markaLink.MarkaAdi, "0");
                           markLink.Add(markaLink);
                       }
                   }

                   return markLink;
               }
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
               string query = "SELECT COUNT(id) FROM tbl_markalar";
               return (int)SqlHelper.ExecuteScalar(CommandType.Text, query);
           }
           catch (Exception)
           {
               throw;
           }
       }
    }
}
