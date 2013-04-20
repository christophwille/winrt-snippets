using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicSamples.Data
{
    public class SpeakerSessionModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public partial class DataSource
    {
        public async Task<List<SpeakerSessionModel>> QueryWithJoinAsync()
        {
            var db = CreateAsyncConnection();

            string query = @"SELECT Speakers.Name, Session.Title
                            FROM Speakers
                            INNER JOIN Session
                            ON Speakers.Id = Session.SpeakerId";

            var result = await db.QueryAsync<SpeakerSessionModel>(query);
            return result;
        }
    }
}
