using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace BasicSamples.Data
{
    public partial class DataSource
    {
        public async Task<int> GetSessionCountAsync()
        {
            var db = CreateAsyncConnection();

            var result = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Session");
            return result;
        }

        public async Task<int> GetSessionCount2Async()
        {
            var db = CreateAsyncConnection();

            var result = await db.Table<Session>().CountAsync();
            return result;
        }

        public async Task<List<Session>> GetSessionsStartingWithSAsync()
        {
            var db = CreateAsyncConnection();

            var query = db.Table<Session>().Where(s => s.Title.StartsWith("S"));
            var matched = await query.ToListAsync();

            return matched;
        }

        public async Task<Speaker> GetSpeakerAsync(string name)
        {
            var db = CreateAsyncConnection();

            var speaker = await db.Table<Speaker>().Where(d => d.Name == name).FirstOrDefaultAsync();
            return speaker;
        }

        public async Task<List<Speaker>> GetSpeakersGraphAsync()
        {
            var db = CreateAsyncConnection();

            var speakers = await db.Table<Speaker>().ToListAsync();
            
            var speakerIds = speakers.Select(s => s.Id).ToList();
            var sessions = await db.Table<Session>().Where(s => speakerIds.Contains(s.SpeakerId)).ToListAsync();

            foreach (var speaker in speakers)
            {
                int speakerId = speaker.Id; // closure safety measure
                speaker.Sessions = sessions.Where(s => s.SpeakerId == speakerId).ToList();
            }

            return speakers;
        }
    }
}
