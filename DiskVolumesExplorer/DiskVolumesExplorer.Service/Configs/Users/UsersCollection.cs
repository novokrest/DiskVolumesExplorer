using System;
using System.Configuration;

namespace DiskVolumesExplorer.Service.Configs.Users
{
    [ConfigurationCollection(typeof(UserElement), AddItemName = "User")]
    internal class UsersCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new UserElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UserElement)element).Name;
        }

        public UserElement this[int index]
        {
            get { return (UserElement)BaseGet(index); }
        }
    }
}
