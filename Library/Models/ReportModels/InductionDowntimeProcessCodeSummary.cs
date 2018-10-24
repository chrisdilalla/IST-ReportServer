using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDLibrary.Utils;

namespace Library.Models.ReportModels
{
    public class InductionDowntimeProcessCodeSummary
    {
        public InductionDowntimeProcessCodeSummary()
        {
            ProcessCodeSummaries = new List<ProcessCodeSummary>();
            JobCountSummaries = new List<JobCountSummary>();
            DowntimeEvents = new List<InductionDownTime>();
        }

        public int FurnaceLine { get; set; }
        public DateTime? FirstDate { get; set; }
        public DateTime? LastDate { get; set; }
        public TimeSpan TotalTimeSpan { get; set; }
        public TimeSpan TotalDownTimeSpan { get; set; }
        public string TotalTimeSpanString { get; set; }
        public string TotalDownTimeSpanString { get; set; }
        public decimal TotalPercentDowntime { get; set; }
        public List<int> JobNumbersProcessed { get; set; }
        public List<ProcessCodeSummary> ProcessCodeSummaries { get; set; }
        public List<JobCountSummary> JobCountSummaries { get; set; }
        public List<InductionDownTime> DowntimeEvents { get; set; }
        public byte[] ReportBytes { get; set; }



        /// <summary>
        /// Takes the OrderSummary Report as retrieved from the database and populates
        /// Timespan objects with h/s records as retrieved.  It also converts the timespan
        /// To a readable string for reporting purposes in format 0d 0h 00mins
        /// </summary>
        public void CalculateTimeSpans()
        {
            foreach (ProcessCodeSummary s in ProcessCodeSummaries)
            {
                s.TimeSpanDown = new TimeSpan(0, s.HoursDown, s.MinutesDown, 0, 0);
                TotalDownTimeSpan += s.TimeSpanDown;
                if (TotalTimeSpan.TotalMilliseconds > 0) s.PercentOfTime = (decimal)(s.TimeSpanDown.TotalMilliseconds / TotalTimeSpan.TotalMilliseconds) * 100;
                s.TimeSpanDownString = DateTimeUtils.ConvertTimeSpanToString(s.TimeSpanDown);
            }

            if (TotalTimeSpan.TotalMilliseconds > 0)
                TotalPercentDowntime =
                    (decimal)(TotalDownTimeSpan.TotalMilliseconds / TotalTimeSpan.TotalMilliseconds) * 100;

            TotalDownTimeSpanString = DateTimeUtils.ConvertTimeSpanToString(TotalDownTimeSpan);
            TotalTimeSpanString = DateTimeUtils.ConvertTimeSpanToString(TotalTimeSpan);
        }


        /// <summary>
        /// Takes the OrderSummary Report as retrieved from the database and works out the 
        /// total counts by job number and populates the JobCountSummary List data
        /// </summary>
        /// <param name="summary"></param>
        public void CalculateOrderCounts()
        {
            DowntimeEvents.OrderBy(a => a.JobNumber).ThenBy(a => a.SmallDateTime);

            foreach (int j in JobNumbersProcessed)
            {
                JobCountSummary jcs = new JobCountSummary { JobNumber = j };
                List<InductionDownTime> jobevents = DowntimeEvents.Where(a => a.JobNumber == j).OrderBy(a => a.SmallDateTime).ToList();
                jcs.FirstRecord = jobevents.FirstOrDefault();
                jcs.LastRecord = jobevents.LastOrDefault();
                if (jcs.FirstRecord != null && jcs.LastRecord != null) jcs.TotalCount = jcs.LastRecord.PartCount - jcs.FirstRecord.PartCount;
                JobCountSummaries.Add(jcs);
            }


        }
    }

    public class ProcessCodeSummary
    {
        public int ProcessCode { get; set; }
        public string ProcessCodeName { get; set; }
        public int HoursDown { get; set; }
        public int MinutesDown { get; set; }
        public TimeSpan TimeSpanDown { get; set; }
        public string TimeSpanDownString { get; set; }
        public decimal PercentOfTime { get; set; }
    }

    public class JobCountSummary
    {
        public int JobNumber { get; set; }
        public InductionDownTime FirstRecord { get; set; }
        public InductionDownTime LastRecord { get; set; }
        public long TotalCount { get; set; }
    }
}
