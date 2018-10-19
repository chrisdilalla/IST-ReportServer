using ReportsWinClient.Forms.UI_Elements;

namespace ReportsWinClient.Forms
{
	partial class CtrlHistory
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.lblNextLoadNo = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboLineNumber = new System.Windows.Forms.ComboBox();
            this.btnUtilizationReport = new System.Windows.Forms.Button();
            this.txtProcessCode = new System.Windows.Forms.TextBox();
            this.txtJobNumber = new System.Windows.Forms.TextBox();
            this.btnRefineSearch = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.datagridviewStopEvents = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.SmallDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoursDown = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinutesDown = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JobNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetReport = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridviewStopEvents)).BeginInit();
            this.SuspendLayout();
            // 
            // dtFrom
            // 
            this.dtFrom.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFrom.Location = new System.Drawing.Point(136, 72);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(168, 22);
            this.dtFrom.TabIndex = 1;
            this.dtFrom.ValueChanged += new System.EventHandler(this.dt_ValueChanged);
            // 
            // lblNextLoadNo
            // 
            this.lblNextLoadNo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNextLoadNo.Location = new System.Drawing.Point(0, 36);
            this.lblNextLoadNo.Name = "lblNextLoadNo";
            this.lblNextLoadNo.Size = new System.Drawing.Size(364, 19);
            this.lblNextLoadNo.TabIndex = 62;
            this.lblNextLoadNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblDesc.Font = new System.Drawing.Font("Arial", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblDesc.Location = new System.Drawing.Point(0, 4);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(364, 36);
            this.lblDesc.TabIndex = 63;
            this.lblDesc.Text = "Search Criteria";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.comboLineNumber);
            this.panel1.Controls.Add(this.btnUtilizationReport);
            this.panel1.Controls.Add(this.txtProcessCode);
            this.panel1.Controls.Add(this.txtJobNumber);
            this.panel1.Controls.Add(this.btnRefineSearch);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtTo);
            this.panel1.Controls.Add(this.lblDesc);
            this.panel1.Controls.Add(this.dtFrom);
            this.panel1.Controls.Add(this.lblNextLoadNo);
            this.panel1.Location = new System.Drawing.Point(14, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(364, 480);
            this.panel1.TabIndex = 64;
            // 
            // comboLineNumber
            // 
            this.comboLineNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLineNumber.FormattingEnabled = true;
            this.comboLineNumber.Location = new System.Drawing.Point(136, 135);
            this.comboLineNumber.Name = "comboLineNumber";
            this.comboLineNumber.Size = new System.Drawing.Size(102, 21);
            this.comboLineNumber.TabIndex = 80;
            // 
            // btnUtilizationReport
            // 
            this.btnUtilizationReport.Location = new System.Drawing.Point(138, 399);
            this.btnUtilizationReport.Name = "btnUtilizationReport";
            this.btnUtilizationReport.Size = new System.Drawing.Size(100, 32);
            this.btnUtilizationReport.TabIndex = 78;
            this.btnUtilizationReport.Text = "Utilization Rpt";
            this.btnUtilizationReport.UseVisualStyleBackColor = true;
            this.btnUtilizationReport.Click += new System.EventHandler(this.btnUtilizationReport_Click);
            // 
            // txtProcessCode
            // 
            this.txtProcessCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtProcessCode.Location = new System.Drawing.Point(136, 201);
            this.txtProcessCode.Name = "txtProcessCode";
            this.txtProcessCode.Size = new System.Drawing.Size(102, 21);
            this.txtProcessCode.TabIndex = 77;
            // 
            // txtJobNumber
            // 
            this.txtJobNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtJobNumber.Location = new System.Drawing.Point(136, 172);
            this.txtJobNumber.Name = "txtJobNumber";
            this.txtJobNumber.Size = new System.Drawing.Size(101, 21);
            this.txtJobNumber.TabIndex = 76;
            // 
            // btnRefineSearch
            // 
            this.btnRefineSearch.Location = new System.Drawing.Point(137, 233);
            this.btnRefineSearch.Name = "btnRefineSearch";
            this.btnRefineSearch.Size = new System.Drawing.Size(101, 32);
            this.btnRefineSearch.TabIndex = 75;
            this.btnRefineSearch.Text = "Search Events";
            this.btnRefineSearch.UseVisualStyleBackColor = true;
            this.btnRefineSearch.Click += new System.EventHandler(this.btnRefineSearch_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 15);
            this.label5.TabIndex = 69;
            this.label5.Text = "Process Code:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(38, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 15);
            this.label4.TabIndex = 68;
            this.label4.Text = "Job Number:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 67;
            this.label3.Text = "Line Number:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 66;
            this.label1.Text = "To Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(49, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 65;
            this.label2.Text = "From Date:";
            // 
            // dtTo
            // 
            this.dtTo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTo.Location = new System.Drawing.Point(136, 102);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(168, 22);
            this.dtTo.TabIndex = 64;
            this.dtTo.ValueChanged += new System.EventHandler(this.dt_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.datagridviewStopEvents);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Location = new System.Drawing.Point(390, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(659, 479);
            this.panel2.TabIndex = 70;
            // 
            // datagridviewStopEvents
            // 
            this.datagridviewStopEvents.AllowUserToAddRows = false;
            this.datagridviewStopEvents.AllowUserToDeleteRows = false;
            this.datagridviewStopEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datagridviewStopEvents.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.datagridviewStopEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridviewStopEvents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SmallDateTime,
            this.ProcessCode,
            this.HoursDown,
            this.MinutesDown,
            this.LineNumber,
            this.JobNumber,
            this.PartCount,
            this.GetReport});
            this.datagridviewStopEvents.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.datagridviewStopEvents.Location = new System.Drawing.Point(12, 48);
            this.datagridviewStopEvents.MultiSelect = false;
            this.datagridviewStopEvents.Name = "datagridviewStopEvents";
            this.datagridviewStopEvents.ReadOnly = true;
            this.datagridviewStopEvents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datagridviewStopEvents.Size = new System.Drawing.Size(634, 394);
            this.datagridviewStopEvents.TabIndex = 64;
            this.datagridviewStopEvents.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridviewOrders_CellContentClick);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.Control;
            this.label11.Font = new System.Drawing.Font("Arial", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label11.Location = new System.Drawing.Point(12, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(634, 36);
            this.label11.TabIndex = 63;
            this.label11.Text = "Stop Events";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SmallDateTime
            // 
            this.SmallDateTime.HeaderText = "Event Date";
            this.SmallDateTime.Name = "SmallDateTime";
            this.SmallDateTime.ReadOnly = true;
            // 
            // ProcessCode
            // 
            this.ProcessCode.HeaderText = "Process Code";
            this.ProcessCode.Name = "ProcessCode";
            this.ProcessCode.ReadOnly = true;
            // 
            // HoursDown
            // 
            this.HoursDown.HeaderText = "Hours Down";
            this.HoursDown.Name = "HoursDown";
            this.HoursDown.ReadOnly = true;
            // 
            // MinutesDown
            // 
            this.MinutesDown.HeaderText = "Minutes Down";
            this.MinutesDown.Name = "MinutesDown";
            this.MinutesDown.ReadOnly = true;
            // 
            // LineNumber
            // 
            this.LineNumber.HeaderText = "Furnace Line";
            this.LineNumber.Name = "LineNumber";
            this.LineNumber.ReadOnly = true;
            // 
            // JobNumber
            // 
            this.JobNumber.HeaderText = "Job Number";
            this.JobNumber.Name = "JobNumber";
            this.JobNumber.ReadOnly = true;
            // 
            // PartCount
            // 
            this.PartCount.HeaderText = "Part Count";
            this.PartCount.Name = "PartCount";
            this.PartCount.ReadOnly = true;
            // 
            // GetReport
            // 
            this.GetReport.HeaderText = "";
            this.GetReport.Name = "GetReport";
            this.GetReport.ReadOnly = true;
            this.GetReport.Text = "";
            // 
            // CtrlHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CtrlHistory";
            this.Size = new System.Drawing.Size(1060, 490);
            this.Load += new System.EventHandler(this.CtrlHistory_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datagridviewStopEvents)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DateTimePicker dtFrom;
		private System.Windows.Forms.Label lblNextLoadNo;
		private System.Windows.Forms.Label lblDesc;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DateTimePicker dtTo;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.DataGridView datagridviewStopEvents;
		private System.Windows.Forms.Button btnRefineSearch;
		private System.Windows.Forms.TextBox txtJobNumber;
		private System.Windows.Forms.TextBox txtProcessCode;
        private System.Windows.Forms.Button btnUtilizationReport;
        private System.Windows.Forms.ComboBox comboLineNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn SmallDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoursDown;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinutesDown;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartCount;
        private System.Windows.Forms.DataGridViewButtonColumn GetReport;
    }
}
