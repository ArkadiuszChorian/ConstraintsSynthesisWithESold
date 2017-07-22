using ES.Engine.Solutions;

namespace ES.Engine.Selection
{
    public interface ISurvivorsSelector
    {
        Solution[] Select(Solution[] parentSolutions, Solution[] offspringSolutions);
    }
}
