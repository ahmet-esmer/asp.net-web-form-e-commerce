using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace AddPrefixModule
{
    public class Redirector :IHttpModule
    {
        private static Regex regex = new Regex("(http|https)://www\\.", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public void Dispose()
        {

        }


        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            Uri url = application.Context.Request.Url;

            //bool hasWWW = regex.IsMatch(url.ToString());

            //LoggerLibrary.LogManager.SqlDB.Write(url.ToString(), url.Host.ToString());

            if (url.AbsolutePath.Contains("/main") || url.ToString().Contains("default.asp"))
            {
                application.Context.Response.RedirectPermanent("~/");
            }
            else if  (!regex.IsMatch(url.ToString()))
            {
                String newUrl = url.Scheme + "://www." + url.Host + url.PathAndQuery;
                application.Context.Response.Redirect(newUrl);
            } 
        }
    }
}
