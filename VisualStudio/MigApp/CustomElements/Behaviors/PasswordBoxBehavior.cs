using Microsoft.Xaml.Behaviors;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MigApp.CustomElements.Behaviors
{
    public class PasswordBoxBehavior : Behavior<PasswordBox>
    {
        private bool _isUpdating;

        public SecureString Password
        {
            get => (SecureString)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(
                nameof(Password),
                typeof(SecureString),
                typeof(PasswordBoxBehavior),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnPasswordPropertyChanged));

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += OnPasswordChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged -= OnPasswordChanged;
            base.OnDetaching();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_isUpdating) return;

            _isUpdating = true;
            try
            {
                var newPassword = new SecureString();
                foreach (char c in AssociatedObject.Password)
                {
                    newPassword.AppendChar(c);
                }
                newPassword.MakeReadOnly();

                Password = newPassword;
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = (PasswordBoxBehavior)d;
            if (behavior._isUpdating) return;

            behavior._isUpdating = true;
            try
            {
                behavior.AssociatedObject.Password = "";

                if (e.NewValue is SecureString secureString && secureString.Length > 0)
                {
                    behavior.AssociatedObject.Password = SecureStringToString(secureString);
                }
            }
            finally
            {
                behavior._isUpdating = false;
            }
        }

        private static string SecureStringToString(SecureString value)
        {
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.SecureStringToBSTR(value);
                return Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(ptr);
            }
        }
    }
}