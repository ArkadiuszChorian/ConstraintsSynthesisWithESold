using System;
using ES.Engine.Models;

namespace ES.Engine.PopulationGeneration
{
    public static class PopulationGeneratorsFactory
    {
        public static IPopulationGenerator GetPopulationGenerator(ExperimentParameters experimentParameters)
        {
            switch (experimentParameters.TypeOfMutation)
            {
                case ExperimentParameters.MutationType.UncorrelatedOneStep:
                    return new OsmPopulationRandomGenerator();
                case ExperimentParameters.MutationType.UncorrelatedNSteps:
                    return new NsmPopulationRandomGenerator();
                case ExperimentParameters.MutationType.Correlated:
                    return new CmPopulationRandomGenerator();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
