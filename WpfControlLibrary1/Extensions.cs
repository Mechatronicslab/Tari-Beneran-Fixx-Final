using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfControlLibrary1
{

    public static class Extensions
    {
        #region Drawing
        // Supercommand of drawing the skeleton from body
        public static void DrawSkeleton(this Canvas canvas, CanvasSkeletal skele)
        {
            List<Joint> listJoint = skele.getList();

            foreach (Joint joint in listJoint)
            {
                canvas.DrawJoint(joint, listJoint.IndexOf(joint));
            }
            /*
            for (int i = 0; i < 8; i++)
            {
                canvas.DrawJoint(listJoint[i], i);
            }
            */

            canvas.DrawBone(skele.waist, skele.spine);
            canvas.DrawBone(skele.spine, skele.chest);
            canvas.DrawBone(skele.chest, skele.neck);
            canvas.DrawBone(skele.neck, skele.head);
            canvas.DrawBone(skele.head, skele.head_tip);
            canvas.DrawBone(skele.chest, skele.collar_L);
            canvas.DrawBone(skele.collar_L, skele.upperArm_L);
            canvas.DrawBone(skele.upperArm_L, skele.foreArm_L);
            canvas.DrawBone(skele.foreArm_L, skele.hand_L);
            canvas.DrawBone(skele.hand_L, skele.middle_L);
            canvas.DrawBone(skele.hand_L, skele.thumb_L);
            canvas.DrawBone(skele.chest, skele.collar_R);
            canvas.DrawBone(skele.collar_R, skele.upperArm_R);
            canvas.DrawBone(skele.upperArm_R, skele.foreArm_R);
            canvas.DrawBone(skele.foreArm_R, skele.hand_R);
            canvas.DrawBone(skele.hand_R, skele.middle_R);
            canvas.DrawBone(skele.hand_R, skele.thumb_R);
            canvas.DrawBone(skele.waist, skele.upperLeg_L);
            canvas.DrawBone(skele.upperLeg_L, skele.lowerLeg_L);
            canvas.DrawBone(skele.lowerLeg_L, skele.foot_L);
            canvas.DrawBone(skele.foot_L, skele.toes_L);
            canvas.DrawBone(skele.waist, skele.upperLeg_R);
            canvas.DrawBone(skele.upperLeg_R, skele.lowerLeg_R);
            canvas.DrawBone(skele.lowerLeg_R, skele.foot_R);
            canvas.DrawBone(skele.foot_R, skele.toes_R);

        }

        // Draw joint as a point
        public static void DrawJoint(this Canvas canvas, Joint joint, int labele)
        {
            double Scale = 2;
            if (joint.trackingState < 0.5)
            {
                return;
            }
            Ellipse ellipse = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = new SolidColorBrush(Colors.Yellow)
            };
            System.Windows.Controls.Label lab = new System.Windows.Controls.Label();
            lab.Content = labele.ToString();
            lab.Foreground = new SolidColorBrush(Colors.White);

            Canvas.SetLeft(lab, Scale * joint.posX + canvas.ActualWidth / 2 - ellipse.Width / 2);
            Canvas.SetTop(lab, Scale * -joint.posY + canvas.ActualHeight / 2 - ellipse.Height / 2);

            Canvas.SetLeft(ellipse, Scale * joint.posX + canvas.ActualWidth / 2 - ellipse.Width / 2);
            Canvas.SetTop(ellipse, Scale * -joint.posY + canvas.ActualHeight / 2 - ellipse.Height / 2);

            canvas.Children.Add(ellipse);
            canvas.Children.Add(lab);
        }
        // Draw skeleton as a line from two joints
        public static void DrawBone(this Canvas canvas, Joint first, Joint second)
        {
            double Scale = 2;
            if (first.trackingState < 0.5 || second.trackingState < 0.5)
            {
                return;
            }

            Line line = new Line
            {
                X1 = Scale * first.posX + canvas.ActualWidth / 2,
                Y1 = Scale * -first.posY + canvas.ActualHeight / 2,
                X2 = Scale * second.posX + canvas.ActualWidth / 2,
                Y2 = Scale * -second.posY + canvas.ActualHeight / 2,
                StrokeThickness = 8,
                Stroke = new SolidColorBrush(Colors.Red)
            };

            canvas.Children.Add(line);

        }
        public static void DrawSkeletonSide(this Canvas canvas, CanvasSkeletal skele)
        {
            List<Joint> listJoint = skele.getList();

            foreach (Joint joint in listJoint)
            {
                canvas.DrawJointSide(joint, listJoint.IndexOf(joint));
            }
            /*
            for (int i = 0; i < 8; i++)
            {
                canvas.DrawJoint(listJoint[i], i);
            }
            */

            canvas.DrawBoneSide(skele.waist, skele.spine);
            canvas.DrawBoneSide(skele.spine, skele.chest);
            canvas.DrawBoneSide(skele.chest, skele.neck);
            canvas.DrawBoneSide(skele.neck, skele.head);
            canvas.DrawBoneSide(skele.head, skele.head_tip);
            canvas.DrawBoneSide(skele.chest, skele.collar_L);
            canvas.DrawBoneSide(skele.collar_L, skele.upperArm_L);
            canvas.DrawBoneSide(skele.upperArm_L, skele.foreArm_L);
            canvas.DrawBoneSide(skele.foreArm_L, skele.hand_L);
            canvas.DrawBoneSide(skele.hand_L, skele.middle_L);
            canvas.DrawBoneSide(skele.hand_L, skele.thumb_L);
            canvas.DrawBoneSide(skele.chest, skele.collar_R);
            canvas.DrawBoneSide(skele.collar_R, skele.upperArm_R);
            canvas.DrawBoneSide(skele.upperArm_R, skele.foreArm_R);
            canvas.DrawBoneSide(skele.foreArm_R, skele.hand_R);
            canvas.DrawBoneSide(skele.hand_R, skele.middle_R);
            canvas.DrawBoneSide(skele.hand_R, skele.thumb_R);
            canvas.DrawBoneSide(skele.waist, skele.upperLeg_L);
            canvas.DrawBoneSide(skele.upperLeg_L, skele.lowerLeg_L);
            canvas.DrawBoneSide(skele.lowerLeg_L, skele.foot_L);
            canvas.DrawBoneSide(skele.foot_L, skele.toes_L);
            canvas.DrawBoneSide(skele.waist, skele.upperLeg_R);
            canvas.DrawBoneSide(skele.upperLeg_R, skele.lowerLeg_R);
            canvas.DrawBoneSide(skele.lowerLeg_R, skele.foot_R);
            canvas.DrawBoneSide(skele.foot_R, skele.toes_R);

        }

        // Draw joint as a point
        public static void DrawJointSide(this Canvas canvas, Joint joint, int labele)
        {
            double Scale = 2;
            if (joint.trackingState < 0.5)
            {
                return;
            }
            Ellipse ellipse = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = new SolidColorBrush(Colors.Yellow)
            };
            System.Windows.Controls.Label lab = new System.Windows.Controls.Label();
            lab.Content = labele.ToString();
            lab.Foreground = new SolidColorBrush(Colors.White);

            Canvas.SetLeft(lab, Scale * joint.posZ + canvas.ActualWidth / 2 - ellipse.Width / 2);
            Canvas.SetTop(lab, Scale * -joint.posY + canvas.ActualHeight / 2 - ellipse.Height / 2);

            Canvas.SetLeft(ellipse, Scale * joint.posZ + canvas.ActualWidth / 2 - ellipse.Width / 2);
            Canvas.SetTop(ellipse, Scale * -joint.posY + canvas.ActualHeight / 2 - ellipse.Height / 2);

            canvas.Children.Add(ellipse);
            canvas.Children.Add(lab);
        }
        // Draw skeleton as a line from two joints
        public static void DrawBoneSide(this Canvas canvas, Joint first, Joint second)
        {
            double Scale = 2;
            if (first.trackingState < 0.5 || second.trackingState < 0.5)
            {
                return;
            }

            Line line = new Line
            {
                X1 = Scale * first.posZ + canvas.ActualWidth / 2,
                Y1 = Scale * -first.posY + canvas.ActualHeight / 2,
                X2 = Scale * second.posZ + canvas.ActualWidth / 2,
                Y2 = Scale * -second.posY + canvas.ActualHeight / 2,
                StrokeThickness = 8,
                Stroke = new SolidColorBrush(Colors.Red)
            };

            canvas.Children.Add(line);
        }
        #endregion
        #region Transforms
        public static List<CanvasSkeletal> SkeletalTransformation(this List<BrekelMongo> batch)
        {
            List<CanvasSkeletal> SkeleSet = new List<CanvasSkeletal>();
            foreach (BrekelMongo brekel in batch)
            {
                CanvasSkeletal Skele = new CanvasSkeletal(brekel, "MOD");
                SkeleSet.Add(Skele);
                Console.WriteLine("Loaded LINE:{0}", SkeleSet.Count());
            }
            return SkeleSet;
        }
        #endregion
    }

}