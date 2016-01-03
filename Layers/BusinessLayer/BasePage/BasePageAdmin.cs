using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace BusinessLayer.BasePage
{
     public class BasePageAdmin :Page
    {
        protected void mesajGosterOk(string str)
        {
            ((HtmlGenericControl)Master.FindControl("Mesaj_Ok")).Visible = true;
            ((Label)Master.FindControl("Mesaj_yaz_ok")).Text = str.ToString();
        }
        protected void mesajGizleOk()
        {
            ((HtmlGenericControl)Master.FindControl("Mesaj_Ok")).Visible = false;
        }

        protected void mesajGosterNo(string str)
        {
            ((HtmlGenericControl)Master.FindControl("Mesaj_No")).Visible = true;
            ((Label)Master.FindControl("Mesaj_yaz_no")).Text = str.ToString();
        }

        protected void mesajGizleNo()
        {
            ((HtmlGenericControl)Master.FindControl("Mesaj_No")).Visible = false;
        }

        protected void mesajGosterInfo(string str)
        {
            ((HtmlGenericControl)Master.FindControl("Mesaj_Info")).Visible = true;
            ((Label)Master.FindControl("Mesaj_yaz_Info")).Text = str.ToString();

        }

        protected void mesajGizlenfo()
        {
            ((HtmlGenericControl)Master.FindControl("Mesaj_Info")).Visible = false;

        }

        protected void mesajGosterSis(string str, Exception exception)
        {
            ((HtmlGenericControl)Master.FindControl("Mesaj_Sis")).Visible = true;
            ((Label)Master.FindControl("Mesaj_yaz_sis")).Text = str.ToString() + exception.ToString();

            LoggerLibrary.LogManager.Text.Write(str, exception);
        }

        protected void mesajGosterSis(string str, string exception)
        {
            ((HtmlGenericControl)Master.FindControl("Mesaj_Sis")).Visible = true;
            ((Label)Master.FindControl("Mesaj_yaz_sis")).Text = str.ToString() + exception.ToString();

            LoggerLibrary.LogManager.Text.Write(str, exception);
        }
    }
}
