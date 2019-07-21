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

namespace StorybrewScripts
{
    public class FullSbPart12 : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            // Calculate distance between two points like this:
            // var distance = Math.Sqrt(Math.Pow(endPosition.Y - startPosition.Y, 2) + Math.Pow(endPosition.X - startPosition.X, 2));

            LineToSquare();
            LineOnly();
            Time();
            Circles();
            // OnlyOneCircle();
        }

        public void LineToSquare()
        {
            var layer = GetLayer("");
            var sprite = layer.CreateSprite("sb/p.png", OsbOrigin.TopRight);
            var sprite2 = layer.CreateSprite("sb/p.png", OsbOrigin.BottomRight);
            var distanceLength = Math.Sqrt(Math.Pow(120 - 120, 2) + Math.Pow(200 - 440, 2));
            var distanceHeight = Math.Sqrt(Math.Pow(360 - 120, 2) + Math.Pow(200 - 200, 2));
            var distanceHeight2 = Math.Sqrt(Math.Pow(315 - 120, 2) + Math.Pow(200 - 200, 2));

            sprite.Move(216661, 440, 120);
            sprite.ScaleVec(OsbEasing.OutBack, 216661, 217488, 0, 1, distanceLength, 1);
            sprite.ScaleVec(OsbEasing.OutBack, 217488, 217902, distanceLength, 1, distanceLength, distanceHeight);
        }

        public void LineOnly()
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

            var CustomTime = new int[] { 218316, 218420, 218523, 218626, 218730, 218833, 218937 };
            foreach (var time in CustomTime)
            {
                for (int i = 0; i < 1; i++)
                {
                    int startTime = 218316;
                    int endTime = 219971;
                    int radius = 200;
                    int radiusNew = -200;
                    var oneForth = 104; // this is the time between 1/4 and 2/4
                    double angle = Random(0, 360);
                    var TravelTime = Random(1000, 3000);
                    int duration = endTime - startTime;

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

                    var sprite = layer.CreateSprite("sb/p.png", OsbOrigin.CentreLeft);

                    sprite.Move(time, time + oneForth, Position, mPosition);
                    sprite.Move(OsbEasing.InBack, 219144, endTime, mPosition, nPosition);
                    sprite.ScaleVec(time, time + oneForth, 1, 1, distance / 2, 1);
                    sprite.ScaleVec(OsbEasing.OutQuint, 219144, endTime, distance / 2, 1, 10, 1);
                    sprite.Fade(time, 1);
                    sprite.Fade(OsbEasing.In, endTime - 200, endTime, 1, 0);
                    sprite.Rotate(time, Rotation);
                }
            }
        }

        public void Time()
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
        }

        // public void OnlyOneCircle()
        // {
        //     var layer = GetLayer("");
        //     var circle = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre);

        //     circle.Move(OsbEasing.InOutExpo, 219971, 220385, 320, 200, 360, 240);
        //     circle.Move(OsbEasing.InOutExpo, 220385, 220592, 360, 240, 320, 280);
        //     circle.Move(OsbEasing.InOutExpo, 220592, 220799, 320, 280, 280, 240);
        //     circle.Move(OsbEasing.InOutExpo, 220799, 221213, 280, 240, 320, 200);
        //     circle.Move(OsbEasing.InOutExpo, 221213, 221626, 320, 200, 360, 240);
        //     circle.Scale(219971, 0.30);
        //     circle.Color(219971, Color4.Black);
        // }

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
    }
}
