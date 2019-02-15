
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov.Topology;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfControlLibrary1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        int valueS;
        string MongoCString = "mongodb://maria:maria123@167.205.7.226:27017/kinect";
        IMongoDatabase db;
        IMongoCollection<BrekelMongo> collection;
        DataHolder dH = new DataHolder();
        string selectedDB;
        bool _play = false;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        string _dbmode = "CSV";
        string _dataType = "Brekel";
        GestureDataHolder dHG;
        public HiddenMarkovClassifier GestureHMC;
        public myClassifier myPoseHMC;
        string[] labels;
        Stopwatch SW = new Stopwatch();
        string PathHMC = Directory.GetCurrentDirectory() + "\\Layer1Rujung.csv";
        string PathCSV = Directory.GetCurrentDirectory() + "\\Yametee.csv";
        string PathSAVE = Directory.GetCurrentDirectory() + "\\Layer2.csv";
        //string selectedDB;
        int recog_buff;
        public UserControl1()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(40);
            MongoClient client = new MongoClient(MongoCString);
            db = client.GetDatabase("kinect");
            var listclct = db.ListCollections().ToList();
            List<string> clctnames = new List<string>();
            foreach (var doc in listclct)
            {
                clctnames.Add(doc["name"].ToString());
            }
            CBx.ItemsSource = clctnames;
        }



        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            canvas.Children.Clear();
            valueS = (int)Math.Round(Suraido.Value, 0);
            SlideLab.Content = Suraido.Value.ToString();
            try{
                    dH.LoadRow(collection, valueS);
                    canvas.DrawSkeleton(dH.skeleton[0]);
                }catch{
                    MessageBox.Show("Please load data first");
                }
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_play)
            {
                _play = !_play;
                PlayB.Content = "PLAY";
                dispatcherTimer.Stop();

            }
            else
            {
                _play = !_play;
                PlayB.Content = "STOP";
                dispatcherTimer.Start();

            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Dispatcher.Invoke((Action)delegate
            {
                next();
            }, null);
        }

        private void next() {
            Suraido.Value++;
        }

        private void CBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDB = CBx.SelectedItem.ToString();
        }

        private void LOAD_Click(object sender, RoutedEventArgs e)
        {
            var filter = FilterDefinition<BrekelMongo>.Empty;
            collection = db.GetCollection<BrekelMongo>(selectedDB);
            Suraido.Maximum = collection.Find(filter).Count() - 1;
            MessageBox.Show(Suraido.Maximum.ToString());
            dH.LoadRow(collection, 0);
            canvas.Children.Clear();
            //SideCanvas.Children.Clear();
            canvas.DrawSkeleton(dH.skeleton[0]);
            //SideCanvas.DrawSkeletonSide(dH.skeleton[0]);
        }

        private void CBI1_Selected(object sender, RoutedEventArgs e)
        {
            MongoClient client = new MongoClient(MongoCString);
            db = client.GetDatabase("kinect");
            var listclct = db.ListCollections().ToList();
            List<string> clctnames = new List<string>();
            foreach (var doc in listclct)
            {
                clctnames.Add(doc["name"].ToString());
            }
            CB2.ItemsSource = clctnames;
            _dbmode = "Mongo";
        }
        private void CBI2_Selected(object sender, RoutedEventArgs e)
        {
            _dbmode = "CSV";
        }

        private void CB2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDB = CB2.SelectedItem.ToString();
            //Console.WriteLine(slctclct.GetType());
            LoadRow.Opacity = 100;
        }

        private void Button_Clicks(object sender, RoutedEventArgs e)
        {
            if (myPoseHMC != null)
            {
                SW.Reset();
                SW.Start();
                if (_dbmode == "Mongo")
                {
                    if (_dataType == "Ours")
                    {
                        var collection = db.GetCollection<MongoJoint>(selectedDB);
                        dHG.Load(TBLabel.Text, collection, Int32.Parse(TBStart.Text), Int32.Parse(TBEnd.Text));
                    }
                    else
                    {
                        var collection = db.GetCollection<BrekelMongo>(selectedDB);
                        dHG.Load(TBLabel.Text, collection, Int32.Parse(TBStart.Text), Int32.Parse(TBEnd.Text));
                    }

                }
                else if (_dbmode == "CSV")
                {
                    dHG.LoadCSV(TBLabel.Text, PathCSV, Int32.Parse(TBStart.Text), Int32.Parse(TBEnd.Text));
                }
                SW.Stop();
                Console.WriteLine("Loading done in {0} milliseconds", SW.ElapsedMilliseconds);
            }
            else
            {
                MessageBox.Show("Please load layer 1");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dHG.layer1 != null)
            {
                int nh_state = 2;
                nh_state = Int32.Parse(TBHState.Text);
                //_modeClassify = 0;
                //hmc = new HiddenMarkovClassifier(dH.Labels.Count, new int[] { 3, 3 }, 3, dH.Labels.ToArray());
                GestureHMC = new HiddenMarkovClassifier(dHG.Labels.Count, new Ergodic(nh_state, true), myPoseHMC.models.Count(), dHG.Labels.ToArray());
                Console.WriteLine("Classifier Created...");
                BindingList<Samples> seqs = dHG.Sequences;
                BindingList<String> classes = dHG.Labels;

                int[][] inputs = new int[seqs.Count][];
                int[] outputs = new int[seqs.Count];

                for (int i = 0; i < inputs.Length; i++)
                {
                    inputs[i] = seqs[i].Sequence;
                    outputs[i] = seqs[i].labelIndex;
                }

                int iterations = 50;
                double tolerance = 0.0001;

                var teacher = new HiddenMarkovClassifierLearning(GestureHMC,
                    i => new BaumWelchLearning(GestureHMC.Models[i])
                    {
                        Tolerance = tolerance,
                        Iterations = iterations
                    });
                teacher.Empirical = true;
                teacher.Rejection = true;
                Console.WriteLine("Commencing Training...");
                double error = teacher.Run(inputs, outputs);
                Console.WriteLine("Training done with error of : {0}", error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int totalError = 0;
            foreach (var sample in dHG.Sequences)
            {
                /*
                if (_modeClassify == 0)
                {
                    recog_buff = hmc.Compute(sample.Sequence);
                }
                else
                {
                    recog_buff = myHMC.Classify(sample.Sequence);
                }
                */
                recog_buff = GestureHMC.Compute(sample.Sequence);
                Console.WriteLine("Sequence {0} originally {1} recognized as {2}", String.Join(" ", sample.Sequence), sample.labelIndex, recog_buff);
                if (recog_buff != sample.labelIndex)
                {
                    totalError++;
                }
            }
            Console.WriteLine("Performance of HMM: Error: " + totalError + " out of " + dHG.Sequences.Count + " sequences");
        }

        private void LoadHMM_Click(object sender, RoutedEventArgs e)
        {
            myPoseHMC = new myClassifier();
            ThingSaver LoadedHMC = loadThingSaverCSV(PathHMC);
            Console.WriteLine("Loaded Model No.1 has Initial Probability of {0}", String.Join(" ", LoadedHMC.constructs[0].init));
            List<HiddenMarkovModel> hmms = new List<HiddenMarkovModel>();
            for (int i = 0; i < LoadedHMC.constructs.Count(); i++)
            {
                var hmm = new HiddenMarkovModel(LoadedHMC.constructs[i].transitions, LoadedHMC.constructs[i].emissions, LoadedHMC.constructs[i].init);
                hmms.Add(hmm);
            }
            if (LoadedHMC.Threshold != null)
            {
                var hmm = new HiddenMarkovModel(LoadedHMC.Threshold.transitions, LoadedHMC.Threshold.emissions, LoadedHMC.Threshold.init);
                Console.WriteLine("Loaded Threshold Model has Initial Probability of {0}", String.Join(" ", LoadedHMC.Threshold.init));
                hmms.Add(hmm);
            }
            HiddenMarkovModel[] hmmsa = hmms.ToArray();
            myPoseHMC.classes = LoadedHMC.classes;
            myPoseHMC.symbols = LoadedHMC.symblos;
            myPoseHMC.models = hmmsa;
            dHG = new GestureDataHolder(myPoseHMC);
            Console.WriteLine("Loaded {0} models", myPoseHMC.models.Count());
        }

        private void SaveHMM_Click(object sender, RoutedEventArgs e)
        {
            /*
            Console.WriteLine("Created a HMC with {0} classes so there is {1} models", hmc.Classes, hmc.Models.Count());
            Console.WriteLine("Probabilitas Priori : [{0}]", String.Join(", ", hmc.Priors));
            Console.WriteLine("Other numbers: Sensitivity: {0} ; Symbols: {1}", hmc.Sensitivity, hmc.Symbols);
            for (int i = 0; i < hmc.Models.Count(); i++)
            {
                Console.WriteLine("MODEL No.{0}:", i + 1);
                Console.WriteLine(String.Join(" ", hmc.Models[i].Probabilities));
                Console.WriteLine("Transitions:");
                
                for (int j = 0; j <= hmc.Models[i].Transitions.GetUpperBound(0); j++)
                {
                    //Console.WriteLine(String.Join(" ", hmc.Models[i].Transitions.GetRow(j)));
                    for (int k = 0; k <= hmc.Models[i].Transitions.GetUpperBound(1); k++)
                    {
                        Console.Write("{0} ",Math.Exp(hmc.Models[i].Transitions[j,k]));
                    }
                    Console.WriteLine();
                }
                
            }
            */
            ThingSaver SavedHMC = GetModelsConstructor(GestureHMC);
            Console.WriteLine("Model No.1 has Initial Probability of {0}", String.Join(" ", SavedHMC.constructs[0].init));
            Console.WriteLine("Model No.2 has Initial Probability of {0}", String.Join(" ", SavedHMC.constructs[1].init));
            SaveConstructor_toCSV(SavedHMC, PathSAVE);
        }
        public ThingSaver GetModelsConstructor(HiddenMarkovClassifier hmc)
        {
            //Get HMC Constructor
            ThingSaver hmcConstruct = new ThingSaver(hmc.Classes, hmc.Symbols);
            if (labels != null)
            {
                hmcConstruct.names = labels;
            }
            //Get Constructor for each model
            for (int i = 0; i < hmc.Models.Count(); i++)
            {
                Console.WriteLine("Transitions:");
                HMMConstruct HMMdummy = new HMMConstruct();
                // Get Init Probabilities
                double[] probsDummy = new double[hmc.Models[i].Probabilities.Count()];
                for (int j = 0; j < hmc.Models[i].Probabilities.Count(); j++)
                {
                    probsDummy[j] = Math.Exp(hmc.Models[i].Probabilities[j]);
                }
                HMMdummy.init = probsDummy;
                HMMdummy.n_state = hmc.Models[i].Transitions.GetUpperBound(0) + 1;
                double[,] transDummy = new double[HMMdummy.n_state, HMMdummy.n_state];
                for (int j = 0; j < HMMdummy.n_state; j++)
                {
                    //Console.WriteLine(String.Join(" ", hmc.Models[i].Transitions.GetRow(j)));
                    for (int k = 0; k < HMMdummy.n_state; k++)
                    {
                        //Console.Write("{0} ", Math.Exp(hmc.Models[i].Transitions[j, k]));
                        transDummy[j, k] = Math.Exp(hmc.Models[i].Transitions[j, k]);
                    }
                }
                HMMdummy.transitions = transDummy;
                HMMdummy.n_symbols = hmc.Models[i].Emissions.GetUpperBound(1) + 1;
                double[,] emitDummy = new double[HMMdummy.n_state, HMMdummy.n_symbols];
                for (int j = 0; j <= hmc.Models[i].Emissions.GetUpperBound(0); j++)
                {
                    //Console.WriteLine(String.Join(" ", hmc.Models[i].Transitions.GetRow(j)));
                    for (int k = 0; k <= hmc.Models[i].Emissions.GetUpperBound(1); k++)
                    {
                        //Console.Write("{0} ", Math.Exp(hmc.Models[i].Transitions[j, k]));
                        emitDummy[j, k] = Math.Exp(hmc.Models[i].Emissions[j, k]);
                    }
                }
                HMMdummy.emissions = emitDummy;
                hmcConstruct.constructs.Add(HMMdummy);
            }
            hmcConstruct.thresholdFlag = false;
            if (hmc.Threshold != null)
            {
                hmcConstruct.thresholdFlag = true;
                double[] TprobsDummy = new double[hmc.Threshold.Probabilities.Count()];
                for (int j = 0; j < hmc.Threshold.Probabilities.Count(); j++)
                {
                    TprobsDummy[j] = Math.Exp(hmc.Threshold.Probabilities[j]);
                }
                hmcConstruct.Threshold = new HMMConstruct();
                hmcConstruct.Threshold.init = TprobsDummy;
                hmcConstruct.Threshold.n_state = hmc.Threshold.Transitions.GetUpperBound(0) + 1;
                double[,] TtransDummy = new double[hmcConstruct.Threshold.n_state, hmcConstruct.Threshold.n_state];
                for (int j = 0; j < hmcConstruct.Threshold.n_state; j++)
                {
                    //Console.WriteLine(String.Join(" ", hmc.Models[i].Transitions.GetRow(j)));
                    for (int k = 0; k < hmcConstruct.Threshold.n_state; k++)
                    {
                        //Console.Write("{0} ", Math.Exp(hmc.Models[i].Transitions[j, k]));
                        TtransDummy[j, k] = Math.Exp(hmc.Threshold.Transitions[j, k]);
                    }
                }
                hmcConstruct.Threshold.transitions = TtransDummy;
                hmcConstruct.Threshold.n_symbols = hmc.Threshold.Emissions.GetUpperBound(1) + 1;
                double[,] TemitDummy = new double[hmcConstruct.Threshold.n_state, hmcConstruct.Threshold.n_symbols];
                for (int j = 0; j < hmcConstruct.Threshold.n_state; j++)
                {
                    //Console.WriteLine(String.Join(" ", hmc.Models[i].Transitions.GetRow(j)));
                    for (int k = 0; k < hmcConstruct.Threshold.n_symbols; k++)
                    {
                        //Console.Write("{0} ", Math.Exp(hmc.Models[i].Transitions[j, k]));
                        TemitDummy[j, k] = Math.Exp(hmc.Threshold.Emissions[j, k]);
                    }
                }
                hmcConstruct.Threshold.emissions = TemitDummy;
            }
            //*/
            return hmcConstruct;
        }
        public void SaveConstructor_toCSV(ThingSaver Saver, string Path)
        {
            string lines;
            int flag = (Saver.names != null) ? 1 : 0;
            int tFlag = (Saver.thresholdFlag) ? 1 : 0;
            //First Line for HMC 
            lines = Saver.classes + ";" + Saver.symblos + ";" + Saver.constructs.Count() + ";" + flag + ";" + tFlag;
            writelinetoCSV(Path, lines);
            //Next Line for names (if exist)
            if (Saver.names != null)
            {
                lines = String.Join(";", Saver.names);
                writelinetoCSV(Path, lines);
            }
            //Next do for each HMMs
            for (int i = 0; i < Saver.constructs.Count(); i++)
            {
                //Line 0: HMM Struct
                lines = Saver.constructs[i].n_state + ";" + Saver.constructs[i].n_symbols;
                writelinetoCSV(Path, lines);
                //Line 1: initial probability
                lines = String.Join(";", Saver.constructs[i].init);
                writelinetoCSV(Path, lines);
                //Lines 2: transistion probability
                for (int j = 0; j < Saver.constructs[i].n_state; j++)
                {
                    lines = "" + Saver.constructs[i].transitions[j, 0];//refresh lines
                    for (int k = 1; k < Saver.constructs[i].n_state; k++)
                    {
                        //Console.Write("{0} ", Math.Exp(hmc.Models[i].Transitions[j, k]));
                        lines = lines + ";" + Saver.constructs[i].transitions[j, k];
                    }
                    writelinetoCSV(Path, lines);
                }
                //Lines 3: emission probability
                for (int j = 0; j < Saver.constructs[i].n_state; j++)
                {
                    lines = "" + Saver.constructs[i].emissions[j, 0];//refresh lines
                    for (int k = 1; k < Saver.constructs[i].n_symbols; k++)
                    {
                        //Console.Write("{0} ", Math.Exp(hmc.Models[i].Transitions[j, k]));
                        lines = lines + ";" + Saver.constructs[i].emissions[j, k];
                    }
                    writelinetoCSV(Path, lines);
                }
            }
            //hmc.Save(Directory.GetCurrentDirectory() + "\\Dummy.dat");
            if (Saver.Threshold != null)
            {
                lines = Saver.Threshold.n_state + ";" + Saver.Threshold.n_symbols;
                writelinetoCSV(Path, lines);
                //Line 1: initial probability
                lines = String.Join(";", Saver.Threshold.init);
                writelinetoCSV(Path, lines);
                //Lines 2: transistion probability
                for (int j = 0; j < Saver.Threshold.n_state; j++)
                {
                    lines = "" + Saver.Threshold.transitions[j, 0];//refresh lines
                    for (int k = 1; k < Saver.Threshold.n_state; k++)
                    {
                        //Console.Write("{0} ", Math.Exp(hmc.Models[i].Transitions[j, k]));
                        lines = lines + ";" + Saver.Threshold.transitions[j, k];
                    }
                    writelinetoCSV(Path, lines);
                }
                //Lines 3: emission probability
                for (int j = 0; j < Saver.Threshold.n_state; j++)
                {
                    lines = "" + Saver.Threshold.emissions[j, 0];//refresh lines
                    for (int k = 1; k < Saver.Threshold.n_symbols; k++)
                    {
                        //Console.Write("{0} ", Math.Exp(hmc.Models[i].Transitions[j, k]));
                        lines = lines + ";" + Saver.Threshold.emissions[j, k];
                    }
                    writelinetoCSV(Path, lines);
                }
            }
            //*/
        }
        public void writelinetoCSV(string Path, string Line)
        {
            StreamWriter sw = File.AppendText(Path);
            if (!File.Exists(Path))
            {
                File.Create(Path);
            }
            sw.WriteLine(Line);
            sw.Close();
        }
        public ThingSaver loadThingSaverCSV(string Path)
        {
            string Lines;
            string[] LineBuffer;
            ThingSaver ThingDummy;
            using (StreamReader sr = new StreamReader(Path))
            {
                Lines = sr.ReadLine();//1st Line HMC struct
                LineBuffer = Lines.Split(';');
                ThingDummy = new ThingSaver(Int32.Parse(LineBuffer[0]), Int32.Parse(LineBuffer[1]));
                //NEXT TO DO:IF label array is saved
                //NOW: bypassed to model structures
                int models = Int32.Parse(LineBuffer[2]);
                int nFlag = Int32.Parse(LineBuffer[3]);
                int tFlag = Int32.Parse(LineBuffer[4]);
                for (int i = 0; i < models; i++)
                {
                    Lines = sr.ReadLine();
                    LineBuffer = Lines.Split(';');
                    HMMConstruct HMMdummy = new HMMConstruct();
                    HMMdummy.n_state = Int32.Parse(LineBuffer[0]);
                    HMMdummy.n_symbols = Int32.Parse(LineBuffer[1]);
                    // Get Init Probabilities
                    double[] probsDummy = new double[HMMdummy.n_state];
                    LineBuffer = sr.ReadLine().Split(';');
                    for (int j = 0; j < HMMdummy.n_state; j++)
                    {
                        probsDummy[j] = Double.Parse(LineBuffer[j]);
                    }
                    HMMdummy.init = probsDummy;
                    double[,] transDummy = new double[HMMdummy.n_state, HMMdummy.n_state];
                    for (int j = 0; j < HMMdummy.n_state; j++)
                    {
                        LineBuffer = sr.ReadLine().Split(';');
                        for (int k = 0; k < HMMdummy.n_state; k++)
                        {
                            transDummy[j, k] = Double.Parse(LineBuffer[k]);
                        }
                    }
                    HMMdummy.transitions = transDummy;
                    double[,] emitDummy = new double[HMMdummy.n_state, HMMdummy.n_symbols];
                    for (int j = 0; j < HMMdummy.n_state; j++)
                    {
                        LineBuffer = sr.ReadLine().Split(';');
                        for (int k = 0; k < HMMdummy.n_symbols; k++)
                        {
                            emitDummy[j, k] = Double.Parse(LineBuffer[k]);
                        }
                    }
                    HMMdummy.emissions = emitDummy;
                    ThingDummy.constructs.Add(HMMdummy);
                }
                if (tFlag == 1)
                {
                    Lines = sr.ReadLine();
                    LineBuffer = Lines.Split(';');
                    HMMConstruct HMMdummy = new HMMConstruct();
                    HMMdummy.n_state = Int32.Parse(LineBuffer[0]);
                    HMMdummy.n_symbols = Int32.Parse(LineBuffer[1]);
                    // Get Init Probabilities
                    double[] probsDummy = new double[HMMdummy.n_state];
                    LineBuffer = sr.ReadLine().Split(';');
                    for (int j = 0; j < HMMdummy.n_state; j++)
                    {
                        probsDummy[j] = Double.Parse(LineBuffer[j]);
                    }
                    HMMdummy.init = probsDummy;
                    double[,] transDummy = new double[HMMdummy.n_state, HMMdummy.n_state];
                    for (int j = 0; j < HMMdummy.n_state; j++)
                    {
                        LineBuffer = sr.ReadLine().Split(';');
                        for (int k = 0; k < HMMdummy.n_state; k++)
                        {
                            transDummy[j, k] = Double.Parse(LineBuffer[k]);
                        }
                    }
                    HMMdummy.transitions = transDummy;
                    double[,] emitDummy = new double[HMMdummy.n_state, HMMdummy.n_symbols];
                    for (int j = 0; j < HMMdummy.n_state; j++)
                    {
                        LineBuffer = sr.ReadLine().Split(';');
                        for (int k = 0; k < HMMdummy.n_symbols; k++)
                        {
                            emitDummy[j, k] = Double.Parse(LineBuffer[k]);
                        }
                    }
                    HMMdummy.emissions = emitDummy;
                    ThingDummy.Threshold = HMMdummy;
                }
                sr.Close();
            }
            return ThingDummy;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            dHG.Reset();
        }

        private void Radio_Brekel_Checked(object sender, RoutedEventArgs e)
        {
            _dataType = "Brekel";
            if (Radio_Ours.IsChecked == true)
            {
                Radio_Ours.IsChecked = false;
            }
        }
        private void Radio_Ours_Checked(object sender, RoutedEventArgs e)
        {
            _dataType = "Ours";
            if (Radio_Brekel.IsChecked == true)
            {
                Radio_Brekel.IsChecked = false;
            }
        }
        private int[] getSubArray(int[] input, int startIndex, int lastIndex)
        {
            List<int> list = new List<int>();
            for (int i = startIndex; i <= lastIndex; i++)
            {
                list.Add(input[i]);
            }
            int[] output = list.ToArray();
            return output;
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            int index = 0;
            try
            {
                index = Int32.Parse(indexSample.Text);
            }
            catch
            {
                MessageBox.Show("Please insert only integer sample");
                return;
            }
            if (dHG != null)
            {
                if (index < dHG.Sequences.Count())
                {
                    Samples analyzeSample = dHG.Sequences[index];
                    int lengthASample = dHG.Sequences[index].Sequence.Count();
                    Console.WriteLine("Sequence {0} originally {1}", String.Join(" ", analyzeSample.Sequence), analyzeSample.labelIndex);
                    for (int i = 0; i < lengthASample; i++)
                    {
                        for (int j = i + 1; j < lengthASample; j++)
                        {
                            int[] subSequence = getSubArray(dHG.Sequences[index].Sequence, i, j);
                            try
                            {
                                double logLikely = GestureHMC.LogLikelihood(subSequence);
                                recog_buff = GestureHMC.Compute(subSequence);
                                Console.WriteLine("SubSequence {0} recognized as {1} with confidence {2}", String.Join(" ", subSequence), recog_buff, logLikely);
                            }
                            catch
                            {
                                MessageBox.Show("Please Create Gesture HMM first");
                            }
                        }
                    }
                }
                else
                    return;
            }
            else
                return;

        }
    }
}
