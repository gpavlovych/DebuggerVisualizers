using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace NumericArrayVisualizer
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

        public Point[] Points
        {
            get { return (Point[]) GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }

        public Point3D[] Points2D
        {
            get { return (Point3D[]) GetValue(Points2DProperty); }
            set { SetValue(Points2DProperty, value); }
        }

        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(Point[]),
                typeof(MainWindow), new FrameworkPropertyMetadata(default(Point[])));

        public static readonly DependencyProperty Points2DProperty =
            DependencyProperty.Register("Points2D", typeof(Point3D[]),
                typeof(MainWindow), new FrameworkPropertyMetadata(default(Point3D[])));

        protected override void OnRender(System.Windows.Media.DrawingContext dc)
        {
            if (Points != null)
            {
                var xmin = Points.Min(point => point.X);
                var xmax = Points.Max(point => point.X);
                var ymin = Points.Min(point => point.Y);
                var ymax = Points.Max(point => point.Y);

                foreach (var point in Points)
                {
                    dc.DrawRectangle(new SolidColorBrush(Colors.Red), new Pen(new SolidColorBrush(Colors.Red), 0.5),
                        new Rect(
                            new Point(
                                (point.X - xmin)*this.grd.ActualWidth/(xmax - xmin),
                                (point.Y - ymin)*this.grd.ActualHeight/(ymax - ymin)),
                            new Size(0.5, 0.5)));
                }
            }
            if (Points2D != null)
            {
                var xmin = Points2D.Min(point => point.X);
                var xmax = Points2D.Max(point => point.X);
                var ymin = Points2D.Min(point => point.Y);
                var ymax = Points2D.Max(point => point.Y);
                var zmin = Points2D.Min(point => point.Z);
                var zmax = Points2D.Max(point => point.Z);
                foreach (var point in Points2D)
                {
                    var val = (byte)((point.Z - zmin)*256/(zmax - zmin));
                    var color = Color.FromRgb(val, 0, 0);
                    dc.DrawRectangle(new SolidColorBrush(color), new Pen(new SolidColorBrush(color), 0.5),
                        new Rect(
                                new Point(
                                (point.X - xmin) * this.grd.ActualWidth / (xmax - xmin),
                                (point.Y - ymin) * this.grd.ActualHeight / (ymax - ymin)),
                            new Size(0.5, 0.5)));
                }
            }
        }
    }
}
