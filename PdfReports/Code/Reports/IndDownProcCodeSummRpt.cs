using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Library.Models.ReportModels;
using PdfReports.Code.ReportsEngine;

namespace PdfReports.Code.Reports
{
    public class IndDownProcCodeSummRpt : ReportBase
    {
        public IndDownProcCodeSummRpt(InductionDowntimeProcessCodeSummary data)
            : base(data)
        {
            ;
        }

        public byte[] BuildPdf()
        {
            Config.Portrait = true;
            Config.HeaderText = $"Induction Downtime Report Furnace #{Config.PcSumm.FurnaceLine}";
            Config.SubheaderText = $"From {Config.PcSumm.RequestStartDate} to {Config.PcSumm.RequestEndDate}";
            Config.ShowLogo = true;
            Config.TopSeparatorLine = true;
            Config.BottomSeparatorLine = true;
            Config.PagingInHeader = false; // does not look good in the header (alignment problem)
            Config.PagingInFooter = true;
            Config.SignatureUsed = PdfReportConfig.ESignatureUsed.None;

            PdfReportBuilder pdfBuilder = new PdfReportBuilder(Config);
            pdfBuilder.ParametersSectionBuilder += OnBuildParametersSection;
            //pdfBuilder.DataSectionBuilder += OnBuildDataSection;

            return pdfBuilder.CreatePdf();
        }

        private void OnBuildParametersSection(Document doc, PdfReportConfig cfg)
        {
            const int cols = 8;
            PdfPTable table = new PdfPTable(cols);
            table.DefaultCell.PaddingBottom = 5;
            table.WidthPercentage = 100;
            table.HeaderRows = 0;

            // columns relative widths
            float[] colWidths = new float[] { 1, 1, 1, 1, 1, 1, 1, 1 };
            table.SetWidths(colWidths);


            Phrase phrase = new Phrase { new Chunk("SUMMARY", cfg.TableHeaderFont) };
            PdfPCell cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 8;
            cell.GrayFill = (float)0.95;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk("First Event: ", cfg.TableCellFont) };
            //phrase.Add(new Chunk(cfg.PcSumm.FirstDate.ToString(), cfg.TableCellFont));
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk(cfg.PcSumm.FirstDate.ToString(), cfg.TableCellFont) };
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk("Last Event: ", cfg.TableCellFont) };
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk(cfg.PcSumm.LastDate.ToString(), cfg.TableCellFont) };
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk("Total Time Analyzed: ", cfg.TableCellFont) };
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk(cfg.PcSumm.TotalTimeSpanString, cfg.TableCellFont) };
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk("Total Downtime Logged:", cfg.TableCellFont) };
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk(cfg.PcSumm.TotalDownTimeSpanString, cfg.TableCellFont) };
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk("Down Time Percent:", cfg.TableCellFont) };
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk($"{cfg.PcSumm.TotalPercentDowntime.ToString("#0.0")}%", cfg.TableCellFont) };
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 6;
            table.AddCell(cell);

            string jobnums = "";
            for (int i = 0; i < cfg.PcSumm.JobNumbersProcessed.Count; i++)
            {
                if (i == 0)
                {
                    jobnums += $"{cfg.PcSumm.JobNumbersProcessed[i]}";
                }
                else
                {
                    jobnums += $",{cfg.PcSumm.JobNumbersProcessed[i]}";
                }
            }

            phrase = new Phrase { new Chunk("Job Numbers: ", cfg.TableCellFont) };
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk(jobnums, cfg.TableCellFont) };
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 6;
            table.AddCell(cell);

            doc.Add(table);
            doc.Add(new Phrase("\n"));

            foreach (JobCountSummary jc in cfg.PcSumm.JobCountSummaries)
            {
                //table = ReportShared.DefineStandardTable(cfg);
                table = new PdfPTable(cols);
                table.DefaultCell.PaddingBottom = 5;
                table.WidthPercentage = 100;
                table.HeaderRows = 0;
                table.SpacingAfter = 10;

                phrase = new Phrase { new Chunk($"{jc.JobNumber} JOB SUMMARY", cfg.TableHeaderFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 8;
                cell.GrayFill = (float)0.95;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk("First Event Date:", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 2;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk($"{jc.FirstEventDate}", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 2;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk("Last Event Date:", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 2;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk($"{jc.LastEventDate}", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 2;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk("Total Job Time:", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 2;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk($"{jc.TotalTimeSpanString}", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 2;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk("Total Down Time:", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 2;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk($"{jc.TotalDownTimeSpanString}", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 2;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk() };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 4;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk("Down Time Percent:", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 2;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk($"{jc.TotalPercentDowntime.ToString("0.0")}%", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 2;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk("Total Part Count:", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 2;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk($"{jc.TotalPartCount}", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 2;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk("Avg Parts/Min:", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 2;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk($"{jc.AvgPartsPerMinute.ToString("####0")}", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 2;
                table.AddCell(cell);
                doc.Add(table);
            }
            //doc.Add(new Phrase("\n"));
            foreach (ProcessCodeSummary sum in cfg.PcSumm.ProcessCodeSummaries)
            {
                //table = ReportShared.DefineStandardTable(cfg);
                table = new PdfPTable(cols);
                table.DefaultCell.PaddingBottom = 5;
                table.WidthPercentage = 100;
                table.HeaderRows = 0;

                phrase = new Phrase { new Chunk($"PROCESS CODE #{sum.ProcessCode} - {sum.ProcessCodeName}", cfg.TableHeaderFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 8;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk("Time Down:", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 1;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk($"{sum.TimeSpanDownString}", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 7;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk("% of Tot Time:", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 1;
                table.AddCell(cell);

                phrase = new Phrase { new Chunk($"{sum.PercentOfTime.ToString("#0.0")}%", cfg.TableCellFont) };
                cell = new PdfPCell(phrase);
                cell.Border = 0;
                cell.Colspan = 7;
                table.AddCell(cell);

                table.AddCell(new PdfPCell());

                doc.Add(table);
                //doc.Add(new Phrase("\n"));
            }

        }

        private void OnBuildDataSection(Document doc, PdfReportConfig cfg)
        {
            //foreach (ProcessCodeSummary ps in cfg.PcSumm.ProcessCodeSummaries)
            //{
            //          doc.Add(new Phrase("\n"));
            //          PdfPTable table = ReportShared.CreateProcCodeStats(cfg,ps);
            //          doc.Add(table);
            //    doc.NewPage();
            //}
        }

    }


}
