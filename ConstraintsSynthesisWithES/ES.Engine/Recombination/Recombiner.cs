using System.Collections.Generic;
using System.Linq;
using ES.Engine.Models;
using ES.Engine.Solutions;
using ES.Engine.Utils;

namespace ES.Engine.Recombination
{
    public abstract class Recombiner
    {
        protected Recombiner(ExperimentParameters experimentParameters)
        {
            NumberOfSolutionsToRecombine = (int)experimentParameters.PartOfPopulationToRecombine * experimentParameters.BasePopulationSize;
        }

        public int NumberOfSolutionsToRecombine { get; set; }

        protected IList<Solution> SelectParents(IList<Solution> parents)
        {
            var selectedParents = new HashSet<Solution>();
           
            while (selectedParents.Count < NumberOfSolutionsToRecombine)
            {
                selectedParents.Add(parents[MersenneTwister.Instance.Next(parents.Count)]);
            }

            return selectedParents.ToList();
        }

        public abstract Solution Recombine(IList<Solution> parents, Solution child = null);
    }
}
