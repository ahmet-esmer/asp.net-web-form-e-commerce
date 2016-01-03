using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLayer;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class HataMesajlariDB :BaseDB
    {

   

        public static int ItemCount()
        {
            try
            {
                string query = "SELECT COUNT(id) FROM tbl_hataMesajlari";
                return (int)SqlHelper.ExecuteScalar(CommandType.Text,query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
