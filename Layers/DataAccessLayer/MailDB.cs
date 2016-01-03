using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class MailDB :BaseDB
    {

        public static int ItemCount()
        {
            try
            {
                string query = "SELECT COUNT(id) FROM tbl_mail_adresleri";
                return (int)SqlHelper.ExecuteScalar(CommandType.Text, query);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void TarihKaydet(string ePosta, string parametre)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[2];
                parameter[0] = new SqlParameter("@ePosta", SqlDbType.NVarChar);
                parameter[0].Value = ePosta;
                parameter[1] = new SqlParameter("@parametre", SqlDbType.NVarChar);
                parameter[1].Value = parametre;

                SqlHelper.ExecuteNonQuery("mail_KayitTarih", parameter);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
