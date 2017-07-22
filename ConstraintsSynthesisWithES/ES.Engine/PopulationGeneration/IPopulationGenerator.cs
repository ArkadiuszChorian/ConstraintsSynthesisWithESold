using ES.Engine.Models;
using ES.Engine.Solutions;

namespace ES.Engine.PopulationGeneration
{
    public interface IPopulationGenerator
    {
        Solution[] GeneratePopulation(ExperimentParameters experimentParameters);
    }
}
