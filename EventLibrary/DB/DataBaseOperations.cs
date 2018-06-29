using EventLibrary.Interfaces;
using EventLibrary.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace EventLibrary.DB
{
    public class DataBaseOperations : IDbOperations
    {
        private readonly EmailService _emailService = new EmailService();
        private readonly List<DB.Event> _eventList = new List<DB.Event>();
        private List<DB.Event> ParseToDbEventList(IEnumerable<EventClasses.Event> eventsList)
        {
            foreach (var item in eventsList)
            {
                DB.Event dbEvent = new Event
                {
                    Date = item.Date,
                    Description = item.Description,
                    Link = item.Link,
                    Name = item.Name,
                    City = item.City
                };
                _eventList.Add(dbEvent);
            }
            return _eventList;
        }
        public void Insert(IEnumerable<EventClasses.Event> eventsList)
        {
            var parsedList = ParseToDbEventList(eventsList);
            using (ItEventsParserEntities context = new ItEventsParserEntities())
            {
                if (!(context.Events.Any()))
                {
                    foreach (var item in parsedList)
                    {
                        context.Events.Add(item);
                        context.SaveChanges();
                    }
                }
                else
                {
                    foreach (var item in parsedList)
                    {
                        if (!(context.Events.Any(obj => obj.Name == item.Name && obj.Link == item.Link)))
                        {
                            context.Events.Add(item);
                            context.SaveChanges();
                            try
                            {
                                var body = _emailService.PrepareBody(item);
                                var mail = _emailService.Create(body);
                                _emailService.Send(mail);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }
                }
            }
        }
        public void ReadAll()
        {
            using (ItEventsParserEntities context = new ItEventsParserEntities())
            {
                if (context.Events.Any())
                {
                    var ptx = (from r in context.Events select r);
                }
                else
                {
                    Console.WriteLine("Table is empty");
                }
            }
        }
        public void ReadSingle(int? id, string name)
        {
            using (ItEventsParserEntities context = new ItEventsParserEntities())
            {
                if (context.Events.Any())
                {
                    if (id != null && name == null)
                    {
                        var ptx = (from r in context.Events where r.id == id select r);
                    }
                    else
                    {
                        var ptx = (from r in context.Events where r.Name == name select r);
                    }
                }
                else
                {
                    Console.WriteLine("Table is empty");
                }
            }
        }
        public void DeleteSingle(string name, int? id)
        {
            using (ItEventsParserEntities context = new ItEventsParserEntities())
            {
                if (context.Events.Any())
                {
                    if (id != null && name == null)
                    {
                        DB.Event db = new DB.Event()
                        {
                            id = id ?? default(int)
                        };
                        context.Events.Remove(db);
                        context.SaveChanges();
                    }
                    else
                    {
                        DB.Event db = new DB.Event()
                        {
                            Name = name
                        };
                        context.Events.Remove(db);
                        context.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("Table is empty");
                }
            }
        }

        public void UpdateEvent(int? dbEventId, string dbEventName, string newName, string newDesc, string newLink, string newCity, string newDate)
        {
            using (ItEventsParserEntities context = new ItEventsParserEntities())
            {
                if (context.Events.Any())
                {
                    if (dbEventId != null && dbEventName == null)
                    {
                        DB.Event db = new DB.Event()
                        {
                            Name = newName,
                            City = newCity,
                            Date = newDate,
                            Description = newDesc,
                            Link = newLink
                        };
                        var dep = context.Events.First(item => item.id == dbEventId);
                        dep.Name = newName;
                        dep.Description = newDesc;
                        dep.City = newCity;
                        dep.Date = newDate;
                        dep.Link = newLink;
                        context.SaveChanges();
                    }
                    else
                    {
                        DB.Event db = new DB.Event()
                        {
                            Name = newName,
                            City = newCity,
                            Date = newDate,
                            Description = newDesc,
                            Link = newLink
                        };
                        var dep = context.Events.First(item => item.Name == dbEventName);
                        dep.Name = newName;
                        dep.Description = newDesc;
                        dep.City = newCity;
                        dep.Date = newDate;
                        dep.Link = newLink;
                        context.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("Table is empty");
                }
            }
        }

        // wersja sql
        //private readonly string _connectionString = ConfigurationManager.AppSettings["connectionString"];
        // private readonly string _query = Resource1.query;
        //public void Insert(IEnumerable<Event> eventsList)
        //{
        //using (SqlConnection connection = new SqlConnection(_connectionString))
        //{
        //    foreach (var item in eventsList)
        //    {
        //        using (SqlCommand command = new SqlCommand(_query, connection))
        //        {
        //            connection.Open();


        //            command.Parameters.AddWithValue("@name", item.Name);
        //            command.Parameters.AddWithValue("@date", item.Date);
        //            command.Parameters.AddWithValue("@description", item.Description);
        //            command.Parameters.AddWithValue("@link", item.Link);
        //            int result = command.ExecuteNonQuery();
        //            if (result < 0)
        //            {
        //                Console.WriteLine("Error inserting data");
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    var body = _emailService.PrepareBody(item);
        //                    var mail = _emailService.Create(body);
        //                    _emailService.Send(mail);
        //                }
        //                catch (Exception e)
        //                {
        //                    Console.WriteLine(e);
        //                }
        //            }
        //            connection.Close();
        //        }
        //    }
        //}
    }
}

