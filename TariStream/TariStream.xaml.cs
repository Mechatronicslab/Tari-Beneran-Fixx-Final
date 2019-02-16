using Microsoft.Kinect;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Accord.Statistics.Models.Markov;

using System.Threading.Tasks;
using System.Drawing.Imaging;
namespace TariStream
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        string[] poseLabels = { "Awalan K1", "Tangan di samping", "Tangan agak naik", "Rentang tangan", "Rentang agak ke atas", "Tangan di samping", "Awalan K2 dan K3", "Awalan2 K2 dan K3", "Awalan K4", "Random", "Tak Terrekognisi" };
        string MongoCString = "mongodb://maria:maria123@167.205.7.226:27017/kinect";
        // Modes
        Mode _mode = Mode.Color;
        LogMode _logMode = LogMode.CSV;
        // Kinect Objects
        KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        IList<Body> _bodies;
        List<int> gestures;
        // Bool modes
        bool _displayBody = false;
        bool _takeRecord = false;
        // Buffers
        DateTime startTime;
        //DateTime stampTime;
        string prevData;
        TimeSpan time0;
        TimeSpan rtime;
        MongoJoint Harvester = new MongoJoint();
        GestureDetect Detective;
        string Path;
        int B_COUNT = 0;
        KinectDTParser parser = new KinectDTParser();
        int[] B_state;
        int answer;
        myClassifier myHMC;
        myClassifier myGestureHMC;
        myClassifier myPhraseHMC;
        Stopwatch SW = new Stopwatch();
  
        ColorFrameReader cfr;
        byte[] colorData;
        ColorImageFormat format;
        WriteableBitmap wbmp;
        BitmapSource img;
        int imageSerial;
        bool recordStarted;

        public UserControl1()
        {
            InitializeComponent();
            gestures = new List<int>();
            recordStarted = false;
            System.IO.DirectoryInfo directory = new DirectoryInfo("./img/");
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sensor = KinectSensor.GetDefault();

            if (_sensor != null)
            {
                _sensor.Open();
                
                _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body );
                var fd = _sensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);
                uint frameSize = fd.BytesPerPixel * fd.LengthInPixels;
                colorData = new byte[frameSize];
                format = ColorImageFormat.Bgra;
                //cfr = _sensor.ColorFrameSource.OpenReader();
  
                _reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
        

                // Deleting all previous image in ./img directory
               


                
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }
        }



        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {

            var reference = e.FrameReference.AcquireFrame();

            // Color
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                
                //camera.Source = wbmp;
                if (frame != null)
                {
                    if (_mode == Mode.Color)
                    {
                        
                        double height = frame.ToBitmap().Height;
                        double width = frame.ToBitmap().Width;
                        var trf = new ScaleTransform(canvas.ActualWidth / width, canvas.ActualHeight / height);
                        img = (BitmapSource)frame.ToBitmap();
                        var imgSource = new TransformedBitmap(img, trf);
                        camera.Source = imgSource;
                        /*frame.CopyConvertedFrameDataToArray(colorData, format);
                        var fd = frame.FrameDescription;
                        var bytesPerPixel = (PixelFormats.Bgr32.BitsPerPixel) / 8;
                        var stride = bytesPerPixel * frame.FrameDescription.Width;

                        bmpSource = BitmapSource.Create(fd.Width, fd.Height, 96.0, 96.0, PixelFormats.Bgr32, null, colorData, stride);
                        wbmp = new WriteableBitmap(bmpSource);
                        camera.Source = wbmp;*/

                     
                    }
                }
            }

            // Depth
            using (var frame = reference.DepthFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (_mode == Mode.Depth)
                    {
                        camera.Source = frame.ToBitmap();
                    }
                }
            }

            // Infrared
            using (var frame = reference.InfraredFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (_mode == Mode.Infrared)
                    {
                        double height = frame.ToBitmap().Height;
                        double width = frame.ToBitmap().Width;
                        var trf = new ScaleTransform(canvas.ActualWidth / width, canvas.ActualHeight / height);
                        img = (BitmapSource)frame.ToBitmap();
                        var imgSource = new TransformedBitmap(img, trf);
                        camera.Source = imgSource;
                        camera.Source = frame.ToBitmap();
                    }
                }
            }

            // Body
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    canvas.Children.Clear();

                    _bodies = new Body[frame.BodyFrameSource.BodyCount];

                    frame.GetAndRefreshBodyData(_bodies);
                    
                    B_COUNT = 0;
                    bool allBodynull = true;
                    foreach (var body in _bodies)
                    {
                        if (body != null)
                        {
                            allBodynull = false;
                            if (body.IsTracked)
                            {

                                if (prevData == "0")
                                {
                                    time0 = reference.BodyFrameReference.RelativeTime;
                                }
                                SW.Reset();
                                SW.Start();
                                prevData = Harvester.Harvest(rtime, body);
                                B_COUNT++;
                                /*if (recordStarted)
                                {
                                    // JpegBitmapEncoder to save BitmapSource to file
                                    // imageSerial is the serial of the sequential image
                                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                                    encoder.Frames.Add(BitmapFrame.Create(img));
                                    using (var fs = new FileStream("./img/" + (imageSerial++) + ".jpeg", FileMode.Create, FileAccess.Write))
                                    {
                                        encoder.Save(fs);
                                    }
                                }*/

                                //camera.Source = wbmp;
                                // Draw skeleton.
                                if (_displayBody)
                                {
                                    canvas.DrawSkeleton(body, _sensor, _mode);
                                }
                                if (_takeRecord)
                                {
                                    rtime = reference.BodyFrameReference.RelativeTime.Subtract(time0);
                                    //stampTime = startTime.Add(rtime);
                                    if (_logMode == LogMode.Mongo)
                                    {
                                        //Harvester.stamp = Harvester.stamp + B_COUNT;
                                        Path = sFilename.Text + startTime.ToString();
                                        if (rtime != TimeSpan.Zero)
                                        {
                                            Write2Mongo(Path, Harvester);
                                        }
                                        B_state = parser.parsefromKinectRow(Harvester, "HIERARCHIC");
                                        answer = 9 * B_state[0] + 3 * B_state[1] + B_state[2];
                                        Output_Label.Content = String.Join(" ", parser.observedStates, answer);
                                                                            }
                                    else if (_logMode == LogMode.CSV)
                                    {
                                        //SW.Start();
                                        prevData = CSV_Extractor(body) + (rtime.TotalMilliseconds / 1000).ToString();
                                        Path = Directory.GetCurrentDirectory() + "\\CSV\\" + sFilename.Text + ".csv";
                                        Write2Base(Path, prevData);
                                        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                                        encoder.Frames.Add(BitmapFrame.Create(img));
                                        using (var fs = new FileStream("./img/" + (imageSerial++) + ".jpeg", FileMode.Create, FileAccess.Write))
                                        {
                                            encoder.Save(fs);
                                        }
                                        SW.Stop();
                                        Console.WriteLine(SW.ElapsedMilliseconds.ToString());
                                        
                                        //SW.Reset();
                                    }
                                }
                                if (_detectGesture.IsChecked == true)
                                {
                                    if (myHMC != null)
                                    {
                                        B_state = parser.parsefromKinectRow(Harvester, "HIERARCHIC");
                                        Ori_Label.Content = String.Join(" ", B_state);
                                        Detect_Label.Content = String.Join(" ", myHMC.Compute(B_state));// TO DO: Merge HMM Evaluation to this part
                                        int pose = myHMC.Classify(B_state);
                                        if (gestures.Count > 15)
                                        {
                                            if (myPhraseHMC != null)
                                            {
                                                Phrase_ResultL.Content = "SALAH";
                                            }
                                            gestures = new List<int>();
                                            //ShowResult(anser);
                                            //Result_Grid.Opacity = 0;
                                        }
                                        Gesture_Label.Content = poseLabels[pose];
                                        if (Detective != null)
                                        {
                                            Detective.AddObservation(pose);
                                            Layer2Detect.Content = "Holder Counter: " + Detective.HolderCounter;
                                            var report = Detective.tryRecogGesture();
                                            int gesture = -1;
                                            if (Detective.HolderCounter > 24)
                                            {
                                                gesture = Detective.Decide(report, true);
                                            }
                                            else
                                            {
                                                gesture = Detective.Decide(report, false);
                                            }
                                            if (gesture >= 0)
                                            {
                                                //Add to recognized List
                                                gestures.Add(gesture);
                                                Gestures_Label.Content = "Recognized gestures: " + String.Join(" ", gestures);
                                            }
                                            if (gestures.Count >= 2)
                                            {
                                                //If buffered more than 6 gestures and phrase is loaded
                                                if (myPhraseHMC != null)
                                                {
                                                    //Reset Result
                                                    if ((string)Phrase_ResultL.Content != "")
                                                        Phrase_ResultL.Content = "";
                                                    if (gestures.Count >= 6)
                                                    {
                                                        //Try recognize phrases
                                                        int recognum = 0;
                                                        for (int v = 0; v <= gestures.Count - 6; v++)
                                                        {
                                                            int[] Gestseq = gestures.Skip(v).Take(6).ToArray();
                                                            if (myPhraseHMC.Classify(Gestseq) == 0)
                                                            {
                                                                recognum++;
                                                            }
                                                        }
                                                        if (recognum > 2)
                                                        {
                                                            Phrase_ResultL.Content = "BENAR";
                                                            gestures = new List<int>();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Layer2Detect.Content = "Please load layer2";
                                        }
                                    }
                                    else
                                    {
                                        Detect_Label.Content = "Please Load HMM";
                                    }
                                }
                                else
                                {
                                    //if detection unchecked
                                    Detect_Label.Content = "";
                                    if (Detective != null)
                                    {
                                        Detective.Reset();
                                        Layer2Detect.Content = "Holder Counter: " + Detective.HolderCounter;
                                    }
                                    if (gestures.Count > 0)

                                        gestures = new List<int>();
                                }
                            }
                        }
                    }
                    if (allBodynull)
                    {
                        if (Detective != null)
                        {
                            if (Detective.HolderCounter > 1)
                            {
                                var report = Detective.tryRecogGesture();
                                gestures.Add(Detective.Decide(report, true));
                                Gestures_Label.Content = "Recognized gestures: " + String.Join(" ", gestures);
                            }
                            if (myPhraseHMC != null)
                            {
                                if (gestures.Count >= 6)
                                {
                                    //Try recognize phrases
                                    for (int v = 0; v <= gestures.Count - 6; v++)
                                    {
                                        int[] Gestseq = gestures.Skip(v).Take(6).ToArray();
                                        if (myPhraseHMC.Classify(Gestseq) == 0)
                                        {
                                            Phrase_ResultL.Content = "BENAR";
                                        }
                                    }
                                    if ((string)Phrase_ResultL.Content != "BENAR")
                                        Phrase_ResultL.Content = "SALAH";
                                    gestures = new List<int>();
                                }
                            }
                        }
                    }
                }
            }
            //Console.WriteLine(_logMode.ToString());
        }

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Color;
        }

        private void Depth_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Depth;
        }

        private void Infrared_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Infrared;
        }

        private void Body_Click(object sender, RoutedEventArgs e)
        {
            _displayBody = !_displayBody;
            if (_displayBody)
            {
                B_Button.Content = "Body ON";
            }
            else
            {
                B_Button.Content = "Body OFF";
            }
        }

        private void S_Click(object sender, RoutedEventArgs e)
        {
            _takeRecord = !_takeRecord;
            startTime = DateTime.Now;
            /*Mencoba Ambil Waktu Server
            MongoClient client = new MongoClient("mongodb://maria:maria123@167.205.7.226:27017/kinect");
            //var db = client.GetDatabase("Coba2DB");
            var db = client.GetDatabase("kinect");
            var cmd = new BsonDocumentCommand<BsonDocument>(new BsonDocument { { "serverStatus", 1 } });
            var rests = db.RunCommand(cmd);
            DateTime servTime = rests["localTime"].ToLocalTime();
            delayDB = servTime.Subtract(DateTime.Now);
            */
            prevData = "0";
            if (_takeRecord)
            {
                System.IO.DirectoryInfo directory = new DirectoryInfo("./img/");
                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                S_Button.Content = "Cut!!";
                imageSerial = 0;
                recordStarted = true;
                

            }
            else
            {
                S_Button.Content = "Take!!";
                recordStarted = false;
                Process.Start("ffmpeg.exe", "-framerate 10 -i ./img/%d.jpeg -c:v libx264 -r 30 -pix_fmt yuv420p kinect_video.mp4");
                

            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Target.SelectedItem == cbi1)
            {
                _logMode = LogMode.Mongo;
            }
            else
            {
                _logMode = LogMode.CSV;
            }
        }
        // Program Routines
        #region Routines
        // Extracting Skeleton Data as CSV
        public string CSV_Extractor(Body body)
        {

            string currentData = "";
            JointType[] JOI = { JointType.SpineBase, JointType.SpineMid, JointType.SpineShoulder,
                              JointType.Neck, JointType.Head, JointType.Head, JointType.HipLeft,
                              JointType.KneeLeft, JointType.AnkleLeft, JointType.FootLeft,
                              JointType.HipRight, JointType.KneeRight, JointType.AnkleRight,
                              JointType.FootRight, JointType.WristLeft, JointType.ShoulderLeft,
                              JointType.ElbowLeft, JointType.HandLeft, JointType.WristRight,
                              JointType.ShoulderRight, JointType.ElbowRight, JointType.HandRight,
                              JointType.ThumbLeft, JointType.HandTipLeft, JointType.ThumbRight, JointType.HandTipRight};
            double Px, Py, Pz, pch, yw, rll;
            Vector4 Ori;
            foreach (JointType numb in JOI)
            {
                Px = body.Joints[numb].Position.X;
                Py = body.Joints[numb].Position.Y;
                Pz = body.Joints[numb].Position.Z;
                Ori = body.JointOrientations[numb].Orientation;
                pch = Ori.Pitch();
                yw = Ori.Yaw();
                rll = Ori.Roll();
                currentData = currentData + Px + ";" + Py + ";" + Pz + ";" + pch + ";" + yw + ";" + rll + ";";
            }
            return currentData;
        }
        // Writing to CSV File
        public void Write2Base(string Path, string dataJoint)
        {
            StreamWriter sw = File.AppendText(Path);
            if (!File.Exists(Path))
            {
                File.Create(Path);
            }
            sw.Write(dataJoint);
            sw.WriteLine();
            sw.Close();
        }
        // Writing to MongoDB
        public void Write2Mongo(string cPath, MongoJoint data)
        {
            //MongoClient client = new MongoClient();
            MongoClient client = new MongoClient(MongoCString);
            //var db = client.GetDatabase("Coba2DB");
            var db = client.GetDatabase("kinect");
            var collection = db.GetCollection<MongoJoint>(cPath);
            collection.InsertOne(data);
        }
        #endregion

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            string PathLayer1 = Directory.GetCurrentDirectory() + "\\Layer1.csv";
            string PathLayer2 = Directory.GetCurrentDirectory() + "\\Layer2.csv";
            string PathLayer3 = Directory.GetCurrentDirectory() + "\\Layer3.csv";
            myHMC = LoadHMC(PathLayer1);
            myGestureHMC = LoadHMC(PathLayer2);
            myPhraseHMC = LoadHMC(PathLayer3);
            Detective = new GestureDetect(myGestureHMC);
            /*
            myHMC = new myClassifier();
            string PathLayer1 = Directory.GetCurrentDirectory() + "\\Layer1Rujung.csv";
            ThingSaver LoadedHMC = loadThingSaverCSV(PathLayer1);
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
                hmms.Add(hmm);
            }
            HiddenMarkovModel[] hmmsa = hmms.ToArray();
            myHMC.classes = LoadedHMC.classes;
            myHMC.symbols = LoadedHMC.symblos;
            myHMC.models = hmmsa;
            */
            FraseMode.Content = "Kenui";
            Phrase_ResultL.FontWeight = FontWeights.Bold;
            Phrase_ResultL.FontSize = 14;
            Phrase_ResultL.Content = "LOADED";
        }
        /*
        private void ShowResult(int Label)
        {
            if (Label == 1)
            {
                Result_Grid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF15FF5F"));
                Frase_Result.Content = "Kenui";
            }
            else
            {
                Result_Grid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF1515"));
                Frase_Result.Content = "SALAH KAMU";
            }
        }
        private void hideResult()
        {
            Result_Grid.Opacity = 0;
        }
         */
        private void Load2_Click(object sender, RoutedEventArgs e)
        {
            string PathLayer1 = Directory.GetCurrentDirectory() + "\\Layer1Sabung.csv";
            string PathLayer2 = Directory.GetCurrentDirectory() + "\\Layer2Sabung.csv";
            string PathLayer3 = Directory.GetCurrentDirectory() + "\\Layer3SabungFrase2.csv";
            myHMC = LoadHMC(PathLayer1);
            myGestureHMC = LoadHMC(PathLayer2);
            myPhraseHMC = LoadHMC(PathLayer3);
            Detective = new GestureDetect(myGestureHMC);
            /*
            myGestureHMC = new myClassifier();
            string PathLayer2 = Directory.GetCurrentDirectory() + "\\Layer2Rujung.csv";
            ThingSaver LoadedHMC = loadThingSaverCSV(PathLayer2);
            Console.WriteLine("Loaded Gesture HMM");
            List<HiddenMarkovModel> hmms = new List<HiddenMarkovModel>();
            for (int i = 0; i < LoadedHMC.constructs.Count(); i++)
            {
                var hmm = new HiddenMarkovModel(LoadedHMC.constructs[i].transitions, LoadedHMC.constructs[i].emissions, LoadedHMC.constructs[i].init);
                hmms.Add(hmm);
            }
            if (LoadedHMC.Threshold != null)
            { 
                var hmm = new HiddenMarkovModel(LoadedHMC.Threshold.transitions, LoadedHMC.Threshold.emissions, LoadedHMC.Threshold.init);
                hmms.Add(hmm);
            }
            HiddenMarkovModel[] hmmsa = hmms.ToArray();
            //hmc = new HiddenMarkovClassifier(LoadedHMC.classes, topolsa, LoadedHMC.symblos);
            myGestureHMC.classes = LoadedHMC.classes;
            myGestureHMC.symbols = LoadedHMC.symblos;
            myGestureHMC.models = hmmsa;
            Detective = new GestureDetect(myGestureHMC);
            */
            FraseMode.Content = "Sabung";
            Phrase_ResultL.FontWeight = FontWeights.Bold;
            Phrase_ResultL.FontSize = 14;
            Phrase_ResultL.Content = "LOADED";
        }
        public ThingSaver loadThingSaverCSV(string Path)
        {
            string Lines;
            string[] LineBuffer;
            ThingSaver HMCbuffer;
            using (StreamReader sr = new StreamReader(Path))
            {
                Lines = sr.ReadLine();//1st Line HMC struct(contains HMC constants : nState,nSymbol,nModel,nameF,thresholdF)
                LineBuffer = Lines.Split(';');
                HMCbuffer = new ThingSaver(Int32.Parse(LineBuffer[0]), Int32.Parse(LineBuffer[1]));
                //NEXT TO DO:IF label array is also saved
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
                    HMCbuffer.constructs.Add(HMMdummy);
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
                    HMCbuffer.Threshold = HMMdummy;
                }
                sr.Close();
            }
            return HMCbuffer;
        }
        private myClassifier LoadHMC(string loadPath)
        {
            myHMC = new myClassifier();
            ThingSaver LoadedHMC = loadThingSaverCSV(loadPath);
            Console.WriteLine("Loaded Phrase HMM");
            List<HiddenMarkovModel> hmms = new List<HiddenMarkovModel>();
            for (int i = 0; i < LoadedHMC.constructs.Count(); i++)
            {
                var hmm = new HiddenMarkovModel(LoadedHMC.constructs[i].transitions, LoadedHMC.constructs[i].emissions, LoadedHMC.constructs[i].init);
                hmms.Add(hmm);
            }
            if (LoadedHMC.Threshold != null)
            {
                var hmm = new HiddenMarkovModel(LoadedHMC.Threshold.transitions, LoadedHMC.Threshold.emissions, LoadedHMC.Threshold.init);
                hmms.Add(hmm);
            }
            HiddenMarkovModel[] hmmsa = hmms.ToArray();
            //hmc = new HiddenMarkovClassifier(LoadedHMC.classes, topolsa, LoadedHMC.symblos);
            myHMC.classes = LoadedHMC.classes;
            myHMC.symbols = LoadedHMC.symblos;
            myHMC.models = hmmsa;
            return myHMC;
        }

        private void Load3_Click(object sender, RoutedEventArgs e)
        {
            string PathLayer1 = Directory.GetCurrentDirectory() + "\\Layer1Rujung.csv";
            string PathLayer2 = Directory.GetCurrentDirectory() + "\\Layer2Rujung.csv";
            string PathLayer3 = Directory.GetCurrentDirectory() + "\\Layer3RujungFrase.csv";
            myHMC = LoadHMC(PathLayer1);
            myGestureHMC = LoadHMC(PathLayer2);
            myPhraseHMC = LoadHMC(PathLayer3);
            Detective = new GestureDetect(myGestureHMC);
            /*
            ThingSaver LoadedHMC = loadThingSaverCSV(PathLayer3);
            Console.WriteLine("Loaded Phrase HMM");
            List<HiddenMarkovModel> hmms = new List<HiddenMarkovModel>();
            for (int i = 0; i < LoadedHMC.constructs.Count(); i++)
            {
                var hmm = new HiddenMarkovModel(LoadedHMC.constructs[i].transitions, LoadedHMC.constructs[i].emissions, LoadedHMC.constructs[i].init);
                hmms.Add(hmm);
            }
            if (LoadedHMC.Threshold != null)
            {
                var hmm = new HiddenMarkovModel(LoadedHMC.Threshold.transitions, LoadedHMC.Threshold.emissions, LoadedHMC.Threshold.init);
                hmms.Add(hmm);
            }
            HiddenMarkovModel[] hmmsa = hmms.ToArray();
            //hmc = new HiddenMarkovClassifier(LoadedHMC.classes, topolsa, LoadedHMC.symblos);
            myPhraseHMC.classes = LoadedHMC.classes;
            myPhraseHMC.symbols = LoadedHMC.symblos;
            myPhraseHMC.models = hmmsa;
            //Set the phrase_detect layer
            */
            FraseMode.Content = "Rujung";
            Phrase_ResultL.FontWeight = FontWeights.Bold;
            Phrase_ResultL.FontSize = 14;
            Phrase_ResultL.Content = "LOADED";
        }
    }

    public enum Mode
    {
        Color,
        Depth,
        Infrared
    }
    public enum LogMode
    {
        Mongo,
        CSV
    }
}
