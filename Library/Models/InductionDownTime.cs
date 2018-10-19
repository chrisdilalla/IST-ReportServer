using System;

namespace Library.Models
{
    public class InductionDownTime
    {
        public DateTime SmallDateTime { get; set; }
        public int ProcessCode { get; set; }
        public int HoursDown { get; set; }
        public int MinutesDown { get; set; }
        public int LineNumber { get; set; }
        public int JobNumber { get; set; }
        public string HasBeenRead { get; set; }
        public long PartCount { get; set; }
    }
}
