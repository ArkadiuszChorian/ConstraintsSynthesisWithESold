using System.Collections.Generic;
using System.Linq;
using ES.Engine.Benchmarks;
using ES.Engine.Constraints;
using ES.Engine.Models;
using ES.Engine.PointsGeneration;

namespace ES.Engine.PrePostProcessing
{
    public class RedundantConstraintsRemover : IProcessor<Constraint[]>
    {
        private readonly IPointsGenerator _domainSpaceSampler;
        private readonly IBenchmark _benchmark;
        private readonly int _numberOfPointsToGenerate;

        public RedundantConstraintsRemover(IPointsGenerator pointsGenerator, IBenchmark benchmark, ExperimentParameters experimentParameters)
        {
            _domainSpaceSampler = pointsGenerator;
            _benchmark = benchmark;
            
            var numberOfDimensions = benchmark.Domains.Length;
            var domains = benchmark.Domains;
            var domainSamplingStep = experimentParameters.DomainSamplingStep;
            var temp = 1.0;

            for (var i = 0; i < numberOfDimensions; i++)
            {
                temp *= (domains[i].UpperLimit - domains[i].LowerLimit) / domainSamplingStep;
            }

            _numberOfPointsToGenerate = (int) temp;

            //var spaceSize = 1.0;



            //_numberOfPointsToGenerate = (int)(experimentParameters.DomainSamplingStep * spaceSize);
        }

        public Constraint[] ApplyProcessing(Constraint[] constraints)
        {          
            var points = _domainSpaceSampler.GeneratePoints(_numberOfPointsToGenerate, _benchmark);
            Points = points;         

            var numberOfPoints = points.Length;
            var allConstraints = constraints.ToList();
            var reducedConstraints = new List<Constraint>();
            
            for (var i = 0; i < numberOfPoints; i++)
            {
                var numberOfConstraints = allConstraints.Count;
                var isCutByOneConstraint = false;
                Constraint obligatoryConstraint = null;        

                for (var j = 0; j < numberOfConstraints; j++)
                {
                    if (allConstraints[j].IsSatisfyingConstraint(points[i])) continue;

                    if (isCutByOneConstraint)
                    {
                        isCutByOneConstraint = false;
                        break;
                    }
                    
                    isCutByOneConstraint = true;
                    obligatoryConstraint = allConstraints[j];
                }

                if (isCutByOneConstraint && !reducedConstraints.Contains(obligatoryConstraint))
                {
                    reducedConstraints.Add(obligatoryConstraint);
                }
            }

            return reducedConstraints.ToArray();
        }

        public Point[] Points { get; set; }    
    }
}
