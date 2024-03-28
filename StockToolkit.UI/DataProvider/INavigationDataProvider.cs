using StockToolkit.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockToolkit.UI.DataProvider
{
    public interface INavigationDataProvider
    {
        IEnumerable<Stock> GetAllStocks();

        Task<IEnumerable<Stock>> GetAllStocksAsync();
    }
}
