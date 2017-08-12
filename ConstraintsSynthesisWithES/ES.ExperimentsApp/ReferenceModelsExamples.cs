using System.Collections.Generic;
using ES.Engine.Constraints;

namespace ES.ExperimentsApp
{
    public static class ReferenceModelsExamples
    {
        public static List<Constraint> Example1 => new List<Constraint>
            {
                new Linear2DConstraint(1, 60, Linear2DConstraint.InequalityValue.UnderLine),
                new Linear2DConstraint(1, 0, Linear2DConstraint.InequalityValue.OverLine),
                new Linear2DConstraint(-2, 60, Linear2DConstraint.InequalityValue.UnderLine),
                new Linear2DConstraint(-2, 0, Linear2DConstraint.InequalityValue.OverLine)
            };

        public static List<Constraint> Example2 => new List<Constraint>
            {
                //new LinearConstraint(new []{1.0, 0}, 10.0),
                //new LinearConstraint(new []{0, -1.0}, 10.0),
                new LinearConstraint(new []{-1.0, 1.0}, 20.0),
                new LinearConstraint(new []{1.0, -1.0}, 20.0),

                new LinearConstraint(new []{-1.0, -1.0}, 20.0),
                new LinearConstraint(new []{1.0, 1.0}, 20.0)
            };

        public static List<Constraint> Example3 => new List<Constraint>
            {
                new LinearConstraint(new []{1.0, 0}, 20),
                new LinearConstraint(new []{-1.0, 0}, 20),
                new LinearConstraint(new []{0, 1.0}, 20),
                new LinearConstraint(new []{0, -1.0}, 20)
            };
    }
}
