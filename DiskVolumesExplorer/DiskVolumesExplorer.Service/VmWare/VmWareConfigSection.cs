using System.Configuration;

namespace DiskVolumesExplorer.Service.VmWare
{
    public class VmWareConfigSection : ConfigurationSection, IVmWareConfig
    {
        public const string SectionName = "VmWareConfig";

        private const string UrlProperty = "Url";
        private const string UserProperty = "User";
        private const string PasswordProperty = "Password";

        [ConfigurationProperty(UrlProperty, DefaultValue = "", IsRequired = true)]
        public string Url => (string) this[UrlProperty];

        [ConfigurationProperty(UserProperty, DefaultValue = "", IsRequired = true)]
        public string User => (string) this[UserProperty];

        [ConfigurationProperty(PasswordProperty, DefaultValue = "", IsRequired = true)]
        public string Password => (string) this[PasswordProperty];
    }

    internal static class VmWareConfigLoader
    {
        public static IVmWareConfig LoadConfig()
        {
            return (VmWareConfigSection) ConfigurationManager.GetSection(VmWareConfigSection.SectionName);
        }
    }
}
