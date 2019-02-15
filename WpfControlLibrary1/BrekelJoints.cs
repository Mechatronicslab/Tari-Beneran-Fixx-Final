using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlLibrary1
{
    class BrekelJoints1
    {
        public Joint1 waist { get; set; }
        public Joint1 spine { get; set; }
        public Joint1 chest { get; set; }
        public Joint1 neck { get; set; }
        public Joint1 head { get; set; }
        public Joint1 head_tip { get; set; }
        public Joint1 upperLeg_L { get; set; }
        public Joint1 lowerLeg_L { get; set; }
        public Joint1 foot_L { get; set; }
        public Joint1 toes_L { get; set; }
        public Joint1 upperLeg_R { get; set; }
        public Joint1 lowerLeg_R { get; set; }
        public Joint1 foot_R { get; set; }
        public Joint1 toes_R { get; set; }
        public Joint1 collar_L { get; set; }
        public Joint1 upperArm_L { get; set; }
        public Joint1 foreArm_L { get; set; }
        public Joint1 hand_L { get; set; }
        public Joint1 collar_R { get; set; }
        public Joint1 upperArm_R { get; set; }
        public Joint1 foreArm_R { get; set; }
        public Joint1 hand_R { get; set; }
        public Joint1 thumb_R { get; set; }
        public Joint1 thumb_L { get; set; }
        public Joint1 middle_R { get; set; }
        public Joint1 middle_L { get; set; }
        public BrekelJoints1()
        {
            waist = new Joint1(0, 0, 0, 0);
            spine = new Joint1(0, 0, 0, 0);
            chest = new Joint1(0, 0, 0, 0);
            neck = new Joint1(0, 0, 0, 0);
            head = new Joint1(0, 0, 0, 0);
            head_tip = new Joint1(0, 0, 0, 0);
            upperLeg_L = new Joint1(0, 0, 0, 0);    
            lowerLeg_L = new Joint1(0, 0, 0, 0);
            foot_L = new Joint1(0, 0, 0, 0);
            toes_L = new Joint1(0, 0, 0, 0);
            upperLeg_R = new Joint1(0, 0, 0, 0);
            lowerLeg_R = new Joint1(0, 0, 0, 0);
            foot_R = new Joint1(0, 0, 0, 0);
            toes_R = new Joint1(0, 0, 0, 0);
            collar_L = new Joint1(0, 0, 0, 0);
            upperArm_L = new Joint1(0, 0, 0, 0);
            foreArm_L = new Joint1(0, 0, 0, 0);
            hand_L = new Joint1(0, 0, 0, 0);
            collar_R = new Joint1(0, 0, 0, 0);
            upperArm_R = new Joint1(0, 0, 0, 0);
            foreArm_R = new Joint1(0, 0, 0, 0);
            hand_R = new Joint1(0, 0, 0, 0);
            thumb_L = new Joint1(0, 0, 0, 0);
            middle_L = new Joint1(0, 0, 0, 0);
            thumb_R = new Joint1(0, 0, 0, 0);
            middle_R = new Joint1(0, 0, 0, 0);
        }
        public BrekelJoints1(BrekelMongo prev)
        {
            //waist = new Joint1(prev.waist_tx - 45, prev.waist_ty - 60, prev.waist_tz + 200, prev.waist_conf);
            waist = new Joint1(0, 0, 0, prev.waist_conf);
            spine = new Joint1(prev.spine_tx, prev.spine_ty, prev.spine_tz, prev.spine_rx, prev.spine_ry, prev.spine_rz, prev.spine_conf, waist);
            chest = new Joint1(prev.chest_tx, prev.chest_ty, prev.chest_tz, prev.chest_rx, prev.chest_ry, prev.chest_rz, prev.chest_conf, spine);
            neck = new Joint1(prev.neck_tx, prev.neck_ty, prev.neck_tz, prev.neck_rx, prev.neck_ry, prev.neck_rz, prev.neck_conf, chest);
            head = new Joint1(prev.head_tx, prev.head_ty, prev.head_tz, prev.head_rx, prev.head_ry, prev.head_rz, prev.head_conf, neck);
            head_tip = new Joint1(prev.head_tip_tx, prev.head_tip_ty, prev.head_tip_tz, prev.head_tip_rx, prev.head_tip_ry, prev.head_tip_rz, prev.head_tip_conf, head);
            upperLeg_L = new Joint1(prev.upperLeg_L_tx, prev.upperLeg_L_ty, prev.upperLeg_L_tz, prev.upperLeg_L_rx, prev.upperLeg_L_ry, prev.upperLeg_L_rz, prev.upperLeg_L_conf, waist);
            lowerLeg_L = new Joint1(prev.lowerLeg_L_tx, prev.lowerLeg_L_ty, prev.lowerLeg_L_tz, prev.lowerLeg_L_rx, prev.lowerLeg_L_ry, prev.lowerLeg_L_rz, prev.lowerLeg_L_conf, upperLeg_L);
            foot_L = new Joint1(prev.foot_L_tx, prev.foot_L_ty, prev.foot_L_tz, prev.foot_L_rx, prev.foot_L_ry, prev.foot_L_rz, prev.foot_L_conf, lowerLeg_L);
            toes_L = new Joint1(prev.toes_L_tx, prev.toes_L_ty, prev.toes_L_tz, prev.toes_L_rx, prev.toes_L_ry, prev.toes_L_rz, prev.toes_L_conf, foot_L);
            upperLeg_R = new Joint1(prev.upperLeg_R_tx, prev.upperLeg_R_ty, prev.upperLeg_R_tz, prev.upperLeg_R_rx, prev.upperLeg_R_ry, prev.upperLeg_R_rz, prev.upperLeg_R_conf, waist);
            lowerLeg_R = new Joint1(prev.lowerLeg_R_tx, prev.lowerLeg_R_ty, prev.lowerLeg_R_tz, prev.lowerLeg_R_rx, prev.lowerLeg_R_ry, prev.lowerLeg_R_rz, prev.lowerLeg_R_conf, upperLeg_R);
            foot_R = new Joint1(prev.foot_R_tx, prev.foot_R_ty, prev.foot_R_tz, prev.foot_R_rx, prev.foot_R_ry, prev.foot_R_rz, prev.foot_R_conf, lowerLeg_R);
            toes_R = new Joint1(prev.toes_R_tx, prev.toes_R_ty, prev.toes_R_tz, prev.toes_R_rx, prev.toes_R_ry, prev.toes_R_rz, prev.toes_R_conf, foot_R);
            collar_L = new Joint1(prev.collar_L_tx, prev.collar_L_ty, prev.collar_L_tz, prev.collar_L_rx, prev.collar_L_ry, prev.collar_L_rz, prev.collar_L_conf, neck);
            upperArm_L = new Joint1(prev.upperArm_L_tx, prev.upperArm_L_ty, prev.upperArm_L_tz, prev.upperArm_L_rx, prev.upperArm_L_ry, prev.upperArm_L_rz, prev.upperArm_L_conf, collar_L);
            foreArm_L = new Joint1(prev.foreArm_L_tx, prev.foreArm_L_ty, prev.foreArm_L_tz, prev.foreArm_L_rx, prev.foreArm_L_ry, prev.foreArm_L_rz, prev.foreArm_L_conf, upperArm_L);
            hand_L = new Joint1(prev.hand_L_tx, prev.hand_L_ty, prev.hand_L_tz, prev.hand_L_rx, prev.hand_L_ry, prev.hand_L_rz, prev.hand_L_conf, foreArm_L);
            collar_R = new Joint1(prev.collar_R_tx, prev.collar_R_ty, prev.collar_R_tz, prev.collar_R_rx, prev.collar_R_ry, prev.collar_R_rz, prev.collar_R_conf, neck);
            upperArm_R = new Joint1(prev.upperArm_R_tx, prev.upperArm_R_ty, prev.upperArm_R_tz, prev.upperArm_R_rx, prev.upperArm_R_ry, prev.upperArm_R_rz, prev.upperArm_R_conf, collar_R);
            foreArm_R = new Joint1(prev.foreArm_R_tx, prev.foreArm_R_ty, prev.foreArm_R_tz, prev.foreArm_R_rx, prev.foreArm_R_ry, prev.foreArm_R_rz, prev.foreArm_R_conf, upperArm_R);
            hand_R = new Joint1(prev.hand_R_tx, prev.hand_R_ty, prev.hand_R_tz, prev.hand_R_rx, prev.hand_R_ry, prev.hand_R_rz, prev.hand_R_conf, foreArm_R);
            thumb_L = new Joint1(prev.thumb_L_0_tx, prev.thumb_L_0_ty, prev.thumb_L_0_tz, prev.thumb_L_0_rx, prev.thumb_L_0_ry, prev.thumb_L_0_rz, prev.thumb_L_0_conf, hand_L);
            middle_L = new Joint1(prev.middle_L_0_tx, prev.middle_L_0_ty, prev.middle_L_0_tz, prev.middle_L_0_rx, prev.middle_L_0_ry, prev.middle_L_0_rz, prev.middle_L_0_conf, hand_L);
            thumb_R = new Joint1(prev.thumb_R_0_tx, prev.thumb_R_0_ty, prev.thumb_R_0_tz, prev.thumb_R_0_rx, prev.thumb_R_0_ry, prev.thumb_R_0_rz, prev.thumb_R_0_conf, hand_R);
            middle_R = new Joint1(prev.middle_R_0_tx, prev.middle_R_0_ty, prev.middle_R_0_tz, prev.middle_R_0_rx, prev.middle_R_0_ry, prev.middle_R_0_rz, prev.middle_R_0_conf, hand_R);
        }
        public List<Joint1> getList()
        {
            List<Joint1> list = new List<Joint1>();
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
    public class Joint1
    {
        public double posX { get; set; }
        public double posY { get; set; }
        public double posZ { get; set; }
        public double[,] rotM { get; set; }
        public double trackingState { get; set; }
        public Joint1(double x, double y, double z, double track)
        {
            posX = x;
            posY = y;
            posZ = z;
            rotM = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            trackingState = track;
        }
        public Joint1(double x, double y, double z, double rx, double ry, double rz, double track)
        {
            posX = x;
            posY = y;
            posZ = z;
            rotM = transM(rx, ry, rz);
            trackingState = track;
        }
        /*
        public Joint1(double x, double y, double z, double rx, double ry, double rz, double track,Joint1 prev)
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

        public Joint1(double x, double y, double z, double rx, double ry, double rz, double track, Joint1 prev)
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
            /*
            trM[0, 0] = Math.Sin(rx * conv) * Math.Sin(ry * conv) * Math.Sin(rz * conv) + Math.Cos(ry * conv) * Math.Cos(rz * conv);
            trM[0, 1] = Math.Sin(rx * conv) * Math.Sin(ry * conv) * Math.Cos(rz * conv) - Math.Cos(ry * conv) * Math.Sin(rz * conv);
            trM[0, 2] = Math.Cos(rx * conv) * Math.Sin(ry * conv);
            trM[1, 0] = Math.Cos(rx * conv) * Math.Sin(rz * conv);
            trM[1, 1] = Math.Cos(rx * conv) * Math.Cos(rz * conv);
            trM[1, 2] = -Math.Sin(rx * conv);
            trM[2, 0] = Math.Sin(rx * conv) * Math.Cos(ry * conv) * Math.Sin(rz * conv) - Math.Sin(ry * conv) * Math.Cos(rz * conv);
            trM[2, 1] = Math.Sin(rx * conv) * Math.Cos(ry * conv) * Math.Cos(rz * conv) + Math.Sin(ry * conv) * Math.Sin(rz * conv);
            trM[2, 2] = Math.Cos(rx * conv) * Math.Cos(ry * conv);
            */
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
