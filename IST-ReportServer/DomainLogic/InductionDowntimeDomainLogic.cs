using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Code.Dapper;
using Library.Models;
using Library.Models.ReportModels;

namespace WebservicePortal.DomainLogic
{
    public class InductionDowntimeDomainLogic
    {
        private readonly InductionDowntimeRepository _inductionDowntimeRepository;

        public InductionDowntimeDomainLogic()
        {
            _inductionDowntimeRepository = new InductionDowntimeRepository();
        }

        public InductionDowntimeResponseDto GetInductionDowntimeData(DateTime startdate,
            DateTime enddate, int? lineNumber, int? jobNumber, int? processCode)
        {
            InductionDowntimeResponseDto dto = new InductionDowntimeResponseDto();
            dto.Data = _inductionDowntimeRepository.GetDownTimeEvents(startdate,
                enddate, lineNumber, jobNumber, processCode);
            return dto;

        }

        public List<int> GetDistinctFurnaceLines(DateTime startdate, DateTime enddate)
        {
            return _inductionDowntimeRepository.GetDistinctFurnaceLines(startdate, enddate);
        }

        public InductionDowntimeProcessCodeSummary GetProcessCodeSummary(DateTime startdate, DateTime enddate, int? lineNumber, int? jobNumber, int? processCode)
        {
            InductionDowntimeProcessCodeSummary summary = new InductionDowntimeProcessCodeSummary();
            summary.FurnaceLine = lineNumber ?? 0;
            summary.DowntimeEvents = _inductionDowntimeRepository.GetDownTimeEvents(
                startdate, enddate, lineNumber, jobNumber, processCode).OrderBy(x => x.SmallDateTime).ToList();
            summary.JobNumbersProcessed = summary.DowntimeEvents.Select(x => x.JobNumber).Distinct().ToList();
            summary.JobNumbersProcessed.Sort();

            InductionDownTime first = summary.DowntimeEvents.FirstOrDefault();
            InductionDownTime last = summary.DowntimeEvents.LastOrDefault();

            if (first != null)
            {
                summary.FirstDate = first.SmallDateTime.AddHours(-first.HoursDown);
                summary.FirstDate = ((DateTime)(summary.FirstDate)).AddMinutes(-first.MinutesDown);
            }
            if (last != null) summary.LastDate = last.SmallDateTime;

            if (summary.LastDate != null && summary.FirstDate != null)
            {
                summary.TotalTimeSpan =((DateTime)summary.LastDate).Subtract((DateTime)summary.FirstDate);
            }

            summary.ProcessCodeSummaries = _inductionDowntimeRepository.GetProcessCodeSummary(startdate, enddate, lineNumber, jobNumber, processCode);

            CalculateTimeSpans(summary);

            return summary;

        }

        public void CalculateTimeSpans(InductionDowntimeProcessCodeSummary summary)
        {
            foreach (ProcessCodeSummary s in summary.ProcessCodeSummaries)
            {
                s.TimeSpanDown = new TimeSpan(0, s.HoursDown, s.MinutesDown, 0, 0);
                summary.TotalDownTimeSpan += s.TimeSpanDown;
                if (summary.TotalTimeSpan.TotalMilliseconds > 0)  s.PercentOfTime = (decimal)(s.TimeSpanDown.TotalMilliseconds/ summary.TotalTimeSpan.TotalMilliseconds)*100;
            }

            if (summary.TotalTimeSpan.TotalMilliseconds > 0) summary.TotalPercentDowntime = (decimal)(summary.TotalDownTimeSpan.TotalMilliseconds/summary.TotalTimeSpan.TotalMilliseconds)*100
            ;

        }
    }


}
