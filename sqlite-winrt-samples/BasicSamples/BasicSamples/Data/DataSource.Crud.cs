using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicSamples.Data
{
    public partial class DataSource
    {
        public async Task<int> InsertSpeakerAsync(Speaker speaker)
        {
            var db = CreateAsyncConnection();

            int result = await db.InsertAsync(speaker);
            return result;
        }

        public async Task<int> UpdateSpeakerAsync(Speaker speaker)
        {
            var db = CreateAsyncConnection();

            int result = await db.UpdateAsync(speaker);
            return result;
        }

        public async Task<int> DeleteSpeakerAsync(Speaker speaker)
        {
            var db = CreateAsyncConnection();

            int result = await db.DeleteAsync(speaker);
            return result;
        }

        public async Task<int> DeleteSpeakerByIdAsync(int speakerId)
        {
            var db = CreateAsyncConnection();

            int result = await db.DeleteAsync(new Speaker() { Id = speakerId });
            return result;
        }

        public async Task<int> DeleteSpeakerByCommandAsync(int speakerId)
        {
            var db = CreateAsyncConnection();

            int result = await db.ExecuteAsync("DELETE FROM Speakers WHERE Id=?", speakerId);
            return result;
        }
    }
}
