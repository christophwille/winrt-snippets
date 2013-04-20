using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicSamples.Data
{
    public partial class DataSource
    {
        // This is the only synchronous demo in here, just to show how it would work
        public void InitializeDb()
        {
            using (var db = CreateConnection())
            {
                db.CreateTable<Speaker>();
                db.CreateTable<Session>();
            }
        }

        public async Task InitializeDbAsync()
        {
            var db = CreateAsyncConnection();

            // This would also work:
            //db.CreateTableAsync<Speaker>();
            //db.CreateTableAsync<Session>();

            await db.CreateTablesAsync(typeof (Speaker), typeof (Session));
        }

        // To reset the demos
        public async Task CleanDatabaseAsync()
        {
            var db = CreateAsyncConnection();

            try
            {
                await db.DropTableAsync<Session>();
                await db.DropTableAsync<Speaker>();
            }
            catch
            {
            }
        }
    }
}
