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
        public static void FloodFill4X(WriteableBitmap bmp, Point pt, Color target, Color repl)
        {
            Stack<Point> pxStack = new Stack<Point>();
            target = bmp.GetPixel((int)pt.X, (int)pt.Y);

            pxStack.Push(pt);

            while (pxStack.Count > 0)
            {
                Point a = pxStack.Pop();

                if (a.X < bmp.Width && a.X > 0 && a.Y > 0 && a.Y < bmp.Height)
                {
                    if (bmp.GetPixel((int)a.X, (int)a.Y) == target)
                    {
                        bmp.SetPixel((int)a.X, (int)a.Y, repl);
                        pxStack.Push(new Point(a.X - 1, a.Y));
                        pxStack.Push(new Point(a.X + 1, a.Y));
                        pxStack.Push(new Point(a.X, a.Y - 1));
                        pxStack.Push(new Point(a.X, a.Y + 1));
                    }
                }
            }
            
        }

        public static void FloodFill8X(WriteableBitmap bmp, Point pt, Color target, Color repl)
        {
            Stack<Point> pxStack = new Stack<Point>();
            target = bmp.GetPixel((int)pt.X, (int)pt.Y);

            pxStack.Push(pt);

            while (pxStack.Count > 0)
            {
                Point a = pxStack.Pop();

                if (a.X < bmp.Width && a.X > 0 && a.Y > 0 && a.Y < bmp.Height)
                {
                    if (bmp.GetPixel((int)a.X, (int)a.Y) == target)
                    {
                        bmp.SetPixel((int)a.X, (int)a.Y, repl);

                        pxStack.Push(new Point(a.X - 1, a.Y + 1));
                        pxStack.Push(new Point(a.X - 1, a.Y));
                        pxStack.Push(new Point(a.X + 1, a.Y));
                        pxStack.Push(new Point(a.X, a.Y - 1));
                        pxStack.Push(new Point(a.X - 1, a.Y - 1));
                        pxStack.Push(new Point(a.X + 1, a.Y + 1));
                        pxStack.Push(new Point(a.X, a.Y + 1));
                        pxStack.Push(new Point(a.X + 1, a.Y - 1));
                    }
                }
            }

        }

        public static void Scanline(WriteableBitmap bmp, Point pt, Color target, Color repl)
        {
            Stack<Point> pxStack = new Stack<Point>();

            target = bmp.GetPixel((int)pt.X, (int)pt.Y);

            int ymax;
            bool left;
            bool right;

            pxStack.Push(pt);

            while (pxStack.Count != 0) {
                Point a = pxStack.Pop();
                ymax = (int) a.Y;

                while (ymax > 0 && bmp.GetPixel((int)a.X, ymax) == target)
                {
                    ymax--;
                }

                ymax++;

                left = false;
                right = false;

                while (ymax < bmp.Height && bmp.GetPixel((int)a.X, ymax) == target)
                {
                    bmp.SetPixel((int)a.X, ymax, repl);

                    if (!right && a.X < bmp.Width - 1 && bmp.GetPixel((int)a.X + 1, ymax) == target)
                    {
                        pxStack.Push(new Point(a.X + 1, ymax));
                        right = true;
                    }
                    else if (right && a.X < bmp.Width - 1 && bmp.GetPixel((int)a.X + 1, ymax) != target)
                    {
                        right = false;
                    }
                    if (!left && a.X > 0 && bmp.GetPixel((int)a.X - 1, ymax) == target)
                    {
                        pxStack.Push(new Point(a.X - 1, ymax));
                        left = true;
                    }
                    else if (left && a.X - 1 == 0 && bmp.GetPixel((int)a.X - 1, ymax) != target)
                    {
                        left = false;
                    }
                    
                    ymax++;
                }

            }

        }
    }
}