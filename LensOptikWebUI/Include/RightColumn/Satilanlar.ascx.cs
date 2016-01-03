using System;
using System.Collections.Generic;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using BusinessLayer.Cashing;

public partial class Include_RightColumn_Satilanlar : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EnCokSatilanlar();
        }
    }

    private void EnCokSatilanlar()
    {
        try
        {
            if (CacheStorage.Exists(CacheKeys.TopSellers))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.TopSellers))
                    {
                        CacheStorage.Store(CacheKeys.TopSellers, UrunDB.EnCokSatilanlar(), "tbl_urunler");
                    }
                }
            }

            rptPanelSatilanler.DataSource = Cache[CacheKeys.TopSellers] as List<Urun>;
            rptPanelSatilanler.DataBind();
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Ürün en çok satanlar Listeleme Hatası", ex);
        }
    }
}