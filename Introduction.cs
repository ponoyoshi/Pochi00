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
            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 28011, 29345, 0, 300, false, OsbEasing.OutExpo);
            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 28095, 29345, 0, 300, false, OsbEasing.OutExpo);

            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 80011, 81345, 0, 300, false, OsbEasing.OutExpo);
            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 80178, 81345, 0, 290, false, OsbEasing.OutExpo);
            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 80345, 81345, 0, 280, false, OsbEasing.OutExpo);
            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 80511, 81345, 0, 270, false, OsbEasing.OutExpo);
            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 80678, 81345, 0, 260, false, OsbEasing.OutExpo);
            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 81345, 82678, 0, 400, false, OsbEasing.OutExpo);

            
            FlatBackground flatBackground = new FlatBackground(this);
            flatBackground.GenerateGradientBackground(11, 6678, new Color4(.1f, 0.04f, 0.1f, 1), Color4.Black);

            ParticleManager particleManager = new ParticleManager(this);
            particleManager.GenerateFairy(11, new Vector2(320, 240));
            particleManager.GenerateFog(30011, 70678, 320, 50, 20, "FOGBACK");
            particleManager.GenerateFog(30011, 70678, 400, 50, 20, "FOGFRONT");
            particleManager.GenerateFog(81345, 93345, 320, 100, 50, "FOGFRONT");

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

            TextManager textManager = new TextManager(this);
            textManager.GenerateTextVerticlalLetter("A_HISA", 11, 4011, new Vector2(320, 240), 0.2f, "Bold");

            flatBackground.GenerateFlash(70678, 5000);
            flatBackground.GenerateGradientBackground(70678, 81345, new Color4(.05f, 0.1f, 0.2f, 1), Color4.Black);
            
            particleManager.GenerateCircleParticles(70678, 74678, 80011, 81345);
            particleManager.GenerateCircleParticles(102678, 113261, 113428, 124011);

            flatBackground.RoundFade(92011, 102678);
            flatBackground.GenerateGradientBackground(102678, 124011, new Color4(20/255.0f, 0, 20/255.0f, 1), Color4.Black);


            particleManager.GenerateFairy(102678, new Vector2(320, 240));
            particleManager.GenerateDirectionalCross(102678, 121345, 5000, 100);

            //Fairies for circular part
            int[] fairies = {
                113345, 114678, 115345, 116011, 116345, 116678, 117011, 117345, 118011, 118678, 119345, 119678, 120011, 120678, 121345
            };
            GenerateFairies(fairies, particleManager);
            
            TransitionManager transitionManager = new TransitionManager(this);
            transitionManager.TransitionLines(123345, 124011, 124011);


            //3rd BG

            flatBackground.GenerateFlash(145345, 1000);
            flatBackground.GenerateFlash(28011, 1000);
            flatBackground.GenerateFlash(168011, 1000);
            flatBackground.GenerateFlash(189345, 1000);
            flatBackground.GenerateFlash(200011, 3000);
            
            transitionManager.TransitionLines(166345, 166678, 167678);
            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 166678, 168011, 0, 100, false, OsbEasing.OutExpo);

            particleManager.GenerateFog(168011, 200011, 320, 50, 20, "FOGFRONT");
            particleManager.GenerateFog(168011, 200011, 400, 50, 20, "FOGTOP");


            //3dot
            var dot1 = GetLayer("PARTICLES").CreateSprite("sb/c.png");
            var dot2 = GetLayer("PARTICLES").CreateSprite("sb/c.png");
            var dot3 = GetLayer("PARTICLES").CreateSprite("sb/c.png");

            dot1.Fade(167011, 167345, 0, 1);
            dot1.Scale(167011, 0.1);
            dot1.Move(OsbEasing.OutExpo, 167011, 167345, 300, 250, 300, 240);

            dot2.Fade(167345, 167678, 0, 1);
            dot2.Scale(167345, 0.1);
            dot2.Move(OsbEasing.OutExpo, 167345, 167678, 340, 230, 340, 240);

            dot3.Fade(167678, 168011, 0, 1);
            dot3.Scale(OsbEasing.OutExpo, 167678, 168011, 0.3, 0.1);

            dot1.Fade(168011, 168345, 1, 0);
            dot2.Fade(168011, 168345, 1, 0);
            dot3.Fade(168011, 168345, 1, 0);
            dot1.Scale(168011, 168345, 0.1, 0.3);
            dot2.Scale(168011, 168345, 0.1, 0.2);
            dot3.Scale(168011, 168345, 0.1, 0.4);

            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 168011, 169345, 0, 500, false, OsbEasing.OutExpo);
            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 195011, 196011, 0, 300, false, OsbEasing.OutExpo);
            shapeManager.GenerateEmptySquare(new Vector2(320, 240), 194011, 194678, 400, 0, true, OsbEasing.InExpo);

            //PLANES LINES
            int[] linesTimes = {
                145345, 146678, 147345, 148011, 149345, 150011, 150678, 151345, 152011, 152678, 153011, 153345, 154678, 156011, 157345, 158011, 158678,
                159345, 160011, 160678, 161345, 162011, 162678, 163011, 163345, 164011, 164678, 165011, 165345, 148678, 149011, 155345, 157011, 157178,
                159678, 146345, 146511, 155345
            };

            flatBackground.GenerateFlash(156011, 5000);
            flatBackground.GenerateFlash(178678, 2000);
            flatBackground.GenerateFlash(195011, 1000);


            foreach(var hitobject in Beatmap.HitObjects)
            {
                foreach(var time in linesTimes)
                {
                    if(hitobject.StartTime >= time - 5 && hitobject.StartTime <= time + 5)
                    {
                        particleManager.GenerateLinesPlane(time, hitobject.Position, Random(0,10) > 5 ? true : false);
                    }
                }
            }


            //SONG 2////////////////////////////////////////////////////////////////////////////////////////////////////
            flatBackground.GenerateGradientBackground(203420, 216661, new Color4(0.06f, 0.01f, 0.06f, 1), Color4.Black);
            flatBackground.GenerateGradientBackground(216661, 229902, new Color4(0.1f, 0.01f, 0.01f, 1), Color4.Black);
            flatBackground.GenerateGradientBackground(229902, 256385, new Color4(0.01f, 0.01f, 0.06f, 1), Color4.Black);
            flatBackground.GenerateGradientBackground(256385, 272937, new Color4(0.01f, 0.06f, 0.01f, 1), Color4.Black);
            particleManager.GenerateCircleParticles(203420, 216661, 229902, 236523);

        }
        private void GenerateFairies(int[] times, ParticleManager manager)
        {
            foreach(var hitobject in Beatmap.HitObjects)
            {
                foreach(var time in times)
                {
                    if(hitobject.StartTime >= time - 5 && hitobject.StartTime <= time + 5)
                    {
                        manager.GenerateFairy(hitobject.StartTime, hitobject.Position);
                    }
                }
            }
        }
    }
}
