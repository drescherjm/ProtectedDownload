using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtectedDownload
{
    public class RequestDetails
    {
        private int userID;
		private string tokenString;
        
		public int UserID
		{
			get {return userID;}
			set {userID = value;}
		}
		public string Token
		{
			get {return tokenString;}
			set {tokenString = value;}
		}
				
		public RequestDetails(int userID, string strToken)
		{
			this.userID = userID;
            this.tokenString = strToken;
		}

        public RequestDetails() { }
    }
}