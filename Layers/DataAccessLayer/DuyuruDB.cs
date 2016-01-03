using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DuyuruDB : BaseDB
    {

        public static DataSet ResimAdiListele(int id)
        {
            SqlParameter[] parametre = new SqlParameter[] 
            { 
               new SqlParameter("@id", id)
            };

            return SqlHelper.ExecuteDataset("duyuru_Resim_Getir", parametre);

        }
    }
}
