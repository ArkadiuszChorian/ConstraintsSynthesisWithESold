using System;
using ES.Engine.Models;

namespace ES.Engine.Selection
{
    public class SurvivorsUnionSelector : ISurvivorsSelector
    {
        public SurvivorsUnionSelector(ExperimentParameters experimentParameters)
        {
            ExperimentParameters = experimentParameters;
        }

        public ExperimentParameters ExperimentParameters { get; set; }

        public Solution[] Select(Solution[] parentSolutions, Solution[] offspringSolutions)
        {
            var parentsLength = parentSolutions.Length;
            var offspringLength = offspringSolutions.Length;
            var union = new int[parentsLength + offspringLength];
            var survivors = new Solution[ExperimentParameters.BasePopulationSize];

            Array.Copy(parentSolutions, union, parentsLength);
            Array.Copy(offspringSolutions, 0, union, parentsLength, offspringLength);
        
            Array.Sort(union);
            Array.Copy(union, survivors, ExperimentParameters.BasePopulationSize);

            return survivors;
        }
    }
}
