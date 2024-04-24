namespace PaintApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Opaque, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
            using (var font = new Font("Times New Roman", 24))
            {
                e.Graphics.DrawString("Hello, World!", font, Brushes.DarkBlue, new PointF(10, 10));
            }
            base.OnPaint(e);
        }

    }
}