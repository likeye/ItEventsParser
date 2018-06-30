using EventLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventLibrary.DB
{
    public class DataBaseOperations : IDbOperations
    {
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
                    City = item.City,
                    HasSentEmail = null
                };
                _eventList.Add(dbEvent);
            }
            return _eventList;
        }
        public void Insert(IEnumerable<EventClasses.Event> eventsList)
        {
            var parsedList = ParseToDbEventList(eventsList);
            using (ItEventsParserEntity context = new ItEventsParserEntity())
            {
                if (!(context.Events.Any()))
                {
                    foreach (var item in parsedList)
                    {
                        item.HasSentEmail = "Yes";
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
                        }
                    }
                }
            }
        }
        public IEnumerable<DB.Event> ReadAllToList()
        {
            using (ItEventsParserEntity context = new ItEventsParserEntity())
            {
                if (context.Events.Any())
                {
                    var ptx = (from r in context.Events select r);
                    var newList = ptx.ToList<DB.Event>();
                    return newList;
                }
                else
                {
                    throw new ArgumentException("Table is empty");
                }
            }
        }
        public string ReadSingle(string name)
        {
            using (ItEventsParserEntity context = new ItEventsParserEntity())
            {
                if (context.Events.Any())
                {
                    var ptx = context.Events.Find(name);
                    return $" name: {ptx.Name} \n date: {ptx.Date} \n desc: {ptx.Description} \n link: {ptx.Link} \n city: {ptx.City} \n sent: {ptx.HasSentEmail}";
                }
                else
                {
                    return "Table is empty";
                }
            }
        }
        public string ReadSingle(int id)
        {
            using (ItEventsParserEntity context = new ItEventsParserEntity())
            {
                if (context.Events.Any())
                {
                    var ptx = context.Events.Find(id);
                    return $" name: {ptx.Name} \n date: {ptx.Date} \n desc: {ptx.Description} \n link: {ptx.Link} \n city: {ptx.City} \n sent: {ptx.HasSentEmail}";
                }
                else
                {
                    return "Table is empty";
                }
            }
        }
        public void DeleteSingle(int id)
        {
            using (ItEventsParserEntity context = new ItEventsParserEntity())
            {
                if (context.Events.Any())
                {
                    DB.Event db = new DB.Event()
                    {
                        id = id 
                    };
                    context.Events.Attach(db);
                    context.Events.Remove(db);
                    context.SaveChanges();
                }
            }
        }
        public void DeleteSingle(string name)
        {
            using (ItEventsParserEntity context = new ItEventsParserEntity())
            {
                if (context.Events.Any())
                {
                    DB.Event db = new DB.Event()
                    {
                        Name = name
                    };
                    context.Events.Attach(db);
                    context.Events.Remove(db);
                    context.SaveChanges();
                }
            }
        }
        public void UpdateEvent(int dbEventId, DB.Event newEvent)
        {
            using (ItEventsParserEntity context = new ItEventsParserEntity())
            {
                if (context.Events.Any())
                {
                    var dep = context.Events.First(item => item.id == dbEventId);
                    if (newEvent.Name != null)
                    {
                        dep.Name = newEvent.Name;
                    }
                    if (newEvent.City != null)
                    {
                        dep.City = newEvent.City;
                    }
                    if (newEvent.Date != null)
                    {
                        dep.Date = newEvent.Date;
                    }
                    if (newEvent.Description != null)
                    {
                        dep.Description = newEvent.Description;
                    }
                    if (newEvent.Link != null)
                    {
                        dep.Link = newEvent.Link;
                    }
                    if (newEvent.HasSentEmail != null)
                    {
                        dep.HasSentEmail = newEvent.HasSentEmail;
                    }
                    context.SaveChanges();
                }
            }
        }
        public void UpdateEvent(string dbEventName, DB.Event newEvent)
        {
            using (ItEventsParserEntity context = new ItEventsParserEntity())
            {
                if (context.Events.Any())
                {
                    var dep = context.Events.First(item => item.Name == dbEventName);
                    if (newEvent.Name != null)
                    {
                        dep.Name = newEvent.Name;
                    }
                    if (newEvent.City != null)
                    {
                        dep.City = newEvent.City;
                    }
                    if (newEvent.Date != null)
                    {
                        dep.Date = newEvent.Date;
                    }
                    if (newEvent.Description != null)
                    {
                        dep.Description = newEvent.Description;
                    }
                    if (newEvent.Link != null)
                    {
                        dep.Link = newEvent.Link;
                    }
                    if (newEvent.HasSentEmail != null)
                    {
                        dep.HasSentEmail = newEvent.HasSentEmail;
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}

