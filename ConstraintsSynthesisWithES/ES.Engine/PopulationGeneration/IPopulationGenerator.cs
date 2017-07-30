using ES.Engine.Models;

namespace ES.Engine.PopulationGeneration
{
    public interface IPopulationGenerator
    {
        Solution[] GeneratePopulation(ExperimentParameters experimentParameters);
    }
}
