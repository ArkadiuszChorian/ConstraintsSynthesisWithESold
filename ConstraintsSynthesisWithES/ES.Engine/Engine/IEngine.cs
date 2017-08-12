using System.Collections.Generic;
using ES.Engine.Benchmarks;
using ES.Engine.Constraints;
using ES.Engine.Models;

namespace ES.Engine.Engine
{
    public interface IEngine
    {
        //IList<Constraint> GetSynthesizedModel();
        //IList<Constraint> GetReferenceModel();
        MathModel SynthesizeModel(Point[] trainingPoints);
        Statistics EvaluateModel(Point[] testPoints);

        IBenchmark Benchmark { get; set; }
        ExperimentParameters ExperimentParameters { get; set; }
        Statistics Statistics { get; set; }
        MathModel MathModel { get; set; }

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
        //Solution[] BasePopulation { get; set; }
        //Solution[] OffspringPopulation { get; set; }
        //Solution[] InitialPopulation { get; set; }     
    }
}
