using System;
using System.Windows.Forms;

namespace SdkDemoForItNation2013
{
    public partial class FirstWindow : Form
    {
        //main const
        public FirstWindow()
        {
            InitializeComponent();
            CenterToScreen();
            LoadApplicationSettings();
            txtCompany.Focus();
        }
        
        //btnStartAgent click event (saves user input in settings config)
        private void btnStartAgent_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSite.Text) ||
                    string.IsNullOrEmpty(txtCompany.Text) ||
                    string.IsNullOrEmpty(txtUsername.Text) ||
                    string.IsNullOrEmpty(txtPassword.Text))
                    MessageBox.Show(@"All fields are required.", @"Missing values", MessageBoxButtons.OK);

                SaveApplicationSettings();
                new NotificationAgent(txtSite.Text, txtCompany.Text, txtUsername.Text, txtPassword.Text, rbtManagedDevice.Checked);
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error starting agent: \n" + ex.Message, @"Could not start agent");
            }
        }

        //loads user input from settings config
        private void LoadApplicationSettings()
        {
            try
            {
                var config = Properties.Settings.Default;
                txtSite.Text = config.Site;
                txtCompany.Text = config.Company;
                txtUsername.Text = config.IntegratorUsername;
                txtPassword.Text = config.IntegratorPassword;
                rbtManagedDevice.Checked = config.UseManagedDeviceApi;
                rbtConfiguration.Checked = config.UseConfigurationApi;
            }
            catch (Exception)
            {
                MessageBox.Show(@"Could not load application settings.");
            }
        }

        //saves user input to settings config
        private void SaveApplicationSettings()
        {
            try
            {
                var config = Properties.Settings.Default;
                config.Site = txtSite.Text;
                config.Company = txtCompany.Text;
                config.IntegratorUsername = txtUsername.Text;
                config.IntegratorPassword = txtPassword.Text;
                config.UseManagedDeviceApi = rbtManagedDevice.Checked;
                config.UseConfigurationApi = rbtConfiguration.Checked;
                config.Save();
            }
            catch (Exception)
            {
                MessageBox.Show(@"Could not save application settings.");
            }
        }
    }
}
