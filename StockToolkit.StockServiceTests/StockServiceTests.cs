using StockToolkit.DataAccess;
using StockToolkit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StockToolkit.DataAccessTests
{
    public class StockServiceTests
    {
        private StockService _stockService;

        public StockServiceTests()
        {
            _stockService = new StockService();
        }

        [Fact]
        public void GenerateAllStocksAndMakeSureIBMStockIDAlwaysFirst()
        {
            IEnumerable<Stock> stocks = _stockService.GetAllStocks();
            Assert.Equal(4, stocks.Count());
            Stock ibmStock = _stockService.GetStock(1);
            Assert.Equal(ibmStock.StockCode, "IBM");
        }        

        [Fact]
        public void CheckExceptions()
        {
            var exceptionType = typeof(KeyNotFoundException);
            Assert.Throws(exceptionType,() => _stockService.GetStock(-1));
            Assert.Throws(exceptionType, () => _stockService.GetStock(int.MaxValue));
        }

        [Fact]
        public void CheckBasicInformation()
        {
            int stockID = 1;
            Stock ibmStock = _stockService.GetStock(stockID);
            Assert.Equal(ibmStock.StockName, "International Business Machine");
            Assert.Equal(ibmStock.StockCode, "IBM");
            Assert.Equal(ibmStock.StockPrice, null);
            Assert.Equal(ibmStock.TradeTime, null);

            ibmStock = _stockService.GetStock(stockID, StockGetType.RealTimePrice);
            Assert.Equal(ibmStock.StockName, "International Business Machine");
            Assert.Equal(ibmStock.StockCode, "IBM");
            Assert.True(ibmStock.StockPrice >= 10.0D && ibmStock.StockPrice <= 100.0D);
            Assert.True(ibmStock.TradeTime > new DateTime(2024, 1, 1));
        }
    }
}
