namespace ES.Engine.Models
{
    public class Point
    {
        public Point(int numberOfDimensions)
        {
            Coordinates = new double[numberOfDimensions];
        }

        public double[] Coordinates { get; set; }
        public double DistanceToNearestNeighbour { get; set; }
    }
}
