namespace PointsLibrary
{
    /// <summary>
    /// Класс, представляющий точку на плоскости.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Конструктор класса Point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(double x, double y)
        {
            PointX = x;
            PointY = y;
        }

        /// <summary>
        /// Свойство, возвращающее и задающее значение х-координате данной точки.
        /// </summary>
        public double PointX { set; get; }
        /// <summary>
        /// Свойство, возвращающее и задающее значение y-координате данной точки.
        /// </summary>
        public double PointY { set; get; }

        public override string ToString()
        {
            return PointX + "," + PointY;
        }
    }
}
