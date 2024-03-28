using Moq;
using Prism.Events;
using StockToolkit.Model;
using StockToolkit.UI.Events;
using StockToolkit.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StockToolkit.UITest.ViewModel
{
    public class MainViewModelTests
    {
        private Mock<INavigationViewModel> _navigationViewModelMock;
        private Mock<IEventAggregator> _eventAggregatorMock;
        private OpenStockEditViewEvent _openStockEditViewEvent;
        private MainViewModel _view_model;
        private List<Mock<IStockEditViewModel>> _stockEditViewModelMocks;

        public MainViewModelTests()
        {
            _stockEditViewModelMocks = new List<Mock<IStockEditViewModel>>();
            _navigationViewModelMock = new Mock<INavigationViewModel>();
            _eventAggregatorMock = new Mock<IEventAggregator>();
            _openStockEditViewEvent = new OpenStockEditViewEvent();
            _eventAggregatorMock.Setup(x => x.GetEvent<OpenStockEditViewEvent>()).Returns(_openStockEditViewEvent);
            _view_model = new MainViewModel(_navigationViewModelMock.Object,
                CreateStockEditViewModel, _eventAggregatorMock.Object);
        }

        private IStockEditViewModel CreateStockEditViewModel()
        {
            var stockEditViewModelMock = new Mock<IStockEditViewModel>();
            stockEditViewModelMock.Setup(vm => vm.Load(It.IsAny<int>()))
                .Callback<int>(stockId =>
                {
                    stockEditViewModelMock.Setup(vm => vm.Stock).Returns(new Stock {  StockID = stockId });
                });
            _stockEditViewModelMocks.Add(stockEditViewModelMock);
            return stockEditViewModelMock.Object;
        }

        /// <summary>
        /// make sure the INavigationViewModel.Load() executed only once
        /// when MainViewModel.Load() tuns
        /// </summary>
        [Fact]
        public void call_load_method_of_navigation_view_model()
        {            
            _view_model.Load();
            _navigationViewModelMock.Verify(s=> s.Load(), Times.Once);
        }

        [Fact]
        public void ShouldAddStockEditViewModelsOnlyOnce()
        {
            _openStockEditViewEvent.Publish(5);
            _openStockEditViewEvent.Publish(5);
            _openStockEditViewEvent.Publish(6);
            _openStockEditViewEvent.Publish(7);
            _openStockEditViewEvent.Publish(7);

            Assert.Equal(3, _view_model.StockEditViewModels.Count());
        }
    }
}
