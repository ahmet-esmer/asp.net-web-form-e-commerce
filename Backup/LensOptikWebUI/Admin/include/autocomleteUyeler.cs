using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using BusinessLayer;



[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class autocomleteUyeler : System.Web.Services.WebService
{

    [WebMethod]
    public string[] arama(string prefixText, int count)
    {
        using (SqlConnection baglan = new SqlConnection(ConnectionString.Get))
        {
            ArrayList user = new ArrayList();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = baglan;
                cmd.Connection.Open();
                cmd.CommandText = "kullanici_Autocomlet";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@data", SqlDbType.NVarChar);
                cmd.Parameters["@data"].Value = prefixText;
               
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            user.Add(dr[0].ToString());
                        }
                    }
                }
                
                return (string[])(user.ToArray(typeof(string)));
            }
        }
    }

}


