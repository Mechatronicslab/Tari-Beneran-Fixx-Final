using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Models.Markov;

namespace f_userCamera
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
                probs[i] = Math.Exp(models[i].Evaluate(sequence));
            }
            return probs;
        }
        public int[] Sort(int[] input)
        {
            int[] indexes = new int[input.Count()];
            for (int i = 0; i < input.Count(); i++)
            {
                indexes[i] = i;
            }
            Array.Sort(input, indexes);
            return indexes;
        }
    }

    public class ThingSaver
    {
        public int classes { get; set; }
        public List<HMMConstruct> constructs { get; set; }
        public int symblos { get; set; }
        public string[] names { get; set; }
        public HMMConstruct Threshold { get; set; }
        public ThingSaver(int noClass, int noSymbol)
        {
            classes = noClass;
            symblos = noSymbol;
            constructs = new List<HMMConstruct>();
        }
    }

    public class HMMConstruct
    {
        public int n_state { get; set; }
        public int n_symbols { get; set; }
        public double[] init { get; set; }
        public double[,] transitions { get; set; }
        public double[,] emissions { get; set; }
    }

}
