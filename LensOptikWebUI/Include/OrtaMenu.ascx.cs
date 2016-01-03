using System;
using System.Collections.Generic;
using BusinessLayer.Cashing;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class include_orta_menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OrtaMenu();
            KullaniciPanel();
        }
    }

    protected void OrtaMenu()
    {
        try
        {

            if (CacheStorage.Exists(CacheKeys.CentralMenu))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.CentralMenu))
                    {
                        CacheStorage.Store(CacheKeys.CentralMenu, 
                            IcerikBaslikDB.ListelemeLink("ortaMenu"), "tbl_icerik_basliklar");
                    }
                }
            }

            rptOrtaMenu.DataSource = Cache[CacheKeys.CentralMenu] as List<IcerikBaslik>;
            rptOrtaMenu.DataBind();
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Orta Menu", hata);
        }
    }

    protected void KullaniciPanel()
    {
        if (Request.Cookies["LensOptikLogin"] != null)
        {
            pnlKulanici.Visible = false;
            pnlKulaniciList.Visible = true;
        }

        if (Request.Cookies["LensOptikAdminGiris"] != null)
        {
            kulaniciAdmin.Visible = true;
        }

    }
}