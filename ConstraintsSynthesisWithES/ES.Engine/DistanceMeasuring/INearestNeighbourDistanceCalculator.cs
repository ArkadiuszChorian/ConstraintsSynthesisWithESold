using ES.Engine.Models;

namespace ES.Engine.DistanceMeasuring
{
    public interface INearestNeighbourDistanceCalculator
    {
        void CalculateNearestNeighbourDistances(Point[] points);
    }
}
