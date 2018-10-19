using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class InductionDowntimeRequestObject
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? LineNumber { get; set; }
        public int? JobNumber { get; set; }
        public int? ProcessCode { get; set; }
    }

    //InductionDowntimeRequestObject obj = new InductionDowntimeRequestObject
    //{
    //    StartDate = startdate,
    //    EndDate = enddate,
    //    LineNumber = lineNumber,
    //    JobNumber = jobNumber,
    //    ProcessCode = processCode
    //};


}
