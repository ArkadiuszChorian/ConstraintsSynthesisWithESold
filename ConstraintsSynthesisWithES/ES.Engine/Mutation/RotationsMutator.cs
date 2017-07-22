using System;
using ES.Engine.Models;
using ES.Engine.Solutions;
using ES.Engine.Utils;

namespace ES.Engine.Mutation
{
    public class RotationsMutator : IMutator
    {
        private readonly MersenneTwister _randomGenerator;

        public RotationsMutator(ExperimentParameters experimentParameters)
        {
            RotationAngle = experimentParameters.RotationAngle;
            _randomGenerator = MersenneTwister.Instance;
        }

        public double RotationAngle { get; set; }

        public Solution Mutate(Solution solution)
        {
            var vectorSize = solution.RotationsCoefficients.Length;

            //for (var i = 0; i < vectorSize; i++)
            //{
            //    for (var j = i + 1; j < vectorSize; j++)
            //    {
            //        //var mutationValue = RotationAngle * MersenneTwister.Instance.NextDoublePositive();
            //        var mutationValue = RotationAngle * _randomGenerator.NextDoublePositive();
            //        //var mutationValue = RotationAngle * 0.5;

            //        solution.RotationsCoefficients[i][j] += mutationValue;
            //        solution.RotationsCoefficients[j][i] += mutationValue;

            //        if (Math.Abs(solution.RotationsCoefficients[i][j]) > Math.PI)
            //        {
            //            var reduction = 2 * Math.PI * Math.Sign(solution.RotationsCoefficients[i][j]);

            //            solution.RotationsCoefficients[i][j] -= reduction;
            //            solution.RotationsCoefficients[j][i] -= reduction;
            //        }
            //    }
            //}

            for (var i = 0; i < vectorSize; i++)
            {
                //var mutationValue = RotationAngle * MersenneTwister.Instance.NextDoublePositive();
                var mutationValue = RotationAngle * _randomGenerator.NextDoublePositive();
                //var mutationValue = RotationAngle * 0.5;

                solution.RotationsCoefficients[i] += mutationValue;

                if (!(Math.Abs(solution.RotationsCoefficients[i]) > Math.PI)) continue;
                var reduction = 2 * Math.PI * Math.Sign(solution.RotationsCoefficients[i]);

                solution.RotationsCoefficients[i] -= reduction;
            }

            return solution;
        }
    }
}
