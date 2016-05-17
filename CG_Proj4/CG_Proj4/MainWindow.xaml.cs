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
        private bool mouseClicked = true;
        public MainWindow()
        {
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

            X1.Text = coordsX.Substring(3);
            Y1.Text = coordsY.Substring(3);

            mouseClicked = true;
        }
    }
}
