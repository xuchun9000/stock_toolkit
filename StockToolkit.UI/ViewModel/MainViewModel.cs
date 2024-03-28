using Prism.Events;
using StockToolkit.UI.Command;
using StockToolkit.UI.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace StockToolkit.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IStockEditViewModel _stockEditViewModel;
        public MainViewModel(
            INavigationViewModel navigationViewModel,
            Func<IStockEditViewModel> stockEditVmCreator,
            IEventAggregator eventAggregator)
        {
            NavigationViewModel = navigationViewModel;
            StockEditViewModels = new ObservableCollection<IStockEditViewModel>();
            _stockEditVmCreator = stockEditVmCreator;
            eventAggregator.GetEvent<OpenStockEditViewEvent>().Subscribe(OnOpenStockEditView);
            CloseStockTabCommand = new DelegateCommand(OnCloseStockTabCommandExecute);
            LoadingMessage = "Loading All Stocks";
            NavigationViewModel.OnLoadFinish += NavigationViewModel_OnLoadFinish;
        }

        private void NavigationViewModel_OnLoadFinish(string loading_message)
        {
            LoadingMessage = loading_message;
        }

        private void OnCloseStockTabCommandExecute(object obj)
        {
            if (obj is StockEditViewModel viewModel && StockEditViewModels.Contains(viewModel))
            {
                StockEditViewModels.Remove(viewModel);
            }
        }

        private void OnOpenStockEditView(int stockId)
        {
            var stockEditVm = StockEditViewModels.SingleOrDefault(s=> s.Stock.StockID == stockId);
            if (stockEditVm == null)
            {
                stockEditVm = _stockEditVmCreator();
                StockEditViewModels.Add(stockEditVm);
                stockEditVm.Load(stockId);
            }

            SelectedStockEditViewModel = stockEditVm;
        }

        public INavigationViewModel NavigationViewModel { get; private set; }

        public ObservableCollection<IStockEditViewModel> StockEditViewModels { get; private set; }

        private Func<IStockEditViewModel> _stockEditVmCreator;
        private IStockEditViewModel _selectedStockEditViewModel;
        private string loadingMessage;

        public IStockEditViewModel SelectedStockEditViewModel
        { 
            get => _selectedStockEditViewModel;
            set
            {
                _selectedStockEditViewModel = value;
                OnPropertyChanged();
            }
        }

        public void Load()
        {
            this.NavigationViewModel.Load();
        }

        public DelegateCommand CloseStockTabCommand { get; private set; }

        public string LoadingMessage 
        { 
            get => loadingMessage;
            private set
            {
                loadingMessage = value;
                OnPropertyChanged();
            }
        }
    }
}
