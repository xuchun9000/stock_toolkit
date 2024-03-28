using System;

namespace StockToolkit.UI.ViewModel
{
    public interface INavigationViewModel
    {
        void Load();
        event Action<string> OnLoadFinish;
    }
}
