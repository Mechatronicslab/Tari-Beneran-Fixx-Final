using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Models.Markov;
using Accord.MachineLearning;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Fields;
using Accord.Statistics.Models.Fields.Functions;
using Accord.Statistics.Models.Fields.Learning;
using Accord.Statistics.Models.Markov.Topology;
using Accord.Statistics.Testing;
using Accord.Statistics.Models.Fields.Features;
using Accord.Statistics.Filters;
using CsvHelper;

namespace f_userCamera
{
    public class GestureDetect
    {
        myClassifier gestureHMC;
        List<int>[] observations { get; set; }
        int timeSpan = 5;
        public int HolderCounter { get; set; }
        int LimitRecog = 25;

        public GestureDetect(myClassifier HMC)
        {

            gestureHMC = HMC;
            observations = new List<int>[2 * timeSpan];
            for (int i = 0; i < 2 * timeSpan; i++)
            {
                observations[i] = new List<int>();
            }
            HolderCounter = 1;
        }

        public void Reset()
        {
            for (int i = 0; i < 2 * timeSpan; i++)
            {
                observations[i] = new List<int>();
            }
            HolderCounter = 1;
        }

        public void AddObservation(int Label)
        {
            int index = (HolderCounter - 1) % timeSpan;
            int Add = (HolderCounter > 5) ? 2 : 1;
            for (int i = 0; i < Add; i++)
            {
                observations[i * timeSpan + index].Add(Label);
            }
            HolderCounter++;
        }

        public Report[] tryRecogGesture()
        {
            Report[] result = new Report[2 * timeSpan];
            double[] confs;
            int recog;
            double prob;
            //Compute 
            for (int i = 0; i < observations.Count(); i++)
            {
                //Compute all possible gesture and its confidence rate
                if (observations[i].Count > 2)
                {
                    recog = gestureHMC.Classify(observations[i].ToArray());
                    confs = gestureHMC.Compute(observations[i].ToArray());
                    Array.Sort(confs);
                    prob = confs[confs.Length - 1];
                    result[i] = new Report(recog, prob);
                }
            }
            //get the result for overall recognition
            return result;
        }

        public List<int[]> getObservations()
        {
            List<int[]> output = new List<int[]>();
            foreach (List<int> observe in observations)
            {
                output.Add(observe.ToArray());
            }
            return output;
        }

        public class Report
        {
            public int recog { get; set; }
            public double prob { get; set; }
            public Report(int r, double p)
            {
                recog = r;
                prob = p;
            }
        }

        public int Decide(Report[] input, bool emergency)
        {
            int[] recogCount = new int[gestureHMC.classes + 1];
            for (int j = 0; j < input.Count(); j++)
            {
                if (input[j] != null)
                {
                    recogCount[input[j].recog]++;
                }
            }
            int maxIndex = 0;
            int maxim = recogCount[0];
            for (int j = 0; j < recogCount.Count() - 1; j++)
            {
                if (maxim < recogCount[j])
                {
                    maxIndex = j;
                    maxim = recogCount[j];
                }
            }
            if (maxim > 4)
            {
                Reset();
                return maxIndex;
            }
            if (emergency)
            {
                Reset();
                if (maxim != 0)
                {
                    return gestureHMC.classes;
                }
                else
                {
                    return maxIndex;
                }
            }
            return -1;
        }
    }
}
