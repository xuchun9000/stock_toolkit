using System;
using System.Windows;
using System.Windows.Media;

namespace StockToolkit.UI.ViewModel
{
    public class StockBarViewModel : ViewModelBase
    {
        private Brush barStrock;
        private string barText;
        private double barThickness;

        public double BarLeft { get; set; }
        public double BarTop { get; set; }
        public string BarText 
        { 
            get => barText;
            set
            {
                barText = value;
                OnPropertyChanged();
            }
        }

        public double BarValue {  get; set; }

        public double BarWidth { get; set; }

        public double BarHeight { get; set; }

        private double? orgBarThickness = null;
        

        public double BarThickness 
        { 
            get => barThickness;
            set
            {
                barThickness = value;
                if(!orgBarThickness.HasValue)
                {
                    orgBarThickness = barThickness;
                }
                OnPropertyChanged();
            }
        }


        private Brush orgBarStrock = null;
        public Brush BarStrock 
        { 
            get => barStrock;
            set
            {
                barStrock = value;
                if(orgBarStrock == null)
                {
                    orgBarStrock = barStrock;
                }
                OnPropertyChanged();
            }
        }

        public DateTime? TradeTime { get; internal set; }

        public void HighlightBackground()
        {
            BarStrock = new SolidColorBrush(Colors.LightYellow);
            BarText = BarValue.ToString("0.00");            
        }
        public void ResetBackground()
        {
            BarStrock = orgBarStrock;
            BarText = null;
        }        
    }
}
