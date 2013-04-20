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
        public const string DatabaseName = "sample.db3";

        public SQLiteAsyncConnection CreateAsyncConnection()
        {
            return new SQLiteAsyncConnection(DatabaseName);
        }

        public async Task<List<Speaker>> GetSpeakerAsync()
        {
            var db = CreateAsyncConnection();

            return await db.Table<Speaker>().ToListAsync();
        }
    }
}
