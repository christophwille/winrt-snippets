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
        // Run this in SQLiteSpy first to generate the tables
        /*
        CREATE TABLE artist(
          artistid    INTEGER PRIMARY KEY, 
          artistname  TEXT
        );

        CREATE TABLE track(
          trackid     INTEGER PRIMARY KEY, 
          trackname   TEXT, 
          trackartist INTEGER,
          FOREIGN KEY(trackartist) REFERENCES artist(artistid) ON DELETE CASCADE
        );         
        */

        public class artist
        {
            [PrimaryKey]
            public int artistid { get; set; }
            public string artistname { get; set; }
        }

        public class track
        {
            [PrimaryKey]
            public int trackid { get; set; }
            public string trackname { get; set; }
            public int trackartist { get; set; }
        }

        // http://www.sqlite.org/foreignkeys.html#fk_enable 
        // CASCADE is honored *only* if you do this; simple constraint violation will work without this
        public async Task<SQLiteAsyncConnection> GetForeignKeyEnabledConnectionAsync()
        {
            var db = CreateAsyncConnection();
            await db.ExecuteAsync("PRAGMA foreign_keys = ON;");

            return db;
        }

        public async Task FkInsertAsync()
        {
            var db = await GetForeignKeyEnabledConnectionAsync();

            var artist = new artist()
            {
                artistid = 100,
                artistname = "Who Cares"
            };

            await db.InsertAsync(artist);

            var track = new track()
            {
                trackid = 1000,
                trackname = "Song - What Song?",
                trackartist = 100
            };

            await db.InsertAsync(track);

            var trackInViolation = new track()
            {
                trackid = 1000,
                trackname = "Hey, I'll violate the FK",
                trackartist = 1
            };

            try
            {
                await db.InsertAsync(trackInViolation);
            }
            catch (Exception ex)
            {
                var exMessage = ex.Message;
            }

            // Do the cascaded delete - 
            await db.DeleteAsync(artist);
        }
    }
}
