using System;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;

namespace DataAccessLayer
{
    public class BankaApiDB:BaseDB
    {
        public static BankaApi SanalPosBilgileri(int bankaId, int taksitMiktar)
        {
            BankaApi _api = new BankaApi();
            try
            {
                SqlParameter[] prm = new SqlParameter[2];
                prm[0] = new SqlParameter("@bankaId", SqlDbType.Int);
                prm[0].Value = bankaId;
                prm[1] = new SqlParameter("@taksit", SqlDbType.Int);
                prm[1].Value = taksitMiktar;

                using (SqlDataReader dr = SqlHelper.ExecuteReader("bankaSanalPos_GetirOdeme", prm))
                {
                    while (dr.Read())
                    {
                        _api.ApiPassword = dr.GetString(dr.GetOrdinal("apiSifre"));
                        _api.HostName = dr.GetString(dr.GetOrdinal("sunucu"));
                        _api.ApiName = dr.GetString(dr.GetOrdinal("apiKullanici"));
                        _api.ClientId = dr.GetString(dr.GetOrdinal("magazaAdi"));
                        _api.BankaAdi = dr.GetString(dr.GetOrdinal("bankaAdi"));
                        _api.BankaKodu = dr.GetString(dr.GetOrdinal("bankaKod"));
                    }
                }

                return _api;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
