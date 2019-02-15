using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlLibrary1
{
    class MongoJoint
    {
        [BsonId]
        public string stamp { get; set; }
        public double waist_tx { get; set; }
        public double waist_ty { get; set; }
        public double waist_tz { get; set; }
        public double waist_rx { get; set; }
        public double waist_ry { get; set; }
        public double waist_rz { get; set; }
        public double spine_tx { get; set; }
        public double spine_ty { get; set; }
        public double spine_tz { get; set; }
        public double spine_rx { get; set; }
        public double spine_ry { get; set; }
        public double spine_rz { get; set; }
        public double chest_tx { get; set; }
        public double chest_ty { get; set; }
        public double chest_tz { get; set; }
        public double chest_rx { get; set; }
        public double chest_ry { get; set; }
        public double chest_rz { get; set; }
        public double neck_tx { get; set; }
        public double neck_ty { get; set; }
        public double neck_tz { get; set; }
        public double neck_rx { get; set; }
        public double neck_ry { get; set; }
        public double neck_rz { get; set; }
        public double head_tx { get; set; }
        public double head_ty { get; set; }
        public double head_tz { get; set; }
        public double head_rx { get; set; }
        public double head_ry { get; set; }
        public double head_rz { get; set; }
        public double upperLeg_L_tx { get; set; }
        public double upperLeg_L_ty { get; set; }
        public double upperLeg_L_tz { get; set; }
        public double upperLeg_L_rx { get; set; }
        public double upperLeg_L_ry { get; set; }
        public double upperLeg_L_rz { get; set; }
        public double lowerLeg_L_tx { get; set; }
        public double lowerLeg_L_ty { get; set; }
        public double lowerLeg_L_tz { get; set; }
        public double lowerLeg_L_rx { get; set; }
        public double lowerLeg_L_ry { get; set; }
        public double lowerLeg_L_rz { get; set; }
        public double foot_L_tx { get; set; }
        public double foot_L_ty { get; set; }
        public double foot_L_tz { get; set; }
        public double foot_L_rx { get; set; }
        public double foot_L_ry { get; set; }
        public double foot_L_rz { get; set; }
        public double toes_L_tx { get; set; }
        public double toes_L_ty { get; set; }
        public double toes_L_tz { get; set; }
        public double toes_L_rx { get; set; }
        public double toes_L_ry { get; set; }
        public double toes_L_rz { get; set; }
        public double upperLeg_R_tx { get; set; }
        public double upperLeg_R_ty { get; set; }
        public double upperLeg_R_tz { get; set; }
        public double upperLeg_R_rx { get; set; }
        public double upperLeg_R_ry { get; set; }
        public double upperLeg_R_rz { get; set; }
        public double lowerLeg_R_tx { get; set; }
        public double lowerLeg_R_ty { get; set; }
        public double lowerLeg_R_tz { get; set; }
        public double lowerLeg_R_rx { get; set; }
        public double lowerLeg_R_ry { get; set; }
        public double lowerLeg_R_rz { get; set; }
        public double foot_R_tx { get; set; }
        public double foot_R_ty { get; set; }
        public double foot_R_tz { get; set; }
        public double foot_R_rx { get; set; }
        public double foot_R_ry { get; set; }
        public double foot_R_rz { get; set; }
        public double toes_R_tx { get; set; }
        public double toes_R_ty { get; set; }
        public double toes_R_tz { get; set; }
        public double toes_R_rx { get; set; }
        public double toes_R_ry { get; set; }
        public double toes_R_rz { get; set; }
        public double collar_L_tx { get; set; }
        public double collar_L_ty { get; set; }
        public double collar_L_tz { get; set; }
        public double collar_L_rx { get; set; }
        public double collar_L_ry { get; set; }
        public double collar_L_rz { get; set; }
        public double upperArm_L_tx { get; set; }
        public double upperArm_L_ty { get; set; }
        public double upperArm_L_tz { get; set; }
        public double upperArm_L_rx { get; set; }
        public double upperArm_L_ry { get; set; }
        public double upperArm_L_rz { get; set; }
        public double foreArm_L_tx { get; set; }
        public double foreArm_L_ty { get; set; }
        public double foreArm_L_tz { get; set; }
        public double foreArm_L_rx { get; set; }
        public double foreArm_L_ry { get; set; }
        public double foreArm_L_rz { get; set; }
        public double hand_L_tx { get; set; }
        public double hand_L_ty { get; set; }
        public double hand_L_tz { get; set; }
        public double hand_L_rx { get; set; }
        public double hand_L_ry { get; set; }
        public double hand_L_rz { get; set; }
        public double collar_R_tx { get; set; }
        public double collar_R_ty { get; set; }
        public double collar_R_tz { get; set; }
        public double collar_R_rx { get; set; }
        public double collar_R_ry { get; set; }
        public double collar_R_rz { get; set; }
        public double upperArm_R_tx { get; set; }
        public double upperArm_R_ty { get; set; }
        public double upperArm_R_tz { get; set; }
        public double upperArm_R_rx { get; set; }
        public double upperArm_R_ry { get; set; }
        public double upperArm_R_rz { get; set; }
        public double foreArm_R_tx { get; set; }
        public double foreArm_R_ty { get; set; }
        public double foreArm_R_tz { get; set; }
        public double foreArm_R_rx { get; set; }
        public double foreArm_R_ry { get; set; }
        public double foreArm_R_rz { get; set; }
        public double hand_R_tx { get; set; }
        public double hand_R_ty { get; set; }
        public double hand_R_tz { get; set; }
        public double hand_R_rx { get; set; }
        public double hand_R_ry { get; set; }
        public double hand_R_rz { get; set; }
    }
}
