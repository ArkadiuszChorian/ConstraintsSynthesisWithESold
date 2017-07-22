using ES.Engine.Benchmarks;
using ES.Engine.Evaluation;
using ES.Engine.Logging;
using ES.Engine.Models;
using ES.Engine.Mutation;
using ES.Engine.MutationSupervison;
using ES.Engine.PointsGeneration;
using ES.Engine.PopulationGeneration;
using ES.Engine.Recombination;
using ES.Engine.Selection;
using ES.Engine.Solutions;

namespace ES.Engine.Engine
{
    public class UmEngineWithRecombination : UmEngineWithoutRecombination
    {
        public UmEngineWithRecombination(IBenchmark benchmark, IPopulationGenerator populationGenerator, IEvaluator evaluator, ILogger logger, IMutator objectMutator, IMutator stdDeviationsMutator, IMutationRuleSupervisor mutationRuleSupervisor, IParentsSelector parentsParentsSelector, ISurvivorsSelector survivorsSelector, IPointsGenerator positivePointsGenerator, IPointsGenerator negativePointsGenerator, ExperimentParameters experimentParameters, Solution[] basePopulation, Solution[] offspringPopulation, IRecombiner objectRecombiner, IRecombiner stdDeviationsRecombiner) : base(benchmark, populationGenerator, evaluator, logger, objectMutator, stdDeviationsMutator, mutationRuleSupervisor, parentsParentsSelector, survivorsSelector, positivePointsGenerator, negativePointsGenerator, experimentParameters, basePopulation, offspringPopulation)
        {
            ObjectRecombiner = objectRecombiner;
            StdDeviationsRecombiner = stdDeviationsRecombiner;
        }

        public IRecombiner ObjectRecombiner { get; set; }
        public IRecombiner StdDeviationsRecombiner { get; set; }  

        public override void RunExperiment()
        {
            //BasePopulation = PopulationGenerator.GeneratePopulation(ExperimentParameters);

            //for (var i = 0; i < ExperimentParameters.NumberOfGenerations; i++)
            //{
            //    var newPopulation = ParentsSelector.Select(BasePopulation);

            //    for (var j = 0; j < newPopulation.Count; j++)
            //    {
            //        //TODO: Recombination
            //        newPopulation[j] = StdDeviationsRecombiner.Recombine(newPopulation);
            //        newPopulation[j] = ObjectRecombiner.Recombine(newPopulation, newPopulation[i]);

            //        newPopulation[j] = StdDeviationsMutator.Mutate(newPopulation[j]);
            //        newPopulation[j] = ObjectMutator.Mutate(newPopulation[j]);                  

            //        newPopulation[j].FitnessScore = Evaluator.Evaluate(newPopulation[j]);
            //    }

            //    BasePopulation = SurvivorsSelector.MakeUnionOrDistinct(newPopulation, BasePopulation);
            //    BasePopulation = SurvivorsSelector.Select(newPopulation);
            //}

            //BasePopulation = BasePopulation.OrderByDescending(solution => solution.FitnessScore).ToList();
        }
    }
}
