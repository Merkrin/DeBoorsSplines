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
        private double[] KnotsVector;

        private void CalculateKnotsVektor(int controlPointsAmount, SplineCollection splineCollection)
        {
            int knotsAmount = controlPointsAmount + splineOrder;

            KnotsVector = new double[knotsAmount];

            for(int i = 0; i < splineOrder; i++)
            {
                KnotsVector[i] = firstKnot;
            }

            for(int i = splineOrder; i <= knotsAmount - splineOrder; i++)
            {
                KnotsVector[i] = KnotsVector[i - 1] + splineCollection.Parameter;
            }

            for(int i = knotsAmount - splineOrder + 1; i < knotsAmount; i++)
            {
                KnotsVector[i] = KnotsVector[i - 1];
            }
        }

        private double CalculateN(int controlPoint, int degree, double knot)
        {
            double[] N = new double[degree + 1];
            double savedNumber,
                temporaryNumber;
            int knotsAmount = KnotsVector.Length - 1;

            if(controlPoint == 0 && knot == KnotsVector[0] 
                || controlPoint == (knotsAmount-degree-1) 
                && knot == KnotsVector[knotsAmount])
            {
                return 1;
            }

            if(knot < KnotsVector[controlPoint] || knot >= KnotsVector[controlPoint + degree + 1])
            {
                return 0;
            }

            for(int i = 0; i <= degree; i++)
            {
                if (knot >= KnotsVector[controlPoint + i] && knot < KnotsVector[controlPoint + i + 1])
                    N[i] = 1;
                else
                    N[i] = 0;
            }

            for(int i = 1; i <= degree; i++)
            {
                if (N[0] == 0)
                    savedNumber = 0;
                else
                    savedNumber = ((knot - KnotsVector[controlPoint]) * N[0]) / (KnotsVector[controlPoint + i] - KnotsVector[controlPoint]);

                for(int j = 0; j < degree - i + 1; j++)
                {
                    double leftKnot = KnotsVector[controlPoint + j + 1],
                        rightKnot = KnotsVector[controlPoint+j+i+1];

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
    }
}
