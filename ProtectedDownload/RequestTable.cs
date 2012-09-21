using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ProtectedDownload
{
    public class RequestTable
    {
        private string connectionString;

        //////////////////////////////////////////////////////////////////////////

        public RequestTable()
		{
			// Get connection string from web.config.
            connectionString = WebConfigurationManager.ConnectionStrings["MainDB"].ConnectionString;
		}
		public RequestTable(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public void InsertRequest(RequestDetails request)
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("InsertRequest", con);
			cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4));
            cmd.Parameters["@UserID"].Value = request.UserID;
			cmd.Parameters.Add(new SqlParameter("@Token", SqlDbType.UniqueIdentifier));
            cmd.Parameters["@Token"].Value = new Guid(request.Token);
       		
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
    }
}