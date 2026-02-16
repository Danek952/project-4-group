using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShopProducts.Model;

namespace ShopProducts.Pages
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public int Count;

        public AuthPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (TBoxLogin.Text != "" && TBoxPassword.Password != "")
            {
                if (App.Context.Users.FirstOrDefault(p => p.Login == TBoxLogin.Text && p.Password == TBoxPassword.Password) is User user)
                {
                    Count = 0;

                    if (user.Block == false)
                    {
                        TBoxLogin.Text = "";
                    }
                    if (user.Role == 1)
                    {
                        MessageBox.Show("Вы успешно авторизовались как Администратор", "Успешный вход!", MessageBoxButton.OK, MessageBoxImage.Information);
                        App.CurrentUser = user;
                        if (user.FirstAuth == false)
                        {
                            user.FirstAuth = true;
                            DataContext = user;
                            App.Context.SaveChanges();
                            NavigationService.Navigate(new ChangePasswordPage());
                        }
                        else
                            NavigationService.Navigate(new AdminMenuPage());
                    }
                }
            }
        }
    }
}