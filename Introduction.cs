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
            
            ShapeManager shapeManager = new ShapeManager(this);
		    shapeManager.GenerateEmptySquare(new Vector2(320, 240), 11, 1345, 0, 100, false, OsbEasing.OutExpo);
		    shapeManager.GenerateEmptySquare(new Vector2(320, 240), 2678, 5845, 500, 0, true, OsbEasing.InExpo);
            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 5345, 5845, 0, 300, false, OsbEasing.OutExpo);
            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 5511, 5845, 0, 200, false, OsbEasing.OutExpo);
            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 5678, 5845, 0, 100, false, OsbEasing.OutExpo);
            
            FlatBackground flatBackground = new FlatBackground(this);
            flatBackground.GenerateGradientBackground(11, 6678, new Color4(.1f, 0.04f, 0.1f, 1), Color4.Black);

            var slicedCircle = GetLayer("").CreateSprite("sb/cs.png");
            slicedCircle.Fade(11, 2678, 0, 0.3);
            slicedCircle.Rotate(OsbEasing.OutExpo, 11, 2678, 0, Math.PI*2);
            slicedCircle.Rotate(OsbEasing.InExpo, 3345, 5345, Math.PI*2, -Math.PI*2);
            slicedCircle.Scale(OsbEasing.OutExpo, 11, 2678, 0, 0.7);
            slicedCircle.Scale(OsbEasing.InExpo, 3345, 5345, 0.7, 2);

            var dotCircle = GetLayer("").CreateSprite("sb/cs.png");
            slicedCircle.Fade(11, 2678, 0, 0.3);
            slicedCircle.Rotate(OsbEasing.OutExpo, 11, 2678, 0, Math.PI*2);
            slicedCircle.Rotate(OsbEasing.InExpo, 3345, 5345, Math.PI*2, -Math.PI*2);
            slicedCircle.Scale(OsbEasing.OutExpo, 11, 2678, 0, 0.7);
            slicedCircle.Scale(OsbEasing.InExpo, 3345, 5345, 0.7, 2);
        }
    }
}
