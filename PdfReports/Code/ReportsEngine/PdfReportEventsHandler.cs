using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfReports.Code.ReportsEngine
{
	public class PdfReportEventsHandler : PdfPageEventHelper
	{
		// the content byte object of the writer
		private PdfContentByte m_cb;

		// report creation time
		private DateTime m_printTime = DateTime.Now;

		// we will put the final number of pages using a template
		private PdfTemplate m_headerTemplate, m_footerTemplate;

		private readonly PdfReportConfig m_cfg;

		private Image m_logo;

		public PdfReportEventsHandler(PdfReportConfig cfg)
		{
			m_cfg = cfg ?? new PdfReportConfig();
		}

		public override void OnOpenDocument(PdfWriter writer, Document document)
		{
			try
			{
				m_printTime = DateTime.Now;

				m_cb = writer.DirectContent;
				m_headerTemplate = m_cb.CreateTemplate(100, 100);//was 100;
				m_footerTemplate = m_cb.CreateTemplate(50, 50);
			}
			catch (DocumentException dx)
			{
				Debug.WriteLine(dx.Message);
			}
			catch (IOException ix)
			{
				Debug.WriteLine(ix.Message);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		public override void OnCloseDocument(PdfWriter writer, Document document)
		{
			base.OnCloseDocument(writer, document);

			m_headerTemplate.BeginText();
			m_headerTemplate.SetFontAndSize(m_cfg.HeaderPagingFont, m_cfg.PagingFontSize);
			m_headerTemplate.SetTextMatrix(0, 0);
			m_headerTemplate.ShowText((writer.PageNumber - 1).ToString(CultureInfo.InvariantCulture));
			m_headerTemplate.EndText();

			m_footerTemplate.BeginText();
			m_footerTemplate.SetFontAndSize(m_cfg.HeaderPagingFont, m_cfg.PagingFontSize);
			m_footerTemplate.SetTextMatrix(0, 0);
			m_footerTemplate.ShowText((writer.PageNumber - 1).ToString(CultureInfo.InvariantCulture));
			m_footerTemplate.EndText();
		}

		/// <summary>
		/// Takes care of the page header and footer. Updates the paging information if needed.
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="document"></param>
		public override void OnEndPage(PdfWriter writer, Document document)
		{
			base.OnEndPage(writer, document);

			Phrase p1Header = new Phrase(m_cfg.HeaderText, m_cfg.HeaderBigFont);

			if (m_cfg.HasLogo())
			{
				// Here add a possibility for the configuration to have the logo as a memory image
				// I think the automated builder will require it
				m_logo = Image.GetInstance(m_cfg.LogoUrl);
				if (m_logo != null)
					m_logo.ScaleToFit(150, 100);
			}

			// paging
			String pagingText = "Page " + writer.PageNumber + " of ";
			float pagingFixedLen = m_cfg.HeaderPagingFont.GetWidthPoint(pagingText, m_cfg.PagingFontSize);
			float paging999Len = m_cfg.HeaderPagingFont.GetWidthPoint("999", m_cfg.PagingFontSize);

			if (m_cfg.PagingInHeader)
			{
				// adds "nn" in "Page 1 of nn"
				float x = document.PageSize.GetRight(document.RightMargin + pagingFixedLen + paging999Len);
				float y = document.PageSize.GetTop(45);
				m_cb.BeginText();
				m_cb.SetFontAndSize(m_cfg.HeaderPagingFont, m_cfg.PagingFontSize);
				m_cb.SetTextMatrix(x, y);
				m_cb.ShowText(pagingText);
				m_cb.EndText();
				m_cb.AddTemplate(m_headerTemplate, x + pagingFixedLen, y);
			}

			if (m_cfg.PagingInFooter)
			{
				// adds "nn" in "Page 1 of nn"
				float x = document.LeftMargin;
				float y = document.PageSize.GetBottom(30);

				m_cb.BeginText();
				m_cb.SetFontAndSize(m_cfg.HeaderPagingFont, m_cfg.PagingFontSize);
				m_cb.SetTextMatrix(x, y);
				m_cb.ShowText(pagingText);
				m_cb.EndText();
				m_cb.AddTemplate(m_footerTemplate, x + pagingFixedLen, y);
			}

			// header row 1
			PdfPCell cell1Logo = m_logo != null ? new PdfPCell(m_logo) : new PdfPCell();
			PdfPCell cell2Header = new PdfPCell(p1Header);
			PdfPCell cell3 = new PdfPCell();

			// header row 2
			PdfPCell cell456Subheader = new PdfPCell(new Phrase(m_cfg.SubheaderText, m_cfg.HeaderNormalFont));

			// header row 3
			PdfPCell cell7Date = new PdfPCell();
			PdfPCell cell8 = new PdfPCell(new Phrase("Created on: " + m_cfg.ReportCreationDateTime.ToString(CultureInfo.InvariantCulture), m_cfg.HeaderDateFont));
			PdfPCell cell9Time = new PdfPCell();


			// set the alignment of all three cells and set border to 0
			cell1Logo.HorizontalAlignment = Element.ALIGN_LEFT;
			cell2Header.HorizontalAlignment = Element.ALIGN_CENTER;
			cell3.HorizontalAlignment = Element.ALIGN_RIGHT;
			cell456Subheader.HorizontalAlignment = Element.ALIGN_CENTER;
			cell7Date.HorizontalAlignment = Element.ALIGN_LEFT;
			cell8.HorizontalAlignment = Element.ALIGN_CENTER;
			cell9Time.HorizontalAlignment = Element.ALIGN_RIGHT;

			cell2Header.VerticalAlignment = Element.ALIGN_BOTTOM;
			cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
			cell456Subheader.VerticalAlignment = Element.ALIGN_TOP;
			cell7Date.VerticalAlignment = Element.ALIGN_MIDDLE;
			cell8.VerticalAlignment = Element.ALIGN_MIDDLE;
			cell9Time.VerticalAlignment = Element.ALIGN_MIDDLE;

			cell456Subheader.Colspan = 3;

			cell1Logo.Border = 0;
			cell2Header.Border = 0;
			cell3.Border = 0;
			cell456Subheader.Border = 0;
			cell7Date.Border = 0;
			cell8.Border = 0;
			cell9Time.Border = 0;

			// add all cells into the header table
			PdfPTable headerTable = new PdfPTable(3);
			headerTable.AddCell(cell1Logo);
			headerTable.AddCell(cell2Header);
			headerTable.AddCell(cell3);
			headerTable.AddCell(cell456Subheader); // full row
			headerTable.AddCell(cell7Date);
			headerTable.AddCell(cell8);
			headerTable.AddCell(cell9Time);

			headerTable.TotalWidth = document.PageSize.Width - 80f;
			float[] colWidths = new float[] { 50, 80, 50 };
			headerTable.SetWidths(colWidths);
			//headerTable.WidthPercentage = 70;
			//pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;

			// This writes rows from PdfWriter in PdfTable
			// The first param is start row. -1 indicates there is no end row and all the rows to be included to write
			// Third and fourth params are x and y position to start writing
			headerTable.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);

			//// add all cells into the footer table
			//PdfPTable footerTable = new PdfPTable(1);
			//footerTable.TotalWidth = document.PageSize.Width - 80f;
			//PdfPCell cellrundate = new PdfPCell(new Phrase("Run Date: xxxxxx"));
			//cellrundate.VerticalAlignment = Element.ALIGN_MIDDLE;
			//cellrundate.HorizontalAlignment = Element.ALIGN_RIGHT;
			//footerTable.AddCell(cellrundate);
			//footerTable.WriteSelectedRows(0, -1, 40, document.PageSize.GetBottom(50), writer.DirectContent);



			if (m_cfg.TopSeparatorLine)
			{
				// moves the pointer and draws a line to separate header section from rest of page
				m_cb.MoveTo(40, document.PageSize.Height - 100);//was 100
				m_cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);//was100
				m_cb.Stroke();
			}

			if (m_cfg.BottomSeparatorLine)
			{
				// moves the pointer and draws a line to separate footer section from rest of page
				m_cb.MoveTo(40, document.PageSize.GetBottom(50));
				m_cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
				m_cb.Stroke();
			}
		}

	}

}
