using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BasicSamples.Data
{
    public partial class DataSource
    {
        public async Task<List<sqlite_master>> AdvGetTablesAsync()
        {
            var db = CreateAsyncConnection();

            var tables = await db.Table<sqlite_master>()
                .Where(t => t.type == "table")
                .OrderBy(t => t.name)
                .ToListAsync();

            return tables;
        }


    }
}
