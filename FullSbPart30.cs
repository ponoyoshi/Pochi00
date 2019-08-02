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
        }

        public void Text(OsbEasing Easing, int StartTime, int EndTime, float Size, float Fade, int Speed)
        {
            var position = new Vector2(320, 410);
            var Beat = Beatmap.GetTimingPointAt(StartTime).BeatDuration / 1;

            TextManager textmanager = new TextManager(this);
            textmanager.GenerateRotatingText(Easing, "             ...........             ", StartTime, EndTime, position, Size, Fade, (int)Beat * Speed, "Regular");
        }
    }
}
