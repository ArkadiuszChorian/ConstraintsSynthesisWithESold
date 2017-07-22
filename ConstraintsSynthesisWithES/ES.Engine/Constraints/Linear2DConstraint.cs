namespace ES.Engine.Constraints
{
    public class Linear2DConstraint : LinearConstraint
    {
        public Linear2DConstraint(double[] termsCoefficients, double limitingValue) : base(termsCoefficients, limitingValue)
        {
        }

        public Linear2DConstraint(double a, double b, InequalityValues inequalityValues) : this(new []{-a, (double)inequalityValues * 1.0}, (double)inequalityValues * b)
        {           
        }

        public enum InequalityValues
        {
            OverLine = -1,
            UnderLine = 1
        }
    }
}
