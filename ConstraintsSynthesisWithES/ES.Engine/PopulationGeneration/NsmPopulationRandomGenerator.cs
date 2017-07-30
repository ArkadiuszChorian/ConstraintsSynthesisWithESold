using ES.Engine.Models;
using ES.Engine.Utils;

namespace ES.Engine.PopulationGeneration
{
    public class NsmPopulationRandomGenerator : IPopulationGenerator
    {
        private readonly MersenneTwister _randomGenerator;

        public NsmPopulationRandomGenerator()
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
                    population[i].StdDeviationsCoefficients[j] = _randomGenerator.NextDoublePositive();
                }
            }

            return population;
        }

        //public IList<Solution> GeneratePopulation(ExperimentParameters experimentParameters)
        //{
        //    var population = new List<Solution>(experimentParameters.BasePopulationSize);

        //    for (var i = 0; i < experimentParameters.BasePopulationSize; i++)
        //    {
        //        population.Add(new Solution(experimentParameters));

        //        for (var j = 0; j < population[i].ObjectCoefficients.Length; j++)
        //        {
        //            population[i].ObjectCoefficients[j] = MersenneTwister.Instance.NextDouble();
        //            population[i].StdDeviationsCoefficients[j] = MersenneTwister.Instance.NextDoublePositive();
        //            //population[i].ObjectCoefficients[j] = MersenneTwister.Instance.NextDouble(-10.0, 10.0);
        //            //population[i].StdDeviationsCoefficients[j] = MersenneTwister.Instance.NextDouble(-10.0, 10.0);
        //            //if (Math.Abs(population[i].StdDeviationsCoefficients[j]) < 0.1)
        //            //{
        //            //    Debugger.Break();
        //            //}
        //        }
        //    }

        //    return population;
        //}
    }
}
