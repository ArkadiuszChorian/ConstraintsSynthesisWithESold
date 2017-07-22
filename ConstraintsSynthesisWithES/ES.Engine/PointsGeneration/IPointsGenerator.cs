using ES.Engine.Benchmarks;
using ES.Engine.Models;

namespace ES.Engine.PointsGeneration
{
    public interface IPointsGenerator
    {
        //Point[] GeneratePoints(int numberOfPointsToGenerate, List<Constraint> constraints);
        Point[] GeneratePoints(int numberOfPointsToGenerate, IBenchmark benchmark);
    }
}
