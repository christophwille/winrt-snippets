using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BasicSamples.Data
{
    public partial class DataSource
    {
        private const string DataBaseName = "demo.db3";

        // Create a synchronous connection, this one is disposable
        private SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(DataBaseName);
        }

        private SQLiteAsyncConnection CreateAsyncConnection()
        {
            return new SQLiteAsyncConnection(DataBaseName);
        }
    }
}
