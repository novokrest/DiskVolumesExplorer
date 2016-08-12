using System.Configuration;

namespace AppConfigTester.VmWare
{
    public class VmWareServiceConfigSection : ConfigurationSection
    {
        public const string SectionName = "VmWareService";

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

    public static class VmWareServiceConfigLoader
    {
        public static VmWareServiceConfigSection LoadConfig()
        {
            return (VmWareServiceConfigSection) ConfigurationManager.GetSection(VmWareServiceConfigSection.SectionName);
        }
    }
}
