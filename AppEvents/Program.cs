﻿using System;
using EventLibrary;
namespace AppEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            OperationsOnDataBase operations = new OperationsOnDataBase();
            operations.Conection();

        }
    }
}
