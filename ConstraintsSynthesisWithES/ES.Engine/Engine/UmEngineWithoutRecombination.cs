using ES.Engine.Benchmarks;
using ES.Engine.Evaluation;
using ES.Engine.Logging;
using ES.Engine.Models;
using ES.Engine.Mutation;
using ES.Engine.MutationSupervison;
using ES.Engine.PointsGeneration;
using ES.Engine.PopulationGeneration;
using ES.Engine.Selection;
using ES.Engine.Solutions;

namespace ES.Engine.Engine
{
    public class UmEngineWithoutRecombination : IEngine
    {
        public UmEngineWithoutRecombination(IBenchmark benchmark, IPopulationGenerator populationGenerator, IEvaluator evaluator, ILogger logger, IMutator objectMutator, IMutator stdDeviationsMutator, IMutationRuleSupervisor mutationRuleSupervisor, IParentsSelector parentsParentsSelector, ISurvivorsSelector survivorsSelector, IPointsGenerator positivePointsGenerator, IPointsGenerator negativePointsGenerator, ExperimentParameters experimentParameters, Solution[] basePopulation, Solution[] offspringPopulation)
        {
            Benchmark = benchmark;
            PopulationGenerator = populationGenerator;
            Evaluator = evaluator;
            Logger = logger;
            ObjectMutator = objectMutator;
            StdDeviationsMutator = stdDeviationsMutator;
            MutationRuleSupervisor = mutationRuleSupervisor;
            ParentsSelector = parentsParentsSelector;
            SurvivorsSelector = survivorsSelector;
            PositivePointsGenerator = positivePointsGenerator;
            NegativePointsGenerator = negativePointsGenerator;
            ExperimentParameters = experimentParameters;
            BasePopulation = basePopulation;
            OffspringPopulation = offspringPopulation;
        }

        public IBenchmark Benchmark { get; set; }
        public IPopulationGenerator PopulationGenerator { get; set; }
        public IEvaluator Evaluator { get; set; }
        public ILogger Logger { get; set; }
        public IMutator ObjectMutator { get; set; }
        public IMutator StdDeviationsMutator { get; set; }
        public IMutationRuleSupervisor MutationRuleSupervisor { get; set; }
        public IParentsSelector ParentsSelector { get; set; }
        public ISurvivorsSelector SurvivorsSelector { get; set; }
        public IPointsGenerator PositivePointsGenerator { get; set; }
        public IPointsGenerator NegativePointsGenerator { get; set; }
        public ExperimentParameters ExperimentParameters { get; set; }
        public Solution[] BasePopulation { get; set; }
        public Solution[] OffspringPopulation { get; set; }
        public Solution[] InitialPopulation { get; set; }

        public virtual void RunExperiment()
        {
            //BasePopulation = PopulationGenerator.GeneratePopulation(ExperimentParameters);

            //for (var i = 0; i < ExperimentParameters.OffspringPopulationSize; i++)
            //{
            //    OffspringPopulation.Add(new Solution(ExperimentParameters));
            //}
            
            //InitialPopulation = BasePopulation.DeepCopyByExpressionTree();

            //for (var i = 0; i < ExperimentParameters.NumberOfGenerations; i++)
            //{
            //    //var newPopulation = ParentsSelector.Select(BasePopulation);

            //    //MutationRuleSupervisor.IncrementGenerationNumber();

            //    for (var j = 0; j < ExperimentParameters.OffspringPopulationSize; j++)
            //    {
            //        OffspringPopulation[j] = BasePopulation[MersenneTwister.Instance.Next(BasePopulation.Count)].DeepCopyByExpressionTree();

            //        OffspringPopulation[j] = StdDeviationsMutator.Mutate(OffspringPopulation[j]);
            //        OffspringPopulation[j] = ObjectMutator.Mutate(OffspringPopulation[j]);

            //        OffspringPopulation[j].FitnessScore = Evaluator.Evaluate(OffspringPopulation[j]);
            //        //MutationRuleSupervisor.RemeberSolutionParameters(newPopulation[j]);

            //        //newPopulation[j] = StdDeviationsMutator.Mutate(newPopulation[j]);
            //        //newPopulation[j] = ObjectMutator.Mutate(newPopulation[j]);

            //        //MutationRuleSupervisor.IncrementMutationsNumber();

            //        //newPopulation[j].FitnessScore = Evaluator.Evaluate(newPopulation[j]);

            //        //MutationRuleSupervisor.CompareNewSolutionParameters(newPopulation[j]);
            //    }

            //    //MutationRuleSupervisor.EnsureRuleFullfillment(newPopulation);

            //    //BasePopulation = SurvivorsSelector.MakeUnionOrDistinct(newPopulation, BasePopulation);
            //    //BasePopulation = SurvivorsSelector.Select(newPopulation);

            //    BasePopulation = SurvivorsSelector.Select(OffspringPopulation);
            //}

            //BasePopulation = BasePopulation.OrderByDescending(solution => solution.FitnessScore).ToList();
        }      
    }
}
