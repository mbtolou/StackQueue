namespace StackQueue
{
    partial class frmMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGenerateData = new System.Windows.Forms.Button();
            this.btnFCFS = new System.Windows.Forms.Button();
            this.btnSJF = new System.Windows.Forms.Button();
            this.btnRR = new System.Windows.Forms.Button();
            this.btnHRRN = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.lstMain = new System.Windows.Forms.ListView();
            this.ProcessId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ArrivalTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RemainTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ServiceTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WaitVsService = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WaitTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FinishTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkAllProcessStartIn0 = new System.Windows.Forms.CheckBox();
            this.txtNumOfProcess = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblWait = new System.Windows.Forms.Label();
            this.lblAvg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumOfProcess)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerateData
            // 
            this.btnGenerateData.Location = new System.Drawing.Point(12, 12);
            this.btnGenerateData.Name = "btnGenerateData";
            this.btnGenerateData.Size = new System.Drawing.Size(96, 23);
            this.btnGenerateData.TabIndex = 0;
            this.btnGenerateData.Text = "Generate Data";
            this.btnGenerateData.UseVisualStyleBackColor = true;
            this.btnGenerateData.Click += new System.EventHandler(this.btnGenerateData_Click);
            // 
            // btnFCFS
            // 
            this.btnFCFS.Location = new System.Drawing.Point(522, 12);
            this.btnFCFS.Name = "btnFCFS";
            this.btnFCFS.Size = new System.Drawing.Size(96, 23);
            this.btnFCFS.TabIndex = 1;
            this.btnFCFS.Text = "FCFS";
            this.btnFCFS.UseVisualStyleBackColor = true;
            this.btnFCFS.Click += new System.EventHandler(this.btnFCFS_Click);
            // 
            // btnSJF
            // 
            this.btnSJF.Location = new System.Drawing.Point(420, 12);
            this.btnSJF.Name = "btnSJF";
            this.btnSJF.Size = new System.Drawing.Size(96, 23);
            this.btnSJF.TabIndex = 2;
            this.btnSJF.Text = "SJF";
            this.btnSJF.UseVisualStyleBackColor = true;
            this.btnSJF.Click += new System.EventHandler(this.btnSJF_Click);
            // 
            // btnRR
            // 
            this.btnRR.Location = new System.Drawing.Point(318, 12);
            this.btnRR.Name = "btnRR";
            this.btnRR.Size = new System.Drawing.Size(96, 23);
            this.btnRR.TabIndex = 3;
            this.btnRR.Text = "RR";
            this.btnRR.UseVisualStyleBackColor = true;
            this.btnRR.Click += new System.EventHandler(this.btnRR_Click);
            // 
            // btnHRRN
            // 
            this.btnHRRN.Location = new System.Drawing.Point(216, 12);
            this.btnHRRN.Name = "btnHRRN";
            this.btnHRRN.Size = new System.Drawing.Size(96, 23);
            this.btnHRRN.TabIndex = 4;
            this.btnHRRN.Text = "HRRN";
            this.btnHRRN.UseVisualStyleBackColor = true;
            this.btnHRRN.Click += new System.EventHandler(this.btnHRRN_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(114, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Reset Data";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // lstMain
            // 
            this.lstMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProcessId,
            this.ArrivalTime,
            this.RemainTime,
            this.ServiceTime,
            this.WaitVsService,
            this.WaitTime,
            this.FinishTime});
            this.lstMain.FullRowSelect = true;
            this.lstMain.HideSelection = false;
            this.lstMain.LabelEdit = true;
            this.lstMain.Location = new System.Drawing.Point(12, 41);
            this.lstMain.Name = "lstMain";
            this.lstMain.Size = new System.Drawing.Size(606, 380);
            this.lstMain.TabIndex = 6;
            this.lstMain.UseCompatibleStateImageBehavior = false;
            this.lstMain.View = System.Windows.Forms.View.Details;
            // 
            // ProcessId
            // 
            this.ProcessId.Text = "ProcessId";
            // 
            // ArrivalTime
            // 
            this.ArrivalTime.Text = "ArrivalTime";
            this.ArrivalTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ArrivalTime.Width = 85;
            // 
            // RemainTime
            // 
            this.RemainTime.Text = "RemainTime";
            this.RemainTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RemainTime.Width = 85;
            // 
            // ServiceTime
            // 
            this.ServiceTime.Text = "ServiceTime";
            this.ServiceTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ServiceTime.Width = 85;
            // 
            // WaitVsService
            // 
            this.WaitVsService.Text = "WaitVsService";
            this.WaitVsService.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.WaitVsService.Width = 104;
            // 
            // WaitTime
            // 
            this.WaitTime.Text = "WaitTime";
            this.WaitTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.WaitTime.Width = 85;
            // 
            // FinishTime
            // 
            this.FinishTime.Text = "FinishTime";
            this.FinishTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FinishTime.Width = 85;
            // 
            // chkAllProcessStartIn0
            // 
            this.chkAllProcessStartIn0.AutoSize = true;
            this.chkAllProcessStartIn0.Location = new System.Drawing.Point(12, 427);
            this.chkAllProcessStartIn0.Name = "chkAllProcessStartIn0";
            this.chkAllProcessStartIn0.Size = new System.Drawing.Size(126, 17);
            this.chkAllProcessStartIn0.TabIndex = 7;
            this.chkAllProcessStartIn0.Text = "All Process Start In 0";
            this.chkAllProcessStartIn0.UseVisualStyleBackColor = true;
            // 
            // txtNumOfProcess
            // 
            this.txtNumOfProcess.Location = new System.Drawing.Point(119, 452);
            this.txtNumOfProcess.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.txtNumOfProcess.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtNumOfProcess.Name = "txtNumOfProcess";
            this.txtNumOfProcess.Size = new System.Drawing.Size(56, 21);
            this.txtNumOfProcess.TabIndex = 8;
            this.txtNumOfProcess.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 456);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Num Of Processes :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(417, 431);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Avrage Time :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(433, 456);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "WaitTime :";
            // 
            // lblWait
            // 
            this.lblWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWait.Location = new System.Drawing.Point(497, 452);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(121, 21);
            this.lblWait.TabIndex = 13;
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAvg
            // 
            this.lblAvg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAvg.Location = new System.Drawing.Point(497, 427);
            this.lblAvg.Name = "lblAvg";
            this.lblAvg.Size = new System.Drawing.Size(121, 21);
            this.lblAvg.TabIndex = 14;
            this.lblAvg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 478);
            this.Controls.Add(this.lblAvg);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNumOfProcess);
            this.Controls.Add(this.chkAllProcessStartIn0);
            this.Controls.Add(this.lstMain);
            this.Controls.Add(this.btnHRRN);
            this.Controls.Add(this.btnRR);
            this.Controls.Add(this.btnSJF);
            this.Controls.Add(this.btnFCFS);
            this.Controls.Add(this.btnGenerateData);
            this.Controls.Add(this.button4);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "بررسی روش های مختلف زمانبندی";
            ((System.ComponentModel.ISupportInitialize)(this.txtNumOfProcess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateData;
        private System.Windows.Forms.Button btnFCFS;
        private System.Windows.Forms.Button btnSJF;
        private System.Windows.Forms.Button btnRR;
        private System.Windows.Forms.Button btnHRRN;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListView lstMain;
        private System.Windows.Forms.ColumnHeader ProcessId;
        private System.Windows.Forms.ColumnHeader ArrivalTime;
        private System.Windows.Forms.ColumnHeader RemainTime;
        private System.Windows.Forms.ColumnHeader ServiceTime;
        private System.Windows.Forms.ColumnHeader WaitVsService;
        private System.Windows.Forms.ColumnHeader WaitTime;
        private System.Windows.Forms.ColumnHeader FinishTime;
        private System.Windows.Forms.CheckBox chkAllProcessStartIn0;
        private System.Windows.Forms.NumericUpDown txtNumOfProcess;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Label lblAvg;
    }
}