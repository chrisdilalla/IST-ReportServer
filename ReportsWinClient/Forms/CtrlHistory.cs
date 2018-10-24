using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Library.Models;
using Library.Models.ReportModels;
using Library.Utils;
using ReportsWinClient.DataAccess;

namespace ReportsWinClient.Forms
{
    public partial class CtrlHistory : CtrlSubScreenBase
    {
        public CtrlHistory()
        {
            InitializeComponent();
        }

        private void CtrlHistory_Load(object sender, EventArgs e)
        {
            ScreenTitle = "Production History";
            ConfigureGrids();
            SetupDefaultParameters();
            GetFurnaceLines();
        }

        private void GetFurnaceLines()
        {
            comboLineNumber.Items.Clear();
            List<int> furnaces = DataAccessLayer.ReadFurnaceLines();
            foreach (int furnace in furnaces)
            {
                comboLineNumber.Items.Add(furnace);
            }
            comboLineNumber.SelectedIndex = 0;
        }

        private void ConfigureGrids()
        {
            //disable highlighting
            datagridviewStopEvents.DefaultCellStyle.SelectionBackColor = datagridviewStopEvents.DefaultCellStyle.BackColor;
            datagridviewStopEvents.DefaultCellStyle.SelectionForeColor = datagridviewStopEvents.DefaultCellStyle.ForeColor;


            //Change cell font
            foreach (DataGridViewColumn c in datagridviewStopEvents.Columns)
            {
                c.HeaderCell.Style.Font = new Font("Arial", 12.0F, FontStyle.Bold);
                c.DefaultCellStyle.Font = new Font("Arial", 10.0F, FontStyle.Regular);
            }

            var colReport1 = datagridviewStopEvents.Columns["GetReport"];
            if (colReport1 != null)
            {
                colReport1.DefaultCellStyle = new DataGridViewCellStyle { Padding = new Padding(2) };
                colReport1.Width = 100;
                colReport1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }

        private void SetupDefaultParameters()
        {
            dtTo.Value = DateTime.Now;
            dtFrom.Value = DateTime.Now.AddDays(-1);

        }

        private void btnRefineSearch_Click(object sender, EventArgs e)
        {
            int? jobnumber = StrUtils.CvtStrToInt(txtJobNumber.Text, 0);
            if (jobnumber == 0) jobnumber = null;

            int? processcode = StrUtils.CvtStrToInt(txtProcessCode.Text, 0);
            if (processcode == 0) processcode = null;

            InductionDowntimeRequestObject req = new InductionDowntimeRequestObject
            {
                StartDate = dtFrom.Value.Date,
                EndDate = dtTo.Value.Date.AddDays(1),
                LineNumber = StrUtils.CvtStrToInt(comboLineNumber.Text, 0),
                JobNumber = jobnumber,
                ProcessCode = processcode
            };

            InductionDowntimeProcessCodeSummary summary = DataAccessLayer.ReadDowntimeEvents(req);
            PopulateEventsGrid(summary.DowntimeEvents);

            SaveAndOpenReports(summary);


            ////store events list locally
            //PopulateEventsGrid(resp.Orders);

            ////get loads and store them locally

            //List<long> orderpkeys = resp.Orders.Select(o => o.Pkey).ToList();

            //if (orderpkeys.Count > 0)
            //{
            //    LoadsReadRequest lreq = new LoadsReadRequest { OrderPkeys = orderpkeys, ReturnImages = true, ReturnImagesAsThumbnails = true, ReturnStatistics = false };
            //    LoadsReadResponse lresp = DAL.ReadLoads(lreq);
            //    PopulateLoadsGrid(lresp.Loads);
            //}


        }

        private void PopulateEventsGrid(List<InductionDownTime> events)
        {
            datagridviewStopEvents.Rows.Clear();
            foreach (InductionDownTime o in events.OrderBy(a => a.SmallDateTime))
            {
                int rowindex = datagridviewStopEvents.Rows.Add();
                var row = datagridviewStopEvents.Rows[rowindex];
                row.Tag = o.SmallDateTime;

                row.Cells["SmallDateTime"].Value = o.SmallDateTime;
                row.Cells["ProcessCode"].Value = o.ProcessCode;
                row.Cells["HoursDown"].Value = o.HoursDown;
                row.Cells["MinutesDown"].Value = o.MinutesDown;
                row.Cells["LineNumber"].Value = o.LineNumber;
                row.Cells["JobNumber"].Value = o.JobNumber;
                row.Cells["PartCount"].Value = o.PartCount;

                row.Cells["GetReport"].Value = "Get Report";
            }
            datagridviewStopEvents.ClearSelection();
        }

        private void datagridviewOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                Cursor = Cursors.WaitCursor;
                DataGridViewRow selectedrow = datagridviewStopEvents.Rows[e.RowIndex];
                //long pkey = StrUtils.CvtStrToLong(selectedrow.Tag.ToString(), 0);

                //ReportsReadRequest req = new ReportsReadRequest();
                //ReportRequest req1 = new ReportRequest
                //{
                //    ReturnOrderSummaryLoads = true,
                //    ReportType = EReportType.OrderSummary,
                //    ObjectPkey = pkey
                //};
                //req.ReportRequests.Add(req1);

                //ReportRequest req2 = new ReportRequest()
                //{
                //    ReportType = EReportType.UtilizationReport,
                //    UtilizationReportRequest = new UtilizationReportRequest
                //    {
                //        OrderPkey = pkey
                //    }
                //};
                //req.ReportRequests.Add(req2);

                //ReportsReadResponse resp = DAL.ReadReports(req);

                //SaveAndOpenReports(resp);
                Cursor = Cursors.Default;
            }
        }

        private void SaveAndOpenReports(InductionDowntimeProcessCodeSummary summ)
        {
            CleanTemporaryDirectory();

            string directory = Properties.Settings.Default.TempDirectory;
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            string path = Path.Combine(directory, string.Format("{0}.pdf", $"ReportName_{DateTime.Now.ToFileTime()}")); 

            File.WriteAllBytes(path, summ.ReportBytes);
            Process.Start(path);
            Cursor = Cursors.Default;
        }

        private void CleanTemporaryDirectory()
        {
            string tempdirectory = Properties.Settings.Default.TempDirectory;

            if (!Directory.Exists(tempdirectory)) return;

            string[] filePaths = Directory.GetFiles(tempdirectory);
            foreach (string filePath in filePaths)
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    //intentionally left blank to skip any open files
                }
            }

        }

        private void dt_ValueChanged(object sender, EventArgs e)
        {
            if (dtFrom.Value.Date > dtTo.Value.Date)
            {
                btnRefineSearch.Enabled = false;
                return;
            }

            if (dtTo.Value.Date.Subtract(dtFrom.Value.Date).TotalDays > 365)
            {
                btnRefineSearch.Enabled = false;
                return;
            }

            btnRefineSearch.Enabled = true;
        }

        private DateTime NormalizeDateForIstShifts(DateTime date, bool isfromdate)
        {
            DateTime retvalue = date.Date.AddDays(isfromdate ? -1 : 0);
            retvalue = retvalue.AddHours(23);
            return retvalue.ToUniversalTime();
        }


    }
}
