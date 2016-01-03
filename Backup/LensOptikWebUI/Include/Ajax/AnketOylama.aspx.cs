using System;
using System.Data.SqlClient;
using System.Web;
using DataAccessLayer;
using BusinessLayer;
using System.Data;

public partial class Include_Ajax_AnketOylama : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [System.Web.Services.WebMethod]
    public static string Kayit(string id)
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@id", id);
            SqlHelper.ExecuteNonQuery("anketSoru_Oyla", parametre);

            HttpCookie anket = new HttpCookie("AnketOylama");
            anket.Values.Add("Anket", "Oy Kullanıldı");

            HttpContext.Current.Response.Cookies.Add(anket);

            return "1";
        }
        catch (Exception)
        {
            return "0";
        }
    }
}