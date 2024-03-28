using System;

namespace StockToolkit.Model
{
    public class Stock
    {
        public int StockID { get; set; }
        public string StockCode { get; set; }
        public string StockName { get; set; }
        public DateTime? TradeTime { get; set; }
        public double? StockPrice { get; set; }
    }
}
