using ColorLibrary;
using PointsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DeBoorsSplines
{
    /// <summary>
    /// Класс рисования сплайна.
    /// </summary>
    public class DrawingClass
    {
        // Экземпляр класса ColorDialogs.
        private ColorDialogs colorDialogs = new ColorDialogs();

        /// <summary>
        /// Свойство, возвращающее и задающее значение начального цвета.
        /// </summary>
        public string StartingColor { set; get; }
        /// <summary>
        /// Свойство, возвращающее и задающее значение конечного цвета.
        /// </summary>
        public string EndingColor { set; get; }

        // Свойство, возвращающее и задающее значение ссылке на класс MainWindow.
        private MainWindow mainWindow { set; get; }
        // Свойство, возвращающее и задающее значение ссылке на класс
        // SplineCollection.
        private SplineCollection splineCollection { set; get; }

        /// <summary>
        /// Конструктор класса рисования сплайна.
        /// </summary>
        /// <param name="mainWindow">
        /// Экземпляр класса <see cref="MainWindow"/>.
        /// </param>
        /// <param name="splineCollection">
        /// Экземпляр класса <see cref="SplineCollection"/>.
        /// </param>
        public DrawingClass(MainWindow mainWindow,
            SplineCollection splineCollection)
        {
            this.mainWindow = mainWindow;
            this.splineCollection = splineCollection;

            StartingColor = "0,0,0";
            EndingColor = "190,190,190";
        }

        // Кисть для заливки окружностей, отмечающих на рабочем пространстве 
        // опорные точки.
        private static SolidColorBrush pointsBrush = new SolidColorBrush
        {
            Color = Color.FromRgb(254, 88, 92)
        };

        // Метод отрисовки опорных точек.
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

                Canvas.SetLeft(controlPoint,
                    splineCollection.PointsList[i].PointX - 4);
                Canvas.SetTop(controlPoint,
                    splineCollection.PointsList[i].PointY - 4);

                mainWindow.DrawingCanvas.Children.Add(controlPoint);
            }
        }

        /// <summary>
        /// Метод отрисовки индексов опорных точек.
        /// </summary>
        public void DrawIndexes()
        {
            for (int i = 0; i < splineCollection.PointsList.Count(); i++)
            {
                Label label = new Label()
                {
                    Content = i.ToString()
                };

                Canvas.SetLeft(label, splineCollection.PointsList[i].PointX - 20);
                Canvas.SetTop(label, splineCollection.PointsList[i].PointY - 20);

                mainWindow.DrawingCanvas.Children.Add(label);
            }
        }

        /// <summary>
        /// Метод, рисующий прямые между последовательными опорными точками.
        /// </summary>
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
                    Stroke = Brushes.LightSteelBlue,
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
        public void DrawSpline()
        {
            int partsAmount = (int)Math.Ceiling((double)
                splineCollection.PointsList.Count() / 4);

            List<Color> colors = colorDialogs.SetColors(StartingColor,
                EndingColor, partsAmount);

            int colorPicker = 0,
                cnt = 0;

            int pointsAmount = (int)Math.Ceiling(
                (splineCollection.SplinePointsList.Count() - 1) /
                (splineCollection.PointsList.Count() / 4.0));

            for (int i = 1; i < splineCollection.SplinePointsList.Count(); i++)
            {
                if (cnt >= pointsAmount - 1 && colorPicker + 1 < colors.Count())
                {
                    colorPicker++;
                    cnt = 0;
                }

                SolidColorBrush solidColorBrush = new SolidColorBrush
                {
                    Color = colors[colorPicker]
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
