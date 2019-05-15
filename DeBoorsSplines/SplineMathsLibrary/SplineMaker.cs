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

        // Метод подсчёта коэффициента N_i_p
        private double CalculateN(int controlPoint, double knot, SplineCollection splineCollection)
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
        private Point GetPoint(int controlPointsAmount, double t, SplineCollection splineCollection)
        {
            double x = 0,
                y = 0;

            for (int i = 0; i < controlPointsAmount; i++)
            {
                double n = CalculateN(i, t, splineCollection);

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
        public void SetSplineCurve(int controlPointsAmount, SplineCollection splineCollection)
        {
            splineCollection.SplinePointsList = new List<Point>();

            for (double i = 0; i <= 1; i += splineCollection.Parameter)
            {
                splineCollection.SplinePointsList.Add(GetPoint(controlPointsAmount, i, splineCollection));
            }
        }

        public void CloseCurve(SplineCollection splineCollection)
        {
            splineCollection.PointsList[splineCollection.PointsList.Count-1] = splineCollection.PointsList[0];
            splineCollection.PointsList[splineCollection.PointsList.Count - 2] = splineCollection.PointsList[2];
            splineCollection.PointsList[splineCollection.PointsList.Count - 3] = splineCollection.PointsList[3];
            //splineCollection.PointsList.Add(splineCollection.PointsList[1]);
            //splineCollection.PointsList.Add(splineCollection.PointsList[2]);

            splineCollection.KnotsVector = new double[splineCollection.PointsList.Count + 4];

            double step = 1d / (splineCollection.KnotsVector.Length - 1);

            splineCollection.KnotsVector[0] = 0;

            for (int i = 1; i < splineCollection.KnotsVector.Length - 1; i++)
            {
                splineCollection.KnotsVector[i] = splineCollection.KnotsVector[i - 1] + step;
                //splineCollection.KnotsVector[i] = i / splineCollection.KnotsVector.Length;
            }

            splineCollection.KnotsVector[splineCollection.KnotsVector.Length - 1] = 1;



            //splineCollection.KnotsVector = new double[splineCollection.PointsList.Count + 4 + 1 + 1];

            //splineCollection.KnotsVector[0] = 0;
            //splineCollection.KnotsVector[splineCollection.PointsList.Count + 4 + 1] = 1;

            //for (int i = 1; i < splineCollection.PointsList.Count + 4 + 1 + 1; i++)
            //{
            //    splineCollection.KnotsVector[i] = i / (splineCollection.PointsList.Count + 4 + 1);
            //}

            
        }
    }
}
