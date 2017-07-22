using ES.Engine.Constraints;
using ES.Engine.Models;

namespace ES.Engine.Benchmarks
{
    public interface IBenchmark
    {
        Constraint[] Constraints { get; set; }
        Domain[] Domains { get; set; }
    }
}
