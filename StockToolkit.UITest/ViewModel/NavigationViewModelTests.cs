using Moq;
using Prism.Events;
using StockToolkit.Model;
using StockToolkit.UI.DataProvider;
using StockToolkit.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StockToolkit.UITest.ViewModel
{
    public class NavigationViewModelTests
    {
        private NavigationViewModel _viewModel;

        public NavigationViewModelTests()
        {
            Mock<IEventAggregator> eventAggregatorMock = new Mock<IEventAggregator>();
            Mock<INavigationDataProvider> navigationDataProviderMock = new Mock<INavigationDataProvider>();
            navigationDataProviderMock.Setup(s => s.GetAllStocks())
                .Returns(new List<Stock>
                    {
                        new Stock { StockID = 1, StockCode = "test 1" },
                        new Stock { StockID = 2, StockCode = "test 2" },
                        new Stock { StockID = 3, StockCode = "test 3" },
                    });

            _viewModel = 
                new NavigationViewModel(navigationDataProviderMock.Object, eventAggregatorMock.Object);
        }

        [Fact]
        public void ShouldLoadStocks()
        {
            _viewModel.OnLoadFinish += message => Assert.Equal(3, _viewModel.Stocks.Count);
            _viewModel.Load();
        }

        [Fact]
        public void ShouldLoadStocksOnlyOnce() 
        {
            _viewModel.OnLoadFinish += message => Assert.Equal(3, _viewModel.Stocks.Count);
            _viewModel.Load();
            _viewModel.Load();
        }
    }
}
