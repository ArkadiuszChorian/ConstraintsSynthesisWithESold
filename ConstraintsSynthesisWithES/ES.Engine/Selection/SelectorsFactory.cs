using System;
using ES.Engine.Models;

namespace ES.Engine.Selection
{
    public static class SelectorsFactory
    {
        public static IParentsSelector GetParentsSelector(ExperimentParameters experimentParameters)
        {
            return new ParentsRandomSelector(experimentParameters);
            //switch (experimentParameters.TypeOfSurvivorsSelection)
            //{
            //    case ExperimentParameters.SelectionType.Distinct:
            //        return new ParentsRandomSelector(experimentParameters); ;
            //    case ExperimentParameters.SelectionType.Union:
            //        return new ParentsRandomUnionSelector(experimentParameters);
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //}            
        }

        public static ISurvivorsSelector GetSurvivorsSelector(ExperimentParameters experimentParameters)
        {
            switch (experimentParameters.TypeOfSurvivorsSelection)
            {
                case ExperimentParameters.SelectionType.Distinct:
                    return new SurvivorsDistinctSelector(experimentParameters);
                case ExperimentParameters.SelectionType.Union:
                    return new SurvivorsUnionSelector(experimentParameters);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
