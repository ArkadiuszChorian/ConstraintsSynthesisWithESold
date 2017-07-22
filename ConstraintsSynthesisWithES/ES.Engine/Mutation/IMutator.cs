using ES.Engine.Solutions;

namespace ES.Engine.Mutation
{
    public interface IMutator
    {
        Solution Mutate(Solution solution);
    }
}
