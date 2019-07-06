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

                if (UseHitobjectColor)
                {
                    sprite.Color(hitobject.StartTime, hitobject.Color);
                }

                else
                {
                    sprite.Color(hitobject.StartTime, Color4.White);
                }

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
    }
}
