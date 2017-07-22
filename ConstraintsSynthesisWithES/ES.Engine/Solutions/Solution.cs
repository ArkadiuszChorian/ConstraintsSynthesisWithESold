using System;
using ES.Engine.Models;

namespace ES.Engine.Solutions
{
    public class Solution : IComparable<Solution>
    {
        public Solution(ExperimentParameters experimentParameters)
        {
            ObjectCoefficients = new double[(experimentParameters.NumberOfDimensions + 1) * experimentParameters.NumberOfConstraints];

            switch (experimentParameters.TypeOfMutation)
            {
                case ExperimentParameters.MutationType.UncorrelatedOneStep:
                    break;
                case ExperimentParameters.MutationType.UncorrelatedNSteps:
                    StdDeviationsCoefficients = new double[(experimentParameters.NumberOfDimensions + 1) * experimentParameters.NumberOfConstraints];
                    break;
                case ExperimentParameters.MutationType.Correlated:
                    StdDeviationsCoefficients = new double[(experimentParameters.NumberOfDimensions + 1) * experimentParameters.NumberOfConstraints];

                    //var size = (experimentParameters.NumberOfDimensions * experimentParameters.NumberOfConstraints) * (experimentParameters.NumberOfDimensions * experimentParameters.NumberOfConstraints - 1) / 2;
                    //var size = ObjectCoefficients.Length * (ObjectCoefficients.Length - 1) / 2;
                    var size = ObjectCoefficients.Length * (ObjectCoefficients.Length - 1) / 2 + ObjectCoefficients.Length;

                    //RotationsCoefficients = new double[experimentParameters.NumberOfDimensions * (experimentParameters.NumberOfDimensions - 1) / 2][];
                    RotationsCoefficients = new double[size];

                    //for (var i = 0; i < RotationsCoefficients.Length; i++)
                    //{                        
                    //    //RotationsCoefficients[i] = new double[experimentParameters.NumberOfDimensions * (experimentParameters.NumberOfDimensions - 1) / 2];
                    //    RotationsCoefficients[i] = new double[size];
                    //}
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }           
        }

        public Solution(int vectorSize)
        {
            ObjectCoefficients = new double[vectorSize];
            StdDeviationsCoefficients = new double[vectorSize];

            //RotationsCoefficients = new double[vectorSize * (vectorSize - 1) / 2];
            RotationsCoefficients = new double[vectorSize * (vectorSize - 1) / 2];

            //for (var i = 0; i < RotationsCoefficients.Length; i++)
            //{
            //    RotationsCoefficients[i] = new double[vectorSize * (vectorSize - 1) / 2];
            //}
        }

        public Solution(Solution solution)
        {
            ObjectCoefficients = solution.ObjectCoefficients;
            OneStepStdDeviation = solution.OneStepStdDeviation;
            StdDeviationsCoefficients = solution.StdDeviationsCoefficients;
            RotationsCoefficients = solution.RotationsCoefficients;
            FitnessScore = solution.FitnessScore;
        }

        public double[] ObjectCoefficients { get; set; }
        public double OneStepStdDeviation { get; set; }
        public double[] StdDeviationsCoefficients { get; set; }
        //public double[][] RotationsCoefficients { get; set; }
        public double[] RotationsCoefficients { get; set; }
        public double FitnessScore { get; set; }

        public int CompareTo(Solution other)
        {
            //Minus sign to sort descending
            return -FitnessScore.CompareTo(other.FitnessScore);
        }
    }
}
