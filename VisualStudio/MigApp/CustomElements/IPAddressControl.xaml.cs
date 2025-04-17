using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MigApp.CustomElements
{
    public partial class IPAddressControl : UserControl
    {
        public IPAddressControl()
        {
            InitializeComponent();
        }

        #region Свойства привязки
        public static readonly DependencyProperty IPProperty =
            DependencyProperty.Register(nameof(IP), typeof(string), typeof(IPAddressControl),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string IP
        {
            get => (string)GetValue(IPProperty);
            set
            {
                SetValue(IPProperty, value);
                SetIPFromText(value);
            }
        }
        #endregion

        #region Обработка событий
        private void SetIPFromText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                string[] parts = text.Split('.');
                ip1.Text = parts.Length > 0 ? parts[0] : "";
                ip2.Text = parts.Length > 1 ? parts[1] : "";
                ip3.Text = parts.Length > 2 ? parts[2] : "";
                ip4.Text = parts.Length > 3 ? parts[3] : "";
            }
        }

        private void SetTextFromIP(object sender, RoutedEventArgs e)
        {
            IP = $"{ip1.Text}.{ip2.Text}.{ip3.Text}.{ip4.Text}";
        }

        private void IPcheck(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (textBox.Text.Length == 3)
                    MoveFocusToNextTextBox(GetTextBoxIndex(textBox));
                try
                {
                    if (Convert.ToInt32(textBox.Text) > 255)
                    {
                        textBox.Text = "255";
                        textBox.CaretIndex = textBox.Text.Length;
                    }
                }
                catch { }
            }
        }

        private void IPGotFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            e.Handled = true;
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
                textBox.SelectAll();
        }

        private void NextIP(object sender, KeyEventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;
            int currentIndex = GetTextBoxIndex(currentTextBox);
            switch (e.Key)
            {
                case Key.Right:
                    {
                        if (currentTextBox.CaretIndex == currentTextBox.Text.Length)
                        {
                            e.Handled = true;
                            MoveFocusToNextTextBox(currentIndex);
                        }
                        break;
                    }
                case Key.OemPeriod:
                case Key.Enter:
                case Key.Space:
                    {
                        e.Handled = true;
                        MoveFocusToNextTextBox(currentIndex);
                        break;
                    }
                case Key.Left:
                    {
                        if (currentTextBox.CaretIndex == 0)
                        {
                            e.Handled = true;
                            MoveFocusToPreviousTextBox(currentIndex);
                        }
                        break;
                    }
                case Key.Back:
                    {
                        if (currentTextBox.Text.Length == 0)
                        {
                            e.Handled = true;
                            MoveFocusToPreviousTextBox(currentIndex);
                        }
                        break;
                    }

            }
        }

        private int GetTextBoxIndex(TextBox textBox)
        {
            if (textBox == ip1) return 0;
            if (textBox == ip2) return 1;
            if (textBox == ip3) return 2;
            if (textBox == ip4) return 3;
            return -1;
        }

        private void MoveFocusToNextTextBox(int currentIndex)
        {
            if (currentIndex >= 0 && currentIndex < 3)
            {
                switch (currentIndex)
                {
                    case 0: ip2.Focus(); break;
                    case 1: ip3.Focus(); break;
                    case 2: ip4.Focus(); break;
                }
            }
        }

        private void MoveFocusToPreviousTextBox(int currentIndex)
        {
            if (currentIndex > 0)
            {
                switch (currentIndex)
                {
                    case 1: ip1.Focus(); break;
                    case 2: ip2.Focus(); break;
                    case 3: ip3.Focus(); break;
                }
            }
        }

        private void NumOnlyIP(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsDigit(e.Text, 0))
                {
                    e.Handled = true;
                }
            }
            catch { }
        }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            string toSet = IP;
            SetIPFromText(toSet);
        }
        #endregion


    }
}
