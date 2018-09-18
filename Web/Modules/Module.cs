using System;
using System.Diagnostics;
using System.Web;

namespace Web.Modules
{
    public class TimerModule : IHttpModule
    {
        private Stopwatch timer;

        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += HandleBeginRequest;
            context.EndRequest += HandleEndRequest;
        }

        #endregion

        private void HandleBeginRequest(object src, EventArgs args)
        {
            timer = Stopwatch.StartNew();
        }

        private void HandleEndRequest(object src, EventArgs args)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Write(string.Format(
                "<br/>" +
                "<div style='color:gray;'> Time of processing: {0:F5} </div>",
                ((float)timer.ElapsedTicks) / Stopwatch.Frequency));
        }
    }
}
