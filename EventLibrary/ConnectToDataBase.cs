using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLibrary
{
    public class ConnectToDataBase
    {
        private void Conection()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server= []; Database = []; Trusted_Connection = true";

            }
        }
    }
}
