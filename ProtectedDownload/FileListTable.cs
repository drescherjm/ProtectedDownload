using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ProtectedDownload
{
    public class FileListTable
    {
        private string connectionString;

        //////////////////////////////////////////////////////////////////////////

        public FileListTable()
		{
			// Get connection string from web.config.
            connectionString = WebConfigurationManager.ConnectionStrings["MainDB"].ConnectionString;
		}
		public FileListTable(string connectionString)
		{
			this.connectionString = connectionString;
		}
        
        public FileListDetails GetFileDetails(int nID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetFileUsingID", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 4));
            cmd.Parameters["@ID"].Value = nID;

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (!reader.HasRows) return null;

                reader.Read();

                FileListDetails fld = new FileListDetails((int)reader["ID"], (string)reader["FileName"], (string)reader["Description"]);

                reader.Close();

                return fld;
                
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

            //return retVal;
        }
    }
}