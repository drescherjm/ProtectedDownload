using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        }
    }
}