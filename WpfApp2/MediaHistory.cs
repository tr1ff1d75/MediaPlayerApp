using System;

namespace MediaPlayerApp
{
    public class MediaHistory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public DateTime Date { get; set; }
        public bool Favorite { get; set; }
        public string Path { get; set; }
    }
}