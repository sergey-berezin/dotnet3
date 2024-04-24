using Particles;
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

namespace WPFParticles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ParticleSystem p = new ParticleSystem(500);
        private Ellipse[] e;
        private DateTime prevTime = DateTime.MinValue;
        private Queue<double> recentFps = new Queue<double>();

        const double Radius = 10;

        public MainWindow()
        {
            InitializeComponent();


            e = new Ellipse[p.Particles.Length];
            for (int i = 0; i < e.Length; i++)
            {
                e[i] = new Ellipse
                {
                    Width = Radius * 2,
                    Height = Radius * 2,
                    Fill = Brushes.Blue,
                    ToolTip = $"Particle #{i+1}"
                };
                e[i].MouseLeftButtonDown += (s,a) => MessageBox.Show("Click!");
                canvas.Children.Add(e[i]);
            }

            SetPositions();

            CompositionTarget.Rendering += (s, a) =>
            {
                p = p.Animate(0.01);
                SetPositions();
                var now = DateTime.Now;
                if (prevTime != DateTime.MinValue)
                {
                    recentFps.Enqueue(1 / (now - prevTime).TotalSeconds);
                    if(recentFps.Count > 10)
                    {
                        recentFps.Dequeue();
                    }
                    fps.Text = String.Format("FPS = {0:00.00}", recentFps.Average());
                }
                prevTime = now;
            };
        }

        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SetPositions()
        {
            double size = Math.Min(canvas.RenderSize.Width, canvas.RenderSize.Height);
            double left = (canvas.RenderSize.Width - size) / 2;
            double top = (canvas.RenderSize.Height - size) / 2;
            foreach (var i in p.Particles.Zip(e))
            {
                Canvas.SetLeft(i.Second, left + i.First.X * size - Radius);
                Canvas.SetTop(i.Second, top + i.First.Y * size - Radius);
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            SetPositions();
        }
    }
}
