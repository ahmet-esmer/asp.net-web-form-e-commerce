using System;
using ModelLayer;

public partial class Market_OdemeMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void ActifMenu(MenuMap Secenek)
    {

        if (Secenek == MenuMap.kart)
        {
            hlKart.CssClass = "OdemeMenu aktif";
        }
        else if (Secenek == MenuMap.havale)
        {
            hlHavale.CssClass = "OdemeMenu aktif";
        }
        else if (Secenek == MenuMap.kapida)
        {
            hlKapida.CssClass = "OdemeMenu aktif";
        }
       
    }
}