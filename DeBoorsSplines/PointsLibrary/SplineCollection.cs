using System.Collections.Generic;

namespace PointsLibrary
{
    public class SplineCollection
    {
        /// <summary>
        /// Свойство, возвращающее и задающее значение шага параметра.
        /// </summary>
        public double Parameter { set; get; }

        /// <summary>
        /// Свойство, возвращающее и задающее значение списка опорных точек.
        /// </summary>
        public List<Point> PointsList { set; get; }

        /// <summary>
        /// Свойство, возвращающее и задающее значение массива вектора узлов.
        /// </summary>
        public double[] KnotsVector { set; get; }

        /// <summary>
        /// Свойство, возвращающее и задающее значение списка точек сплайна.
        /// </summary>
        public List<Point> SplinePointsList { set; get; }
    }
}
