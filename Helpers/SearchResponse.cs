using System;

namespace seeker.Helpers
{
    public class SearchResponse
    {
        private string title;
        private string url;
        private int occurrences;
        public string Title { get => title; set => title = value; }
        public string Url { get => url; set => url = value; }
        public int Occurrences { get => occurrences; set => occurrences = value; }
    }
}
