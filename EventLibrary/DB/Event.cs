//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventLibrary.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string City { get; set; }
        public string HasSentEmail { get; set; }
    }
}
