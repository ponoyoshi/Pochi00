using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class Birds : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            FlyingBirds(3500, 7000, 20, 30, 587210, 608543, 30, false, true, 0.02, 0.05);
            FlyingBirds(3500, 7000, 20, 30, 587210, 608543, 30, true, false, 0.02, 0.05);
        }

        private void FlyingBirds(int MinDuration, int MaxDuration, int FlyingSpeed, int Acceleration, int StartTime, int EndTime, int SpriteAmount, bool right, bool left, double ScaleMin, double ScaleMax)
        {
            // normally configurables:

            Vector2 StartPosition = new Vector2(320, 260);
            Vector2 EndPosition = new Vector2(320, 380);
            OsbOrigin ParticleOrigin = OsbOrigin.Centre;
            string ParticlePath = "sb/bird.png";
            bool RandomParticleFade = false;
            double ParticleFadeMin = 1;
            double ParticleFadeMax = 1;
            int FadeTimeIn = 2000;
            int FadeTimeOut = 500;
            bool RandomScale = true;
            bool RandomDuration = true;
            bool Additive = false;
            int NewColorEvery = 1;
            Color4 Color = Color4.White;
            Color4 Color2 = Color4.White;

            // script
            if (StartTime == EndTime)
            {
                StartTime = (int)Beatmap.HitObjects.First().StartTime;
                EndTime = (int)Beatmap.HitObjects.Last().EndTime;
            }

            EndTime = Math.Min(EndTime, (int)AudioDuration);
            StartTime = Math.Min(StartTime, EndTime);

            var layer = GetLayer("");
            using (var pool = new OsbSpritePool(layer, ParticlePath, ParticleOrigin, (sprite, startTime, endTime) =>
            { }))
            {
                var RealTravelTime = RandomDuration ? Random(MinDuration, MaxDuration) : MinDuration;
                for (int i = StartTime; i < (EndTime); i += RealTravelTime / SpriteAmount)
                {
                    var sprite = pool.Get(i, i + RealTravelTime);

                    var RandomScaling = Random(ScaleMin, ScaleMax);
                    var FlipInterval = Random(FlyingSpeed * 12, Acceleration * 8);

                    var lastX = Random(StartPosition.X, EndPosition.X);
                    var lastY = Random(StartPosition.Y, EndPosition.Y);

                    // var rVec = MathHelper.DegreesToRadians(Random(0, AngleShift));
                    // var sVec = Random(FlyingSpeed, FlyingSpeed);
                    // var vX = (Math.Cos(rVec) * sVec) / (FlyingSpeed / 2f);
                    // var vY = (Math.Sin(rVec) * sVec) / (FlyingSpeed / 2f);

                    var rVec = MathHelper.DegreesToRadians(Random(360));
                    var sVec = FlyingSpeed * 2;
                    var vX = Math.Cos(rVec) * sVec;
                    var vY = Math.Sin(rVec) * sVec;

                    var lastAngle = 0d;
                    var timeStep = FlipInterval * Random(1, 3);

                    // Sprite stuff
                    for (var t = i; t < i + RealTravelTime; t += (int)timeStep)
                    {
                        // right
                        if (right)
                        {
                            var nextX = lastX + vX;
                            var nextY = lastY - (vY / 5);

                            var currentAngle = sprite.RotationAt(t);
                            var newAngle = Math.Atan2((nextY - lastY), (nextX - lastX)) + (Math.PI / 2);

                            var startPosition = new Vector2d(lastX, lastY);
                            var endPosition = new Vector2d(lastX, lastY);

                            var angle = Math.Atan2((startPosition.Y - endPosition.Y), (startPosition.X - endPosition.X)) - Math.PI / 2f;

                            sprite.Move(t, t + timeStep, lastX, lastY, nextX, nextY);
                            sprite.Rotate(t, newAngle);

                            if (currentAngle > MathHelper.RadiansToDegrees(0.05))
                            {
                                sprite.Rotate(OsbEasing.OutQuad, t, t + timeStep, currentAngle, newAngle);
                            }

                            else
                            {
                                sprite.Rotate(t + timeStep, newAngle);
                            }

                            if (currentAngle < MathHelper.RadiansToDegrees(-0.05))
                            {
                                sprite.Rotate(OsbEasing.OutQuad, t, t + timeStep, currentAngle, newAngle);
                            }

                            else
                            {
                                sprite.Rotate(t + timeStep, newAngle);
                            }

                            vX += Random(FlyingSpeed) * timeStep / 1000;
                            vY += Random(FlyingSpeed) * timeStep / 1000;

                            lastX = nextX;
                            lastY = nextY;
                            lastAngle = angle;
                        }

                        // left
                        else if (left)
                        {
                            var nextX = lastX - vX;
                            var nextY = lastY - (vY / 15);

                            var currentAngle = sprite.RotationAt(t);
                            var newAngle = Math.Atan2((nextY - lastY), (nextX - lastX)) + (Math.PI / 2);

                            var startPosition = new Vector2d(lastX, lastY);
                            var endPosition = new Vector2d(lastX, lastY);

                            var angle = Math.Atan2((startPosition.Y - endPosition.Y), (startPosition.X - endPosition.X)) - Math.PI / 2f;

                            sprite.Move(t, t + timeStep, lastX, lastY, nextX, nextY);
                            sprite.Rotate(t, newAngle);
                            sprite.FlipH(t, t + timeStep);

                            if (currentAngle > MathHelper.RadiansToDegrees(0.05))
                            {
                                sprite.Rotate(OsbEasing.OutQuad, t, t + timeStep, currentAngle, newAngle);
                            }

                            else
                            {
                                sprite.Rotate(t + timeStep, newAngle);
                            }

                            if (currentAngle < MathHelper.RadiansToDegrees(-0.05))
                            {
                                sprite.Rotate(OsbEasing.OutQuad, t, t + timeStep, currentAngle, newAngle);
                            }

                            else
                            {
                                sprite.Rotate(t + timeStep, newAngle);
                            }

                            vX += Random(FlyingSpeed) * timeStep / 1000;
                            vY += Random(FlyingSpeed) * timeStep / 1000;

                            lastX = nextX;
                            lastY = nextY;
                            lastAngle = angle;
                        }
                    }

                    // Fade stuff
                    var ParticleFade = RandomParticleFade ? Random(ParticleFadeMin, ParticleFadeMax) : ParticleFadeMin;

                    if (i < EndTime - (FadeTimeIn + FadeTimeOut))
                    {
                        sprite.Fade(i, i + FadeTimeIn, 0, ParticleFade);
                        if (i < EndTime - RealTravelTime)
                        {
                            sprite.Fade(i + RealTravelTime - FadeTimeOut, i + RealTravelTime, ParticleFade, 0);
                        }
                        else
                        {
                            sprite.Fade(EndTime - FadeTimeOut, EndTime, ParticleFade, 0);
                        }
                    }

                    else
                    {
                        sprite.Fade(i, 0);
                    }

                    // Color stuff
                    if (i % NewColorEvery == 1)
                    {
                        sprite.Color(i, Color);
                    }

                    else
                    {
                        sprite.Color(i, Color2);
                    }

                    if (Additive)
                    {
                        sprite.Additive(i, i + RealTravelTime);
                    }

                    // Scale (FlippingSpeed) stuff
                    if (ScaleMin != ScaleMax)
                    {
                        if (RandomScale)
                        {
                            if (ScaleMin == ScaleMax && ScaleMin != 1)
                            {
                                sprite.ScaleVec(i, ScaleMin, ScaleMin);
                            }

                            sprite.ScaleVec(i, RandomScaling, RandomScaling);
                            sprite.StartLoopGroup(i, EndTime - StartTime / 2);
                            sprite.ScaleVec(OsbEasing.In, 0, Random(FlipInterval, FlipInterval), RandomScaling - 0.005, RandomScaling, 0, RandomScaling / 2);
                            sprite.ScaleVec(OsbEasing.Out, Random(FlipInterval, FlipInterval), Random(FlipInterval, FlipInterval) * 2, 0, RandomScaling / 2, RandomScaling - 0.005, RandomScaling);
                            sprite.EndGroup();
                        }

                        else
                        {
                            sprite.ScaleVec(i, ScaleMin, ScaleMax);

                            if (ScaleMin == ScaleMax && ScaleMin != 1)
                            {
                                sprite.ScaleVec(i, ScaleMin, ScaleMin);
                            }
                        }
                    }
                }
            }
        }
    }
}
