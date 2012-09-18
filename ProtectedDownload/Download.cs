using System;
using System.Web;

namespace ProtectedDownload
{
    public class Download : IHttpHandler
    {
        public void ProcessRequest(System.Web.HttpContext context)
        {
            // Make the HTTP context objects easily available.
		    HttpResponse response = context.Response;
		    HttpRequest request = context.Request;
		    HttpServerUtility server = context.Server;

            string file = request.QueryString["file"];
            string id = request.QueryString["id"];

            if (IsAValidKey(id)) {
                StartDownload(context, file);
            }
            else
            {
                response.Write("The link you have used is either expired or is invalid.");
            }

        }

        public bool IsReusable
        {
            get { return true; }
        }
        
        private bool IsAValidKey(string key)
        {
            bool retVal = !string.IsNullOrEmpty(key);
            if (retVal) {
                retVal =  key.Contains("John");
            }
            return retVal;
        }

        private void StartDownload(System.Web.HttpContext context, string dowloadFile ) 
        {
            context.Response.Buffer = true;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", 
                "attachment; filename=" + dowloadFile);

            context.Response.ContentType = "application/zip";

            string path = context.Server.MapPath("./Files/" + dowloadFile);

            context.Response.WriteFile(path);

        }
    }
}