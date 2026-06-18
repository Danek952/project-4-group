using System.Windows;
using ShopProducts.Model;

namespace ShopProducts.Pages
{
    public partial class ServiceEditWindow : Window
    {
        public Service Service { get; private set; }

        public ServiceEditWindow(Service service = null)
        {
            InitializeComponent();
            Service = service ?? new Service();
            DataContext = Service;

            if (service != null)
            {
                NameBox.Text = service.Name;
                DescriptionBox.Text = service.Description;
                PriceBox.Text = service.Price.ToString();
                foreach (System.Windows.Controls.ComboBoxItem item in CategoryBox.Items)
                {
                    if (item.Content.ToString() == service.Category)
                    {
                        CategoryBox.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите название услуги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(PriceBox.Text, out decimal price))
            {
                MessageBox.Show("Введите корректную цену", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Service.Name = NameBox.Text;
            Service.Description = DescriptionBox.Text;
            Service.Price = price;
            Service.Category = (CategoryBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString() ?? "Продажа";

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}