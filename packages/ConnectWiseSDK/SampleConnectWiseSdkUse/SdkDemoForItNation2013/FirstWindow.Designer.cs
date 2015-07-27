namespace SdkDemoForItNation2013
{
    partial class FirstWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirstWindow));
            this.btnStartAgent = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtSite = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rbtManagedDevice = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtConfiguration = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartAgent
            // 
            this.btnStartAgent.Location = new System.Drawing.Point(135, 291);
            this.btnStartAgent.Name = "btnStartAgent";
            this.btnStartAgent.Size = new System.Drawing.Size(75, 23);
            this.btnStartAgent.TabIndex = 8;
            this.btnStartAgent.Text = "Start Agent";
            this.btnStartAgent.UseVisualStyleBackColor = true;
            this.btnStartAgent.Click += new System.EventHandler(this.btnStartAgent_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Site:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Company:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Integrator Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Integrator Password:";
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(144, 98);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(158, 20);
            this.txtCompany.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(144, 150);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(158, 20);
            this.txtPassword.TabIndex = 4;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(144, 124);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(158, 20);
            this.txtUsername.TabIndex = 3;
            // 
            // txtSite
            // 
            this.txtSite.Location = new System.Drawing.Point(144, 72);
            this.txtSite.Name = "txtSite";
            this.txtSite.Size = new System.Drawing.Size(158, 20);
            this.txtSite.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(67, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(209, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "ConnectWise Demo Agent Settings:";
            // 
            // rbtManagedDevice
            // 
            this.rbtManagedDevice.AutoSize = true;
            this.rbtManagedDevice.Location = new System.Drawing.Point(23, 19);
            this.rbtManagedDevice.Name = "rbtManagedDevice";
            this.rbtManagedDevice.Size = new System.Drawing.Size(125, 17);
            this.rbtManagedDevice.TabIndex = 6;
            this.rbtManagedDevice.TabStop = true;
            this.rbtManagedDevice.Text = "Managed Device Api";
            this.rbtManagedDevice.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtConfiguration);
            this.groupBox1.Controls.Add(this.rbtManagedDevice);
            this.groupBox1.Location = new System.Drawing.Point(83, 193);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 74);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "What To Use:";
            // 
            // rbtConfiguration
            // 
            this.rbtConfiguration.AutoSize = true;
            this.rbtConfiguration.Location = new System.Drawing.Point(23, 42);
            this.rbtConfiguration.Name = "rbtConfiguration";
            this.rbtConfiguration.Size = new System.Drawing.Size(105, 17);
            this.rbtConfiguration.TabIndex = 7;
            this.rbtConfiguration.TabStop = true;
            this.rbtConfiguration.Text = "Configuration Api";
            this.rbtConfiguration.UseVisualStyleBackColor = true;
            // 
            // FirstWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(227)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(344, 347);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSite);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtCompany);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStartAgent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FirstWindow";
            this.Text = "Demo Agent Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartAgent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtSite;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbtManagedDevice;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtConfiguration;
    }
}