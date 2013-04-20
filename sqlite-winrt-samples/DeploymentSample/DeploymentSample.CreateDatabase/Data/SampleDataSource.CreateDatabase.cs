using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DeploymentSample.Data
{
    public partial class SampleDataSource
    {
        public SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(DatabaseName);
        }

        public void InitializeDatabase()
        {
            using (var db = CreateConnection())
            {
                db.CreateTable<Speaker>();
            }
        }

        public void FillWithSampleData()
        {
            using (var db = CreateConnection())
            {
                db.Insert(new Speaker() { Name = "Christoph Wille" });
                db.Insert(new Speaker() { Name = "Alexander Zeitler" });
                db.Insert(new Speaker() { Name = "Albert Weinert" });
            }
        }
    }
}
