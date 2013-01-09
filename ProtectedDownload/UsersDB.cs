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

		public int InsertUser(UserDetails user)
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("InsertUser", con);
			cmd.CommandType = CommandType.StoredProcedure;
    
			cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 50));
			cmd.Parameters["@FirstName"].Value = user.FirstName;
			cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 50));
			cmd.Parameters["@LastName"].Value = user.LastName;
            cmd.Parameters.Add(new SqlParameter("@EmailAddress", SqlDbType.NVarChar, 50));
            cmd.Parameters["@EmailAddress"].Value = user.Email;
			cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 4));
			cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
		
			try 
			{
				con.Open();
				cmd.ExecuteNonQuery();
				return (int)cmd.Parameters["@ID"].Value;
			}
			catch (SqlException err) 
			{
				// Replace the error with something less specific.
				// You could also log the error now.
				throw new ApplicationException("Data error." + err.ToString());
			}
			finally 
			{
				con.Close();			
			}
		}

        public void MarkUserEmailVerifyied(int nUser)
        {

            string queryString = "UPDATE Users SET AccountVerified=1 WHERE ID=" + nUser;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(queryString, con);
            cmd.CommandType = CommandType.Text;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error." + err.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// This member returns true if the email address of the user has been verified 
        /// by using a download link that was sent by email.
        /// </summary>
        /// <param name="nUser"></param>
        /// <returns></returns>

        public bool HasUserVerifiedEmail(int nUser)
        {

            bool retVal = false;

            string queryString = "Select * From Users WHERE AccountVerified=1 AND ID=" + nUser;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(queryString, con);
            DataTable dt = new DataTable();

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);

                retVal = (dt.Rows.Count > 0);

            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error." + err.ToString());
            }
            finally
            {
                con.Close();
            }

            return retVal;
        }
    }
}