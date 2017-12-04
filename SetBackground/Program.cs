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

namespace SetBackground
{
    class Program
    {
        static TimeSpan startTime = TimeSpan.Zero;
        static TimeSpan interval = TimeSpan.FromSeconds(20);


        static void Main(string[] args)
        {
            var configs = ConfigurationManager.GetSection("APIs/*");
            //var configs = ConfigurationManager.GetSection("APIs/LanguageAPI");

            var lastSong = string.Empty;
            var spotify = new SpotifyWeb("http://localhost", 8000, "477f1b8f37194360b9744d0d087a1d1b", Scope.UserReadPlaybackState);
            var musicMatch = new MusicXMatchAPI("7304f2f18acb12a2f22f3338c60f3a9f");
            var msText = new MicrosoftTextAnalytics();

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
