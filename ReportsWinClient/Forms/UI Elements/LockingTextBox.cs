using System.Drawing;
using System.Windows.Forms;

namespace ReportsWinClient.Forms.UI_Elements
{
	public class LockingTextBox : TextBox
	{
		public LockingTextBox()
		{
			base.BackColor = SystemColors.Control;
		}

		public void LockMe()
		{
			ReadOnly = true;
			base.BackColor = SystemColors.Control;// SystemColors.Control;

			if (BorderStyle != BorderStyle.None)
			{
				Left = Left + 3;
				Top = Top + 3;
				BorderStyle = BorderStyle.None;
			}		
		}

		public void UnLockMe()
		{
			ReadOnly = false;
			base.BackColor = SystemColors.Window;
			if (BorderStyle != BorderStyle.Fixed3D)
			{
				Left = Left - 3;
				Top = Top - 3;
				BorderStyle = BorderStyle.Fixed3D;
			}
		}


	    public void DimMe(bool readOnly = true)
	    {
	        BackColor = ReadOnly ? SystemColors.Control : SystemColors.Window;
	        ForeColor = Color.Gray;
	    }

	    public void UnDimMe()
	    {
            BackColor = ReadOnly ? SystemColors.Control : SystemColors.Window;
            ForeColor = Color.Black;
	    }
	}
}

