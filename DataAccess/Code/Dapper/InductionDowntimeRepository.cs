using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Library.Models;
using Library.Models.ReportModels;

namespace DataAccess.Code.Dapper
{
    public interface IInductionDowntimeRepository
    {
        IList<InductionDownTime> GetDownTimeEvents(DateTime startdate, DateTime enddate,
            int? lineNumber, int? jobNumber, int? processCode);
    }

    public class InductionDowntimeRepository : BaseDatabaseAccess, IInductionDowntimeRepository
    {
        public IList<InductionDownTime> GetDownTimeEvents(DateTime startdate, DateTime enddate,
            int? lineNumber, int? jobNumber, int? processCode)
        {
            string query = "SELECT * FROM InductionDowntime WHERE " +
                                 "[SmallDateTime] >= @StartDate AND [SmallDateTime] <= @EndDate ";

            if (lineNumber != null && lineNumber > 0) query += " AND LineNumber = @LineNumber";
            if (jobNumber != null && jobNumber > 0) query += " AND JobNumber = @JobNumber";
            if (processCode != null) query += " AND ProcessCode = @ProcessCode";

            var parameters = new
            {
                StartDate = startdate,
                EndDate = enddate,
                LineNumber = lineNumber,
                JobNumber = jobNumber,
                ProcessCode = processCode
            };

            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                IList<InductionDownTime> entries = sqlConnection.Query<InductionDownTime>(query, parameters).AsList();
                return entries;
            }
        }

        public List<int> GetDistinctFurnaceLines(DateTime startdate, DateTime enddate)
        {
            string query = "SELECT DISTINCT LineNumber FROM InductionDowntime WHERE LineNumber is not null AND " +
                     "[SmallDateTime] >= @StartDate AND [SmallDateTime] <= @EndDate ORDER BY LineNumber ";


            var parameters = new
            {
                StartDate = startdate,
                EndDate = enddate,
            };

            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                List<int> entries = sqlConnection.Query<int>(query, parameters).AsList();
                return entries;
            }
        }


        public List<ProcessCodeSummary> GetProcessCodeSummary(DateTime startdate, DateTime enddate, int? lineNumber, int? jobNumber, int? processCode)
        {
            string query = "SELECT ProcessCode, " +
                           "(Select [Description] from ProcessCodes Where Code = ProcessCode) as ProcessCodeName," +
                           "SUM([HoursDown]) as 'HoursDown', " +
                           "SUM([MinutesDown]) as 'MinutesDown' " +
                           "FROM InductionDowntime WHERE " +
                           "[SmallDateTime] >= @StartDate AND [SmallDateTime] <= @EndDate ";

            if (lineNumber != null && lineNumber > 0) query += "AND LineNumber = @LineNumber ";
            if (jobNumber != null && jobNumber > 0) query += "AND JobNumber = @JobNumber ";
            if (processCode != null) query += "AND ProcessCode = @ProcessCode ";

            query += "GROUP BY ProcessCode ORDER BY ProcessCode ASC";

            var parameters = new
            {
                StartDate = startdate,
                EndDate = enddate,
                LineNumber = lineNumber,
                JobNumber = jobNumber,
                ProcessCode = processCode
            };

            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                List<ProcessCodeSummary> entries = sqlConnection.Query<ProcessCodeSummary>(query, parameters).AsList();
                return entries;
            }
        }




    }
}
