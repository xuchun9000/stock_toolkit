using Moq;
using StockToolkit.DataAccess;
using StockToolkit.Model;
using StockToolkit.UI.ViewModel;
using System.Collections.Generic;
using Xunit;

namespace StockToolkit.UITest.ViewModel
{
    public class StockEditViewModelTests
    {
        private Mock<IStockService> _stockService;
        private StockEditViewModel _stockEditViewModel;

        public StockEditViewModelTests()
        {
            _stockService = new Mock<IStockService>();
            _stockService
                .Setup(s => s.GetStock(It.IsAny<int>(), StockGetType.BasicInfo))
                .Returns<int, StockGetType>((s, i) => new Stock {  StockID = s });
            _stockEditViewModel = new StockEditViewModel(_stockService.Object);
        }

        [Fact]
        public void AfterLoadStockShouldHasBasicInfo()
        {
            _stockEditViewModel.Load(1);
            Assert.Equal(_stockEditViewModel.Stock.StockID, 1);

            _stockEditViewModel.Load(2);
            Assert.Equal(_stockEditViewModel.Stock.StockID, 2);
        }
    }
}
