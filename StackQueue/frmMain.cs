using System;
using System.Drawing;
using System.Windows.Forms;

namespace StackQueue
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private JobSchadules js = new JobSchadules();

        /// <summary>
        /// این تابع برای تولید اطلاعات تصادفی است
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateData_Click(object sender, EventArgs e)
        {
            lstMain.Items.Clear();
            js = new JobSchadules();
            js.NumOfProcesses = (int)txtNumOfProcess.Value;
            js.ArrivalTimeInZero = chkAllProcessStartIn0.Checked;
            js.ProcessChanged += js_ProcessChanged;
            js.ProcessCompeleted += js_ProcessCompeleted;

            js.GenerateData();

            foreach (var p in js.processTable)
            {
                lstMain.Items.Add(new ListViewItem(new string[] { p.ProcessId.ToString(), p.ArrivalTime.ToString(), p.RemainTime.ToString(), p.ServiceTime.ToString(), p.Turna_vs_Service.ToString(), p.WaitTime.ToString(), p.FinishTime.ToString() }));
                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
            }
        }

        /// <summary>
        /// وقفه تکمیل پردازش
        /// </summary>
        /// <param name="p"></param>
        private void js_ProcessCompeleted(ProcessJob p)
        {
            ListViewItem fi = FindItem(p);

            if (fi == null)
                return;

            if (fi.SubItems[6].Text != p.FinishTime.ToString())
            {
                fi.SubItems[6].Text = p.FinishTime.ToString();
                fi.SubItems[6].BackColor = Color.SkyBlue;
            }

            fi.BackColor = Color.GreenYellow;
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
        }

        private Color cellColor = Color.Red;
        private Color precellColor = Color.Orange;

        private ListViewItem.ListViewSubItem preItem1 = null;
        private ListViewItem.ListViewSubItem preItem2 = null;
        private ListViewItem.ListViewSubItem preItem4 = null;
        private ListViewItem.ListViewSubItem preItem5 = null;

        /// <summary>
        /// وقفه خدمات رسانی به یک پردازش
        /// </summary>
        /// <param name="p"></param>
        private void js_ProcessChanged(ProcessJob p)
        {
            ListViewItem fi = FindItem(p);

            if (fi == null)
                return;

            if (preItem1 != null)
                preItem1.BackColor = precellColor;
            if (preItem2 != null)
                preItem2.BackColor = precellColor;
            if (preItem4 != null)
                preItem4.BackColor = precellColor;
            if (preItem5 != null)
                preItem5.BackColor = precellColor;

            fi.UseItemStyleForSubItems = false;
            //fi.BackColor = Color.White;

            if (fi.SubItems[1].Text != p.ArrivalTime.ToString())
            {
                preItem1 = fi.SubItems[1];
                fi.SubItems[1].Text = p.ArrivalTime.ToString();
                fi.SubItems[1].BackColor = cellColor;
            }

            if (fi.SubItems[2].Text != p.RemainTime.ToString())
            {
                preItem2 = fi.SubItems[2];
                fi.SubItems[2].Text = p.RemainTime.ToString();
                fi.SubItems[2].BackColor = cellColor;
            }

            if (fi.SubItems[4].Text != p.Turna_vs_Service.ToString())
            {
                preItem4 = fi.SubItems[4];

                fi.SubItems[4].Text = p.Turna_vs_Service.ToString();
                fi.SubItems[4].BackColor = cellColor;
            }

            if (fi.SubItems[5].Text != p.WaitTime.ToString())
            {
                preItem5 = fi.SubItems[5];

                fi.SubItems[5].Text = p.WaitTime.ToString();
                fi.SubItems[5].BackColor = cellColor;
            }

            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
        }

        private ListViewItem FindItem(ProcessJob p)
        {
            if (lstMain.Items.Count == 0)
                return null;

            foreach (ListViewItem v in lstMain.Items)
                if (v.SubItems[0].Text == p.ProcessId.ToString())
                    return v;

            return null;
        }

        /// <summary>
        /// بازنشانی اطلاعات پردازش ها
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            js.ResetData();
            lstMain.Items.Clear();

            foreach (var p in js.processTable)
            {
                var it = lstMain.Items.Add(new ListViewItem(new string[] { p.ProcessId.ToString(), p.ArrivalTime.ToString(), p.RemainTime.ToString(), p.ServiceTime.ToString(), p.Turna_vs_Service.ToString(), p.WaitTime.ToString(), p.FinishTime.ToString() }));
                it.BackColor = Color.White;
                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
            }
        }

        private void btnFCFS_Click(object sender, EventArgs e)
        {
            button4.PerformClick();
            var outmean = js.FCFS();
            lblAvg.Text = outmean.MeanVsService.ToString();
            lblWait.Text = outmean.MeanWaitTime.ToString();
        }

        private void btnSJF_Click(object sender, EventArgs e)
        {
            button4.PerformClick();
            var outmean = js.SRT();
            lblAvg.Text = outmean.MeanVsService.ToString();
            lblWait.Text = outmean.MeanWaitTime.ToString();
        }

        private void btnRR_Click(object sender, EventArgs e)
        {
            button4.PerformClick();
            var outmean = js.RR();
            lblAvg.Text = outmean.MeanVsService.ToString();
            lblWait.Text = outmean.MeanWaitTime.ToString();
        }

        private void btnHRRN_Click(object sender, EventArgs e)
        {
            button4.PerformClick();
            var outmean = js.HRRN();
            lblAvg.Text = outmean.MeanVsService.ToString();
            lblWait.Text = outmean.MeanWaitTime.ToString();
        }
    }
}