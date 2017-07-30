using System;
using ES.Engine.Models;
using ES.Engine.Utils;

namespace ES.Engine.Mutation
{
    public class CmObjectMutator : IMutator
    {
        private readonly double[] _zeroMeans;

        public CmObjectMutator(ExperimentParameters experimentParameters)
        {
            _zeroMeans = new double[(experimentParameters.NumberOfDimensions + 1) * experimentParameters.NumberOfConstraints];
        }

        public Solution Mutate(Solution solution)
        {
            var vectorSize = solution.ObjectCoefficients.Length;
            var covarianceMatrix = new double[vectorSize, vectorSize];

            for (var i = 0; i < vectorSize; i++)
            {
                for (var j = 0; j < vectorSize; j++)
                {
                    if (i == j)
                    {
                        covarianceMatrix[i, j] = Math.Pow(solution.StdDeviationsCoefficients[i], 2);
                    }
                    else
                    {
                        //covarianceMatrix[i, j] = (Math.Pow(solution.StdDeviationsCoefficients[i], 2) - Math.Pow(solution.StdDeviationsCoefficients[j], 2)) * Math.Tan(2 * solution.RotationsCoefficients[i][j]) / 2;
                        covarianceMatrix[i, j] = (Math.Pow(solution.StdDeviationsCoefficients[i], 2) - Math.Pow(solution.StdDeviationsCoefficients[j], 2)) * Math.Tan(2 * solution.RotationsCoefficients[FromMatrixToVector(i, j, vectorSize)]) / 2;
                    }
                }
            }

            var mutationVector = new RobustMultivariateNormalDistribution(_zeroMeans, covarianceMatrix).Generate();

            for (var i = 0; i < solution.ObjectCoefficients.Length; i++)
            {
                solution.ObjectCoefficients[i] += mutationVector[i];
            }

            return solution;
        }

        private static int FromMatrixToVector(int i, int j, int n)
        {
            if (i <= j)
                return i * n - (i - 1) * i / 2 + j - i;
            return j * n - (j - 1) * j / 2 + i - j;
        }
    }
}
