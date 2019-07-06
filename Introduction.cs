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
    public class Introduction : StoryboardObjectGenerator
    {
        
        public override void Generate()
        {
            
            ShapeManager shapeManager = new ShapeManager(StoryboardObjectGenerator.Current);
		    shapeManager.GenerateEmptySquare(new Vector2(320, 240), 11, 1345, 0, 200, false, OsbEasing.OutExpo);
		    shapeManager.GenerateEmptySquare(new Vector2(320, 240), 2678, 5845, 500, -1000, true, OsbEasing.InExpo);
            new ColorBackground(11, 6678, new Color4(.1f, .1f, .1f, 1), this);
            
        }
    }
}
