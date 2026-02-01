using System.Configuration;
using System.Data;
using System.Windows;

namespace ShopProducts
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Model.HostStepanovContext Context { get; set; } = new Model.HostStepanovContext();

        public static Model.User? CurrentUser { get; set; } = null;

    }
}
