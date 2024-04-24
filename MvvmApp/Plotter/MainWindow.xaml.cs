using Flee.PublicTypes;
using OxyPlot;
using OxyPlot.Series;
using System.Linq;
using System.Windows;
using System;

namespace Plotter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private (double[], double[]) EvaluateSeries(string expression)
        {
            ExpressionContext context = new ExpressionContext();
            context.Variables["x"] = double.NaN;
            IGenericExpression<double> e = context.CompileGeneric<double>(expression);

            var x = Enumerable.Range(0, 101).Select(i => -1 + i / 50.0).ToArray();
            var y = x.Select(xx =>
                {
                    context.Variables["x"] = xx;
                    return e.Evaluate();
                }).ToArray();


            return (x, y);
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var (x, y) = EvaluateSeries(expressionBox.Text);

                table.ItemsSource = x.Zip(y).Select(t => new Point { X = t.First, Y = t.Second });

                LineSeries line = new LineSeries();
                for (var i = 0; i < x.Length; i++)
                    line.Points.Add(new DataPoint(x[i], y[i]));
                var pm = new PlotModel { Title = "Chart" };
                pm.Series.Add(line);

                chart.Model = pm;
            }
            catch(Exception exc)
            {
                MessageBox.Show($"Ошибка вычислений: {exc}");
            }
        }
    }
}
