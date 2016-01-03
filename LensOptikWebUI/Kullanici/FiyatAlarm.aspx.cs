using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataAccessLayer;
using LoggerLibrary;

public partial class Kullanici_Fiyat_Alarm: System.Web.UI.Page
{
    public int uyeId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoginKontrol();
        Page.Title = KullaniciOperasyon.GetName() + " | Fiyat Alarm Listesi";

        if (!IsPostBack)
        {
            kulaniciFiyatIndirim(uyeId);
        }
    }

    private void LoginKontrol()
    {
        if (KullaniciOperasyon.LoginKontrol())
        {
            uyeId = KullaniciOperasyon.GetId();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    #region Kulanıcı Fiyat İndirim Listeleme İşlemi
    private void kulaniciFiyatIndirim(int uyeId)
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@uyeId", uyeId);

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "kullanici_IndirimListele", parametre);

            if (ds.Tables[0].Rows.Count == 0)
            {
                Mesaj.Info("Fiyat Alarm Listesinde Ürün Bulunamadı.");
            }


            gvwListe.DataSource = ds;
            gvwListe.DataBind();
        }
        catch (Exception hata)
        {
            Mesaj.ErrorSis("Fiyat Alarm Lisrteleme Hatası");
            LogManager.SqlDB.Write("Kulanıcı Fiyat Alarm Lisrteleme Hatası", hata);
        }
    }
    #endregion

    #region Kulanıcı Fiyat Alarm Ürün Silme
    protected void grwKulaniciFiyatIndirim_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "fiyatIndirimSil")
        {
            string id = e.CommandArgument.ToString();
            try
            {
                SqlParameter parametre = new SqlParameter("@id", Convert.ToInt32(id));

                SqlHelper.ExecuteNonQuery("kullanici_IndirimSil", parametre);

                kulaniciFiyatIndirim(uyeId);
            }
            catch (Exception hata)
            {
                Mesaj.ErrorSis("Fiyat alarm  ürün silinirken hata oluştu.");
                LogManager.SqlDB.Write("Fiyat alarm  ürün silinirken hata oluştu", hata);
            }
        }
    }
    #endregion 


    protected void gvwListe_Paging(object sender, GridViewPageEventArgs e)
    {
        gvwListe.PageIndex = e.NewPageIndex;
        kulaniciFiyatIndirim(uyeId);
    }
}