using System.Collections.Generic;
using System.Data.SqlClient;
using ModelLayer;
using System;
using System.Data;

namespace DataAccessLayer
{
    public class BankaMesajlariDB :BaseDB
    {
        public static int ItemCount()
        {
            try
            {
                string query = "SELECT COUNT(id) FROM tbl_bankaMesajlari";
                return (int)SqlHelper.ExecuteScalar(CommandType.Text, query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
