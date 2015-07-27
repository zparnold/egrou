using System;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.VisualBasic.Devices;

namespace SdkDemoForItNation2013
{
    public static class LocalMachineInfo
    {
        //uses VisualBasic to get most of the computer information
        public static ComputerInfo ComputerInfo = new ComputerInfo();

        //gets mac addess
        public static string GetMacAddress()
        {
            try
            {
                var mac = string.Empty;
                var mc = new ManagementClass("Win32_NetworkAdapter");
                ManagementObjectCollection moCol = mc.GetInstances();
                foreach (ManagementObject mo in moCol)
                    if (mo != null)
                    {
                        if (mo["MacAddress"] != null)
                        {
                            mac = mo["MACAddress"].ToString();
                            if (mac != string.Empty)
                                break;
                        }
                    }
                return mac;
            }
            catch
            {
                return string.Empty;
            }
        }

        //gets ip address
        public static string GetIpAddress()
        {
            try
            {
                IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
                return ipEntry.AddressList[ipEntry.AddressList.Length - 1].ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        //gets computer name
        public static string GetName()
        {
            return Environment.MachineName;
        }

        //gets default gateway
        public static string GetDefaultGateway()
        {
            try
            {
                var card = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault();
                if (card == null) return null;
                var address = card.GetIPProperties().GatewayAddresses.FirstOrDefault();
                if (address != null) return address.Address.ToString();
                return "";
            }
            catch
            {
                return string.Empty;
            }
        }

        //gets cpu speed
        public static string GetCpuSpeed()
        {
            try
            {
                var mo = new ManagementObject("Win32_Processor.DeviceID='CPU0'");
                var sp = (uint) (mo["MaxClockSpeed"]);
                mo.Dispose();
                return sp.ToString(CultureInfo.InvariantCulture);
            }
            catch
            {
                return string.Empty;
            }
        }

        //gets user logged in
        public static string GetUserLoggedIn()
        {
            return Environment.UserDomainName + "\\" + Environment.UserName;
        }

        //gets bios serial number
        internal static string GetSerialNumber()
        {
            try
            {
                var mos = new ManagementObjectSearcher("Select * From Win32_BIOS");
                foreach (ManagementObject getserial in mos.Get())
                {
                    return getserial["SerialNumber"].ToString();
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        //gets bios model number
        internal static string GetModelNumber()
        {
            try
            {
                var mos = new ManagementObjectSearcher("Select * From Win32_BIOS");
                foreach (ManagementObject getserial in mos.Get())
                {
                    return getserial["Version"].ToString();
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
