using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using ES.Engine.DistanceMeasuring;
using ES.Engine.Engine;
using ES.Engine.Models;
using ES.Engine.PointsGeneration;
using ES.Engine.Utils;
using Version = ES.Engine.Models.Version;

namespace ES.ExperimentsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;//CultureInfo.GetCultureInfo("en-US");

            var dbPath = Path.GetFullPath("Database test.db");
            var databaseContext = new DatabaseContext(dbPath);
            
            //var experimentParameters = Arguments.GetExperimentParameters();

            var experimentParameters = new ExperimentParameters(2, 10,
                typeOfMutation: ExperimentParameters.MutationType.Correlated,
                typeOfBenchmark: ExperimentParameters.BenchmarkType.Simplexn,
                stepThreshold: 0.1, numberOfGenerations: 1,
                basePopulationSize: 15,
                //basePopulationSize: 3,
                offspringPopulationSize: 100,
                //offspringPopulationSize: 20,
                globalLerningRate: 1 / Math.Sqrt(2 * 2),
                //globalLerningRate: 0.7,
                individualLearningRate: 1 / Math.Sqrt(2 * Math.Sqrt(2)),
                //individualLearningRate: 0.8,
                numberOfPositivePoints: 10,
                numberOfNegativePoints: 10,
                ballnBoundaryValue: 2,
                //simplexnBoundaryValue: 2,
                seed: 1,
                useRecombination: false,
                numberOfParentsSolutionsToSelect: 5
                //domainSamplingStep: 1
                );           

            if (databaseContext.Exists(experimentParameters)) return;

            if (experimentParameters.TypeOfBenchmark == ExperimentParameters.BenchmarkType.Other)
            {
                experimentParameters.ConstraintsToPointsGeneration = ReferenceModelsExamples.Example1;
            }

            var version = new Version(DateTime.Now, experimentParameters.GetHashString());
            var stoper = new Stopwatch();
            var distanceCalculator = new CanberraDistanceCalculator();

            var engine = EngineFactory.GetEngine(experimentParameters);

            stoper.Restart();
           
            var positivePointsGenerator = new PositivePointsGenerator();
            var positiveTrainingPoints = positivePointsGenerator.GeneratePoints(experimentParameters.NumberOfPositivePoints, engine.Benchmark);

            stoper.Stop();
            engine.Statistics.PositiveTrainingPointsGenerationTime = stoper.Elapsed;
            stoper.Restart();

            var negativePointsGenerator = new NegativePointsGenerator(positiveTrainingPoints, distanceCalculator, new NearestNeighbourDistanceCalculator(distanceCalculator));
            var negativeTrainingPoints = negativePointsGenerator.GeneratePoints(experimentParameters.NumberOfNegativePoints, engine.Benchmark);

            stoper.Stop();
            engine.Statistics.NegativeTrainingPointsGenerationTime = stoper.Elapsed;
         
            var trainingPoints = positiveTrainingPoints.Concat(negativeTrainingPoints).ToArray();

            var mathModel = engine.SynthesizeModel(trainingPoints);

            stoper.Restart();

            var positiveTestPoints = positivePointsGenerator.GeneratePoints(experimentParameters.NumberOfPositivePoints, engine.Benchmark);

            var negativeTestPoints = negativePointsGenerator.GeneratePoints(experimentParameters.NumberOfNegativePoints, engine.Benchmark);

            var testPoints = positiveTestPoints.Concat(negativeTestPoints).ToArray();

            stoper.Stop();
            engine.Statistics.TestPointsGenerationTime = stoper.Elapsed;

            var statistics = engine.EvaluateModel(testPoints);

            databaseContext.Insert(version);
            databaseContext.Insert(experimentParameters);
            databaseContext.Insert(statistics);
            databaseContext.Insert(mathModel);
            databaseContext.Save();
            databaseContext.Dispose(); 
            
            TryGetVisibleResults(engine, positiveTrainingPoints, negativeTrainingPoints);                
        }

        private static void TryGetVisibleResults(IEngine engine, IList<Point> positiveTrainingPoints, IList<Point> negativeTrainingPoints)
        {
            if (Arguments.HasAnyKeys()) return;

            Console.WriteLine("#############################################");
            Console.WriteLine("################## FINISHED #################");
            Console.WriteLine("#############################################");
            Console.Write(engine.Statistics.ToReadableString());

            if (engine.ExperimentParameters.NumberOfDimensions == 2)
            {
                new Visualization().ShowTwoPlots(positiveTrainingPoints, negativeTrainingPoints, engine.MathModel);
            }

            Console.ReadKey();
        }

        //private static string GetDbPath(string fileName)
        //{
        //    if (Environment.OSVersion.Platform == PlatformID.Unix) return fileName;

        //    var driveToStore = DriveInfo.GetDrives().Any(x => x.Name.Contains("F"))
        //        ? DriveInfo.GetDrives().First(d => d.Name.Contains("F")).Name
        //        : DriveInfo.GetDrives().First(d => d.Name.Contains("C")).Name;

        //    var pathToOneDrive = Directory.GetDirectories(driveToStore, "OneDrive?", SearchOption.AllDirectories).FirstOrDefault();

        //    return Path.GetFullPath(pathToOneDrive + "\\MGR Database\\" + fileName);
        //}
    }
}
