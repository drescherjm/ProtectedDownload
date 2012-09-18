using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtectedDownload
{
    public class FileDenialHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
             context.Response.Redirect(context.Server.MapPath(
                 "./AccessDenied.aspx"));

            //context.Response.Redirect("AccessDenied.aspx");
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}