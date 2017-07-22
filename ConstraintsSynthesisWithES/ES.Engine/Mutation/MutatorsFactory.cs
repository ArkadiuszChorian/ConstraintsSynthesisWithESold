using System;
using ES.Engine.Models;

namespace ES.Engine.Mutation
{
    public static class MutatorsFactory
    {
        public static IMutator GetObjectMutator(ExperimentParameters experimentParameters)
        {
            switch (experimentParameters.TypeOfMutation)
            {
                case ExperimentParameters.MutationType.UncorrelatedOneStep:
                    return new OsmObjectMutator();
                case ExperimentParameters.MutationType.UncorrelatedNSteps:
                    return new NsmObjectMutator();
                case ExperimentParameters.MutationType.Correlated:
                    return new CmObjectMutator(experimentParameters);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static IMutator GetStdDevsMutator(ExperimentParameters experimentParameters)
        {
            switch (experimentParameters.TypeOfMutation)
            {
                case ExperimentParameters.MutationType.UncorrelatedOneStep:
                    return new OsmStdDevsMutator(experimentParameters);
                case ExperimentParameters.MutationType.UncorrelatedNSteps:
                    return new NsmStdDevsMutator(experimentParameters);
                case ExperimentParameters.MutationType.Correlated:
                    return new NsmStdDevsMutator(experimentParameters);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static IMutator GetRotationsMutator(ExperimentParameters experimentParameters)
        {
            return new RotationsMutator(experimentParameters);
        }
    }
}
