using System.Windows;
using System.Windows.Interactivity;

namespace DiskVolumesExplorer.Client.Components
{
    internal class CancelCloseWindowBehavior : Behavior<Window>
    {
        public static readonly DependencyProperty CancelCloseProperty =
          DependencyProperty.Register("CancelClose", typeof(bool),
            typeof(CancelCloseWindowBehavior), new FrameworkPropertyMetadata(false));

        public bool CancelClose
        {
            get { return (bool)GetValue(CancelCloseProperty); }
            set { SetValue(CancelCloseProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.Closing += (sender, args) => args.Cancel = CancelClose;
        }
    }
}
