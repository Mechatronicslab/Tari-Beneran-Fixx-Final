using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlLibrary1
{
    public class ThingSaver
    {
        public int classes { get; set; }
        public List<HMMConstruct> constructs { get; set; }
        public int symblos { get; set; }
        public string[] names { get; set; }
        public HMMConstruct Threshold { get; set; }
        public bool thresholdFlag { get; set; }
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
