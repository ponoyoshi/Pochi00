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
            HighlightEffect(8, 6678, 28011, "sb/cf.png", 0.5f, 0.8f, 1000, 1, OsbEasing.OutExpo, true);
            
            // Section 14
            HighlightEffect(8, 256385, 272937, "sb/cf.png", 0.5f, 0.8f, 1000, 1, OsbEasing.OutExpo, true);
            
            // Section 19
            HighlightEffect(8, 325902, 332523, "sb/cf.png", 0.5f, 0.8f, 1000, 1, OsbEasing.OutExpo, true);



            // Section 4
            GenerateKiaiHightlight(81345, 92011);
        }

        public void HighlightEffect(int BeatDivisor, int StartTime, int EndTime, string SpritePath, float StartScale, float EndScale, int FadeTime, float Fade, OsbEasing Easing, bool UseHitobjectColor)
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
            foreach(var hitobject in Beatmap.HitObjects)
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
        private void GenerateBeam(int startTime)
        {
            foreach(var hitobject in Beatmap.HitObjects)
            {
                
            }
        }
    }
}
