using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataAccessLayer;
using LoggerLibrary;

public partial class Kullanici_Favori : System.Web.UI.Page
{
    public int uyeId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoginKontrol();
        Page.Title = KullaniciOperasyon.GetName() + " | Favori Ürünler";

        if (!IsPostBack)
        {
            FavariUrunListeleme(uyeId);
        }
    }


    #region Login Kontrol
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
    #endregion


    #region Kulanıcı Favori Listeleme İşlemi
    private void FavariUrunListeleme(int uyeId)
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@uyeId", uyeId);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "kullanici_FavoriUrunListele", parametre);


            if (ds.Tables[0].Rows.Count == 0)
            {
                Mesaj.Info("Favori Ürününüz Bulunamadı.");
            }

            gvwListe.DataSource = ds;
            gvwListe.DataBind();
        }
        catch (Exception ex)
        {
            Mesaj.ErrorSis("Kulanıcı Sipariş Listeleme Hatası");
            LogManager.SqlDB.Write("Kulanıcı Sipariş Listeleme Hatası", ex);
        }
    }
    #endregion

    #region Kulanıcı Favori Ürün Silme
    protected void grwKulaniciFavoriler_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "favoriSil")
        {
            string id = e.CommandArgument.ToString();
            try
            {
                SqlParameter parametre = new SqlParameter("@favoriId", Convert.ToInt32(id));
                SqlHelper.ExecuteNonQuery("kullanici_FavoriUrunSil", parametre);
                FavariUrunListeleme(uyeId);
            }
            catch (Exception ex)
            {
                Mesaj.ErrorSis("Favorilerden ürün silinirken hata oluştu.");
                LoggerLibrary.LogManager.SqlDB.Write("Favorilerden ürün silinirken hata oluştu.", ex);
            }
        }
    }
    #endregion

    protected void gvwListe_Paging(object sender, GridViewPageEventArgs e)
    {
        gvwListe.PageIndex = e.NewPageIndex;
        FavariUrunListeleme(uyeId);
    }
}