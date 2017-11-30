using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpotifyAPI.Web.Enums;
using SetBackground.MusicAPI;
using SetBackground.LyricsAPI;

namespace SetBackground
{
    class Program
    {
        static TimeSpan startTime = TimeSpan.Zero;
        static TimeSpan interval = TimeSpan.FromSeconds(20);


        static void Main(string[] args)
        {
            var lastSong = string.Empty;
            var spotify = new SpotifyWeb("http://localhost", 8000, "477f1b8f37194360b9744d0d087a1d1b", Scope.UserReadPlaybackState);
            var musicMatch = new MusicXMatchAPI("7304f2f18acb12a2f22f3338c60f3a9f");

            Console.WriteLine("start");
            var timer = new Timer((e) =>
            {
                var song = spotify.GetCurrentSong();
                if(song != null && song.Title != null)
                {
                    if(lastSong != song.Title)
                    {
                        lastSong = song.Title;
                        Console.Write($"{song.Title}: ");
                        var lyrics = musicMatch.GetLyrics(lastSong, song.Artist);
                        Console.WriteLine(lyrics);
                    }
                    
                }
                else
                    Console.WriteLine("no song");
            }, null, startTime, interval);
            
            

            Console.ReadLine();
        }
    }
}
