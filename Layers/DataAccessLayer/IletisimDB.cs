using System;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;

namespace DataAccessLayer
{
    public class IletisimDB : BaseDB
    {

        public static Iletisim Get()
        {

            try
            {
                Iletisim iletisim = new Iletisim();

                string query = "SELECT * FROM tbl_site_iletisim WHERE id=1";

                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, query))
                {
                    while (dr.Read())
                    {
                        iletisim.Adres = dr.GetString(dr.GetOrdinal("adres"));
                        iletisim.Firma = dr.GetString(dr.GetOrdinal("fimaAdi"));
                        iletisim.Yetkili = dr.GetString(dr.GetOrdinal("yetkiliKisi"));
                        iletisim.VergiNo = dr.GetString(dr.GetOrdinal("vergiNo"));
                        iletisim.VergiDairesi = dr.GetString(dr.GetOrdinal("vergiDairesi"));
                        iletisim.Telefon = dr.GetString(dr.GetOrdinal("telefon"));
                        iletisim.Faks = dr.GetString(dr.GetOrdinal("faks"));
                        iletisim.Eposta = dr.GetString(dr.GetOrdinal("ePosta"));
                    }
                }

                return iletisim;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
