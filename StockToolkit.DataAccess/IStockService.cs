using StockToolkit.Model;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StockToolkit.DataAccess
{
    public interface IStockService : IDisposable
    {
        Stock GetStock(int id, StockGetType stockGetType);
        Task<Stock> GetStockAsync(int id, StockGetType stockGetType);
        IEnumerable<Stock> GetAllStocks();
        Task<IEnumerable<Stock>> GetAllStocksAsync();
    }

    public enum StockGetType
    {
        BasicInfo,
        RealTimePrice
    }
}
