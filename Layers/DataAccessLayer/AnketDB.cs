using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class AnketDB :BaseDB
    {
        public static DataSet GetByParam(string parametre)
        {
            SqlParameter sqlParametre = new SqlParameter("@parametre", parametre);
            return SqlHelper.ExecuteDataset("anket_KayitListele", sqlParametre);
        }

    }
}
