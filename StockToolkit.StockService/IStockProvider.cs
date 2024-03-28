using StockToolkit.Model;
using System;
using System.Collections.Generic;

namespace StockToolkit.StockService
{
    public interface IStockProvider
    {
        Stock GetStock(int id, StockGetType stockGetType);
        IEnumerable<Stock> GetAllStocks();
    }
}
