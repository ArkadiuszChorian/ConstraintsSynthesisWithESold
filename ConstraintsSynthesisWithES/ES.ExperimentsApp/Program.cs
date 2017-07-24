using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ES.Engine.Constraints;
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
        private const bool DbTest = true;

        static void Main(string[] args)
        {
            if (DbTest)
            {
                //TestDb();
                //return;
            }

            var stoper = new Stopwatch();
            stoper.Start();

            var experimentParameters = new ExperimentParameters(2, 10,
                typeOfMutation: ExperimentParameters.MutationType.Correlated,
                typeOfBenchmark: ExperimentParameters.BenchmarkType.Balln,
                stepThreshold: 0.1, numberOfGenerations: 100,
                basePopulationSize: 15,
                //basePopulationSize: 3,
                offspringPopulationSize: 100,
                //offspringPopulationSize: 20,
                globalLerningRate: 1 / Math.Sqrt(2 * 2),
                //globalLerningRate: 0.7,
                individualLearningRate: 1 / Math.Sqrt(2 * Math.Sqrt(2)),
                //individualLearningRate: 0.8,
                numberOfPositiveMeasurePoints: 1000,
                numberOfNegativeMeasurePoints: 1000,
                ballnBoundaryValue: 2,
                //simplexnBoundaryValue: 2,
                seed: 1,
                useRecombination: false,
                numberOfParentsSolutionsToSelect: 5
                //domainSamplingStep: 0.8
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
                numberOfPositiveMeasurePoints: 300,
                numberOfNegativeMeasurePoints: 300,
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
                numberOfPositiveMeasurePoints: 300,
                numberOfNegativeMeasurePoints: 300
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
                numberOfPositiveMeasurePoints: 100,
                numberOfNegativeMeasurePoints: 100
                );

            var visualization = new Visualization();

            var constraints = new List<Constraint>
            {
                new Linear2DConstraint(1, 60, Linear2DConstraint.InequalityValues.UnderLine),
                new Linear2DConstraint(1, 0, Linear2DConstraint.InequalityValues.OverLine),
                new Linear2DConstraint(-2, 60, Linear2DConstraint.InequalityValues.UnderLine),
                new Linear2DConstraint(-2, 0, Linear2DConstraint.InequalityValues.OverLine)
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
            engine.RunExperiment();

            var bestSolutionConstraints = engine.BasePopulation.First().GetConstraints(experimentParameters);

            var evaluator = (Evaluator)engine.Evaluator;

            var remover = new RedundantConstraintsRemover(new DomainSpaceSampler(), engine.Benchmark, experimentParameters);
            
            var testConstraints = new[]
            {
                new Linear2DConstraint(1, 20, Linear2DConstraint.InequalityValues.UnderLine),
                new Linear2DConstraint(1, -20, Linear2DConstraint.InequalityValues.OverLine),
                new Linear2DConstraint(-1, 20, Linear2DConstraint.InequalityValues.UnderLine),
                new Linear2DConstraint(-1, -20, Linear2DConstraint.InequalityValues.OverLine),
                new Linear2DConstraint(1, 80, Linear2DConstraint.InequalityValues.UnderLine),
                new Linear2DConstraint(1, 70, Linear2DConstraint.InequalityValues.UnderLine),
            };

            var postConstraints = remover.ApplyProcessing(bestSolutionConstraints);

            visualization
                .AddNextPlot()
                .AddPoints(evaluator.PositivePoints, OxyColors.Green)
                .AddPoints(evaluator.NegativePoints, OxyColors.Red)
                .AddConstraints(engine.Benchmark.Constraints, OxyPalettes.Rainbow, xMin: engine.Benchmark.Domains[0].LowerLimit, xMax: engine.Benchmark.Domains[0].UpperLimit)
                .AddNextPlot()
                .AddPoints(evaluator.PositivePoints, OxyColors.Green)
                .AddPoints(evaluator.NegativePoints, OxyColors.Red)
                .AddConstraints(bestSolutionConstraints, OxyPalettes.Rainbow)
                //.AddNextPlot()
                //.AddPoints(remover.Points, OxyColors.Orange)
                //.AddConstraints(testConstraints, OxyPalettes.Rainbow)
                .AddNextPlot()
                //.AddPoints(remover.Points, OxyColors.Orange)
                .AddPoints(evaluator.PositivePoints, OxyColors.Green)
                .AddPoints(evaluator.NegativePoints, OxyColors.Red)
                .AddConstraints(postConstraints, OxyPalettes.Rainbow, xMin: engine.Benchmark.Domains[0].LowerLimit, xMax: engine.Benchmark.Domains[0].UpperLimit)
                //.AddNextPlot()
                //.AddPoints(evaluator.PositivePoints, OxyColors.Green)
                //.AddPoints(evaluator.NegativePoints, OxyColors.Red)
                //.AddConstraints(engine.InitialPopulation.First().GetConstraints(experimentParameters), OxyPalettes.Rainbow)
                .Show();

            //var engine2 = engine as CmEngineWithoutRecombination;

            //for (var i = 0; i < engine2.OneSolutionHistory.Count; i++)
            //{
            //    visualization
            //        .AddNextPlot(title: "Step " + i)
            //        .AddPoints(evaluator.PositivePoints, OxyColors.Green)
            //        .AddConstraints(engine2.OneSolutionHistory[i].GetConstraints(experimentParameters), OxyPalettes.Rainbow);
            //}

            //visualization.Show();

            stoper.Stop();
            Console.WriteLine("Done!");
            Console.WriteLine("=== Time ===");
            Console.WriteLine("SEC = " + stoper.Elapsed.TotalSeconds);
            Console.WriteLine("MIN = " + stoper.Elapsed.TotalMinutes);
            Console.WriteLine("MIN = " + stoper.Elapsed.Minutes);
            Console.ReadKey();
        }

        private static void TestDb()
        {
            var path = DatabaseConfig.DbFullPath;
            var db = new Database(path);            
            var exp = db.NewExperiment();
            for (int i = 0; i < DatabaseConfig.ColumnsNames.Count; i++)
            {
                exp.Add(DatabaseConfig.ColumnsNames[i], i);
            }
            exp.Add("Gowno", 45);
            var set = exp.NewChildDataSet("tablica2");
            set.Add("wewnetrzne", 33);
            var set2 = set.NewChildDataSet("tablica3");
            set2.Add("yyy", 44);
            set2.Save();
            set.Save();
            exp.Save();
            exp.Dispose();
            db.Dispose();
        }
    }
}
