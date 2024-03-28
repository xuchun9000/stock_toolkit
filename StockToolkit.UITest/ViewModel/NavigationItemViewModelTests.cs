using Moq;
using Prism.Events;
using StockToolkit.UI.Events;
using StockToolkit.UI.ViewModel;
using System.Reflection.PortableExecutable;
using Xunit;

namespace StockToolkit.UITest.ViewModel
{
    public class NavigationItemViewModelTests
    {
        [Fact]
        public void ShouldPublishOpenStockEditViewEvent()
        {
            const int stockID = 7;
            var eventMock = new Mock<OpenStockEditViewEvent>();
            var eventAggregatorMock = new Mock<IEventAggregator>();
            eventAggregatorMock.Setup(s=> s.GetEvent<OpenStockEditViewEvent>())
                .Returns(eventMock.Object);

            var viewModel = new NavigationItemViewModel(stockID, "matt xu", eventAggregatorMock.Object);
            viewModel.OpenStockEditViewCommand.Execute(null);

            eventMock.Verify(s=> s.Publish(stockID), Times.Once());
        }
    }
}
