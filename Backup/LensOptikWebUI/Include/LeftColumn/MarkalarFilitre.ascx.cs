using System;
using System.Web.UI.WebControls;
using BusinessLayer.Security;
using DataAccessLayer;
using LoggerLibrary;

public partial class IncludeMarkalarFilitre : System.Web.UI.UserControl
{
    string serial;
    string kategoriAdi;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Page.RouteData.Values["katId"] != null)
            {
                kategoriAdi = GuvenlikIslemleri.hackKontrol(Page.RouteData.Values["katId"].ToString()); 
                serial = GuvenlikIslemleri.hackKontrol(Page.RouteData.Values["katId"].ToString());
                MarkaListeleme();
            }
        }
    }

    #region Marka Listeleme İşlemi
    protected void MarkaListeleme()
    {
        try
        {
            rptMarkalar.DataSource = MarkaDB.Liste("kategori", serial);
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

            if (Request.QueryString["markaId"] != null)
            {
                if (MarkaId.Value.ToString() == Request.QueryString["markaId"].ToString())
                {
                    kategoriCss.Style.Add("background-color", "#d5ebf5");
                    kategoriCss.Style.Add("border", "1px solid #9ad0e7");
                }
            }
        }
    }
}