using PlotterViewModel;
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

namespace PlotterMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IUIServices
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(this);
        }

        public void PlotLineSeries(double[] x, double[] y)
        {
            line.Plot(x, y);
        }

        public void ReportError(string message)
        {
            //errorBar.Text = message;
            //errorBar.Visibility = Visibility.Visible;
            MessageBox.Show(message); 
        }
    }

    public class ErrorBarReporter : IUIServices
    {
        public TextBlock ErrorBar { get; set; }

        public void PlotLineSeries(double[] x, double[] y)
        {
            
        }

        public void ReportError(string message)
        {
            ErrorBar.Text = message;
            ErrorBar.Visibility = Visibility.Visible;
        }
    }

    //public class MessageBoxErrorReporter : IUIServices
    //{
    //    public void ReportError(string message) => MessageBox.Show(message);
    //}


}
