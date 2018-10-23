using System;
using Library.Models.ReportModels;
using PdfReports.Code.ReportsEngine;

namespace PdfReports.Code.Reports
{
	public class ReportBase
	{
		public readonly PdfReportConfig Config;

		protected ReportBase(InductionDowntimeProcessCodeSummary pcodesumm)
		{
			Config = new PdfReportConfig();
			Config.ReportCreationDateTime = DateTime.Now;
			Config.PcSumm = pcodesumm;
		    if (pcodesumm.FirstDate != null)
		        Config.StartDate = (DateTime) pcodesumm.FirstDate;
		    if (pcodesumm.LastDate != null)
		        Config.EndDate = (DateTime) pcodesumm.LastDate;
		}



	}
}
