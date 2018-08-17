using EventLibrary.Interfaces;
using EventLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace EventLibrary.DB
{
    public class DataBaseOperations : IDbOperations
    {
        private readonly List<EventDb> _eventList = new List<EventDb>();
        private readonly EventsParserEntity _eventsParserEntity;

        public DataBaseOperations()
        {
            _eventsParserEntity = new EventsParserEntity();
        }

        private List<EventDb> ParseToDbEventList(IEnumerable<Event> eventsList)
        {
            foreach (var item in eventsList)
            {
                var dbEvent = new EventDb
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

        public void Insert(IEnumerable<Event> eventsList)
        {
            var parsedList = ParseToDbEventList(eventsList);
            using (_eventsParserEntity)
            {
                if (!(_eventsParserEntity.Events.Any()))
                {
                    parsedList.ForEach(item =>
                    {
                        item.HasSentEmail = "Yes";
                        _eventsParserEntity.Events.Add(item);
                        _eventsParserEntity.SaveChanges();
                    });
                }
                else
                {
                    foreach (var item in parsedList)
                    {
                        if (_eventsParserEntity.Events.Any(obj => obj.Name == item.Name && obj.Link == item.Link))
                            continue;
                        _eventsParserEntity.Events.Add(item);
                        _eventsParserEntity.SaveChanges();
                    }
                }
            }
        }
        public IEnumerable<EventDb> ReadAllToList()
        {
            using (_eventsParserEntity)
            {
                if (!_eventsParserEntity.Events.Any()) throw new ArgumentException("Table is empty");
                var ptx = (from r in _eventsParserEntity.Events select r);
                var newList = ptx.ToList();
                return newList;
            }
        }
        public string ReadSingle(string name)
        {
            using (_eventsParserEntity)
            {
                if (!_eventsParserEntity.Events.Any()) return "Table is empty";
                var ptx = _eventsParserEntity.Events.Find(name);
                return $" name: {ptx.Name} \n date: {ptx.Date} \n desc: {ptx.Description} \n link: {ptx.Link} \n city: {ptx.City} \n sent: {ptx.HasSentEmail}";

            }
        }
        public string ReadSingle(int id)
        {
            using (_eventsParserEntity)
            {
                if (!_eventsParserEntity.Events.Any()) return "Table is empty";
                var ptx = _eventsParserEntity.Events.Find(id);
                return $" name: {ptx.Name} \n date: {ptx.Date} \n desc: {ptx.Description} \n link: {ptx.Link} \n city: {ptx.City} \n sent: {ptx.HasSentEmail}";

            }
        }
        public void DeleteSingle(int id)
        {
            using (_eventsParserEntity)
            {
                if (!_eventsParserEntity.Events.Any()) return;
                var db = new EventDb()
                {
                    id = id
                };
                _eventsParserEntity.Events.Attach(db);
                _eventsParserEntity.Events.Remove(db);
                _eventsParserEntity.SaveChanges();
            }
        }
        public void DeleteSingle(string name)
        {
            using (_eventsParserEntity)
            {
                if (!_eventsParserEntity.Events.Any()) return;
                var db = new EventDb()
                {
                    Name = name
                };
                _eventsParserEntity.Events.Attach(db);
                _eventsParserEntity.Events.Remove(db);
                _eventsParserEntity.SaveChanges();
            }
        }
        public void UpdateEvent(int dbEventId, EventDb newEvent)
        {
            using (_eventsParserEntity)
            {
                if (!_eventsParserEntity.Events.Any()) return;
                var dep = _eventsParserEntity.Events.First(item => item.id == dbEventId);
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
                _eventsParserEntity.SaveChanges();
            }
        }
    }
}

