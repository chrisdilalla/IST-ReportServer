using System;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Library.Models.ReportModels;
using PdfReports.Code.ReportsEngine;

namespace PdfReports.Code.Reports
{
    public static class ReportShared
    {
        public static PdfPTable DefineStandardTable(PdfReportConfig cfg)
        {
            // the table object
            const int cols = 8;
            PdfPTable table = new PdfPTable(cols);
            table.DefaultCell.PaddingBottom = 5;
            table.WidthPercentage = 100;
            table.HeaderRows = 0;
            table.DefaultCell.Phrase = new Phrase { Font = cfg.TableCellFont };

            // columns relative widths
            float[] colWidths = new float[] { 1, 1, 1, 1, 1, 1, 1, 1 };
            table.SetWidths(colWidths);

            return table;
        }

        //public static PdfPTable CreateZoneHeader(PdfReportConfig cfg, ZoneDetails zDets)
        //{
        //	PdfPTable table = DefineStandardTable(cfg);

        //	Phrase phrase = new Phrase { new Chunk(zDets.ZoneName, cfg.TableHeaderFont) };
        //	PdfPCell cell = new PdfPCell(phrase);
        //	cell.Border = 0;
        //	cell.Colspan = 8;
        //	cell.GrayFill = (float)0.95;
        //	table.AddCell(cell);

        //	string entertime = zDets.EnterTime != null ? ((DateTime) zDets.EnterTime).ToLocalTime().ToString(CultureInfo.InvariantCulture) : "";
        //	string exittime = zDets.ExitTime != null ? ((DateTime)zDets.ExitTime).ToLocalTime().ToString(CultureInfo.InvariantCulture) : "";

        //	phrase = new Phrase { new Chunk("Enter Zone: ", cfg.TableHeaderFont) };
        //	phrase.Add(new Chunk(entertime, cfg.TableCellFont));
        //	cell = new PdfPCell(phrase);
        //	cell.Border = 0;
        //	cell.Colspan = 3;
        //	table.AddCell(cell);

        //	phrase = new Phrase { new Chunk("Exit Zone: ", cfg.TableHeaderFont) };
        //	phrase.Add(new Chunk(exittime, cfg.TableCellFont));
        //	cell = new PdfPCell(phrase);
        //	cell.Border = 0;
        //	cell.Colspan = 3;
        //	table.AddCell(cell);

        //	phrase = new Phrase("");
        //	cell = new PdfPCell(phrase);
        //	cell.Border = 0;
        //	cell.Colspan = 2;
        //	table.AddCell(cell);

        //	return table;
        //}

        public static PdfPTable CreateProcCodeStats(PdfReportConfig cfg, ProcessCodeSummary sum)
        {
            PdfPTable table = DefineStandardTable(cfg);
            Phrase phrase;
            PdfPCell cell;

            phrase = new Phrase { new Chunk($"Process Code {sum.ProcessCodeName}: ", cfg.TableHeaderFont) };
            phrase.Add(new Chunk((sum.ProcessCode).ToString(CultureInfo.InvariantCulture), cfg.TableCellFont));
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk("Time Down: ", cfg.TableHeaderFont) };
            phrase.Add(new Chunk($"{sum.TimeSpanDown}", cfg.TableCellFont));
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            phrase = new Phrase { new Chunk("Percent Down: ", cfg.TableHeaderFont) };
            phrase.Add(new Chunk($"{sum.PercentOfTime}", cfg.TableCellFont));
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            return table;
        }



        public static PdfPTable CreateAlarmTable(PdfReportConfig cfg)
        {
            //Left in as a teaser for next phase of the project

            // the table object
            const int cols = 6;
            PdfPTable table = new PdfPTable(cols);
            table.DefaultCell.PaddingBottom = 5;
            table.WidthPercentage = 100;
            table.HeaderRows = 2;
            table.DefaultCell.Phrase = new Phrase { Font = cfg.TableAlmCellFont };

            // columns relative widths
            float[] colWidths = new float[] { 10, 30, 0, 20, 20, 20 };
            table.SetWidths(colWidths);


            // Alarms - Row1
            Phrase phrase = new Phrase("ALARMS", cfg.TableHeaderFont);
            PdfPCell cell = new PdfPCell(phrase);
            cell.GrayFill = (float).95;
            cell.Border = 0;
            cell.Colspan = 6;
            table.AddCell(cell);

            // Alarms - Row2 - col1
            phrase = new Phrase("ALM #", cfg.TableAlmHeaderFont);
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 1;
            table.AddCell(cell);

            // Alarms - Row2 - col2
            phrase = new Phrase("DESCRIPTION", cfg.TableAlmHeaderFont);
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 1;
            table.AddCell(cell);

            // Alarms - Row2 - col3
            phrase = new Phrase("GROUP", cfg.TableAlmHeaderFont);
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 1;
            table.AddCell(cell);

            // Alarms - Row2 - col4
            phrase = new Phrase("SET TIME", cfg.TableAlmHeaderFont);
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 1;
            table.AddCell(cell);

            // Alarms - Row2 - col5
            phrase = new Phrase("CLEAR TIME", cfg.TableAlmHeaderFont);
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 1;
            table.AddCell(cell);


            // Alarms - Row2 - col6
            phrase = new Phrase("ACK TIME", cfg.TableAlmHeaderFont);
            cell = new PdfPCell(phrase);
            cell.Border = 0;
            cell.Colspan = 1;
            table.AddCell(cell);

            //clsAlarmList almList = cfg.OrderBase.AggregateAlarms();


            //if (almList.Count > 0)
            //{

            //	foreach (clsAlarm alm in almList)
            //	{
            //		cell = new PdfPCell(new Phrase(alm.Alm_ID.ToString(CultureInfo.InvariantCulture), cfg.TableAlmCellFont));
            //		table.AddCell(cell);

            //		cell = new PdfPCell(new Phrase(alm.Alm_Desc, cfg.TableAlmCellFont));
            //		table.AddCell(cell);

            //		cell = new PdfPCell(new Phrase(alm.Alm_Group, cfg.TableAlmCellFont));
            //		table.AddCell(cell);

            //		cell = new PdfPCell(new Phrase(alm.Alm_SetTime.ToString(CultureInfo.InvariantCulture), cfg.TableAlmCellFont));
            //		table.AddCell(cell);

            //		cell = new PdfPCell(new Phrase(alm.Alm_ClearTime.ToString(CultureInfo.InvariantCulture), cfg.TableAlmCellFont));
            //		table.AddCell(cell);

            //		cell = new PdfPCell(new Phrase(alm.Alm_AckTime.ToString(CultureInfo.InvariantCulture), cfg.TableAlmCellFont));
            //		table.AddCell(cell);
            //	}
            //}
            //else
            //{
            cell = new PdfPCell(new Phrase("No alarms recorded for this order", cfg.TableAlmTableEmptyFont));
            cell.Colspan = 6;
            table.AddCell(cell);
            //}

            return table;

        }


        //private PdfPTable CreateBinTable(PdfReportConfig cfg)
        //{
        //	// the table object
        //	const int cols = 6;
        //	PdfPTable table = new PdfPTable(cols);
        //	table.DefaultCell.PaddingBottom = 5;
        //	table.WidthPercentage = 100;
        //	table.HeaderRows = 2;
        //	table.DefaultCell.Phrase = new Phrase { Font = cfg.TableAlmCellFont };

        //	// columns relative widths
        //	float[] colWidths = new float[] { 10, 15, 15, 20, 20, 20 };
        //	table.SetWidths(colWidths);


        //	// Bins - Row1
        //	Phrase phrase = new Phrase("BINS", cfg.TableHeaderFont);
        //	PdfPCell cell = new PdfPCell(phrase);
        //	cell.GrayFill = (float).95;
        //	cell.Border = 0;
        //	cell.Colspan = 6;
        //	table.AddCell(cell);

        //	// Bins - Row2 - col1
        //	phrase = new Phrase("BIN #", cfg.TableAlmHeaderFont);
        //	cell = new PdfPCell(phrase);
        //	cell.Border = 0;
        //	cell.Colspan = 1;
        //	table.AddCell(cell);

        //	// Bins - Row2 - col2
        //	phrase = new Phrase("TARGET Wt", cfg.TableAlmHeaderFont);
        //	cell = new PdfPCell(phrase);
        //	cell.Border = 0;
        //	cell.Colspan = 1;
        //	table.AddCell(cell);

        //	// Bins - Row2 - col3
        //	phrase = new Phrase("ACTUAL Wt", cfg.TableAlmHeaderFont);
        //	cell = new PdfPCell(phrase);
        //	cell.Border = 0;
        //	cell.Colspan = 1;
        //	table.AddCell(cell);

        //	// Bins - Row2 - col4
        //	phrase = new Phrase("BIN CREATE", cfg.TableAlmHeaderFont);
        //	cell = new PdfPCell(phrase);
        //	cell.Border = 0;
        //	cell.Colspan = 1;
        //	table.AddCell(cell);

        //	// Bins - Row2 - col5
        //	phrase = new Phrase("START FILLING", cfg.TableAlmHeaderFont);
        //	cell = new PdfPCell(phrase);
        //	cell.Border = 0;
        //	cell.Colspan = 1;
        //	table.AddCell(cell);


        //	// Bins - Row2 - col6
        //	phrase = new Phrase("STOP FILLING", cfg.TableAlmHeaderFont);
        //	cell = new PdfPCell(phrase);
        //	cell.Border = 0;
        //	cell.Colspan = 1;
        //	table.AddCell(cell);

        //	if (cfg.OrderBase.BinList.Count > 0)
        //	{

        //		foreach (clsBin b in cfg.OrderBase.BinList)
        //		{
        //			cell = new PdfPCell(new Phrase(b.binLDNumber.ToString(CultureInfo.InvariantCulture), cfg.TableAlmCellFont));
        //			table.AddCell(cell);

        //			cell = new PdfPCell(new Phrase(b.binLDwt.ToString(CultureInfo.InvariantCulture), cfg.TableAlmCellFont));
        //			table.AddCell(cell);

        //			cell = new PdfPCell(new Phrase(b.binULDwt.ToString(CultureInfo.InvariantCulture), cfg.TableAlmCellFont));
        //			table.AddCell(cell);

        //			cell = new PdfPCell(new Phrase(b.binLDcreate.ToString(), cfg.TableAlmCellFont));
        //			table.AddCell(cell);

        //			cell = new PdfPCell(new Phrase(b.binULDstart.ToString(), cfg.TableAlmCellFont));
        //			table.AddCell(cell);

        //			cell = new PdfPCell(new Phrase(b.binULDstop.ToString(), cfg.TableAlmCellFont));
        //			table.AddCell(cell);
        //		}
        //	}
        //	else
        //	{
        //		cell = new PdfPCell(new Phrase("No bins recorded for this order", cfg.TableAlmTableEmptyFont));
        //		cell.Colspan = 6;
        //		table.AddCell(cell);
        //	}


        //	return table;
        //}
    }
}
