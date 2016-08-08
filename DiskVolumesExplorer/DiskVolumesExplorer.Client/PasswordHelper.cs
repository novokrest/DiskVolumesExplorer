using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace DiskVolumesExplorer.Client
{
    public static class PasswordHelper
    {
        public static DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached(
            "Password",
            typeof(SecureString),
            typeof(PasswordHelper),
            new FrameworkPropertyMetadata(null));

        public static DependencyProperty AttachProperty = DependencyProperty.RegisterAttached(
            "Attach",
            typeof(bool),
            typeof(PasswordHelper),
            new PropertyMetadata(false, OnAttachPropertyChanged));

        public static void SetAttach(DependencyObject d, object value)
        {
            d.SetValue(AttachProperty, value);
        }

        public static bool GetAttach(DependencyObject d)
        {
            return (bool)d.GetValue(AttachProperty);
        }

        public static void SetPassword(DependencyObject d, object value)
        {
            d.SetValue(PasswordProperty, value);
        }

        public static SecureString GetPassword(DependencyObject d)
        {
            return (SecureString)d.GetValue(PasswordProperty);
        }

        private static void OnAttachPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;

            if (passwordBox == null) return;

            if ((bool)e.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }

            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs args)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                SetPassword(passwordBox, passwordBox.SecurePassword);
            }
        }
    }
}
