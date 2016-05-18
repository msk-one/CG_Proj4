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

namespace CG_Proj4
{
    public partial class MainWindow : Window
    {
        public List<Point> polyPoints { get; set; }
        private bool mouseClicked = true;
        private Random rnd;

        public WriteableBitmap writeableBmp;
        public MainWindow()
        {
            polyPoints = new List<Point>();
            rnd = new Random(12345);

            InitializeComponent();

            writeableBmp = BitmapFactory.New(835, 718);

            mainImage.Source = writeableBmp;
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseClicked)
            {
                coord_X.Content = "X: " + e.GetPosition(mainImage).X;
                coord_Y.Content = "Y: " + e.GetPosition(mainImage).Y;
            }
        }

        private void mainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (mouseClicked) { mouseClicked = false; }
            else { mouseClicked = true; }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string coordsX = coord_X.Content as string;
            string coordsY = coord_Y.Content as string;

            int cX = Convert.ToInt32(coordsX.Substring(3));
            int cY = Convert.ToInt32(coordsY.Substring(3));

            polyPoints.Add(new Point(cX, cY));

            mouseClicked = true;
        }

        private void line_button_Click(object sender, RoutedEventArgs e)
        {
            Polygon myPolygon = new Polygon();
            myPolygon.Stroke = System.Windows.Media.Brushes.Black;
            myPolygon.StrokeThickness = 2;
            myPolygon.HorizontalAlignment = HorizontalAlignment.Left;
            myPolygon.VerticalAlignment = VerticalAlignment.Center;

            PointCollection myPointCollection = new PointCollection(polyPoints);

            myPolygon.Points = myPointCollection;

            int[] points = new int[polyPoints.Count*2];

            for (int i = 0; i < polyPoints.Count * 2; i+=2)
            {
                points[i] = (int) polyPoints[i/2].X;
                points[i + 1] = (int) polyPoints[i/2].Y;
            }

            int[] p = new int[] { 200, 150, 120, 340, 130, 330, 170, 185, 100, 50 };
            writeableBmp.DrawPolyline(p, Color.FromRgb(0,0,0));

            polyPoints.Clear();
        }

        private void circle_button_Click(object sender, RoutedEventArgs e)
        {
            string coordsX = coord_X.Content as string;
            string coordsY = coord_Y.Content as string;

            int cX = Convert.ToInt32(coordsX.Substring(3));
            int cY = Convert.ToInt32(coordsY.Substring(3));

            DrawShape(new Point(cX, cY));
        }

        private void DrawShape(Point p)
        {
            int h = rnd.Next(100, 400);
            int w = rnd.Next(100, 400);

            writeableBmp.DrawRectangle((int)p.X, (int)p.Y, 120, 100, Colors.Black);

            //Shape shape = new System.Windows.Shapes.Rectangle
            //{
            //    Stroke = Brushes.Black,
            //    StrokeThickness = 2,
            //    Height = h,
            //    Width = w
            //};

            //Shape el_shape = new System.Windows.Shapes.Ellipse
            //{
            //    Stroke = Brushes.Black,
            //    StrokeThickness = 2,
            //    Height = h,
            //    Width = w
            //};

            //List<Shape> shList = new List<Shape>();
            //shList.Add(shape);
            //shList.Add(el_shape);

            //Shape selShape = shList[rnd.Next(0, 2)];
            //mainCanvas.Children.Add(selShape);
            //Canvas.SetLeft(selShape, p.X - w);
            //Canvas.SetTop(selShape, p.Y - h);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            writeableBmp.Clear();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string coordsX = coord_X.Content as string;
            string coordsY = coord_Y.Content as string;

            int cX = Convert.ToInt32(coordsX.Substring(3));
            int cY = Convert.ToInt32(coordsY.Substring(3));

            switch (filling_type.Text)
            {
                case "Scanline #1":
                    Point stPt0 = new Point(cX, cY);
                    Color target0 = Colors.White;
                    Color repl0 = Colors.Blue;

                    Algorithms.FloodFill4X(writeableBmp, stPt0, target0, repl0);
                    break;
                case "Floodfill 4X":
                    Point stPt = new Point(cX, cY);
                    Color target = Colors.White;
                    Color repl = Colors.Blue;

                    Algorithms.FloodFill4X(writeableBmp, stPt, target, repl);
                    break;
                case "Floodfill 8X":
                    Point stPt2 = new Point(cX, cY);
                    Color target2 = Colors.White;
                    Color repl2 = Colors.Blue;

                    Algorithms.FloodFill8X(writeableBmp, stPt2, target2, repl2);
                    break;
            }
        }
    }
}
