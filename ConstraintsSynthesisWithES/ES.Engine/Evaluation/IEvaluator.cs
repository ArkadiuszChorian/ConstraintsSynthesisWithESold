using ES.Engine.Models;

namespace ES.Engine.Evaluation
{
    public interface IEvaluator
    {
        Point[] PositivePoints { get; set; }
        Point[] NegativePoints { get; set; }
        //Point[] Points { get; set; }

        double Evaluate(Solution solution);
    }
}
