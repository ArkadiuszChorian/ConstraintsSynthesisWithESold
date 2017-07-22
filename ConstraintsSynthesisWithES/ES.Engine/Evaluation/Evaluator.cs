using ES.Engine.Constraints;
using ES.Engine.Models;
using ES.Engine.Solutions;
using ES.Engine.Utils;

namespace ES.Engine.Evaluation
{
    public class Evaluator : IEvaluator
    {
        public Evaluator(ExperimentParameters experimentParameters)
        {
            ExperimentParameters = experimentParameters;
            NumberOfConstraints = experimentParameters.NumberOfConstraints;
            NumberOfConstraintCoefficients = experimentParameters.NumberOfDimensions + 1;
        }
        public Evaluator(ExperimentParameters experimentParameters, Point[] positiveMeasurePoints)
        {
            ExperimentParameters = experimentParameters;
            PositiveMeasurePoints = positiveMeasurePoints;
            NumberOfConstraints = experimentParameters.NumberOfConstraints;
            NumberOfConstraintCoefficients = experimentParameters.NumberOfDimensions + 1;
        }
        public Evaluator(ExperimentParameters experimentParameters, Point[] positiveMeasurePoints, Point[] negativeMeasurePoints)
        {
            ExperimentParameters = experimentParameters;
            PositiveMeasurePoints = positiveMeasurePoints;
            NegativeMeasurePoints = negativeMeasurePoints;
            NumberOfConstraints = experimentParameters.NumberOfConstraints;
            NumberOfConstraintCoefficients = experimentParameters.NumberOfDimensions + 1;
        }

        public Point[] PositiveMeasurePoints { get; set; }
        public Point[] NegativeMeasurePoints { get; set; }

        //Experiment parameters
        public int NumberOfConstraints { get; set; }
        public int NumberOfConstraintCoefficients { get; set; }
        public ExperimentParameters ExperimentParameters { get; set; }

        //public double Evaluate2(Solution solution)
        //{
        //    var constraints = solution.GetConstraints(ExperimentParameters);
        //    var numberOfPositivePointsSatisfyingConstraints = PositiveMeasurePoints.Count(point => IsSatisfyingConstraints(constraints, point));
        //    var numberOfNegativePointsSatisfyingConstraints = NegativeMeasurePoints.Count(point => IsSatisfyingConstraints(constraints, point));

        //    return (double)numberOfPositivePointsSatisfyingConstraints / (PositiveMeasurePoints.Length + numberOfNegativePointsSatisfyingConstraints);
        //    //return (double)numberOfPositivePointsSatisfyingConstraints / (PositiveMeasurePoints.Length + NegativeMeasurePoints.Length);
        //    //return (double)numberOfPositivePointsSatisfyingConstraints;
        //}

        public double Evaluate(Solution solution)
        {
            var numberOfPositivePointsSatisfyingConstraints = 0;
            var numberOfNegativePointsSatisfyingConstraints = 0;
            var constraints = solution.GetConstraints(ExperimentParameters);

            for (var i = 0; i < PositiveMeasurePoints.Length; i++)
            {
                if (IsSatisfyingConstraints(constraints, PositiveMeasurePoints[i]))
                    numberOfPositivePointsSatisfyingConstraints++;
            }

            for (var i = 0; i < NegativeMeasurePoints.Length; i++)
            {
                if (IsSatisfyingConstraints(constraints, NegativeMeasurePoints[i]))
                    numberOfNegativePointsSatisfyingConstraints++;
            }

            //if (numberOfNegativePointsSatisfyingConstraints > 0 || numberOfPositivePointsSatisfyingConstraints > 0)
            //{
            //    Debugger.Break();
            //}

            return (double)numberOfPositivePointsSatisfyingConstraints / (PositiveMeasurePoints.Length + numberOfNegativePointsSatisfyingConstraints);
        }

        //private bool IsSatisfyingConstraints2(Solution solution, Point point)
        //{
        //    for (var i = 0; i < NumberOfConstraints; i += NumberOfConstraintCoefficients)
        //    {
        //        var constraintComputedValue = 0.0;
        //        var constraintLimitingValue = solution.ObjectCoefficients[i + NumberOfConstraintCoefficients - 1];

        //        for (var j = i; j < NumberOfConstraintCoefficients - 1; j++)
        //        {
        //            constraintComputedValue += solution.ObjectCoefficients[j] * point.Coordinates[j - i];
        //        }

        //        if (constraintComputedValue > constraintLimitingValue)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        //public double Evaluate(Solution solution)
        //{
        //    var numberOfPositivePointsSatisfyingConstraints = 0;
        //    var numberOfNegativePointsSatisfyingConstraints = 0;
        //    //var numberOfRestrictions = Arguments.Get<int>("NumberOfRestrictions");
        //    //var numberOfConstraints = Arguments.Get<int>("NumberOfRestrictions");
        //    //var numberOfConstraintCoefficients = solution.ObjectCoefficients.Length / numberOfConstraints;

        //    for (var i = 0; i < PositiveMeasurePoints.Length; i++)
        //    {
        //        if (IsSatisfyingConstraints(solution, PositiveMeasurePoints[i]))
        //        {
        //            numberOfPositivePointsSatisfyingConstraints++;
        //        }
        //    }

        //    for (var i = 0; i < NegativeMeasurePoints.Length; i++)
        //    {
        //        if (IsSatisfyingConstraints(solution, NegativeMeasurePoints[i]))
        //        {
        //            numberOfNegativePointsSatisfyingConstraints++;
        //        }
        //    }

        //    return (double)numberOfPositivePointsSatisfyingConstraints / (PositiveMeasurePoints.Length + numberOfNegativePointsSatisfyingConstraints);
        //    //return (double)numberOfPositivePointsSatisfyingConstraints / (PositiveMeasurePoints.Length + NegativeMeasurePoints.Length);
        //    //return (double)numberOfPositivePointsSatisfyingConstraints;
        //}

        //private bool IsSatisfyingConstraints2(List<Constraint> constraints, Point point)
        //{
        //    return constraints.All(constraint => constraint.IsSatisfyingConstraint(point));
        //}

        private static bool IsSatisfyingConstraints(Constraint[] constraints, Point point)
        {
            var length = constraints.Length;

            for (var i = 0; i < length; i++)
            {
                if (!constraints[i].IsSatisfyingConstraint(point))
                    return false;
            }

            return true;
        }
    }
}
