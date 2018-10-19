using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Library.Models;
using Library.Models.ReportModels;
using Library.Utils;
using WebservicePortal.DomainLogic;

namespace WebservicePortal.Controllers
{
    
    public class InductionDowntimeController : ApiController
    {
        private readonly InductionDowntimeDomainLogic _inductiondowntimedomainlogic;


        public InductionDowntimeController()
        {
            _inductiondowntimedomainlogic = new InductionDowntimeDomainLogic();
        }

        //GET: api/v1/inductiondowntime/
        [Route("InductionDowntime")]
        public IHttpActionResult Get(DateTime startdate, DateTime enddate, int? lineNumber = null, int? jobNumber = null,
            int? processCode = null)
        {
            InductionDowntimeResponseDto dto = _inductiondowntimedomainlogic.GetInductionDowntimeData(
                startdate, enddate, lineNumber, jobNumber, processCode);

            InductionDowntimeProcessCodeSummary summary = _inductiondowntimedomainlogic.GetProcessCodeSummary(startdate, enddate,
                lineNumber, jobNumber, processCode);
            
            return Ok(summary);
        }


        //GET: api/v1/inductiondowntime/distinctfurnacelines/
        [Route("inductiondowntime/distinctfurnacelines")]
        public IHttpActionResult Get(DateTime startdate, DateTime enddate)
        {
            List<int> furnaceLines = _inductiondowntimedomainlogic.GetDistinctFurnaceLines(
                startdate, enddate);
            return Ok(furnaceLines);
        }

    }
}
