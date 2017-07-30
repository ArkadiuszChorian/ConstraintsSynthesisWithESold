using ES.Engine.Models;

namespace ES.Engine.Selection
{
    public interface IParentsSelector
    {
        Solution[] Select(Solution[] parentSolutions);
    }
}
