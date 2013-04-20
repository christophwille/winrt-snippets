using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BasicSamples.Data
{
    [Table("Speakers")]
    public class Speaker
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        [Ignore]
        public List<Session> Sessions { get; set; }
    }
}
