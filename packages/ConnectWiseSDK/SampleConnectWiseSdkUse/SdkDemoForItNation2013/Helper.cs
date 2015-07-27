using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConnectWiseSDK;

namespace SdkDemoForItNation2013
{
    public static class Helper
    {
        private static ConfigurationApi ConfigurationApi;
        private static CompanyApi CompanyApi;
        private static ManagedDeviceApi ManagedDeviceApi;
        private static ReportingApi ReportingApi;
        private static ServiceTicketApi ServiceTicketApi;
        private const string ConfigurationTypeName = "Desktop";
        private const string CompanyName = "It Nation Demo Company";
        private const string CompanyIdentifier = "ItNationDemoCompany";
        
        //for managed device api
        private const string ManagedId = "ItNationDemoCompany";
        private const string ManagedSolutionName = "DemoIntegration";
        private const string ManagedLevelType = "DemoDesktop";
        private const string ManagedLevelName = "DemoDesktopLevel";
        private static string DeviceId = DateTime.Now.ToString("yyyyMMdd");

        public static bool UseManagedDeviceApi = false;
        public static ConfigurationType ConfigurationType;
        public static Configuration Configuration;
        public static ServiceTicket ServiceTicket;
        private static TicketEditWindow TicketUpdateValues;

        //starts the helper and sets api consts
        public static void StartHelper(string site, string company, string username, string password, bool useManagedDeviceApi)
        {
            UseManagedDeviceApi = useManagedDeviceApi;
            ConfigurationApi = new ConfigurationApi(site, company, username, password, "ItNationDemo2013");
            CompanyApi = new CompanyApi(site, company, username, password, "ItNationDemo2013");
            ManagedDeviceApi = new ManagedDeviceApi(site, company, username, password, "ItNationDemo2013");
            ReportingApi = new ReportingApi(site, company, username, password, "ItNationDemo2013");
            ServiceTicketApi = new ServiceTicketApi(site, company, username, password, "ItNationDemo2013");
        }

        //creates a config with the api selected in settings window
        public static async Task CreateConfig()
        {
            if(UseManagedDeviceApi)
                await CreateConfigViaManagedDeviceApi();
            else
                await CreateConfigViaConfigurationApi();
        }

        //creates a config using ConfigurationApi
        public static async Task CreateConfigViaConfigurationApi()
        {
            string compName = LocalMachineInfo.GetName();

            //setting Configuration object
            Configuration = new Configuration
                {
                    Id = 0,
                    ConfigurationTypeId = await getOrCreateConfigurationType(),
                    CompanyId = await getOrCreateCompany(),
                    ConfigurationName = compName,
                    IPAddress = LocalMachineInfo.GetIpAddress(),
                    MacAddress = LocalMachineInfo.GetMacAddress(),
                    OSType = LocalMachineInfo.ComputerInfo.OSFullName,
                    OSInfo = LocalMachineInfo.ComputerInfo.OSVersion,
                    DefaultGateway = LocalMachineInfo.GetDefaultGateway(),
                    CPUSpeed = LocalMachineInfo.GetCpuSpeed(),
                    RAM = LocalMachineInfo.ComputerInfo.TotalPhysicalMemory.ToString(CultureInfo.InvariantCulture),
                    LastLoginName = LocalMachineInfo.GetUserLoggedIn(),
                    SerialNumber = LocalMachineInfo.GetSerialNumber(),
                    ModelNumber = LocalMachineInfo.GetModelNumber(),
                    IsActive = true,
                    ConfigurationQuestions = new List<ConfigurationQuestion>
                        {
                            new ConfigurationQuestion
                                {
                                    QuestionId = ConfigurationType.ConfigurationTypeQuestions[0].Id,
                                    Answer = "True"
                                }
                        }
                };

            //check to see if there is already a config in CW with the machine's name
            string conditions = string.Format("ConfigurationName like '{0}'", compName);

            var configsFound = await ConfigurationApi.FindConfigurationsAsync(conditions, string.Empty, 3, null, null, new List<string>{"Id"});

            //if there is already a config, update the existing
            if (configsFound != null && configsFound.Count > 0)
                Configuration.Id = configsFound[0].Id;

            try
            {
                //adds or update the config in ConnectWise
                Configuration = await ConfigurationApi.AddOrUpdateConfigurationAsync(Configuration);
            }
            catch (MissingOrInvalidDataException moide)
            {
                MessageBox.Show(moide.Message, @"Invalid data in Configuration object", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (RecordNotFoundException rnfe)
            {
                MessageBox.Show(rnfe.Message, @"Record not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (RecordNotOwnedException rnoe)
            {
                MessageBox.Show(rnoe.Message, @"Integrator has no access to this record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //creates management solution for a company
        //creates config using ManagedDeviceApi and associates it with the management solution created
        public static async Task CreateConfigViaManagedDeviceApi()
        {
            int companyId = await getOrCreateCompany();

            //creates or updates company management solution
            var managementSolution = new ManagementSolution{CompanyId = companyId, ManagedIdentifier = ManagedId, DeviceTypes = "A", Name = ManagedSolutionName};
            await ManagedDeviceApi.UpdateManagementSolutionAsync(managementSolution);

            //setting new list of ManagedDevice
            var managedDevice = new List<ManagedDevice>
                {
                    new ManagedDevice
                        {
                            ManagedIdentifier = ManagedId,
                            CompanyId = companyId,
                            //this level gets created if it does not already exist (uses ConfigTypeId of Device, and uses product and agreement from Billing)
                            Type = ManagedLevelType,
                            Level = ManagedLevelName,
                            Device = new DeviceInfo
                                {
                                    ConfigurationTypeId = await getOrCreateConfigurationType(),
                                    DeviceIdentifier = DeviceId,
                                    DeviceName = LocalMachineInfo.GetName(),
                                    IPAddress = LocalMachineInfo.GetIpAddress(),
                                    MacAddress = LocalMachineInfo.GetMacAddress(),
                                    OSType = LocalMachineInfo.ComputerInfo.OSFullName,
                                    OSInfo = LocalMachineInfo.ComputerInfo.OSVersion,
                                    DefaultGateway = LocalMachineInfo.GetDefaultGateway(),
                                    CPUSpeed = LocalMachineInfo.GetCpuSpeed(),
                                    RAM = LocalMachineInfo.ComputerInfo.TotalPhysicalMemory.ToString(CultureInfo.InvariantCulture),
                                    LastLoginName = LocalMachineInfo.GetUserLoggedIn(),
                                    SerialNumber = LocalMachineInfo.GetSerialNumber(),
                                    ModelNumber = LocalMachineInfo.GetModelNumber(),
                                    Answers = new List<ConfigurationAnswer>
                                        {
                                            new ConfigurationAnswer
                                                {
                                                    QuestionId = ConfigurationType.ConfigurationTypeQuestions[0].Id,
                                                    Value = "True"
                                                }
                                        },
                                    IsActive = true
                                }
                        }
                };

            //if method returns any error message, display in message box
            string errorMessage = await ManagedDeviceApi.UpdateManagedDevicesAsync(ManagedSolutionName, managedDevice);
            if (!string.IsNullOrEmpty(errorMessage))
                MessageBox.Show(errorMessage, @"Error Message From UpdateManagedDevices Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //creates a service ticket in CW from the last error message in windows event logs
        public static async Task CreateServiceTicketForLastErrorMessage()
        {
            try
            {
                string problemDescription = string.Empty;
                string ticketSummary = string.Empty;

                //reads the last entry log and creates problem description
                var log = new EventLog {Log = "Application"};
                foreach (var entry in log.Entries.Cast<EventLogEntry>()
                           .Where(x => x.EntryType == EventLogEntryType.Error)
                           .OrderByDescending(x => x.TimeWritten))
                {
                    ticketSummary = entry.Message;
                    problemDescription = "Message: " + entry.Message;
                    problemDescription += "\nSource: " + entry.Source;
                    problemDescription += "\nLevel: " + entry.EntryType;
                    problemDescription += "\nDateEntered: " + entry.TimeWritten;
                    problemDescription += "\nEventId: " + entry.InstanceId;
                    problemDescription += "\nCategory: " + entry.Category;
                    break;
                }

                //setting new ServiceTicket
                ServiceTicket = new ServiceTicket
                    {
                        CompanyId = await getOrCreateCompany(),
                        Summary = ticketSummary,
                        DetailDescription = problemDescription
                    };

                //if we used ManagedDeviceApi, attaching config to ticket via DeviceId
                if(UseManagedDeviceApi)
                    ServiceTicket.Configurations = new List<TicketConfiguration>
                        {
                            new TicketConfiguration
                                {
                                    DeviceIdentifier = DeviceId
                                }
                        };
                //if we used ConfigurationApi, attaching config to ticket via Configuration.Id
                else
                    ServiceTicket.Configurations = new List<TicketConfiguration>
                        {
                            new TicketConfiguration
                                {
                                    Id = Configuration.Id
                                }
                        };

                //adds or updates service ticket (with the machine attached)
                ServiceTicket = await ServiceTicketApi.AddOrUpdateServiceTicketViaCompanyIdentifierAsync(CompanyIdentifier, ServiceTicket);
            }
            //catch block for RecordNotFoundException
            catch (RecordNotFoundException rnf)
            {
                MessageBox.Show(rnf.Message, @"Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //catch any other exception
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //pops up TicketEditWindow to edit the ticket that was just created
        public static async Task EditLastTicketCreated()
        {
            if (ServiceTicket == null || ServiceTicket.Id < 1)
            {
                MessageBox.Show(@"Please create a ticket first", @"Create Ticket First", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(TicketUpdateValues == null)
                TicketUpdateValues = new TicketEditWindow();
            TicketUpdateValues.ShowDialog();
            ServiceTicket.Summary = TicketUpdateValues.Summary;
            ServiceTicket.StatusName = TicketUpdateValues.StatusName;
            ServiceTicket.ServiceType = TicketUpdateValues.ServiceType;
            ServiceTicket.ServiceSubType = TicketUpdateValues.ServiceSubType;
            ServiceTicket.Source = TicketUpdateValues.Source;
            ServiceTicket.Priority = TicketUpdateValues.Priority;

            try
            {
                //updates the ticket based on user input
                ServiceTicket = await ServiceTicketApi.AddOrUpdateServiceTicketViaCompanyIdentifierAsync(CompanyIdentifier, ServiceTicket);


                //setting ticket note entered
                var ticketNote = new TicketNote
                    {
                        NoteText = TicketUpdateValues.Note,
                        IsPartOfDetailDescription = TicketUpdateValues.IsDetail,
                        IsPartOfInternalAnalysis = TicketUpdateValues.IsInternal,
                        IsPartOfResolution = TicketUpdateValues.IsResolution
                    };

                //adds ticket note to the last ticket created
                ServiceTicketApi.AddOrUpdateTicketNote(ticketNote, ServiceTicket.Id);
            }
            catch (RecordNotFoundException rnfe)
            {
                MessageBox.Show(rnfe.Message, @"Record not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //loads all of the available ticket status for the ticket (two ways to do this)
        public static async Task<List<string>> LoadTicketStatuses()
        {
            try
            {
                var statusList = await ServiceTicketApi.GetServiceStatusesAsync(ServiceTicket.Id);
                return statusList;
            }
            catch (RecordNotFoundException rnfe)
            {
                MessageBox.Show(rnfe.Message, @"Record not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return new List<string>();
            }

            /*
             * another way to do this with ReportingApi
             * 
            
            string conditions = string.Format("SR_Board_RecID = {0}", ServiceTicket.BoardID);
            var results = ReportingApi.RunReportQuery("ServiceStatus", conditions, string.Empty, null, null);
            var statusList = (from resultRow in results
                              from fieldValue in resultRow.Values
                              where fieldValue.Name.Equals("Service_Status_Desc")
                              select fieldValue.Value).ToList();
             */
        }

        //loads all of the available ticket service types
        public static async Task<Dictionary<int, string>> LoadServiceTypes()
        {
            //getting service types for the service board of the ticket
            string conditions = string.Format("SR_Board_RecID = {0}", ServiceTicket.BoardId);
            var results = await ReportingApi.RunReportQueryAsync("ServiceType", conditions, string.Empty, null, null);
            var returnDic = new Dictionary<int, string>();
            foreach (ResultRow resultRow in results)
            {
                int recordId = 0;
                string recordName = "";
                foreach (FieldValue fieldValue in resultRow.Values)
                {
                    if (fieldValue.Name.Equals("SR_Type_RecID"))
                        recordId = Int32.Parse(fieldValue.Value);
                    if (fieldValue.Name.Equals("ServiceType"))
                        recordName = fieldValue.Value;
                }
                returnDic.Add(recordId, recordName);
            }
            return returnDic;
        }

        //loads all of the available ticket service sub types
        public static async Task<Dictionary<int, string>> LoadServiceSubTypes()
        {
            //getting service sub types for the service board of the ticket
            string conditions = string.Format("SR_Board_RecID = {0}", ServiceTicket.BoardId);
            var results = await ReportingApi.RunReportQueryAsync("ServiceSubType", conditions, string.Empty, null, null);
            var returnDic = new Dictionary<int, string>();
            foreach (ResultRow resultRow in results)
            {
                int typeId = 0;
                string recordName = "";
                foreach (FieldValue fieldValue in resultRow.Values)
                {
                    if (fieldValue.Name.Equals("SR_Type_RecID"))
                        typeId = Int32.Parse(fieldValue.Value);
                    if (fieldValue.Name.Equals("ServiceSubTypeDesc"))
                        recordName = fieldValue.Value;
                }
                returnDic.Add(typeId, recordName);
            }
            return returnDic;
        }

        //loads all of the available ticket sources
        public static async Task<List<string>> LoadServiceSources()
        {
            var results = await ReportingApi.RunReportQueryAsync("Service_Source", string.Empty, string.Empty, null, null);
            return (from resultRow in results
                    from fieldValue in resultRow.Values
                    where fieldValue.Name.Equals("Service_Source_Desc")
                    select fieldValue.Value).ToList();
        }

        //loads all of the available ticket priorities
        public static async Task<List<string>> LoadServicePriorities()
        {
            var results = await ReportingApi.RunReportQueryAsync("ServicePriority", string.Empty, string.Empty, null, null);
            return (from resultRow in results
                    from fieldValue in resultRow.Values
                    where fieldValue.Name.Equals("Service_Priority_Desc")
                    select fieldValue.Value).ToList();
        }

        //gets or create a company via CompanyApi
        private static async Task<int> getOrCreateCompany()
        {
            string conditions = string.Format("CompanyIdentifier like '{0}'", CompanyIdentifier);
            var companiesFound = await CompanyApi.FindCompaniesAsync(conditions, "", null, null, new List<string>{"Id"});
            if (companiesFound != null && companiesFound.Count > 0)
                return companiesFound[0].Id;

            var company = new Company
            {
                Id = 0,
                CompanyIdentifier = CompanyIdentifier,
                CompanyName = CompanyName,
                Status = "Active",
                DefaultAddress = new CompanyAddress
                {
                    StreetLines = new List<string> {"123 main street"},
                    City = "Tampa",
                    SiteName = "MainFromDemo",
                    State = "FL",
                    Zip = "33602"
                }
            };

            return CompanyApi.AddOrUpdateCompany(company).Id;
        }

        //gets or creates configuration type via ConfigurationApi
        private static async Task<int> getOrCreateConfigurationType()
        {
            string conditions = string.Format("Name like '{0}' AND InactiveFlag = false", ConfigurationTypeName);
            var configTypesFound = await ConfigurationApi.FindConfigurationTypesAsync(conditions, "", null, null, new List<string>{"Id"});
            if (configTypesFound != null && configTypesFound.Count > 0)
            {
                ConfigurationType = ConfigurationApi.GetConfigurationType(configTypesFound[0].Id);
                return configTypesFound[0].Id;
            }

            ConfigurationType = new ConfigurationType
            {
                Id = 0,
                Name = ConfigurationTypeName,
                InactiveFlag = false,
                ConfigurationTypeQuestions = new List<ConfigurationTypeQuestion>
                    {
                        new ConfigurationTypeQuestion
                            {
                                FieldType = FieldTypes.Checkbox,
                                Question = "Is this device important?",
                                SequenceNumber = 1,
                                RequiredFlag = false,
                                InactiveFlag = false
                            }
                    }
            };
            ConfigurationType = ConfigurationApi.AddOrUpdateConfigurationType(ConfigurationType);
            return ConfigurationType.Id;
        }
    }
}
