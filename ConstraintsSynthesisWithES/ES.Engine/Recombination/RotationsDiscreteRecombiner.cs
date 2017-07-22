using ES.Engine.Models;
using ES.Engine.Solutions;
using ES.Engine.Utils;

namespace ES.Engine.Recombination
{
    public class RotationsDiscreteRecombiner : IRecombiner
    {
        private readonly MersenneTwister _randomGenerator;

        public RotationsDiscreteRecombiner(ExperimentParameters experimentParameters)
        {
            _randomGenerator = MersenneTwister.Instance;
            ExperimentParameters = experimentParameters;
        }

        public ExperimentParameters ExperimentParameters { get; set; }

        public Solution Recombine(Solution[] parents, Solution child = null)
        {
            var vectorSize = parents[0].RotationsCoefficients.Length;
            var numberOfParents = parents.Length;

            if (child == null)
                child = new Solution(ExperimentParameters);

            for (var i = 0; i < vectorSize; i++)
                child.RotationsCoefficients[i] = parents[_randomGenerator.Next(numberOfParents)].RotationsCoefficients[i];

            return child;
        }
    }
}
