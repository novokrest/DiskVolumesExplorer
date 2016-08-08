using DiskVolumesExplorer.Client.Extensions;
using Prism.Mvvm;
using System;
using System.Security;
using System.Windows;

namespace DiskVolumesExplorer.Client
{
    public class ConnectionDialogViewModel : BindableBase
    {
        private string _serverAddress;
        private string _userName;
        private SecureString _password;

        public string ServerAddress
        {
            get
            {
                return _serverAddress;
            }
            set
            {
                if (!string.Equals(_serverAddress, value, StringComparison.Ordinal))
                {
                    _serverAddress = value;
                    OnPropertyChanged();
                }
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (!string.Equals(_userName, value, StringComparison.Ordinal))
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }

        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                MessageBox.Show(_password.ConvertToString());
            }
        }
    }
}
