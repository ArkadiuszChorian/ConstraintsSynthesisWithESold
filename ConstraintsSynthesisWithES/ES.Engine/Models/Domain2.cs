using System;
using System.Collections.Generic;

namespace ES.Engine.Models
{
    public class Domain2
    {
        public Domain2(ExperimentParameters experimentParameters)
        {
            NumberOfDimensions = experimentParameters.NumberOfDimensions;
            Limits = new List<Tuple<double, double>>(experimentParameters.NumberOfDimensions);

            for (var i = 0; i < experimentParameters.NumberOfDimensions; i++)
            {
                //Limits.Add(experimentParameters.DefaultDomainLimit);
            }
        }

        public Domain2(ExperimentParameters experimentParameters, List<Tuple<double, double>> domainLimits)
        {
            NumberOfDimensions = experimentParameters.NumberOfDimensions;
            Limits = domainLimits;
        }

        public int NumberOfDimensions { get; set; }
        public List<Tuple<double, double>> Limits { get; set; }
    }
}
