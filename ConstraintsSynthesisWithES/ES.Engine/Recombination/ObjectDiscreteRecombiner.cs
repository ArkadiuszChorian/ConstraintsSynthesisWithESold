using ES.Engine.Models;
using ES.Engine.Utils;

namespace ES.Engine.Recombination
{
    public class ObjectDiscreteRecombiner : IRecombiner
    {
        private readonly MersenneTwister _randomGenerator;

        public ObjectDiscreteRecombiner(ExperimentParameters experimentParameters)
        {
            _randomGenerator = MersenneTwister.Instance;
            ExperimentParameters = experimentParameters;
        }

        public ExperimentParameters ExperimentParameters { get; set; }

        public Solution Recombine(Solution[] parents, Solution child = null)
        {
            var vectorSize = parents[0].ObjectCoefficients.Length;
            var numberOfParents = parents.Length;

            if (child == null)
                child = new Solution(ExperimentParameters);

            for (var i = 0; i < vectorSize; i++)
                child.ObjectCoefficients[i] = parents[_randomGenerator.Next(numberOfParents)].ObjectCoefficients[i];

            return child;
        }
    }
}
