using System;
using System.Diagnostics;

namespace PdfReports.Code.ReportsEngine
{
	public static class PdfReportUtils
	{
		/// <summary>
		/// Gets the color from HTML hex format.
		/// </summary>
		/// <param name="rgbColor">Color of the HTML RGB string.</param>
		/// <returns></returns>
		public static iTextSharp.text.Color GetColorFromHex(string rgbColor)
		{
			iTextSharp.text.Color color = new iTextSharp.text.Color(0, 0, 0);
			try
			{
				if (rgbColor != null && rgbColor.StartsWith("#"))
					color = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml(rgbColor));
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			return color; // default black
		}

		public static string GetPdfName(string baseName)
		{
			return string.Format("{0}_{1}.pdf", baseName, DateTime.Now.ToString("yyyyMMdd_HHmm"));
		}
	}
}
