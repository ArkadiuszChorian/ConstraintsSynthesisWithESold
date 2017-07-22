using ES.Engine.Models;
using ES.Engine.Solutions;
using ES.Engine.Utils;

namespace ES.Engine.PopulationGeneration
{
    public class OsmPopulationRandomGenerator : IPopulationGenerator
    {
        private readonly MersenneTwister _randomGenerator;

        public OsmPopulationRandomGenerator()
        {
            _randomGenerator = MersenneTwister.Instance;
        }

        public Solution[] GeneratePopulation(ExperimentParameters experimentParameters)
        {
            var basePopulationSize = experimentParameters.BasePopulationSize;
            var population = new Solution[basePopulationSize];

            for (var i = 0; i < basePopulationSize; i++)
            {
                population[i] = new Solution(experimentParameters);

                var lenght = population[i].ObjectCoefficients.Length;

                for (var j = 0; j < lenght; j++)
                {
                    population[i].ObjectCoefficients[j] = _randomGenerator.NextDouble();
                    
                }
                population[i].OneStepStdDeviation = _randomGenerator.NextDoublePositive();
            }

            return population;
        }
    }
}
