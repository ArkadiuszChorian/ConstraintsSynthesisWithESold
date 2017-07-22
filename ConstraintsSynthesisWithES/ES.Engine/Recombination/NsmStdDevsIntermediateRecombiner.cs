using ES.Engine.Models;
using ES.Engine.Solutions;

namespace ES.Engine.Recombination
{
    public class NsmStdDevsIntermediateRecombiner : IRecombiner
    {
        public NsmStdDevsIntermediateRecombiner(ExperimentParameters experimentParameters)
        {
            ExperimentParameters = experimentParameters;
        }

        public ExperimentParameters ExperimentParameters { get; set; }

        public Solution Recombine(Solution[] parents, Solution child = null)
        {
            var vectorSize = parents[0].StdDeviationsCoefficients.Length;
            var numberOfParents = parents.Length;

            if (child == null)
                child = new Solution(ExperimentParameters);

            for (var i = 0; i < vectorSize; i++)
            {
                var sum = 0.0;

                for (var j = 0; j < numberOfParents; j++)
                    sum += parents[j].StdDeviationsCoefficients[i];

                child.StdDeviationsCoefficients[i] = sum / numberOfParents;
            }

            return child;
        }
    }
}
