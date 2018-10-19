using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Image = System.Drawing.Image;

namespace PdfReports.Code.ReportsEngine
{
	public class PdfReportConfig
	{
		public bool Portrait { get; set; }
		public string HeaderText { get; set; }
		public string SubheaderText { get; set; }

		public Image LogoImage { get; set; }
		public string LogoUrl { get; set; }
		public bool ShowLogo { get; set; }

		public bool TopSeparatorLine { get; set; }
		public bool BottomSeparatorLine { get; set; }

		public Font TableTitleFont { get; set; }
		public Font TableHeaderFont { get; set; }
		public Font TableCellFont { get; set; }

		public Font TableAlmHeaderFont { get; set; }
		public Font TableAlmCellFont { get; set; }
		public Font TableAlmTableEmptyFont { get; set; }

		public bool PagingInHeader { get; set; }
		public bool PagingInFooter { get; set; }
		public int PagingFontSize { get; set; }

		public Font HeaderDateFont { get; set; }
		public Font HeaderBigFont { get; set; }
		public Font HeaderNormalFont { get; set; }
		public BaseFont HeaderPagingFont { get; set; }

		public DateTime ReportCreationDateTime { get; set; }

		public ESignatureUsed SignatureUsed { get; set; }

		//public Order Order { get; set; }

		//public FurnLoad Load { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	    //public UtilizationData UtilizationData { get; set; }

	    public string DateFormat = "YYYY-MM-dd h:mm:ss";


		public PdfReportConfig()
		{
			Portrait = true;
			LogoImage = null;
			ShowLogo = false;

			HeaderText = "Report";
			HeaderText = "Account";

			TableTitleFont = FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK);
			TableHeaderFont = FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK);
			TableCellFont = FontFactory.GetFont("Arial", 9, Color.BLACK);

			TableAlmHeaderFont = FontFactory.GetFont("Arial", 7, Font.BOLD, Color.BLACK);
			TableAlmCellFont = FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK);
			TableAlmTableEmptyFont = FontFactory.GetFont("Arial", 7, Font.ITALIC, Color.BLACK);

			PagingInHeader = false;
			PagingInFooter = true;
			PagingFontSize = 10;

			HeaderBigFont = FontFactory.GetFont("Arial", 14, Font.BOLD, Color.BLACK);
			HeaderNormalFont = FontFactory.GetFont("Arial", 11, Font.BOLD, Color.BLACK);
			HeaderDateFont = FontFactory.GetFont("Arial", 8, Color.BLACK); //was 10
			HeaderPagingFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
		}

		public bool HasLogo()
		{
			return ShowLogo && (LogoImage != null || !string.IsNullOrWhiteSpace(LogoUrl));
		}

		public enum ESignatureUsed
		{
			None = 0,
			Standard =1,
		}

	}

}
