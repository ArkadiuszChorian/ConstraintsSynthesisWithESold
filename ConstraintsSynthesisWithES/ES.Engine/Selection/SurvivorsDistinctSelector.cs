using System;
using ES.Engine.Models;
using ES.Engine.Solutions;

namespace ES.Engine.Selection
{
    public class SurvivorsDistinctSelector : ISurvivorsSelector
    {
        public SurvivorsDistinctSelector(ExperimentParameters experimentParameters)
        {
            ExperimentParameters = experimentParameters;
        }

        public ExperimentParameters ExperimentParameters { get; set; }

        public Solution[] Select(Solution[] parentSolutions, Solution[] offspringSolutions)
        {
            var survivors = new Solution[ExperimentParameters.BasePopulationSize];
            Array.Sort(offspringSolutions);
            Array.Copy(offspringSolutions, survivors, ExperimentParameters.BasePopulationSize);

            return survivors;
        }
    }
}
