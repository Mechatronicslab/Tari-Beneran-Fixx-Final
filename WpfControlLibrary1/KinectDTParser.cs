using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlLibrary1
{
    class KinectDTParser
    {
        public MongoJoint rawData { get; set; }
        public BrekelMongo rawDataB { get; set; }
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
            //rawobserves = getrawObserveState(data);
            rawobserves = getrawObserveState(transformed);
            transformed = normalizeTranslate(transformed);
            observedStates = getObserveState(transformed);

            return observedStates;
        }
        public int[] parsefromBrekelRow(BrekelMongo data)
        {
            BrekelMongo transformed = new BrekelMongo();
            rawDataB = data;
            transformed = skeleTrans(data);
            transformed = transformtoHIECoordinate(transformed);
            rawobserves = getrawObserveState(transformed);
            transformed = normalizeTranslate(transformed);
            observedStates = getObserveState(transformed);

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
        private BrekelMongo transformtoHIECoordinate(BrekelMongo prev)
        {
            BrekelMongo trfed = new BrekelMongo();
            trfed.hand_R_tx = transformCoordinate(prev.foreArm_R_tx, prev.hand_R_tx);
            trfed.hand_R_ty = transformCoordinate(prev.foreArm_R_ty, prev.hand_R_ty);
            trfed.hand_R_tz = transformCoordinate(prev.foreArm_R_tz, prev.hand_R_tz);
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
            trfed.collar_L_tx = transformCoordinate(prev.chest_tx, prev.collar_L_tx);
            trfed.collar_L_ty = transformCoordinate(prev.chest_ty, prev.collar_L_ty);
            trfed.collar_L_tz = transformCoordinate(prev.chest_tz, prev.collar_L_tz);
            trfed.upperArm_L_tx = transformCoordinate(prev.collar_L_tx, prev.upperArm_L_tx);
            trfed.upperArm_L_ty = transformCoordinate(prev.collar_L_ty, prev.upperArm_L_ty);
            trfed.upperArm_L_tz = transformCoordinate(prev.collar_L_tz, prev.upperArm_L_tz);
            trfed.foreArm_L_tx = transformCoordinate(prev.upperArm_L_tx, prev.foreArm_L_tx);
            trfed.foreArm_L_ty = transformCoordinate(prev.upperArm_L_ty, prev.foreArm_L_ty);
            trfed.foreArm_L_tz = transformCoordinate(prev.upperArm_L_tz, prev.foreArm_L_tz);
            trfed.hand_L_tx = transformCoordinate(prev.foreArm_L_tx, prev.hand_L_tx);
            trfed.hand_L_ty = transformCoordinate(prev.foreArm_L_ty, prev.hand_L_ty);
            trfed.hand_L_tz = transformCoordinate(prev.foreArm_L_tz, prev.hand_L_tz);
            trfed.collar_R_tx = transformCoordinate(prev.chest_tx, prev.collar_R_tx);
            trfed.collar_R_ty = transformCoordinate(prev.chest_ty, prev.collar_R_ty);
            trfed.collar_R_tz = transformCoordinate(prev.chest_tz, prev.collar_R_tz);
            trfed.upperArm_R_tx = transformCoordinate(prev.collar_R_tx, prev.upperArm_R_tx);
            trfed.upperArm_R_ty = transformCoordinate(prev.collar_R_ty, prev.upperArm_R_ty);
            trfed.upperArm_R_tz = transformCoordinate(prev.collar_R_tz, prev.upperArm_R_tz);
            trfed.foreArm_R_tx = transformCoordinate(prev.upperArm_R_tx, prev.foreArm_R_tx);
            trfed.foreArm_R_ty = transformCoordinate(prev.upperArm_R_ty, prev.foreArm_R_ty);
            trfed.foreArm_R_tz = transformCoordinate(prev.upperArm_R_tz, prev.foreArm_R_tz);
            return trfed;
        }
        public int[] getObserveState(MongoJoint row)
        {
            //int size_obv = 3 * 6;
            int size_obv = 3 * 6;
            int[] observe_rw = new int[size_obv];
            //Put all selected data
            /*
            observe_rw[0] = rotationToObservedState(row.upperArm_L_rx);
            observe_rw[1] = rotationToObservedState(row.upperArm_L_ry);
            observe_rw[2] = rotationToObservedState(row.upperArm_L_rz);
            observe_rw[3] = rotationToObservedState(row.collar_L_rx);
            observe_rw[4] = rotationToObservedState(row.collar_L_ry);
            observe_rw[5] = rotationToObservedState(row.collar_L_rz);
            observe_rw[6] = rotationToObservedState(row.upperArm_R_rx);
            observe_rw[7] = rotationToObservedState(row.upperArm_R_ry);
            observe_rw[8] = rotationToObservedState(row.upperArm_R_rz);
            observe_rw[9] = rotationToObservedState(row.collar_R_rx);
            observe_rw[10] = rotationToObservedState(row.collar_R_ry);
            observe_rw[11] = rotationToObservedState(row.collar_R_rz);
            
            observe_rw[12] = rotationToObservedState(row.hand_R_rx);
            observe_rw[13] = rotationToObservedState(row.hand_R_ry);
            observe_rw[14] = rotationToObservedState(row.hand_R_rz);
            observe_rw[15] = rotationToObservedState(row.hand_L_rx);
            observe_rw[16] = rotationToObservedState(row.hand_L_ry);
            observe_rw[17] = rotationToObservedState(row.hand_L_rz);
            */


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
            observe_rw[12] = translationToObservedState(row.collar_R_tx);
            observe_rw[13] = translationToObservedState(row.collar_R_ty);
            observe_rw[14] = translationToObservedState(row.collar_R_tz);
            observe_rw[15] = translationToObservedState(row.collar_L_tx);
            observe_rw[16] = translationToObservedState(row.collar_L_ty);
            observe_rw[17] = translationToObservedState(row.collar_L_tz);
            return observe_rw;
        }
        public int[] getObserveState(BrekelMongo row)
        {
            int size_obv = 3 * 6;
            int[] observe_rw = new int[size_obv];
            /*
            observe_rw[0] = rotationToObservedState(row.collar_L_rx);
            observe_rw[1] = rotationToObservedState(row.collar_L_ry);
            observe_rw[2] = rotationToObservedState(row.collar_L_rz);
            observe_rw[3] = rotationToObservedState(row.foreArm_L_rx);
            observe_rw[4] = rotationToObservedState(row.foreArm_L_ry);
            observe_rw[5] = rotationToObservedState(row.foreArm_L_rz);
            observe_rw[6] = rotationToObservedState(row.collar_R_rx);
            observe_rw[7] = rotationToObservedState(row.collar_R_ry);
            observe_rw[8] = rotationToObservedState(row.collar_R_rz);
            observe_rw[9] = rotationToObservedState(row.foreArm_R_rx);
            observe_rw[10] = rotationToObservedState(row.foreArm_R_ry);
            observe_rw[11] = rotationToObservedState(row.foreArm_R_rz);
            observe_rw[12] = rotationToObservedState(row.hand_R_rx);
            observe_rw[13] = rotationToObservedState(row.hand_R_ry);
            observe_rw[14] = rotationToObservedState(row.hand_R_rz);
            observe_rw[15] = rotationToObservedState(row.hand_L_rx);
            observe_rw[16] = rotationToObservedState(row.hand_L_ry);
            observe_rw[17] = rotationToObservedState(row.hand_L_rz);
            return observe_rw;
            */
            observe_rw[0] = translationToObservedState(-row.upperArm_L_tx);
            observe_rw[1] = translationToObservedState(row.upperArm_L_ty);
            observe_rw[2] = translationToObservedState(-row.upperArm_L_tz);
            observe_rw[3] = translationToObservedState(-row.foreArm_L_tx);
            observe_rw[4] = translationToObservedState(row.foreArm_L_ty);
            observe_rw[5] = translationToObservedState(-row.foreArm_L_tz);

            observe_rw[6] = translationToObservedState(-row.upperArm_R_tx);
            observe_rw[7] = translationToObservedState(row.upperArm_R_ty);
            observe_rw[8] = translationToObservedState(-row.upperArm_R_tz);
            observe_rw[9] = translationToObservedState(-row.foreArm_R_tx);
            observe_rw[10] = translationToObservedState(row.foreArm_R_ty);
            observe_rw[11] = translationToObservedState(-row.foreArm_R_tz);

            observe_rw[12] = translationToObservedState(-row.hand_R_tx);
            observe_rw[13] = translationToObservedState(row.hand_R_ty);
            observe_rw[14] = translationToObservedState(-row.hand_R_tz);
            observe_rw[15] = translationToObservedState(-row.hand_L_tx);
            observe_rw[16] = translationToObservedState(row.hand_L_ty);
            observe_rw[17] = translationToObservedState(-row.hand_L_tz);
            return observe_rw;
        }
        public double[] getrawObserveState(BrekelMongo row)
        {
            //int size_obv = 3 * 6;
            int size_obv = 3 * 5;
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
            observe_rw[3] = row.collar_R_tx;
            observe_rw[4] = row.collar_R_ty;
            observe_rw[5] = row.collar_R_tz;
            observe_rw[6] = row.chest_tx;
            observe_rw[7] = row.chest_ty;
            observe_rw[8] = row.chest_tz;
            observe_rw[9] = row.upperArm_L_tx;
            observe_rw[10] = row.upperArm_L_ty;
            observe_rw[11] = row.upperArm_L_tz;
            observe_rw[12] = row.upperArm_R_tx;
            observe_rw[13] = row.upperArm_R_ty;
            observe_rw[14] = row.upperArm_R_tz;


            return observe_rw;
        }
        public double[] getrawObserveState(MongoJoint row)
        {
            //int size_obv = 3 * 6;
            int size_obv = 3 * 5;
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
            observe_rw[0] = row.upperArm_L_tx;
            observe_rw[1] = row.upperArm_L_ty;
            observe_rw[2] = row.upperArm_L_tz;
            observe_rw[3] = row.upperArm_R_tx;
            observe_rw[4] = row.upperArm_R_ty;
            observe_rw[5] = row.upperArm_R_tz;
            observe_rw[6] = row.chest_tx;
            observe_rw[7] = row.chest_ty;
            observe_rw[8] = row.chest_tz;
            observe_rw[9] = row.foreArm_L_tx;
            observe_rw[10] = row.foreArm_L_ty;
            observe_rw[11] = row.foreArm_L_tz;
            observe_rw[12] = row.foreArm_R_tx;
            observe_rw[13] = row.foreArm_R_ty;
            observe_rw[14] = row.foreArm_R_tz;
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

            int state;
            if (rot > 90 || rot < -90)
            {
                state = 5;
            }
            else if (rot < -45)
            {
                state = 0;
            }
            else if (rot < -15)
            {
                state = 1;
            }
            else if (rot < 15)
            {
                state = 2;
            }
            else if (rot < 45)
            {
                state = 3;
            }
            else
            {
                state = 4;
            }
            return state;
        }
        private int translationToObservedState(double trans)
        {
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
        private BrekelMongo normalizeTranslate(BrekelMongo joint)
        {
            BrekelMongo trfed = joint;
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
        private BrekelMongo skeleTrans(BrekelMongo prev)
        {
            var skeleton = new BrekelJoints1(prev);
            var trfed = prev;
            trfed.hand_R_tx = skeleton.hand_R.posX;
            trfed.hand_R_ty = skeleton.hand_R.posY;
            trfed.hand_R_tz = skeleton.hand_R.posZ;
            trfed.spine_tx = skeleton.spine.posX;
            trfed.spine_ty = skeleton.spine.posY;
            trfed.spine_tz = skeleton.spine.posZ;
            trfed.chest_tx = skeleton.chest.posX;
            trfed.chest_ty = skeleton.chest.posY;
            trfed.chest_tz = skeleton.chest.posZ;
            trfed.neck_tx = skeleton.neck.posX;
            trfed.neck_ty = skeleton.neck.posY;
            trfed.neck_tz = skeleton.neck.posZ;
            trfed.head_tx = skeleton.head.posX;
            trfed.head_ty = skeleton.head.posY;
            trfed.head_tz = skeleton.head.posZ;
            trfed.upperLeg_L_tx = skeleton.upperLeg_L.posX;
            trfed.upperLeg_L_ty = skeleton.upperLeg_L.posY;
            trfed.upperLeg_L_tz = skeleton.upperLeg_L.posZ;
            trfed.lowerLeg_L_tx = skeleton.lowerLeg_L.posX;
            trfed.lowerLeg_L_ty = skeleton.lowerLeg_L.posY;
            trfed.lowerLeg_L_tz = skeleton.lowerLeg_L.posZ;
            trfed.foot_L_tx = skeleton.foot_L.posX;
            trfed.foot_L_ty = skeleton.foot_L.posY;
            trfed.foot_L_tz = skeleton.foot_L.posZ;
            trfed.toes_L_tx = skeleton.toes_L.posX;
            trfed.toes_L_ty = skeleton.toes_L.posY;
            trfed.toes_L_tz = skeleton.toes_L.posZ;
            trfed.upperLeg_R_tx = skeleton.upperLeg_R.posX;
            trfed.upperLeg_R_ty = skeleton.upperLeg_R.posY;
            trfed.upperLeg_R_tz = skeleton.upperLeg_R.posZ;
            trfed.lowerLeg_R_tx = skeleton.lowerLeg_R.posX;
            trfed.lowerLeg_R_ty = skeleton.lowerLeg_R.posY;
            trfed.lowerLeg_R_tz = skeleton.lowerLeg_R.posZ;
            trfed.foot_R_tx = skeleton.foot_R.posX;
            trfed.foot_R_ty = skeleton.foot_R.posY;
            trfed.foot_R_tz = skeleton.foot_R.posZ;
            trfed.toes_R_tx = skeleton.toes_R.posX;
            trfed.toes_R_ty = skeleton.toes_R.posY;
            trfed.toes_R_tz = skeleton.toes_R.posZ;
            trfed.collar_L_tx = skeleton.collar_L.posX;
            trfed.collar_L_ty = skeleton.collar_L.posY;
            trfed.collar_L_tz = skeleton.collar_L.posZ;
            trfed.upperArm_L_tx = skeleton.upperArm_L.posX;
            trfed.upperArm_L_ty = skeleton.upperArm_L.posY;
            trfed.upperArm_L_tz = skeleton.upperArm_L.posZ;
            trfed.foreArm_L_tx = skeleton.foreArm_L.posX;
            trfed.foreArm_L_ty = skeleton.foreArm_L.posY;
            trfed.foreArm_L_tz = skeleton.foreArm_L.posZ;
            trfed.hand_L_tx = skeleton.hand_L.posX;
            trfed.hand_L_ty = skeleton.hand_L.posY;
            trfed.hand_L_tz = skeleton.hand_L.posZ;
            trfed.collar_R_tx = skeleton.collar_R.posX;
            trfed.collar_R_ty = skeleton.collar_R.posY;
            trfed.collar_R_tz = skeleton.collar_R.posZ;
            trfed.upperArm_R_tx = skeleton.upperArm_R.posX;
            trfed.upperArm_R_ty = skeleton.upperArm_R.posY;
            trfed.upperArm_R_tz = skeleton.upperArm_R.posZ;
            trfed.foreArm_R_tx = skeleton.foreArm_R.posX;
            trfed.foreArm_R_ty = skeleton.foreArm_R.posY;
            trfed.foreArm_R_tz = skeleton.foreArm_R.posZ;
            return trfed;
        }
        /*
        private BrekelMongo rotateBrekel(BrekelMongo joint)
        {
            BrekelMongo trfed = joint;
            Vector3 buff = rotateXYZ(trfed.hand_R_tx, trfed.hand_R_ty, trfed.hand_R_tz, trfed.hand_R_rx, trfed.hand_R_ry, trfed.hand_R_rz);
            trfed.hand_R_tx = buff.x;
            trfed.hand_R_ty = buff.y;
            trfed.hand_R_tz = buff.z;
            buff = rotateXYZ(trfed.spine_tx, trfed.spine_ty, trfed.spine_tz, trfed.spine_rx, trfed.spine_ry, trfed.spine_rz);
            trfed.spine_tx = buff.x;
            trfed.spine_ty = buff.y;
            trfed.spine_tz = buff.z;
            buff = rotateXYZ(trfed.chest_tx, trfed.chest_ty, trfed.chest_tz, trfed.chest_rx, trfed.chest_ry, trfed.chest_rz);
            trfed.chest_tx = buff.x;
            trfed.chest_ty = buff.y;
            trfed.chest_tz = buff.z;
            buff = rotateXYZ(trfed.neck_tx, trfed.neck_ty, trfed.neck_tz, trfed.neck_rx, trfed.neck_ry, trfed.neck_rz);
            trfed.neck_tx = buff.x;
            trfed.neck_ty = buff.y;
            trfed.neck_tz = buff.z;
            buff = rotateXYZ(trfed.head_tx, trfed.head_ty, trfed.head_tz, trfed.head_rx, trfed.head_ry, trfed.head_rz);
            trfed.head_tx = buff.x;
            trfed.head_ty = buff.y;
            trfed.head_tz = buff.z;
            buff = rotateXYZ(trfed.upperLeg_L_tx, trfed.upperLeg_L_ty, trfed.upperLeg_L_tz, trfed.upperLeg_L_rx, trfed.upperLeg_L_ry, trfed.upperLeg_L_rz);
            trfed.upperLeg_L_tx = buff.x;
            trfed.upperLeg_L_ty = buff.y;
            trfed.upperLeg_L_tz = buff.z;
            buff = rotateXYZ(trfed.lowerLeg_L_tx, trfed.lowerLeg_L_ty, trfed.lowerLeg_L_tz, trfed.lowerLeg_L_rx, trfed.lowerLeg_L_ry, trfed.lowerLeg_L_rz);
            trfed.lowerLeg_L_tx = buff.x;
            trfed.lowerLeg_L_ty = buff.y;
            trfed.lowerLeg_L_tz = buff.z;
            buff = rotateXYZ(trfed.foot_L_tx, trfed.foot_L_ty, trfed.foot_L_tz, trfed.foot_L_rx, trfed.foot_L_ry, trfed.foot_L_rz);
            trfed.foot_L_tx = buff.x;
            trfed.foot_L_ty = buff.y;
            trfed.foot_L_tz = buff.z;
            buff = rotateXYZ(trfed.toes_L_tx, trfed.toes_L_ty, trfed.toes_L_tz, trfed.toes_L_rx, trfed.toes_L_ry, trfed.toes_L_rz);
            trfed.toes_L_tx = buff.x;
            trfed.toes_L_ty = buff.y;
            trfed.toes_L_tz = buff.z;
            buff = rotateXYZ(trfed.upperLeg_R_tx, trfed.upperLeg_R_ty, trfed.upperLeg_R_tz, trfed.upperLeg_R_rx, trfed.upperLeg_R_ry, trfed.upperLeg_R_rz);
            trfed.upperLeg_R_tx = buff.x;
            trfed.upperLeg_R_ty = buff.y;
            trfed.upperLeg_R_tz = buff.z;
            buff = rotateXYZ(trfed.lowerLeg_L_tx, trfed.lowerLeg_L_ty, trfed.lowerLeg_L_tz, trfed.lowerLeg_L_rx, trfed.lowerLeg_L_ry, trfed.lowerLeg_L_rz);
            trfed.lowerLeg_R_tx = buff.x;
            trfed.lowerLeg_R_ty = buff.y;
            trfed.lowerLeg_R_tz = buff.z;
            buff = rotateXYZ(trfed.foot_R_tx, trfed.foot_R_ty, trfed.foot_R_tz, trfed.foot_R_rx, trfed.foot_R_ry, trfed.foot_R_rz);
            trfed.foot_R_tx = buff.x;
            trfed.foot_R_ty = buff.y;
            trfed.foot_R_tz = buff.z;
            buff = rotateXYZ(trfed.toes_R_tx, trfed.toes_R_ty, trfed.toes_R_tz, trfed.toes_R_rx, trfed.toes_R_ry, trfed.toes_R_rz);
            trfed.toes_R_tx = buff.x;
            trfed.toes_R_ty = buff.y;
            trfed.toes_R_tz = buff.z;
            buff = rotateXYZ(trfed.collar_L_tx, trfed.collar_L_ty, trfed.collar_L_tz, trfed.collar_L_rx, trfed.collar_L_ry, trfed.collar_L_rz);
            trfed.collar_L_tx = buff.x;
            trfed.collar_L_ty = buff.y;
            trfed.collar_L_tz = buff.z;
            buff = rotateXYZ(trfed.upperArm_L_tx, trfed.upperArm_L_ty, trfed.upperArm_L_tz, trfed.upperArm_L_rx, trfed.upperArm_L_ry, trfed.upperArm_L_rz);
            trfed.upperArm_L_tx = buff.x;
            trfed.upperArm_L_ty = buff.y;
            trfed.upperArm_L_tz = buff.z;
            buff = rotateXYZ(trfed.foreArm_L_tx, trfed.foreArm_L_ty, trfed.foreArm_L_tz, trfed.foreArm_L_rx, trfed.foreArm_L_ry, trfed.foreArm_L_rz);
            trfed.foreArm_L_tx =  buff.x;
            trfed.foreArm_L_ty =  buff.y;
            trfed.foreArm_L_tz =  buff.z;
            buff = rotateXYZ(trfed.hand_L_tx, trfed.hand_L_ty, trfed.hand_L_tz, trfed.hand_L_rx, trfed.hand_L_ry, trfed.hand_L_rz);
            trfed.hand_L_tx = buff.x;
            trfed.hand_L_ty = buff.y;
            trfed.hand_L_tz = buff.z;
            buff = rotateXYZ(trfed.collar_R_tx, trfed.collar_R_ty, trfed.collar_R_tz, trfed.collar_R_rx, trfed.collar_R_ry, trfed.collar_R_rz);
            trfed.collar_R_tx = buff.x;
            trfed.collar_R_ty = buff.y;
            trfed.collar_R_tz = buff.z;
            buff = rotateXYZ(trfed.upperArm_R_tx, trfed.upperArm_R_ty, trfed.upperArm_R_tz, trfed.upperArm_R_rx, trfed.upperArm_R_ry, trfed.upperArm_R_rz);
            trfed.upperArm_R_tx = buff.x;
            trfed.upperArm_R_ty = buff.y;
            trfed.upperArm_R_tz = buff.z;
            buff = rotateXYZ(trfed.foreArm_R_tx,trfed.foreArm_R_ty , trfed.foreArm_R_tz,trfed.foreArm_R_rx,trfed.foreArm_R_ry , trfed.foreArm_R_rz);
            trfed.foreArm_R_tx = buff.x;
            trfed.foreArm_R_ty = buff.y;
            trfed.foreArm_R_tz = buff.z;

            return trfed;
        }
        */

        private class Vector3
        {
            public double x { get; set; }
            public double y { get; set; }
            public double z { get; set; }
        }
        private Vector3 rotateXYZ(double tx, double ty, double tz, double rx, double ry, double rz)
        {
            Vector3 vect = new Vector3();
            double conv = Math.PI / 180;

            vect.y = ty;
            vect.x = tx * Math.Cos(ry * conv) + tz * Math.Sin(ry * conv);
            vect.z = -tx * Math.Sin(ry * conv) + tz * Math.Cos(ry * conv);

            double temp = vect.y;
            vect.y = temp * Math.Cos(rx * conv) - vect.z * Math.Sin(rx * conv);
            vect.z = temp * Math.Sin(rx * conv) + vect.z * Math.Cos(rx * conv);

            temp = vect.x;
            vect.x = temp * Math.Cos(rz * conv) - vect.y * Math.Sin(rz * conv);
            vect.y = temp * Math.Sin(rz * conv) + vect.y * Math.Cos(rz * conv);
            return vect;
        }

    }
}
