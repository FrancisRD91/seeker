using System;

namespace seeker.Helpers
{
    public class IndexResponse
    {
        private bool success;
        private int? indexes;
        private int? words;
        public bool Success {  get { return success;} set { success = value;} }
        public int? Indexes { get { return indexes;} set { indexes = value;} }
        public int? Words { get { return words;} set { words = value;} }
        
    }
}
