using System;
using System.Web.UI.WebControls;
using LoggerLibrary;
using DataAccessLayer;
using ModelLayer;
using BusinessLayer.Cashing;

public partial class IncludeMarkalar : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MarkaListeleme();
        }
    }


    #region Marka Listeleme İşlemi
    protected void MarkaListeleme()
    {
        try
        {
            if (CacheStorage.Exists(CacheKeys.PanelBrand))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.PanelBrand))
                    {
                        CacheStorage.Store(CacheKeys.PanelBrand, MarkaDB.Liste("solPanel"), "tbl_markalar");
                    }
                }
            }

            rptMarkalar.DataSource = Cache[CacheKeys.PanelBrand] as MarkaList;
            rptMarkalar.DataBind();
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("marka  SolPanel Listeleme", hata);
        }
    }
    #endregion


    protected void rptMarkalar_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink kategoriCss = (HyperLink)e.Item.FindControl("hlMarka");
            HiddenField MarkaId = (HiddenField)e.Item.FindControl("hdfMarkaId");

            if (Page.RouteData.Values["markaId"] != null)
            {
                if (MarkaId.Value.ToString() == Page.RouteData.Values["markaId"].ToString())
                {
                    kategoriCss.Style.Add("background-color", "#d5ebf5");
                    kategoriCss.Style.Add("border", "1px solid #9ad0e7");
                }
            }
        }
    }
}