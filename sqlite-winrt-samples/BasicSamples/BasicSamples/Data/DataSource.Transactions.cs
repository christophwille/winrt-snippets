using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Transcoding;

namespace BasicSamples.Data
{
    // 
    // NOTE: I commented the OBSOLETED RunInTransactionAsync method because I definitely don't need it
    //
    public partial class DataSource
    {
        public async Task InsertSpeakerGraphAsync(List<Speaker> speakers)
        {
            var db = CreateAsyncConnection();

            await db.RunInTransactionAsync(tx =>
            {
                tx.InsertAll(speakers);

                var tempSessionList = new List<Session>();

                foreach (var speaker in speakers)
                {
                    if (null != speaker.Sessions)
                    {
                        foreach (var session in speaker.Sessions)
                        {
                            session.SpeakerId = speaker.Id;
                            tempSessionList.Add(session);
                        }
                    }
                }

                tx.InsertAll(tempSessionList);
            });
        }

        public async Task DeleteSpeakerAsync(int speakerId, bool forceTxFail)
        {
            var db = CreateAsyncConnection();

            await db.RunInTransactionAsync(tx =>
            {
                tx.Execute("DELETE FROM Session WHERE SpeakerId=?", speakerId);

                if (forceTxFail)
                    throw new InvalidOperationException();

                tx.Execute("DELETE FROM Speakers WHERE Id=?", speakerId);
            });
        }
    }
}
