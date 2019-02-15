using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlLibrary1
{
    class Samples
    {
        public int[] Sequence;
        public int labelIndex;
        public Samples(int[] Input)
        {
            Sequence = Input;
            labelIndex = -1;
        }
        public Samples(int[] Input, int Output)
        {
            labelIndex = Output;
            Sequence = Input;
        }
    }
}
