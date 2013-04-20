using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DeploymentSample.Data;

namespace DeploymentSample.CreateDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            var ds = new SampleDataSource();

            ds.InitializeDatabase();
            ds.FillWithSampleData();

            Console.WriteLine("Done populating the database");
            Console.Read();
        }
    }
}
