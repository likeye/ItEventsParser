using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLibrary.DB
{
    public class Events
    {
        public Events() { }

        public int id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string City { get; set; }
        public string HasSentEmail { get; set; }
    }
}
