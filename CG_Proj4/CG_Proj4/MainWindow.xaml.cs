﻿using System;
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
        public MainWindow()
        {
            polyPoints = new List<Point>();
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
            //TODO: drawing random shapes
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            mainCanvas.Children.Clear();
        }
    }
}
