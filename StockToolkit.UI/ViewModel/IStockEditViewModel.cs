using StockToolkit.Model;
using StockToolkit.UI.Command;
using System.Windows.Input;

namespace StockToolkit.UI.ViewModel
{
    public interface IStockEditViewModel
    {
        void Load(int stockId);
        Stock Stock { get; set; }

        DelegateCommand PauseCommand { get; set; }

        DelegateCommand ResumeCommand { get; set; }

        DelegateCommand RestartCommand { get; set; }

        DelegateCommand ShowHistoryCommand { get; set; }

    }
}
