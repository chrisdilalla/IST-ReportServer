using System.Windows.Forms;

namespace ReportsWinClient.Code
{
	public static class FormManagement
	{
		public static void DisposeAllControls(Panel panel)
		{
			if (panel.Controls.Count < 1) return;

			for (int i = 0; i < panel.Controls.Count; i++)
				panel.Controls[i].Dispose();
		}
	}
}
