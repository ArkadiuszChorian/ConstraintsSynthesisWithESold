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
    public interface IEngine
    {
        void RunExperiment();

        IBenchmark Benchmark { get; set; }
        IPopulationGenerator PopulationGenerator { get; set; }
        IEvaluator Evaluator { get; set; }
        ILogger Logger { get; set; }
        IMutator ObjectMutator { get; set; }
        IMutator StdDeviationsMutator { get; set; }
        IMutationRuleSupervisor MutationRuleSupervisor { get; set; }
        IParentsSelector ParentsSelector { get; set; }
        ISurvivorsSelector SurvivorsSelector { get; set; }
        IPointsGenerator PositivePointsGenerator { get; set; }
        IPointsGenerator NegativePointsGenerator { get; set; }
        ExperimentParameters ExperimentParameters { get; set; }
        Solution[] BasePopulation { get; set; }
        Solution[] OffspringPopulation { get; set; }
        Solution[] InitialPopulation { get; set; }
    }
}
