using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventLibrary;
namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            OperationsOnDataBase operations = new OperationsOnDataBase();
            operations.Conection();
            Console.ReadKey();
        }
    }
}
