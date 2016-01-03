using System;
using BusinessLayer.Cashing;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class include_hediye_kampanya : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GiftCampaing();
        }
    }

    private void GiftCampaing()
    {
        try
        {
            if (CacheStorage.Exists(CacheKeys.GiftCampaign))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.GiftCampaign))
                    {
                        CacheStorage.Store(CacheKeys.GiftCampaign, ResimDB.Get("hediye"), "tbl_Resimler");
                    }
                }
            }

            Resim panelResim = CacheStorage.Retrieve<Resim>(CacheKeys.GiftCampaign);

            if (panelResim.resimAdi !=  null )
            {
                if (panelResim.resimBaslik == "")
                    hlHediye.NavigateUrl = "~/kampanyalar";
                else
                    hlHediye.NavigateUrl = panelResim.resimBaslik;

                imgHediyeKampanya.ImageUrl = "~/Products/Flash/" + panelResim.resimAdi;
                pnlKampanya.Visible = true;
            }
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Hediye panal:", ex);
        }
    }

}