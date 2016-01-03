using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;
using System;
using LoggerLibrary;

namespace DataAccessLayer
{
    public class BankaDB:BaseDB
    {
        public static void BankaGeriBildirimKaydet(BankRequest bankRequest)
        {
            try
            {
                using (SqlConnection baglan = new SqlConnection(ConnectionString))
                {
                    baglan.Open();
                    using (SqlCommand kmt = new SqlCommand())
                    {
                        kmt.Connection = baglan;
                        kmt.CommandText = "banka_MesajEkle";
                        kmt.CommandType = CommandType.StoredProcedure;
                        kmt.Parameters.Add("@siparisNo", SqlDbType.NVarChar);
                        kmt.Parameters["@siparisNo"].Value = bankRequest.SiparisNo;
                        kmt.Parameters.Add("@kullaniciId", SqlDbType.Int);
                        kmt.Parameters["@kullaniciId"].Value = bankRequest.KrediKart.UserId;
                        kmt.Parameters.Add("@kartAd", SqlDbType.NVarChar);
                        kmt.Parameters["@kartAd"].Value = bankRequest.KrediKart.AdSoyad;
                        kmt.Parameters.Add("@kartNo", SqlDbType.NVarChar);
                        kmt.Parameters["@kartNo"].Value = bankRequest.KrediKart.No.Substring(0, 4)
                            + "-XXXX-XXXX-" + bankRequest.KrediKart.No.Substring(12);
                        kmt.Parameters.Add("@kartCV2", SqlDbType.NVarChar);

                        kmt.Parameters["@kartCV2"].Value = string.Format("{0}**", bankRequest.KrediKart.CV2.Substring(0,1));

                        kmt.Parameters.Add("@redMesaj", SqlDbType.NVarChar);
                        kmt.Parameters["@redMesaj"].Value = bankRequest.PaymentMessage.RedMesaj;
                        kmt.Parameters.Add("@redMesajKodu", SqlDbType.NVarChar);
                        kmt.Parameters["@redMesajKodu"].Value = bankRequest.PaymentMessage.RedMesajKodu;
                        kmt.Parameters.Add("@bankaId", SqlDbType.Int);
                        kmt.Parameters["@bankaId"].Value = bankRequest.BankaId;
                        kmt.Parameters.Add("@taksit", SqlDbType.Int);
                        kmt.Parameters["@taksit"].Value = bankRequest.Taksit;
                        kmt.Parameters.Add("@toplamFiyat", SqlDbType.Money);
                        kmt.Parameters["@toplamFiyat"].Value = bankRequest.TaksitToplam;
                     
                        kmt.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.SqlDB.Write("Banka dönüş raporu kaydetme:", ex);
            }
        }

        public static List<BankaMesaj> GeriBildirimMesajlari(int baslangic, int bitis)
        {
            try
            {
                List<BankaMesaj> mesajlar = new List<BankaMesaj>();

                SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@baslangic", baslangic),
                    new SqlParameter("@bitis", bitis)
                };

                using (SqlDataReader dr = SqlHelper.ExecuteReader("banka_MesajListele", parametre))
                {
                    while (dr.Read())
                    {
               
                        mesajlar.Add(new BankaMesaj
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            SiparisNo = dr.GetString(dr.GetOrdinal("siparisNo")),
                            SiparisId = dr.GetInt32(dr.GetOrdinal("siparisId")),
                            KullaniciId = dr.GetInt32(dr.GetOrdinal("kullaniciId")),
                            KartAd = dr.GetString(dr.GetOrdinal("kartAd")),
                            KartNo = dr.GetString(dr.GetOrdinal("kartNo")),
                            KartCV2 = dr.GetString(dr.GetOrdinal("kartCV2")),
                            RedMesaj = dr.GetString(dr.GetOrdinal("redMesaj")),
                            RedMesajKodu = dr.GetString(dr.GetOrdinal("redMesajKodu")),
                            Taksit = dr.GetInt32(dr.GetOrdinal("taksit")),
                            ToplamFiyat = dr.GetDecimal(dr.GetOrdinal("toplamFiyat")),
                            Tarih = dr.GetDateTime(dr.GetOrdinal("tarih")),
                            AdiSoyadi = dr.GetString(dr.GetOrdinal("adiSoyadi")),
                            BankaAdi = dr.GetString(dr.GetOrdinal("bankaAdi")),
                            HataSayisi = dr.GetInt32(dr.GetOrdinal("hataSayisi")),
        
                        });
                    }
                }

                return mesajlar;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Banka> ListeWeb()
        {
            try
            {
                List<Banka> _banka = new List<Banka>();
                SqlParameter prm = new SqlParameter("@parametre", "web");

                using (SqlDataReader dr = SqlHelper.ExecuteReader("banka_KayitListele", prm))
                {
                    while (dr.Read())
                    {
                        _banka.Add(new Banka
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            BankaAdi = dr.GetString(dr.GetOrdinal("bankaAdi")),
                            BaslikResmi = dr.GetString(dr.GetOrdinal("baslikResmi"))
                        });
                    }
                }

                return _banka;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Banka> Liste(string parametre)
        {
            List<Banka> _banka = new List<Banka>();
            SqlParameter prm = new SqlParameter("@parametre", parametre);

            using (SqlDataReader dr = SqlHelper.ExecuteReader("banka_KayitListele", prm))
            {
                while (dr.Read())
                {
                    _banka.Add(new Banka { 
                        Id = dr.GetInt32(dr.GetOrdinal("id")),
                        BankaAdi = dr.GetString(dr.GetOrdinal("bankaAdi")),
                        Sira = dr.GetInt32(dr.GetOrdinal("sira")),
                        BaslikResmi = dr.GetString(dr.GetOrdinal("baslikResmi")),
                        Durum = dr.GetBoolean(dr.GetOrdinal("durum")),
                       Varsayilan = dr.GetBoolean(dr.GetOrdinal("varsayilan"))
                    });  
                }
            }

            return _banka;
        }



    }
}
