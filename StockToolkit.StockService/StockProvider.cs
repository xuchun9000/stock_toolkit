using StockToolkit.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockToolkit.StockService
{
    public class StockProvider : IStockProvider
    {
        public StockProvider()
        {
            
        }

        private const double maxValue = 100.00;
        private const double minValue = 10.00;

        private Stock GetRealTimeStock(int id)
        {
            Stock resultStock = GetAllStocks().SingleOrDefault(s => s.StockID == id);
            if (resultStock == null)
            {
                throw new KeyNotFoundException();
            }

            resultStock.StockPrice = (new Random()).NextDouble() * (maxValue - minValue) + minValue;
            resultStock.StockTime = DateTime.Now;

            return resultStock;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return new List<Stock>
            {
                new Stock { StockCode = "IBM", StockID=1, StockName="International BUsiness Machine" },
                new Stock { StockCode = "NVDA", StockID=2, StockName="invdia" },
                new Stock { StockCode = "MSFT", StockID=3, StockName="microsoft" },
                new Stock { StockCode = "TSLA", StockID=4, StockName="tsla car" }
            };
        }

        public Stock GetStock(int id, StockGetType stockGetType = StockGetType.BasicInfo)
        {
            Stock resultStock = GetAllStocks().SingleOrDefault(s => s.StockID == id);
            if(resultStock == null)
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
    }

    public enum StockGetType
    {
        BasicInfo,
        RealTimePrice
    }
}
