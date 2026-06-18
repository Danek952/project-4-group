using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ShopProducts.Model;

namespace ShopProducts.Pages
{
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();
            LoadClients();
        }

        private void LoadClients()
        {
            var query = App.Context.Users.AsQueryable();

            string searchText = SearchBox.Text.ToLower();
            string filterType = (FilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Все";

            if (!string.IsNullOrEmpty(searchText))
            {
                query = filterType switch
                {
                    "По фамилии" => query.Where(u => u.LastName.ToLower().Contains(searchText)),
                    "По имени" => query.Where(u => u.FirstName.ToLower().Contains(searchText)),
                    _ => query.Where(u => u.LastName.ToLower().Contains(searchText) || 
                                         u.FirstName.ToLower().Contains(searchText) ||
                                         u.Patronymic.ToLower().Contains(searchText))
                };
            }

            string sortType = (SortComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "По возрастанию";
            query = sortType == "По возрастанию" 
                ? query.OrderBy(u => u.LastName) 
                : query.OrderByDescending(u => u.LastName);

            ClientsGrid.ItemsSource = query.ToList();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadClients();
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadClients();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadClients();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsGrid.SelectedItem is User client)
            {
                var result = MessageBox.Show($"Удалить клиента {client.LastName} {client.FirstName}?", 
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    App.Context.Users.Remove(client);
                    App.Context.SaveChanges();
                    LoadClients();
                }
            }
        }
    }
}
