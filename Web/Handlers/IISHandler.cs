using System;
using System.IO;
using System.Web;

namespace Web.Handlers
{
    public class IISHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string contents = GetPageContent(context);

            string browser = context.Request.Browser.Browser;
            string user_agent = context.Request.UserAgent;
            string url = context.Request.RawUrl;
            string ip = context.Request.UserHostAddress;

            DateTime dt;
            string useUtc = context.Request.QueryString["utc"];
            if (!string.IsNullOrEmpty(useUtc) && useUtc.Equals("true"))
            {
                dt = DateTime.UtcNow;
            }
            else
            {
                dt = DateTime.Now;
            }

            context.Response.Write(contents + string.Format("<h1>{0}</h1>",
                               dt.ToLongTimeString()) + "<p>Browser: " + browser + "</p><p>User-Agent: " + user_agent + "</p><p>Url: " + url +
                               "</p><p><p>IP-address: " + ip + "</p>");
        }

        private static string GetPageContent(HttpContext context)
        {
            StreamReader sr = File.OpenText(context.Request.PhysicalPath);
            string contents = sr.ReadToEnd();
            sr.Close();

            return contents;
        }

        #endregion
    }
}
