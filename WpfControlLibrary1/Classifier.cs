using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Accord.Statistics.Models.Markov;
namespace WpfControlLibrary1
{
    class Classifier
    {
        public BindingList<HiddenMarkovModel> hmms { get; private set; }
        public BindingList<string> Labels { get; private set; }
        public Classifier(BindingList<string> classes)
        {
            Labels = classes;
        }
        public void Train()
        {
        }
    }
}
