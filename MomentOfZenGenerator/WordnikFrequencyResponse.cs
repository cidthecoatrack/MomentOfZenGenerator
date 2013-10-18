using System;

namespace MomentOfZenGenerator
{
    public class WordnikFrequencyResponse
    {
        public String Word { get; set; }
        public Int32 UnknownYearCount { get; set; }
        public String Frequency { get; set; }
        public Int64 TotalCount { get; set; }
    }
}