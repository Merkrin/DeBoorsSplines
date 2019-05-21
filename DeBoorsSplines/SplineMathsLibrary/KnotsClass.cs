using PointsLibrary;

namespace SplineMathsLibrary
{
    /// <summary>
    /// Класс создания вектора узлов.
    /// </summary>
    public class KnotsClass
    {
        // Постоянные значения порядка сплайна и первого узла.
        private const int splineOrder = 4;

        /// <summary>
        /// Метод высчитывания значений точек вектора узлов в диапазоне от 0 до 1.
        /// Первые <see cref="splineOrder"/> точек равны 0, а последние
        /// <see cref="splineOrder"/> - 1. Точки, лежащие между точками 0 и 1,
        /// получают значения такие, что knot_(i+1) - knot_i = const для i \in
        /// (<see cref="splineOrder"/>;knotsAmount-<see cref="splineOrder"/>).
        /// </summary>
        /// <param name="controlPointsAmount">
        /// Количество опорных точек.
        /// </param>
        /// <param name="splineCollection">
        /// Объект класса-контейнера SplineCollection с информацией об элементах
        /// сплайна.
        /// </param>
        public void CalculateKnotsVektor(int controlPointsAmount, SplineCollection splineCollection)
        {
            int knotsAmount = controlPointsAmount + splineOrder;
            splineCollection.KnotsVector = new double[knotsAmount];
            double step = 1d / (knotsAmount - 2 * splineOrder + 1);

            for (int i = 0; i < splineOrder; i++)
            {
                splineCollection.KnotsVector[i] = 0;
            }
            for (int i = splineOrder; i < knotsAmount - splineOrder; i++)
            {
                splineCollection.KnotsVector[i] = splineCollection.KnotsVector[i - 1] + step;
            }
            for (int i = knotsAmount - splineOrder; i < knotsAmount; i++)
            {
                splineCollection.KnotsVector[i] = 1;
            }
        }
    }
}
