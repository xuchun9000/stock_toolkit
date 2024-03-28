using StockToolkit.DataAccess;
using StockToolkit.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockToolkit.UI.DataProvider
{
    public class NavigationDataProvider : INavigationDataProvider
    {
        private Func<IStockService> _stockServiceCreator;

        public NavigationDataProvider(Func<IStockService> stockServiceCreator)
        {
            _stockServiceCreator = stockServiceCreator;
        }
        public IEnumerable<Stock> GetAllStocks()
        {
            using(var stockService = _stockServiceCreator())
            {
                var stocks = stockService.GetAllStocks();
                return stocks;
            }            
        }

        public Task<IEnumerable<Stock>> GetAllStocksAsync()
        {
            using (var stockService = _stockServiceCreator())
            {
                Task<IEnumerable<Stock>> stocksTask = stockService.GetAllStocksAsync();
                return stocksTask;
            }
        }
    }
}
