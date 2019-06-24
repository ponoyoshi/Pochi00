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

namespace StorybrewScripts
{
    public class DebugTimestamps : StoryboardObjectGenerator
    {
        int oldTime = 11;
        FontGenerator font;
        public override void Generate()
        {
            font = SetFont();
		    GenerateSections();
            
        }
        private FontGenerator SetFont()
        {
            var font = LoadFont("sb/DEBUG/f", new FontDescription{
                FontPath = "Verdana",
                FontSize = 100,
                FontStyle = FontStyle.Regular,
                Color = Color4.White
            });
            return font;
        }
        private void GenerateSections()
        {
            int ID = 0;
            foreach(var time in Beatmap.Bookmarks)
            {
                GenerateSection(oldTime, time, ID);
                oldTime = time;
                ID++;
            }
        }
        private void GenerateSection(int startTime, int endTime, int ID)
        {
            var texture = font.GetTexture($"Section - [{ID}]");
            var sprite = GetLayer("DEBUG").CreateSprite(texture.Path, OsbOrigin.CentreLeft, new Vector2(-100, 430));
            sprite.Fade(startTime, endTime, 1, 1);
            sprite.Scale(startTime, 0.1);
        }
    }
}
