using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CG_Proj4
{
    public class Algorithms
    {
        public static int pixelSize = 1;

        public static void DrawPixel(Canvas c, int x, int y)
        {
            Ellipse rec = new Ellipse();
            Canvas.SetTop(rec, y);
            Canvas.SetLeft(rec, x);
            rec.Width = pixelSize;
            rec.Height = pixelSize;
            rec.Fill = new SolidColorBrush(Colors.Black);
            c.Children.Add(rec);
        }

        public static void DrawPixel(Canvas c, int x, int y, int color)
        {
            Ellipse rec = new Ellipse();
            Canvas.SetTop(rec, y);
            Canvas.SetLeft(rec, x);
            rec.Width = pixelSize;
            rec.Height = pixelSize;

            int r = (color >> 16) & 0xff;
            int g = (color >> 8) & 0xff;
            int b = color & 0xff;

            rec.Fill = new SolidColorBrush(Color.FromRgb((byte)r, (byte)g, (byte)b));
            c.Children.Add(rec);
        }

        public static void DrawPixel(Canvas c, int x, int y, Color color)
        {
            Ellipse rec = new Ellipse();
            Canvas.SetTop(rec, y);
            Canvas.SetLeft(rec, x);
            rec.Width = pixelSize;
            rec.Height = pixelSize;

            rec.Fill = new SolidColorBrush(color);
            c.Children.Add(rec);
        }

        public static Color GetPixelColor(Visual visual, double x, double y)
        {
            Point pt = new Point(x, y);

            Point ptDpi = getScreenDPI(visual);

            Size srcSize = VisualTreeHelper.GetDescendantBounds(visual).Size;

            Rect percentSrcRec = new Rect(pt.X / srcSize.Width, pt.Y / srcSize.Height,
                                          1 / srcSize.Width, 1 / srcSize.Height);

            var bmpOut = new RenderTargetBitmap((int)(ptDpi.X / 96d),
                                                (int)(ptDpi.Y / 96d),
                                                ptDpi.X, ptDpi.Y, PixelFormats.Default);

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                dc.DrawRectangle(new VisualBrush { Visual = visual, Viewbox = percentSrcRec },
                                 null,
                                 new Rect(0, 0, 1d, 1d));
            }
            bmpOut.Render(dv);

            var bytes = new byte[4];
            int iStride = 4;
            bmpOut.CopyPixels(bytes, iStride, 0);

            return Color.FromArgb(bytes[0], bytes[1], bytes[2], bytes[3]);
        }
        public static Color GetPixelColor(Visual visual, Point pt)
        {
            Point ptDpi = getScreenDPI(visual);

            Size srcSize = VisualTreeHelper.GetDescendantBounds(visual).Size;

            Rect percentSrcRec = new Rect(pt.X / srcSize.Width, pt.Y / srcSize.Height,
                                          1 / srcSize.Width, 1 / srcSize.Height);

            var bmpOut = new RenderTargetBitmap((int)(ptDpi.X / 96d),
                                                (int)(ptDpi.Y / 96d),
                                                ptDpi.X, ptDpi.Y, PixelFormats.Default);

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                dc.DrawRectangle(new VisualBrush { Visual = visual, Viewbox = percentSrcRec },
                                 null,
                                 new Rect(0, 0, 1d, 1d));
            }
            bmpOut.Render(dv);

            var bytes = new byte[4];
            int iStride = 4;
            bmpOut.CopyPixels(bytes, iStride, 0);

            return Color.FromArgb(bytes[0], bytes[1], bytes[2], bytes[3]);
        }

        public static Point getScreenDPI(Visual v)
        {
            PresentationSource source = PresentationSource.FromVisual(v);
            Point ptDpi;
            if (source != null)
            {
                ptDpi = new Point(96.0 * source.CompositionTarget.TransformToDevice.M11,
                                   96.0 * source.CompositionTarget.TransformToDevice.M22);
            }
            else
                ptDpi = new Point(96d, 96d);
            return ptDpi;
        }

        private static bool ColorMatch(Color a, Color b)
        {
            return (a.Equals(b));
        }

        public static void FloodFill4X(Canvas c, Point pt, Color targetColor, Color replacementColor)
        {
            Stack<Point> pixels = new Stack<Point>();
            targetColor = GetPixelColor(c, pt);
            pixels.Push(pt);

            while (pixels.Count > 0)
            {
                Point a = pixels.Pop();
                if (a.X < pt.X+400 && a.X > pt.X - 400 && a.Y < pt.Y+400 && a.Y > pt.Y-400)
                {
                    if (GetPixelColor(c, a) == targetColor)
                    {
                        DrawPixel(c, (int)a.X, (int)a.Y, replacementColor);
                        pixels.Push(new Point(a.X - 1, a.Y));
                        pixels.Push(new Point(a.X + 1, a.Y));
                        pixels.Push(new Point(a.X, a.Y - 1));
                        pixels.Push(new Point(a.X - 1, a.Y + 1));
                    }
                }
            }
        }

        public static void FloodFill(Canvas c, Point pt, Color targetColor, Color replacementColor)
        {
            Queue<Point> q = new Queue<Point>();
            q.Enqueue(pt);
            while (q.Count > 0)
            {
                Point n = q.Dequeue();
                if (!ColorMatch(GetPixelColor(c, n.X, n.Y), targetColor))
                    continue;
                Point w = n, e = new Point(n.X + 1, n.Y);
                while ((w.X >= 0) && ColorMatch(GetPixelColor(c, w.X, w.Y), targetColor))
                {
                    DrawPixel(c, (int)w.X, (int)w.Y, replacementColor);
                    if ((w.Y > 0) && ColorMatch(GetPixelColor(c, w.X, w.Y-1), targetColor))
                        q.Enqueue(new Point(w.X, w.Y - 1));
                    if ((w.Y < pt.Y + 400 - 1) && ColorMatch(GetPixelColor(c, w.X, w.Y + 1), targetColor))
                        q.Enqueue(new Point(w.X, w.Y + 1));
                    w.X--;
                }
                while ((e.X <= pt.X + 400 - 1) && ColorMatch(GetPixelColor(c, e.X, e.Y), targetColor))
                {
                    DrawPixel(c, (int)e.X, (int)e.Y, replacementColor);
                    if ((e.Y > 0) && ColorMatch(GetPixelColor(c, e.X, e.Y-1), targetColor))
                        q.Enqueue(new Point(e.X, e.Y - 1));
                    if ((e.Y < pt.X + 400 - 1) && ColorMatch(GetPixelColor(c, e.X, e.Y+1), targetColor))
                        q.Enqueue(new Point(e.X, e.Y + 1));
                    e.X++;
                }
            }
        }
    }
}