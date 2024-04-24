using System.Reactive.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RxMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel(
                Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove")
                    .Select(e => GetPoint(e.EventArgs)),
                Observable.FromEventPattern<MouseButtonEventArgs>(this, "MouseDown")
                    .Select(e => GetPoint(e.EventArgs)),
                Observable.FromEventPattern<MouseButtonEventArgs>(this, "MouseUp")
                    .Select(e => GetPoint(e.EventArgs)));


            RxMVVM.Point GetPoint(MouseEventArgs e)
            {
                var pos = e.GetPosition(c);
                return new RxMVVM.Point() { X = pos.X, Y = pos.Y };
            }
        }
    }
}