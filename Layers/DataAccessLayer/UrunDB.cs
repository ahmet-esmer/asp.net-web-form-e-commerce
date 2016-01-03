using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer;
using ModelLayer;

namespace DataAccessLayer
{

    public  class UrunDB:BaseDB
    {
        #region Anasayfa Ürünleri
        public static List<Urun> Urunler(string parametre)
        {
            try
            {
              List<Urun> urunler = new List<Urun>();
              SqlParameter sqlParametre = new SqlParameter("@parametre", parametre);

              using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_Listele", sqlParametre))
              {

                  while (dr.Read())
                  {
                      urunler.Add(new Urun(dr.GetString(dr.GetOrdinal("resimAdi")),
                         dr.GetInt32(dr.GetOrdinal("id")),
                         dr.GetString(dr.GetOrdinal("urunAdi")),
                         dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                         dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat")),
                         dr.GetString(dr.GetOrdinal("doviz")),
                         dr.GetInt32(dr.GetOrdinal("urunKDV")),
                          LinkBulding.Urun(
                         dr.GetString(dr.GetOrdinal("kategoriadi")),
                         dr.GetInt32(dr.GetOrdinal("id")),
                         dr.GetString(dr.GetOrdinal("urunAdi")))
                         ));
                  }

                  return urunler;
              }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Kaydet
        public static int kaydet(Urun urun)
        {
            int geriDonus = 0;
            try
            {
                SqlParameter[] parameter = new SqlParameter[24];
                parameter[0] = new SqlParameter("@title", SqlDbType.NVarChar);
                parameter[0].Value = urun.title;
                parameter[1] = new SqlParameter("@description", SqlDbType.NVarChar);
                parameter[1].Value = urun.description;
                parameter[2] = new SqlParameter("@keywords", SqlDbType.NVarChar);
                parameter[2].Value = urun.keywords;
                parameter[3] = new SqlParameter("@kategoriId", SqlDbType.Int);
                parameter[3].Value = urun.kategoriId;
                parameter[4] = new SqlParameter("@markaId", SqlDbType.Int);
                parameter[4].Value = urun.markaId;
                parameter[5] = new SqlParameter("@urunKodu", SqlDbType.NVarChar);
                parameter[5].Value = urun.urunKodu;
                parameter[6] = new SqlParameter("@urunAdi", SqlDbType.NVarChar);
                parameter[6].Value = urun.urunAdi;
                parameter[7] = new SqlParameter("@kisaAciklama", SqlDbType.NVarChar);
                parameter[7].Value = urun.kisaAciklama;
                parameter[8] = new SqlParameter("@urunFiyat", SqlDbType.Money);
                parameter[8].Value = urun.urunFiyat;
                parameter[9] = new SqlParameter("@uIndirimFiyat", SqlDbType.Money);
                parameter[9].Value = urun.uIndirimFiyat;
                parameter[10] = new SqlParameter("@doviz", SqlDbType.NVarChar);
                parameter[10].Value = urun.doviz;
                parameter[11] = new SqlParameter("@urunKDV", SqlDbType.Int);
                parameter[11].Value = urun.kdv;
                parameter[12] = new SqlParameter("@havaleIndirim", SqlDbType.Int);
                parameter[12].Value = urun.havaleIndirim;
                parameter[13] = new SqlParameter("@desiMiktari", SqlDbType.NVarChar);
                parameter[13].Value = urun.desiMiktari;
                parameter[14] = new SqlParameter("@urunStok", SqlDbType.NVarChar);
                parameter[14].Value = urun.urunStok;
                parameter[15] = new SqlParameter("@kiritikStok", SqlDbType.NVarChar);
                parameter[15].Value = urun.kiritikStok;
                parameter[16] = new SqlParameter("@stokCins", SqlDbType.NVarChar);
                parameter[16].Value = urun.stokCins;
                parameter[17] = new SqlParameter("@urunOzellik ", SqlDbType.NVarChar);
                parameter[17].Value = urun.urunOzellik;
                parameter[18] = new SqlParameter("@hit", SqlDbType.Int);
                parameter[18].Value = urun.hit;
                parameter[19] = new SqlParameter("@durum", SqlDbType.Bit);
                parameter[19].Value = urun.durum;
                parameter[20] = new SqlParameter("@resim_adi", SqlDbType.NVarChar);
                parameter[20].Value = urun.resimAdi;
                parameter[21] = new SqlParameter("@ozellikList", SqlDbType.NVarChar);
                parameter[21].Value = urun.listOzellik;

                parameter[22] = new SqlParameter("@deger_dondur", SqlDbType.Int);
                parameter[22].Direction = ParameterDirection.Output;


                if (urun.id > 0)// Ürün Güncelleme
                {
                    parameter[23] = new SqlParameter("@urunId", SqlDbType.NVarChar);
                    parameter[23].Value = urun.id;
                    SqlHelper.ExecuteNonQuery("urun_Guncelle", parameter);
                    geriDonus = int.Parse(parameter[22].Value.ToString());
                    
                }
                else
                {
                   SqlHelper.ExecuteNonQuery("urun_Ekle", parameter);
                   geriDonus = int.Parse(parameter[22].Value.ToString());
                }
            }
            catch (Exception hata)
            {
                throw hata;
            }

            return geriDonus;
        }
        #endregion

        #region Seçenek Kaydet
        public static void secenekKaydet(int urunId, string secenekler)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[2];
                parameter[0] = new SqlParameter("@urunId", SqlDbType.Int);
                parameter[0].Value = urunId;
                parameter[1] = new SqlParameter("@secenekler", SqlDbType.NVarChar);
                parameter[1].Value = secenekler;

                SqlHelper.ExecuteNonQuery("urun_SecenekEkle", parameter);

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Seçenek Getirme
        public static string[] urunSecenekGetir(int urunId)
        {
            string[] secenekler = null;
            string deger =  null;
            try
            {

                SqlParameter parametre = new SqlParameter("@urunId", urunId);
                using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_SecenekGetir", parametre))
                {
                    while (dr.Read())
                    {
                        deger = dr.GetString(dr.GetOrdinal("secenek"));
                    }


                    if (deger != null)
                    {
                        secenekler = deger.Split('|');
                    }  
                }

                

                return secenekler;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Kayıt Getirme web
        public static Urun GetById(int id)
        {
            Urun kategoriTablo = new Urun();

            try
            {
                SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@parametre", "web"),
                    new SqlParameter("@id", id),
                };

                using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_KayitGetir", parametre))
                {
                    string KisaAciklama = string.Empty;
                    string urunOzellik = string.Empty;
                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(dr.GetOrdinal("kisaAciklama")))
                        {
                            KisaAciklama = dr.GetString(dr.GetOrdinal("kisaAciklama"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("urunOzellik")))
                        {
                            urunOzellik = dr.GetString(dr.GetOrdinal("urunOzellik"));
                        }

                        kategoriTablo = new Urun
                        {
                            markaAdi = dr.GetString(dr.GetOrdinal("markaAdi")),
                            id = dr.GetInt32(dr.GetOrdinal("id")),
                            urunAdi = dr.GetString(dr.GetOrdinal("urunAdi")),
                            kisaAciklama = KisaAciklama,
                            urunFiyat = dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                            uIndirimFiyat = dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat")),
                            kdv = dr.GetInt32(dr.GetOrdinal("urunKDV")),
                            desiMiktari = dr.GetInt32(dr.GetOrdinal("desiMiktari")),
                            urunStok = dr.GetInt32(dr.GetOrdinal("urunStok")),
                            kiritikStok = dr.GetInt32(dr.GetOrdinal("kiritikStok")),
                            stokCins = dr.GetString(dr.GetOrdinal("stokCins")),
                            urunOzellik = urunOzellik,
                            hit = dr.GetInt32(dr.GetOrdinal("hit")),
                            havaleIndirim = dr.GetInt32(dr.GetOrdinal("havaleIndirim")),
                            doviz = dr.GetString(dr.GetOrdinal("doviz")),
                            title = dr.GetString(dr.GetOrdinal("title")),
                            description = dr.GetString(dr.GetOrdinal("description")),
                            keywords = dr.GetString(dr.GetOrdinal("keywords"))
                       
                        };
                    }

                }

                return kategoriTablo;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 233)
                    SqlConnection.ClearAllPools();

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Kayıt Getirme Admin
        public static Urun getir(int id, string bolge)
        {
            Urun kategoriTablo = new Urun();
            try
            {
                SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@parametre", bolge ),
                    new SqlParameter("@id", id),
                };

                using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_KayitGetir", parametre))
                {
                    string KisaAciklama = string.Empty;
                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(dr.GetOrdinal("kisaAciklama")))
                        {
                            KisaAciklama = dr.GetString(dr.GetOrdinal("kisaAciklama"));
                        }

                        kategoriTablo = new Urun
                        {
                            id = dr.GetInt32(dr.GetOrdinal("id")),
                            kategoriId = dr.GetInt32(dr.GetOrdinal("kategoriId")),
                            markaId = dr.GetInt32(dr.GetOrdinal("markaId")),
                            urunKodu = dr.GetString(dr.GetOrdinal("urunKodu")),
                            urunAdi = dr.GetString(dr.GetOrdinal("urunAdi")),
                            kisaAciklama = KisaAciklama,
                            urunFiyat = dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                            uIndirimFiyat = dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat")),
                            kdv = dr.GetInt32(dr.GetOrdinal("urunKDV")),
                            desiMiktari = dr.GetInt32(dr.GetOrdinal("desiMiktari")),
                            urunStok = dr.GetInt32(dr.GetOrdinal("urunStok")),
                            kiritikStok = dr.GetInt32(dr.GetOrdinal("kiritikStok")),
                            stokCins = dr.GetString(dr.GetOrdinal("stokCins")),
                            urunOzellik = dr.GetString(dr.GetOrdinal("urunOzellik")),
                            hit = dr.GetInt32(dr.GetOrdinal("hit")),
                            durum = dr.GetBoolean(dr.GetOrdinal("durum")),
                            title = dr.GetString(dr.GetOrdinal("title")),
                            description = dr.GetString(dr.GetOrdinal("description")),
                            keywords = dr.GetString(dr.GetOrdinal("keywords")),
                            havaleIndirim = dr.GetInt32(dr.GetOrdinal("havaleIndirim")),
                            doviz = dr.GetString(dr.GetOrdinal("doviz")),

                        };
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

        #region Ürün  Özellik Listeleme
        public static List<string> urunOzellikleri(int urunId)
        {
            try
            {
                SqlParameter parametre = new SqlParameter("@urunId ", urunId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_OzellikGetir", parametre))
                {
                    List<string> ozellikler = new List<string>();

                    while (dr.Read())
                    {
                      if (!dr.IsDBNull(dr.GetOrdinal("ozellik")))
                           ozellikler.Add(dr.GetString(dr.GetOrdinal("ozellik")));
                    }
                    return ozellikler;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public static UrunHediyeTek HediyeUrun(int id)
        {
            UrunHediyeTek hediye = new UrunHediyeTek();
            try
            {
                SqlParameter parametre = new SqlParameter("@id", id);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("urunKampanya_Getir", parametre))
                {
                    while (dr.Read())
                    {
                        hediye.ResimAdi = dr.GetString(dr.GetOrdinal("resimAdi"));
                        hediye.UrunFiyat = dr.GetDecimal(dr.GetOrdinal("urunFiyat"));
                        hediye.Doviz = dr.GetString(dr.GetOrdinal("doviz"));
                        hediye.UrunAdi = dr.GetString(dr.GetOrdinal("urunAdi"));
                    }
                }

                return hediye;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Urun> EnCokSatilanlar()
        {
            SqlParameter parametre = new SqlParameter("@parametre", "satilanlar");
            using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_Listele", parametre))
            {
                List<Urun> urunler = new List<Urun>();

                while (dr.Read())
                {
                    urunler.Add(new Urun(
                    dr.GetString(dr.GetOrdinal("resimAdi")),
                    dr.GetInt32(dr.GetOrdinal("id")),
                    dr.GetString(dr.GetOrdinal("urunAdi")),
                    dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                    dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat")),
                    dr.GetString(dr.GetOrdinal("doviz")),
                    dr.GetInt32(dr.GetOrdinal("urunKDV")),
                     LinkBulding.Urun(
                    dr.GetString(dr.GetOrdinal("kategoriadi")),
                    dr.GetInt32(dr.GetOrdinal("id")),
                    dr.GetString(dr.GetOrdinal("urunAdi")))
                    ));
                }

                return urunler;
            }
        
        }

        public static SqlDataReader AramaAdmin(int urunid, string prm)
        {
            SqlParameter[] parametre = new SqlParameter[] 
            { 
               new SqlParameter("@parametre", prm ),
               new SqlParameter("@id", urunid),
            };

            return SqlHelper.ExecuteReader("Urun_KayitAra", parametre);
       
        }

        public static void Delete(int id)
        {
            try
            {
                SqlParameter parametre = new SqlParameter("@id", id);
                SqlHelper.ExecuteNonQuery("urun_KayitSil", parametre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Urun> YeniUrunler(int baslangic, int bitis)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@Baslangic", baslangic),
                new SqlParameter("@Bitis", bitis),
            };

            using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_ListeleYeni", prm))
            {
                List<Urun> urunler = new List<Urun>();
                while (dr.Read())
                {
                    urunler.Add(new Urun(dr.GetString(dr.GetOrdinal("resimAdi")),
                       dr.GetInt32(dr.GetOrdinal("id")),
                       dr.GetString(dr.GetOrdinal("urunAdi")),
                       dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                       dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat")),
                       dr.GetString(dr.GetOrdinal("doviz")),
                       dr.GetInt32(dr.GetOrdinal("urunKDV")),
                        LinkBulding.Urun(
                       dr.GetString(dr.GetOrdinal("kategoriadi")),
                       dr.GetInt32(dr.GetOrdinal("id")),
                       dr.GetString(dr.GetOrdinal("urunAdi")))
                       ));
                }

                return urunler;
            }

        }

        public static int YeniUrunlerToplamKayit()
        {
          return (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "urun_ListeleYeniNo");
        }

        public static int UrunVarmi(int urunId)
        {
            return (int)SqlHelper.ExecuteScalar(CommandType.Text,
                "SELECT COUNT(id) FROM tbl_urunler WHERE id="+ urunId.ToString());
        }

        public static int UrunToplamKayit(string serial, string parameter, int markaId)
        {
            try
            {
                SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@serial", serial),
                    new SqlParameter("@parametre", parameter),
                    new SqlParameter("@markaId ", markaId)
                };

                return (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "urun_SayafaNo", parametre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Urun> Urunler(int baslangic, int bitis, string serial, string parametre, int markaId)
        {
            try
            {
                SqlParameter[] sqlParametre = new SqlParameter[] 
             { 
                new SqlParameter("@Baslangic", baslangic),
                new SqlParameter("@Bitis", bitis),
                new SqlParameter("@serial", serial),
                new SqlParameter("@parametre", parametre),
                new SqlParameter("@markaId", markaId)
             };

                using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_KayitListele", sqlParametre))
                {
                    List<Urun> urunler = new List<Urun>();
                    while (dr.Read())
                    {
                        urunler.Add(new Urun(dr.GetString(dr.GetOrdinal("resimAdi")),
                          dr.GetInt32(dr.GetOrdinal("id")),
                          dr.GetString(dr.GetOrdinal("urunAdi")),
                          dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                          dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat")),
                          dr.GetString(dr.GetOrdinal("doviz")),
                          dr.GetInt32(dr.GetOrdinal("urunKDV")),
                           LinkBulding.Urun(
                          dr.GetString(dr.GetOrdinal("kategoriadi")),
                          dr.GetInt32(dr.GetOrdinal("id")),
                          dr.GetString(dr.GetOrdinal("urunAdi")))
                          ));
                    }

                    return urunler;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Urun> HediyeliUrunler(int hediyeId)
        {
            try
            {
                SqlParameter sqlParametre = new SqlParameter("@hediyeId", hediyeId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("urun_ListeleHediye", sqlParametre))
                {
                    List<Urun> urunler = new List<Urun>();
                    while (dr.Read())
                    {
                        urunler.Add(new Urun(dr.GetString(dr.GetOrdinal("resimAdi")),
                          dr.GetInt32(dr.GetOrdinal("id")),
                          dr.GetString(dr.GetOrdinal("urunAdi")),
                          dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                          dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat")),
                          dr.GetString(dr.GetOrdinal("doviz")),
                          dr.GetInt32(dr.GetOrdinal("urunKDV")),
                           LinkBulding.Urun(
                          dr.GetString(dr.GetOrdinal("kategoriadi")),
                          dr.GetInt32(dr.GetOrdinal("id")),
                          dr.GetString(dr.GetOrdinal("urunAdi")))
                          ));
                    }

                    return urunler;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}
