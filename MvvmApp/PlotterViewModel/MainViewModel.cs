using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace PlotterViewModel
{
    public interface IUIServices
    {
        void ReportError(string message);
        void PlotLineSeries(double[] x, double[] y);
    }

    public class MainViewModel : ViewModelBase
    {
        private double[] x = new double[0];
        private double[] y = new double[0];

        private readonly IUIServices uiServices;

        public IEnumerable<RowViewModel> TableData 
        {
            get => x.Zip(y).Select(t => new RowViewModel { X = t.First, F = t.Second });
        }

        public PlotModel ChartData
        {
            get
            {
                LineSeries line = new LineSeries();
                for (var i = 0; i < x.Length; i++)
                    line.Points.Add(new DataPoint(x[i], y[i]));
                var pm = new PlotModel { Title = "Chart" };
                pm.Series.Add(line);

                return pm;
            }
        }

        private void OnPlot(object arg)
        {
            try
            {
                (x, y) = Evaluator.Evaluator.EvaluateSeries(Expression);
                uiServices.PlotLineSeries(x, y);
                RaisePropertyChanged(nameof(ChartData));
                RaisePropertyChanged(nameof(TableData));
            }
            catch(Exception e)
            {
                uiServices.ReportError(e.Message);
            }

        }

        public string Expression { get; set; }
        
        public ICommand PlotCommand { get; private set; }

        public MainViewModel(IUIServices uiServices)
        {
            this.uiServices = uiServices;
            PlotCommand = new RelayCommand(OnPlot,_ => !string.IsNullOrEmpty(Expression));
        }
    }

    public class RowViewModel
    {
        public double X { get; set; }
        public double F { get; set; }
    }
}

// var mvm = new MainViewModel();
// mvm.TableData.Should().Equal(...)
// mvm.ChartData.Should().Equal(...)