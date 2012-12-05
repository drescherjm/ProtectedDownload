using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;


namespace ProtectedDownload
{
    public partial class RequestFile : System.Web.UI.Page
    {
        private UsersDB db = new UsersDB();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected bool canSubmit()
        {
            return regexEmailValid.IsValid && RequiredFieldValidator1.IsValid && RequiredFieldValidator2.IsValid && RequiredFieldValidator3.IsValid;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(canSubmit()) {
                submit();  
            }
        }

        protected void submit()
        {
            string strDownloadToken = DownloadToken.generate(TextBoxEmail.Text);

            int nUserID = db.InsertUser(new UserDetails(0, TextBoxFName.Text, TextBoxLName.Text, TextBoxEmail.Text));

            sendEmail(nUserID, TextBoxEmail.Text, strDownloadToken);
            
        }

        public static string ResolveMyUrl(string originalUrl)
        {

            if (originalUrl == null)

                return null;



            // *** Absolute path - just return

            if (originalUrl.IndexOf("://") != -1)

                return originalUrl;



            // *** Fix up image path for ~ root app dir directory

            if (originalUrl.StartsWith("~"))
            {

                string newUrl = "";

                if (HttpContext.Current != null)

                    newUrl = HttpContext.Current.Request.ApplicationPath +

                          originalUrl.Substring(1).Replace("//", "/");

                else

                    // *** Not context: assume current directory is the base directory

                    throw new ArgumentException("Invalid URL: Relative URL not allowed.");



                // *** Just to be sure fix up any double slashes

                return newUrl;

            }



            return originalUrl;

        }

        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {

            // *** Is it already an absolute Url?

            if (serverUrl.IndexOf("://") > -1)

                return serverUrl;



            // *** Start by fixing up the Url an Application relative Url

            string newUrl = ResolveMyUrl(serverUrl);



            Uri originalUri = HttpContext.Current.Request.Url;

            newUrl = (forceHttps ? "https" : originalUri.Scheme) +

                     "://" + originalUri.Authority + newUrl;



            return newUrl;

        } 

        protected void sendEmail(int nUserID, string strEmail, string strToken)
        {
            //Response.Write("UserID= " + nUserID + "<br>Email= " + strEmail + "<br>Token= " + strToken);

            RequestTable table = new RequestTable();

            table.InsertRequest(new RequestDetails(nUserID, strToken));

            string strURL = ResolveServerUrl("/download",false);

            string strLogoURL = ResolveServerUrl("/Images/Pitt-Logo.gif", false);

            string strMsg = "<body> <img src=\"" + strLogoURL + "\" /> <br> We have recieved your request to download our software package. The following is a link containing the software you requested. <br>" + strURL + "?&file=test.zip&id=" + strToken + " </body>";

            MailMessage msgeme = new MailMessage("\"John M. Drescher\" <jdresch@pitt.edu>", strEmail, "My Statistics Software Download Link", strMsg);
            msgeme.IsBodyHtml = true;

            //SmtpClient smtpclient = new SmtpClient("in.mailjet.com", 587);
            SmtpClient smtpclient = new SmtpClient();
            smtpclient.EnableSsl = true;
            //smtpclient.Credentials = new NetworkCredential("secret", "secret");
            //smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpclient.Send(msgeme);

            var response = base.Response;
            response.Redirect("./DownloadEmailSent.aspx");

        }
    }
}