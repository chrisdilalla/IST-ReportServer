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
			Config.HeaderText = "Production Downtime Report";
		    Config.SubheaderText = $"From {Config.PcSumm.FirstDate} to {Config.PcSumm.LastDate}";
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


		    Phrase phrase = new Phrase();// { new Chunk("SUMMARY", cfg.TableHeaderFont) };
            PdfPCell cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 8;
            cell.GrayFill = (float)0.95;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk("FIRST EVENT: ", cfg.TableHeaderFont) };
			phrase.Add(new Chunk(cfg.PcSumm.FirstDate.ToString(), cfg.TableCellFont));
			cell = new PdfPCell(phrase);
			cell.Border = 0;
			cell.Colspan = 8;
			table.AddCell(cell);

			phrase = new Phrase { new Chunk("LAST EVENT: ", cfg.TableHeaderFont) };
            phrase.Add(new Chunk(cfg.PcSumm.LastDate.ToString(), cfg.TableCellFont));
            cell = new PdfPCell(phrase);
			cell.Border = 0;
			cell.Colspan = 8;
			table.AddCell(cell);

            phrase = new Phrase { new Chunk("TOTAL TIME ANALYZED: ", cfg.TableHeaderFont) };
            phrase.Add(new Chunk(cfg.PcSumm.TotalTimeSpan.ToString(), cfg.TableCellFont));
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 8;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk("TOTAL DOWN TIME:", cfg.TableHeaderFont) };
			phrase.Add(new Chunk(cfg.PcSumm.TotalDownTimeSpan.ToString(), cfg.TableCellFont));
			cell = new PdfPCell(phrase);
			cell.Border = 0;
			cell.Colspan = 8;
			table.AddCell(cell);

            phrase = new Phrase { new Chunk("DOWN TIME PERCENTAGE:", cfg.TableHeaderFont) };
            phrase.Add(new Chunk($"{cfg.PcSumm.TotalPercentDowntime.ToString(CultureInfo.InvariantCulture)}%", cfg.TableCellFont));
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 8;
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

            phrase = new Phrase { new Chunk("JOB NUMBERS: ", cfg.TableHeaderFont) };
            phrase.Add(new Chunk(jobnums, cfg.TableCellFont));
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 8;
            table.AddCell(cell);

            doc.Add(table);
			doc.Add(new Phrase("\n"));


		    foreach (ProcessCodeSummary sum in cfg.PcSumm.ProcessCodeSummaries)
		    {
                //table = ReportShared.DefineStandardTable(cfg);


                table = new PdfPTable(cols);
                table.DefaultCell.PaddingBottom = 5;
                table.WidthPercentage = 100;
                table.HeaderRows = 0;



                phrase = new Phrase {new Chunk($"Process Code {sum.ProcessCodeName}: ", cfg.TableHeaderFont)};
		        phrase.Add(new Chunk((sum.ProcessCode).ToString(CultureInfo.InvariantCulture), cfg.TableCellFont));
		        cell = new PdfPCell(phrase);
		        cell.Border = 0;
		        cell.Colspan = 8;
		        table.AddCell(cell);

		        phrase = new Phrase {new Chunk("Time Down: ", cfg.TableHeaderFont)};
		        phrase.Add(new Chunk($"{sum.TimeSpanDown}", cfg.TableCellFont));
		        cell = new PdfPCell(phrase);
		        cell.Border = 0;
		        cell.Colspan = 8;
		        table.AddCell(cell);

		        phrase = new Phrase {new Chunk("Percent Down: ", cfg.TableHeaderFont)};
		        phrase.Add(new Chunk($"{sum.PercentOfTime}", cfg.TableCellFont));
		        cell = new PdfPCell(phrase);
		        cell.Border = 0;
		        cell.Colspan = 8;
		        table.AddCell(cell);

		        doc.Add(table);
                doc.Add(new Phrase("\n"));
            }

		}

		private void OnBuildDataSection(Document doc, PdfReportConfig cfg)
		{
		    foreach (ProcessCodeSummary ps in cfg.PcSumm.ProcessCodeSummaries)
		    {
                doc.Add(new Phrase("\n"));
                PdfPTable table = ReportShared.CreateProcCodeStats(cfg,ps);
                doc.Add(table);
		        doc.NewPage();
		    }
        }

	}


}
