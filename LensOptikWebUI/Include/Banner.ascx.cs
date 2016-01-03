using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using BusinessLayer.Cashing;


public partial class include_banner : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BannerListele();
        }
    }

    #region Reklam Listeleme İşlemi
    private void BannerListele()
    {
        try
        {
      
            if (CacheStorage.Exists(CacheKeys.Banner))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.Banner))
                    {
                        CacheStorage.Store(CacheKeys.Banner, BannerDB.BannerList("reklamWeb"), "tbl_Resimler");
                    }
                }
            }

            rptBannerBuyuk.DataSource = Cache[CacheKeys.Banner] as List<Banner>;
            rptBannerBuyuk.DataBind();

            rptBannerKucuk.DataSource = Cache[CacheKeys.Banner] as List<Banner>;
            rptBannerKucuk.DataBind();

        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Anasayfa Banner", hata);
        }
    }
    #endregion
}