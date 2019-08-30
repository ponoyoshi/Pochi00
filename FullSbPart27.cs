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
    public class FullSbPart27 : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            PianoHighlights();
            Background();
            Butterflies();
            Circles();
            Flares(444543, 465877);
            Flares(473877, 497877);
        }

        public void PianoHighlights()
        {
            var pianoHits = new int[]{
                465877, 466043, 466210, 466377, 466543, 466710, 466877, 467043,
                467210, 467377, 467543, 467710, 467877, 468043, 468210, 468377, 468543
            };

            int amount = 2;

            double angle = 0;
            double radius = 60;
            for (var i = 0; i < amount; i++)
            {
                foreach (var hits in pianoHits)
                {
                    var Position = new Vector2(320, 240);
                    var ConnectionAngle = Math.PI / amount;

                    Vector2 position = new Vector2(
                        (float)(320 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius));

                    var layer = GetLayer("");
                    var sprite = layer.CreateSprite("sb/p.png", OsbOrigin.Centre);

                    var timeStep = 100;
                    for (double time = 465877; time < 473877; time += timeStep)
                    {
                        angle += 0.09;

                        Vector2 nPosition = new Vector2(
                            (float)(320 + Math.Cos(angle) * radius),
                            (float)(240 + Math.Sin(angle) * radius)
                        );

                        var distance = Math.Sqrt(Math.Pow(120 - 120, 2) + Math.Pow(200 - 440, 2));
                        var Rotation = Math.Atan2((position.Y - nPosition.Y), (position.X - nPosition.X)) - Math.PI / 2f;
                        sprite.Move(time, time + timeStep, position, nPosition);

                        sprite.Fade(hits, 0.2);
                        sprite.Additive(hits, 473877);
                        sprite.Rotate(hits, Rotation);
                        sprite.ScaleVec(OsbEasing.OutExpo, hits, hits + 1000, 0, 30, distance, 30);
                        if (i % 1 == 1)
                        {
                            sprite.Color(hits, Color4.OrangeRed);
                        }
                        else sprite.Color(hits, Color4.YellowGreen);

                        if (i % 2 == 1)
                        {
                            sprite.Color(469877, Color4.IndianRed);
                        }
                        else sprite.Color(469877, Color4.Cyan);

                        if (i % 2 == 1)
                        {
                            sprite.Color(471877, Color4.IndianRed);
                        }
                        else sprite.Color(471877, Color4.Gold);

                        position = nPosition;
                    }
                    angle += ConnectionAngle / (amount / 2);
                }
            }
        }

        public void Background()
        {
            var bitmap = GetMapsetBitmap("sb/p.png");
            var sprite = GetLayer("bg").CreateSprite("sb/p.png", OsbOrigin.Centre);
            sprite.ScaleVec(465877, 854.0f / bitmap.Width, 480.0f / bitmap.Height);
            sprite.Fade(465877, 473877, 0.2, 0.2);
            sprite.Color(465877, Color4.Gray);
        }

        public void Butterflies()
        {
            var Hits = new int[]{
                465877, 467877, 469877, 471877
            };
            var Radius = Random(5, 30);
            var DistanceFromCentre = 0;

            double rad, x, y;
            int deg;
            for (var i = 0; i < Random(50, 100); i++)
            {
                deg = Random(30, 150);
                rad = deg * Math.PI / 30;
                x = Radius * (float)Math.Cos(rad) + 320;
                y = Radius * (float)Math.Sin(rad) + 240;

                var x2 = DistanceFromCentre * (float)Math.Cos(rad) + 320;
                var y2 = DistanceFromCentre * (float)Math.Sin(rad) + 240;


                var SpriteScaleMin = 0.02;
                var SpriteScaleMax = 0.1;
                var SpriteFadeMin = 0.1;
                var SpriteFadeMax = 0.5;

                var MinTravelTime = 500;
                var MaxTravelTime = 2000;
                var RealScaling = true ? Random(SpriteScaleMin, SpriteScaleMax) : SpriteScaleMin;
                var RealFadeAmount = true ? Random(SpriteFadeMin, SpriteFadeMax) : SpriteFadeMin;
                var RealTravelTime = true ? Random(MinTravelTime, MaxTravelTime) : MinTravelTime;
                var Easing = true ? Random((int)OsbEasing.In, (int)OsbEasing.InExpo) : (int)OsbEasing.In;

                var tick = Beatmap.GetTimingPointAt(0).BeatDuration / 2;
                var RandomTick = Beatmap.GetTimingPointAt(0).BeatDuration / Random(2, 2 / 1.8);

                foreach (var hits in Hits)
                {
                    var sprite = GetLayer("").CreateSprite("sb/s.png", OsbOrigin.Centre);

                    sprite.Scale(hits, hits + (RealTravelTime / 4), 0, RealScaling);
                    sprite.Scale(RealTravelTime - (RealTravelTime / 4), RealTravelTime, RealScaling, 0);

                    var ReverseMovement = false;
                    if (ReverseMovement)
                    {
                        sprite.Move((OsbEasing)Easing, hits, hits + RealTravelTime, x, y, x2, y2);
                        sprite.Rotate(hits, Math.Atan2((y - y2), (x - x2)) - Math.PI / 2f);
                    }

                    else
                    {
                        sprite.Move((OsbEasing)Easing, hits, hits + RealTravelTime, x2, y2, x, y);
                        sprite.Rotate(hits, Math.Atan2((y2 - y), (x2 - x)) - Math.PI / 2f);
                    }

                    var FadeTimeIn = 200;
                    var FadeTimeOut = 200;
                    if (hits < hits + RealTravelTime - (FadeTimeIn + FadeTimeOut))
                    {
                        sprite.Fade(hits, hits + FadeTimeIn, 0, RealFadeAmount);

                        if (hits < hits + RealTravelTime - RealTravelTime)
                        {
                            sprite.Fade(hits + 1000 - FadeTimeOut, hits + RealTravelTime, RealFadeAmount, 0);
                        }

                        else
                        {
                            sprite.Fade(hits + RealTravelTime - FadeTimeOut, hits + RealTravelTime, RealFadeAmount, 0);
                        }

                    }
                    else
                    {
                        sprite.Fade(hits, 0);
                    }
                    sprite.Additive(hits, hits + RealTravelTime);

                    if (i % 1 == 1)
                    {
                        sprite.Color(465877, Color4.OrangeRed);
                    }
                    else sprite.Color(465877, Color4.YellowGreen);

                    if (i % 2 == 1)
                    {
                        sprite.Color(469877, Color4.IndianRed);
                    }
                    else sprite.Color(469877, Color4.Cyan);

                    if (i % 2 == 1)
                    {
                        sprite.Color(471877, Color4.IndianRed);
                    }
                    else sprite.Color(471877, Color4.Gold);
                }
            }
        }

        public void Circles()
        {
            int amount = 6;

            double angle = 0;
            double radius = 350;
            for (var i = 0; i < amount; i++)
            {
                var ConnectionAngle = Math.PI / amount;

                Vector2 position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));


                var layer = GetLayer("");
                var sprite = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre);

                sprite.Scale(OsbEasing.InOutSine, 465877, 468543, 0.3, 0.6);
                sprite.Scale(OsbEasing.InOutSine, 468543, 470043, 0.6, 0.3);
                sprite.Scale(OsbEasing.InOutSine, 470043, 471877, 0.3, 0.6);
                sprite.Scale(OsbEasing.InOutSine, 471877, 473877, 0.6, 0.3);
                sprite.Additive(465877, 473877);
                sprite.Fade(465877, 0.03);

                var timeStep = 130;
                for (double time = 465877; time < 473877; time += timeStep)
                {
                    angle += 0.03;

                    Vector2 nPosition = new Vector2(
                        (float)(320 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius)
                    );

                    sprite.Move(time, time + timeStep, position, nPosition);

                    position = nPosition;
                }
                angle += ConnectionAngle / (amount / 2);
            }
        }

        public void Flares(int startTime, int endTime)
        {
            var sprite = GetLayer("").CreateSprite("sb/flare.jpg", OsbOrigin.Centre);
            var sprite2 = GetLayer("").CreateSprite("sb/flare2.jpg", OsbOrigin.TopLeft);

            var Beat = Beatmap.GetTimingPointAt(startTime).BeatDuration;
            var Fade = 0.8;

            sprite.Scale(startTime, 0.6);
            sprite.Move(startTime, endTime, 727, 40, 747, 40);
            sprite.Additive(startTime, endTime);
            sprite.Fade(startTime, startTime + 2000, 0, Fade);
            sprite.Fade(startTime + 2000, endTime - (Beat * 3), Fade, Fade);
            sprite.Fade(endTime - (Beat * 3), endTime - (Beat * 2), Fade, 0);

            var Rotation = MathHelper.DegreesToRadians(90);
            var Rotation2 = MathHelper.DegreesToRadians(120);
            
            sprite2.Scale(startTime, 0.6);
            sprite2.Move(startTime, endTime, 727, 40, 747, 40);
            sprite2.Additive(startTime, endTime);
            sprite2.Fade(startTime, startTime + 2000, 0, Fade);
            sprite2.Fade(startTime + 2000, endTime - (Beat * 3), Fade, Fade);
            sprite2.Fade(endTime - (Beat * 3), endTime - (Beat * 2), Fade, 0);
            sprite2.Rotate(startTime, endTime, Rotation, Rotation2);
        }
    }
}