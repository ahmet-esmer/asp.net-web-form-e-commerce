using System;
using System.Web.UI;
using BusinessLayer.PagingLink;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using ServiceLayer;

public partial class Icerik_ZiyaretciDefteri : Page
{
    private Paging paging;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ZiyaretciKayitSayisi();
            ZiyaretciDefteriListele();
            PageMetaTag();
        }
    }

    private void ZiyaretciKayitSayisi()
    {
        try
        {
            paging = new Paging();
            paging.CurentPage = QueryString.Value<int>("Sayfa");
            paging.TotolItem = ZiyaretciDefteriDB.ItemCount("web");
            paging.QueryString = Request.QueryString;
          
            ltlSayfalama.Text = PagingLinkWeb.GetHtmlCode(paging);
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Ziyaretçi Defteri Kayıt Toplam", ex);
        }
    }

    #region Ziyaretçi Defteri Listeleme İşlemi
    private void ZiyaretciDefteriListele()
    {
        try
        {
            rptZiyaretci.DataSource = ZiyaretciDefteriDB.Liste( paging.StartItem, paging.EndItem, "web");
            rptZiyaretci.DataBind();
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Ziyaretçi Defteri Listeleme: ", ex);
        }
    }
    #endregion

    #region Sayfa Metatağ Getirme İşlemi
    public void PageMetaTag()
    {
        try
        {
            if (RouteData.Values["sayfaId"] != null)
            {
                MetaTag metaTag = IcerikDB.MetaTag(Convert.ToInt32(RouteData.Values["sayfaId"]));
                Page.Title = metaTag.Title;
                Page.MetaDescription = metaTag.Description;
                Page.MetaKeywords = metaTag.Keywords;
            }
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Ziyaretçi defteri metatag: ", ex);
        }
    }
    #endregion
}