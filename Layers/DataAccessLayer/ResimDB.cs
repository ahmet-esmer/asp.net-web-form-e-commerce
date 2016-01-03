using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;

namespace DataAccessLayer
{
   public class ResimDB :BaseDB
    {

      #region Admin Ürün  Resim Listeleme
      public static List<Resim> ResimListele(int id, string parametre)
      {
          try
          {
              List<Resim> resimTablo = new List<Resim>();

              SqlParameter[] sqlParamResim = new SqlParameter[] 
                { 
                    new SqlParameter("@id", id),
                    new SqlParameter("@parametre", parametre)
                };

              using (SqlDataReader dr = SqlHelper.ExecuteReader("resim_Bilgi_Getir", sqlParamResim))
              {
                  while (dr.Read())
                  {
                      string resimBaslik = string.Empty;

                      if (!dr.IsDBNull(dr.GetOrdinal("resim_baslik")))
                      {
                          resimBaslik = dr.GetString(dr.GetOrdinal("resim_baslik"));
                      }

                      Resim info = new Resim(
                          dr.GetInt32(dr.GetOrdinal("id")),
                          dr.GetString(dr.GetOrdinal("resim_adi")),
                          dr.GetInt32(dr.GetOrdinal("sira")),
                          resimBaslik);

                      resimTablo.Add(info);
                  }
              }

              return resimTablo;
          }
          catch (Exception)
          {
              throw;
          }
      }
      #endregion


      public static Resim Get(string parametre)
      {
          try
          {
              Resim _resim = new Resim();

              SqlParameter sqlParametre = new SqlParameter("@paremetre", parametre);

              using (SqlDataReader dr = SqlHelper.ExecuteReader("resim_ListeleWeb", sqlParametre))
              {
                  while (dr.Read())
                  {
                      if (!dr.IsDBNull(dr.GetOrdinal("resim_baslik")))
                         _resim.resimBaslik = dr.GetString(dr.GetOrdinal("resim_baslik"));
                     

                       _resim.resimAdi = dr.GetString(dr.GetOrdinal("resim_adi"));
                  }
              }

              return _resim;
          }
          catch (Exception)
          {
              throw;
          }
      }

      // Kullanılmadı
      public static string ResimAdi(string parametre)
      {
          try
          {
              string resim = "";

              SqlParameter sqlParametre = new SqlParameter("@paremetre", parametre);

              using (SqlDataReader dr = SqlHelper.ExecuteReader("resim_ListeleWeb", sqlParametre))
              {
                  while (dr.Read())
                  {
                      resim = dr.GetString(dr.GetOrdinal("resim_adi"));
                  }
              }

              return resim;
          }
          catch (Exception)
          {
              throw;
          }
      }

      public static void Duzenle(int resimId, string sira, string param)
      {
          try
          {
              SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@id", resimId),
                    new SqlParameter("@sira", sira),
                    new SqlParameter("@parametre", param),
                };

              SqlHelper.ExecuteNonQuery("resim_Guncelle", parametre);

          }
          catch (Exception)
          {
              throw;
          }
      }

      public static void ResimSil(int resimId)
      {
          try
          {
              SqlParameter parametre = new SqlParameter("@id", resimId);
              SqlHelper.ExecuteNonQuery("resim_Sil", parametre);
          }
          catch (Exception)
          {
              throw;
          }
      }

      public static void Kaydet(int id, string resimAdi, string parametre)
      {
          try
          {
              SqlParameter[] parameter = new SqlParameter[3];
              parameter[0] = new SqlParameter("@genel_id", SqlDbType.Int);
              parameter[0].Value = id;
              parameter[1] = new SqlParameter("@resim_adi", SqlDbType.NVarChar);
              parameter[1].Value = resimAdi;
              parameter[2] = new SqlParameter("@parametre", SqlDbType.NVarChar);
              parameter[2].Value = parametre;

              SqlHelper.ExecuteNonQuery("urun_resimEkle", parameter);
          }
          catch (Exception)
          {
              throw;
          }
      }

      
    }
}
