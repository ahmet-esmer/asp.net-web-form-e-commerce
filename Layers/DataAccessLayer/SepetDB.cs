using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLayer;
using System.Data.SqlClient;
using BusinessLayer;

namespace DataAccessLayer
{
    public class SepetDB : BaseDB
    {
        public static List<Sepet> GetListFor(int kullaniciId)
        {

            try
            {
                SqlParameter parametre = new SqlParameter("@id", kullaniciId);
                List<Sepet> sepetListe = new List<Sepet>();

                using (SqlDataReader dr = SqlHelper.ExecuteReader("sepet_UrunGetir", parametre))
                {
                    while (dr.Read())
                    {
                        UrunHediye _hediye = new UrunHediye();

                        if (!dr.IsDBNull(dr.GetOrdinal("hediyeId")))
                        {
                            _hediye.Id = dr.GetInt32(dr.GetOrdinal("hediyeId"));  
                        }
                       

                        if (_hediye.Id > 0)
                        {
                            _hediye.Resim = dr.GetString(dr.GetOrdinal("resim"));
                            _hediye.UrunAdi = dr.GetString(dr.GetOrdinal("hediyeAdi"));
                            _hediye.Secenek = dr.GetString(dr.GetOrdinal("hediyeBilgi"));
                        }

                        sepetListe.Add(new Sepet
                        {
                            ResimAdi = dr.GetString(dr.GetOrdinal("resimAdi")),
                            StokCins = dr.GetString(dr.GetOrdinal("stokCins")),
                            UrunAdi = dr.GetString(dr.GetOrdinal("urunAdi")),
                            UrunId = dr.GetInt32(dr.GetOrdinal("urunId")),
                            UrunStok = dr.GetInt32(dr.GetOrdinal("urunStok")),
                            UrunKDV = dr.GetInt32(dr.GetOrdinal("urunKDV")),
                            Miktar = dr.GetInt32(dr.GetOrdinal("miktar")),
                            SepetId = dr.GetInt32(dr.GetOrdinal("sepetId")),
                            UrunFiyat = SepetOperasyon.UrunFiyat(
                                                      dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                                                      dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat"))),
                            Doviz = dr.GetString(dr.GetOrdinal("doviz")),
                            SagAdet = dr.GetInt32(dr.GetOrdinal("sagAdet")),
                            SolAdet = dr.GetInt32(dr.GetOrdinal("solAdet")),
                            SagBilgi = dr.GetString(dr.GetOrdinal("sagBilgi")),
                            SolBilgi = dr.GetString(dr.GetOrdinal("solBilgi")),
                            ObjHediye = _hediye,
                            HavaleIndirim = dr.GetInt32(dr.GetOrdinal("havaleIndirim")),
                        });

                    }
                }

                return sepetListe;
            }
            catch (Exception )
            {
                throw;
            }
        }

        public static List<Sepet> SepetOzet(int kullaniciId)
        {
            try
            {
                List<Sepet> sepetListe = new List<Sepet>();

                SqlParameter parametre = new SqlParameter("@id", kullaniciId);

                using (SqlDataReader dr = SqlHelper.ExecuteReader("sepet_UrunGetir", parametre))
                {
                    while (dr.Read())
                    {
                        UrunHediye _hediye = new UrunHediye();

                        if (!dr.IsDBNull(dr.GetOrdinal("hediyeId")))
                        {
                            _hediye.Id = dr.GetInt32(dr.GetOrdinal("hediyeId"));
                        }


                        sepetListe.Add(new Sepet
                        {
                            UrunId = dr.GetInt32(dr.GetOrdinal("urunId")),
                            UrunKDV = dr.GetInt32(dr.GetOrdinal("urunKDV")),
                            Miktar = dr.GetInt32(dr.GetOrdinal("miktar")),
                            UrunFiyat = SepetOperasyon.UrunFiyat(
                                                      dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                                                      dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat"))),
                            Doviz = dr.GetString(dr.GetOrdinal("doviz")),
                            ObjHediye = _hediye,

                        });
                    }
                }

                return sepetListe;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SepetSiparisKayit> GetListForInsert(int kullaniciId)
        {

            try
            {
                SqlParameter parametre = new SqlParameter("@id", kullaniciId);
                List<SepetSiparisKayit> sepetListe = new List<SepetSiparisKayit>();

                using (SqlDataReader dr = SqlHelper.ExecuteReader("sepet_UrunGetirSiparisKayit", parametre))
                {
                    while (dr.Read())
                    {

                        SepetSiparisKayit sepet = new SepetSiparisKayit
                           {
                               UrunId = dr.GetInt32(dr.GetOrdinal("urunId")),
                               Fiyat = SepetOperasyon.UrunFiyat(dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                                                     dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat"))),
                               Miktar = dr.GetInt32(dr.GetOrdinal("miktar")),
                               SagAdet = dr.GetInt32(dr.GetOrdinal("sagAdet")),
                               SolAdet = dr.GetInt32(dr.GetOrdinal("solAdet")),
                               SagBilgi = dr.GetString(dr.GetOrdinal("sagBilgi")),
                               SolBilgi = dr.GetString(dr.GetOrdinal("solBilgi")),
                               SepetId = dr.GetInt32(dr.GetOrdinal("sepetId"))
                           };


                        if (!dr.IsDBNull(dr.GetOrdinal("hediyeId")))
                        {
                            sepet.HediyeId = dr.GetInt32(dr.GetOrdinal("hediyeId"));
                            sepet.HediyeBilgi = dr.GetString(dr.GetOrdinal("hediyeBilgi"));
                        }

                        sepetListe.Add(sepet);
                    }
                }

                return sepetListe;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
