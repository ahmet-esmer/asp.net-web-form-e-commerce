using System;
using DataAccessLayer;
using System.Collections.Generic;
using ModelLayer;
using BusinessLayer.Cashing;

public partial class include_altLinkler : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AltMenu();
        }
    }

    protected void AltMenu()
    {
        try
        {
            if (CacheStorage.Exists(CacheKeys.SubMenu))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.SubMenu))
                    {
                        CacheStorage.Store(CacheKeys.SubMenu, 
                            IcerikBaslikDB.ListelemeLink("altMenu"), "tbl_icerik_basliklar");
                    }
                }
            }
            rptAltMenu.DataSource = Cache[CacheKeys.SubMenu] as List<IcerikBaslik>;
            rptAltMenu.DataBind();
        }
        catch (Exception hata)
        {
            LoggerLibrary.LogManager.SqlDB.Write("Alt Menu", hata);
        }
    }
}