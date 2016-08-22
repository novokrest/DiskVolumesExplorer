using System.Configuration;

namespace DiskVolumesExplorer.Service.Configs.Users
{
    internal class UserElement : ConfigurationElement, IUserConfig
    {
        private const string UserProperty = "name";
        private const string PasswordProperty = "password";

        [ConfigurationProperty(UserProperty, IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string) base[UserProperty];
            }
            set
            {
                base[UserProperty] = value;
            }
        }

        [ConfigurationProperty(PasswordProperty, IsKey = false, IsRequired = true)]
        public string Password
        {
            get
            {
                return (string)base[PasswordProperty];
            }
            set
            {
                base[PasswordProperty] = value;
            }
        }
    }
}
