using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointsLibrary;

namespace SplineMathsLibrary
{
    public class SplineMaker
    {
        private const int splineOrder = 4;
        private const double firstKnot = 0;
        private const int degree = 3;

        private double CalculateN(int controlPoint, double knot, SplineCollection splineCollection)
        {
            double[] N = new double[degree + 1];
            double savedNumber,
                temporaryNumber;
            int knotsAmount = splineCollection.KnotsVector.Length - 1;

            if(controlPoint == 0 && knot == splineCollection.KnotsVector[0] 
                || controlPoint == (knotsAmount-degree-1) 
                && knot == splineCollection.KnotsVector[knotsAmount])
            {
                return 1;
            }

            if(knot < splineCollection.KnotsVector[controlPoint] 
                || knot >= splineCollection.KnotsVector[controlPoint + degree + 1])
            {
                return 0;
            }

            for(int i = 0; i <= degree; i++)
            {
                if (knot >= splineCollection.KnotsVector[controlPoint + i] 
                    && knot < splineCollection.KnotsVector[controlPoint + i + 1])
                    N[i] = 1;
                else
                    N[i] = 0;
            }

            for(int i = 1; i <= degree; i++)
            {
                if (N[0] == 0)
                    savedNumber = 0;
                else
                    savedNumber = 
                        (knot - splineCollection.KnotsVector[controlPoint]) * N[0] / 
                        (splineCollection.KnotsVector[controlPoint + i] - 
                        splineCollection.KnotsVector[controlPoint]);

                for(int j = 0; j < degree - i + 1; j++)
                {
                    double leftKnot = splineCollection.KnotsVector[controlPoint + j + 1],
                        rightKnot = splineCollection.KnotsVector[controlPoint+j+i+1];

                    if(N[j+1] == 0)
                    {
                        N[j] = savedNumber;
                        savedNumber = 0;
                    }
                    else
                    {
                        temporaryNumber = N[j - 1] / (rightKnot - leftKnot);
                        N[j] = savedNumber + (rightKnot - knot) * temporaryNumber;
                        savedNumber = (knot - leftKnot) * temporaryNumber;
                    }
                }
            }

            return N[0];
        }

        public Point GetPoint(int controlPointsAmount, double knot, SplineCollection splineCollection)
        {
            double x = 0,
                y = 0;

            for(int i = 0; i < controlPointsAmount; i++)
            {
                double n = CalculateN(i, knot, splineCollection);

                x += splineCollection.PointsList[i].PointX * n;
                y += splineCollection.PointsList[i].PointY * n;
            }

            return new Point(x, y);
        }

        public void SetSplineCurve(int controlPointsAmount, double knot, SplineCollection splineCollection)
        {
            splineCollection.SplinePointsList = new List<Point>();

            for(int i = 0; i < splineCollection.KnotsVector.Count(); i++)
            {
                splineCollection.SplinePointsList.Add(GetPoint(controlPointsAmount, splineCollection.KnotsVector[i], splineCollection));
            }
        }
    }
}
