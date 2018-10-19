using System;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfReports.Code.ReportsEngine
{
	public class PdfReportBuilder
	{
		public event Action<Document, PdfReportConfig> ParametersSectionBuilder;
		public event Action<Document, PdfReportConfig> DataSectionBuilder;
		public event Action<Document, PdfReportConfig> EndSectionBuilder;

		private readonly PdfReportConfig m_config;

		public PdfReportBuilder(PdfReportConfig cfg)
		{
			m_config = cfg;
		}

		public byte[] CreatePdf()
		{
			//set the page dimensions
			Document doc = new Document(m_config.Portrait ? PageSize.LETTER : PageSize.LETTER.Rotate(), 40, 40, 130, 70);

			using (MemoryStream output = new MemoryStream())
			{
				PdfWriter wri = PdfWriter.GetInstance(doc, output);
				wri.PageEvent = new PdfReportEventsHandler(m_config);
				doc.Open();

				if (ParametersSectionBuilder != null)
				{
					try
					{
						ParametersSectionBuilder(doc, m_config);
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine("Parameters: " + ex.Message);
					}
				}

				if (DataSectionBuilder != null)
				{
					try
					{
						DataSectionBuilder(doc, m_config);
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine("Data: " + ex.Message);
					}
				}

				if (m_config.SignatureUsed != PdfReportConfig.ESignatureUsed.None)
				{
					try
					{
						BuildSignature(doc, m_config);
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine("Signature: " + ex.Message);
					}
				}

				doc.Close();
				return output.ToArray();
			}

		}

		private void BuildSignature(Document doc, PdfReportConfig cfg)
		{
			switch (cfg.SignatureUsed)
			{
				case PdfReportConfig.ESignatureUsed.Standard:
					BuildStandardSignature(doc, cfg);
					break;

				default:
					//do nothing
					break;
			}
		}

		private void BuildStandardSignature(Document doc, PdfReportConfig cfg)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("\n");
			sb.Append("\nSupervisor Signature: __________________________________________   Date: _________________");

			Phrase phrase = new Phrase(sb.ToString(),cfg.HeaderNormalFont);
			doc.Add(phrase);
		}

	}

}
