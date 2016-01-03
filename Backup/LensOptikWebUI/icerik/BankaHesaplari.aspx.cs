using System;
using DataAccessLayer;
using LoggerLibrary;


public partial class Icerik_BankaHesaplari : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BankaHesapBilgiListele();
        }
    }

    #region Banka Hesap Bilgisi  Listele
    private void BankaHesapBilgiListele()
    {
        try
        {
            rptHesapNo.DataSource = BankaHesaplariDB.HesapListe("web");
            rptHesapNo.DataBind(); 
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Banka Hesaplari Listelerken hata oluştu.", hata);
        }
    }
    #endregion

}