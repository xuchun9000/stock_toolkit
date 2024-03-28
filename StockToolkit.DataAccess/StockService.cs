using StockToolkit.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockToolkit.DataAccess
{
    public class StockService : IStockService
    {
        public StockService()
        {

        }

        private const double maxValue = 100.00;
        private const double minValue = 10.00;
        private IEnumerable<Stock> allStocks;

        private Stock GetRealTimeStock(int id)
        {
            Stock resultStock = GetAllStocks().SingleOrDefault(s => s.StockID == id);
            if (resultStock == null)
            {
                throw new KeyNotFoundException();
            }

            double nextDoubel = (new Random()).NextDouble() * (maxValue - minValue) + minValue;

            return new Stock
            {
                StockName = resultStock.StockName,
                StockID = resultStock.StockID,
                StockCode = resultStock.StockCode,
                StockPrice = nextDoubel,
                TradeTime = DateTime.Now,
            };
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            if (allStocks == null)
            {
                // sleep 4 seconds to similate the loading time in real life
                Thread.Sleep(4000);
                allStocks = new List<Stock>
                {
                    new Stock { StockCode = "IBM", StockID=1, StockName="International Business Machine" },
                    new Stock { StockCode = "NVDA", StockID=2, StockName="invdia" },
                    new Stock { StockCode = "MSFT", StockID=3, StockName="microsoft" },
                    new Stock { StockCode = "TSLA", StockID=4, StockName="tsla car" }
                };
            }
            return allStocks;
        }

        public Task<IEnumerable<Stock>> GetAllStocksAsync()
        {
            return Task.Run(() => GetAllStocks());
        }

        public Stock GetStock(int id, StockGetType stockGetType = StockGetType.BasicInfo)
        {
            Stock resultStock = GetAllStocks().SingleOrDefault(s => s.StockID == id);
            if (resultStock == null)
            {
                throw new KeyNotFoundException();
            }

            if (stockGetType == StockGetType.BasicInfo)
            {
                return resultStock;
            }
            else if (stockGetType == StockGetType.RealTimePrice)
            {
                resultStock = GetRealTimeStock(resultStock.StockID);
            }

            return resultStock;
        }

        public Task<Stock> GetStockAsync(int id, StockGetType stockGetType)
        {
            return Task.Run(() => GetStock(id, stockGetType));
        }

        public void Dispose()
        {
            // just for demo now
        }
    }
}
