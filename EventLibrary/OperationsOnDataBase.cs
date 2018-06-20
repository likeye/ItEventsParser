using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Configuration;

namespace EventLibrary
{
    public class OperationsOnDataBase
    {
        public void Conection()
        {
            string provider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                {
                    Console.Write("ConnectionError");
                    Console.ReadLine();
                    return;
                }

                connection.ConnectionString = connectionString;
                connection.Open();
                DbCommand command = factory.CreateCommand();
                if (command == null)
                {
                    Console.Write("CommandError");
                    Console.ReadLine();
                    return;
                }
                
                command.Connection = connection;
                command.CommandText = "INSERT INTO [Events] (Id,Name,Date,Description,Link) VALUES";

                using (DbDataReader dataReader= command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"{dataReader["Id"]}" + $"{dataReader["Name"]}" + $"{dataReader["Date"]}" + $"{dataReader["Link"]}" );
                    }
                }

                Console.ReadLine();
            }
        }
    }
}
