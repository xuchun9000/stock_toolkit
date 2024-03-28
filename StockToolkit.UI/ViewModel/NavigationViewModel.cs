using Prism.Events;
using StockToolkit.Model;
using StockToolkit.UI.DataProvider;
using System;
using System.Collections.ObjectModel;

namespace StockToolkit.UI.ViewModel
{    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        public NavigationViewModel(INavigationDataProvider dataProvider, IEventAggregator eventAggregator)
        {
            Stocks = new ObservableCollection<NavigationItemViewModel>();
            _dataProvider = dataProvider;
            _eventAggregator = eventAggregator;
        }

        /// <summary>
        /// here I use async and await to load the all stocks in another thread,
        /// so the UI thread will not be blocked when loading large data which may wait long time
        /// </summary>
        public async void Load()
        {
            Stocks.Clear();
            foreach (var stock in await _dataProvider.GetAllStocksAsync())
            {
                Stocks.Add(new NavigationItemViewModel(stock.StockID, stock.StockCode, _eventAggregator));
            }

            OnLoadFinish?.Invoke("Load finished");
        }

        public ObservableCollection<NavigationItemViewModel> Stocks { get; private set; }

        private INavigationDataProvider _dataProvider;
        private IEventAggregator _eventAggregator;

        public event Action<string> OnLoadFinish;
    }
}
