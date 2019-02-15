using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TariStream
{
    public static class Extensions
    {
        // Extension of ImageSource converting frames to Bitmap
        #region Camera
        // Color frame to Bitmap 
        public static ImageSource ToBitmap(this ColorFrame frame)
        {
            int width = frame.FrameDescription.Width;
            int height = frame.FrameDescription.Height;
            PixelFormat format = PixelFormats.Bgr32;

            byte[] pixels = new byte[width * height * ((format.BitsPerPixel + 7) / 8)];

            if (frame.RawColorImageFormat == ColorImageFormat.Bgra)
            {
                frame.CopyRawFrameDataToArray(pixels);
            }
            else
            {
                frame.CopyConvertedFrameDataToArray(pixels, ColorImageFormat.Bgra);
            }

            int stride = width * format.BitsPerPixel / 8;

            return BitmapSource.Create(width, height, 96, 96, format, null, pixels, stride);
        }
        // Depth frame to Bitmap
        public static ImageSource ToBitmap(this DepthFrame frame)
        {
            int width = frame.FrameDescription.Width;
            int height = frame.FrameDescription.Height;
            PixelFormat format = PixelFormats.Bgr32;

            ushort minDepth = frame.DepthMinReliableDistance;
            ushort maxDepth = frame.DepthMaxReliableDistance;

            ushort[] pixelData = new ushort[width * height];
            byte[] pixels = new byte[width * height * (format.BitsPerPixel + 7) / 8];

            frame.CopyFrameDataToArray(pixelData);

            int colorIndex = 0;
            for (int depthIndex = 0; depthIndex < pixelData.Length; ++depthIndex)
            {
                ushort depth = pixelData[depthIndex];

                byte intensity = (byte)(depth >= minDepth && depth <= maxDepth ? depth : 0);

                pixels[colorIndex++] = intensity; // Blue
                pixels[colorIndex++] = intensity; // Green
                pixels[colorIndex++] = intensity; // Red

                ++colorIndex;
            }

            int stride = width * format.BitsPerPixel / 8;

            return BitmapSource.Create(width, height, 96, 96, format, null, pixels, stride);
        }
        // IR frame to Bitmap
        public static ImageSource ToBitmap(this InfraredFrame frame)
        {
            int width = frame.FrameDescription.Width;
            int height = frame.FrameDescription.Height;
            PixelFormat format = PixelFormats.Bgr32;

            ushort[] frameData = new ushort[width * height];
            byte[] pixels = new byte[width * height * (format.BitsPerPixel + 7) / 8];

            frame.CopyFrameDataToArray(frameData);

            int colorIndex = 0;
            for (int infraredIndex = 0; infraredIndex < frameData.Length; infraredIndex++)
            {
                ushort ir = frameData[infraredIndex];

                byte intensity = (byte)(ir >> 7);

                pixels[colorIndex++] = (byte)(intensity / 1); // Blue
                pixels[colorIndex++] = (byte)(intensity / 1); // Green   
                pixels[colorIndex++] = (byte)(intensity / 0.4); // Red

                colorIndex++;
            }

            int stride = width * format.BitsPerPixel / 8;

            return BitmapSource.Create(width, height, 96, 96, format, null, pixels, stride);
        }

        #endregion
        // Extension of Joint in scaling to current frame modes
        #region Body
        // OBSOLETE
        // Scale to Defined pixel with varying skeleton max value
        public static Joint ScaleTo(this Joint joint, double width, double height, float skeletonMaxX, float skeletonMaxY)
        {
            joint.Position = new CameraSpacePoint
            {
                X = Scale(width, skeletonMaxX, joint.Position.X),
                Y = Scale(height, skeletonMaxY, -joint.Position.Y),
                Z = joint.Position.Z
            };

            return joint;
        }
        // Scale to (Supercommand)
        public static Joint ScaleTo(this Joint joint, double width, double height)
        {
            return ScaleTo(joint, width, height, 1.0f, 1.0f);
        }
        // Scale routine
        private static float Scale(double maxPixel, double maxSkeleton, float position)
        {
            float value = (float)((((maxPixel / maxSkeleton) / 2) * position) + (maxPixel / 2));

            if (value > maxPixel)
            {
                return (float)maxPixel;
            }

            if (value < 0)
            {
                return 0;
            }

            return value;
        }
        // OBSOLETE   
        // Map to floater
        public static Joint MapTo(this Joint joint, Mode _mode, KinectSensor _sensor)
        {
            if (_mode == Mode.Color)
            {
                ColorSpacePoint colorPoint = _sensor.CoordinateMapper.MapCameraPointToColorSpace(joint.Position);
                joint.Position.X = float.IsInfinity(colorPoint.X) ? 0 : colorPoint.X;
                joint.Position.Y = float.IsInfinity(colorPoint.Y) ? 0 : colorPoint.Y;
            }
            else if (_mode == Mode.Depth || _mode == Mode.Infrared) // Change the Image and Canvas dimensions to 512x424
            {
                DepthSpacePoint depthPoint = _sensor.CoordinateMapper.MapCameraPointToDepthSpace(joint.Position);
                joint.Position.X = float.IsInfinity(depthPoint.X) ? 0 : depthPoint.X * 1920 / 512;
                joint.Position.Y = float.IsInfinity(depthPoint.Y) ? 0 : depthPoint.Y * 1080 / 424;
            }
            return joint;
        }
        // Convert to Relative Coordinate
        public static Body ConvertToHipRef(this Body body)
        {
            var refer = body.Joints[JointType.SpineBase].Position;
            var arrayE = Enum.GetValues(typeof(JointType)).Cast<int>().Select(x => x.ToString()).ToArray();
            foreach (var joy in arrayE)
            {
                JointType JoT = (JointType)Enum.Parse(typeof(JointType), joy);
                body.Joints[JoT].changeRef(refer.X, refer.Y, refer.Z);
            }
            body.Joints[JointType.SpineBase].changeRef(-refer.X, -refer.Y, -refer.Z);
            return body;
        }
        public static Joint changeRef(this Joint joint, float x, float y, float z)
        {
            joint.Position.X = joint.Position.X - x;
            joint.Position.Y = joint.Position.Y - y;
            joint.Position.Z = joint.Position.Z - z;
            return joint;
        }
        #endregion
        // Extension of Canvas for drawing skeleton in application
        #region Drawing
        // Supercommand of drawing the skeleton from body
        public static void DrawSkeleton(this Canvas canvas, Body body, KinectSensor sense, Mode mod)
        {
            double[] scales = new double[] { canvas.ActualWidth / 1920, canvas.ActualHeight / 1080 };
            if (body == null) return;

            foreach (Joint joint in body.Joints.Values)
            {
                canvas.DrawPoint(joint, sense, mod, scales);
            }

            canvas.DrawLine(body.Joints[JointType.Head], body.Joints[JointType.Neck], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.Neck], body.Joints[JointType.SpineShoulder], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.SpineShoulder], body.Joints[JointType.ShoulderLeft], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.SpineShoulder], body.Joints[JointType.ShoulderRight], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.SpineShoulder], body.Joints[JointType.SpineMid], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.ShoulderLeft], body.Joints[JointType.ElbowLeft], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.ShoulderRight], body.Joints[JointType.ElbowRight], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.ElbowLeft], body.Joints[JointType.WristLeft], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.ElbowRight], body.Joints[JointType.WristRight], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.WristLeft], body.Joints[JointType.HandLeft], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.WristRight], body.Joints[JointType.HandRight], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.HandLeft], body.Joints[JointType.HandTipLeft], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.HandRight], body.Joints[JointType.HandTipRight], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.HandLeft], body.Joints[JointType.ThumbLeft], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.HandRight], body.Joints[JointType.ThumbRight], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.SpineMid], body.Joints[JointType.SpineBase], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.SpineBase], body.Joints[JointType.HipLeft], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.SpineBase], body.Joints[JointType.HipRight], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.HipLeft], body.Joints[JointType.KneeLeft], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.HipRight], body.Joints[JointType.KneeRight], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.KneeLeft], body.Joints[JointType.AnkleLeft], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.KneeRight], body.Joints[JointType.AnkleRight], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.AnkleLeft], body.Joints[JointType.FootLeft], sense, mod, scales);
            canvas.DrawLine(body.Joints[JointType.AnkleRight], body.Joints[JointType.FootRight], sense, mod, scales);
        }
        // Draw joint as a point
        public static void DrawPoint(this Canvas canvas, Joint joint, KinectSensor sense, Mode mod, double[] scales)
        {
            if (joint.TrackingState == TrackingState.NotTracked) return;

            //joint = joint.ScaleTo(canvas.ActualWidth, canvas.ActualHeight);
            joint = joint.MapTo(mod, sense);

            Ellipse ellipse = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = new SolidColorBrush(Colors.LightBlue)
            };

            Canvas.SetLeft(ellipse, joint.Position.X * scales[0] - ellipse.Width / 2);
            Canvas.SetTop(ellipse, joint.Position.Y * scales[1] - ellipse.Height / 2);

            canvas.Children.Add(ellipse);
        }
        // Draw skeleton as a line from two joints
        public static void DrawLine(this Canvas canvas, Joint first, Joint second, KinectSensor sense, Mode mod, double[] scales)
        {
            if (first.TrackingState == TrackingState.NotTracked || second.TrackingState == TrackingState.NotTracked) return;

            //first = first.ScaleTo(canvas.ActualWidth, canvas.ActualHeight);
            //second = second.ScaleTo(canvas.ActualWidth, canvas.ActualHeight);
            first = first.MapTo(mod, sense);
            second = second.MapTo(mod, sense);

            Line line = new Line
            {
                X1 = first.Position.X * scales[0],
                Y1 = first.Position.Y * scales[1],
                X2 = second.Position.X * scales[0],
                Y2 = second.Position.Y * scales[1],
                StrokeThickness = 8,
                Stroke = new SolidColorBrush(Colors.LightBlue)
            };

            canvas.Children.Add(line);
        }

        #endregion
        // Extension of Vector4 for Calculating orientation
        #region Calculate
        public static double Yaw(this Vector4 Ori)
        {
            double val1 = 2.0 * (Ori.W * Ori.Z + Ori.Y * Ori.X);
            double val2 = 1.0 - 2.0 * (Ori.Z * Ori.Z + Ori.Y * Ori.Y);
            return Math.Atan2(val1, val2) * (180.0 / Math.PI);
        }
        public static double Pitch(this Vector4 Ori)
        {
            double val = 2.0 * (Ori.W * Ori.Y - Ori.Z * Ori.X);
            val = val > 1.0 ? 1.0 : val;
            val = val < -1.0 ? -1.0 : val;
            return (Math.Asin(val)) * (180.0 / Math.PI);
        }
        public static double Roll(this Vector4 Ori)
        {
            double val1 = 2.0 * (Ori.W * Ori.X + Ori.Y * Ori.Z);
            double val2 = 1.0 - 2.0 * (Ori.X * Ori.X + Ori.Y * Ori.Y);
            return (Math.Atan2(val1, val2)) * (180.0 / Math.PI);
        }
        #endregion
        // Extension of MongoJoint to Harvest Data
        #region Harvester
        public static string Harvest(this MongoJoint Joints, DateTime delay, Body body)
        {
            //Joints.stamp = reference.BodyFrameReference.RelativeTime.ToString();
            Joints.stamp = delay.ToString() + " " + body.TrackingId;
            Joints.waist_tx = body.Joints[JointType.SpineBase].Position.X;
            Joints.waist_ty = body.Joints[JointType.SpineBase].Position.Y;
            Joints.waist_tz = body.Joints[JointType.SpineBase].Position.Z;
            Joints.waist_rx = body.JointOrientations[JointType.SpineBase].Orientation.Pitch();
            Joints.waist_ry = body.JointOrientations[JointType.SpineBase].Orientation.Roll();
            Joints.waist_rz = body.JointOrientations[JointType.SpineBase].Orientation.Yaw();
            Joints.spine_tx = body.Joints[JointType.SpineMid].Position.X;
            Joints.spine_ty = body.Joints[JointType.SpineMid].Position.Y;
            Joints.spine_tz = body.Joints[JointType.SpineMid].Position.Z;
            Joints.spine_rx = body.JointOrientations[JointType.SpineMid].Orientation.Pitch();
            Joints.spine_ry = body.JointOrientations[JointType.SpineMid].Orientation.Roll();
            Joints.spine_rz = body.JointOrientations[JointType.SpineMid].Orientation.Yaw();
            Joints.chest_tx = body.Joints[JointType.SpineShoulder].Position.X;
            Joints.chest_ty = body.Joints[JointType.SpineShoulder].Position.Y;
            Joints.chest_tz = body.Joints[JointType.SpineShoulder].Position.Z;
            Joints.chest_rx = body.JointOrientations[JointType.SpineShoulder].Orientation.Pitch();
            Joints.chest_ry = body.JointOrientations[JointType.SpineShoulder].Orientation.Roll();
            Joints.chest_rz = body.JointOrientations[JointType.SpineShoulder].Orientation.Yaw();
            Joints.neck_tx = body.Joints[JointType.Neck].Position.X;
            Joints.neck_ty = body.Joints[JointType.Neck].Position.Y;
            Joints.neck_tz = body.Joints[JointType.Neck].Position.Z;
            Joints.neck_rx = body.JointOrientations[JointType.Neck].Orientation.Pitch();
            Joints.neck_ry = body.JointOrientations[JointType.Neck].Orientation.Roll();
            Joints.neck_rz = body.JointOrientations[JointType.Neck].Orientation.Yaw();
            Joints.head_tx = body.Joints[JointType.Head].Position.X;
            Joints.head_ty = body.Joints[JointType.Head].Position.Y;
            Joints.head_tz = body.Joints[JointType.Head].Position.Z;
            Joints.head_rx = body.JointOrientations[JointType.Head].Orientation.Pitch();
            Joints.head_ry = body.JointOrientations[JointType.Head].Orientation.Roll();
            Joints.head_rz = body.JointOrientations[JointType.Head].Orientation.Yaw();
            Joints.upperLeg_L_tx = body.Joints[JointType.HipLeft].Position.X;
            Joints.upperLeg_L_ty = body.Joints[JointType.HipLeft].Position.Y;
            Joints.upperLeg_L_tz = body.Joints[JointType.HipLeft].Position.Z;
            Joints.upperLeg_L_rx = body.JointOrientations[JointType.HipLeft].Orientation.Pitch();
            Joints.upperLeg_L_ry = body.JointOrientations[JointType.HipLeft].Orientation.Roll();
            Joints.upperLeg_L_rz = body.JointOrientations[JointType.HipLeft].Orientation.Yaw();
            Joints.lowerLeg_L_tx = body.Joints[JointType.KneeLeft].Position.X;
            Joints.lowerLeg_L_ty = body.Joints[JointType.KneeLeft].Position.Y;
            Joints.lowerLeg_L_tz = body.Joints[JointType.KneeLeft].Position.Z;
            Joints.lowerLeg_L_rx = body.JointOrientations[JointType.KneeLeft].Orientation.Pitch();
            Joints.lowerLeg_L_ry = body.JointOrientations[JointType.KneeLeft].Orientation.Roll();
            Joints.lowerLeg_L_rz = body.JointOrientations[JointType.KneeLeft].Orientation.Yaw();
            Joints.foot_L_tx = body.Joints[JointType.AnkleLeft].Position.X;
            Joints.foot_L_ty = body.Joints[JointType.AnkleLeft].Position.Y;
            Joints.foot_L_tz = body.Joints[JointType.AnkleLeft].Position.Z;
            Joints.foot_L_rx = body.JointOrientations[JointType.AnkleLeft].Orientation.Pitch();
            Joints.foot_L_ry = body.JointOrientations[JointType.AnkleLeft].Orientation.Roll();
            Joints.foot_L_rz = body.JointOrientations[JointType.AnkleLeft].Orientation.Yaw();
            Joints.toes_L_tx = body.Joints[JointType.FootLeft].Position.X;
            Joints.toes_L_ty = body.Joints[JointType.FootLeft].Position.Y;
            Joints.toes_L_tz = body.Joints[JointType.FootLeft].Position.Z;
            Joints.toes_L_rx = body.JointOrientations[JointType.FootLeft].Orientation.Pitch();
            Joints.toes_L_ry = body.JointOrientations[JointType.FootLeft].Orientation.Roll();
            Joints.toes_L_rz = body.JointOrientations[JointType.FootLeft].Orientation.Yaw();
            Joints.upperLeg_R_tx = body.Joints[JointType.HipRight].Position.X;
            Joints.upperLeg_R_ty = body.Joints[JointType.HipRight].Position.Y;
            Joints.upperLeg_R_tz = body.Joints[JointType.HipRight].Position.Z;
            Joints.upperLeg_R_rx = body.JointOrientations[JointType.HipRight].Orientation.Pitch();
            Joints.upperLeg_R_ry = body.JointOrientations[JointType.HipRight].Orientation.Roll();
            Joints.upperLeg_R_rz = body.JointOrientations[JointType.HipRight].Orientation.Yaw();
            Joints.lowerLeg_R_tx = body.Joints[JointType.KneeRight].Position.X;
            Joints.lowerLeg_R_ty = body.Joints[JointType.KneeRight].Position.Y;
            Joints.lowerLeg_R_tz = body.Joints[JointType.KneeRight].Position.Z;
            Joints.lowerLeg_R_rx = body.JointOrientations[JointType.KneeRight].Orientation.Pitch();
            Joints.lowerLeg_R_ry = body.JointOrientations[JointType.KneeRight].Orientation.Roll();
            Joints.lowerLeg_R_rz = body.JointOrientations[JointType.KneeRight].Orientation.Yaw();
            Joints.foot_R_tx = body.Joints[JointType.AnkleRight].Position.X;
            Joints.foot_R_ty = body.Joints[JointType.AnkleRight].Position.Y;
            Joints.foot_R_tz = body.Joints[JointType.AnkleRight].Position.Z;
            Joints.foot_R_rx = body.JointOrientations[JointType.AnkleRight].Orientation.Pitch();
            Joints.foot_R_ry = body.JointOrientations[JointType.AnkleRight].Orientation.Roll();
            Joints.foot_R_rz = body.JointOrientations[JointType.AnkleRight].Orientation.Yaw();
            Joints.toes_R_tx = body.Joints[JointType.FootRight].Position.X;
            Joints.toes_R_ty = body.Joints[JointType.FootRight].Position.Y;
            Joints.toes_R_tz = body.Joints[JointType.FootRight].Position.Z;
            Joints.toes_R_rx = body.JointOrientations[JointType.FootRight].Orientation.Pitch();
            Joints.toes_R_ry = body.JointOrientations[JointType.FootRight].Orientation.Roll();
            Joints.toes_R_rz = body.JointOrientations[JointType.FootRight].Orientation.Yaw();
            Joints.collar_L_tx = body.Joints[JointType.WristLeft].Position.X;
            Joints.collar_L_ty = body.Joints[JointType.WristLeft].Position.Y;
            Joints.collar_L_tz = body.Joints[JointType.WristLeft].Position.Z;
            Joints.collar_L_rx = body.JointOrientations[JointType.WristLeft].Orientation.Pitch();
            Joints.collar_L_ry = body.JointOrientations[JointType.WristLeft].Orientation.Roll();
            Joints.collar_L_rz = body.JointOrientations[JointType.WristLeft].Orientation.Yaw();
            Joints.upperArm_L_tx = body.Joints[JointType.ShoulderLeft].Position.X;
            Joints.upperArm_L_ty = body.Joints[JointType.ShoulderLeft].Position.Y;
            Joints.upperArm_L_tz = body.Joints[JointType.ShoulderLeft].Position.Z;
            Joints.upperArm_L_rx = body.JointOrientations[JointType.ShoulderLeft].Orientation.Pitch();
            Joints.upperArm_L_ry = body.JointOrientations[JointType.ShoulderLeft].Orientation.Roll();
            Joints.upperArm_L_rz = body.JointOrientations[JointType.ShoulderLeft].Orientation.Yaw();
            Joints.foreArm_L_tx = body.Joints[JointType.ElbowLeft].Position.X;
            Joints.foreArm_L_ty = body.Joints[JointType.ElbowLeft].Position.Y;
            Joints.foreArm_L_tz = body.Joints[JointType.ElbowLeft].Position.Z;
            Joints.foreArm_L_rx = body.JointOrientations[JointType.ElbowLeft].Orientation.Pitch();
            Joints.foreArm_L_ry = body.JointOrientations[JointType.ElbowLeft].Orientation.Roll();
            Joints.foreArm_L_rz = body.JointOrientations[JointType.ElbowLeft].Orientation.Yaw();
            Joints.hand_L_tx = body.Joints[JointType.HandLeft].Position.X;
            Joints.hand_L_ty = body.Joints[JointType.HandLeft].Position.Y;
            Joints.hand_L_tz = body.Joints[JointType.HandLeft].Position.Z;
            Joints.hand_L_rx = body.JointOrientations[JointType.HandLeft].Orientation.Pitch();
            Joints.hand_L_ry = body.JointOrientations[JointType.HandLeft].Orientation.Roll();
            Joints.hand_L_rz = body.JointOrientations[JointType.HandLeft].Orientation.Yaw();
            Joints.upperArm_R_tx = body.Joints[JointType.ShoulderRight].Position.X;
            Joints.upperArm_R_ty = body.Joints[JointType.ShoulderRight].Position.Y;
            Joints.upperArm_R_tz = body.Joints[JointType.ShoulderRight].Position.Z;
            Joints.upperArm_R_rx = body.JointOrientations[JointType.ShoulderRight].Orientation.Pitch();
            Joints.upperArm_R_ry = body.JointOrientations[JointType.ShoulderRight].Orientation.Roll();
            Joints.upperArm_R_rz = body.JointOrientations[JointType.ShoulderRight].Orientation.Yaw();
            Joints.collar_R_tx = body.Joints[JointType.WristRight].Position.X;
            Joints.collar_R_ty = body.Joints[JointType.WristRight].Position.Y;
            Joints.collar_R_tz = body.Joints[JointType.WristRight].Position.Z;
            Joints.collar_R_rx = body.JointOrientations[JointType.WristRight].Orientation.Pitch();
            Joints.collar_R_ry = body.JointOrientations[JointType.WristRight].Orientation.Roll();
            Joints.collar_R_rz = body.JointOrientations[JointType.WristRight].Orientation.Yaw();
            Joints.foreArm_R_tx = body.Joints[JointType.ElbowRight].Position.X;
            Joints.foreArm_R_ty = body.Joints[JointType.ElbowRight].Position.Y;
            Joints.foreArm_R_tz = body.Joints[JointType.ElbowRight].Position.Z;
            Joints.foreArm_R_rx = body.JointOrientations[JointType.ElbowRight].Orientation.Pitch();
            Joints.foreArm_R_ry = body.JointOrientations[JointType.ElbowRight].Orientation.Roll();
            Joints.foreArm_R_rz = body.JointOrientations[JointType.ElbowRight].Orientation.Yaw();
            Joints.hand_R_tx = body.Joints[JointType.HandRight].Position.X;
            Joints.hand_R_ty = body.Joints[JointType.HandRight].Position.Y;
            Joints.hand_R_tz = body.Joints[JointType.HandRight].Position.Z;
            Joints.hand_R_rx = body.JointOrientations[JointType.HandRight].Orientation.Pitch();
            Joints.hand_R_ry = body.JointOrientations[JointType.HandRight].Orientation.Roll();
            Joints.hand_R_rz = body.JointOrientations[JointType.HandRight].Orientation.Yaw();
            return "HARVESTED";
        }
        public static string Harvest(this MongoJoint Joints, TimeSpan delay, Body body)
        {
            //Joints.stamp = reference.BodyFrameReference.RelativeTime.ToString();
            Joints.stamp = delay.ToString() + " " + body.TrackingId;
            Joints.waist_tx = body.Joints[JointType.SpineBase].Position.X;
            Joints.waist_ty = body.Joints[JointType.SpineBase].Position.Y;
            Joints.waist_tz = body.Joints[JointType.SpineBase].Position.Z;
            Joints.waist_rx = body.JointOrientations[JointType.SpineBase].Orientation.Pitch();
            Joints.waist_ry = body.JointOrientations[JointType.SpineBase].Orientation.Roll();
            Joints.waist_rz = body.JointOrientations[JointType.SpineBase].Orientation.Yaw();
            Joints.spine_tx = body.Joints[JointType.SpineMid].Position.X;
            Joints.spine_ty = body.Joints[JointType.SpineMid].Position.Y;
            Joints.spine_tz = body.Joints[JointType.SpineMid].Position.Z;
            Joints.spine_rx = body.JointOrientations[JointType.SpineMid].Orientation.Pitch();
            Joints.spine_ry = body.JointOrientations[JointType.SpineMid].Orientation.Roll();
            Joints.spine_rz = body.JointOrientations[JointType.SpineMid].Orientation.Yaw();
            Joints.chest_tx = body.Joints[JointType.SpineShoulder].Position.X;
            Joints.chest_ty = body.Joints[JointType.SpineShoulder].Position.Y;
            Joints.chest_tz = body.Joints[JointType.SpineShoulder].Position.Z;
            Joints.chest_rx = body.JointOrientations[JointType.SpineShoulder].Orientation.Pitch();
            Joints.chest_ry = body.JointOrientations[JointType.SpineShoulder].Orientation.Roll();
            Joints.chest_rz = body.JointOrientations[JointType.SpineShoulder].Orientation.Yaw();
            Joints.neck_tx = body.Joints[JointType.Neck].Position.X;
            Joints.neck_ty = body.Joints[JointType.Neck].Position.Y;
            Joints.neck_tz = body.Joints[JointType.Neck].Position.Z;
            Joints.neck_rx = body.JointOrientations[JointType.Neck].Orientation.Pitch();
            Joints.neck_ry = body.JointOrientations[JointType.Neck].Orientation.Roll();
            Joints.neck_rz = body.JointOrientations[JointType.Neck].Orientation.Yaw();
            Joints.head_tx = body.Joints[JointType.Head].Position.X;
            Joints.head_ty = body.Joints[JointType.Head].Position.Y;
            Joints.head_tz = body.Joints[JointType.Head].Position.Z;
            Joints.head_rx = body.JointOrientations[JointType.Head].Orientation.Pitch();
            Joints.head_ry = body.JointOrientations[JointType.Head].Orientation.Roll();
            Joints.head_rz = body.JointOrientations[JointType.Head].Orientation.Yaw();
            Joints.upperLeg_L_tx = body.Joints[JointType.HipLeft].Position.X;
            Joints.upperLeg_L_ty = body.Joints[JointType.HipLeft].Position.Y;
            Joints.upperLeg_L_tz = body.Joints[JointType.HipLeft].Position.Z;
            Joints.upperLeg_L_rx = body.JointOrientations[JointType.HipLeft].Orientation.Pitch();
            Joints.upperLeg_L_ry = body.JointOrientations[JointType.HipLeft].Orientation.Roll();
            Joints.upperLeg_L_rz = body.JointOrientations[JointType.HipLeft].Orientation.Yaw();
            Joints.lowerLeg_L_tx = body.Joints[JointType.KneeLeft].Position.X;
            Joints.lowerLeg_L_ty = body.Joints[JointType.KneeLeft].Position.Y;
            Joints.lowerLeg_L_tz = body.Joints[JointType.KneeLeft].Position.Z;
            Joints.lowerLeg_L_rx = body.JointOrientations[JointType.KneeLeft].Orientation.Pitch();
            Joints.lowerLeg_L_ry = body.JointOrientations[JointType.KneeLeft].Orientation.Roll();
            Joints.lowerLeg_L_rz = body.JointOrientations[JointType.KneeLeft].Orientation.Yaw();
            Joints.foot_L_tx = body.Joints[JointType.AnkleLeft].Position.X;
            Joints.foot_L_ty = body.Joints[JointType.AnkleLeft].Position.Y;
            Joints.foot_L_tz = body.Joints[JointType.AnkleLeft].Position.Z;
            Joints.foot_L_rx = body.JointOrientations[JointType.AnkleLeft].Orientation.Pitch();
            Joints.foot_L_ry = body.JointOrientations[JointType.AnkleLeft].Orientation.Roll();
            Joints.foot_L_rz = body.JointOrientations[JointType.AnkleLeft].Orientation.Yaw();
            Joints.toes_L_tx = body.Joints[JointType.FootLeft].Position.X;
            Joints.toes_L_ty = body.Joints[JointType.FootLeft].Position.Y;
            Joints.toes_L_tz = body.Joints[JointType.FootLeft].Position.Z;
            Joints.toes_L_rx = body.JointOrientations[JointType.FootLeft].Orientation.Pitch();
            Joints.toes_L_ry = body.JointOrientations[JointType.FootLeft].Orientation.Roll();
            Joints.toes_L_rz = body.JointOrientations[JointType.FootLeft].Orientation.Yaw();
            Joints.upperLeg_R_tx = body.Joints[JointType.HipRight].Position.X;
            Joints.upperLeg_R_ty = body.Joints[JointType.HipRight].Position.Y;
            Joints.upperLeg_R_tz = body.Joints[JointType.HipRight].Position.Z;
            Joints.upperLeg_R_rx = body.JointOrientations[JointType.HipRight].Orientation.Pitch();
            Joints.upperLeg_R_ry = body.JointOrientations[JointType.HipRight].Orientation.Roll();
            Joints.upperLeg_R_rz = body.JointOrientations[JointType.HipRight].Orientation.Yaw();
            Joints.lowerLeg_R_tx = body.Joints[JointType.KneeRight].Position.X;
            Joints.lowerLeg_R_ty = body.Joints[JointType.KneeRight].Position.Y;
            Joints.lowerLeg_R_tz = body.Joints[JointType.KneeRight].Position.Z;
            Joints.lowerLeg_R_rx = body.JointOrientations[JointType.KneeRight].Orientation.Pitch();
            Joints.lowerLeg_R_ry = body.JointOrientations[JointType.KneeRight].Orientation.Roll();
            Joints.lowerLeg_R_rz = body.JointOrientations[JointType.KneeRight].Orientation.Yaw();
            Joints.foot_R_tx = body.Joints[JointType.AnkleRight].Position.X;
            Joints.foot_R_ty = body.Joints[JointType.AnkleRight].Position.Y;
            Joints.foot_R_tz = body.Joints[JointType.AnkleRight].Position.Z;
            Joints.foot_R_rx = body.JointOrientations[JointType.AnkleRight].Orientation.Pitch();
            Joints.foot_R_ry = body.JointOrientations[JointType.AnkleRight].Orientation.Roll();
            Joints.foot_R_rz = body.JointOrientations[JointType.AnkleRight].Orientation.Yaw();
            Joints.toes_R_tx = body.Joints[JointType.FootRight].Position.X;
            Joints.toes_R_ty = body.Joints[JointType.FootRight].Position.Y;
            Joints.toes_R_tz = body.Joints[JointType.FootRight].Position.Z;
            Joints.toes_R_rx = body.JointOrientations[JointType.FootRight].Orientation.Pitch();
            Joints.toes_R_ry = body.JointOrientations[JointType.FootRight].Orientation.Roll();
            Joints.toes_R_rz = body.JointOrientations[JointType.FootRight].Orientation.Yaw();
            Joints.collar_L_tx = body.Joints[JointType.WristLeft].Position.X;
            Joints.collar_L_ty = body.Joints[JointType.WristLeft].Position.Y;
            Joints.collar_L_tz = body.Joints[JointType.WristLeft].Position.Z;
            Joints.collar_L_rx = body.JointOrientations[JointType.WristLeft].Orientation.Pitch();
            Joints.collar_L_ry = body.JointOrientations[JointType.WristLeft].Orientation.Roll();
            Joints.collar_L_rz = body.JointOrientations[JointType.WristLeft].Orientation.Yaw();
            Joints.upperArm_L_tx = body.Joints[JointType.ShoulderLeft].Position.X;
            Joints.upperArm_L_ty = body.Joints[JointType.ShoulderLeft].Position.Y;
            Joints.upperArm_L_tz = body.Joints[JointType.ShoulderLeft].Position.Z;
            Joints.upperArm_L_rx = body.JointOrientations[JointType.ShoulderLeft].Orientation.Pitch();
            Joints.upperArm_L_ry = body.JointOrientations[JointType.ShoulderLeft].Orientation.Roll();
            Joints.upperArm_L_rz = body.JointOrientations[JointType.ShoulderLeft].Orientation.Yaw();
            Joints.foreArm_L_tx = body.Joints[JointType.ElbowLeft].Position.X;
            Joints.foreArm_L_ty = body.Joints[JointType.ElbowLeft].Position.Y;
            Joints.foreArm_L_tz = body.Joints[JointType.ElbowLeft].Position.Z;
            Joints.foreArm_L_rx = body.JointOrientations[JointType.ElbowLeft].Orientation.Pitch();
            Joints.foreArm_L_ry = body.JointOrientations[JointType.ElbowLeft].Orientation.Roll();
            Joints.foreArm_L_rz = body.JointOrientations[JointType.ElbowLeft].Orientation.Yaw();
            Joints.hand_L_tx = body.Joints[JointType.HandLeft].Position.X;
            Joints.hand_L_ty = body.Joints[JointType.HandLeft].Position.Y;
            Joints.hand_L_tz = body.Joints[JointType.HandLeft].Position.Z;
            Joints.hand_L_rx = body.JointOrientations[JointType.HandLeft].Orientation.Pitch();
            Joints.hand_L_ry = body.JointOrientations[JointType.HandLeft].Orientation.Roll();
            Joints.hand_L_rz = body.JointOrientations[JointType.HandLeft].Orientation.Yaw();
            Joints.upperArm_R_tx = body.Joints[JointType.ShoulderRight].Position.X;
            Joints.upperArm_R_ty = body.Joints[JointType.ShoulderRight].Position.Y;
            Joints.upperArm_R_tz = body.Joints[JointType.ShoulderRight].Position.Z;
            Joints.upperArm_R_rx = body.JointOrientations[JointType.ShoulderRight].Orientation.Pitch();
            Joints.upperArm_R_ry = body.JointOrientations[JointType.ShoulderRight].Orientation.Roll();
            Joints.upperArm_R_rz = body.JointOrientations[JointType.ShoulderRight].Orientation.Yaw();
            Joints.collar_R_tx = body.Joints[JointType.WristRight].Position.X;
            Joints.collar_R_ty = body.Joints[JointType.WristRight].Position.Y;
            Joints.collar_R_tz = body.Joints[JointType.WristRight].Position.Z;
            Joints.collar_R_rx = body.JointOrientations[JointType.WristRight].Orientation.Pitch();
            Joints.collar_R_ry = body.JointOrientations[JointType.WristRight].Orientation.Roll();
            Joints.collar_R_rz = body.JointOrientations[JointType.WristRight].Orientation.Yaw();
            Joints.foreArm_R_tx = body.Joints[JointType.ElbowRight].Position.X;
            Joints.foreArm_R_ty = body.Joints[JointType.ElbowRight].Position.Y;
            Joints.foreArm_R_tz = body.Joints[JointType.ElbowRight].Position.Z;
            Joints.foreArm_R_rx = body.JointOrientations[JointType.ElbowRight].Orientation.Pitch();
            Joints.foreArm_R_ry = body.JointOrientations[JointType.ElbowRight].Orientation.Roll();
            Joints.foreArm_R_rz = body.JointOrientations[JointType.ElbowRight].Orientation.Yaw();
            Joints.hand_R_tx = body.Joints[JointType.HandRight].Position.X;
            Joints.hand_R_ty = body.Joints[JointType.HandRight].Position.Y;
            Joints.hand_R_tz = body.Joints[JointType.HandRight].Position.Z;
            Joints.hand_R_rx = body.JointOrientations[JointType.HandRight].Orientation.Pitch();
            Joints.hand_R_ry = body.JointOrientations[JointType.HandRight].Orientation.Roll();
            Joints.hand_R_rz = body.JointOrientations[JointType.HandRight].Orientation.Yaw();
            return "HARVESTED";
        }
        #endregion
    }
}
