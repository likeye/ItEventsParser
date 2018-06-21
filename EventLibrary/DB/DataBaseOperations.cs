using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using EventLibrary.Interfaces;
using EventLibrary.EventClasses;
using EventLibrary.SMTP;

namespace EventLibrary.DB
{
    public class DataBaseOperations : IDbOperations
    {
        private readonly SmtpOperations _smtpOperations = new SmtpOperations();
        private readonly string _connectionString = ConfigurationManager.AppSettings["connectionString"];
        private readonly string _query = Resource1.query;
        public void Insert(IEnumerable<Event> eventsList)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in eventsList)
                {
                    using (SqlCommand command = new SqlCommand(_query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@name", item.Name);
                        command.Parameters.AddWithValue("@date", item.Date);
                        command.Parameters.AddWithValue("@description", item.Description);
                        command.Parameters.AddWithValue("@link", item.Link);
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Console.WriteLine("Error inserting data");
                        }
                        else
                        {
                            var body =_smtpOperations.BodyOfEmail(item);
                            var mail =_smtpOperations.CreateEmail(body);
                            _smtpOperations.SendEmail(mail);
                        }
                        connection.Close();
                    }
                }
            }
        }
    }
}
