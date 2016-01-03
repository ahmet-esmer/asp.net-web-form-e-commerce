using System;
using System.Web;
using BusinessLayer.Cashing;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;


public partial class include_pop_up : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            popUpListele();
            ReOrderPopUP();
        }
    }

    private void popUpListele()
    {
        try
        {
            if (Request.Cookies["LensOptikPopUp"] == null)
            {

                if (CacheStorage.Exists(CacheKeys.PopUp))
                {
                    lock (new object())
                    {
                        if (CacheStorage.Exists(CacheKeys.PopUp))
                            CacheStorage.Store(CacheKeys.PopUp, ResimDB.Get("popUp"), "tbl_Resimler");
                    }
                }

                Resim panelResim = CacheStorage.Retrieve<Resim>(CacheKeys.PopUp);

                if (panelResim.resimAdi != null)
                {
                    imgPopUp.ImageUrl = "~/Products/Flash/" + panelResim.resimAdi;
                    hlLink.NavigateUrl = panelResim.resimBaslik;
                    dialog.Height = 570;
                    dialog.Width = 405;
                    pnlPopUp.Visible = true;
                }

                HttpCookie popUp = new HttpCookie("LensOptikPopUp");
                popUp.Values.Add("durum", "1");
                popUp.Expires = DateTime.Now.AddDays(120);
                Response.Cookies.Add(popUp);
            }          
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Pop Up Hatası", ex);
        }
    }

    private void ReOrderPopUP()
    {
        string userId = "0";
        try
        {
            if (Request.Cookies["SiparisAdetPopUp"] != null)
            {
                HttpCookie hatirlaYaz = Request.Cookies["SiparisAdetPopUp"];
                userId = hatirlaYaz["UserId"];
                hdfId.Value = userId;

                pnlPopUp.Visible = true;
                ckbSiparis.Visible = true;

                imgPopUp.ImageUrl = "~/Images/reorder.jpg";
                hlLink.NavigateUrl = "~/Kullanici/Siparisler.aspx";

                dialog.Height = 300;
                dialog.Width = 480;

                if (Request.Cookies["SiparisAdetPopUp"] != null)
                    Response.Cookies["SiparisAdetPopUp"].Expires = DateTime.Now.AddDays(-1);

            }
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Pop Up Hatası UserId: ", ex);
        }
    }

}