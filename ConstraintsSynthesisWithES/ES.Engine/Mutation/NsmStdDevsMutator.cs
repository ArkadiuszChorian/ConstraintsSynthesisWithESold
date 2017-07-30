using System;
using ES.Engine.Models;
using ES.Engine.Utils;

namespace ES.Engine.Mutation
{
    public class NsmStdDevsMutator : IMutator
    {
        private readonly MersenneTwister _randomGenerator;

        public NsmStdDevsMutator(ExperimentParameters experimentParameters)
        {
            GlobalLearningRate = experimentParameters.GlobalLearningRate;
            IndividualLearningRate = experimentParameters.IndividualLearningRate;
            StepThreshold = experimentParameters.StepThreshold;
            _randomGenerator = MersenneTwister.Instance;
        }

        public double GlobalLearningRate { get; set; }
        public double IndividualLearningRate { get; set; }
        public double StepThreshold { get; set; }

        public Solution Mutate(Solution solution)
        {
            var numberOfCoefficients = solution.StdDeviationsCoefficients.Length;

            for (var j = 0; j < numberOfCoefficients; j++)
            {
                solution.StdDeviationsCoefficients[j] *= Math.Exp(IndividualLearningRate * _randomGenerator.NextDoublePositive() + GlobalLearningRate * _randomGenerator.NextDoublePositive());
                solution.StdDeviationsCoefficients[j] = solution.StdDeviationsCoefficients[j] < StepThreshold ? StepThreshold : solution.StdDeviationsCoefficients[j];
            }

            return solution;
        }
    }
}
