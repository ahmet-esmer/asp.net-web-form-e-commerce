using System;
using BusinessLayer;
using BusinessLayer.Cashing;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class Include_RightColumn_AltFlash : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SagPanelFlashAlt();
        }
    }

    private void SagPanelFlashAlt()
    {
        try
        {
       
            if (CacheStorage.Exists(CacheKeys.RightFlash))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.RightFlash))
                    {
                        CacheStorage.Store(CacheKeys.RightFlash, ResimDB.Get("sagFlash"), "tbl_Resimler");
                    }
                }
            }

            Resim panelResim = CacheStorage.Retrieve<Resim>(CacheKeys.RightFlash);

            if (panelResim.resimAdi != null)
            {
                ltlFlash.Text = Flash.swfTool(ResolveUrl("~/Products/Flash/" + panelResim.resimAdi), 190, 416);
                sagAltFlash.Visible = true;
            } 
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Sağ panel alt flash", ex);
        }
    }

}