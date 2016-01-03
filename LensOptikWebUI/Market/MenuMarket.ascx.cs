using System;
using BusinessLayer;
using DataAccessLayer;
using ModelLayer;

public partial class Market_MarketMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AdresLink();

            if (Session["adresMenu"] != null)
            {
                hlAdres.Enabled = true;
            }

            if (Session["onayMenu"] != null)
            {
                hlOnay.Enabled = true;
            }
            if (Session["odemeMenu"] != null)
            {
                hlOdeme.Enabled = true;
            }
        }
    }

    private void AdresLink()
    {
        if (KullaniciAdresDB.AdresVarmi(KullaniciOperasyon.GetId()) > 0)
            hlAdres.NavigateUrl = "~/Market/TeslimatBilgisi.aspx";
        else
            hlAdres.NavigateUrl = "~/Market/ilkAdres.aspx";  
    }

    public void ActifMenu(MenuMap Secenek)
    {

        if (Secenek == MenuMap.ilkAdres || Secenek == MenuMap.AdresSec )
        {
            hlAdres.CssClass = "marketLink on";
        }
        else if (Secenek == MenuMap.Onay)
        {
            hlOnay.CssClass = "marketLink on";
        }
        else if (Secenek == MenuMap.Odeme)
        {
            hlOdeme.CssClass = "marketLink on";
        }
        else if (Secenek == MenuMap.SiparisOzeti)
        {
            hlOzet.CssClass = "marketLink on";
        }
    }
}