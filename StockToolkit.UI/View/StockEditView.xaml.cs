using Autofac;
using Autofac.Core;
using StockToolkit.DataAccess;
using StockToolkit.UI.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockToolkit.UI.View
{
    /// <summary>
    /// Interaction logic for StockEditView.xaml
    /// </summary>
    public partial class StockEditView : UserControl
    {
        private IStockService _stockService;

        /// <summary>
        /// https://stackoverflow.com/questions/7177432/how-to-display-items-in-canvas-through-binding
        /// </summary>
        public StockEditView()
        {
            InitializeComponent();            
            IContainer container = (new BootStrapper()).BootStrap();
            _stockService = container.Resolve<IStockService>();
        }
    }
}
