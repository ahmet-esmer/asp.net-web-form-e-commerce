using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer;
using ModelLayer;

namespace DataAccessLayer
{
   public class IcerikBaslikDB:BaseDB
    {

        #region Kaydet
        public static int kaydet(string anakategori_serial, string kayitId, string kategoriAdi, Boolean durum, string alan, string link)
        {
            int geriDonus = 0;
            try
            {
                SqlParameter[] parameter = new SqlParameter[7];
                parameter[0] = new SqlParameter("@anakategori_serial", SqlDbType.NVarChar);
                parameter[0].Value = anakategori_serial;
                parameter[1] = new SqlParameter("@kayitId", SqlDbType.Int);
                parameter[1].Value = Convert.ToInt32(kayitId);
                parameter[2] = new SqlParameter("@kategoriAdi", SqlDbType.NVarChar);
                parameter[2].Value = kategoriAdi;
                parameter[3] = new SqlParameter("@durum", SqlDbType.Int);
                parameter[3].Value = durum;
                parameter[4] = new SqlParameter("@alan", SqlDbType.NVarChar);
                parameter[4].Value = alan;
                parameter[5] = new SqlParameter("@link", SqlDbType.NVarChar);
                parameter[5].Value = link;
                parameter[6] = new SqlParameter("@kayit_sinirla", SqlDbType.Int);
                parameter[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery("icerik_baslikadiEkle", parameter);
                geriDonus = int.Parse(parameter[6].Value.ToString());
            }
            catch (Exception hata)
            {
                throw hata;
            }

            return geriDonus;
        }
        #endregion

        #region Listeleme
        public static List<IcerikBaslik> listeleme(string bolge)
        {
            try
            {
                SqlParameter param = new SqlParameter("@bolge", bolge);
                List<IcerikBaslik> icerikTablo = new List<IcerikBaslik>();

                using (SqlDataReader dr = SqlHelper.ExecuteReader("icerik_baslikadiListele", param))
                {
                    string gosterimAlani;
                    while (dr.Read())
                    {
                        gosterimAlani = dr.GetString(dr.GetOrdinal("gosterimAlani"));

                        switch (gosterimAlani)
                        {
                            case "1": gosterimAlani = "Üst Menu"; break;
                            case "2": gosterimAlani = "Ana Menu"; break;
                            case "3": gosterimAlani = "Alt Menu"; break;
                        }

                        icerikTablo.Add(new IcerikBaslik
                        {
                            id = dr.GetInt32(dr.GetOrdinal("id")),
                            kategoriadi = dr.GetString(dr.GetOrdinal("kategoriadi")),
                            anaId = dr.GetInt32(dr.GetOrdinal("anaId")),
                            durum = dr.GetBoolean(dr.GetOrdinal("durum")),
                            serial = dr.GetString(dr.GetOrdinal("serial")),
                            sira = dr.GetInt32(dr.GetOrdinal("sira")),
                            gosterimAlani = gosterimAlani
                        });
                    }
                }

                return icerikTablo;
            }
            catch (Exception hata)
            {
                throw hata;
            }
        }
        #endregion

        #region Listeleme Web
        public static List<IcerikBaslik> ListelemeLink(string bolge)
        {
            try
            {
                SqlParameter param = new SqlParameter("@bolge", bolge);
                List<IcerikBaslik> icerikBaslik = new List<IcerikBaslik>();

                using (SqlDataReader dr = SqlHelper.ExecuteReader("icerik_baslikadiListele", param))
                {

                    while (dr.Read())
                    {
                        string sayfaLink = dr.GetString(dr.GetOrdinal("link"));
                        int id = dr.GetInt32(dr.GetOrdinal("id"));
                        string kategoriAdi = dr.GetString(1);
                        
                      icerikBaslik.Add(new IcerikBaslik(id, kategoriAdi, LinkBulding.Icerik(sayfaLink,id,kategoriAdi)));
                    }
                }

                return icerikBaslik;
            }
            catch (Exception hata)
            {
                throw hata;
            }
        }
        #endregion

        #region Getirme
        public static IcerikBaslik getir(int id)
        {
            IcerikBaslik info = new IcerikBaslik();
            try
            {
                SqlParameter param = new SqlParameter("@id", id);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("icerik_baslikadiGetir", param))
                {
                    while (dr.Read())
                    {
                        info = new IcerikBaslik(
                        dr.GetInt32(dr.GetOrdinal("id")),
                        dr.GetInt32(dr.GetOrdinal("anaId")),
                        dr.GetString(dr.GetOrdinal("kategoriadi")),
                        dr.GetBoolean(dr.GetOrdinal("durum")),
                        dr.GetString(dr.GetOrdinal("serial")),
                        dr.GetInt32(dr.GetOrdinal("sira")),
                        dr.GetString(dr.GetOrdinal("gosterimAlani")),
                        dr.GetString(dr.GetOrdinal("link")));
                    }
                }

                return info;
            }
            catch (Exception hata)
            {
                throw hata;
            }
        }
        #endregion
    }
}
