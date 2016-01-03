using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataAccessLayer;

public partial class Kullanici_Puanlari : System.Web.UI.Page
{
    public int uyeId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoginKontrol();
        Page.Title = KullaniciOperasyon.GetName() + " | Puanlar";

        if (!IsPostBack)
        {
            kulaniciSiparisListele(uyeId);
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

    #region Kulanıcı Sipariş Listeleme İşlemi
    private void kulaniciSiparisListele(int uyeId)
    {
        try
        {
            gvwListe.DataSource = KullaniciPuanDB.Liste(uyeId);
            gvwListe.DataBind();
        }
        catch (Exception)
        {
            Mesaj.ErrorSis("Puan Listeleme Hatası");
        }
    }
    #endregion

    protected void gvwListe_Paging(object sender, GridViewPageEventArgs e)
    {
        gvwListe.PageIndex = e.NewPageIndex;
        kulaniciSiparisListele(uyeId);
    }
}