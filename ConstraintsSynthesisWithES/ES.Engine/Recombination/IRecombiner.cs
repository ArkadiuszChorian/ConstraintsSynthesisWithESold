using ES.Engine.Models;

namespace ES.Engine.Recombination
{
    public interface IRecombiner
    {
        Solution Recombine(Solution[] parents, Solution child = null);
    }
}
