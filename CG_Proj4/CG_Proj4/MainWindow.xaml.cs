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
        public MainWindow()
        {
            polyPoints = new List<Point>();
            rnd = new Random(12345);
            InitializeComponent();
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseClicked)
            {
                coord_X.Content = "X: " + e.GetPosition(mainCanvas).X;
                coord_Y.Content = "Y: " + e.GetPosition(mainCanvas).Y;
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

            //int maxPolySize = 300;

            //int startX = Convert.ToInt32(X1.Text);
            //int startY = Convert.ToInt32(Y1.Text);

            //int vCount = Convert.ToInt32(verticesCount.Text);
            //if (vCount < 3)
            //{
            //    vCount = 3;
            //    verticesCount.Text = "3";
            //}

            //Random rn = new Random();
            //polyPoints = new List<Point>();
            //for (int i = 0; i < vCount; i++)
            //{

            //    int tempX = rn.Next(startX - maxPolySize, startX + maxPolySize);
            //    int tempY = rn.Next(startY - maxPolySize, startY + maxPolySize);

            //    polyPoints.Add(new Point(tempX, tempY));
            //}

            Polygon myPolygon = new Polygon();
            myPolygon.Stroke = System.Windows.Media.Brushes.Black;
            myPolygon.StrokeThickness = 2;
            myPolygon.HorizontalAlignment = HorizontalAlignment.Left;
            myPolygon.VerticalAlignment = VerticalAlignment.Center;

            PointCollection myPointCollection = new PointCollection(polyPoints);

            myPolygon.Points = myPointCollection;

            mainCanvas.Children.Add(myPolygon);
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

            Shape shape = new System.Windows.Shapes.Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Height = h,
                Width = w
            };

            Shape el_shape = new System.Windows.Shapes.Ellipse
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Height = h,
                Width = w
            };

            List<Shape> shList = new List<Shape>();
            shList.Add(shape);
            shList.Add(el_shape);

            Shape selShape = shList[rnd.Next(0, 2)];
            mainCanvas.Children.Add(selShape);
            Canvas.SetLeft(selShape, p.X - w);
            Canvas.SetTop(selShape, p.Y - h);
        }

        public WriteableBitmap toBmp(Canvas canvas)
        {
            Transform transform = canvas.LayoutTransform;
            canvas.LayoutTransform = null;
            Size size = new Size(canvas.Width, canvas.Height);
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));
            RenderTargetBitmap renderBitmap =
              new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);

            renderBitmap.Render(canvas);        
            canvas.LayoutTransform = transform;
        
            return new WriteableBitmap(renderBitmap);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            mainCanvas.Children.Clear();
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

                    break;
                case "Floodfill 4X":
                    Point stPt = new Point(cX, cY);
                    Color target = Colors.White;
                    Color repl = Colors.Violet;

                    Algorithms.FloodFill(mainCanvas, stPt, target, repl);
                    break;
                case "Floodfill 8X":

                    break;
            }
        }
    }
}
