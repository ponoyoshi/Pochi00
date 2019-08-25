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
    public class FullSbPart30 : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            Text(OsbEasing.InOutQuart, 500543, 527210, 0.3f, 0.5f, 20);
            ThreeDSquares(315971, 329213);
            PianoHighlights();
            MovingCameraEffect("sb/c3.png", 527210, 543210, 0.5);
            BG();
        }

        public void BG()
        {
            var bitmap = GetMapsetBitmap("sb/p.png");
            var bg = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre);

            bg.Scale(527210, 854.0f / bitmap.Width);
            // bg.Color(527210, Color4.White);
            bg.Fade(527210, 548543, 0.1, 0.1);
            bg.Fade(527210, 549877, 0.5, 0.1);
            bg.Fade(527210, 553877, 0.1, 0.1);
        }

        public void Text(OsbEasing Easing, int StartTime, int EndTime, float Size, float Fade, int Speed)
        {
            var position = new Vector2(320, 228);
            var Beat = Beatmap.GetTimingPointAt(StartTime).BeatDuration / 1;

            TextManager textmanager = new TextManager(this);
            textmanager.GenerateRotatingText(Easing, "                  ...........                  ", StartTime, EndTime, position, Size, Fade, (int)Beat * Speed, "Regular");
        }

        private void ThreeDSquares(int StartTime, int EndTime)
        {
            var CentreX = 320;
            var CentreY = 240;

            var Radius = 500;
            var TimeBetweenSprites = 50;
            var DistanceFromCentre = 50;

            var SpriteScaleMin = 2;
            var SpriteScaleMax = 10;
            var SpriteFadeMin = 0.1;
            var SpriteFadeMax = 0.8;

            var MinTravelTime = 5000;
            var MaxTravelTime = 10000;
            var FadeTimeIn = 250;
            var FadeTimeOut = 250;

            var RandomScaling = true;
            var RandomScale = true;
            var RandomFade = true;
            var RandomTravelTime = true;
            var ReverseMovement = false;
            var Easing = OsbEasing.None;

            double rad, x, y;
            int deg;
            if (TimeBetweenSprites != 0)
            {
                for (int i = StartTime; i <= EndTime; i += TimeBetweenSprites)
                {
                    deg = Random(30, 150);
                    rad = deg * Math.PI / 30;
                    x = Radius * (float)Math.Cos(rad) + 320;
                    y = Radius * (float)Math.Sin(rad) + 240;

                    var x2 = DistanceFromCentre * (float)Math.Cos(rad) + 320;
                    var y2 = DistanceFromCentre * (float)Math.Sin(rad) + 240;

                    var par = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre);
                    var RealScaling = RandomScaling ? Random(SpriteScaleMin, SpriteScaleMax) : SpriteScaleMin;
                    var RealFadeAmount = RandomFade ? Random(SpriteFadeMin, SpriteFadeMax) : SpriteFadeMin;
                    var RealTravelTime = RandomTravelTime ? Random(MinTravelTime, MaxTravelTime) : MinTravelTime;

                    var Scale = RealScaling;
                    if (RandomScale)
                    {
                        par.ScaleVec(i, i + RealTravelTime / 2, Scale, Scale, Scale, -Scale);
                        par.ScaleVec(i + RealTravelTime / 2, i + RealTravelTime, Scale, -Scale, Scale, Scale);
                        par.Rotate(i, i + RealTravelTime / 2, MathHelper.DegreesToRadians(-90),
                                                             MathHelper.DegreesToRadians(90));
                        par.Rotate(i + RealTravelTime / 2, i + RealTravelTime, MathHelper.DegreesToRadians(90),
                                                             MathHelper.DegreesToRadians(180));
                    }

                    if (ReverseMovement)
                    {
                        par.Move(Easing, i - MaxTravelTime, i + RealTravelTime, x, y, x2, y2);
                    }

                    else
                    {
                        par.Move(Easing, i - MaxTravelTime, i + RealTravelTime, x2, y2, x, y);
                    }

                    if (i < EndTime - (FadeTimeIn + FadeTimeOut))
                    {
                        par.Fade(i, i + FadeTimeIn, 0, RealFadeAmount);

                        if (i < EndTime - RealTravelTime)
                        {
                            par.Fade(i + RealTravelTime - FadeTimeOut, i + RealTravelTime, RealFadeAmount, 0);
                        }

                        else
                        {
                            par.Fade(EndTime - FadeTimeOut, EndTime, RealFadeAmount, 0);
                        }

                    }
                    else
                    {
                        par.Fade(i, 0);
                    }

                    // par.Additive(i, i + RealTravelTime);
                }

            }
            else if (TimeBetweenSprites == 0)
            {





                for (int i = 0; i <= 100; i++)
                {
                    deg = Random(1, 360);
                    rad = deg * Math.PI / 180;
                    x = Random(-108, 750);
                    y = Random(0, 480);

                    var par = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre);
                    var RealScaling = RandomScaling ? Random(SpriteScaleMin, SpriteScaleMax) : SpriteScaleMin;
                    var RealFadeAmount = RandomFade ? Random(SpriteFadeMin, SpriteFadeMax) : SpriteFadeMin;
                    var RealTravelTime = RandomTravelTime ? Random(MinTravelTime, MaxTravelTime) : MinTravelTime;

                    par.Move(Easing, StartTime - MaxTravelTime, EndTime, CentreX, CentreY, CentreX, CentreY);

                    var Scale = RealScaling;
                    if (RandomScale)
                    {
                        par.ScaleVec(i, i + RealTravelTime / 2, Scale, Scale, Scale, -Scale);
                        par.ScaleVec(i + RealTravelTime / 2, i + RealTravelTime, Scale, -Scale, Scale, Scale);
                        par.Rotate(i, i + RealTravelTime / 2, MathHelper.DegreesToRadians(-90),
                                                             MathHelper.DegreesToRadians(90));
                        par.Rotate(i + RealTravelTime / 2, i + RealTravelTime, MathHelper.DegreesToRadians(90),
                                                             MathHelper.DegreesToRadians(180));
                    }

                    if (i < EndTime - (FadeTimeIn + FadeTimeOut))
                    {
                        par.Fade(i, i + FadeTimeIn, 0, RealFadeAmount);

                        if (i < EndTime - RealTravelTime)
                        {
                            par.Fade(i + RealTravelTime - FadeTimeOut, i + RealTravelTime, RealFadeAmount, 0);
                        }

                        else
                        {
                            par.Fade(EndTime - FadeTimeOut, EndTime, RealFadeAmount, 0);
                        }

                    }
                    else
                    {
                        par.Fade(i, 0);
                    }

                    // par.Additive(StartTime, EndTime);
                }
            }
        }

        public void MovingCameraEffect(string spritePath, int startTime, int endTime, double scale)
        {
            Vector2 pos = new Vector2((-107 * 2), 0);
            float spacing = 0.2f;

            var spriteBitmap = GetMapsetBitmap(spritePath);

            while (pos.X < (747 * 2) + spriteBitmap.Height * scale)
            {
                while (pos.Y < (480 * 2) + spriteBitmap.Height * scale)
                {
                    var sprite = GetLayer("").CreateSprite(spritePath, OsbOrigin.Centre);

                    sprite.Fade(startTime, 0.05f);
                    sprite.Fade(531210, 535210, 0.05f, 0.2f);
                    sprite.Fade(542543, endTime, 0.2f, 1);
                    sprite.Fade(endTime, endTime + 1000, 1, 0);
                    sprite.Additive(startTime, endTime);
                    sprite.Scale(startTime, scale - spacing);
                    sprite.Scale(531210, 535210, scale - spacing, scale + 0.05 - spacing);
                    sprite.Scale(OsbEasing.In, 542543, endTime, scale + 0.05 - spacing, scale + 1 - spacing);

                    foreach (var hitobject in Beatmap.HitObjects)
                    {
                        if ((startTime != 0 || endTime != 0) &&
                        (hitobject.StartTime < startTime - 5 || endTime - 5 <= hitobject.StartTime))
                            continue;

                        sprite.Color(hitobject.StartTime, hitobject.Color);
                    }


                    // Camera movement stuff STARTS

                    // no rotations
                    var move1 = new Vector2(transform1(pos).X, transform1(pos).Y);
                    // with rotation (45)
                    var move2 = new Vector2(transform2(pos).X, transform2(pos).Y);
                    // with rotation (-45)
                    var move3 = new Vector2(transform3(pos).X, transform3(pos).Y);
                    // with rotation (-90)
                    var move4 = new Vector2(transform4(pos).X, transform3(pos).Y);

                    // left
                    sprite.Move(OsbEasing.OutSine, startTime, 528210, new Vector2(move1.X - 200, move1.Y), new Vector2(move1.X, move1.Y));
                    // down
                    sprite.Move(OsbEasing.OutSine, 528210, 528543, new Vector2(move1.X, move1.Y), new Vector2(move1.X, move1.Y + 200));
                    // up
                    sprite.Move(OsbEasing.OutSine, 528543, 528877, new Vector2(move1.X, move1.Y + 200), new Vector2(move2.X, move2.Y - 200));
                    // up
                    sprite.Move(OsbEasing.OutSine, 528877, 529210, new Vector2(move2.X, move2.Y - 200), new Vector2(move1.X, move1.Y - 400));
                    // right
                    sprite.Move(OsbEasing.OutSine, 529210, 529543, new Vector2(move1.X, move1.Y - 400), new Vector2(move1.X + 200, move1.Y - 400));
                    // right
                    sprite.Move(OsbEasing.OutSine, 529543, 529877, new Vector2(move1.X + 200, move1.Y - 400), new Vector2(move1.X + 400, move1.Y - 400));
                    // down
                    sprite.Move(OsbEasing.OutSine, 529877, 530210, new Vector2(move1.X + 400, move1.Y - 400), new Vector2(move1.X + 400, move1.Y - 200));
                    // left
                    sprite.Move(OsbEasing.OutSine, 530210, 530543, new Vector2(move1.X + 400, move1.Y - 200), new Vector2(move1.X + 200, move1.Y - 200));
                    // left
                    sprite.Move(OsbEasing.OutSine, 530543, 530877, new Vector2(move1.X + 200, move1.Y - 200), new Vector2(move1.X - 200, move1.Y - 200));
                    // left
                    sprite.Move(OsbEasing.OutSine, 530877, 531210, new Vector2(move1.X - 200, move1.Y - 200), new Vector2(move3.X - 600, move3.Y - 200));
                    // right
                    sprite.Move(OsbEasing.OutSine, 531210, 531543, new Vector2(move3.X - 600, move3.Y - 200), new Vector2(move4.X - 200, move4.Y - 200));
                    // left
                    sprite.Move(OsbEasing.InOutSine, 531543, 531710, new Vector2(move4.X - 200, move4.Y - 200), new Vector2(move3.X - 0, move3.Y - 200));
                    // rotate back to angle 0
                    sprite.Move(OsbEasing.OutExpo, 531710, 532210, new Vector2(move3.X - 0, move3.Y - 200), new Vector2(move1.X - 0, move1.Y - 200));
                    // up
                    sprite.Move(OsbEasing.OutSine, 532210, 532543, new Vector2(move1.X - 0, move1.Y - 200), new Vector2(move1.X - 0, move1.Y - 400));
                    // right
                    sprite.Move(OsbEasing.OutSine, 532543, 532877, new Vector2(move1.X - 0, move1.Y - 400), new Vector2(move1.X + 200, move1.Y - 400));
                    // down
                    sprite.Move(OsbEasing.OutSine, 532877, 533210, new Vector2(move1.X + 200, move1.Y - 400), new Vector2(move1.X + 200, move1.Y - 0));
                    // left
                    sprite.Move(OsbEasing.OutElasticHalf, 533210, 534210, new Vector2(move1.X + 200, move1.Y - 0), new Vector2(move1.X - 200, move1.Y - 0));
                    // up
                    sprite.Move(OsbEasing.OutSine, 534210, 534543, new Vector2(move1.X - 200, move1.Y - 0), new Vector2(move1.X - 200, move1.Y - 200));
                    // up
                    sprite.Move(OsbEasing.OutSine, 534543, 534877, new Vector2(move1.X - 200, move1.Y - 200), new Vector2(move1.X - 200, move1.Y - 400));
                    // down
                    sprite.Move(OsbEasing.OutSine, 534877, 535210, new Vector2(move1.X - 200, move1.Y - 400), new Vector2(move1.X - 200, move1.Y));


                    // part 2 of this section

                    var d = (endTime - startTime) / 2; // duration for half of the section
                    Log(d.ToString());

                    // left
                    sprite.Move(OsbEasing.OutSine, startTime + d, 528210 + d, new Vector2(move1.X - 200, move1.Y), new Vector2(move1.X, move1.Y));
                    // down
                    sprite.Move(OsbEasing.OutSine, 528210 + d, 528543 + d, new Vector2(move1.X, move1.Y), new Vector2(move1.X, move1.Y + 200));
                    // up
                    sprite.Move(OsbEasing.OutSine, 528543 + d, 528877 + d, new Vector2(move1.X, move1.Y + 200), new Vector2(move2.X, move2.Y - 200));
                    // up
                    sprite.Move(OsbEasing.OutSine, 528877 + d, 529210 + d, new Vector2(move2.X, move2.Y - 200), new Vector2(move1.X, move1.Y - 400));
                    // right
                    sprite.Move(OsbEasing.OutSine, 529210 + d, 529543 + d, new Vector2(move1.X, move1.Y - 400), new Vector2(move1.X + 200, move1.Y - 400));
                    // right
                    sprite.Move(OsbEasing.OutSine, 529543 + d, 529877 + d, new Vector2(move1.X + 200, move1.Y - 400), new Vector2(move1.X + 400, move1.Y - 400));
                    // down
                    sprite.Move(OsbEasing.OutSine, 529877 + d, 530210 + d, new Vector2(move1.X + 400, move1.Y - 400), new Vector2(move1.X + 400, move1.Y - 200));
                    // left
                    sprite.Move(OsbEasing.OutSine, 530210 + d, 530543 + d, new Vector2(move1.X + 400, move1.Y - 200), new Vector2(move1.X + 200, move1.Y - 200));
                    // left
                    sprite.Move(OsbEasing.OutSine, 530543 + d, 530877 + d, new Vector2(move1.X + 200, move1.Y - 200), new Vector2(move1.X - 200, move1.Y - 200));
                    // left
                    sprite.Move(OsbEasing.OutSine, 530877 + d, 531210 + d, new Vector2(move1.X - 200, move1.Y - 200), new Vector2(move3.X - 600, move3.Y - 200));
                    // right
                    sprite.Move(OsbEasing.OutSine, 531210 + d, 531543 + d, new Vector2(move3.X - 600, move3.Y - 200), new Vector2(move4.X - 200, move4.Y - 200));
                    // left and rotate to angle -45
                    sprite.Move(OsbEasing.InOutSine, 531543 + d, 531710 + d, new Vector2(move4.X - 200, move4.Y - 200), new Vector2(move3.X - 0, move3.Y - 200));
                    // rotate back to angle 0
                    sprite.Move(OsbEasing.OutSine, 531710 + d, 532210 + d, new Vector2(move3.X - 0, move3.Y - 200), new Vector2(move1.X - 0, move1.Y - 200));
                    // up
                    sprite.Move(OsbEasing.OutSine, 532210 + d, 532543 + d, new Vector2(move1.X - 0, move1.Y - 200), new Vector2(move1.X - 0, move1.Y - 400));
                    // right
                    sprite.Move(OsbEasing.OutSine, 532543 + d, 532877 + d, new Vector2(move1.X - 0, move1.Y - 400), new Vector2(move1.X + 200, move1.Y - 400));
                    // down
                    sprite.Move(OsbEasing.OutSine, 532877 + d, 533210 + d, new Vector2(move1.X + 200, move1.Y - 400), new Vector2(move1.X + 200, move1.Y - 0));
                    // left
                    sprite.Move(OsbEasing.OutElasticHalf, 533210 + d, 534210 + d, new Vector2(move1.X + 200, move1.Y - 0), new Vector2(move1.X - 200, move1.Y - 0));

                    // Camera movement stuff ENDS


                    pos.Y += spriteBitmap.Height * (float)scale;
                }
                pos.Y = 0;
                pos.X += spriteBitmap.Height * (float)scale;
            }
        }

        private Vector2 transform1(Vector2 position)
        {
            float angle = 0;
            double Rotation = angle / 180 * Math.PI;
            return Vector2.Transform(new Vector2(position.X, position.Y),
                                    Quaternion.FromEulerAngles((float)(Rotation), 0, 0));
        }

        private Vector2 transform2(Vector2 position)
        {
            float angle = 45;
            double Rotation = angle / 180 * Math.PI;
            return Vector2.Transform(new Vector2(position.X, position.Y),
                                    Quaternion.FromEulerAngles((float)(Rotation), 0, 0));
        }

        private Vector2 transform3(Vector2 position)
        {
            float angle = -45;
            double Rotation = angle / 180 * Math.PI;
            return Vector2.Transform(new Vector2(position.X, position.Y),
                                    Quaternion.FromEulerAngles((float)(Rotation), 0, 0));
        }

        private Vector2 transform4(Vector2 position)
        {
            float angle = -90;
            double Rotation = angle / 180 * Math.PI;
            return Vector2.Transform(new Vector2(position.X, position.Y),
                                    Quaternion.FromEulerAngles((float)(Rotation), 0, 0));
        }

        public void PianoHighlights()
        {
            // left
            var pianoHits = new int[]{
                543210, 544877, 546210, 546877, 547377, 547877, 548543, 549210, 550210, 551210, 552210, 552877
            };

            // middle
            var pianoHits2 = new int[]{
                543877, 545210, 545877, 546710, 547210, 547710, 549043, 549710, 550877, 551877, 552377, 553210
            };

            // right
            var pianoHits3 = new int[]{
                544543, 545543, 546543, 547043, 547543, 548210, 548877, 549877, 550543, 551543, 552543
            };

            var speed = 100;
            var pD = 50; //pD stands for preDelay in this case
            var easingStart = OsbEasing.OutExpo;
            var easingEnd = OsbEasing.InSine;
            var layer = GetLayer("");
            var circleBitmap = GetMapsetBitmap("sb/c3.png");
            var heightMax = Math.Sqrt(Math.Pow(190 - 290, 2) + Math.Pow(0 - 0, 2));

            var circle = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre, new Vector2(320 - 200, 240 - 50));
            var circle2 = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre, new Vector2(320, 240 - 50));
            var circle3 = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre, new Vector2(320 + 200, 240 - 50));
            var circle4 = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre, new Vector2(320 - 200, 240 + 50));
            var circle5 = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre, new Vector2(320, 240 + 50));
            var circle6 = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre, new Vector2(320 + 200, 240 + 50));

            var sprite = layer.CreateSprite("sb/p.png", OsbOrigin.TopCentre, new Vector2(320 - 200, 240 - 50));
            var sprite2 = layer.CreateSprite("sb/p.png", OsbOrigin.TopCentre, new Vector2(320, 240 - 50));
            var sprite3 = layer.CreateSprite("sb/p.png", OsbOrigin.TopCentre, new Vector2(320 + 200, 240 - 50));

            var arc = layer.CreateSprite("sb/c6.png", OsbOrigin.Centre, new Vector2(320 - 200, 240 + 50));
            var arc2 = layer.CreateSprite("sb/c6.png", OsbOrigin.Centre, new Vector2(320, 240 + 50));
            var arc3 = layer.CreateSprite("sb/c6.png", OsbOrigin.Centre, new Vector2(320 + 200, 240 + 50));

            foreach (var hits in pianoHits)
            {
                circle.Fade(543210, 553293, 1, 1);
                circle.ScaleVec(543210, 0.2, 0.2);

                circle4.Fade(543210, 553293, 1, 1);
                circle4.ScaleVec(543210, 0.2, 0.2);
                circle4.MoveY(easingStart, hits - pD, hits - pD + speed, 240 - 50, 240 + 50);
                circle4.MoveY(easingEnd, hits - pD + speed, hits - pD + (speed * 2), 240 + 50, 240 - 50);

                arc.Fade(hits - pD + speed, hits - pD + (speed) * 2, 1, 0);
                arc.ScaleVec(hits - pD + speed, hits - pD + (speed) * 2, 0.2, 0.2, 0.235, 0.2);
                arc.MoveY(OsbEasing.In, hits - pD + speed, hits - pD + (speed) * 2, 240 + 50, 240 + 70);

                foreach (var hitobject in Beatmap.HitObjects)
                {
                    if ((hits - pD != 0 || hits - pD + (speed * 2) != 0) &&
                    (hitobject.StartTime < hits - pD - 5 || hits - pD + (speed * 2) - 5 <= hitobject.StartTime))
                        continue;

                    circle.Color(hits - pD, hits - pD + speed, hitobject.Color, hitobject.Color);
                    circle4.Color(hits - pD, hits - pD + speed, hitobject.Color, hitobject.Color);
                    arc.Color(hits - pD + speed, hitobject.Color);
                }
            }

            foreach (var hits in pianoHits2)
            {
                circle2.Fade(543210, 553293, 1, 1);
                circle2.ScaleVec(543210, 0.2, 0.2);

                circle5.Fade(543210, 553293, 1, 1);
                circle5.ScaleVec(543210, 0.2, 0.2);
                circle5.MoveY(easingStart, hits - pD, hits - pD + speed, 240 - 50, 240 + 50);
                circle5.MoveY(easingEnd, hits - pD + speed, hits - pD + (speed * 2), 240 + 50, 240 - 50);

                arc2.Fade(hits - pD + speed, hits - pD + (speed) * 2, 1, 0);
                arc2.ScaleVec(hits - pD + speed, hits - pD + (speed) * 2, 0.2, 0.2, 0.235, 0.2);
                arc2.MoveY(OsbEasing.In, hits - pD + speed, hits - pD + (speed) * 2, 240 + 50, 240 + 70);

                foreach (var hitobject in Beatmap.HitObjects)
                {
                    if ((hits - pD != 0 || hits - pD + (speed * 2) != 0) &&
                    (hitobject.StartTime < hits - pD - 5 || hits - pD + (speed * 2) - 5 <= hitobject.StartTime))
                        continue;

                    circle2.Color(hits - pD, hits - pD + speed, hitobject.Color, hitobject.Color);
                    circle5.Color(hits - pD, hits - pD + speed, hitobject.Color, hitobject.Color);
                    arc2.Color(hits - pD + speed, hitobject.Color);
                }
            }

            foreach (var hits in pianoHits3)
            {
                circle3.Fade(543210, 553293, 1, 1);
                circle3.ScaleVec(543210, 0.2, 0.2);

                circle6.Fade(543210, 553293, 1, 1);
                circle6.ScaleVec(543210, 0.2, 0.2);
                circle6.MoveY(easingStart, hits - pD, hits - pD + speed, 240 - 50, 240 + 50);
                circle6.MoveY(easingEnd, hits - pD + speed, hits - pD + (speed * 2), 240 + 50, 240 - 50);

                arc3.Fade(hits - pD + speed, hits - pD + (speed) * 2, 1, 0);
                arc3.ScaleVec(hits - pD + speed, hits - pD + (speed) * 2, 0.2, 0.2, 0.235, 0.2);
                arc3.MoveY(OsbEasing.In, hits - pD + speed, hits - pD + (speed) * 2, 240 + 50, 240 + 70);

                foreach (var hitobject in Beatmap.HitObjects)
                {
                    if ((hits - pD != 0 || hits - pD + (speed * 2) != 0) &&
                    (hitobject.StartTime < hits - pD - 5 || hits - pD + (speed * 2) - 5 <= hitobject.StartTime))
                        continue;

                    circle3.Color(hits - pD, hits - pD + speed, hitobject.Color, hitobject.Color);
                    circle6.Color(hits - pD, hits - pD + speed, hitobject.Color, hitobject.Color);
                    arc3.Color(hits - pD + speed, hitobject.Color);
                }
            }



            foreach (var hits in pianoHits)
            {
                sprite.Fade(543210, 553293, 1, 1);
                sprite.ScaleVec(easingStart, hits - pD, hits - pD + speed, 108.6, 0, 108.6, heightMax);
                sprite.ScaleVec(easingEnd, hits - pD + speed, hits - pD + (speed * 2), 108.6, heightMax, 108.6, 0);

                foreach (var hitobject in Beatmap.HitObjects)
                {
                    if ((hits - pD != 0 || hits - pD + (speed * 2) != 0) &&
                    (hitobject.StartTime < hits - pD - 5 || hits - pD + (speed * 2) - 5 <= hitobject.StartTime))
                        continue;

                    sprite.Color(hits - pD, hits - pD + speed, hitobject.Color, hitobject.Color);
                }
            }

            foreach (var hits in pianoHits2)
            {
                sprite2.Fade(543210, 553293, 1, 1);
                sprite2.ScaleVec(easingStart, hits - pD, hits - pD + speed, 108.6, 0, 108.6, heightMax);
                sprite2.ScaleVec(easingEnd, hits - pD + speed, hits - pD + (speed * 2), 108.6, heightMax, 108.6, 0);

                foreach (var hitobject in Beatmap.HitObjects)
                {
                    if ((hits - pD != 0 || hits - pD + (speed * 2) != 0) &&
                    (hitobject.StartTime < hits - pD - 5 || hits - pD + (speed * 2) - 5 <= hitobject.StartTime))
                        continue;

                    sprite2.Color(hits - pD, hits - pD + speed, hitobject.Color, hitobject.Color);
                }
            }

            foreach (var hits in pianoHits3)
            {
                sprite3.Fade(543210, 553293, 1, 1);
                sprite3.ScaleVec(easingStart, hits - pD, hits - pD + speed, 108.6, 0, 108.6, heightMax);
                sprite3.ScaleVec(easingEnd, hits - pD + speed, hits - pD + (speed * 2), 108.6, heightMax, 108.6, 0);

                foreach (var hitobject in Beatmap.HitObjects)
                {
                    if ((hits - pD != 0 || hits - pD + (speed * 2) != 0) &&
                    (hitobject.StartTime < hits - pD - 5 || hits - pD + (speed * 2) - 5 <= hitobject.StartTime))
                        continue;

                    sprite3.Color(hits - pD, hits - pD + speed, hitobject.Color, hitobject.Color);
                }
            }
        }
    }
}
