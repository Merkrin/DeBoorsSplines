using PointsLibrary;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ColorLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DeBoorsSplines
{
    /// <summary>
    /// Класс рисования сплайна.
    /// </summary>
    public class DrawingClass
    {
        private ColorDialogs colorDialogs = new ColorDialogs();

        public string StartingColor { set; get; }
        public string EndingColor { set; get; }

        private MainWindow mainWindow { set; get; }
        private SplineCollection splineCollection { set; get; }

        public DrawingClass(MainWindow mainWindow, SplineCollection splineCollection)
        {
            this.mainWindow = mainWindow;
            this.splineCollection = splineCollection;

            StartingColor = "#000000";
            EndingColor = "#bebebe";
        }

        // Кисть для заливки окружностей, отмечающих на рабочем пространстве 
        // опорные точки.
        private static SolidColorBrush pointsBrush = new SolidColorBrush {
            Color = System.Windows.Media.Color.FromRgb(254, 88, 92)
        };

        private void DrawControlPoints()
        {
            for (int i = 0; i < splineCollection.PointsList.Count(); i++)
            {
                Ellipse controlPoint = new Ellipse
                {
                    Fill = pointsBrush,
                    Width = 8,
                    Height = 8,
                };

                Canvas.SetLeft(controlPoint, splineCollection.PointsList[i].PointX - 4);
                Canvas.SetTop(controlPoint, splineCollection.PointsList[i].PointY - 4);

                mainWindow.DrawingCanvas.Children.Add(controlPoint);
            }
        }

        /// <summary>
        /// Метод, рисующий прямые между последовательными опорными точками.
        /// </summary>
        /// <param name="mainWindow">
        /// Объект класса MainWindow для работы с элементами интерфейса.
        /// </param>
        /// <param name="splineCollection">
        /// Объект класса-контейнера SplineCollection с информацией об элементах
        /// сплайна.
        /// </param>
        public void DrawControlLines()
        {
            for (int i = 1; i < splineCollection.PointsList.Count(); i++)
            {
                Line line = new Line
                {
                    X1 = splineCollection.PointsList[i - 1].PointX,
                    Y1 = splineCollection.PointsList[i - 1].PointY,
                    X2 = splineCollection.PointsList[i].PointX,
                    Y2 = splineCollection.PointsList[i].PointY,
                    Stroke = System.Windows.Media.Brushes.LightSteelBlue,
                    StrokeThickness = 2
                };

                mainWindow.DrawingCanvas.Children.Add(line);
            }

            DrawControlPoints();
        }

        /// <summary>
        /// Метод рисования сплайна, то есть линий между двумя последовательными
        /// точками сплайна.
        /// </summary>
        /// <param name="mainWindow">
        /// Объект класса MainWindow для работы с элементами интерфейса.
        /// </param>
        /// <param name="splineCollection">
        /// Объект класса-контейнера SplineCollection с информацией об элементах
        /// сплайна.
        /// </param>
        public void DrawSpline()
        {
            int partsAmount = (int)Math.Ceiling((double)
                splineCollection.PointsList.Count()/4);

            List<System.Drawing.Color> colors = colorDialogs.SetColors(StartingColor, EndingColor, partsAmount);

            int colorPicker = 0,
                cnt = 0;

            int pointsAmount = (int)Math.Ceiling((splineCollection.SplinePointsList.Count() - 1) / (splineCollection.PointsList.Count() / 4.0));

            //int pointsAmount = (int)Math.Floor((double)
            //    (splineCollection.SplinePointsList.Count()-1) /
            //    splineCollection.PointsList.Count())*4;

            for (int i = 2; i < splineCollection.SplinePointsList.Count(); i++)
            {
                if (/*(i-1) % (pointsAmount) == 0*/ cnt >= pointsAmount - 1 && colorPicker + 1 < colors.Count())
                {
                    //if (colorPicker < colors.Count() - 1)
                    colorPicker++;
                    cnt = 0;
                }

                SolidColorBrush solidColorBrush = new SolidColorBrush
                {
                    Color = System.Windows.Media.Color.FromRgb(colors[colorPicker].R, colors[colorPicker].G, colors[colorPicker].B)
                };

                    Line line = new Line
                    {
                        X1 = splineCollection.SplinePointsList[i - 1].PointX,
                        Y1 = splineCollection.SplinePointsList[i - 1].PointY,
                        X2 = splineCollection.SplinePointsList[i].PointX,
                        Y2 = splineCollection.SplinePointsList[i].PointY,
                        Stroke = solidColorBrush,
                        StrokeThickness = 3
                    };

                mainWindow.DrawingCanvas.Children.Add(line);

                cnt++;
            }
        }
    }
}
