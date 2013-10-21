using System;
using System.Collections.Generic;

namespace MomentOfZenGenerator.Wordnik
{
    public class WordnikFrequencyResponse
    {
        public Int32 UnknownYearCount { get; set; }
        public Int64 TotalCount { get; set; }
        public String FrequencyString { get; set; }
        public String Word { get; set; }
        public IEnumerable<Frequency> Frequency { get; set; }
    }

    public class Frequency
    {
        public Int64 Count { get; set; }
        public Int32 Year { get; set; }
    }
}