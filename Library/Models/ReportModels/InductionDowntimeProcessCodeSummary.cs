using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public DateTime RequestStartDate { get; set; }
        public DateTime RequestEndDate { get; set; }
        public int FurnaceLine { get; set; }
        public DateTime? FirstDate { get; set; }
        public DateTime? LastDate { get; set; }
        public TimeSpan TotalTimeSpan { get; set; }
        public TimeSpan TotalDownTimeSpan { get; set; }
        public string TotalTimeSpanString { get; set; }
        public string TotalDownTimeSpanString { get; set; }
        public double TotalPercentDowntime { get; set; }
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
                if (TotalTimeSpan.TotalMilliseconds > 0) s.PercentOfTime = (s.TimeSpanDown.TotalMilliseconds / TotalTimeSpan.TotalMilliseconds) * 100;
                s.TimeSpanDownString = DateTimeUtils.ConvertTimeSpanToString(s.TimeSpanDown);
            }

            if (TotalTimeSpan.TotalMilliseconds > 0)
                TotalPercentDowntime =(TotalDownTimeSpan.TotalMilliseconds / TotalTimeSpan.TotalMilliseconds) * 100;

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
            try
            {
                foreach (int j in JobNumbersProcessed)
                {
                    JobCountSummary jcs = new JobCountSummary { JobNumber = j };
                    List<InductionDownTime> jobevents = DowntimeEvents.Where(a => a.JobNumber == j).OrderBy(a => a.SmallDateTime).ToList();
                    var firstRecord = jobevents.FirstOrDefault();
                    var lastRecord = jobevents.LastOrDefault();
                    if (firstRecord != null)
                        jcs.FirstEventDate = firstRecord.SmallDateTime.AddHours(-firstRecord.HoursDown).AddMinutes(-firstRecord.MinutesDown);
                    if (lastRecord != null)
                        jcs.LastEventDate = lastRecord.SmallDateTime;
                    if (firstRecord != null && lastRecord != null)
                    {
                        jcs.TotalPartCount = lastRecord.PartCount - firstRecord.PartCount;
                        jcs.TotalTimeSpan = lastRecord.SmallDateTime - firstRecord.SmallDateTime;
                    }
                    jcs.TotalTimeSpanString = DateTimeUtils.ConvertTimeSpanToString(jcs.TotalTimeSpan);

                    foreach (InductionDownTime e in jobevents)
                        jcs.TotalDownTimeSpan += new TimeSpan(0, e.HoursDown, e.MinutesDown, 0, 0);

                    if (jcs.TotalDownTimeSpan.TotalMinutes > 0)
                    {
                        jcs.AvgPartsPerMinute = jcs.TotalPartCount / jcs.TotalTimeSpan.TotalMinutes;
                        jcs.TotalPercentDowntime = jcs.TotalDownTimeSpan.TotalMinutes / jcs.TotalTimeSpan.TotalMinutes * 100;
                    }

                    jcs.TotalDownTimeSpanString = DateTimeUtils.ConvertTimeSpanToString(jcs.TotalDownTimeSpan);
                    JobCountSummaries.Add(jcs);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            JobCountSummaries = JobCountSummaries.OrderBy(a => a.FirstEventDate).ToList();
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
        public double PercentOfTime { get; set; }
    }

    public class JobCountSummary
    {
        public int JobNumber { get; set; }
        public DateTime FirstEventDate { get; set; }
        public DateTime LastEventDate { get; set; }
        public TimeSpan TotalTimeSpan { get; set; }
        public string   TotalTimeSpanString { get; set; }
        public long TotalPartCount { get; set; }
        public double AvgPartsPerMinute { get; set; }
        public TimeSpan TotalDownTimeSpan{ get; set; }
        public string TotalDownTimeSpanString { get; set; }
        public double TotalPercentDowntime { get; set; }
    }
}
