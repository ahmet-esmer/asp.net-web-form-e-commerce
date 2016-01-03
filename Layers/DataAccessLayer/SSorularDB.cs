using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLayer;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class SSorularDB :BaseDB
    {
        public static SSorulanList Liste()
        {
            SSorulanList SSSoruTablo = new SSorulanList();

            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "SSSorular_Panel"))
            {
                while (dr.Read())
                {
                    SSSoruTablo.Add(new SSorulan
                    {
                        id = dr.GetInt32(dr.GetOrdinal("id")),
                        soru = dr.GetString(dr.GetOrdinal("soru"))
                    });
                }
            }

            return SSSoruTablo;
        }

        public static SSorulanList Liste(int baslangic, int bitis, string parametre)
        {
            try
            {
                SSorulanList SSoruList = new SSorulanList();
                SqlParameter[] sqlParameter = new SqlParameter[] 
                { 
                    new SqlParameter("@Baslangic", baslangic),
                    new SqlParameter("@Bitis", bitis),
                    new SqlParameter("@parametre", parametre)
                };

                using (SqlDataReader dr = SqlHelper.ExecuteReader("SSSorulanlar_Listele", sqlParameter))
                {
                    while (dr.Read())
                    {
                        SSoruList.Add(new SSorulan
                        {
                            soru = dr.GetString(dr.GetOrdinal("soru")),
                            cevap = dr.GetString(dr.GetOrdinal("cevap")),
                            id = dr.GetInt32(dr.GetOrdinal("id"))
                        });
                    }
                }

                return SSoruList;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public static int ListItemCount(string parametre)
        {
            try
            {
               SqlParameter sqlNo = new SqlParameter("@parametre", parametre );
               return (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "SSSorulanlar_SayfaNo", sqlNo);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static int ItemCount()
        {
            try
            {
                string query = "SELECT COUNT(id) FROM tbl_SSSorulanlar";
                return (int)SqlHelper.ExecuteScalar(CommandType.Text, query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
