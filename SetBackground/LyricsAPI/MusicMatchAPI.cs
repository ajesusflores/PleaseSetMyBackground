using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetBackground.LyricsAPI
{
    class MusicMatchAPI : ILyricsProvider
    {
        public string GetLyrics(string songName)
        {
            return "El murcielago come feliz kiwi.";
        }

        public string GetLyrics(string songName, string artistName)
        {
            return GetLyrics(songName);
        }

        public string GetLyrics(string songName, string artistName, string albumName)
        {
            return GetLyrics(songName, artistName);
        }
    }
}
