using System;
using System.Data;
using System.Data.SqlClient;
using LoggerLibrary;
using DataAccessLayer;
using BusinessLayer.Security;
using BusinessLayer;

public partial class Include_Ajax_MailKayit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [System.Web.Services.WebMethod]
    public static string Kayit(string mail)
    {
        string retVal;

        try
        {
            SqlParameter[] parametre = new SqlParameter[3];
            string ePosta = GuvenlikIslemleri.hackKontrol(mail);

            if (GenelFonksiyonlar.GecerliMailAdresi(ePosta))
            {
                parametre[0] = new SqlParameter("@adSoyad", " ");
                parametre[1] = new SqlParameter("@mail", ePosta.ToLower());

                parametre[2] = new SqlParameter("@deger_dondur", SqlDbType.Int);
                parametre[2].Direction = ParameterDirection.Output;


                SqlHelper.ExecuteNonQuery("mailAdres_KayitEkle", parametre);

                if ((int)parametre[2].Value == 0)
                {
                    retVal = "Kayıt İşlemi Başarı ile Gerçekleşti.";
                }
                else
                {
                    retVal = "E-Posata Adresi Daha Önce Kaydedilmiştir.";
                } 
            }
            else
            {
                retVal = "Lütfen Geçerli E-Posta adresi yazınız.";
            }
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Mail List Ekleme", hata);

           retVal=  "Hata Oluştu";
        }

        return retVal;
    }
}