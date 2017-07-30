﻿namespace ES.Engine.Models
{
    public class Statistics
    {
        public int TruePositives { get; set; }
        public int FalsePositives { get; set; }
        public int TrueNegatives { get; set; }
        public int FalseNegatives { get; set; }

        public double Recall => (double)TruePositives / (TruePositives + FalseNegatives);
        public double Specificity => (double)TrueNegatives / (TrueNegatives + FalsePositives);
        public double Precision => (double)TruePositives / (TruePositives + FalsePositives);
        public double NegativePredictiveValue => (double)TrueNegatives / (TrueNegatives + FalseNegatives);
        public double MissRate => 1 - Recall;
        public double FallOut => 1 - Specificity;
        public double FalseDiscoveryRate => 1 - Precision;
        public double FalseOmissionRate => 1 - NegativePredictiveValue;
        public double Accuracy => (double)(TruePositives + TrueNegatives) / (TruePositives + TrueNegatives + FalsePositives + FalseNegatives);
        public double F1Score => 2 * Precision * Recall / (Precision + Recall);
    }
}
