using ES.Engine.Models;
using ES.Engine.Utils;

namespace ES.Engine.Mutation
{
    public class NsmObjectMutator : IMutator
    {
        private readonly MersenneTwister _randomGenerator;

        public NsmObjectMutator()
        {
            _randomGenerator = MersenneTwister.Instance;
        }

        public Solution Mutate(Solution solution)
        {
            var numberOfCoefficients = solution.ObjectCoefficients.Length;

            for (var i = 0; i < numberOfCoefficients; i++)
            {
                solution.ObjectCoefficients[i] += solution.StdDeviationsCoefficients[i] * _randomGenerator.NextDoublePositive();
            }

            return solution;
        }
    }
}
