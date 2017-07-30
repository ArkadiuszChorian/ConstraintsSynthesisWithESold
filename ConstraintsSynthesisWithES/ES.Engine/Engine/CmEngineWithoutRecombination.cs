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

namespace ES.Engine.Engine
{
    public class CmEngineWithoutRecombination : UmEngineWithoutRecombination
    {
        //public CmEngineWithoutRecombination(IBenchmark benchmark, IPopulationGenerator populationGenerator, IEvaluator evaluator, ILogger logger, IMutator objectMutator, IMutator stdDeviationsMutator, IMutationRuleSupervisor mutationRuleSupervisor, IParentsSelector parentsParentsSelector, ISurvivorsSelector survivorsSelector, IPointsGenerator positivePointsGenerator, IPointsGenerator negativePointsGenerator, ExperimentParameters experimentParameters, Solution[] basePopulation, Solution[] offspringPopulation, IMutator rotationsMutator) : base(benchmark, populationGenerator, evaluator, logger, objectMutator, stdDeviationsMutator, mutationRuleSupervisor, parentsParentsSelector, survivorsSelector, positivePointsGenerator, negativePointsGenerator, experimentParameters, basePopulation, offspringPopulation)
        //{
        //    RotationsMutator = rotationsMutator;
        //}

        public CmEngineWithoutRecombination(IBenchmark benchmark, IPopulationGenerator populationGenerator, IEvaluator evaluator, ILogger logger, IMutator objectMutator, IMutator stdDeviationsMutator, IMutationRuleSupervisor mutationRuleSupervisor, IParentsSelector parentsParentsSelector, ISurvivorsSelector survivorsSelector, IProcessor<Constraint[]> redundantConstrainsRemover, ExperimentParameters experimentParameters, Statistics statistics, Solution[] basePopulation, Solution[] offspringPopulation, IMutator rotationsMutator) : base(benchmark, populationGenerator, evaluator, logger, objectMutator, stdDeviationsMutator, mutationRuleSupervisor, parentsParentsSelector, survivorsSelector, redundantConstrainsRemover, experimentParameters, statistics, basePopulation, offspringPopulation)
        {
            RotationsMutator = rotationsMutator;
        }

        protected IMutator RotationsMutator;
               
        //public override void SynthesizeModel(Point[] trainingPoints)
        //{
        //    var offspringPopulationSize = ExperimentParameters.OffspringPopulationSize;
        //    var numberOfGenerations = ExperimentParameters.NumberOfGenerations;
            
        //    BasePopulation = PopulationGenerator.GeneratePopulation(ExperimentParameters);
            
        //    for (var i = 0; i < offspringPopulationSize; i++)
        //        OffspringPopulation[i] = new Solution(ExperimentParameters);           

        //    InitialPopulation = BasePopulation.DeepCopyByExpressionTree();

        //    for (var i = 0; i < numberOfGenerations; i++)
        //    {
        //        for (var j = 0; j < offspringPopulationSize; j++)
        //        {
        //            OffspringPopulation[j] = ParentsSelector.Select(BasePopulation)[0];
                    
        //            OffspringPopulation[j] = StdDeviationsMutator.Mutate(OffspringPopulation[j]);
        //            OffspringPopulation[j] = RotationsMutator.Mutate(OffspringPopulation[j]);
        //            OffspringPopulation[j] = ObjectMutator.Mutate(OffspringPopulation[j]);

        //            OffspringPopulation[j].FitnessScore = Evaluator.Evaluate(OffspringPopulation[j]);
        //        }
        //        BasePopulation = SurvivorsSelector.Select(BasePopulation, OffspringPopulation);
        //    }
        //}

        protected override void Evolve(int offspringPopulationSize)
        {
            for (var j = 0; j < offspringPopulationSize; j++)
            {
                OffspringPopulation[j] = ParentsSelector.Select(BasePopulation)[0];

                OffspringPopulation[j] = StdDeviationsMutator.Mutate(OffspringPopulation[j]);
                OffspringPopulation[j] = RotationsMutator.Mutate(OffspringPopulation[j]);
                OffspringPopulation[j] = ObjectMutator.Mutate(OffspringPopulation[j]);

                OffspringPopulation[j].FitnessScore = Evaluator.Evaluate(OffspringPopulation[j]);
            }
            BasePopulation = SurvivorsSelector.Select(BasePopulation, OffspringPopulation);
        }
    }
}
