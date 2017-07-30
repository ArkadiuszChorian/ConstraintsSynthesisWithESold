using System.Collections.Generic;
using ES.Engine.Benchmarks;
using ES.Engine.Constraints;
using ES.Engine.Evaluation;
using ES.Engine.Logging;
using ES.Engine.Models;
using ES.Engine.Mutation;
using ES.Engine.MutationSupervison;
using ES.Engine.PointsGeneration;
using ES.Engine.PopulationGeneration;
using ES.Engine.Selection;
using ES.Engine.Utils;
using ExperimentDatabase;

namespace ES.Engine.Engine
{
    public interface IEngine
    {
        IList<Constraint> GetSynthesizedModel();
        IList<Constraint> GetReferenceModel();
        void SynthesizeModel(Point[] trainingPoints);
        Statistics EvaluateModel(Point[] testPoints);

        IBenchmark Benchmark { get; set; }
        //IPopulationGenerator PopulationGenerator { get; set; }
        //IEvaluator Evaluator { get; set; }
        //ILogger Logger { get; set; }
        //IMutator ObjectMutator { get; set; }
        //IMutator StdDeviationsMutator { get; set; }
        //IMutationRuleSupervisor MutationRuleSupervisor { get; set; }
        //IParentsSelector ParentsSelector { get; set; }
        //ISurvivorsSelector SurvivorsSelector { get; set; }
        //IPointsGenerator PositivePointsGenerator { get; set; }
        //IPointsGenerator NegativePointsGenerator { get; set; }
        ExperimentParameters ExperimentParameters { get; set; }
        Statistics Statistics { get; set; }
        //Solution[] BasePopulation { get; set; }
        //Solution[] OffspringPopulation { get; set; }
        //Solution[] InitialPopulation { get; set; }     
    }
}
