﻿using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using BusinessLayer;



[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class KartSahibi : System.Web.Services.WebService
{

    [WebMethod]

    public string[] arama(string prefixText, int count)
    {

        SqlConnection baglan = new SqlConnection(ConnectionString.Get);
        SqlCommand komutver = new SqlCommand();

        komutver.Connection = baglan;
        komutver.CommandText = "siparis_Odeme_Autocomlet";
        komutver.CommandType = CommandType.StoredProcedure;
        komutver.Parameters.Add("@data", SqlDbType.NVarChar);
        string car = "";
        komutver.Parameters["@data"].Value = car + prefixText.ToString();

        SqlDataReader dr;
        ArrayList PN = new ArrayList();
        baglan.Open();
        dr = komutver.ExecuteReader();

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                PN.Add(dr[0].ToString());
            }

        }
        baglan.Close();
        baglan.Dispose();
        return (string[])(PN.ToArray(typeof(string)));


    }

}


