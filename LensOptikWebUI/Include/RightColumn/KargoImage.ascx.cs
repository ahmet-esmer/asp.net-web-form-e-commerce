using System;
using System.Data;
using System.Data.SqlClient;
using LoggerLibrary;
using DataAccessLayer;
using BusinessLayer;
using BusinessLayer.Cashing;
using ModelLayer;

public partial class Include_RightColumn_KargoImage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SagPanelResimGetir();
        }
    }


    private void SagPanelResimGetir()
    {
        try
        {
            if (CacheStorage.Exists(CacheKeys.CargoImage))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.CargoImage))
                    {
                        CacheStorage.Store(CacheKeys.CargoImage, ResimDB.Get("sagKargo"), "tbl_Resimler");
                    }
                }
            }

            Resim panelResim = CacheStorage.Retrieve<Resim>(CacheKeys.CargoImage);

            if (panelResim.resimAdi != null)
            {
                imgSagKargo.ImageUrl = "~/Products/Flash/" + panelResim.resimAdi;
                kargoAlani.Visible = true;
            }

        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Sağ Panel Kargo Hatası", hata);
        }
    }
}