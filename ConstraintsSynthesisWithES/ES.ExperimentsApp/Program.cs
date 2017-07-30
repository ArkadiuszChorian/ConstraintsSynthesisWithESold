using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ES.Engine.Constraints;
using ES.Engine.DistanceMeasuring;
using ES.Engine.Engine;
using ES.Engine.Evaluation;
using ES.Engine.Models;
using ES.Engine.PointsGeneration;
using ES.Engine.PrePostProcessing;
using ES.Engine.Utils;
using ExperimentDatabase;
using OxyPlot;

namespace ES.ExperimentsApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var stoper = new Stopwatch();
            stoper.Start();

            var experimentParameters = new ExperimentParameters(6, 10,
                typeOfMutation: ExperimentParameters.MutationType.Correlated,
                typeOfBenchmark: ExperimentParameters.BenchmarkType.Cuben,
                stepThreshold: 0.1, numberOfGenerations: 100,
                basePopulationSize: 15,
                //basePopulationSize: 3,
                offspringPopulationSize: 100,
                //offspringPopulationSize: 20,
                globalLerningRate: 1 / Math.Sqrt(2 * 2),
                //globalLerningRate: 0.7,
                individualLearningRate: 1 / Math.Sqrt(2 * Math.Sqrt(2)),
                //individualLearningRate: 0.8,
                numberOfPositivePoints: 1000,
                numberOfNegativePoints: 1000,
                ballnBoundaryValue: 2,
                //simplexnBoundaryValue: 2,
                seed: 1,
                useRecombination: false,
                numberOfParentsSolutionsToSelect: 5,
                domainSamplingStep: 1
                );

            var experimentParameters4 = new ExperimentParameters(2, 10,
                typeOfMutation: ExperimentParameters.MutationType.Correlated,
                typeOfBenchmark: ExperimentParameters.BenchmarkType.Simplexn,
                stepThreshold: 0.1, numberOfGenerations: 300,
                basePopulationSize: 15,
                //basePopulationSize: 3,
                offspringPopulationSize: 100,
                //offspringPopulationSize: 20,
                globalLerningRate: 1 / Math.Sqrt(2 * 2),
                //globalLerningRate: 0.7,
                individualLearningRate: 1 / Math.Sqrt(2 * Math.Sqrt(2)),
                //individualLearningRate: 0.8,
                numberOfPositivePoints: 300,
                numberOfNegativePoints: 300,
                ballnBoundaryValue: 10
                );

            var experimentParameters3 = new ExperimentParameters(2, 10, 
                typeOfMutation: ExperimentParameters.MutationType.Correlated,
                stepThreshold: 0.1, numberOfGenerations: 300,
                basePopulationSize: 30,
                //basePopulationSize: 3,
                offspringPopulationSize: 200,
                //offspringPopulationSize: 20,
                globalLerningRate: 1 / Math.Sqrt(2 * 2),
                //globalLerningRate: 0.7,
                individualLearningRate: 1 / Math.Sqrt(2 * Math.Sqrt(2)),
                //individualLearningRate: 0.8,
                numberOfPositivePoints: 300,
                numberOfNegativePoints: 300
                );           

            var experimentParameters2 = new ExperimentParameters(2, 10,
                typeOfMutation: ExperimentParameters.MutationType.Correlated,
                stepThreshold: 0.1, numberOfGenerations: 10,
                basePopulationSize: 15,
                //basePopulationSize: 3,
                offspringPopulationSize: 100,
                //offspringPopulationSize: 20,
                globalLerningRate: 1 / Math.Sqrt(2 * 2),
                //globalLerningRate: 0.7,
                individualLearningRate: 1 / Math.Sqrt(2 * Math.Sqrt(2)),
                numberOfPositivePoints: 100,
                numberOfNegativePoints: 100
                );

            var visualization = new Visualization();

            var constraints = new List<Constraint>
            {
                new Linear2DConstraint(1, 60, Linear2DConstraint.InequalityValue.UnderLine),
                new Linear2DConstraint(1, 0, Linear2DConstraint.InequalityValue.OverLine),
                new Linear2DConstraint(-2, 60, Linear2DConstraint.InequalityValue.UnderLine),
                new Linear2DConstraint(-2, 0, Linear2DConstraint.InequalityValue.OverLine)
            };

            var constraints2 = new List<Constraint>
            {
                //new LinearConstraint(new []{1.0, 0}, 10.0),
                //new LinearConstraint(new []{0, -1.0}, 10.0),
                new LinearConstraint(new []{-1.0, 1.0}, 20.0),
                new LinearConstraint(new []{1.0, -1.0}, 20.0),

                new LinearConstraint(new []{-1.0, -1.0}, 20.0),
                new LinearConstraint(new []{1.0, 1.0}, 20.0)
            };

            var constraints3 = new List<Constraint>
            {
                new LinearConstraint(new []{1.0, 0}, 20),
                new LinearConstraint(new []{-1.0, 0}, 20),
                new LinearConstraint(new []{0, 1.0}, 20),
                new LinearConstraint(new []{0, -1.0}, 20)
            };

            experimentParameters.ConstraintsToPointsGeneration = constraints;

            var engine = EngineFactory.GetEngine(experimentParameters);

            var distanceCalculator = new CanberraDistanceCalculator();

            var positivePointsGenerator = new PositivePointsGenerator();
            var positiveTrainingPoints = positivePointsGenerator.GeneratePoints(experimentParameters.NumberOfPositivePoints, engine.Benchmark);

            var negativePointsGenerator = new NegativePointsGenerator(positiveTrainingPoints, distanceCalculator, new NearestNeighbourDistanceCalculator(distanceCalculator));
            var negativeTrainingPoints = negativePointsGenerator.GeneratePoints(experimentParameters.NumberOfNegativePoints, engine.Benchmark);

            var trainingPoints = positiveTrainingPoints.Concat(negativeTrainingPoints).ToArray();

            engine.SynthesizeModel(trainingPoints);

            //var bestSolutionConstraints = engine.BasePopulation.First().GetConstraints(experimentParameters);
            var synthesizedModel = engine.GetSynthesizedModel();

            //var evaluator = (Evaluator)engine.Evaluator;

            //var remover = new RedundantConstraintsRemover(new DomainSpaceSampler(), engine.Benchmark, experimentParameters);
            
            var testConstraints = new[]
            {
                new Linear2DConstraint(1, 20, Linear2DConstraint.InequalityValue.UnderLine),
                new Linear2DConstraint(1, -20, Linear2DConstraint.InequalityValue.OverLine),
                new Linear2DConstraint(-1, 20, Linear2DConstraint.InequalityValue.UnderLine),
                new Linear2DConstraint(-1, -20, Linear2DConstraint.InequalityValue.OverLine),
                new Linear2DConstraint(1, 80, Linear2DConstraint.InequalityValue.UnderLine),
                new Linear2DConstraint(1, 70, Linear2DConstraint.InequalityValue.UnderLine),
            };

            //var postConstraints = remover.ApplyProcessing(bestSolutionConstraints);
            //var postConstraints = remover.ApplyProcessing(bestSolutionConstraints.ToArray());

            if (experimentParameters.NumberOfDimensions == 2)
            {
                visualization.ShowTwoPlots(positiveTrainingPoints, negativeTrainingPoints, engine.Benchmark, synthesizedModel);
            }         

            //    .AddNextPlot()
            //    .AddPoints(evaluator.PositivePoints, OxyColors.Green)
            //    .AddPoints(evaluator.NegativePoints, OxyColors.Red)
            //    .AddConstraints(engine.Benchmark.Constraints, OxyPalettes.Rainbow, xMin: engine.Benchmark.Domains[0].LowerLimit, xMax: engine.Benchmark.Domains[0].UpperLimit)
            //    .AddNextPlot()
            //    .AddPoints(evaluator.PositivePoints, OxyColors.Green)
            //    .AddPoints(evaluator.NegativePoints, OxyColors.Red)
            //    .AddConstraints(bestSolutionConstraints, OxyPalettes.Rainbow)
            //    //.AddNextPlot()
            //    //.AddPoints(remover.Points, OxyColors.Orange)
            //    //.AddConstraints(testConstraints, OxyPalettes.Rainbow)
            //    .AddNextPlot()
            //    //.AddPoints(remover.Points, OxyColors.Orange)
            //    .AddPoints(evaluator.PositivePoints, OxyColors.Green)
            //    .AddPoints(evaluator.NegativePoints, OxyColors.Red)
            //    .AddConstraints(postConstraints, OxyPalettes.Rainbow, xMin: engine.Benchmark.Domains[0].LowerLimit, xMax: engine.Benchmark.Domains[0].UpperLimit)
            //    //.AddNextPlot()
            //    //.AddPoints(evaluator.PositivePoints, OxyColors.Green)
            //    //.AddPoints(evaluator.NegativePoints, OxyColors.Red)
            //    //.AddConstraints(engine.InitialPopulation.First().GetConstraints(experimentParameters), OxyPalettes.Rainbow)
            //    .Show();

            var positiveTestPoints = positivePointsGenerator.GeneratePoints(experimentParameters.NumberOfPositivePoints, engine.Benchmark);

            var negativeTestPoints = negativePointsGenerator.GeneratePoints(experimentParameters.NumberOfNegativePoints, engine.Benchmark);

            var testPoints = positiveTestPoints.Concat(negativeTestPoints).ToArray();

            var statistics = engine.EvaluateModel(testPoints);

            stoper.Stop();
            Console.WriteLine("Done!");
            Console.WriteLine("=== Time ===");
            Console.WriteLine("SEC = " + stoper.Elapsed.TotalSeconds);
            Console.WriteLine("MIN = " + stoper.Elapsed.TotalMinutes);
            Console.WriteLine("MIN = " + stoper.Elapsed.Minutes);

            Console.WriteLine("\n");
            Console.WriteLine("TP = " + statistics.TruePositives);
            Console.WriteLine("TN = " + statistics.TrueNegatives);
            Console.WriteLine("FP = " + statistics.FalsePositives);
            Console.WriteLine("FN = " + statistics.FalseNegatives);

            Console.WriteLine("Recall = " + statistics.Recall);
            Console.WriteLine("Precision = " + statistics.Precision);
            Console.WriteLine("Accuracy = " + statistics.Accuracy);

            Console.WriteLine("F1 = " + statistics.F1Score);

            Console.ReadKey();
        }     
    }
}
