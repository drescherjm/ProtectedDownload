using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProtectedDownload
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = TextBox1.Text;

            if (!string.IsNullOrEmpty(email)) {

//                 string encode = DownloadToken.generate(email);
//                 Response.Write("Encoded ID= " + encode);
// 
//                 Response.Write("Decoded " + DownloadToken.decode(encode));

                Response.Redirect(Server.MapPath("./RequestFile.aspx"));
            }
        }
    }
}