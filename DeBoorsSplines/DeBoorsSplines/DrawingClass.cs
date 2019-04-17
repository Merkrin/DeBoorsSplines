using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PointsLibrary;

namespace DeBoorsSplines
{
    public class DrawingClass
    {
        private static SolidColorBrush pointsBrush = new SolidColorBrush { Color = Color.FromRgb(254, 88, 92)};

        private void DrawControlPoints(MainWindow mainWindow, SplineCollection splineCollection)
        {
            for(int i = 0; i < splineCollection.PointsList.Count(); i++)
            {
                Ellipse controlPoint = new Ellipse
                {
                    Fill = pointsBrush,
                    Width = 8,
                    Height = 8,
                };

                Canvas.SetLeft(controlPoint, splineCollection.PointsList[i].PointX-4);
                Canvas.SetTop(controlPoint, splineCollection.PointsList[i].PointY-4);

                mainWindow.DrawingCanvas.Children.Add(controlPoint);
            }
        }

        public void DrawControlLines(MainWindow mainWindow, SplineCollection splineCollection)
        {
            for (int i = 1; i < splineCollection.PointsList.Count(); i++)
            {
                Line line = new Line
                {
                    X1 = splineCollection.PointsList[i-1].PointX,
                    Y1 = splineCollection.PointsList[i - 1].PointY,
                    X2 = splineCollection.PointsList[i].PointX,
                    Y2 = splineCollection.PointsList[i].PointY,
                    Stroke = Brushes.LightSteelBlue,
                    StrokeThickness = 2
                };

                mainWindow.DrawingCanvas.Children.Add(line);
            }

            DrawControlPoints(mainWindow, splineCollection);
        }

        public void DrawSpline(MainWindow mainWindow, SplineCollection splineCollection)
        {
            for(int i = 1; i < splineCollection.SplinePointsList.Count(); i++)
            {
                Line line = new Line
                {
                    X1 = splineCollection.SplinePointsList[i - 1].PointX,
                    Y1 = splineCollection.SplinePointsList[i - 1].PointY,
                    X2 = splineCollection.SplinePointsList[i].PointX,
                    Y2 = splineCollection.SplinePointsList[i].PointY,
                    Stroke = Brushes.Black,
                    StrokeThickness = 3
                };

                mainWindow.DrawingCanvas.Children.Add(line);
            }
        }
    }
}
