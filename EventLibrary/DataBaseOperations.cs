using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace EventLibrary
{
    public class DataBaseOperations
    {
        private string connectionString = ConfigurationManager.AppSettings["connectionString"];
       private String query = "IF NOT EXISTS (SELECT * FROM [Events] WHERE Name=@name AND Date=@date AND Description=@description AND Link=@link) " +
                              "INSERT INTO [Events] (Name,Date,Description,Link) VALUES (@name,@date,@description, @link)";

        public void InsertData(List<Events> eventsList)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    foreach (var item in eventsList)
                    {
                        command.Parameters.AddWithValue("@name", item.Name);
                        command.Parameters.AddWithValue("@date", item.Date);
                        command.Parameters.AddWithValue("@description", item.Description);
                        command.Parameters.AddWithValue("@link", item.Link);
                    }
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result <0)
                        Console.WriteLine("Error inserting data");
                }
            }
        }

        
    }
}
