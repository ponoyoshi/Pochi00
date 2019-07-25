using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;

namespace StorybrewScripts
{
    public class FullSbPart12 : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            // Calculate distance between two points like this:
            // var distance = Math.Sqrt(Math.Pow(endPosition.Y - startPosition.Y, 2) + Math.Pow(endPosition.X - startPosition.X, 2));

            LineToSquare();
            ClockOuter();
            Circles();
            CirclesHighlight();
            OnlyOneCircle();
            Tunnel();
            ParticleManipulation();

            // true = time
            // false = time2
            LineOnly(true, 218316, 0); // new timing starts here
            LineOnly(true, 218420, 0); // new timing starts here
            LineOnly(true, 218523, 0); // new timing starts here
            LineOnly(true, 218626, 0); // new timing starts here
            LineOnly(true, 218730, 0); // new timing starts here
            LineOnly(true, 218833, 0); // new timing starts here
            LineOnly(true, 218937, 0); // new timing starts here
            LineOnly(false, 0, 226592); // new timing starts here
            LineOnly(false, 0, 226592);
            LineOnly(false, 0, 226695); // new timing starts here
            LineOnly(false, 0, 226695);
            LineOnly(false, 0, 226799); // new timing starts here
            LineOnly(false, 0, 226799);
            LineOnly(false, 0, 226902); // new timing starts here
            LineOnly(false, 0, 226902);
            LineOnly(false, 0, 227006); // new timing starts here
            LineOnly(false, 0, 227006);
            LineOnly(false, 0, 227109); // new timing starts here
            LineOnly(false, 0, 227109);
            LineOnly(false, 0, 227213); // new timing starts here
            LineOnly(false, 0, 227213);
            LineOnly(false, 0, 227316); // new timing starts here
            LineOnly(false, 0, 227316);

            // true = left to right
            // false = right to left
            ParticleBurst(true, 224109, 0, 400);
            ParticleBurst(false, 0, 224523, 400);
            ParticleBurst(true, 225764, 0, 207);
            ParticleBurst(false, 0, 226075, 207);
            ParticleBurst(true, 226385, 0, 104);
        }

        public void LineOnly(bool timeType, int time, int time2)
        {
            var layer = GetLayer("");
            var up = layer.CreateSprite("sb/p.png", OsbOrigin.TopRight);
            var down = layer.CreateSprite("sb/p.png", OsbOrigin.BottomLeft);
            var left = layer.CreateSprite("sb/p.png", OsbOrigin.BottomLeft);
            var right = layer.CreateSprite("sb/p.png", OsbOrigin.TopRight);

            var distanceUp = Math.Sqrt(Math.Pow(120 - 120, 2) + Math.Pow(200 - 440, 2));
            var distanceDown = Math.Sqrt(Math.Pow(360 - 360, 2) + Math.Pow(200 - 440, 2));
            var distanceLeft = Math.Sqrt(Math.Pow(360 - 120, 2) + Math.Pow(200 - 200, 2));
            var distanceRight = Math.Sqrt(Math.Pow(120 - 360, 2) + Math.Pow(440 - 440, 2));

            up.Move(217902, 440, 120);
            up.ScaleVec(OsbEasing.OutBack, 217902, 218316, distanceUp, 50, distanceUp, 1);

            down.Move(217902, 200, 360);
            down.ScaleVec(OsbEasing.OutBack, 217902, 218316, distanceDown, 50, distanceDown, 1);

            left.Move(217902, 200, 120);
            left.ScaleVec(OsbEasing.OutBack, 217902, 218316, distanceLeft, 50, distanceLeft, 1);
            left.Rotate(217902, MathHelper.DegreesToRadians(90));

            right.Move(217902, 440, 360);
            right.ScaleVec(OsbEasing.OutBack, 217902, 218316, distanceRight, 50, distanceRight, 1);
            right.Rotate(217902, MathHelper.DegreesToRadians(90));

            /*********************************************************************************************/

            for (int i = 0; i < 1; i++)
            {
                int endTime = 219971;
                int endTime2 = 227420;
                int radius = 200;
                int radiusNew = -200;
                var oneForth = 104; // this is the time between 1/4 and 2/4
                double angle = Random(0, 360);
                var TravelTime = Random(1000, 3000);

                // start position
                Vector2 Position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius)
                );

                // middle position (if necessary)
                Vector2 mPosition = new Vector2(
                    (float)(320 + Math.Cos(angle)),
                    (float)(240 + Math.Sin(angle))
                );

                // end position
                Vector2 nPosition = new Vector2(
                    (float)(320 + Math.Cos(angle) * radiusNew),
                    (float)(240 + Math.Sin(angle) * radiusNew)
                );

                var distance = Math.Sqrt(Math.Pow(nPosition.Y - Position.Y, 2) + Math.Pow(nPosition.X - Position.X, 2));
                var Rotation = Math.Atan2((nPosition.Y - Position.Y), (nPosition.X - Position.X));

                if (timeType == true)
                {
                    var sprite = layer.CreateSprite("sb/p.png", OsbOrigin.CentreLeft);
                    sprite.Move(time, time + oneForth, Position, mPosition);
                    sprite.Move(OsbEasing.InBack, 219144, endTime, mPosition, nPosition);
                    sprite.ScaleVec(time, time + oneForth, 1, 1, distance / 2, 1);
                    sprite.ScaleVec(OsbEasing.OutQuint, 219144, endTime, distance / 2, 1, 10, 1);
                    sprite.Rotate(time, Rotation);
                    sprite.Fade(time, 1);
                    sprite.Fade(OsbEasing.In, endTime - 200, endTime, 1, 0);
                }

                else
                {
                    var sprite2 = layer.CreateSprite("sb/p.png", OsbOrigin.CentreLeft);
                    sprite2.Move(time2, time2 + oneForth, Position, mPosition);
                    sprite2.ScaleVec(time2, time2 + oneForth, 1, 1, distance / 2, 1);
                    sprite2.Rotate(time2, Rotation);
                    sprite2.Fade(time2, endTime2, 1, 1);
                }
            }
        }

        public void LineToSquare()
        {
            var sprite = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.TopRight);
            var sprite2 = GetLayer("whiteBox").CreateSprite("sb/p.png", OsbOrigin.BottomRight);
            var distanceLength = Math.Sqrt(Math.Pow(120 - 120, 2) + Math.Pow(200 - 440, 2));
            var distanceHeight = Math.Sqrt(Math.Pow(360 - 120, 2) + Math.Pow(200 - 200, 2));

            sprite.Move(216661, 440, 120);
            sprite.ScaleVec(OsbEasing.OutBack, 216661, 217488, 0, 1, distanceLength, 1);
            sprite.ScaleVec(OsbEasing.OutBack, 217488, 217902, distanceLength, 1, distanceLength, distanceHeight);

            sprite2.Move(224937, 440, 360);
            sprite2.ScaleVec(OsbEasing.OutBack, 224937, 225764, distanceLength, 1, distanceLength, distanceHeight);
        }

        public void ClockOuter()
        {
            var layer = GetLayer("");
            var sprite = layer.CreateSprite("sb/cs.png", OsbOrigin.Centre);

            sprite.Move(218316, 320, 240);
            sprite.Scale(218316, 0.45);
            sprite.Fade(218316, 219971, 1, 1);

            sprite.Move(219971, 320, 240);
            sprite.Scale(219971, 0.45);
            sprite.Fade(219971, 220385, 1, 1);
            sprite.Fade(220385, 220385 + 1, 1, 0);
            sprite.Fade(220437, 220592, 1, 1);
            sprite.Fade(220592, 220592 + 1, 1, 0);
            sprite.Fade(220644, 220799, 1, 1);
            sprite.Fade(220799, 220799 + 1, 1, 0);
            sprite.Fade(220851, 221213, 1, 1);
            sprite.Fade(221213, 221213 + 1, 1, 0);
            sprite.Fade(221264, 221626, 1, 1);
            sprite.Fade(221626, 221626 + 1, 1, 0);

            sprite.Fade(227420, 227730, 1, 1);
            sprite.Fade(227730, 227730 + 1, 1, 0);
            sprite.Fade(227833, 228247, 1, 1);
            sprite.Fade(228247, 228247 + 1, 1, 0);
        }

        public void OnlyOneCircle()
        {
            var layer = GetLayer("blackCircle");
            var circle = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre);
            var circle2 = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre);
            var circle3 = GetLayer("innerCircle").CreateSprite("sb/c3.png", OsbOrigin.Centre);
            var circle4 = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre);
            var circle5 = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre);
            var circle6 = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre);
            var circle7 = layer.CreateSprite("sb/c5.png", OsbOrigin.Centre);

            circle.Move(OsbEasing.OutExpo, 219971, 220385, 320, 200, 360, 240);
            circle.Move(OsbEasing.OutExpo, 220385, 220592, 360, 240, 320, 280);
            circle.Move(OsbEasing.OutExpo, 220592, 220799, 320, 280, 280, 240);
            circle.Move(OsbEasing.OutExpo, 220799, 221213, 280, 240, 320, 200);
            circle.Move(OsbEasing.InSine, 221213, 221626, 320, 200, 280, 240);
            circle.Color(219971, Color4.Black);
            circle.Fade(219971, 221626, 1, 1);
            circle.Fade(221626, 227420, 0, 0);
            circle.Scale(219971, 0.3);

            circle2.Move(OsbEasing.InSine, 221213, 221626, 320, 200, 360, 240);
            circle2.Color(219971, Color4.Black);
            circle2.Fade(221213, 221626, 1, 1);
            circle2.Fade(221626, 227420, 0, 0);
            circle2.Scale(219971, 0.3);

            circle4.Move(224937, 320, 240);
            circle4.Scale(OsbEasing.InBounce, 224937, 225764, 0.42, 0.37);
            circle4.Color(224937, Color4.Black);
            circle4.Fade(224937, 225764, 1, 1);
            circle4.Fade(225764, 226592, 0, 0);

            circle3.Move(224937, 320, 240);
            circle3.Scale(OsbEasing.InBounce, 224937, 225764, 0, 0.42);
            circle3.Color(224937, Color4.White);

            circle5.Move(226592, 320, 240);
            circle5.Color(226592, Color4.White);
            circle5.Fade(226592, 227420, 1, 1);
            circle5.Scale(OsbEasing.InOutBounce, 226592, 227420, 0.1, 0.42);

            circle6.Move(OsbEasing.OutExpo, 227420, 227833, 280, 260, 330, 260);
            circle6.Move(OsbEasing.OutExpo, 227833, 228247, 330, 260, 280, 260);
            circle6.Color(227420, Color4.Black);
            circle6.Fade(227420, 228247, 1, 1);
            circle6.Fade(228247, 228248, 0, 0);
            circle6.Scale(227420, 0.3);

            circle7.Move(229695, 320, 240);
            circle7.Scale(OsbEasing.InOutSine, 229695, 230472, 0.42, 0);
            circle7.Color(229695, Color4.Black);
            circle7.Fade(229695 - 200, 229695, 0, 1);
            circle7.Fade(230471, 230471 + 200, 1, 0);
        }

        public void Circles()
        {
            int Amount = 2;
            double radius = 60;
            int StartTime = 217488;
            int EndTime = 218057;
            var Pos = new Vector2(320, 240);
            var ConnectionAngle = Math.PI / Amount;

            double rad;
            double angle = 0;
            for (int i = 0; i < Amount; i++)
            {
                rad = angle * Math.PI / ConnectionAngle;
                var x = (int)radius * (float)Math.Cos(rad) + Pos.X;
                var y = (int)radius * (float)Math.Sin(rad) + Pos.Y;
                var Position = new Vector2(x, y);
                // new radius
                var x2 = (int)-radius * (float)Math.Cos(rad) + Pos.X;
                var y2 = (int)-radius * (float)Math.Sin(rad) + Pos.Y;
                var nPos = new Vector2(x2, y2);

                var circle = GetLayer("").CreateSprite("sb/c3.png", OsbOrigin.Centre);

                circle.Scale(StartTime, 0.24);
                circle.Fade(StartTime, EndTime, 1, 1);
                circle.Move(StartTime, Position);
                circle.Color(StartTime, Color4.Black);

                /*********************************************************************************************/

                var timeStep = 15;
                for (double time = StartTime; time < EndTime; time += timeStep)
                {
                    rad += -0.03;

                    // prev radius
                    x = (int)radius * (float)Math.Cos(rad) + Pos.X;
                    y = (int)radius * (float)Math.Sin(rad) + Pos.Y;

                    var newPos = new Vector2(x, y);
                    var Rotation = Math.Atan2((y - Position.Y), (x - Position.X)) - Math.PI / 2f;

                    circle.Move(time, time + timeStep, Position, newPos);

                    Position = newPos;
                }
                angle += ConnectionAngle / (Amount / 2);
            }

        }

        public void CirclesHighlight()
        {
            var layer = GetLayer("circleMiddle");
            var layer2 = GetLayer("circleOutline");
            var circleMiddle = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre, new Vector2(320, 240));
            var circleOutline = layer.CreateSprite("sb/c2.png", OsbOrigin.Centre, new Vector2(320, 240));
            var circleOutline2 = layer2.CreateSprite("sb/c2.png", OsbOrigin.Centre, new Vector2(320, 240));

            circleMiddle.Scale(OsbEasing.InExpo, 219971, 220385, 0.25, 0);
            circleMiddle.Scale(OsbEasing.InSine, 221213, 221626, 0.25, 0.255);
            circleMiddle.Fade(219971, 221626, 1, 1);
            circleMiddle.Fade(221626, 221627, 1, 0);
            circleMiddle.Scale(227420, 0.25);
            circleMiddle.Fade(227420, 228247, 1, 1);
            circleMiddle.Fade(228247, 228248, 1, 0);

            circleOutline.Scale(OsbEasing.Out, 219971, 220385, 0.35, 0.4);
            circleOutline.Scale(OsbEasing.Out, 220385, 221626, 0.4, 0.4);
            circleOutline.Fade(219971, 221626, 1, 1);
            circleOutline.Fade(221626, 221627, 1, 0);
            circleOutline.Scale(227420, 0.4);
            circleOutline.Fade(227420, 228247, 1, 1);
            circleOutline.Fade(228247, 228248, 1, 0);

            circleOutline2.Scale(220385, 220592, 0.4, 0.4);

            var layer3 = GetLayer("");
            var circleInner = layer3.CreateSprite("sb/c3.png", OsbOrigin.Centre, new Vector2(320, 240));
            var circleOuter = layer3.CreateSprite("sb/c4.png", OsbOrigin.Centre, new Vector2(320, 240));
            var circleOuter2 = layer3.CreateSprite("sb/c4.png", OsbOrigin.Centre, new Vector2(320, 240));
            var circleOuter3 = layer3.CreateSprite("sb/c4.png", OsbOrigin.Centre, new Vector2(320, 240));
            var circleOuter4 = layer3.CreateSprite("sb/c4.png", OsbOrigin.Centre, new Vector2(320, 240));
            var circleOuter5 = layer3.CreateSprite("sb/c2.png", OsbOrigin.Centre, new Vector2(320, 240));

            circleInner.Scale(OsbEasing.OutBack, 223282, 224109, 0.4, 0);
            circleInner.Fade(OsbEasing.InExpo, 223282, 224109, 1, 0);

            circleOuter.Scale(OsbEasing.OutBack, 223282, 224109, 0.4, 0.7);
            circleOuter.StartLoopGroup(223282, 4);
            circleOuter.Fade(OsbEasing.InExpo, 0, 207, 1, 0);
            circleOuter.EndGroup();

            circleOuter2.Scale(OsbEasing.OutBack, 223282, 224109, 0.35, 0.7);
            circleOuter2.StartLoopGroup(223282, 4);
            circleOuter2.Fade(OsbEasing.InExpo, 0, 207, 1, 0);
            circleOuter2.EndGroup();

            circleOuter3.Scale(OsbEasing.OutBack, 223282, 224109, 0.30, 0.7);
            circleOuter3.StartLoopGroup(223282, 4);
            circleOuter3.Fade(OsbEasing.InExpo, 0, 207, 1, 0);
            circleOuter3.EndGroup();

            circleOuter4.Scale(OsbEasing.OutBack, 223282, 224109, 0.25, 0.7);
            circleOuter4.StartLoopGroup(223282, 4);
            circleOuter4.Fade(OsbEasing.InExpo, 0, 207, 1, 0);
            circleOuter4.EndGroup();

            circleOuter5.Scale(OsbEasing.OutElastic, 223488, 223902, 0.90, 0.99);
            circleOuter5.StartLoopGroup(223488, 2);
            circleOuter5.Fade(OsbEasing.InExpo, 0, 207, 1, 0);
            circleOuter5.EndGroup();
        }

        public void Tunnel()
        {
            var layer = GetLayer("");
            var middleTime = 222454;
            var endTime = 223282;
            var sprite1 = layer.CreateSprite("sb/c2.png", OsbOrigin.Centre, new Vector2(320, 240));
            var sprite2 = layer.CreateSprite("sb/c2.png", OsbOrigin.Centre, new Vector2(320, 240));
            var sprite3 = layer.CreateSprite("sb/c2.png", OsbOrigin.Centre, new Vector2(320, 240));
            var sprite4 = layer.CreateSprite("sb/c2.png", OsbOrigin.Centre, new Vector2(320, 240));
            var sprite5 = layer.CreateSprite("sb/c2.png", OsbOrigin.Centre, new Vector2(320, 240));
            var sprite6 = layer.CreateSprite("sb/c2.png", OsbOrigin.Centre, new Vector2(320, 240));

            var sprite7 = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre);
            var sprite8 = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre);
            var sprite9 = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre);

            var sprite10 = layer.CreateSprite("sb/c4.png", OsbOrigin.Centre, new Vector2(320, 240));

            sprite1.Scale(OsbEasing.OutExpo, 221626, middleTime, 0.6, 0.1);
            sprite1.Scale(OsbEasing.OutBack, middleTime, endTime, 0.1, 0.6);

            sprite2.Scale(OsbEasing.OutExpo, 221730, middleTime, 0.6, 0.13);
            sprite2.Scale(OsbEasing.OutBack, middleTime, endTime, 0.13, 0.6);

            sprite3.Scale(OsbEasing.OutExpo, 221833, middleTime, 0.6, 0.16);
            sprite3.Scale(OsbEasing.OutBack, middleTime, endTime, 0.16, 0.6);

            sprite4.Scale(OsbEasing.OutExpo, 221937, middleTime, 0.6, 0.19);
            sprite4.Scale(OsbEasing.OutBack, middleTime, endTime, 0.19, 0.6);

            sprite5.Scale(OsbEasing.OutExpo, 222040, middleTime, 0.6, 0.22);
            sprite5.Scale(OsbEasing.OutBack, middleTime, endTime, 0.22, 0.6);

            sprite6.Scale(OsbEasing.OutExpo, 222247, middleTime, 0.6, 0.25);
            sprite6.Scale(OsbEasing.OutBack, middleTime, endTime, 0.25, 0.6);

            sprite7.Scale(222661, 0.2);
            sprite7.MoveY(222661, 240);
            sprite7.MoveX(OsbEasing.OutExpo, 222661, endTime, 320, 520);
            sprite7.MoveX(OsbEasing.Out, endTime, 223695, 520, 747);
            sprite7.Fade(OsbEasing.OutExpo, endTime, 223695, 1, 0);

            sprite8.Scale(222868, 0.2);
            sprite8.MoveY(222868, 240);
            sprite8.MoveX(OsbEasing.OutExpo, 222868, endTime, 320, 120);
            sprite8.MoveX(OsbEasing.Out, endTime, 223695, 120, -107);
            sprite8.Fade(OsbEasing.OutExpo, endTime, 223695, 1, 0);

            sprite9.MoveY(223075, 240);
            sprite9.Scale(OsbEasing.OutExpo, 223075, endTime, 0.6, 0.4);
            sprite9.Fade(OsbEasing.OutExpo, 223075, endTime, 0, 1);
        }

        // ParticleBurst starts here
        public void ParticleBurst(bool leftToRight, int leftToRightTime, int rightToLeftTime, int Duration)
        {
            var minAmount = 50;
            var maxAmount = 100;
            var Amount = Random(minAmount, maxAmount);
            for (var i = 0; i < Amount; i++) //Generate trail
            {
                float Width = 747;
                float Height = (float)Random(-2, 2);

                var posY = 240;
                var posX = 320 - (Width / 2);
                var Position = new Vector2(posX, posY);
                var spriteSpace = Width / Amount;
                var TrailPosition = new Vector2(Position.X + i * spriteSpace + Random(-5, 5),
                                                Position.Y + Height);

                var layer = GetLayer("");
                var box = layer.CreateSprite("sb/s.png", OsbOrigin.Centre);
                var circle = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre);

                var FadeTime = Duration / (Amount * 2);
                var boxFade = Random(0.1, 0.5);
                var circleFade = Random(0.01, 0.05);
                var boxScale = Random(0.01, 0.05);
                var circleScale = Random(0.05, 0.3);

                if (leftToRight == true)
                {
                    var L = leftToRightTime;

                    box.Scale(L, boxScale);
                    box.Rotate(L, Random(-100, 100));
                    box.Fade(L + FadeTime * i, L + FadeTime * i, 0, boxFade);
                    box.Fade(L + Duration - FadeTime, L + Duration, boxFade, 0);
                    box.Additive(L, L + Duration);
                    box.Move(OsbEasing.OutElastic, L, L + Duration, startTransformBox(TrailPosition).X + (30), startTransformBox(TrailPosition).Y - (20),
                                                                       endTransformBox(TrailPosition).X + (30), endTransformBox(TrailPosition).Y - (20) + Random(-10, 10));

                    // switching start and end positions
                    circle.Scale(L, boxScale);
                    circle.Fade(L + FadeTime * i, L + FadeTime * i, 0, circleFade);
                    circle.Fade(L + Duration - FadeTime, L + Duration, circleFade, 0);
                    circle.Additive(L, L + Duration);
                    circle.Move(OsbEasing.OutBounce, L, L + Duration, startTransformCircle(TrailPosition).X + (650), startTransformCircle(TrailPosition).Y + (450),
                                                                         endTransformCircle(TrailPosition).X + (650), endTransformCircle(TrailPosition).Y + (450) + Random(-20, 20));
                }

                else
                {
                    var R = rightToLeftTime;

                    box.Scale(R, boxScale);
                    box.Rotate(R, Random(-100, 100));
                    box.Fade(R + FadeTime * i, R + FadeTime * i, 0, boxFade);
                    box.Fade(R + Duration - FadeTime, R + Duration, boxFade, 0);
                    box.Additive(R, R + Duration);
                    box.Move(OsbEasing.OutElastic, R, R + Duration, startTransformCircle(TrailPosition).X + (650), startTransformCircle(TrailPosition).Y + (450),
                                                                       endTransformCircle(TrailPosition).X + (650), endTransformCircle(TrailPosition).Y + (450) + Random(-10, 10));

                    // switching start and end positions
                    circle.Scale(R, boxScale);
                    circle.Fade(R + FadeTime * i, R + FadeTime * i, 0, circleFade);
                    circle.Fade(R + Duration - FadeTime, R + Duration, circleFade, 0);
                    circle.Additive(R, R + Duration);
                    circle.Move(OsbEasing.OutBounce, R, R + Duration, startTransformBox(TrailPosition).X + (30), startTransformBox(TrailPosition).Y - (20),
                                                                         endTransformBox(TrailPosition).X + (30), endTransformBox(TrailPosition).Y - (20) + Random(-20, 20));
                }
            }
        }
        private Vector2 startTransformBox(Vector2 position)
        {
            float angle = 0;
            double Rotation = angle / 180 * Math.PI;
            return Vector2.Transform(new Vector2(position.X, position.Y),
                                    Quaternion.FromEulerAngles((float)(Rotation), 0, 0));
        }
        private Vector2 endTransformBox(Vector2 position)
        {
            float angle = 5;
            double Rotation = angle / 180 * Math.PI;
            return Vector2.Transform(new Vector2(position.X, position.Y),
                                    Quaternion.FromEulerAngles((float)(Rotation), 0, 0));
        }
        private Vector2 startTransformCircle(Vector2 position)
        {
            float angle = 180;
            double Rotation = angle / 180 * Math.PI;
            return Vector2.Transform(new Vector2(position.X, position.Y),
                                    Quaternion.FromEulerAngles((float)(Rotation), 0, 0));
        }
        private Vector2 endTransformCircle(Vector2 position)
        {
            float angle = 175;
            double Rotation = angle / 180 * Math.PI;
            return Vector2.Transform(new Vector2(position.X, position.Y),
                                    Quaternion.FromEulerAngles((float)(Rotation), 0, 0));
        }
        // ParticleBurst stuff ends here

        public void ParticleManipulation()
        {
            var Amount = 150;
            var StartTime = 228247;
            var EndTime = 230316;
            for (var i = 0; i < Amount; i++) //Generate trail
            {
                float Width = 854;
                float Height = (float)Random(-190, 190);
                float nHeight = (float)Random(50, 430);
                float nHeight2 = (float)Random(51, 429);

                var posY = 240;
                var posX = 320 - (Width / 2);
                var pos = new Vector2(posX, posY);
                var spriteSpace = Width / Amount;
                var position = new Vector2(pos.X + i * spriteSpace + Random(-5, 5),
                                                pos.Y + Height);

                var layer = GetLayer("ParticleMani...");
                var sprite = layer.CreateSprite("sb/s.png", OsbOrigin.Centre);
                var sprite2 = layer.CreateSprite("sb/ch2.png", OsbOrigin.Centre);

                var Delay = 2;
                var Fade = Random(0.1, 0.5);
                var FadeNew = Random(0.5, 1);
                var Scale = Random(0.03, 0.06);
                var Rotation = MathHelper.DegreesToRadians(100);

                sprite.Additive(StartTime, EndTime);
                sprite.Fade(StartTime + Delay * i, StartTime + 200 + Delay * i, 0, Fade);
                sprite.Fade(229075, 229126, Fade, FadeNew);
                sprite.Fade(229282, 229333, FadeNew, Fade);
                sprite.Fade(229488, 229489, Fade, FadeNew);
                sprite.Fade(229902 + Delay * i, EndTime + Delay * i, FadeNew, 0);

                sprite.Scale(StartTime, Scale);
                sprite.Scale(OsbEasing.Out, 229075, 229282, Scale + 0.08, Scale);
                sprite.Scale(229902, 230523, Scale, 0);
                sprite.Rotate(OsbEasing.OutBack, StartTime, 229075, 0, Random(-Rotation, Rotation));
                sprite.Rotate(OsbEasing.OutBack, 229075, 229282, Random(-Rotation, Rotation), 0);
                sprite.Move(OsbEasing.OutBack, StartTime + (Delay + 6) * i, 229075, new Vector2(800, position.Y), position);
                sprite.Move(229075, 229282 + Delay * i, new Vector2(position.X, nHeight), new Vector2(position.X, nHeight));
                sprite.Move(229282, 229488 + Delay * i, new Vector2(position.X, nHeight2), new Vector2(position.X, nHeight2));
                sprite.Move(OsbEasing.OutBack, 229488 + Delay * i, 229902 + Delay * i, new Vector2(position.X, nHeight2), new Vector2(position.X, Random(230, 250)));
                sprite.Move(OsbEasing.Out, 229902 + Delay * i, EndTime + Delay * i, new Vector2(position.X, Random(230, 250)), new Vector2(position.X, 240));

                // progressive piano after the first wub part
                // var rotation = Random(-Rotation, Rotation);
                // sprite2.Scale(229902, Scale + 0.05);
                // sprite2.Rotate(229902, 256385, rotation, rotation + Random(-10, 10));
                // sprite2.Additive(229902, 256385);
                // sprite2.Fade(229902 + (Delay * 10) * i, 229902 + (Delay * 11) * i, 0, Fade - 0.3);
                // sprite2.Fade(256385 + (Delay * 1) * i, 256385 + (Delay * 2) * i, Fade - 0.3, 0);
                // sprite2.Move(OsbEasing.OutBack, 229902 + (Delay * 10) * i, 229902 + (Delay * 11) * i, new Vector2(800, position.Y), position);
            }
        }
    }
}
