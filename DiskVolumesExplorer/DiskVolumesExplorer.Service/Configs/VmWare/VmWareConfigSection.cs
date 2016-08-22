using DiskVolumesExplorer.Service.Core.Configs;
using System.Configuration;

namespace DiskVolumesExplorer.Service.Configs.VmWare
{
    public class VmWareConfigSection : ConfigurationSection, IVmWareConnectionConfig
    {
        public const string SectionName = "VmWareConfig";

        private const string ServerProperty = "Server";
        private const string UserProperty = "User";
        private const string PasswordProperty = "Password";
        private const string ThumbPrintProperty = "ThumbPrint";

        [ConfigurationProperty(ServerProperty, IsRequired = true)]
        public string Server => (string) this[ServerProperty];

        [ConfigurationProperty(UserProperty, IsRequired = true)]
        public string User => (string) this[UserProperty];

        [ConfigurationProperty(PasswordProperty, IsRequired = true)]
        public string Password => (string) this[PasswordProperty];

        [ConfigurationProperty(ThumbPrintProperty, DefaultValue = "", IsRequired = true)]
        public string ThumbPrint => (string)this[ThumbPrintProperty];
    }
}
