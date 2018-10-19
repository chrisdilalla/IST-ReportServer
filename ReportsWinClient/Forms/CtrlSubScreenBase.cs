using System.Collections.Generic;
using System.Windows.Forms;
using ReportsWinClient.Code;

namespace ReportsWinClient.Forms
{
	public partial class CtrlSubScreenBase : UserControl
	{

		public List<SubPanel> SubPanels = new List<SubPanel>();
		public string ScreenTitle { get; set; }

		public class SubPanel
		{
			public Panel SPanel { get; set; }
			public Control DefaultControl { get; set; }

			public SubPanel(Panel panel, Control control)
			{
				SPanel = panel;
				DefaultControl = control;
			}
		}

		public void DefineDefaultSubPanel(Panel panel,Control ctrl)
		{
			SubPanels.Add(new SubPanel(panel,ctrl));
		}		


		public CtrlSubScreenBase()
		{
			InitializeComponent();
		}


		public void LoadDefaultSubPanels()
		{
			foreach (SubPanel sp in SubPanels)
			{
				FormManagement.DisposeAllControls(sp.SPanel);
				sp.SPanel.Controls.Add(sp.DefaultControl);
			}
			if (SubPanels.Count == 0)
			{
				//MessageBox.Show(string.Format("{0} - No Default Panels Defined", Name));
			}
		}
	}
}
