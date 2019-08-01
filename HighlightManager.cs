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
    public class HighlightManager : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            // Section 1
            GenerateRing(8, 6678, 28011, "sb/cf.png", 0.5f, 0.8f, 1000, 1, OsbEasing.OutExpo, true);

            // Section 14
            GenerateRing(8, 256385, 272937, "sb/cf.png", 0.5f, 0.8f, 1000, 1, OsbEasing.OutExpo, true);

            // Section 19
            GenerateRing(8, 325902, 332523, "sb/cf.png", 0.5f, 0.8f, 1000, 1, OsbEasing.OutExpo, true);



            // Section 4
            GenerateKiaiHightlight(81345, 92011);
            GenerateBeam(412543, 423210);

            /* GenerateBeam(new int[]{
                81345, 86678, 88011, 89345, 90678, 91011, 91345, 91678, 92011, 82011, 82678, 83345, 83678, 84011
            });*/

            GenerateBeam(81345, 92011);

            // Section 7

            var d = 134678 - 124011;
            var PianoHits = new int[]{
                // part 1
                124011, 124178, 124345, 124511, 124845, 125178, 125345, 125511, 125678, 125845, 126178, 126511,
                126678, 127011, 127178, 127511, 127678, 127845, 128011, 128345, 128511, 128845, 129345, 129511,
                129678, 129845, 130178, 130345, 130511, 130678, 131011, 131178, 131678, 131845, 132011, 132178,
                132345, 132511, 132845, 133011, 133178, 133345, 133511, 133678, 133845,
                // part 2
                124011 + d, 124178 + d, 124345 + d, 124511 + d, 124845 + d, 125178 + d, 125345 + d, 125511 + d, 125678 + d, 125845 + d, 126178 + d, 126511 + d,
                126678 + d, 127011 + d, 127178 + d, 127511 + d, 127678 + d, 127845 + d, 128011 + d, 128345 + d, 128511 + d, 128845 + d, 129345 + d, 129511 + d,
                129678 + d, 129845 + d, 130178 + d, 130345 + d, 130511 + d, 130678 + d, 131011 + d, 131178 + d, 131678 + d, 131845 + d, 132011 + d, 132178 + d,
                132345 + d, 132511 + d, 132845 + d, 133011 + d, 133178 + d, 133345 + d, 133511 + d, 133678 + d, 133845 + d,
            };
            var DumHits = new int[]{
                // drumrolls
                134345, 134428, 134511, 134595
            };

            GenerateVerticalBar(PianoHits, DumHits);
            // I know that it's duplicated xD
            // But effect 

            //Section 21
            GeneratePiano(359006, 376441);

            //Section 23
            List<double> t23 = new List<double>();
            for(double i23 = 380543; i23 < 401877; i23 += Beatmap.GetTimingPointAt(380543).BeatDuration * 1.5f)
            {
                t23.Add(i23);
            }
            foreach(var hitobject in Beatmap.HitObjects)
            {
                foreach(var time in t23)
                {
                    if(hitobject.StartTime > time - 10 && hitobject.StartTime < time + 10)
                    {
                        GenerateCircle(hitobject.StartTime, hitobject.Position);
                    }
                }
            }

        }
        public void GeneratePiano(int startTime, int endTime)
        {
            foreach(var hitobject in Beatmap.HitObjects)
            {
                if ((startTime != 0 || endTime != 0) &&
                    (hitobject.StartTime < startTime - 5 || endTime - 5 <= hitobject.StartTime))
                    continue;

                
                var sprite = GetLayer("").CreateSprite("sb/grad.png", OsbOrigin.CentreLeft, new Vector2(hitobject.Position.X, 450));
                sprite.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 0.5, 0);
                sprite.Rotate(hitobject.StartTime, -Math.PI/2);
                sprite.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 3000, 0.3, 0.5, 0.3, 0);

                var sprite2 = GetLayer("").CreateSprite("sb/grad.png", OsbOrigin.CentreLeft, new Vector2(hitobject.Position.X, 30));
                sprite2.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 0.5, 0);
                sprite2.Rotate(hitobject.StartTime, Math.PI/2);
                sprite2.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 3000, 0.3, 0.5, 0.3, 0);
            }
        }

        public void GenerateRing(int BeatDivisor, int StartTime, int EndTime, string SpritePath, float StartScale, float EndScale, int FadeTime, float Fade, OsbEasing Easing, bool UseHitobjectColor)
        {
            var hitobjectLayer = GetLayer("");
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if ((StartTime != 0 || EndTime != 0) &&
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;

                var sprite = hitobjectLayer.CreateSprite(SpritePath, OsbOrigin.Centre, hitobject.Position);
                sprite.Additive(hitobject.StartTime, hitobject.EndTime + FadeTime);
                sprite.Color(hitobject.StartTime, UseHitobjectColor ? hitobject.Color : Color4.White);

                if (hitobject is OsuSlider)
                {
                    var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / BeatDivisor;
                    var startTime = hitobject.StartTime;
                    while (true)
                    {
                        var endTime = startTime + timestep;

                        var complete = hitobject.EndTime - endTime < 5;
                        if (complete) endTime = hitobject.EndTime;

                        var startPosition = sprite.PositionAt(startTime);
                        sprite.Move(startTime, endTime, startPosition, hitobject.PositionAtTime(endTime));
                        sprite.Fade(Easing, hitobject.EndTime, hitobject.EndTime + FadeTime, Fade, 0);
                        sprite.Scale(Easing, hitobject.EndTime, hitobject.EndTime + FadeTime, StartScale, EndScale);

                        if (complete) break;
                        startTime += timestep;
                    }
                }

                else
                {
                    sprite.Fade(Easing, hitobject.StartTime, hitobject.EndTime + FadeTime, Fade, 0);
                    sprite.Scale(Easing, hitobject.StartTime, hitobject.EndTime + FadeTime, StartScale, EndScale);
                }
            }
        }
        private void GenerateKiaiHightlight(int startTime, int endTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime > startTime - 5 && hitobject.StartTime < endTime + 5)
                {
                    var sprite = GetLayer("").CreateSprite("sb/hl.png", OsbOrigin.Centre, hitobject.Position);
                    sprite.Additive(hitobject.StartTime, hitobject.StartTime + 1000);
                    sprite.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 0.3, 0);
                    sprite.Scale(hitobject.StartTime, 0.3);


                    if (hitobject is OsuSlider)
                    {
                        var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 8;
                        var sTime = hitobject.StartTime;
                        while (true)
                        {
                            var stepSprite = GetLayer("").CreateSprite("sb/hl.png", OsbOrigin.Centre, hitobject.PositionAtTime(sTime));
                            stepSprite.Additive(sTime, sTime + 1000);
                            stepSprite.Fade(sTime, sTime + 1000, 0.3, 0);
                            stepSprite.Scale(sTime, 0.3);

                            if (sTime > hitobject.EndTime)
                                break;

                            sTime += timestep;
                        }
                    }
                }
            }
        }
        private void GenerateBeam(int startTime, int endTime)
        {
            double lastObject = 0;
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime >= startTime && hitobject.StartTime <= endTime)
                {
                    if (hitobject.StartTime - lastObject > 100)
                    {
                        var sprite = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre, hitobject.Position);
                        sprite.Rotate(hitobject.StartTime, Random(-Math.PI / 8, Math.PI / 8));
                        sprite.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 1000, 5, 1000, 0, 1000);
                        sprite.Additive(hitobject.StartTime, hitobject.StartTime + 1000);
                        sprite.Fade(hitobject.StartTime, 0.5);
                    }


                    lastObject = hitobject.StartTime;
                }


                /* foreach(var startTime in startTimes)
                {
                    if(startTime >= hitobject.StartTime - 5 && startTime <= hitobject.StartTime + 5)
                    {
                        var sprite = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre, hitobject.Position);
                        sprite.Rotate(hitobject.StartTime, Random(-Math.PI/8, Math.PI/8));
                        sprite.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 2000, 10, 1000, 0, 1000);
                        sprite.Additive(hitobject.StartTime, hitobject.StartTime + 2000);
                    }
                }*/
            }
        }

        private void GenerateVerticalBar(int[] pianoHits, int[] DumHits)
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (var hit in pianoHits)
                {
                    var position = new Vector2(Random(0, 640), 240);
                    var sprite = GetLayer("PianoHighlights").CreateSprite("sb/p.png", OsbOrigin.Centre, position);

                    sprite.ScaleVec(hit, 40, 480);
                    sprite.Fade(hit, hit + 500, 0.025, 0);
                    sprite.Additive(hit, hit + 500);
                    sprite.Color(hit, Color4.LightBlue);
                }
            }

            for (int i = 0; i < 15; i++)
            {
                foreach (var Hit in DumHits)
                {
                    var position = new Vector2(Random(0, 640), 240);
                    var sprite = GetLayer("PianoHighlights").CreateSprite("sb/p.png", OsbOrigin.Centre, position);

                    sprite.ScaleVec(Hit, 10, 480);
                    sprite.Fade(Hit, Hit + 300, 0.03, 0);
                    sprite.Additive(Hit, Hit + 300);
                    sprite.Color(Hit, Color4.GreenYellow);
                }
            }
        }
        private void GenerateCircle(double startTime, Vector2 position)
        {
            var sprite = GetLayer("").CreateSprite("sb/c2.png", OsbOrigin.Centre, position);
            sprite.Fade(startTime, startTime + 1000, 0.5, 0);
            sprite.ScaleVec(OsbEasing.OutExpo, startTime, startTime + 500, 0, 0.3, position.Y/1000, position.Y/5000);
        }
    }
}
