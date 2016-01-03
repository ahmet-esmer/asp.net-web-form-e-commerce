using System;
using BusinessLayer.Cashing;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class Include_RightColumn_AltImage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SagPanelResimAlt();
        }
    }

    private void SagPanelResimAlt()
    {
        try
        {
  
            if (CacheStorage.Exists(CacheKeys.RightSubImage))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.RightSubImage))
                    {
                        CacheStorage.Store(CacheKeys.RightSubImage, ResimDB.Get("sagAlt"), "tbl_Resimler");
                    }
                }
            }

            Resim panelResim = CacheStorage.Retrieve<Resim>(CacheKeys.RightSubImage);

            if (panelResim.resimAdi !=  null )
            {
                hlISagAlt.NavigateUrl = panelResim.resimBaslik;
                imgSagAlt.ImageUrl = "~/Products/Flash/" + panelResim.resimAdi;
                sagAltPanel.Visible = true;
            }
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Sağ panel alt resim", hata);
        }
    }

}