using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.ReportModels
{
    public class InductionDowntimeProcessCodeSummary
    {
        public InductionDowntimeProcessCodeSummary()
        {
            ProcessCodeSummaries = new List<ProcessCodeSummary>();
            DowntimeEvents = new List<InductionDownTime>();
        }

        public int FurnaceLine { get; set; }
        public DateTime? FirstDate { get; set; }
        public DateTime? LastDate { get; set; }
        public TimeSpan TotalTimeSpan { get; set; }
        public TimeSpan TotalDownTimeSpan { get; set; }
        public decimal TotalPercentDowntime { get; set; }
        public List<int> JobNumbersProcessed { get; set; }
        public List<ProcessCodeSummary> ProcessCodeSummaries { get; set; }
        public List<InductionDownTime> DowntimeEvents { get; set; }
        public byte[] ReportBytes { get; set; }
    }

    public class ProcessCodeSummary
    {
        public int ProcessCode { get; set; }
        public string ProcessCodeName { get; set; }
        public int HoursDown { get; set; }
        public int MinutesDown { get; set; }
        public TimeSpan TimeSpanDown { get; set; }
        public decimal PercentOfTime { get; set; }
    }
}
