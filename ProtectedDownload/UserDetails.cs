using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtectedDownload
{
    public class UserDetails
    {
        private int userID;
		private string firstName;
		private string lastName;


		public int UserID
		{
			get {return userID;}
			set {userID = value;}
		}
		public string FirstName
		{
			get {return firstName;}
			set {firstName = value;}
		}
		public string LastName
		{
			get {return lastName;}
			set {lastName = value;}
		}
		
		public UserDetails(int userID, string firstName, string lastName)
		{
			this.userID = userID;
			this.firstName = firstName;
			this.lastName = lastName;
		}

		public UserDetails(){}
    }
}