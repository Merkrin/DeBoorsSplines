namespace PointsLibrary
{
    public class Point
    {
        public Point(int x, int y)
        {
            PointX = x;
            PointY = y;
        }

        public int PointX { set; get; }
        public int PointY { set; get; }

        public override string ToString()
        {
            return PointX + "," + PointY;
        }
    }
}
