using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpotifyAPI.Web.Enums;
using SetBackground.MusicAPI;

namespace SetBackground
{
    class Program
    {
        static TimeSpan startTime = TimeSpan.Zero;
        static TimeSpan interval = TimeSpan.FromSeconds(15);


        static void Main(string[] args)
        {
            var spotify = new SpotifyWeb("http://localhost", 8000, "1ac6c5dcd98548c088f861b3908260d1", Scope.UserReadPlaybackState);
            
            var timer = new Timer((e) =>
            {
                Console.WriteLine("start");

                var s = spotify.GetCurrentSong();


            }, null, startTime, interval);

            Console.ReadLine();
        }
    }
}
