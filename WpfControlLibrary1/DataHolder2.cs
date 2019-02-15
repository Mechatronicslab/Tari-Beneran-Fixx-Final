using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlLibrary1
{
   class GestureDataHolder
    {
        public BindingList<string> Labels { get; private set; }
        public BindingList<Samples> Sequences { get; private set; }
        public myClassifier layer1 { get; set; }
        public GestureDataHolder(myClassifier myPoseHMC)
        {
            Labels = new BindingList<string>();
            Sequences = new BindingList<Samples>();
            layer1 = myPoseHMC;
        }
        public void Load(string label, IMongoCollection<MongoJoint> source,int start,int end)
        {
            //Cek Label jika tidak ada masukkan ke Labels
            int i = Labels.IndexOf(label);
            if (i < 0)
            {
                Labels.Add(label);
                i = Labels.IndexOf(label);
            }
            //Do Loading
            var filter = FilterDefinition<MongoJoint>.Empty;// TO DO: Import class of Kinect Data and change all BsonDocument instances
            var cursor = source.Find(filter).Skip(start - 1).Limit(end - start + 1);
            List<MongoJoint> batch = cursor.ToList<MongoJoint>();
            KinectDTParser translator = new KinectDTParser();
            List<int> GestureSeqs = new List<int>();
            int Pose;
            int startParse;
            //Make 5 sequences pose for gesture
            for (int id = 0; id < 5; id++)
            {
                startParse = id;
                while (startParse < batch.Count())
                {
                    int[] seqs = translator.parsefromKinectRow(batch[startParse], "HIERARCHIC");
                    Pose = layer1.Classify(seqs);
                    GestureSeqs.Add(Pose);
                    startParse = startParse + 5;
                }
                Sequences.Add(new Samples(GestureSeqs.ToArray(), i));
            }

            foreach (Samples sample in Sequences)
            {
                Console.WriteLine(String.Join(";",sample.Sequence));
            }
            foreach (string lable in Labels)
            {
                Console.Write(lable+" ");
            }
            Console.WriteLine(Sequences.Count());
        }
        public void Load(string label, IMongoCollection<BrekelMongo> source, int start, int end)
        {
            //Cek Label jika tidak ada masukkan ke Labels
            int i = Labels.IndexOf(label);
            if (i < 0)
            {
                Labels.Add(label);
                i = Labels.IndexOf(label);
            }
            //Do Loading
            var filter = FilterDefinition<BrekelMongo>.Empty;// TO DO: Import class of Kinect Data and change all BsonDocument instances
            var cursor = source.Find(filter).Skip(start-1).Limit(end-start+1);
            List<BrekelMongo> batch = cursor.ToList<BrekelMongo>();
            //batch = batch.GetRange(0, end - start + 1);
            KinectDTParser translator = new KinectDTParser();
            List<int> GestureSeqs = new List<int>();
            int Pose;
            int startParse;
            //Make 5 sequences pose for gesture
            for (int id = 0; id < 5; id++)
            {
                startParse = id;
                while (startParse < batch.Count())
                {
                    int[] seqs = translator.parsefromBrekelRow(batch[startParse]);
                    Pose = layer1.Classify(seqs);
                    GestureSeqs.Add(Pose);
                    startParse = startParse + 5;
                }
                Sequences.Add(new Samples(GestureSeqs.ToArray(), i));
                GestureSeqs.Clear();
            }
            foreach (Samples sample in Sequences)
            {
                Console.WriteLine(String.Join(";", sample.Sequence));
            }
            foreach (string lable in Labels)
            {
                Console.Write(lable + " ");
            }
            Console.WriteLine(Sequences.Count());
        }
        public void Reset()
        {
            Labels = new BindingList<string>();
            Sequences = new BindingList<Samples>();
        }
        public void LoadCSV(string label, string source, int start, int end)
        {
            //Cek Label jika tidak ada masukkan ke Labels
            int i = Labels.IndexOf(label);
            if (i < 0)
            {
                Labels.Add(label);
                i = Labels.IndexOf(label);
            }
            //Do Loading
            /*
            var filter = FilterDefinition<MongoJoint>.Empty;// TO DO: Import class of Kinect Data and change all BsonDocument instances
            var cursor = source.Find(filter);
            List<MongoJoint> batch = cursor.ToList<MongoJoint>();
            */
            List<MongoJoint> batch = new List<MongoJoint>();
            using (StreamReader sr=new StreamReader(source))
            {
                string[] lines;
                for (int j = 1; j<start ; j++)
                {
                    sr.ReadLine();// Skip until startline
                }
                for (int j = 0; j <= end-start; j++)
                {
                    lines = sr.ReadLine().Split(';');
                    batch.Add(MJfromCSV(lines));
                }
            }
            KinectDTParser translator = new KinectDTParser();
            foreach (MongoJoint bison in batch)
            {
                //Convert Bison to int[] sequence
                int[] seqs = translator.parsefromKinectRow(bison, "HIERARCHIC");//TO DO: Change new int to method converting bison
                Sequences.Add(new Samples(seqs, i));
            }
            foreach (Samples sample in Sequences)
            {
                Console.WriteLine(String.Join(" ", sample.Sequence));
            }
            foreach (string lable in Labels)
            {
                Console.Write(lable + " ");
            }
            Console.WriteLine(Sequences.Count());
        }
        public MongoJoint MJfromCSV(string[] fromCSV)
        {
            MongoJoint MJ=new MongoJoint();
            MJ.waist_tx = double.Parse(fromCSV[0]);
            MJ.waist_ty = double.Parse(fromCSV[1]);
            MJ.waist_tz = double.Parse(fromCSV[2]);
            MJ.waist_rx = double.Parse(fromCSV[3]);
            MJ.waist_ry = double.Parse(fromCSV[4]);
            MJ.waist_rz = double.Parse(fromCSV[5]);
            MJ.spine_tx = double.Parse(fromCSV[6]);
            MJ.spine_ty = double.Parse(fromCSV[7]);
            MJ.spine_tz = double.Parse(fromCSV[8]);
            MJ.spine_rx = double.Parse(fromCSV[9]);
            MJ.spine_ry = double.Parse(fromCSV[10]);
            MJ.spine_rz = double.Parse(fromCSV[11]);
            MJ.chest_tx = double.Parse(fromCSV[12]);
            MJ.chest_ty = double.Parse(fromCSV[13]);
            MJ.chest_tz = double.Parse(fromCSV[14]);
            MJ.chest_rx = double.Parse(fromCSV[15]);
            MJ.chest_ry = double.Parse(fromCSV[16]);
            MJ.chest_rz = double.Parse(fromCSV[17]);
            MJ.neck_tx = double.Parse(fromCSV[18]);
            MJ.neck_ty = double.Parse(fromCSV[19]);
            MJ.neck_tz = double.Parse(fromCSV[20]);
            MJ.neck_rx = double.Parse(fromCSV[21]);
            MJ.neck_ry = double.Parse(fromCSV[22]);
            MJ.neck_rz = double.Parse(fromCSV[23]);
            MJ.head_tx = double.Parse(fromCSV[24]);
            MJ.head_ty = double.Parse(fromCSV[25]);
            MJ.head_tz = double.Parse(fromCSV[26]);
            MJ.head_rx = double.Parse(fromCSV[27]);
            MJ.head_ry = double.Parse(fromCSV[28]);
            MJ.head_rz = double.Parse(fromCSV[29]);
            MJ.upperLeg_L_tx = double.Parse(fromCSV[30]);
            MJ.upperLeg_L_ty = double.Parse(fromCSV[31]);
            MJ.upperLeg_L_tz = double.Parse(fromCSV[32]);
            MJ.upperLeg_L_rx = double.Parse(fromCSV[33]);
            MJ.upperLeg_L_ry = double.Parse(fromCSV[34]);
            MJ.upperLeg_L_rz = double.Parse(fromCSV[35]);
            MJ.lowerLeg_L_tx = double.Parse(fromCSV[36]);
            MJ.lowerLeg_L_ty = double.Parse(fromCSV[37]);
            MJ.lowerLeg_L_tz = double.Parse(fromCSV[38]);
            MJ.lowerLeg_L_rx = double.Parse(fromCSV[39]);
            MJ.lowerLeg_L_ry = double.Parse(fromCSV[40]);
            MJ.lowerLeg_L_rz = double.Parse(fromCSV[41]);
            MJ.foot_L_tx = double.Parse(fromCSV[42]);
            MJ.foot_L_ty = double.Parse(fromCSV[43]);
            MJ.foot_L_tz = double.Parse(fromCSV[44]);
            MJ.foot_L_rx = double.Parse(fromCSV[45]);
            MJ.foot_L_ry = double.Parse(fromCSV[46]);
            MJ.foot_L_rz = double.Parse(fromCSV[47]);
            MJ.toes_L_tx = double.Parse(fromCSV[48]);
            MJ.toes_L_ty = double.Parse(fromCSV[49]);
            MJ.toes_L_tz = double.Parse(fromCSV[50]);
            MJ.toes_L_rx = double.Parse(fromCSV[51]);
            MJ.toes_L_ry = double.Parse(fromCSV[52]);
            MJ.toes_L_rz = double.Parse(fromCSV[53]);
            MJ.upperLeg_R_tx = double.Parse(fromCSV[54]);
            MJ.upperLeg_R_ty = double.Parse(fromCSV[55]);
            MJ.upperLeg_R_tz = double.Parse(fromCSV[56]);
            MJ.upperLeg_R_rx = double.Parse(fromCSV[57]);
            MJ.upperLeg_R_ry = double.Parse(fromCSV[58]);
            MJ.upperLeg_R_rz = double.Parse(fromCSV[59]);
            MJ.lowerLeg_R_tx = double.Parse(fromCSV[60]);
            MJ.lowerLeg_R_ty = double.Parse(fromCSV[61]);
            MJ.lowerLeg_R_tz = double.Parse(fromCSV[62]);
            MJ.lowerLeg_R_rx = double.Parse(fromCSV[63]);
            MJ.lowerLeg_R_ry = double.Parse(fromCSV[64]);
            MJ.lowerLeg_R_rz = double.Parse(fromCSV[65]);
            MJ.foot_R_tx = double.Parse(fromCSV[66]);
            MJ.foot_R_ty = double.Parse(fromCSV[67]);
            MJ.foot_R_tz = double.Parse(fromCSV[68]);
            MJ.foot_R_rx = double.Parse(fromCSV[69]);
            MJ.foot_R_ry = double.Parse(fromCSV[70]);
            MJ.foot_R_rz = double.Parse(fromCSV[71]);
            MJ.toes_R_tx = double.Parse(fromCSV[72]);
            MJ.toes_R_ty = double.Parse(fromCSV[73]);
            MJ.toes_R_tz = double.Parse(fromCSV[74]);
            MJ.toes_R_rx = double.Parse(fromCSV[75]);
            MJ.toes_R_ry = double.Parse(fromCSV[76]);
            MJ.toes_R_rz = double.Parse(fromCSV[77]);
            MJ.collar_L_tx = double.Parse(fromCSV[78]);
            MJ.collar_L_ty = double.Parse(fromCSV[79]);
            MJ.collar_L_tz = double.Parse(fromCSV[80]);
            MJ.collar_L_rx = double.Parse(fromCSV[81]);
            MJ.collar_L_ry = double.Parse(fromCSV[82]);
            MJ.collar_L_rz = double.Parse(fromCSV[83]);
            MJ.upperArm_L_tx = double.Parse(fromCSV[84]);
            MJ.upperArm_L_ty = double.Parse(fromCSV[85]);
            MJ.upperArm_L_tz = double.Parse(fromCSV[86]);
            MJ.upperArm_L_rx = double.Parse(fromCSV[87]);
            MJ.upperArm_L_ry = double.Parse(fromCSV[88]);
            MJ.upperArm_L_rz = double.Parse(fromCSV[89]);
            MJ.foreArm_L_tx = double.Parse(fromCSV[90]);
            MJ.foreArm_L_ty = double.Parse(fromCSV[91]);
            MJ.foreArm_L_tz = double.Parse(fromCSV[92]);
            MJ.foreArm_L_rx = double.Parse(fromCSV[93]);
            MJ.foreArm_L_ry = double.Parse(fromCSV[94]);
            MJ.foreArm_L_rz = double.Parse(fromCSV[95]);
            MJ.hand_L_tx = double.Parse(fromCSV[96]);
            MJ.hand_L_ty = double.Parse(fromCSV[97]);
            MJ.hand_L_tz = double.Parse(fromCSV[98]);
            MJ.hand_L_rx = double.Parse(fromCSV[99]);
            MJ.hand_L_ry = double.Parse(fromCSV[100]);
            MJ.hand_L_rz = double.Parse(fromCSV[101]);
            MJ.collar_R_tx = double.Parse(fromCSV[102]);
            MJ.collar_R_ty = double.Parse(fromCSV[103]);
            MJ.collar_R_tz = double.Parse(fromCSV[104]);
            MJ.collar_R_rx = double.Parse(fromCSV[105]);
            MJ.collar_R_ry = double.Parse(fromCSV[106]);
            MJ.collar_R_rz = double.Parse(fromCSV[107]);
            MJ.upperArm_R_tx = double.Parse(fromCSV[108]);
            MJ.upperArm_R_ty = double.Parse(fromCSV[109]);
            MJ.upperArm_R_tz = double.Parse(fromCSV[110]);
            MJ.upperArm_R_rx = double.Parse(fromCSV[111]);
            MJ.upperArm_R_ry = double.Parse(fromCSV[112]);
            MJ.upperArm_R_rz = double.Parse(fromCSV[113]);
            MJ.foreArm_R_tx = double.Parse(fromCSV[114]);
            MJ.foreArm_R_ty = double.Parse(fromCSV[115]);
            MJ.foreArm_R_tz = double.Parse(fromCSV[116]);
            MJ.foreArm_R_rx = double.Parse(fromCSV[117]);
            MJ.foreArm_R_ry = double.Parse(fromCSV[118]);
            MJ.foreArm_R_rz = double.Parse(fromCSV[119]);
            MJ.hand_R_tx = double.Parse(fromCSV[120]);
            MJ.hand_R_ty = double.Parse(fromCSV[121]);
            MJ.hand_R_tz = double.Parse(fromCSV[122]);
            MJ.hand_R_rx = double.Parse(fromCSV[123]);
            MJ.hand_R_ry = double.Parse(fromCSV[124]);
            MJ.hand_R_rz = double.Parse(fromCSV[125]);
            MJ.stamp = fromCSV[126];
            return MJ;
        }
    }
}
