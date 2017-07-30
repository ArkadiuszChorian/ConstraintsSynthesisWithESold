using System;
using System.Collections.Generic;
using System.Linq;
using ES.Engine.Benchmarks;
using ES.Engine.Constraints;
using ES.Engine.Evaluation;
using ES.Engine.Logging;
using ES.Engine.Models;
using ES.Engine.Mutation;
using ES.Engine.MutationSupervison;
using ES.Engine.PointsGeneration;
using ES.Engine.PopulationGeneration;
using ES.Engine.PrePostProcessing;
using ES.Engine.Selection;
using ES.Engine.Utils;
using ExperimentDatabase;

namespace ES.Engine.Engine
{
    public abstract class EngineBase : IEngine
    {
        //protected IBenchmark Benchmark;
        protected IPopulationGenerator PopulationGenerator;
        protected IEvaluator Evaluator;
        protected ILogger Logger;
        protected IMutator ObjectMutator;
        protected IMutator StdDeviationsMutator;
        protected IMutationRuleSupervisor MutationRuleSupervisor;
        protected IParentsSelector ParentsSelector;
        protected ISurvivorsSelector SurvivorsSelector;
        protected IProcessor<Constraint[]> RedundantConstriantsRemover;
        protected Solution[] BasePopulation;
        protected Solution[] OffspringPopulation;
        protected Constraint[] SynthesizedModel;
        protected Solution[] InitialPopulation;

        //protected EngineBase(IBenchmark benchmark, IPopulationGenerator populationGenerator, IEvaluator evaluator, ILogger logger, IMutator objectMutator, IMutator stdDeviationsMutator, IMutationRuleSupervisor mutationRuleSupervisor, IParentsSelector parentsParentsSelector, ISurvivorsSelector survivorsSelector, IPointsGenerator positivePointsGenerator, IPointsGenerator negativePointsGenerator, ExperimentParameters experimentParameters, Solution[] basePopulation, Solution[] offspringPopulation)
        //{
        //    Benchmark = benchmark;
        //    PopulationGenerator = populationGenerator;
        //    Evaluator = evaluator;
        //    Logger = logger;
        //    ObjectMutator = objectMutator;
        //    StdDeviationsMutator = stdDeviationsMutator;
        //    MutationRuleSupervisor = mutationRuleSupervisor;
        //    ParentsSelector = parentsParentsSelector;
        //    SurvivorsSelector = survivorsSelector;
        //    PositivePointsGenerator = positivePointsGenerator;
        //    NegativePointsGenerator = negativePointsGenerator;
        //    ExperimentParameters = experimentParameters;
        //    BasePopulation = basePopulation;
        //    OffspringPopulation = offspringPopulation;
        //}

        protected EngineBase(IBenchmark benchmark, IPopulationGenerator populationGenerator, IEvaluator evaluator, ILogger logger, IMutator objectMutator, IMutator stdDeviationsMutator, IMutationRuleSupervisor mutationRuleSupervisor, IParentsSelector parentsParentsSelector, ISurvivorsSelector survivorsSelector, IProcessor<Constraint[]> redundantConstrainsRemover, ExperimentParameters experimentParameters, Statistics statistics, Solution[] basePopulation, Solution[] offspringPopulation)
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
            RedundantConstriantsRemover = redundantConstrainsRemover;
            ExperimentParameters = experimentParameters;
            Statistics = statistics;
            BasePopulation = basePopulation;
            OffspringPopulation = offspringPopulation;
        }

        public ExperimentParameters ExperimentParameters { get; set; }
        public Statistics Statistics { get; set; }
        public IBenchmark Benchmark { get; set; }       

        public IList<Constraint> GetSynthesizedModel()
        {
            //return BasePopulation.First().GetConstraints(ExperimentParameters);
            return SynthesizedModel;
        }

        public IList<Constraint> GetReferenceModel()
        {
            return Benchmark.Constraints;
        }

        public virtual void SynthesizeModel(Point[] trainingPoints)
        {
            var offspringPopulationSize = ExperimentParameters.OffspringPopulationSize;
            var numberOfGenerations = ExperimentParameters.NumberOfGenerations;

            Evaluator.PositivePoints = trainingPoints.Where(tp => tp.ClassificationType == ClassificationType.Positive).ToArray();
            Evaluator.NegativePoints = trainingPoints.Where(tp => tp.ClassificationType == ClassificationType.Negative).ToArray();

            BasePopulation = PopulationGenerator.GeneratePopulation(ExperimentParameters);

            for (var i = 0; i < offspringPopulationSize; i++)
                OffspringPopulation[i] = new Solution(ExperimentParameters);

            InitialPopulation = BasePopulation.DeepCopyByExpressionTree();

            for (var i = 0; i < numberOfGenerations; i++)
            {
                Evolve(offspringPopulationSize);
            }    

            SynthesizedModel = RedundantConstriantsRemover.ApplyProcessing(BasePopulation.First().GetConstraints(ExperimentParameters));
        }

        public virtual Statistics EvaluateModel(Point[] testPoints)
        {
            var numberOfPoints = testPoints.Length;
            var constraints = GetSynthesizedModel();

            for (var i = 0; i < numberOfPoints; i++)
            {
                if (constraints.IsSatisfyingConstraints(testPoints[i]))
                {
                    switch (testPoints[i].ClassificationType)
                    {
                        case ClassificationType.Negative:
                            Statistics.FalsePositives++;
                            break;
                        case ClassificationType.Positive:
                            Statistics.TruePositives++;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    switch (testPoints[i].ClassificationType)
                    {
                        case ClassificationType.Negative:
                            Statistics.TrueNegatives++;
                            break;
                        case ClassificationType.Positive:
                            Statistics.FalseNegatives++;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            return Statistics;
        }

        protected abstract void Evolve(int offspringPopulationSize);
    }
}
