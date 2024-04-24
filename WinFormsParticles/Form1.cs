using Particles;
using System.Net.Http.Headers;

namespace WinFormsParticles
{
    public partial class Form1 : Form
    {
        private ParticleSystem particles = new ParticleSystem(50000);
        private DateTime prevTime = DateTime.MinValue;
        private Queue<double> fps = new Queue<double>();

        public Form1()
        {
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            const int Radius = 20;

            e.Graphics.Clear(Color.White);
            int size = Math.Min(ClientRectangle.Width, ClientRectangle.Height);
            int left = (ClientRectangle.Width - size) / 2;
            int top = (ClientRectangle.Height - size) / 2;
            foreach(var p in particles.Particles) 
            {
                int x = (int)(left + size * p.X);
                int y = (int)(top + size * p.Y);
                e.Graphics.FillEllipse(Brushes.Blue, x - Radius / 2, y - Radius / 2, Radius, Radius);
            }

            particles = particles.Animate(0.01);
            Invalidate();

            var now = DateTime.Now;
            if (prevTime != DateTime.MinValue)
            {
                fps.Enqueue(1 / (now - prevTime).TotalSeconds);
                if(fps.Count > 10)
                {
                    fps.Dequeue();
                }
                Text = string.Format("Particles: FPS = {0:00.00}", fps.Average());
            }
            prevTime = now;

            base.OnPaint(e);
        }
    }
}