using MigApp.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MigApp.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для IPView.xaml
    /// </summary>
    public partial class IPView : UserControl
    {

        public IPView()
        {
            InitializeComponent();
        }

        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            if (e.Column.Header.ToString() == "IP")
            {
                //IPViewModel iPViewModel = DataContext as IPViewModel;
                // Вызов  метода  сортировки  в  ViewModel
                //iPViewModel.Table = mc.SortTableByIP("ASC", iPViewModel.Table);

                e.Handled = true;
            }
        }
        private void NumOnly(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsDigit(e.Text, 0))
                {
                    e.Handled = true;
                }
                else if (Subnet.Text == "0")
                {
                    Subnet.Text = e.Text;
                    e.Handled = true;
                    Subnet.CaretIndex = Subnet.Text.Length;
                }
            }
            catch { }

        }

        private void Subnet_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            if (int.TryParse(textBox.Text, out int subnet))
            {
                // Значение корректное
                ((IPViewModel)DataContext).Subnet = subnet; // Обновляем ViewModel
            }
            else if (textBox.Text == "")
            {
                ((IPViewModel)DataContext).Subnet = 0;
            }
            else
            {
                // Значение некорректное - восстанавливаем предыдущее значение (или другое действие)
                textBox.Text = ((IPViewModel)DataContext).Subnet.ToString();
            }
        }
    }

    public class IPRowStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            // Проверяем, что item не null и что container - это DataGridRow
            if (item == null || !(container is DataGridRow row)) return null;

            // Получаем IP-адрес текущей строки (предполагаем, что свойство называется "IP")
            string ipAddress = (string)(row.Item as dynamic).IP;  //Или другой способ доступа к IP

            // Проверка на наличие группы (Group - свойство, создаваемое при группировке)
            var group = row.DataContext.GetType().GetProperty("Group")?.GetValue(row.DataContext);

            if (group != null)
            {
                // Стиль для строк с одинаковым IP (выделение)
                return new Style(typeof(DataGridRow))
                {
                    Setters = {
                    new Setter(DataGridRow.BackgroundProperty, System.Windows.Media.Brushes.LightYellow), //Или другой цвет
                    new Setter(DataGridRow.FontWeightProperty, FontWeights.Bold) // Или другое форматирование
                }
                };
            }

            return null; // Стандартный стиль для строк с уникальным IP
        }
    }

}
