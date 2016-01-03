using System;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;
using System.Collections.Generic;
using ModelLayer.JSON;

namespace DataAccessLayer
{
    public class BankaTaksitDB:BaseDB
    {

        public static  double  TaksitVade(int bankaId, int taksitMiktar)
        {
            double vade = 0;
            try
            {
                SqlParameter[] prm = new SqlParameter[2];
                prm[0] = new SqlParameter("@bankaId", SqlDbType.Int);
                prm[0].Value = bankaId;
                prm[1] = new SqlParameter("@taksit", SqlDbType.Int);
                prm[1].Value = taksitMiktar;

                using (SqlDataReader dr = SqlHelper.ExecuteReader("bankaTaksit_VadeGetir", prm))
                {
                    while (dr.Read())
                    {
                        vade = dr.GetDouble(dr.GetOrdinal("vadeFarki"));
                    }
                }

                return vade;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static TaksitJson List(int bankaId, decimal fiyat)
        {
            TaksitJson _taksit = new TaksitJson();

            try
            {
                SqlParameter parametre = new SqlParameter("@id", bankaId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("bankaKayit_Getir", parametre))
                {
                    decimal toplamFiyat = 0;
                    decimal aylikFiyat = 0;

                    while (dr.Read())
                    {
                        if (_taksit.Resim == null)
                          _taksit.Resim =  dr.GetString(dr.GetOrdinal("baslikResmi"));  

                        Taksit obj = new Taksit
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            Ay =  dr.GetInt32(dr.GetOrdinal("taksit")),
                            AylikVade = dr.GetDouble(dr.GetOrdinal("vadeFarki")),
                        };

                        toplamFiyat = (fiyat * Convert.ToDecimal(obj.AylikVade) / 100) + fiyat;
                        aylikFiyat = toplamFiyat / obj.Ay;

                        obj.ToplamFiyat = toplamFiyat.ToString("C");
                        obj.AylikFiyat = aylikFiyat.ToString("C");

                        _taksit.Taksit.Add(obj);
                    }
                }
                return _taksit;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<TaksitJson> UrunDetayListe(decimal fiyat)
        {
            try
            {
                List<TaksitJson> _banka = new List<TaksitJson>();
                SqlParameter prm = new SqlParameter("@parametre", "web");

                using (SqlDataReader dr = SqlHelper.ExecuteReader("banka_KayitListele", prm))
                {
                    while (dr.Read())
                    {
                        _banka.Add(new TaksitJson
                        {
                            Resim = dr.GetString(dr.GetOrdinal("baslikResmi")),
                            Taksit = TaksitList(dr.GetInt32(dr.GetOrdinal("id")), fiyat)
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

        private static List<Taksit> TaksitList(int bankaId, decimal fiyat)
        {
            List<Taksit> _taksit = new List<Taksit>();

            try
            {
                SqlParameter parametre = new SqlParameter("@id", bankaId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("bankaKayit_Getir", parametre))
                {
                    decimal toplamFiyat = 0;
                    decimal aylikFiyat = 0;

                    while (dr.Read())
                    {
                        Taksit obj = new Taksit
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            Ay = dr.GetInt32(dr.GetOrdinal("taksit")),
                            AylikVade = dr.GetDouble(dr.GetOrdinal("vadeFarki")),
                        };

                        toplamFiyat = (fiyat * Convert.ToDecimal(obj.AylikVade) / 100) + fiyat;
                        aylikFiyat = toplamFiyat / obj.Ay;

                        obj.ToplamFiyat = toplamFiyat.ToString("C");
                        obj.AylikFiyat = aylikFiyat.ToString("C");

                        _taksit.Add(obj);
                    }
                }
                return _taksit;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
