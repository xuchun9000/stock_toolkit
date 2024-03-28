using StockToolkit.DataAccess;
using StockToolkit.Model;
using StockToolkit.UI.Command;
using StockToolkit.UI.Dialog;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace StockToolkit.UI.ViewModel
{
    public class StockEditViewModel : ViewModelBase, IStockEditViewModel
    {
        #region private const fields
        private const double canvasHeight = 400D;
        private const double yBigParts = 10D;
        private const double ySmallPartsInBigPart = 2D;
        private const double yTotalValue = 100D;

        private const double canvasWidth = 600D;
        private const double xParts = 50D;
        private const double xInsertTextValuePointValue = 10D;
        private const double xTotalValue = 100D;
        private const double xOffset = 80D;
        #endregion

        #region private fields
        private IStockService _stockService;
        private ObservableCollection<StockBarViewModel> _stockBarViewModels;
        private DispatcherTimer _timer;
        private double _top = 20;
        private ObservableCollection<StockBarViewModel> yAxisLines;
        private ObservableCollection<StockBarViewModel> xAxisLines;

        private int xInitPoint = 0;
        private Stock previousRealTimeStock = null;
        private double convertStockPriceToScreenValue(double price)
        {
            return canvasHeight - price / yTotalValue * canvasHeight;
        }
        #endregion

        #region private methods
        private void _timer_Tick(object sender, EventArgs e)
        {
            Stock realTimeStock = _stockService.GetStock(this.Stock.StockID, StockGetType.RealTimePrice);
            double left = XAxisLines[xInitPoint].BarLeft;
            double top = convertStockPriceToScreenValue(realTimeStock.StockPrice.Value);
            double height = 10;
            double width = 10;
            double thickness = 1;
            Brush brush = new SolidColorBrush(Colors.White);

            if (previousRealTimeStock != null)
            {
                if (realTimeStock.StockPrice > previousRealTimeStock.StockPrice)
                {
                    height = convertStockPriceToScreenValue(previousRealTimeStock.StockPrice.Value) - top;
                    brush = new SolidColorBrush(Colors.Green);
                }
                else if (realTimeStock.StockPrice < previousRealTimeStock.StockPrice)
                {
                    height = top - convertStockPriceToScreenValue(previousRealTimeStock.StockPrice.Value);
                    top = convertStockPriceToScreenValue(previousRealTimeStock.StockPrice.Value);
                    brush = new SolidColorBrush(Colors.Red);
                }
            }

            previousRealTimeStock = realTimeStock;
            StockBarViewModels.Add(
                new StockBarViewModel
                {
                    BarLeft = left,
                    BarTop = top,
                    BarHeight = height,
                    BarWidth = width,
                    BarStrock = brush,
                    BarThickness = thickness,
                    TradeTime = realTimeStock.TradeTime,
                    BarValue = realTimeStock.StockPrice.Value
                }); 
            xInitPoint++;
            if(xInitPoint >= XAxisLines.Count)
            {
                _timer.Stop();
            }

            ResumeCommand.RaiseCanExecuteChanged();
            PauseCommand.RaiseCanExecuteChanged();
        }
        private void initXAxisLines()
        {
            double top = YAxisLines.Last().BarTop;
            foreach (int x in Enumerable.Range(0, (int)xParts))
            {
                double left = canvasWidth / xParts * x + xOffset;
                double xValue = xTotalValue / xParts * x;
                string textValue = null;
                if(x % xInsertTextValuePointValue == 0)
                {
                    textValue = xValue.ToString();
                }

                var xPart = new StockBarViewModel
                {
                    BarHeight = 10D,
                    BarWidth = 2D,
                    BarLeft = left,
                    BarTop = top,
                    BarValue = xValue,                    
                    BarStrock = new SolidColorBrush(Colors.White)
                };
                XAxisLines.Add(xPart);
            }            
        }
        private void initYAxisLines()
        {
            for(int i=0; i< yBigParts; i++)
            {
                double longBarValue = yTotalValue - (yTotalValue / yBigParts * i);
                double longBarTop = canvasHeight / yBigParts * i;

                var longPart = new StockBarViewModel
                {
                    BarHeight = 2D,
                    BarWidth = 40D,
                    BarLeft = 0,
                    BarTop = longBarTop,
                    BarValue = longBarValue,
                    BarText = longBarValue.ToString("0.00"),
                    BarStrock = new SolidColorBrush(Colors.White)
                };
                YAxisLines.Add(longPart);

                for (int j = 1; j < ySmallPartsInBigPart; j++)
                {
                    double shortBarValue = longBarValue - yTotalValue / yBigParts / ySmallPartsInBigPart * j;
                    double shortBarTop = longBarTop + (canvasHeight / yBigParts / ySmallPartsInBigPart) * j;
                    var shortPart = new StockBarViewModel
                    {
                        BarHeight = 2D,
                        BarWidth = 20D,
                        BarLeft = 0,
                        BarTop = shortBarTop,
                        BarValue = shortBarValue,
                        BarStrock = new SolidColorBrush(Colors.Red)
                    };
                    YAxisLines.Add(shortPart);
                }
            }
        }
        #endregion

        public StockEditViewModel(IStockService stockService)
        {
            _stockService = stockService;
            StockBarViewModels = new ObservableCollection<StockBarViewModel>();
            YAxisLines = new ObservableCollection<StockBarViewModel>();
            XAxisLines = new ObservableCollection<StockBarViewModel>();

            initYAxisLines();
            initXAxisLines();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;

            PauseCommand = new DelegateCommand(OnPauseCommandExecute, obj => _timer.IsEnabled && xInitPoint < XAxisLines.Count);
            ResumeCommand = new DelegateCommand(OnResumeCommandExecute, obj => !_timer.IsEnabled && xInitPoint < XAxisLines.Count && StockBarViewModels.Count > 0);
            RestartCommand = new DelegateCommand(OnRestartCommandExecute);
            ShowHistoryCommand = new DelegateCommand(OnShowHistoryCommandExecute);
        }

        #region private command executes
        private void OnShowHistoryCommandExecute(object obj)
        {
            if(PauseCommand.CanExecute(null)) PauseCommand.Execute(null);
            ShowStockHistoryDialog showStockHistoryDialog = new ShowStockHistoryDialog();
            showStockHistoryDialog.DataContext = this;
            showStockHistoryDialog.ShowDialog();
        }

        private void OnRestartCommandExecute(object obj)
        {
            _timer.Stop();
            xInitPoint = 0;            
            StockBarViewModels.Clear();

            ResumeCommand.RaiseCanExecuteChanged();
            PauseCommand.RaiseCanExecuteChanged();
            _timer.Start();
        }

        private void OnResumeCommandExecute(object obj)
        {
            _timer.Start();
            ResumeCommand.RaiseCanExecuteChanged();
            PauseCommand.RaiseCanExecuteChanged();
        }

        private void OnPauseCommandExecute(object obj)
        {
            _timer.Stop();
            PauseCommand.RaiseCanExecuteChanged();
            ResumeCommand.RaiseCanExecuteChanged();
        }
        #endregion

        public Stock Stock { get; set; }

        public void Load(int stockId)
        {
            Stock = _stockService.GetStock(stockId, StockGetType.BasicInfo);
            _timer.Start();
        }

        public ObservableCollection<StockBarViewModel> StockBarViewModels
        {
            get => _stockBarViewModels;
            set
            {
                _stockBarViewModels = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<StockBarViewModel> XAxisLines 
        { 
            get => xAxisLines;
            set
            {
                xAxisLines = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<StockBarViewModel> YAxisLines
        {
            get => yAxisLines; 
            
            set
            {
                yAxisLines = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand PauseCommand { get; set; }
        public DelegateCommand ResumeCommand { get; set; }
        public DelegateCommand RestartCommand {  get; set; }
        public DelegateCommand ShowHistoryCommand { get; set; }
    }
}
