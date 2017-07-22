using System;
using ES.Engine.Models;

namespace ES.Engine.Recombination
{
    public static class RecombinersFactory
    {
        public static IRecombiner GetObjectRecombiner(ExperimentParameters experimentParameters)
        {
            switch (experimentParameters.TypeOfObjectsRecombiner)
            {
                case ExperimentParameters.RecombinerType.Discrete:
                    return new ObjectDiscreteRecombiner(experimentParameters);
                case ExperimentParameters.RecombinerType.Intermediate:
                    return new ObjectIntermediateRecombiner(experimentParameters);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static IRecombiner GetStdDevsRecombiner(ExperimentParameters experimentParameters)
        {
            switch (experimentParameters.TypeOfMutation)
            {
                case ExperimentParameters.MutationType.UncorrelatedOneStep:
                    switch (experimentParameters.TypeOfStdDeviationsRecombiner)
                    {
                        case ExperimentParameters.RecombinerType.Discrete:
                            return new OsmStdDevsDiscreteRecombiner(experimentParameters);
                        case ExperimentParameters.RecombinerType.Intermediate:
                            return new OsmStdDevsIntermediateRecombiner(experimentParameters);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                case ExperimentParameters.MutationType.UncorrelatedNSteps:
                    switch (experimentParameters.TypeOfStdDeviationsRecombiner)
                    {
                        case ExperimentParameters.RecombinerType.Discrete:
                            return new NsmStdDevsDiscreteRecombiner(experimentParameters);
                        case ExperimentParameters.RecombinerType.Intermediate:
                            return new NsmStdDevsIntermediateRecombiner(experimentParameters);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                case ExperimentParameters.MutationType.Correlated:
                    switch (experimentParameters.TypeOfStdDeviationsRecombiner)
                    {
                        case ExperimentParameters.RecombinerType.Discrete:
                            return new NsmStdDevsDiscreteRecombiner(experimentParameters);
                        case ExperimentParameters.RecombinerType.Intermediate:
                            return new NsmStdDevsIntermediateRecombiner(experimentParameters);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static IRecombiner GetRotationsRecombiner(ExperimentParameters experimentParameters)
        {
            switch (experimentParameters.TypeOfRotationsRecombiner)
            {
                case ExperimentParameters.RecombinerType.Discrete:
                    return new RotationsDiscreteRecombiner(experimentParameters);
                case ExperimentParameters.RecombinerType.Intermediate:
                    return new RotationsIntermediateRecombiner(experimentParameters);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
