using ES.Engine.Solutions;

namespace ES.Engine.Selection
{
    public interface IParentsSelector
    {
        Solution[] Select(Solution[] parentSolutions);
    }
}
