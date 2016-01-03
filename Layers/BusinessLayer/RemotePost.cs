using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web;


namespace BusinessLayer
{
    public class RemotePost
    {
        private System.Collections.Specialized.NameValueCollection Inputs
        = new System.Collections.Specialized.NameValueCollection();

        public string Url = "";
        public string Method = "post";
        public string FormName = "form1";

        public void Add(string name, object value)
        {
            
            Inputs.Add(name, value as string );
        }

        public void Post()
        {
            try
            {
                HttpResponse response = HttpContext.Current.Response;

                response.Clear();
                response.Write("<html><head>");
                response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
                response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >",FormName, Method, Url));

                for (int i = 0; i < Inputs.Keys.Count; i++)
                {
                    response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">",
                        Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
                }

                response.Write("</form>");
                response.Write("</body></html>");
                response.End();

            }
            catch (Exception)
            {
               throw;
            }
           
        }
    }
}
