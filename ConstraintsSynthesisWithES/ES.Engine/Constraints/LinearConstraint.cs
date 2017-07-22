﻿using ES.Engine.Models;

namespace ES.Engine.Constraints
{
    public class LinearConstraint : Constraint
    {
        public LinearConstraint(double[] termsCoefficients, double limitingValue) : base(termsCoefficients, limitingValue)
        {
        }

        public override bool IsSatisfyingConstraint(Point point)
        {
            var constraintSum = 0.0;
            var numberOfDimensions = TermsCoefficients.Length;

            for (var i = 0; i < numberOfDimensions; i++)
            {
                constraintSum += TermsCoefficients[i] * point.Coordinates[i];
            }

            return constraintSum <= LimitingValue;
        }
    }
}
