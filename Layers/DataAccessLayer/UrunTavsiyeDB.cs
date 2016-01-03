using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BusinessLayer;
using ModelLayer;

namespace DataAccessLayer
{
    public class UrunTavsiyeDB :BaseDB
    {

        public static List<UrunTavsiye> Liste(int urunId)
        {
            try
            {
                SqlParameter parametre = new SqlParameter("@id", urunId);
                
                using (SqlDataReader drTv = SqlHelper.ExecuteReader("urunTavsiye_Listele", parametre))
                {
                    List<UrunTavsiye> urunTavsiyeTablo = new List<UrunTavsiye>();

                    while (drTv.Read())
                    {
                        urunTavsiyeTablo.Add( new UrunTavsiye{
                          UrunFiyat =  drTv.GetDecimal(drTv.GetOrdinal("urunFiyat")),
                          UIndirimFiyat =  drTv.GetDecimal(drTv.GetOrdinal("uIndirimFiyat")),
                          UrunAdi = drTv.GetString(drTv.GetOrdinal("urunAdi")),
                          ResimAdi =  drTv.GetString(drTv.GetOrdinal("resimAdi")),
                          Doviz =  drTv.GetString(drTv.GetOrdinal("doviz")),

                          Link =  LinkBulding.Urun("Tavsiye",
                                    drTv.GetInt32(drTv.GetOrdinal("tavUrunId")),
                                    drTv.GetString(drTv.GetOrdinal("urunAdi")))
                        });
                    }

                    return urunTavsiyeTablo;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
