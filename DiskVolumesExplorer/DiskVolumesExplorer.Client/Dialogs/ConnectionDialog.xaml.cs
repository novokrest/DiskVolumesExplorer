using System.Windows;

namespace DiskVolumesExplorer.Client.Dialogs
{
    /// <summary>
    /// Interaction logic for ConnectionDialog.xaml
    /// </summary>
    public partial class ConnectionDialog : Window
    {
        public ConnectionDialog(Window owner)
        {
            InitializeComponent();
            Owner = owner;
        }
    }
}
