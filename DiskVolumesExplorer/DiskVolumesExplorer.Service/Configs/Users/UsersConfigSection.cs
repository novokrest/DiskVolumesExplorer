using System.Configuration;

namespace DiskVolumesExplorer.Service.Configs.Users
{
    internal class UsersConfigSection : ConfigurationSection
    {
        public const string SectionName = "Users";

        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public UsersCollection Instances
        {
            get { return (UsersCollection)this[""]; }
            set { this[""] = value; }
        }
    }
}
