using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlLibrary1
{
    public class CanvasSkeletal
    {
        public Joint waist { get; set; }
        public Joint spine { get; set; }
        public Joint chest { get; set; }
        public Joint neck { get; set; }
        public Joint head { get; set; }
        public Joint head_tip { get; set; }
        public Joint upperLeg_L { get; set; }
        public Joint lowerLeg_L { get; set; }
        public Joint foot_L { get; set; }
        public Joint toes_L { get; set; }
        public Joint upperLeg_R { get; set; }
        public Joint lowerLeg_R { get; set; }
        public Joint foot_R { get; set; }
        public Joint toes_R { get; set; }
        public Joint collar_L { get; set; }
        public Joint upperArm_L { get; set; }
        public Joint foreArm_L { get; set; }
        public Joint hand_L { get; set; }
        public Joint collar_R { get; set; }
        public Joint upperArm_R { get; set; }
        public Joint foreArm_R { get; set; }
        public Joint hand_R { get; set; }
        public Joint thumb_R { get; set; }
        public Joint thumb_L { get; set; }
        public Joint middle_R { get; set; }
        public Joint middle_L { get; set; }
        public CanvasSkeletal()
        {
            waist = new Joint(0, 0, 0, 0);
            spine = new Joint(0, 0, 0, 0);
            chest = new Joint(0, 0, 0, 0);
            neck = new Joint(0, 0, 0, 0);
            head = new Joint(0, 0, 0, 0);
            head_tip = new Joint(0, 0, 0, 0);
            upperLeg_L = new Joint(0, 0, 0, 0);
            lowerLeg_L = new Joint(0, 0, 0, 0);
            foot_L = new Joint(0, 0, 0, 0);
            toes_L = new Joint(0, 0, 0, 0);
            upperLeg_R = new Joint(0, 0, 0, 0);
            lowerLeg_R = new Joint(0, 0, 0, 0);
            foot_R = new Joint(0, 0, 0, 0);
            toes_R = new Joint(0, 0, 0, 0);
            collar_L = new Joint(0, 0, 0, 0);
            upperArm_L = new Joint(0, 0, 0, 0);
            foreArm_L = new Joint(0, 0, 0, 0);
            hand_L = new Joint(0, 0, 0, 0);
            collar_R = new Joint(0, 0, 0, 0);
            upperArm_R = new Joint(0, 0, 0, 0);
            foreArm_R = new Joint(0, 0, 0, 0);
            hand_R = new Joint(0, 0, 0, 0);
            thumb_L = new Joint(0, 0, 0, 0);
            middle_L = new Joint(0, 0, 0, 0);
            thumb_R = new Joint(0, 0, 0, 0);
            middle_R = new Joint(0, 0, 0, 0);
        }
        public CanvasSkeletal(BrekelMongo prev)
        {
            waist = new Joint(0, 0, 0, prev.waist_conf);
            spine = new Joint(waist.posX + prev.spine_tx, waist.posY + prev.spine_ty, waist.posZ + prev.spine_tz, prev.spine_conf);
            chest = new Joint(spine.posX + prev.chest_tx, spine.posY + prev.chest_ty, spine.posZ + prev.chest_tz, prev.chest_conf);
            neck = new Joint(chest.posX + prev.neck_tx, chest.posY + prev.neck_ty, chest.posZ + prev.neck_tz, prev.neck_conf);
            head = new Joint(neck.posX + prev.head_tx, neck.posY + prev.head_ty, neck.posZ + prev.head_tz, prev.head_conf);
            head_tip = new Joint(head.posX + prev.head_tip_tx, head.posY + prev.head_tip_ty, head.posZ + prev.head_tip_tz, prev.head_tip_conf);
            upperLeg_L = new Joint(waist.posX + prev.upperLeg_L_tx, waist.posY + prev.upperLeg_L_ty, waist.posZ + prev.upperLeg_L_tz, prev.upperLeg_L_conf);
            lowerLeg_L = new Joint(upperLeg_L.posX + prev.lowerLeg_L_tx, upperLeg_L.posY + prev.lowerLeg_L_ty, upperLeg_L.posZ + prev.lowerLeg_L_tz, prev.lowerLeg_L_conf);
            foot_L = new Joint(lowerLeg_L.posX + prev.foot_L_tx, lowerLeg_L.posY + prev.foot_L_ty, lowerLeg_L.posZ + prev.foot_L_tz, prev.foot_L_conf);
            toes_L = new Joint(foot_L.posX + prev.toes_L_tx, foot_L.posY + prev.toes_L_ty, foot_L.posZ + prev.toes_L_tz, prev.toes_L_conf);
            upperLeg_R = new Joint(waist.posX + prev.upperLeg_R_tx, waist.posY + prev.upperLeg_R_ty, waist.posZ + prev.upperLeg_R_tz, prev.upperLeg_R_conf);
            lowerLeg_R = new Joint(upperLeg_R.posX + prev.lowerLeg_R_tx, upperLeg_R.posY + prev.lowerLeg_R_ty, upperLeg_R.posZ + prev.lowerLeg_R_tz, prev.lowerLeg_R_conf);
            foot_R = new Joint(lowerLeg_R.posX + prev.foot_R_tx, lowerLeg_R.posY + prev.foot_R_ty, lowerLeg_R.posZ + prev.foot_R_tz, prev.foot_R_conf);
            toes_R = new Joint(foot_R.posX + prev.toes_R_tx, foot_R.posY + prev.toes_R_ty, foot_R.posZ + prev.toes_R_tz, prev.toes_R_conf);
            collar_L = new Joint(neck.posX + prev.collar_L_tx, neck.posY + prev.collar_L_ty, neck.posZ + prev.collar_L_tz, prev.collar_L_conf);
            upperArm_L = new Joint(collar_L.posX + prev.upperArm_L_tx, collar_L.posY + prev.upperArm_L_ty, collar_L.posZ + prev.upperArm_L_tz, prev.upperArm_L_conf);
            foreArm_L = new Joint(upperArm_L.posX + prev.foreArm_L_tx, upperArm_L.posY + prev.foreArm_L_ty, upperArm_L.posZ + prev.foreArm_L_tz, prev.foreArm_L_conf);
            hand_L = new Joint(foreArm_L.posX + prev.hand_L_tx, foreArm_L.posY + prev.hand_L_ty, foreArm_L.posZ + prev.hand_L_tz, prev.hand_L_conf);
            collar_R = new Joint(neck.posX + prev.collar_R_tx, neck.posY + prev.collar_R_ty, neck.posZ + prev.collar_R_tz, prev.collar_R_conf);
            upperArm_R = new Joint(collar_R.posX + prev.upperArm_R_tx, collar_R.posY + prev.upperArm_R_ty, collar_R.posZ + prev.upperArm_R_tz, prev.upperArm_R_conf);
            foreArm_R = new Joint(upperArm_R.posX + prev.foreArm_R_tx, upperArm_R.posY + prev.foreArm_R_ty, upperArm_R.posZ + prev.foreArm_R_tz, prev.foreArm_R_conf);
            hand_R = new Joint(foreArm_R.posX + prev.hand_R_tx, foreArm_R.posY + prev.hand_R_ty, foreArm_R.posZ + prev.hand_R_tz, prev.hand_R_conf);
            thumb_L = new Joint(hand_L.posX + prev.thumb_L_0_tx, hand_L.posY + prev.thumb_L_0_ty, hand_L.posZ + prev.thumb_L_0_tz, prev.thumb_L_0_conf);
            middle_L = new Joint(hand_L.posX + prev.middle_L_0_tx, hand_L.posY + prev.middle_L_0_ty, hand_L.posZ + prev.middle_L_0_tz, prev.middle_L_0_conf);
            thumb_R = new Joint(hand_R.posX + prev.thumb_R_0_tx, hand_R.posY + prev.thumb_R_0_ty, hand_R.posZ + prev.thumb_R_0_tz, prev.thumb_R_0_conf);
            middle_R = new Joint(hand_R.posX + prev.middle_R_0_tx, hand_R.posY + prev.middle_R_0_ty, hand_R.posZ + prev.middle_R_0_tz, prev.middle_R_0_conf);
        }
        public CanvasSkeletal(BrekelMongo prev, string mode)
        {
            waist = new Joint(prev.waist_tx - 45, prev.waist_ty - 60, prev.waist_tz + 200, prev.waist_conf);
            //waist = new Joint(0, 0, 0, prev.waist_conf);
            spine = new Joint(prev.spine_tx, prev.spine_ty, prev.spine_tz, prev.spine_rx, prev.spine_ry, prev.spine_rz, prev.spine_conf, waist);
            chest = new Joint(prev.chest_tx, prev.chest_ty, prev.chest_tz, prev.chest_rx, prev.chest_ry, prev.chest_rz, prev.chest_conf, spine);
            neck = new Joint(prev.neck_tx, prev.neck_ty, prev.neck_tz, prev.neck_rx, prev.neck_ry, prev.neck_rz, prev.neck_conf, chest);
            head = new Joint(prev.head_tx, prev.head_ty, prev.head_tz, prev.head_rx, prev.head_ry, prev.head_rz, prev.head_conf, neck);
            head_tip = new Joint(prev.head_tip_tx, prev.head_tip_ty, prev.head_tip_tz, prev.head_tip_rx, prev.head_tip_ry, prev.head_tip_rz, prev.head_tip_conf, head);
            upperLeg_L = new Joint(prev.upperLeg_L_tx, prev.upperLeg_L_ty, prev.upperLeg_L_tz, prev.upperLeg_L_rx, prev.upperLeg_L_ry, prev.upperLeg_L_rz, prev.upperLeg_L_conf, waist);
            lowerLeg_L = new Joint(prev.lowerLeg_L_tx, prev.lowerLeg_L_ty, prev.lowerLeg_L_tz, prev.lowerLeg_L_rx, prev.lowerLeg_L_ry, prev.lowerLeg_L_rz, prev.lowerLeg_L_conf, upperLeg_L);
            foot_L = new Joint(prev.foot_L_tx, prev.foot_L_ty, prev.foot_L_tz, prev.foot_L_rx, prev.foot_L_ry, prev.foot_L_rz, prev.foot_L_conf, lowerLeg_L);
            toes_L = new Joint(prev.toes_L_tx, prev.toes_L_ty, prev.toes_L_tz, prev.toes_L_rx, prev.toes_L_ry, prev.toes_L_rz, prev.toes_L_conf, foot_L);
            upperLeg_R = new Joint(prev.upperLeg_R_tx, prev.upperLeg_R_ty, prev.upperLeg_R_tz, prev.upperLeg_R_rx, prev.upperLeg_R_ry, prev.upperLeg_R_rz, prev.upperLeg_R_conf, waist);
            lowerLeg_R = new Joint(prev.lowerLeg_R_tx, prev.lowerLeg_R_ty, prev.lowerLeg_R_tz, prev.lowerLeg_R_rx, prev.lowerLeg_R_ry, prev.lowerLeg_R_rz, prev.lowerLeg_R_conf, upperLeg_R);
            foot_R = new Joint(prev.foot_R_tx, prev.foot_R_ty, prev.foot_R_tz, prev.foot_R_rx, prev.foot_R_ry, prev.foot_R_rz, prev.foot_R_conf, lowerLeg_R);
            toes_R = new Joint(prev.toes_R_tx, prev.toes_R_ty, prev.toes_R_tz, prev.toes_R_rx, prev.toes_R_ry, prev.toes_R_rz, prev.toes_R_conf, foot_R);
            collar_L = new Joint(prev.collar_L_tx, prev.collar_L_ty, prev.collar_L_tz, prev.collar_L_rx, prev.collar_L_ry, prev.collar_L_rz, prev.collar_L_conf, neck);
            upperArm_L = new Joint(prev.upperArm_L_tx, prev.upperArm_L_ty, prev.upperArm_L_tz, prev.upperArm_L_rx, prev.upperArm_L_ry, prev.upperArm_L_rz, prev.upperArm_L_conf, collar_L);
            foreArm_L = new Joint(prev.foreArm_L_tx, prev.foreArm_L_ty, prev.foreArm_L_tz, prev.foreArm_L_rx, prev.foreArm_L_ry, prev.foreArm_L_rz, prev.foreArm_L_conf, upperArm_L);
            hand_L = new Joint(prev.hand_L_tx, prev.hand_L_ty, prev.hand_L_tz, prev.hand_L_rx, prev.hand_L_ry, prev.hand_L_rz, prev.hand_L_conf, foreArm_L);
            collar_R = new Joint(prev.collar_R_tx, prev.collar_R_ty, prev.collar_R_tz, prev.collar_R_rx, prev.collar_R_ry, prev.collar_R_rz, prev.collar_R_conf, neck);
            upperArm_R = new Joint(prev.upperArm_R_tx, prev.upperArm_R_ty, prev.upperArm_R_tz, prev.upperArm_R_rx, prev.upperArm_R_ry, prev.upperArm_R_rz, prev.upperArm_R_conf, collar_R);
            foreArm_R = new Joint(prev.foreArm_R_tx, prev.foreArm_R_ty, prev.foreArm_R_tz, prev.foreArm_R_rx, prev.foreArm_R_ry, prev.foreArm_R_rz, prev.foreArm_R_conf, upperArm_R);
            hand_R = new Joint(prev.hand_R_tx, prev.hand_R_ty, prev.hand_R_tz, prev.hand_R_rx, prev.hand_R_ry, prev.hand_R_rz, prev.hand_R_conf, foreArm_R);
            thumb_L = new Joint(prev.thumb_L_0_tx, prev.thumb_L_0_ty, prev.thumb_L_0_tz, prev.thumb_L_0_rx, prev.thumb_L_0_ry, prev.thumb_L_0_rz, prev.thumb_L_0_conf, hand_L);
            middle_L = new Joint(prev.middle_L_0_tx, prev.middle_L_0_ty, prev.middle_L_0_tz, prev.middle_L_0_rx, prev.middle_L_0_ry, prev.middle_L_0_rz, prev.middle_L_0_conf, hand_L);
            thumb_R = new Joint(prev.thumb_R_0_tx, prev.thumb_R_0_ty, prev.thumb_R_0_tz, prev.thumb_R_0_rx, prev.thumb_R_0_ry, prev.thumb_R_0_rz, prev.thumb_R_0_conf, hand_R);
            middle_R = new Joint(prev.middle_R_0_tx, prev.middle_R_0_ty, prev.middle_R_0_tz, prev.middle_R_0_rx, prev.middle_R_0_ry, prev.middle_R_0_rz, prev.middle_R_0_conf, hand_R);
        }
        public List<Joint> getList()
        {
            List<Joint> list = new List<Joint>();
            list.Add(waist);//0
            list.Add(spine);//1
            list.Add(chest);//2
            list.Add(neck);//3
            list.Add(head);//4
            list.Add(head_tip);//5
            list.Add(upperLeg_L);//6
            list.Add(lowerLeg_L);//7
            list.Add(foot_L);//8
            list.Add(toes_L);//9
            list.Add(upperLeg_R);//10
            list.Add(lowerLeg_R);//11
            list.Add(foot_R);//12
            list.Add(toes_R);//13
            list.Add(collar_L);//14
            list.Add(upperArm_L);//15
            list.Add(foreArm_L);//16
            list.Add(hand_L);//17
            list.Add(collar_R);//18
            list.Add(upperArm_R);//19
            list.Add(foreArm_R);//20
            list.Add(hand_R);//21
            list.Add(thumb_L);//22
            list.Add(middle_L);//23
            list.Add(thumb_R);//24
            list.Add(middle_R);//25
            return list;
        }
    }
    public class Joint
    {
        public double posX { get; set; }
        public double posY { get; set; }
        public double posZ { get; set; }
        public double[,] rotM { get; set; }
        public double trackingState { get; set; }
        public Joint(double x, double y, double z, double track)
        {
            posX = x;
            posY = y;
            posZ = z;
            rotM = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            trackingState = track;
        }
        public Joint(double x, double y, double z, double rx, double ry, double rz, double track)
        {
            posX = x;
            posY = y;
            posZ = z;
            rotM = transM(rx, ry, rz);
            trackingState = track;
        }
        /*
        public Joint(double x, double y, double z, double rx, double ry, double rz, double track,Joint prev)
        {
            double conv = Math.PI/180;
            trackingState = track;
            //yaw-->roll-->pitch y-->z-->x
            //posY = y;
            //posX = x * Math.Cos(ry * conv) + z * Math.Sin(ry * conv);
            //posZ = -x * Math.Sin(ry * conv) + z * Math.Cos(ry * conv);
            //
            //1st trf
            //double temp = posY;
            //x-->z-->y
            posX = x;
            posY = y * Math.Cos(rx * conv) - z * Math.Sin(rx * conv);
            posZ = y * Math.Sin(rx * conv) + z * Math.Cos(rx * conv);

            double temp = posX;
            posX = temp * Math.Cos(ry * conv) + posZ * Math.Sin(ry * conv);
            posZ = -temp * Math.Sin(ry * conv) + posZ * Math.Cos(ry * conv);

            double temp2 = posX;
            posX = temp2 * Math.Cos(rz * conv) - posY * Math.Sin(rz * conv);
            posY = temp2 * Math.Sin(rz * conv) + posY * Math.Cos(rz * conv);
            
            //2nd trf
            //double temp = posX;
            //posX = temp * Math.Cos(rz * conv) - posY * Math.Sin(rz * conv);
            //posY = temp * Math.Sin(rz * conv) + posY * Math.Cos(rz * conv);
            //double temp2 = posX;
            //posX = temp2 * Math.Cos(ry * conv) + posZ * Math.Sin(ry * conv);
            //posZ = - temp2 * Math.Sin(ry * conv) + posZ * Math.Cos(ry * conv);
            //3rd trf
            //temp = posX;
            //posX = temp * Math.Cos(rz * conv) - posY * Math.Sin(rz * conv);
            //posY = temp * Math.Sin(rz * conv) + posY * Math.Cos(rz * conv);
            //trans
            posX = prev.posX + posX;
            posY = prev.posY + posY;
            posZ = prev.posZ + posZ;
        }
        */

        public Joint(double x, double y, double z, double rx, double ry, double rz, double track, Joint prev)
        {
            double[,] rotChild = transM(rx, ry, rz);
            double[,] rotParent = prev.rotM;

            posX = rotParent[0, 0] * x + rotParent[0, 1] * y + rotParent[0, 2] * z + prev.posX;
            posY = rotParent[1, 0] * x + rotParent[1, 1] * y + rotParent[1, 2] * z + prev.posY;
            posZ = rotParent[2, 0] * x + rotParent[2, 1] * y + rotParent[2, 2] * z + prev.posZ;

            rotM = rotateP(rotChild, rotParent);
            trackingState = track;
        }

        double[,] transM(double rx, double ry, double rz)
        {
            double[,] trM = new double[3, 3];
            double conv = Math.PI / 180;
            trM[0, 0] = Math.Cos(ry * conv) * Math.Cos(rz * conv) - Math.Sin(rx * conv) * Math.Sin(ry * conv) * Math.Sin(rz * conv);
            trM[0, 1] = -Math.Cos(rx * conv) * Math.Sin(rz * conv);
            trM[0, 2] = Math.Sin(ry * conv) * Math.Cos(rz * conv) + Math.Sin(rx * conv) * Math.Cos(ry * conv) * Math.Sin(rz * conv);
            trM[1, 0] = Math.Cos(ry * conv) * Math.Sin(rz * conv) + Math.Sin(rx * conv) * Math.Sin(ry * conv) * Math.Cos(rz * conv);
            trM[1, 1] = Math.Cos(rx * conv) * Math.Cos(rz * conv);
            trM[1, 2] = Math.Sin(ry * conv) * Math.Sin(rz * conv) - Math.Sin(rx * conv) * Math.Cos(ry * conv) * Math.Cos(rz * conv);
            trM[2, 0] = -Math.Cos(rx * conv) * Math.Sin(ry * conv);
            trM[2, 1] = Math.Sin(rx * conv);
            trM[2, 2] = Math.Cos(rx * conv) * Math.Cos(ry * conv);
            return trM;
        }
        double[,] rotateP(double[,] rotC, double[,] rotP)
        {
            double[,] trM = new double[3, 3];
            trM[0, 0] = rotC[0, 0] * rotP[0, 0] + rotC[1, 0] * rotP[0, 1] + rotC[2, 0] * rotP[0, 2];
            trM[0, 1] = rotC[0, 1] * rotP[0, 0] + rotC[1, 1] * rotP[0, 1] + rotC[2, 1] * rotP[0, 2];
            trM[0, 2] = rotC[0, 2] * rotP[0, 0] + rotC[1, 2] * rotP[0, 1] + rotC[2, 2] * rotP[0, 2];
            trM[1, 0] = rotC[0, 0] * rotP[1, 0] + rotC[1, 0] * rotP[1, 1] + rotC[2, 0] * rotP[1, 2];
            trM[1, 1] = rotC[0, 1] * rotP[1, 0] + rotC[1, 1] * rotP[1, 1] + rotC[2, 1] * rotP[1, 2];
            trM[1, 2] = rotC[0, 2] * rotP[1, 0] + rotC[1, 2] * rotP[1, 1] + rotC[2, 2] * rotP[1, 2];
            trM[2, 0] = rotC[0, 0] * rotP[2, 0] + rotC[1, 0] * rotP[2, 1] + rotC[2, 0] * rotP[2, 2];
            trM[2, 1] = rotC[0, 1] * rotP[2, 0] + rotC[1, 1] * rotP[2, 1] + rotC[2, 1] * rotP[2, 2];
            trM[2, 2] = rotC[0, 2] * rotP[2, 0] + rotC[1, 2] * rotP[2, 1] + rotC[2, 2] * rotP[2, 2];
            return trM;
        }
    }
}
