using Prism.Events;
using StockToolkit.UI.Command;
using StockToolkit.UI.Events;
using System;
using System.Windows.Input;

namespace StockToolkit.UI.ViewModel
{
    public class NavigationItemViewModel  
    {
        public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            OpenStockEditViewCommand = new DelegateCommand(OnOpenStockEditViewCommandExecute);
            _eventAggregator = eventAggregator;
        }

        private void OnOpenStockEditViewCommandExecute(object obj) 
            => _eventAggregator.GetEvent<OpenStockEditViewEvent>().Publish(Id);        

        public int Id { get; private set;  }
        public string DisplayMember { get; private set; }

        public ICommand OpenStockEditViewCommand {  get; private set; }

        private IEventAggregator _eventAggregator;
    }
}
