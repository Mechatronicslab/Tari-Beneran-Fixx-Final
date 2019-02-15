using Accord.Statistics.Models.Markov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlLibrary1
{
    public class myClassifier
    {
        public int classes { get; set; }
        public int symbols { get; set; }
        public string[] labels { get; set; }
        public HiddenMarkovModel[] models { get; set; }
        public HiddenMarkovModel threshold { get; set; }

        public int Classify(int[] sequence)
        {
            int index = 0;
            double[] probs = this.Compute(sequence);
            double max = probs[0];
            for (int i = 1; i < probs.Count(); i++)
            {
                if (max < probs[i])
                {
                    max = probs[i];
                    index = i;
                }
            }
            if (threshold != null)
            {
                if (max < threshold.Evaluate(sequence))
                {
                    max = threshold.Evaluate(sequence);
                    return -1;
                }
            }
            return index;
        }
        public double[] Compute(int[] sequence)
        {
            double[] probs = new double[models.Count()];
            for (int i = 0; i < models.Count(); i++)
            {
                probs[i] = models[i].Evaluate(sequence);
            }
            return probs;
        }

    }
}
