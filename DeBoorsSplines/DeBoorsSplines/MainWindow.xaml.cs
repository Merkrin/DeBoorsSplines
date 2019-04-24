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
using PointsLibrary;
using SplineMathsLibrary;

namespace DeBoorsSplines
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileParser fileParser = new FileParser();
        private OpenSaveDialogs openSaveDialogs = new OpenSaveDialogs();
        private SplineCollection splineCollection = new SplineCollection();
        private KnotsClass knotsClass = new KnotsClass();
        private SplineMaker splineMaker = new SplineMaker();
        private DrawingClass drawingClass = new DrawingClass();

        public MainWindow()
        {
            InitializeComponent();

            DeBoorsSplinesAppWindow.MinHeight = 
                SystemParameters.PrimaryScreenHeight / 3 * 2;
            DeBoorsSplinesAppWindow.MinWidth = 
                SystemParameters.PrimaryScreenWidth / 3 * 2;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainGrid.Height = ButtonsGrid.Height = DrawingCanvas.Height 
                = DeBoorsSplinesAppWindow.ActualHeight;

            MainGrid.Width = DeBoorsSplinesAppWindow.ActualWidth;

            // Канвас во весь экран по высоте и на 70% по ширине
            int canvasWidth = 
                (int)(DeBoorsSplinesAppWindow.ActualWidth / 100 * 70);
            DrawingCanvas.Margin = 
                new Thickness(0, 0, MainGrid.Width-canvasWidth, 0);

            ButtonsGrid.Width = MainGrid.Width - canvasWidth;
            ButtonsGrid.Margin = new Thickness(canvasWidth+5, 0, 0, 0);
        }

        private void AddPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AddPointTextBox.Text))
            {

            }
        }

        private void MakeSpline()
        {
            knotsClass.CalculateKnotsVektor(splineCollection.PointsList.Count(), splineCollection);
            splineMaker.SetSplineCurve(splineCollection.PointsList.Count(), splineCollection);
            drawingClass.DrawSpline(this, splineCollection);
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Children.Clear();
            openSaveDialogs.OpenFile(this, fileParser, splineCollection);
            
            if(openSaveDialogs.OnPointsRenewer != null)
            {
                openSaveDialogs.OnPointsRenewer?.Invoke(this, splineCollection);
                MakeSpline();
            }
        }

        private void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (splineCollection.PointsList == null)
            //    splineCollection.PointsList = new List<PointsLibrary.Point>();

            //splineCollection.PointsList.Add(new PointsLibrary.Point(e.GetPosition(this).X, e.GetPosition(this).Y));

            //if (splineCollection.PointsList.Count() >= 4)
            //{
            //    drawingClass.DrawControlLines(this, splineCollection);
            //    knotsClass.CalculateKnotsVektor(splineCollection.PointsList.Count(), splineCollection);
            //    splineMaker.SetSplineCurve(splineCollection.PointsList.Count(), splineCollection);
            //    drawingClass.DrawSpline(this, splineCollection);
            //}
        }
    }
}
