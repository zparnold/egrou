using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SdkDemoForItNation2013
{
    public class NotificationAgent
    {
        public NotifyIcon NotifyIcon;
        public ContextMenu NotificationMenu;
        public BackgroundWorker BackgroundWorker = new BackgroundWorker();

        //main const
        public NotificationAgent(string site, string company, string username, string password, bool useManagedDeviceApi)
        {
            NotificationMenu = new ContextMenu(InitializeMenu());
            StartHelper(site, company, username, password, useManagedDeviceApi);
            StartNotificationIcon();
            NotifyIcon.ContextMenu = NotificationMenu;
            NotifyIcon.BalloonTipClicked += NotifyIconOnBalloonTipClicked;
            BackgroundWorker.DoWork += backgroundWorker_DoWork;
            BackgroundWorker.RunWorkerCompleted += backgroundWorker_Completed;

            //creates or updates a config for the machine in ConnectWise
            BackgroundWorker.RunWorkerAsync("UpdateConfiguration");
        }

        //pops up first notification ballon
        private void StartNotificationIcon()
        {

            NotifyIcon = new NotifyIcon
                {
                    Visible = true,
                    Icon = new Icon("Owl-Icon.ico"),
                    BalloonTipTitle = @"SDK Demo Agent",
                    BalloonTipText = @"Demo Agent monitoring this machine is running!"
                };
            NotifyIcon.ShowBalloonTip(5);
        }

        //click event for notification ballon
        private void NotifyIconOnBalloonTipClicked(object sender, EventArgs eventArgs)
        {
            switch (NotifyIcon.BalloonTipTitle)
            {
                case "Ticket Created":
                    EditLastTicket(this, null);
                    break;
            }
        }

        //to edit the ticket that was last created
        private void EditLastTicket(object sender, EventArgs eventArgs)
        {
            BackgroundWorker.RunWorkerAsync("UpdateTicket");
        }

        //worker completed event
        private void backgroundWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            switch ((string)e.Result)
            {
                case "UpdateConfiguration":
                    ShowBallonTip("Configuration Updated", "This machine was updated successfully in ConnectWise!");
                    break;
                case "CreateLastError":
                    ShowBallonTip("Ticket Created", "Click here to edit ticket!");
                    break;
                case "UpdateTicket":
                    ShowBallonTip("Ticket Updated", "Ticket was successfully updated!");
                    break;
            }
        }

        //worker dowork event
        private async void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var action = (string) e.Argument;
                switch (action)
                {
                    case "UpdateConfiguration":
                        e.Result = action;
                        await Helper.CreateConfig();
                        break;
                    case "CreateLastError":
                        ShowBallonTip("Creating Ticket", "Ticket is being created in ConnectWise, please wait.");
                        e.Result = action;
                        await Helper.CreateServiceTicketForLastErrorMessage();
                        break;
                    case "UpdateTicket":
                        e.Result = action;
                        await Helper.EditLastTicketCreated();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //shows the tip ballon
        public void ShowBallonTip(string title, string message)
        {
            NotifyIcon.BalloonTipTitle = title;
            NotifyIcon.BalloonTipText = message;
            NotifyIcon.ShowBalloonTip(5);
        }

        //starts a new context menu for notification icon
        private MenuItem[] InitializeMenu()
        {
            return new []
                {
                    new MenuItem("Create exception", CreateException),
                    new MenuItem("Create a ticket in CW for last error log", CreateTicketFromLastError), 
                    new MenuItem("Edit last ticket created", EditLastTicket), 
                    new MenuItem("About", ShowAboutInfo), 
                    new MenuItem("Exit", ExitApplication)
                };
        }

        //creates a service ticket from last error message
        private void CreateTicketFromLastError(object sender, EventArgs e)
        {
            BackgroundWorker.RunWorkerAsync("CreateLastError");
        }

        //inserts a new error in windows event logs
        private void CreateException(object sender, EventArgs e)
        {
            try
            {
                throw new ApplicationException("Exception occurred. Please follow up. (Demo)");
            }
            catch (Exception ex)
            {
                var log = new EventLog {Log = "Application", Source = "SdkDemoForItNation2013"};
                log.WriteEntry(ex.Message, EventLogEntryType.Error);
                MessageBox.Show(ex.ToString(), @"Exception occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //exits the application
        private void ExitApplication(object sender, EventArgs e)
        {
            NotifyIcon.Dispose();
            Application.Exit();
        }

        //shows the about information for the demo agent
        private void ShowAboutInfo(object sender, EventArgs e)
        {
            MessageBox.Show(@"This a demo for IT Nation 2013 using our new ConnectWise .Net SDK",
                              @"IT Nation SDK Demo Agent", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //starts static helper class
        public void StartHelper(string site, string company, string username, string password, bool useManagedDeviceApi)
        {
            Helper.StartHelper(site, company, username, password, useManagedDeviceApi);
        }
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool firstTimeRunning;

            //checks if agent is already running
            using (new Mutex(true, "SdkDemoForItNation2013", out firstTimeRunning))
            {
                if (firstTimeRunning)
                {
                    Application.Run(new FirstWindow());
                }
                else
                    MessageBox.Show(@"Agent is already running, it is the ConnectWise Owl logo in the notification icon tray.",
                                @"Agent Is Currently Running", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
