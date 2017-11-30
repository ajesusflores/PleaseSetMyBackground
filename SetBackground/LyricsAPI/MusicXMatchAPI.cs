using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetBackground.LyricsAPI
{
    class MusicXMatchAPI : ILyricsProvider
    {
        LyricsApi _api;

        public MusicXMatchAPI(string apiKey)
        {
            //Configuration.Default.ApiKey.Add("apikey", "7304f2f18acb12a2f22f3338c60f3a9f");
            Configuration.Default.ApiKey.Add("apikey", apiKey);
            _api = new LyricsApi();
        }

        public string GetLyrics(string songName)
        {
            return GetLyrics(songName, null);
        }

        public string GetLyrics(string songName, string artistName)
        {
            return ParseResponse(_api.MatcherLyricsGetGet(null, null, songName, artistName));
        }

        public string GetLyrics(string songName, string artistName, string albumName)
        {
            return GetLyrics(songName, artistName);
        }

        private string ParseResponse(InlineResponse2007 response)
        {
            if (response == null || response.Message == null || response.Message.Header == null || response.Message.Header.StatusCode != 200)
                return string.Empty;

            return response.Message.Body.Lyrics.LyricsBody;
        }
    }
}

