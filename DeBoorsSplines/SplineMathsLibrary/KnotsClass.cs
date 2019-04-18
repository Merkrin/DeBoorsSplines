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

        public void CalculateKnotsVektor(int controlPointsAmount, SplineCollection splineCollection)
        {
            int knotsAmount = controlPointsAmount + splineOrder;
            splineCollection.KnotsVector = new double[knotsAmount];
            double step = 1d/(knotsAmount-2*splineOrder+1);

            for (int i = 0; i < splineOrder; i++)
            {
                splineCollection.KnotsVector[i] = 0;
            }
            for(int i = splineOrder; i < knotsAmount-splineOrder; i++)
            {
                splineCollection.KnotsVector[i] = splineCollection.KnotsVector[i-1]+step;
            }
            for(int i = knotsAmount - splineOrder; i < knotsAmount; i++)
            {
                splineCollection.KnotsVector[i] = 1;
            }
        }
    }
}
