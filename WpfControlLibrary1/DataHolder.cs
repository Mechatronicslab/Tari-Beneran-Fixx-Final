using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlLibrary1
{
    public class DataHolder
    {
        public List<BrekelMongo> batch { get; set; }
        public List<CanvasSkeletal> skeleton { get; set; }
        public DataHolder()
        {
            batch = new List<BrekelMongo>();
        }
        public void Load(IMongoCollection<BrekelMongo> source, double CV_Width = 1920, double CV_Height = 1080)
        {
            var filter = FilterDefinition<BrekelMongo>.Empty;// TO DO: Import class of Kinect Data and change all BsonDocument instances
            var cursor = source.Find(filter).Skip(5000).Limit(500);
            List<BrekelMongo> batch = cursor.ToList<BrekelMongo>();
            Console.WriteLine("DATA GET");
            skeleton = batch.SkeletalTransformation();
        }
        public void LoadRow(IMongoCollection<BrekelMongo> source, int row)
        {
            var filter = FilterDefinition<BrekelMongo>.Empty;// TO DO: Import class of Kinect Data and change all BsonDocument instances
            var cursor = source.Find(filter).Skip(row).Limit(1);
            List<BrekelMongo> batch = cursor.ToList<BrekelMongo>();
            Console.WriteLine("DATA GET");
            skeleton = batch.SkeletalTransformation();
        }
        public void LoadRow(IMongoCollection<BrekelMongo> source, int row, string mode)
        {
            var filter = FilterDefinition<BrekelMongo>.Empty;// TO DO: Import class of Kinect Data and change all BsonDocument instances
            var cursor = source.Find(filter).Skip(row).Limit(2);
            List<BrekelMongo> batch = cursor.ToList<BrekelMongo>();

            skeleton = batch.SkeletalTransformation();
            if (skeleton.Count() >= 2)
            {
                skeleton[0] = movingavg(skeleton[1], skeleton[0]);
            }
            Console.WriteLine("DATA GET");
        }
        public CanvasSkeletal movingavg(CanvasSkeletal curr, CanvasSkeletal pred)
        {
            CanvasSkeletal filtered = new CanvasSkeletal();
            filtered.waist = filter(curr.waist, pred.waist);
            filtered.spine = filter(curr.spine, pred.spine);
            filtered.chest = filter(curr.chest, pred.chest);
            filtered.neck = filter(curr.neck, pred.neck);
            filtered.head = filter(curr.head, pred.head);
            filtered.head_tip = filter(curr.head_tip, pred.head_tip);
            filtered.collar_L = filter(curr.collar_L, pred.collar_L);
            filtered.upperArm_L = filter(curr.upperArm_L, pred.upperArm_L);
            filtered.foreArm_L = filter(curr.foreArm_L, pred.foreArm_L);
            filtered.hand_L = filter(curr.hand_L, pred.hand_L);
            filtered.collar_R = filter(curr.collar_R, pred.collar_R);
            filtered.upperArm_R = filter(curr.upperArm_R, pred.upperArm_R);
            filtered.foreArm_R = filter(curr.foreArm_R, pred.foreArm_R);
            filtered.hand_R = filter(curr.hand_R, pred.hand_R);
            filtered.upperLeg_L = filter(curr.upperLeg_L, pred.upperLeg_L);
            filtered.lowerLeg_L = filter(curr.lowerLeg_L, pred.lowerLeg_L);
            filtered.foot_L = filter(curr.foot_L, pred.foot_L);
            filtered.toes_L = filter(curr.toes_L, pred.toes_L);
            filtered.upperLeg_R = filter(curr.upperLeg_R, pred.upperLeg_R);
            filtered.lowerLeg_R = filter(curr.lowerLeg_R, pred.lowerLeg_R);
            filtered.foot_R = filter(curr.foot_R, pred.foot_R);
            filtered.toes_R = filter(curr.toes_R, pred.toes_R);
            filtered.middle_L = filter(curr.middle_L, pred.middle_L);
            filtered.middle_R = filter(curr.middle_R, pred.middle_R);
            filtered.thumb_L = filter(curr.thumb_L, pred.thumb_L);
            filtered.thumb_R = filter(curr.thumb_R, pred.thumb_R);
            return filtered;
        }
        public Joint filter(Joint curr, Joint pred)
        {
            Joint filtered = new Joint(0, 0, 0, 0);
            if (curr.trackingState != 0 || pred.trackingState != 0)
            {
                double scale1 = curr.trackingState / (curr.trackingState + pred.trackingState);
                double scale2 = pred.trackingState / (curr.trackingState + pred.trackingState);
                filtered.posX = scale1 * curr.posX + scale2 * pred.posX;
                filtered.posY = scale1 * curr.posY + scale2 * pred.posY;
                filtered.posZ = scale1 * curr.posZ + scale2 * pred.posZ;
                filtered.trackingState = Math.Max(curr.trackingState, pred.trackingState);
            }
            return filtered;
        }
    }
}
