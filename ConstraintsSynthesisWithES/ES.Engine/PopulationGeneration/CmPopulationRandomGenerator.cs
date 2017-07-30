using ES.Engine.Models;
using ES.Engine.Utils;

namespace ES.Engine.PopulationGeneration
{
    public class CmPopulationRandomGenerator : IPopulationGenerator
    {
        private readonly MersenneTwister _randomGenerator;

        public CmPopulationRandomGenerator()
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

                lenght = population[i].RotationsCoefficients.Length;

                for (var j = 0; j < lenght; j++)
                {
                    population[i].RotationsCoefficients[j] = _randomGenerator.NextDoublePositive();
                }    
            }

            return population;
        }

        //public Solution[] GeneratePopulation(ExperimentParameters experimentParameters)
        //{
        //    var basePopulationSize = experimentParameters.BasePopulationSize;
        //    //var population = new List<Solution>(experimentParameters.BasePopulationSize);
        //    var population = new Solution[basePopulationSize];

        //    for (var i = 0; i < experimentParameters.BasePopulationSize; i++)
        //    {
        //        population.Add(new Solution(experimentParameters));

        //        for (var j = 0; j < population[i].ObjectCoefficients.Length; j++)
        //        {
        //            population[i].ObjectCoefficients[j] = MersenneTwister.Instance.NextDouble();
        //            population[i].StdDeviationsCoefficients[j] = MersenneTwister.Instance.NextDoublePositive();
        //        }

        //        var size = population[i].RotationsCoefficients.Length;

        //        for (var j = 0; j < size; j++)
        //        {
        //            var randomValue = MersenneTwister.Instance.NextDoublePositive();

        //            population[i].RotationsCoefficients[j] = randomValue;
        //        }

        //        //if (size > 1)
        //        //{
        //        //    for (var j = 0; j < size; j++)
        //        //    {
        //        //        for (var k = j + 1; k < size; k++)
        //        //        {
        //        //            var randomValue = MersenneTwister.Instance.NextDoublePositive();

        //        //            //population[i].RotationsCoefficients[j][k] = randomValue;
        //        //            //population[i].RotationsCoefficients[k][j] = randomValue;
        //        //            population[i].RotationsCoefficients[]
        //        //        }
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    //population[i].RotationsCoefficients[0][0] = MersenneTwister.Instance.NextDoublePositive();
        //        //    population[i].RotationsCoefficients[0] = MersenneTwister.Instance.NextDoublePositive();
        //        //}               
        //    }

        //    return population;
        //}
    }
}
