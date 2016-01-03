using System;

public partial class adminCikis : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["LensOptikAdminGiris"] != null)
            Response.Cookies["LensOptikAdminGiris"].Expires = DateTime.Now.AddDays(-1);

        if (Request.Cookies["LensOptikLogin"] != null)
            Response.Cookies["LensOptikLogin"].Expires = DateTime.Now.AddDays(-1);

        Response.Redirect("Default.aspx");
    }
}
