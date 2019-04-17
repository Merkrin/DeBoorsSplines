namespace PointsLibrary
{
    public class Point
    {
        public Point(double x, double y)
        {
            PointX = x;
            PointY = y;
        }

        public double PointX { set; get; }
        public double PointY { set; get; }

        public override string ToString()
        {
            return PointX + "," + PointY;
        }
    }
}
