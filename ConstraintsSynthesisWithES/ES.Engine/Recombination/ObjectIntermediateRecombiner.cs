using ES.Engine.Models;

namespace ES.Engine.Recombination
{
    public class ObjectIntermediateRecombiner : IRecombiner
    {
        public ObjectIntermediateRecombiner(ExperimentParameters experimentParameters)
        {
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
            {
                var sum = 0.0;

                for (var j = 0; j < numberOfParents; j++)
                    sum += parents[j].ObjectCoefficients[i];

                child.ObjectCoefficients[i] = sum / numberOfParents;
            }

            return child;
        }
    }
}
