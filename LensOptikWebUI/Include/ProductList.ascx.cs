using System;
using System.Collections.Generic;
using ModelLayer;

namespace LensOptikWebUI.Include
{
    public partial class inculude_product_list : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public List<Urun> ProductsToDisplay
        {
            set
            {
                this.rptUrun.DataSource = value;
                this.rptUrun.DataBind();
            }
        }

        public string PagingLinkToDisplay
        {
            set 
            {
                this.ltlSayfalama.Text = value;
            }
        }

    }
}