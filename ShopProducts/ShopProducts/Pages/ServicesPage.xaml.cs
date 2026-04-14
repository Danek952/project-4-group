using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ShopProducts.Model;

namespace ShopProducts.Pages
{
    public partial class ServicesPage : Page
    {
        public ServicesPage()
        {
            InitializeComponent();
            LoadServices();
        }

        private void LoadServices()
        {
            var query = App.Context.Services.AsQueryable();

            string searchText = SearchBox.Text.ToLower();
            string category = (CategoryFilter.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Все";

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(s => s.Name.ToLower().Contains(searchText) ||
                                         s.Description.ToLower().Contains(searchText));
            }

            if (category != "Все")
            {
                query = query.Where(s => s.Category == category);
            }

            ServicesGrid.ItemsSource = query.ToList();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadServices();
        }

        private void CategoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadServices();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var serviceWindow = new ServiceEditWindow();
            if (serviceWindow.ShowDialog() == true)
            {
                App.Context.Services.Add(serviceWindow.Service);
                App.Context.SaveChanges();
                LoadServices();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesGrid.SelectedItem is Service service)
            {
                var editWindow = new ServiceEditWindow(service);
                if (editWindow.ShowDialog() == true)
                {
                    App.Context.SaveChanges();
                    LoadServices();
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesGrid.SelectedItem is Service service)
            {
                var result = MessageBox.Show($"Удалить услугу '{service.Name}'?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    App.Context.Services.Remove(service);
                    App.Context.SaveChanges();
                    LoadServices();
                }
            }
        }
    }
}