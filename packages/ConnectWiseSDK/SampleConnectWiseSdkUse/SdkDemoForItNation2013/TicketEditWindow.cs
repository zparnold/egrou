using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SdkDemoForItNation2013
{
    public class TicketEditWindow : Form
    {
        private List<string> StatusList = new List<string>();
        private Dictionary<int, string> ServiceTypes = new Dictionary<int, string>();
        private Dictionary<int, string> ServiceSubTypes = new Dictionary<int, string>();
        private List<string> Sources = new List<string>();
        private List<string> Priorities = new List<string>();

        //public fields
        public string StatusName;
        public string ServiceType;
        public string ServiceSubType;
        public string Source;
        public string Priority;
        public string Summary;
        public string Note;
        public bool IsDetail;
        public bool IsInternal;
        public bool IsResolution;

        //main const
        public TicketEditWindow()
        {
            InitializeComponent();
            CenterToScreen();
        }

        //ticketeditwindow load event
        private async void TicketEditWindow_Load(object sender, EventArgs e)
        {
            await LoadTicketInformation();
        }

        //load most of the ticket information
        private async Task LoadTicketInformation()
        {
            StatusList = await Helper.LoadTicketStatuses();
            ServiceTypes = await Helper.LoadServiceTypes();
            ServiceSubTypes = await Helper.LoadServiceSubTypes();
            Sources = await Helper.LoadServiceSources();
            Priorities = await Helper.LoadServicePriorities();
            LoadFields();
            lblLoading.Visible = false;
        }

        //loads ui controls with ticket information
        private void LoadFields()
        {
            foreach (string s in StatusList)
                cbxStatus.Items.Add(s);
            foreach (KeyValuePair<int, string> kvp in ServiceTypes)
                cbxServiceType.Items.Add(kvp.Value);
            foreach (string s in Sources)
                cbxSource.Items.Add(s);
            foreach (string s in Priorities)
                cbxPriority.Items.Add(s);
            txtSummary.Text = Helper.ServiceTicket.Summary;
        }

        //cbxServiceType index changed event to load service sub types for the type selected
        private void cbxServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxServiceSubType.Items.Clear();
            int typeId = ServiceTypes.Where(x => x.Value.Equals(cbxServiceType.SelectedItem.ToString())).Select(x => x.Key).FirstOrDefault();
            if (typeId < 1) return;
            if(ServiceSubTypes.All(x => x.Key != typeId)) return;
            foreach (KeyValuePair<int, string> serviceSubType in ServiceSubTypes.Where(x => x.Key == typeId))
                cbxServiceSubType.Items.Add(serviceSubType.Value);
        }

        //btnUpdateTicket click event to set the public fields (helper uses these fields)
        private void btnUpdateTicket_Click(object sender, EventArgs e)
        {
            if (cbxStatus.SelectedIndex > -1)
                StatusName = cbxStatus.SelectedItem.ToString();
            if (cbxServiceType.SelectedIndex > -1)
                ServiceType = cbxServiceType.SelectedItem.ToString();
            if (cbxServiceSubType.SelectedIndex > -1)
                ServiceSubType = cbxServiceSubType.SelectedItem.ToString();
            if (cbxSource.SelectedIndex > -1)
                Source = cbxSource.SelectedItem.ToString();
            if (cbxPriority.SelectedIndex > -1)
                Priority = cbxPriority.SelectedItem.ToString();
            Summary = txtSummary.Text;
            Note = txtNote.Text;
            IsDetail = chkDetail.Checked;
            IsInternal = chkInternal.Checked;
            IsResolution = chkResolution.Checked;
            Hide();
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TicketEditWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxStatus = new System.Windows.Forms.ComboBox();
            this.cbxServiceType = new System.Windows.Forms.ComboBox();
            this.cbxServiceSubType = new System.Windows.Forms.ComboBox();
            this.cbxSource = new System.Windows.Forms.ComboBox();
            this.cbxPriority = new System.Windows.Forms.ComboBox();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkResolution = new System.Windows.Forms.CheckBox();
            this.chkInternal = new System.Windows.Forms.CheckBox();
            this.chkDetail = new System.Windows.Forms.CheckBox();
            this.btnUpdateTicket = new System.Windows.Forms.Button();
            this.lblLoading = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Source:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(152, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Priority:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Summary:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Notes Text:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(98, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Service Sub Type:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(120, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Service Type:";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // cbxStatus
            // 
            this.cbxStatus.FormattingEnabled = true;
            this.cbxStatus.ItemHeight = 13;
            this.cbxStatus.Location = new System.Drawing.Point(199, 29);
            this.cbxStatus.Name = "cbxStatus";
            this.cbxStatus.Size = new System.Drawing.Size(164, 21);
            this.cbxStatus.TabIndex = 1;
            this.cbxStatus.SelectedIndexChanged += new System.EventHandler(this.cbxStatus_SelectedIndexChanged);
            // 
            // cbxServiceType
            // 
            this.cbxServiceType.FormattingEnabled = true;
            this.cbxServiceType.Location = new System.Drawing.Point(199, 57);
            this.cbxServiceType.Name = "cbxServiceType";
            this.cbxServiceType.Size = new System.Drawing.Size(164, 21);
            this.cbxServiceType.TabIndex = 2;
            this.cbxServiceType.SelectedIndexChanged += new System.EventHandler(this.cbxServiceType_SelectedIndexChanged);
            // 
            // cbxServiceSubType
            // 
            this.cbxServiceSubType.FormattingEnabled = true;
            this.cbxServiceSubType.Location = new System.Drawing.Point(199, 85);
            this.cbxServiceSubType.Name = "cbxServiceSubType";
            this.cbxServiceSubType.Size = new System.Drawing.Size(164, 21);
            this.cbxServiceSubType.TabIndex = 3;
            this.cbxServiceSubType.SelectedIndexChanged += new System.EventHandler(this.cbxServiceSubType_SelectedIndexChanged);
            // 
            // cbxSource
            // 
            this.cbxSource.FormattingEnabled = true;
            this.cbxSource.Location = new System.Drawing.Point(199, 132);
            this.cbxSource.Name = "cbxSource";
            this.cbxSource.Size = new System.Drawing.Size(164, 21);
            this.cbxSource.TabIndex = 5;
            this.cbxSource.SelectedIndexChanged += new System.EventHandler(this.cbxSource_SelectedIndexChanged);
            // 
            // cbxPriority
            // 
            this.cbxPriority.FormattingEnabled = true;
            this.cbxPriority.Location = new System.Drawing.Point(199, 160);
            this.cbxPriority.Name = "cbxPriority";
            this.cbxPriority.Size = new System.Drawing.Size(164, 21);
            this.cbxPriority.TabIndex = 6;
            this.cbxPriority.SelectedIndexChanged += new System.EventHandler(this.cbxPriority_SelectedIndexChanged);
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(33, 230);
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(435, 20);
            this.txtSummary.TabIndex = 7;
            this.txtSummary.TextChanged += new System.EventHandler(this.txtSummary_TextChanged);
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(21, 42);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(435, 47);
            this.txtNote.TabIndex = 8;
            this.txtNote.TextChanged += new System.EventHandler(this.txtNote_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkResolution);
            this.groupBox1.Controls.Add(this.chkInternal);
            this.groupBox1.Controls.Add(this.chkDetail);
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 276);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 192);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Note:";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // chkResolution
            // 
            this.chkResolution.AutoSize = true;
            this.chkResolution.Location = new System.Drawing.Point(25, 151);
            this.chkResolution.Name = "chkResolution";
            this.chkResolution.Size = new System.Drawing.Size(116, 17);
            this.chkResolution.TabIndex = 11;
            this.chkResolution.Text = "Part of Resolution?";
            this.chkResolution.UseVisualStyleBackColor = true;
            this.chkResolution.CheckedChanged += new System.EventHandler(this.chkResolution_CheckedChanged);
            // 
            // chkInternal
            // 
            this.chkInternal.AutoSize = true;
            this.chkInternal.Location = new System.Drawing.Point(25, 128);
            this.chkInternal.Name = "chkInternal";
            this.chkInternal.Size = new System.Drawing.Size(142, 17);
            this.chkInternal.TabIndex = 10;
            this.chkInternal.Text = "Part of Internal Analysis?";
            this.chkInternal.UseVisualStyleBackColor = true;
            this.chkInternal.CheckedChanged += new System.EventHandler(this.chkInternal_CheckedChanged);
            // 
            // chkDetail
            // 
            this.chkDetail.AutoSize = true;
            this.chkDetail.Location = new System.Drawing.Point(25, 105);
            this.chkDetail.Name = "chkDetail";
            this.chkDetail.Size = new System.Drawing.Size(149, 17);
            this.chkDetail.TabIndex = 9;
            this.chkDetail.Text = "Part of Detail Desctiption?";
            this.chkDetail.UseVisualStyleBackColor = true;
            this.chkDetail.CheckedChanged += new System.EventHandler(this.chkDetail_CheckedChanged);
            // 
            // btnUpdateTicket
            // 
            this.btnUpdateTicket.Location = new System.Drawing.Point(215, 492);
            this.btnUpdateTicket.Name = "btnUpdateTicket";
            this.btnUpdateTicket.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateTicket.TabIndex = 12;
            this.btnUpdateTicket.Text = "Update";
            this.btnUpdateTicket.UseVisualStyleBackColor = true;
            this.btnUpdateTicket.Click += new System.EventHandler(this.btnUpdateTicket_Click);
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(212, 206);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(87, 13);
            this.lblLoading.TabIndex = 0;
            this.lblLoading.Text = "Loading Ticket...";
            this.lblLoading.Click += new System.EventHandler(this.lblLoading_Click);
            // 
            // TicketEditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(227)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(502, 542);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.btnUpdateTicket);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtSummary);
            this.Controls.Add(this.cbxPriority);
            this.Controls.Add(this.cbxSource);
            this.Controls.Add(this.cbxServiceSubType);
            this.Controls.Add(this.cbxServiceType);
            this.Controls.Add(this.cbxStatus);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TicketEditWindow";
            this.Text = "SDK Demo (Edit Ticket)";
            this.Load += new System.EventHandler(this.TicketEditWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbxStatus;
        private System.Windows.Forms.ComboBox cbxServiceType;
        private System.Windows.Forms.ComboBox cbxServiceSubType;
        private System.Windows.Forms.ComboBox cbxSource;
        private System.Windows.Forms.ComboBox cbxPriority;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkResolution;
        private System.Windows.Forms.CheckBox chkInternal;
        private System.Windows.Forms.CheckBox chkDetail;
        private System.Windows.Forms.Button btnUpdateTicket;
        private System.Windows.Forms.Label lblLoading;

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void cbxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxServiceSubType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cbxPriority_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSummary_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNote_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void chkResolution_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkInternal_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkDetail_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbxSource_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblLoading_Click(object sender, EventArgs e)
        {

        }
    }
}
