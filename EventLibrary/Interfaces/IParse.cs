﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventLibrary.EventClasses;
using EventLibrary.DB;
using EventLibrary.SMTP;
namespace EventLibrary.Interfaces
{
    public interface IParse
    {
        List<Event> Parse(string city,string type, string cost);
    }
}