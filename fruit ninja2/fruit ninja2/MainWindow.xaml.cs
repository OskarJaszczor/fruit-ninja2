using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;

namespace fruit_ninja2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Rectangle> rectangles = new List<Rectangle>();
        Random random = new Random();
        int gameScore = 0;

        public void CreateAndMovementFruit()
        {
            Thread owoc = new Thread(() =>
            {
                double width = random.Next(50, 100);
                double height = random.Next(50, 100);


                double beginingHorizontalVelocity = random.Next(5, 10);
                double beginingVerticalVelocity = -random.Next(15, 20);
                double gravity = 9.8;
                double time = 0;
                Rectangle r = null;
                Dispatcher.Invoke(new Action(() =>
                {
                    r = new Rectangle();

                    SolidColorBrush fillColor = new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)));
                    Point position = new Point(random.Next(0, (int)this.Width) - 100, random.Next(500, 550));
                    Vector velocity = new Vector(0, 0); // Możesz dostosować prędkość początkową wertykalną
                    r.Width = width;
                    r.Height = height;
                    r.Fill = fillColor;
                    Canvas.SetLeft(r, random.Next(0, (int)this.Width) - 500);
                    Canvas.SetTop(r, random.Next(490- 100, 395));
                    //MyRectangle myRectangle = new MyRectangle(width, height, fillColor, position, velocity);
                    //rectangles.Add(myRectangle);
                    rectangles.Add(r);
                    myCanvas.Children.Add(r);
                }));

                while (true)
                    {
                    double x = 0;
                    double y = 0; 
                    Thread.Sleep(21);
                    Dispatcher.Invoke(new Action(() =>
                    {
                        x = Canvas.GetLeft(r);
                        y = Canvas.GetTop(r);

                    }));

                    double newX = x + beginingHorizontalVelocity * time;
                    double newY = y + (beginingVerticalVelocity * time) + (0.5 * gravity * time * time);

                    Dispatcher.Invoke(new Action(() =>
                    {
                        Canvas.SetLeft(r, newX);
                        Canvas.SetTop(r, newY);
                    }));
                    time += 0.1;
                    }
                

            });

            owoc.Start();

        }

        public void FruitMovement(Rectangle r)
        {

        }

        public void FruitSplit(Rectangle r)
        {
            gameScore++;
            score.Text = gameScore.ToString();

        }

        public MainWindow()
        {
            InitializeComponent();


            for(int i = 0; i < 2; i++)
            {
                CreateAndMovementFruit();
            }
            

        }

        private void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var canvas = sender as Canvas;
            HitTestResult hitTest = VisualTreeHelper.HitTest(canvas, e.GetPosition(canvas));
            var element = hitTest.VisualHit;

            if (element.GetType() == typeof(Rectangle))
            {
                FruitSplit((Rectangle)element);
            }
        }

        private void myCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
