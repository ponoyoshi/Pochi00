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
    public class FullSbPart07 : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            OuterCircles(124011, 145345, 2);
            Circles(124011, 145345, 2);
            Text(124011, 145345);

            TransitionManager transitionManager = new TransitionManager(this);
            transitionManager.TransitionLines(144011, 145345, 145345);

            var bitmap = GetMapsetBitmap("sb/p.png");
            var bg = GetLayer("Background").CreateSprite("sb/p.png", OsbOrigin.Centre);
            var Beat = Beatmap.GetTimingPointAt(124011).BeatDuration / 1;

            bg.Scale(124011, 854.0f / bitmap.Width);
            bg.Color(124011, Color4.LightBlue);
            bg.Fade(124011, 124011 + (Beat * 8), 0, 0.13);
            bg.Fade(145345, 145345 + 500, 0.13, 0);
        }

        public void OuterCircles(int StartTime, int EndTime, int Amount)
        {
            var Beat = Beatmap.GetTimingPointAt(StartTime).BeatDuration / 1;
            var Pos = new Vector2(320, 240);
            var ConnectionAngle = Math.PI / Amount;

            double rad;
            double angle = 0;
            double radius = 170;
            for (int i = 0; i < Amount; i++)
            {
                rad = angle * Math.PI / ConnectionAngle;
                var x = (int)radius * (float)Math.Cos(rad) + Pos.X - 2;
                var y = (int)radius * (float)Math.Sin(rad) + Pos.Y;
                var position = new Vector2(x, y);

                var line1 = GetLayer("OuterCircle").CreateSprite("sb/c.png", OsbOrigin.Centre);

                var TravelTime = Beat * 8;
                var duration = EndTime - StartTime;

                line1.StartLoopGroup(StartTime, duration / ((int)TravelTime));
                line1.Fade(0, 1);
                line1.Scale(0, 0.2);
                line1.Scale(TravelTime / 8 - 50, 0.05);
                line1.Scale((TravelTime / 8) * 7 + 50, 0.2);
                line1.Scale(TravelTime, 0.2);
                line1.Color(0, Color4.GreenYellow);
                line1.Color(TravelTime / 8 - 50, Color4.LightBlue);
                line1.Color((TravelTime / 8) * 7 + 50, Color4.GreenYellow);
                line1.Color(TravelTime, Color4.GreenYellow);
                line1.EndGroup();

                var timeStep = 25.5 * 2;
                for (double time = StartTime; time < EndTime; time += timeStep)
                {
                    rad += 0.03;

                    // prev radius
                    x = (int)radius * (float)Math.Cos(rad) + Pos.X - 2;
                    y = (int)radius * (float)Math.Sin(rad) + Pos.Y;

                    var newPos = new Vector2(x, y);
                    var Rotation = Math.Atan2((y - position.Y), (x - position.X)) - Math.PI / 2f;

                    line1.Move(time, time + timeStep, position, newPos);

                    position = newPos;
                }
                angle += ConnectionAngle / (Amount / 2);
            }
        }

        public void Circles(int StartTime, int EndTime, int Amount)
        {
            var Beat = Beatmap.GetTimingPointAt(StartTime).BeatDuration / 1;
            var Pos = new Vector2(320, 240);
            var ConnectionAngle = Math.PI / Amount;

            double rad;
            double angle = 0;
            double radius = 100;
            for (int i = 0; i < Amount; i++)
            {
                rad = angle * Math.PI / ConnectionAngle;
                var x = (int)radius * (float)Math.Cos(rad) + Pos.X - 2;
                var y = (int)radius * (float)Math.Sin(rad) + Pos.Y;
                var position = new Vector2(x, y);
                // new radius
                var x2 = (int)-radius * (float)Math.Cos(rad) + Pos.X - 2;
                var y2 = (int)-radius * (float)Math.Sin(rad) + Pos.Y;
                var nPos = new Vector2(x2, y2);

                var circle1 = GetLayer("Circle1").CreateSprite("sb/c.png", OsbOrigin.Centre);
                var circle2 = GetLayer("Circle2").CreateSprite("sb/c2.png", OsbOrigin.Centre);

                var TravelTime = Beat * 8;
                var duration = EndTime - StartTime;

                circle1.StartLoopGroup(StartTime, duration / ((int)TravelTime));
                circle1.Fade(0, 1);
                circle1.Scale(0, 0.05);
                circle1.Scale(TravelTime / 8 - 50, 0.2);
                circle1.Scale((TravelTime / 8) * 7 + 50, 0.05);
                circle1.Scale(TravelTime, 0.05);
                circle1.Color(0, Color4.LightBlue);
                circle1.Color(TravelTime / 8 - 50, Color4.GreenYellow);
                circle1.Color((TravelTime / 8) * 7 + 50, Color4.LightBlue);
                circle1.Color(TravelTime, Color4.LightBlue);
                circle1.EndGroup();

                circle2.Scale(StartTime, 0.2);
                circle2.Fade(StartTime, EndTime, 1, 1);
                circle2.Move(StartTime, position);



                /*********************************************************************************************/



                var timeStep = 25.5;
                for (double time = StartTime; time < EndTime; time += timeStep)
                {
                    rad += 0.03;

                    // prev radius
                    x = (int)radius * (float)Math.Cos(rad) + Pos.X - 2;
                    y = (int)radius * (float)Math.Sin(rad) + Pos.Y;

                    var newPos = new Vector2(x, y);
                    var Rotation = Math.Atan2((y - position.Y), (x - position.X)) - Math.PI / 2f;

                    circle1.Move(time, time + timeStep, position, newPos);

                    position = newPos;
                }
                angle += ConnectionAngle / (Amount / 2);
            }
        }

        public void Text(int StartTime, int EndTime)
        {
            var position = new Vector2(320, 237);
            var Beat = Beatmap.GetTimingPointAt(StartTime).BeatDuration / 1;

            TextManager textmanager = new TextManager(this);
            textmanager.GenerateRotatingText("alexithymia", StartTime, EndTime, position, 0.16f, (int)Beat * 12, "Regular");
        }
    }
}
