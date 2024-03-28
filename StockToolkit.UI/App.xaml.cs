using Autofac;
using StockToolkit.DataAccess;
using StockToolkit.UI.DataProvider;
using StockToolkit.UI.Startup;
using StockToolkit.UI.View;
using StockToolkit.UI.ViewModel;
using System.Windows;

namespace StockToolkit.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            BootStrapper bootStrapper = new BootStrapper();
            IContainer container = bootStrapper.BootStrap();

            MainWindow mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
