using System;
using System.IO;
using System.Text;
using System.Web;
using ConfigLibrary;
using LoggerLibrary;

namespace GlobalErrorHandler
{
    public class ErrorHandler : IHttpModule
    {
     
        public void Dispose()
        {
         
        }

        public void Init(HttpApplication app)
        {
            app.Error += new EventHandler(app_Error);
        }

        void app_Error(object sender, EventArgs e)
        {
            try
            {
                HttpContext context = HttpContext.Current;
                HttpRequest request = context.Request;
                HttpResponse response = context.Response;
                Exception exception = context.Server.GetLastError();
                Exception exceptionBase = exception.GetBaseException();
                HttpException httpException = exception as HttpException;
                HttpBrowserCapabilities browser = request.Browser;

                string errorPage = "~/";

                StringBuilder sp = new StringBuilder();
                sp.Append("URL"+ request.Url.AbsoluteUri);
                sp.Append("<h6>Gelen URL</h6>" + request.ServerVariables["HTTP_REFERER"]);
                sp.Append("<h6>URL</h6>" + context.Request.Url.ToString());
                sp.Append("<h6>Clint Info</h6>");
                sp.Append(" BrowserTuru: " + browser.Browser + "<br/>");
                sp.Append(" BrowserAdi: " + browser.Type + "<br/>");
                sp.Append(" BrowserVersiyonu: " + browser.Version + "<br/>");
                sp.Append(" Rewrite:" + request.ServerVariables["HTTP_X_ORIGINAL_URL"] + "<br/>");
                sp.Append(" Client IP:" + request.ServerVariables["HTTP_CLIENT_IP"] + "<br/>");
                sp.Append(" Cookie:" + request.ServerVariables["HTTP_COOKIE"] + "<br/>");
                sp.Append(" Agent:" + request.UserAgent + "<br/>");
                sp.Append(" Referrer:" + request.ServerVariables["HTTP_REFERER"] + "<br/>");
                sp.Append(" User CPU:" + request.ServerVariables["HTTP_UA_CPU"] + "<br/>");

                sp.Append("<h6>Exception</h6> " + context.Server.HtmlEncode(exceptionBase.ToString()));
                sp.Append("<h6>Exception Source </h6> " + exceptionBase.Source);
                sp.Append("<h6>Stack Trace </h6> " + exceptionBase.StackTrace);
                sp.Append("<h6>TargetSite </h6> " + exceptionBase.TargetSite);

                sp.Append("<h6>--- Form Parametre ---</h6>");

                for (int i = 0; i < request.Form.Count; i++)
                {
                    if (i > 6)
                    {
                        sp.Append(request.Form.Keys[i].ToString() + ": " + request.Form[i].ToString() + "<br/>");
                    }
                }


                if (sp.ToString().ToLower().Contains("nvalid viewstate")
                    || sp.ToString().ToLower().Contains("failed to load viewstate")
                    || sp.ToString().ToLower().Contains("utm.gif")
                    || sp.ToString().ToLower().Contains("bingbot")
                    || sp.ToString().ToLower().Contains("googlebot")
                    || sp.ToString().ToLower().Contains("webresource.axd")
                    || sp.ToString().ToLower().Contains("scriptresource.axd")
                    || sp.ToString().ToLower().Contains("info@lensoptik.com.tr")
                    || sp.ToString().ToLower().Contains("maxurllength value"))
                {
                    context.Server.ClearError();
                    return;
                }

         
                if (httpException.GetHttpCode() == 404 )
                {
                    errorPage = "~/icerik/ErrorPage/404State.aspx";
                }
                else if (exceptionBase is HttpRequestValidationException || sp.ToString().Contains("potentially dangerous Request"))
                {
                    errorPage = "~/icerik/ErrorPage/Validation.aspx";
                    LogManager.Text.Write(exceptionBase.Message, sp.ToString());
                }
                else
                {
                    errorPage = "~/icerik/ErrorPage/hataSayfasi.aspx";
                    LogManager.SqlDB.Write(exceptionBase.Message, sp.ToString());
                }

                context.Server.ClearError();
                response.Redirect(string.Format("{0}?errorurl=1", errorPage));
                
            }
            catch (Exception ex)
            {
                ApplicationErrorWrite(ex);
            }
        }

        private void ApplicationErrorWrite(Exception exception)
        {

            FileStream fs = new FileStream(ConfigHelper.GetPhysicalPath(@"Logs\Application.txt"),
                FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            streamWriter.WriteLine(" ");
            streamWriter.WriteLine(exception.Message);
            streamWriter.WriteLine("İşlem Tarihi: " + DateTime.Now.ToString("dd MMMM yyyy dddd HH:mm"));
            streamWriter.WriteLine("Hata Mesaj  : " + exception.ToString());
            streamWriter.WriteLine("-------------------------------------------------------------------------");
            streamWriter.Flush();
            streamWriter.Close();

            HttpContext.Current.Response.Redirect("~/icerik/ErrorPage/ApplicationError.htm");
        }

    }
}
