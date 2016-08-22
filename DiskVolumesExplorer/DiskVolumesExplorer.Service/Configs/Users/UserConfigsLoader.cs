using System.Configuration;
using System.Linq;

namespace DiskVolumesExplorer.Service.Configs.Users
{
    internal class UserConfigsLoader
    {
        public static IUserConfig[] LoadUserConfigs()
        {
            var usersConfigSection = (UsersConfigSection)ConfigurationManager.GetSection(UsersConfigSection.SectionName);
            var usersCollection = usersConfigSection.Instances;

            return ConvertUsersCollectionToUserConfigsArray(usersCollection);
        }

        private static IUserConfig[] ConvertUsersCollectionToUserConfigsArray(UsersCollection usersCollection)
        {
            return usersCollection.Cast<UserElement>().ToArray();
        }
    }
}
