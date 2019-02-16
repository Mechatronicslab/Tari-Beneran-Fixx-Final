using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f_userCamera
{
    class KinectDPTParser
    {
        public MongoJoint rawData { get; set; }
        public int[] observedStates;
        public double[] rawobserves;

        public int[] parsefromKinectRow(MongoJoint data, string mode)
        {
            MongoJoint transformed = new MongoJoint();
            rawData = data;
            if (mode == "HIERARCHIC")
            {
                transformed = transformtoHIECoordinate(data);
            }
            else if (mode == "BODY CENTER")
            {
                transformed = transformtoWaistCoordinate(data);
            }
            else
            {
                return null;
            }
            transformed = normalizeTranslate(transformed);
            observedStates = getObserveState(transformed);
            rawobserves = getrawObserveState(transformed);
            return observedStates;
        }

        private MongoJoint transformtoWaistCoordinate(MongoJoint prev)
        {
            double[] refer = new double[] { prev.waist_tx, prev.waist_ty, prev.waist_tz };
            MongoJoint trfed = prev;
            trfed.spine_tx = transformCoordinate(refer[0], trfed.spine_tx);
            trfed.spine_ty = transformCoordinate(refer[1], trfed.spine_ty);
            trfed.spine_tz = transformCoordinate(refer[2], trfed.spine_tz);
            trfed.chest_tx = transformCoordinate(refer[0], trfed.chest_tx);
            trfed.chest_ty = transformCoordinate(refer[1], trfed.chest_ty);
            trfed.chest_tz = transformCoordinate(refer[2], trfed.chest_tz);
            trfed.neck_tx = transformCoordinate(refer[0], trfed.neck_tx);
            trfed.neck_ty = transformCoordinate(refer[1], trfed.neck_ty);
            trfed.neck_tz = transformCoordinate(refer[2], trfed.neck_tz);
            trfed.head_tx = transformCoordinate(refer[0], trfed.head_tx);
            trfed.head_ty = transformCoordinate(refer[1], trfed.head_ty);
            trfed.head_tz = transformCoordinate(refer[2], trfed.head_tz);
            trfed.upperLeg_L_tx = transformCoordinate(refer[0], trfed.upperLeg_L_tx);
            trfed.upperLeg_L_ty = transformCoordinate(refer[1], trfed.upperLeg_L_ty);
            trfed.upperLeg_L_tz = transformCoordinate(refer[2], trfed.upperLeg_L_tz);
            trfed.lowerLeg_L_tx = transformCoordinate(refer[0], trfed.lowerLeg_L_tx);
            trfed.lowerLeg_L_ty = transformCoordinate(refer[1], trfed.lowerLeg_L_ty);
            trfed.lowerLeg_L_tz = transformCoordinate(refer[2], trfed.lowerLeg_L_tz);
            trfed.foot_L_tx = transformCoordinate(refer[0], trfed.foot_L_tx);
            trfed.foot_L_ty = transformCoordinate(refer[1], trfed.foot_L_ty);
            trfed.foot_L_tz = transformCoordinate(refer[2], trfed.foot_L_tz);
            trfed.toes_L_tx = transformCoordinate(refer[0], trfed.toes_L_tx);
            trfed.toes_L_ty = transformCoordinate(refer[1], trfed.toes_L_ty);
            trfed.toes_L_tz = transformCoordinate(refer[2], trfed.toes_L_tz);
            trfed.upperLeg_R_tx = transformCoordinate(refer[0], trfed.upperLeg_R_tx);
            trfed.upperLeg_R_ty = transformCoordinate(refer[1], trfed.upperLeg_R_ty);
            trfed.upperLeg_R_tz = transformCoordinate(refer[2], trfed.upperLeg_R_tz);
            trfed.lowerLeg_R_tx = transformCoordinate(refer[0], trfed.lowerLeg_R_tx);
            trfed.lowerLeg_R_ty = transformCoordinate(refer[1], trfed.lowerLeg_R_ty);
            trfed.lowerLeg_R_tz = transformCoordinate(refer[2], trfed.lowerLeg_R_tz);
            trfed.foot_R_tx = transformCoordinate(refer[0], trfed.foot_R_tx);
            trfed.foot_R_ty = transformCoordinate(refer[1], trfed.foot_R_ty);
            trfed.foot_R_tz = transformCoordinate(refer[2], trfed.foot_R_tz);
            trfed.toes_R_tx = transformCoordinate(refer[0], trfed.toes_R_tx);
            trfed.toes_R_ty = transformCoordinate(refer[1], trfed.toes_R_ty);
            trfed.toes_R_tz = transformCoordinate(refer[2], trfed.toes_R_tz);
            trfed.collar_L_tx = transformCoordinate(refer[0], trfed.collar_L_tx);
            trfed.collar_L_ty = transformCoordinate(refer[1], trfed.collar_L_ty);
            trfed.collar_L_tz = transformCoordinate(refer[2], trfed.collar_L_tz);
            trfed.upperArm_L_tx = transformCoordinate(refer[0], trfed.upperArm_L_tx);
            trfed.upperArm_L_ty = transformCoordinate(refer[1], trfed.upperArm_L_ty);
            trfed.upperArm_L_tz = transformCoordinate(refer[2], trfed.upperArm_L_tz);
            trfed.foreArm_L_tx = transformCoordinate(refer[0], trfed.foreArm_L_tx);
            trfed.foreArm_L_ty = transformCoordinate(refer[1], trfed.foreArm_L_ty);
            trfed.foreArm_L_tz = transformCoordinate(refer[2], trfed.foreArm_L_tz);
            trfed.hand_L_tx = transformCoordinate(refer[0], trfed.hand_L_tx);
            trfed.hand_L_ty = transformCoordinate(refer[1], trfed.hand_L_ty);
            trfed.hand_L_tz = transformCoordinate(refer[2], trfed.hand_L_tz);
            trfed.collar_R_tx = transformCoordinate(refer[0], trfed.collar_R_tx);
            trfed.collar_R_ty = transformCoordinate(refer[1], trfed.collar_R_ty);
            trfed.collar_R_tz = transformCoordinate(refer[2], trfed.collar_R_tz);
            trfed.upperArm_R_tx = transformCoordinate(refer[0], trfed.upperArm_R_tx);
            trfed.upperArm_R_ty = transformCoordinate(refer[1], trfed.upperArm_R_ty);
            trfed.upperArm_R_tz = transformCoordinate(refer[2], trfed.upperArm_R_tz);
            trfed.foreArm_R_tx = transformCoordinate(refer[0], trfed.foreArm_R_tx);
            trfed.foreArm_R_ty = transformCoordinate(refer[1], trfed.foreArm_R_ty);
            trfed.foreArm_R_tz = transformCoordinate(refer[2], trfed.foreArm_R_tz);
            trfed.hand_R_tx = transformCoordinate(refer[0], trfed.hand_R_tx);
            trfed.hand_R_ty = transformCoordinate(refer[1], trfed.hand_R_ty);
            trfed.hand_R_tz = transformCoordinate(refer[2], trfed.hand_R_tz);
            return trfed;
        }

        private MongoJoint transformtoHIECoordinate(MongoJoint prev)
        {
            MongoJoint trfed = new MongoJoint();
            trfed.hand_R_tx = transformCoordinate(prev.collar_R_tx, prev.hand_R_tx);
            trfed.hand_R_ty = transformCoordinate(prev.collar_R_ty, prev.hand_R_ty);
            trfed.hand_R_tz = transformCoordinate(prev.collar_R_tz, prev.hand_R_tz);
            trfed.spine_tx = transformCoordinate(prev.waist_tx, prev.spine_tx);
            trfed.spine_ty = transformCoordinate(prev.waist_ty, prev.spine_ty);
            trfed.spine_tz = transformCoordinate(prev.waist_tz, prev.spine_tz);
            trfed.chest_tx = transformCoordinate(prev.spine_tx, prev.chest_tx);
            trfed.chest_ty = transformCoordinate(prev.spine_ty, prev.chest_ty);
            trfed.chest_tz = transformCoordinate(prev.spine_tz, prev.chest_tz);
            trfed.neck_tx = transformCoordinate(prev.chest_tx, prev.neck_tx);
            trfed.neck_ty = transformCoordinate(prev.chest_ty, prev.neck_ty);
            trfed.neck_tz = transformCoordinate(prev.chest_tz, prev.neck_tz);
            trfed.head_tx = transformCoordinate(prev.neck_tx, prev.head_tx);
            trfed.head_ty = transformCoordinate(prev.neck_ty, prev.head_ty);
            trfed.head_tz = transformCoordinate(prev.neck_tz, prev.head_tz);
            trfed.upperLeg_L_tx = transformCoordinate(prev.waist_tx, prev.upperLeg_L_tx);
            trfed.upperLeg_L_ty = transformCoordinate(prev.waist_ty, prev.upperLeg_L_ty);
            trfed.upperLeg_L_tz = transformCoordinate(prev.waist_tz, prev.upperLeg_L_tz);
            trfed.lowerLeg_L_tx = transformCoordinate(prev.upperLeg_L_tx, prev.lowerLeg_L_tx);
            trfed.lowerLeg_L_ty = transformCoordinate(prev.upperLeg_L_ty, prev.lowerLeg_L_ty);
            trfed.lowerLeg_L_tz = transformCoordinate(prev.upperLeg_L_tz, prev.lowerLeg_L_tz);
            trfed.foot_L_tx = transformCoordinate(prev.lowerLeg_L_tx, prev.foot_L_tx);
            trfed.foot_L_ty = transformCoordinate(prev.lowerLeg_L_ty, prev.foot_L_ty);
            trfed.foot_L_tz = transformCoordinate(prev.lowerLeg_L_tz, prev.foot_L_tz);
            trfed.toes_L_tx = transformCoordinate(prev.foot_L_tx, prev.toes_L_tx);
            trfed.toes_L_ty = transformCoordinate(prev.foot_L_ty, prev.toes_L_ty);
            trfed.toes_L_tz = transformCoordinate(prev.foot_L_tz, prev.toes_L_tz);
            trfed.upperLeg_R_tx = transformCoordinate(prev.waist_tx, prev.upperLeg_R_tx);
            trfed.upperLeg_R_ty = transformCoordinate(prev.waist_ty, prev.upperLeg_R_ty);
            trfed.upperLeg_R_tz = transformCoordinate(prev.waist_tz, prev.upperLeg_R_tz);
            trfed.lowerLeg_R_tx = transformCoordinate(prev.upperLeg_R_tx, prev.lowerLeg_R_tx);
            trfed.lowerLeg_R_ty = transformCoordinate(prev.upperLeg_R_ty, prev.lowerLeg_R_ty);
            trfed.lowerLeg_R_tz = transformCoordinate(prev.upperLeg_R_tz, prev.lowerLeg_R_tz);
            trfed.foot_R_tx = transformCoordinate(prev.lowerLeg_R_tx, prev.foot_R_tx);
            trfed.foot_R_ty = transformCoordinate(prev.lowerLeg_R_ty, prev.foot_R_ty);
            trfed.foot_R_tz = transformCoordinate(prev.lowerLeg_R_tz, prev.foot_R_tz);
            trfed.toes_R_tx = transformCoordinate(prev.foot_R_tx, prev.toes_R_tx);
            trfed.toes_R_ty = transformCoordinate(prev.foot_R_ty, prev.toes_R_ty);
            trfed.toes_R_tz = transformCoordinate(prev.foot_R_tz, prev.toes_R_tz);
            trfed.collar_L_tx = transformCoordinate(prev.foreArm_L_tx, prev.collar_L_tx);
            trfed.collar_L_ty = transformCoordinate(prev.foreArm_L_ty, prev.collar_L_ty);
            trfed.collar_L_tz = transformCoordinate(prev.foreArm_L_tz, prev.collar_L_tz);
            trfed.upperArm_L_tx = transformCoordinate(prev.chest_tx, prev.upperArm_L_tx);
            trfed.upperArm_L_ty = transformCoordinate(prev.chest_ty, prev.upperArm_L_ty);
            trfed.upperArm_L_tz = transformCoordinate(prev.chest_tz, prev.upperArm_L_tz);
            trfed.foreArm_L_tx = transformCoordinate(prev.upperArm_L_tx, prev.foreArm_L_tx);
            trfed.foreArm_L_ty = transformCoordinate(prev.upperArm_L_ty, prev.foreArm_L_ty);
            trfed.foreArm_L_tz = transformCoordinate(prev.upperArm_L_tz, prev.foreArm_L_tz);
            trfed.hand_L_tx = transformCoordinate(prev.collar_L_tx, prev.hand_L_tx);
            trfed.hand_L_ty = transformCoordinate(prev.collar_L_ty, prev.hand_L_ty);
            trfed.hand_L_tz = transformCoordinate(prev.collar_L_tz, prev.hand_L_tz);
            trfed.collar_R_tx = transformCoordinate(prev.foreArm_R_tx, prev.collar_R_tx);
            trfed.collar_R_ty = transformCoordinate(prev.foreArm_R_ty, prev.collar_R_ty);
            trfed.collar_R_tz = transformCoordinate(prev.foreArm_R_tz, prev.collar_R_tz);
            trfed.upperArm_R_tx = transformCoordinate(prev.chest_tx, prev.upperArm_R_tx);
            trfed.upperArm_R_ty = transformCoordinate(prev.chest_ty, prev.upperArm_R_ty);
            trfed.upperArm_R_tz = transformCoordinate(prev.chest_tz, prev.upperArm_R_tz);
            trfed.foreArm_R_tx = transformCoordinate(prev.upperArm_R_tx, prev.foreArm_R_tx);
            trfed.foreArm_R_ty = transformCoordinate(prev.upperArm_R_ty, prev.foreArm_R_ty);
            trfed.foreArm_R_tz = transformCoordinate(prev.upperArm_R_tz, prev.foreArm_R_tz);
            return trfed;
        }

        public int[] getObserveState(MongoJoint row)
        {
            /*Before
            int size_obv = 3 * 5;
            int[] observe_rw = new int[size_obv];
            observe_rw[0] = translationToObservedState(row.collar_L_tx);
            observe_rw[1] = translationToObservedState(row.collar_L_ty);
            observe_rw[2] = translationToObservedState(row.collar_L_tz);
            observe_rw[3] = translationToObservedState(row.foreArm_L_tx);
            observe_rw[4] = translationToObservedState(row.foreArm_L_ty);
            observe_rw[5] = translationToObservedState(row.foreArm_L_tz);
            observe_rw[6] = translationToObservedState(row.collar_R_tx);
            observe_rw[7] = translationToObservedState(row.collar_R_ty);
            observe_rw[8] = translationToObservedState(row.collar_R_tz);
            observe_rw[9] = translationToObservedState(row.foreArm_R_tx);
            observe_rw[10] = translationToObservedState(row.foreArm_R_ty);
            observe_rw[11] = translationToObservedState(row.foreArm_R_tz);
            observe_rw[12] = translationToObservedState(row.head_tx);
            observe_rw[13] = translationToObservedState(row.head_ty);
            observe_rw[14] = translationToObservedState(row.head_tz);
            return observe_rw;
            */
            int size_obv = 3 * 6;
            int[] observe_rw = new int[size_obv];
            observe_rw[0] = translationToObservedState(row.upperArm_L_tx);
            observe_rw[1] = translationToObservedState(row.upperArm_L_ty);
            observe_rw[2] = translationToObservedState(row.upperArm_L_tz);
            observe_rw[3] = translationToObservedState(row.foreArm_L_tx);
            observe_rw[4] = translationToObservedState(row.foreArm_L_ty);
            observe_rw[5] = translationToObservedState(row.foreArm_L_tz);

            observe_rw[6] = translationToObservedState(row.upperArm_R_tx);
            observe_rw[7] = translationToObservedState(row.upperArm_R_ty);
            observe_rw[8] = translationToObservedState(row.upperArm_R_tz);
            observe_rw[9] = translationToObservedState(row.foreArm_R_tx);
            observe_rw[10] = translationToObservedState(row.foreArm_R_ty);
            observe_rw[11] = translationToObservedState(row.foreArm_R_tz);

            observe_rw[12] = translationToObservedState(row.hand_R_tx);
            observe_rw[13] = translationToObservedState(row.hand_R_ty);
            observe_rw[14] = translationToObservedState(row.hand_R_tz);
            observe_rw[15] = translationToObservedState(row.hand_L_tx);
            observe_rw[16] = translationToObservedState(row.hand_L_ty);
            observe_rw[17] = translationToObservedState(row.hand_L_tz);
            //Somehow in brekel everything is mirrorred (should include waist rotation) so everything is minus, except y coord which has been mirrored beforehand
            return observe_rw;
        }

        public double[] getrawObserveState(MongoJoint row)
        {
            //int size_obv = 3 * 6;
            int size_obv = 3;
            double[] observe_rw = new double[size_obv];
            //Put all selected data
            /*
            observe_rw[0] = row.foreArm_L_rx;
            observe_rw[1] = row.foreArm_L_ry;
            observe_rw[2] = row.foreArm_L_rz;
            observe_rw[3] = row.foreArm_R_rx;
            observe_rw[4] = row.foreArm_R_ry;
            observe_rw[5] = row.foreArm_R_rz;
            observe_rw[6] = row.hand_L_rx;
            observe_rw[7] = row.hand_L_ry;
            observe_rw[8] = row.hand_L_rz;
            observe_rw[9] = row.hand_R_rx;
            observe_rw[10] = row.hand_R_ry;
            observe_rw[11] = row.hand_R_rz;
            observe_rw[12] = row.collar_L_rx;
            observe_rw[13] = row.collar_L_ry;
            observe_rw[14] = row.collar_L_rz;
            observe_rw[15] = row.collar_R_rx;
            observe_rw[16] = row.collar_R_ry;
            observe_rw[17] = row.collar_R_rz;
            
             */
            observe_rw[0] = row.collar_L_tx;
            observe_rw[1] = row.collar_L_ty;
            observe_rw[2] = row.collar_L_tz;
            /*
            observe_rw[3] = row.foreArm_R_tx;
            observe_rw[4] = row.foreArm_R_ty;
            observe_rw[5] = row.foreArm_R_tz;
            observe_rw[6] = row.hand_L_tx;
            observe_rw[7] = row.hand_L_ty;
            observe_rw[8] = row.hand_L_tz;
            observe_rw[9] = row.hand_R_tx;
            observe_rw[10] = row.hand_R_ty;
            observe_rw[11] = row.hand_R_tz;
            observe_rw[12] = row.collar_L_tx;
            observe_rw[13] = row.collar_L_ty;
            observe_rw[14] = row.collar_L_tz;
            observe_rw[15] = row.collar_R_tx;
            observe_rw[16] = row.collar_R_ty;
            observe_rw[17] = row.collar_R_tz;
            
            */
            return observe_rw;
        }

        private int rotationToObservedState(double rot)
        {
            /* PREV
            int state = (int)Math.Round((rot * 1));
            //int state = (int)Math.Round((rot *0.1));
            if (state < 0)
            {
                state = 0;
            }
            if (state > 380)
            {
                state = 0;
            }
            return state;
            */
            int state;
            if (rot > 90 || rot < -90)
            {
                state = 3;
            }
            else if (rot < -15)
            {
                state = 0;
            }
            else if (rot > 15)
            {
                state = 2;
            }
            else
            {
                state = 1;
            }
            return state;
        }

        private int translationToObservedState(double trans)
        {
            /*
            int state;
            if (trans > 0.3)
            {
                state = 2;
            }
            else if (trans < -0.3)
            {
                state = 0;
            }
            else
            {
                state = 1;
            }
            return state;
            */
            int state;
            if (trans < -0.7)
            {
                state = 0;
            }
            else if (trans < -0.25)
            {
                state = 1;
            }
            else if (trans < 0.25)
            {
                state = 2;
            }
            else if (trans < 0.7)
            {
                state = 3;
            }
            else
            {
                state = 4;
            }
            return state;
        }

        private double transformCoordinate(double refer, double trans)
        {
            double newer = trans - refer;
            return newer;
        }

        private MongoJoint normalizeTranslate(MongoJoint joint)
        {
            MongoJoint trfed = joint;
            double buff = Math.Sqrt(trfed.hand_R_tx * trfed.hand_R_tx + trfed.hand_R_ty * trfed.hand_R_ty + trfed.hand_R_tz * trfed.hand_R_tz);
            trfed.hand_R_tx = trfed.hand_R_tx / buff;
            trfed.hand_R_ty = trfed.hand_R_ty / buff;
            trfed.hand_R_tz = trfed.hand_R_tz / buff;
            buff = Math.Sqrt(trfed.spine_tx * trfed.spine_tx + trfed.spine_ty * trfed.spine_ty + trfed.spine_tz * trfed.spine_tz);
            trfed.spine_tx = trfed.spine_tx / buff;
            trfed.spine_ty = trfed.spine_ty / buff;
            trfed.spine_tz = trfed.spine_tz / buff;
            buff = Math.Sqrt(trfed.chest_tx * trfed.chest_tx + trfed.chest_ty * trfed.chest_ty + trfed.chest_tz * trfed.chest_tz);
            trfed.chest_tx = trfed.chest_tx / buff;
            trfed.chest_ty = trfed.chest_ty / buff;
            trfed.chest_tz = trfed.chest_tz / buff;
            buff = Math.Sqrt(trfed.neck_tx * trfed.neck_tx + trfed.neck_ty * trfed.neck_ty + trfed.neck_tz * trfed.neck_tz);
            trfed.neck_tx = trfed.neck_tx / buff;
            trfed.neck_ty = trfed.neck_ty / buff;
            trfed.neck_tz = trfed.neck_tz / buff;
            buff = Math.Sqrt(trfed.head_tx * trfed.head_tx + trfed.head_ty * trfed.head_ty + trfed.head_tz * trfed.head_tz);
            trfed.head_tx = trfed.head_tx / buff;
            trfed.head_ty = trfed.head_ty / buff;
            trfed.head_tz = trfed.head_tz / buff;
            buff = Math.Sqrt(trfed.upperLeg_L_tx * trfed.upperLeg_L_tx + trfed.upperLeg_L_ty * trfed.upperLeg_L_ty + trfed.upperLeg_L_tz * trfed.upperLeg_L_tz);
            trfed.upperLeg_L_tx = trfed.upperLeg_L_tx / buff;
            trfed.upperLeg_L_ty = trfed.upperLeg_L_ty / buff;
            trfed.upperLeg_L_tz = trfed.upperLeg_L_tz / buff;
            buff = Math.Sqrt(trfed.lowerLeg_L_tx * trfed.lowerLeg_L_tx + trfed.lowerLeg_L_ty * trfed.lowerLeg_L_ty + trfed.lowerLeg_L_tz * trfed.lowerLeg_L_tz);
            trfed.lowerLeg_L_tx = trfed.lowerLeg_L_tx / buff;
            trfed.lowerLeg_L_ty = trfed.lowerLeg_L_ty / buff;
            trfed.lowerLeg_L_tz = trfed.lowerLeg_L_tz / buff;
            buff = Math.Sqrt(trfed.foot_L_tx * trfed.foot_L_tx + trfed.foot_L_ty * trfed.foot_L_ty + trfed.foot_L_tz * trfed.foot_L_tz);
            trfed.foot_L_tx = trfed.foot_L_tx / buff;
            trfed.foot_L_ty = trfed.foot_L_ty / buff;
            trfed.foot_L_tz = trfed.foot_L_tz / buff;
            buff = Math.Sqrt(trfed.toes_L_tx * trfed.toes_L_tx + trfed.toes_L_ty * trfed.toes_L_ty + trfed.toes_L_tz * trfed.toes_L_tz);
            trfed.toes_L_tx = trfed.toes_L_tx / buff;
            trfed.toes_L_ty = trfed.toes_L_ty / buff;
            trfed.toes_L_tz = trfed.toes_L_tz / buff;
            buff = Math.Sqrt(trfed.upperLeg_R_tx * trfed.upperLeg_R_tx + trfed.upperLeg_R_ty * trfed.upperLeg_R_ty + trfed.upperLeg_R_tz * trfed.upperLeg_R_tz);
            trfed.upperLeg_R_tx = trfed.upperLeg_R_tx / buff;
            trfed.upperLeg_R_ty = trfed.upperLeg_R_ty / buff;
            trfed.upperLeg_R_tz = trfed.upperLeg_R_tz / buff;
            buff = Math.Sqrt(trfed.lowerLeg_L_tx + trfed.lowerLeg_L_tx + trfed.lowerLeg_L_ty * trfed.lowerLeg_L_ty + trfed.lowerLeg_L_tz * trfed.lowerLeg_L_tz);
            trfed.lowerLeg_R_tx = trfed.lowerLeg_L_tx / buff;
            trfed.lowerLeg_R_ty = trfed.lowerLeg_L_ty / buff;
            trfed.lowerLeg_R_tz = trfed.lowerLeg_L_tz / buff;
            buff = Math.Sqrt(trfed.foot_R_tx * trfed.foot_R_tx + trfed.foot_R_ty * trfed.foot_R_ty + trfed.foot_R_tz * trfed.foot_R_tz);
            trfed.foot_R_tx = trfed.foot_R_tx / buff;
            trfed.foot_R_ty = trfed.foot_R_ty / buff;
            trfed.foot_R_tz = trfed.foot_R_tz / buff;
            buff = Math.Sqrt(trfed.toes_R_tx * trfed.toes_R_tx + trfed.toes_R_ty * trfed.toes_R_ty + trfed.toes_R_tz * trfed.toes_R_tz);
            trfed.toes_R_tx = trfed.toes_R_tx / buff;
            trfed.toes_R_ty = trfed.toes_R_ty / buff;
            trfed.toes_R_tz = trfed.toes_R_tz / buff;
            buff = Math.Sqrt(trfed.collar_L_tx * trfed.collar_L_tx + trfed.collar_L_ty * trfed.collar_L_ty + trfed.collar_L_tz * trfed.collar_L_tz);
            trfed.collar_L_tx = trfed.collar_L_tx / buff;
            trfed.collar_L_ty = trfed.collar_L_ty / buff;
            trfed.collar_L_tz = trfed.collar_L_tz / buff;
            buff = Math.Sqrt(trfed.upperArm_L_tx * trfed.upperArm_L_tx + trfed.upperArm_L_ty * trfed.upperArm_L_ty + trfed.upperArm_L_tz * trfed.upperArm_L_tz);
            trfed.upperArm_L_tx = trfed.upperArm_L_tx / buff;
            trfed.upperArm_L_ty = trfed.upperArm_L_ty / buff;
            trfed.upperArm_L_tz = trfed.upperArm_L_tz / buff;
            buff = Math.Sqrt(trfed.foreArm_L_tx * trfed.foreArm_L_tx + trfed.foreArm_L_ty * trfed.foreArm_L_ty + trfed.foreArm_L_tz * trfed.foreArm_L_tz);
            trfed.foreArm_L_tx = trfed.foreArm_L_tx / buff;
            trfed.foreArm_L_ty = trfed.foreArm_L_ty / buff;
            trfed.foreArm_L_tz = trfed.foreArm_L_tz / buff;
            buff = Math.Sqrt(trfed.hand_L_tx * trfed.hand_L_tx + trfed.hand_L_ty * trfed.hand_L_ty + trfed.hand_L_tz * trfed.hand_L_tz);
            trfed.hand_L_tx = trfed.hand_L_tx / buff;
            trfed.hand_L_ty = trfed.hand_L_ty / buff;
            trfed.hand_L_tz = trfed.hand_L_tz / buff;
            buff = Math.Sqrt(trfed.collar_R_tx * trfed.collar_R_tx + trfed.collar_R_ty * trfed.collar_R_ty + trfed.collar_R_tz * trfed.collar_R_tz);
            trfed.collar_R_tx = trfed.collar_R_tx / buff;
            trfed.collar_R_ty = trfed.collar_R_ty / buff;
            trfed.collar_R_tz = trfed.collar_R_tz / buff;
            buff = Math.Sqrt(trfed.upperArm_R_tx * trfed.upperArm_R_tx + trfed.upperArm_R_ty * trfed.upperArm_R_ty + trfed.upperArm_R_tz * trfed.upperArm_R_tz);
            trfed.upperArm_R_tx = trfed.upperArm_R_tx / buff;
            trfed.upperArm_R_ty = trfed.upperArm_R_ty / buff;
            trfed.upperArm_R_tz = trfed.upperArm_R_tz / buff;
            buff = Math.Sqrt(trfed.foreArm_R_tx * trfed.foreArm_R_tx + trfed.foreArm_R_ty * trfed.foreArm_R_ty + trfed.foreArm_R_tz * trfed.foreArm_R_tz);
            trfed.foreArm_R_tx = trfed.foreArm_R_tx / buff;
            trfed.foreArm_R_ty = trfed.foreArm_R_ty / buff;
            trfed.foreArm_R_tz = trfed.foreArm_R_tz / buff;
            return trfed;
        }
    }
}
