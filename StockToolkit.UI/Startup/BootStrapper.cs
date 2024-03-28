using Autofac;
using Prism.Events;
using StockToolkit.DataAccess;
using StockToolkit.UI.DataProvider;
using StockToolkit.UI.View;
using StockToolkit.UI.ViewModel;

namespace StockToolkit.UI.Startup
{
    public class BootStrapper
    {
        public IContainer BootStrap()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<StockEditViewModel>().As<IStockEditViewModel>();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();            
            builder.RegisterType<NavigationDataProvider>().As<INavigationDataProvider>();            
            builder.RegisterType<StockService>().As<IStockService>().SingleInstance();

            return builder.Build();
        }
    }
}
