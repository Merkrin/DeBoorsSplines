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

        public void ScaleSpline(MainWindow mainWindow, SplineCollection splineCollection)
        {
            double oldWidth = mainWindow.DeBoorsSplinesAppWindow.ActualWidth / 100 * 70,
                oldHeight = mainWindow.DrawingCanvas.Height - 10 - 25 - 10-mainWindow.MenuStrip.ActualHeight,
                newWidth = 0,
                newHeight = 0;

            if (splineCollection.PointsList[ScalingPointIndex].PointX >= oldWidth)
            {
                newWidth = splineCollection.PointsList[ScalingPointIndex].PointX+30;

                newHeight = oldHeight * newWidth / oldWidth;
            }
            else
            {
                newHeight = splineCollection.PointsList[ScalingPointIndex].PointY+30;

                newWidth = oldWidth * newHeight / oldHeight;
            }

            double coeff = oldWidth / newWidth;

            for (int i = 0; i < splineCollection.PointsList.Count(); i++)
            {
                splineCollection.PointsList[i].PointX = (int)(coeff* splineCollection.PointsList[i].PointX);
                splineCollection.PointsList[i].PointY = (int)(coeff* splineCollection.PointsList[i].PointY);
            }
        }
    }
}
