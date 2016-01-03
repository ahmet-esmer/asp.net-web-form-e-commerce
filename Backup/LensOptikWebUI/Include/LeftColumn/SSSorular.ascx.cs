using System;
using BusinessLayer.Cashing;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class Include_LeftColumn_SSSorular : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SSSoruListele();
        }
    }

    private void SSSoruListele()
    {
        try
        {
            SSorulanList SSSoruTablo;

            if (CacheStorage.Exists(CacheKeys.Questions))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.Questions))
                    {
                        CacheStorage.Store(CacheKeys.Questions, SSorularDB.Liste(), "tbl_SSSorulanlar");
                    }
                }
            }

            SSSoruTablo = Cache[CacheKeys.Questions] as SSorulanList;
            
            if (SSSoruTablo.Count == 0)
                pnlSSSorular.Visible = false;  

            rptSSSorular.DataSource = SSSoruTablo;
            rptSSSorular.DataBind();
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Duyuru Listeleme Hatası", ex);
        }
    }

}