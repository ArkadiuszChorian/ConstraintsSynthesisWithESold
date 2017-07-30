using ES.Engine.Models;

namespace ES.Engine.Selection
{
    public interface ISurvivorsSelector
    {
        Solution[] Select(Solution[] parentSolutions, Solution[] offspringSolutions);
    }
}
