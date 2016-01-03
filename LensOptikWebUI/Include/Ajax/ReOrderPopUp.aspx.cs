using System;
using System.Data;
using System.Data.SqlClient;
using LoggerLibrary;
using DataAccessLayer;
using BusinessLayer;
using System.Text.RegularExpressions;

public partial class Include_ReOrder_PopUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [System.Web.Services.WebMethod]
    public static string Kayit(string uyeId)
    {
        string retVal = "işlem tamam";
        try
        {
            if (GenelFonksiyonlar.IsNumber(uyeId))
            {
                SqlParameter[] prm = new SqlParameter[3];
                prm[0] = new SqlParameter("@KulaniciId", SqlDbType.Int);
                prm[0].Value = uyeId ;
                prm[1] = new SqlParameter("@aciklama", SqlDbType.NVarChar);
                prm[1].Value = "siparisPopUp";
                prm[2] = new SqlParameter("@deger", SqlDbType.Bit);
                prm[2].Value = false;

                SqlHelper.ExecuteNonQuery("kullanici_SecenekEkle", prm); 
            }
            else
            {
                retVal = "Daha sonra tekrar deneyiniz.";
            }

           
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Kulanıcı sipariş Pop Up bir daha gösterme", ex);
            retVal = "Hata Oluştu";
        }

        return retVal;
    }
}