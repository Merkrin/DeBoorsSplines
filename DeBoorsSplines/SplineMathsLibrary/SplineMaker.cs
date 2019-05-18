using PointsLibrary;
using System.Collections.Generic;

namespace SplineMathsLibrary
{
    /// <summary>
    /// Класс создания сплайна.
    /// </summary>
    public class SplineMaker
    {
        // Постоянное значение степени сплайна.
        private const int degree = 3;
        private SplineCollection splineCollection { set; get; }

        public SplineMaker(SplineCollection splineCollection)
        {
            this.splineCollection = splineCollection;
        }

        // Метод подсчёта коэффициента N_i_p
        private double CalculateN(int controlPoint, double parameter)
        {
            double[] N = new double[degree + 1];
            double savedNumber,
                temporaryNumber;
            int knotsAmount = splineCollection.KnotsVector.Length - 1;

            if (controlPoint == 0 && parameter == splineCollection.KnotsVector[0]
                || controlPoint == (knotsAmount - degree - 1)
                && parameter == splineCollection.KnotsVector[knotsAmount])
            {
                return 1;
            }

            if (parameter < splineCollection.KnotsVector[controlPoint]
                || parameter >= splineCollection.KnotsVector[controlPoint + degree + 1])
            {
                return 0;
            }

            for (int i = 0; i <= degree; i++)
            {
                if (parameter >= splineCollection.KnotsVector[controlPoint + i]
                    && parameter < splineCollection.KnotsVector[controlPoint + i + 1])
                {
                    N[i] = 1;
                }
                else
                {
                    N[i] = 0;
                }
            }

            for (int i = 1; i <= degree; i++)
            {
                if (N[0] == 0)
                {
                    savedNumber = 0;
                }
                else
                {
                    savedNumber =
                        (parameter - splineCollection.KnotsVector[controlPoint]) * N[0] /
                        (splineCollection.KnotsVector[controlPoint + i] -
                        splineCollection.KnotsVector[controlPoint]);
                }

                for (int j = 0; j < degree - i + 1; j++)
                {
                    double leftKnot = splineCollection.KnotsVector[controlPoint + j + 1],
                        rightKnot = splineCollection.KnotsVector[controlPoint + j + i + 1];

                    if (N[j + 1] == 0)
                    {
                        N[j] = savedNumber;
                        savedNumber = 0;
                    }
                    else
                    {
                        temporaryNumber = N[j + 1] / (rightKnot - leftKnot);
                        N[j] = savedNumber + (rightKnot - parameter) * temporaryNumber;
                        savedNumber = (parameter - leftKnot) * temporaryNumber;
                    }
                }
            }

            return N[0];
        }

        /// <summary>
        /// Метод получения точек сплайна.
        /// </summary>
        /// <param name="controlPointsAmount">Количество опорных точек.</param>
        /// <param name="splineCollection">
        /// Объект класса-контейнера SplineCollection с информацией об элементах
        /// сплайна.
        /// </param>
        public void SetSplineCurve(int controlPointsAmount)
        {
            splineCollection.SplinePointsList = new List<Point>();

            for (double i = 0; i <= 1; i += splineCollection.Parameter)
            {
                double x = 0,
                y = 0;

                for (int j = 0; j < controlPointsAmount; j++)
                {
                    double n = CalculateN(j, i);

                    x += splineCollection.PointsList[j].PointX * n;
                    y += splineCollection.PointsList[j].PointY * n;
                }

                splineCollection.SplinePointsList.Add(new Point(x, y));
            }
        }
    }
}
