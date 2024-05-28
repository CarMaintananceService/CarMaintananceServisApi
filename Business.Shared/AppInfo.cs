using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Shared
{
    public class AppInfo
    {
        public static string VersionNumber = "202309";
        public static string VersionNumberMinor = "6";


        public string CompanyName { get; set; }
        public string HostUrl { get; set; }
		public string ClientUrl { get; set; }
		public string DateFormat { get; set; }
        public bool IsConnectionStringEncrypted { get; set; }
        public bool IsMailSendingActive { get; set; }
        public int MailSendingStartDelay { get; set; }
        public int MailSendingEachDelay { get; set; }
    
    }
}
