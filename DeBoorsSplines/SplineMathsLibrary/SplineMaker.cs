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
        internal SplineCollection splineCollection;

        public SplineMaker(SplineCollection splineCollection)
        {
            this.splineCollection = splineCollection;
        }

        // Метод подсчёта коэффициента N_i_p
        private double CalculateN(int controlPoint, double knot)
        {
            double[] N = new double[degree + 1];
            double savedNumber,
                temporaryNumber;
            int knotsAmount = splineCollection.KnotsVector.Length - 1;

            if (controlPoint == 0 && knot == splineCollection.KnotsVector[0]
                || controlPoint == (knotsAmount - degree - 1)
                && knot == splineCollection.KnotsVector[knotsAmount])
            {
                return 1;
            }

            if (knot < splineCollection.KnotsVector[controlPoint]
                || knot >= splineCollection.KnotsVector[controlPoint + degree + 1])
            {
                return 0;
            }

            for (int i = 0; i <= degree; i++)
            {
                if (knot >= splineCollection.KnotsVector[controlPoint + i]
                    && knot < splineCollection.KnotsVector[controlPoint + i + 1])
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
                        (knot - splineCollection.KnotsVector[controlPoint]) * N[0] /
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
                        N[j] = savedNumber + (rightKnot - knot) * temporaryNumber;
                        savedNumber = (knot - leftKnot) * temporaryNumber;
                    }
                }
            }

            return N[0];
        }

        // Метод подсчёта координат точки на сплайне.
        private Point GetPoint(int controlPointsAmount, double t)
        {
            double x = 0,
                y = 0;

            for (int i = 0; i < controlPointsAmount; i++)
            {
                double n = CalculateN(i, t);

                x += splineCollection.PointsList[i].PointX * n;
                y += splineCollection.PointsList[i].PointY * n;
            }

            return new Point(x, y);
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
                splineCollection.SplinePointsList.Add(GetPoint(controlPointsAmount, i));
            }
        }
    }
}
