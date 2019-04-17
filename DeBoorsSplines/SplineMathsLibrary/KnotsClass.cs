using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointsLibrary;

namespace SplineMathsLibrary
{
    public class KnotsClass
    {
        private const int splineOrder = 4;
        private const double firstKnot = 0;

        private void CalculateKnotsVektor(int controlPointsAmount, SplineCollection splineCollection)
        {
            int knotsAmount = controlPointsAmount + splineOrder;

            splineCollection.KnotsVector = new double[knotsAmount];

            for (int i = 0; i < splineOrder; i++)
            {
                splineCollection.KnotsVector[i] = firstKnot;
            }

            for (int i = splineOrder; i <= knotsAmount - splineOrder; i++)
            {
                splineCollection.KnotsVector[i] = splineCollection.KnotsVector[i - 1] + splineCollection.Parameter;
            }

            for (int i = knotsAmount - splineOrder + 1; i < knotsAmount; i++)
            {
                splineCollection.KnotsVector[i] = splineCollection.KnotsVector[i - 1];
            }
        }
    }
}
