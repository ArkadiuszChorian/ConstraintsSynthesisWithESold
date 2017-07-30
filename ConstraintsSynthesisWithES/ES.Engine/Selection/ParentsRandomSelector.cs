using ES.Engine.Models;
using ES.Engine.Utils;

namespace ES.Engine.Selection
{
    public class ParentsRandomSelector : IParentsSelector
    {
        private readonly MersenneTwister _randomGenerator;

        public ParentsRandomSelector(ExperimentParameters experimentParameters)
        {
            _randomGenerator = MersenneTwister.Instance;
            NumberOfSolutionsToSelect = experimentParameters.NumberOfParentsSolutionsToSelect;
        }

        public int NumberOfSolutionsToSelect { get; set; }

        public Solution[] Select(Solution[] parentSolutions)
        {
            var selectedSolutions = new Solution[NumberOfSolutionsToSelect];

            for (var i = 0; i < NumberOfSolutionsToSelect; i++)
            {
                selectedSolutions[i] = parentSolutions[_randomGenerator.Next(parentSolutions.Length)].DeepCopyByExpressionTree();
            }

            return selectedSolutions;
        }
    }
}
