using System;
using System.Collections.Generic;
using BusinessLayer.Cashing;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class Include_RightColumn_ZiyaretciDefteri : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ZiyaretciKayitListele();
        }
    }

    private void ZiyaretciKayitListele()
    {
        try
        {
            if (CacheStorage.Exists(CacheKeys.GuestBook))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.GuestBook))
                    {
                        CacheStorage.Store(CacheKeys.GuestBook, ZiyaretciDefteriDB.Get(), "tbl_ziyaretciDefteri");
                    }
                }
            }

            List<ZiyaretciDefteri> ZiyaretciTablo = Cache[CacheKeys.GuestBook] as List<ZiyaretciDefteri>;

            if (ZiyaretciTablo.Count > 0)
            {
                pnlDuyuru.Visible = true;
                rptZiyaretci.DataSource = ZiyaretciTablo;
                rptZiyaretci.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Ziyaretçi Defteri Hatası", ex);
        }
    }

}