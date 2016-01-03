using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;

namespace DataAccessLayer
{
    public class BannerDB :BaseDB
    {
      public static List<Banner> BannerList(string prm)
      {
          SqlParameter parametre = new SqlParameter("@paremetre", prm );
          List<Banner> bannerListe = new List<Banner>();

          using (SqlDataReader dr = SqlHelper.ExecuteReader("resim_Listele", parametre))
          {
              while (dr.Read())
              {
                  bannerListe.Add(new Banner
                  {
                      ResimAdi = dr.GetString(dr.GetOrdinal("resim_adi")),
                      ResimBaslik = dr.GetString(dr.GetOrdinal("resim_baslik"))
                  });
              }
          }

          return bannerListe;
      }

      public static List<Banner> BannerList()
      {
          SqlParameter parametre = new SqlParameter("@paremetre", "reklam");
          List<Banner> bannerListe = new List<Banner>();

          using (SqlDataReader dr = SqlHelper.ExecuteReader("resim_Listele", parametre))
          {
              while (dr.Read())
              {
                  bannerListe.Add(new Banner
                  {
                      ResimAdi = dr.GetString(dr.GetOrdinal("resim_adi")),
                      Id = dr.GetInt32(dr.GetOrdinal("id")),
                      ResimBaslik = dr.GetString(dr.GetOrdinal("resim_baslik")),
                      Durum = dr.GetBoolean(dr.GetOrdinal("durum")),
                      Sira = dr.GetInt32(dr.GetOrdinal("sira"))
                  });
              }
          }

          return bannerListe;
      }

      public static void Save(Banner banner)
      {
          try
          {
              SqlParameter[] prm = new SqlParameter[6];
              prm[0] = new SqlParameter("@parametre", SqlDbType.NVarChar);
              prm[0].Value = banner.Parametre;
              prm[1] = new SqlParameter("@resim_adi", SqlDbType.NVarChar);
              prm[1].Value = banner.ResimAdi;
              prm[2] = new SqlParameter("@resim_baslik", SqlDbType.NVarChar);
              prm[2].Value = banner.ResimBaslik;
              prm[3] = new SqlParameter("@sira", SqlDbType.Int);
              prm[3].Value = banner.Sira;
              prm[4] = new SqlParameter("@durum", SqlDbType.Bit);
              prm[4].Value = banner.Durum;

              if (banner.Id > 0)
              {
                  prm[5] = new SqlParameter("@id", SqlDbType.NVarChar);
                  prm[5].Value = banner.Id;

                  SqlHelper.ExecuteNonQuery("reklam_Guncelle", prm);
              }
              else
              {
                  SqlHelper.ExecuteNonQuery("ResimEkle", prm); 
              }
          }
          catch (Exception hata)
          {
              throw hata;
          }
      }

      public static void Status(Banner banner)
      {
          try
          {
              SqlParameter[] prm = new SqlParameter[2];
              prm[0] = new SqlParameter("@id", SqlDbType.NVarChar);
              prm[0].Value = banner.Id;
              prm[1] = new SqlParameter("@durum", SqlDbType.Bit);
              prm[1].Value = banner.Durum;

              SqlHelper.ExecuteNonQuery("reklam_Durum", prm);
          }
          catch (Exception hata)
          {
              throw hata;
          }
      }

      public static Banner Get(int id, string parametre)
      {

          SqlParameter[] prm = new SqlParameter[2];
          prm[0] = new SqlParameter("@id", SqlDbType.NVarChar);
          prm[0].Value = id;
          prm[1] = new SqlParameter("@parametre", SqlDbType.NVarChar);
          prm[1].Value = parametre;

          Banner banner = new Banner();

          using (SqlDataReader dr = SqlHelper.ExecuteReader("resim_Bilgi_Getir", prm))
          {
              while (dr.Read())
              {
                  banner.ResimAdi = dr.GetString(dr.GetOrdinal("resim_adi"));
                  banner.Id = dr.GetInt32(dr.GetOrdinal("id"));
                  banner.ResimBaslik = dr.GetString(dr.GetOrdinal("resim_baslik"));
                  banner.Durum = dr.GetBoolean(dr.GetOrdinal("durum"));
                  banner.Sira = dr.GetInt32(dr.GetOrdinal("sira"));
              }
          }

          return banner;
      }

    }
}
