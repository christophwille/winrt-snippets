using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BasicSamples.Data
{
    public class Session
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed(Name="FK_SessionToSpeaker")]
        public int SpeakerId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}
