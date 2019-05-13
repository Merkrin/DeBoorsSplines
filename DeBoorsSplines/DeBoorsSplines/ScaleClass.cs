using PointsLibrary;
using System;
using System.Linq;

namespace DeBoorsSplines
{
    public class ScaleClass
    {
        private int ScalingPointIndex { set; get; }
        private double MaxLength { set; get; }

        public bool CheckScaling(MainWindow mainWindow, SplineCollection splineCollection)
        {
            for (int i = 0; i < splineCollection.PointsList.Count(); i++)
            {
                if (splineCollection.PointsList[i].PointX >=
                    mainWindow.DeBoorsSplinesAppWindow.ActualWidth / 100 * 70 ||
                    splineCollection.PointsList[i].PointY >=
                    mainWindow.DrawingCanvas.Height - 10 - 25 - 10)
                {
                    CheckLength(splineCollection, i);
                }
            }

            if (MaxLength > 0)
            {
                MaxLength = 0;
                return true;
            }

            return false;
        }

        private void CheckLength(SplineCollection splineCollection, int index)
        {
            double x = splineCollection.PointsList[index].PointX,
                y = splineCollection.PointsList[index].PointY;

            if (Math.Sqrt(x * x + y * y) > MaxLength)
            {
                MaxLength = Math.Sqrt(x * x + y * y);

                ScalingPointIndex = index;
            }
        }

        private void ChangeSplineX(SplineCollection splineCollection)
        {
            int indexOfTheClosest = 0;

            // Модуль - расстояние от начала координат к х-координате точки
            double module = splineCollection.PointsList[indexOfTheClosest].PointX;
            
            for(int i = 1; i < splineCollection.PointsList.Count(); i++)
            {
                if (splineCollection.PointsList[i].PointX < module)
                {
                    module = splineCollection.PointsList[i].PointX;

                    indexOfTheClosest = i;
                }
            }

            double idealX = 4,
                changeOfX = splineCollection.PointsList[indexOfTheClosest].PointX - idealX;

            for (int i = 0; i < splineCollection.PointsList.Count(); i++)
            {
                if(i != indexOfTheClosest)
                {
                    splineCollection.PointsList[i].PointX -= changeOfX;
                }
                else
                {
                    splineCollection.PointsList[i].PointX = idealX;
                }
            }
        }

        private void ChangeSplineY(SplineCollection splineCollection)
        {
            int indexOfTheClosest = 0;

            // Модуль - расстояние от начала координат к х-координате точки
            double module = splineCollection.PointsList[indexOfTheClosest].PointY;

            for (int i = 1; i < splineCollection.PointsList.Count(); i++)
            {
                if (splineCollection.PointsList[i].PointY < module)
                {
                    module = splineCollection.PointsList[i].PointY;

                    indexOfTheClosest = i;
                }
            }

            double idealY = 4,
                changeOfY = splineCollection.PointsList[indexOfTheClosest].PointY - idealY;

            for (int i = 0; i < splineCollection.PointsList.Count(); i++)
            {
                if (i != indexOfTheClosest)
                {
                    splineCollection.PointsList[i].PointY -= changeOfY;
                }
                else
                {
                    splineCollection.PointsList[i].PointY = idealY;
                }
            }
        }

        public void ScaleSpline(MainWindow mainWindow, SplineCollection splineCollection)
        {
            ChangeSplineX(splineCollection);
            ChangeSplineY(splineCollection);

            double oldWidth = mainWindow.DeBoorsSplinesAppWindow.ActualWidth / 100 * 70,
                oldHeight = mainWindow.DrawingCanvas.Height - 65 -mainWindow.MenuStrip.ActualHeight,
                newWidth = 0,
                newHeight = 0;

            if (splineCollection.PointsList[ScalingPointIndex].PointX >= oldWidth)
            {
                newWidth = splineCollection.PointsList[ScalingPointIndex].PointX+8;

                //newHeight = oldHeight * newWidth / oldWidth;
            }
            if(splineCollection.PointsList[ScalingPointIndex].PointY >= oldHeight)
            {
                newHeight = splineCollection.PointsList[ScalingPointIndex].PointY+8;

                //newWidth = oldWidth * newHeight / oldHeight;
            }

            double coeffX = oldWidth / newWidth,
                coeffY = oldHeight/newHeight;

            for (int i = 0; i < splineCollection.PointsList.Count(); i++)
            {
                splineCollection.PointsList[i].PointX = (int)(coeffX* splineCollection.PointsList[i].PointX);
                splineCollection.PointsList[i].PointY = (int)(coeffY* splineCollection.PointsList[i].PointY);
            }
        }
    }
}
