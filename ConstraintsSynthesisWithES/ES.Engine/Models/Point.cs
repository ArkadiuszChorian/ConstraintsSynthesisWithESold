namespace ES.Engine.Models
{
    public class Point
    {
        public Point(long numberOfDimensions)
        {
            Coordinates = new double[numberOfDimensions];
        }

        public Point(long numberOfDimensions, ClassificationType classificationType)
        {
            Coordinates = new double[numberOfDimensions];
            ClassificationType = classificationType;
        }

        public double[] Coordinates { get; set; }
        public double DistanceToNearestNeighbour { get; set; }
        public ClassificationType ClassificationType { get; set; }
    }
}
