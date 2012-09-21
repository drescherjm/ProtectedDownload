using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ProtectedDownload
{
    public class UsersDB
    {
        private string connectionString;

        //////////////////////////////////////////////////////////////////////////

        public UsersDB()
		{
			// Get connection string from web.config.
            connectionString = WebConfigurationManager.ConnectionStrings["MainDB"].ConnectionString;
		}
		public UsersDB(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public int InsertEmployee(UserDetails user)
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("InsertUser", con);
			cmd.CommandType = CommandType.StoredProcedure;
    
			cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 50));
			cmd.Parameters["@FirstName"].Value = user.FirstName;
			cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 50));
			cmd.Parameters["@LastName"].Value = user.LastName;
			cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int, 4));
			cmd.Parameters["@EmployeeID"].Direction = ParameterDirection.Output;
		
			try 
			{
				con.Open();
				cmd.ExecuteNonQuery();
				return (int)cmd.Parameters["@EmployeeID"].Value;
			}
			catch (SqlException err) 
			{
				// Replace the error with something less specific.
				// You could also log the error now.
				throw new ApplicationException("Data error.");
			}
			finally 
			{
				con.Close();			
			}
		}
    }
}