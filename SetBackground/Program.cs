using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpotifyAPI.Web.Enums;
using SetBackground.MusicAPI;
using SetBackground.LyricsAPI;
using SetBackground.LanguageAPI;
using System.Configuration;
using System.Diagnostics;
using System.Collections.Specialized;

namespace SetBackground
{
    class Program
    {
        static TimeSpan startTime = TimeSpan.Zero;
        static TimeSpan interval = TimeSpan.FromSeconds(20);


        static void Main(string[] args)
        {
            var languageConfig = ConfigurationManager.GetSection("APIs/LanguageAPI") as NameValueCollection;
            var MSAnalyticsKey = languageConfig["MSTextAnalyticsK1"];

            var lyricsConfig = ConfigurationManager.GetSection("APIs/LyricsAPI") as NameValueCollection;
            var musicXMatchKey = lyricsConfig["MusicXMatchKey"];

            var musicConfig = ConfigurationManager.GetSection("APIs/MusicAPI") as NameValueCollection;
            var spotifyKey = musicConfig["SpotifyKey"];
            var spotifyRedirectUrl = musicConfig["SpotifyRedirectUrl"];
            var spotifRedirectPort = int.Parse(musicConfig["SpotifyListeningPort"]);

            var lastSong = string.Empty;
            var spotify = new SpotifyWeb(spotifyRedirectUrl, spotifRedirectPort, spotifyKey, Scope.UserReadPlaybackState);
            var musicMatch = new MusicXMatchAPI(musicXMatchKey);
            var msText = new MicrosoftTextAnalytics(MSAnalyticsKey);

            var timer = new Timer((e) =>
            {
                Console.WriteLine("==========================S T A R T==========================");
                var song = spotify.GetCurrentSong();
                if(song != null && song.Title != null)
                {
                    if(lastSong != song.Title)
                    {
                        lastSong = song.Title;
                        Console.WriteLine($"{song.Artist} - {song.Title}: ");
                        var lyrics = musicMatch.GetLyricsAndLanguage(lastSong, song.Artist);
                        if(string.IsNullOrEmpty(lyrics.Item1))
                        {
                            Console.WriteLine("*2nd Call to lyrics Service");
                            lyrics = musicMatch.GetLyricsAndLanguage(lastSong, null);
                        }
                        Console.WriteLine(lyrics);
                        var songLanguage = msText.GetLanguage(lyrics.Item1);
                        var songKeys = msText.ExtractKeyPhrases(lyrics.Item1, songLanguage);
                        Console.WriteLine(string.Join(Environment.NewLine, songKeys));
                    }
                    
                }
                else
                    Console.WriteLine("no song");
            }, null, startTime, interval);

            Console.ReadLine();
        }
    }
}
