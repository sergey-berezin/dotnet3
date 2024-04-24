using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reactive.Linq;

namespace RxMVVM
{
    public class MainViewModel
    {
        public ObservableCollection<PointViewModel> Points { get; } =
            new ObservableCollection<PointViewModel>();

        public MainViewModel(IObservable<Point> MouseMove, IObservable<Point> MouseDown, IObservable<Point> MouseUp)
        {
            var draw = 
                from d in MouseDown 
                    from p in MouseMove.StartWith(d).TakeUntil(MouseUp) 
                        select p;

            draw.Subscribe(p => Points.Add(new PointViewModel() { X = (int)p.X, Y = (int)p.Y }));
        }
    }

    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }   
    }

    public class PointViewModel
    {
        public int X { get; init; }

        public int Y { get; init; }
        public int Left => X - Width / 2;
        public int Top => Y - Height / 2;

        public int Width => 20;

        public int Height => 20;
    }
}


