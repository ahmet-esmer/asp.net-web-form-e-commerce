using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class SehirDB :BaseDB
    {
        private static SqlConnection baglan;
        private static SqlDataAdapter adp;
        private static SqlCommand komutver;
        private static DataTable dt;


        public static DataTable Sehirler()
        {
            try
            {
                using (baglan = new SqlConnection(ConnectionString))
                {
                    baglan.Open();
                    using (komutver = new SqlCommand())
                    {
                        komutver.Connection = baglan;
                        komutver.CommandText = "sehir_Listele";
                        komutver.CommandType = CommandType.StoredProcedure;
                        adp = new SqlDataAdapter(komutver);
                        dt = new DataTable();
                        adp.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return dt;
        }


        public static DataTable Ilceler(int id)
        {
            try
            {
                using (baglan = new SqlConnection(ConnectionString))
                {
                    baglan.Open();
                    using (komutver = new SqlCommand())
                    {
                        komutver.Connection = baglan;
                        komutver.CommandText = "ilce_Listele";
                        komutver.Parameters.Add("@ilceId", SqlDbType.Int);
                        komutver.Parameters["@ilceId"].Value = Convert.ToInt32(id);
                        komutver.CommandType = CommandType.StoredProcedure;
                        adp = new SqlDataAdapter(komutver);
                        dt = new DataTable();
                        adp.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

    }
}
