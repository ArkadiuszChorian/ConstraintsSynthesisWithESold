using ES.Engine.Solutions;

namespace ES.Engine.Recombination
{
    public interface IRecombiner
    {
        Solution Recombine(Solution[] parents, Solution child = null);
    }
}
