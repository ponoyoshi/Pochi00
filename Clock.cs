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
    public class Clock : StoryboardObjectGenerator
    {
        OsbSprite[] cadrant = new OsbSprite[60];
        OsbSprite bigHand;
        OsbSprite littleHand;
        OsbSprite center;
        OsbSprite background;
        int currentScale = 800;
        public override void Generate()
        {
            double beat = Beatmap.GetTimingPointAt(11).BeatDuration;
		    GenerateClock();
            ShowClock(6678, 28095, 70678, 72011, 0.2f);
            SetClockSpeed(6678, 48011, beat*4);
            SetClockSpeed(49345, 70678, beat*2);
            ModifyScale(6761, 28011, 250);
            //ModifyScale(28095, 33345, 200);
        }
        private void GenerateClock()
        {
            double angle = 0;
            for(int i = 0; i < 60; i++)
            {
                var position = new Vector2(
                    (float)(320 + Math.Cos(angle) * currentScale),
                    (float)(240 + Math.Sin(angle) * currentScale)
                );

                var cadrantElement = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre, position);
                cadrantElement.ScaleVec(0, 1, i%12==0 ? 30 : 15);
                cadrantElement.Fade(0, 0);
                cadrantElement.Rotate(0, angle + Math.PI/2);
                angle += (Math.PI*2)/60;
                cadrant[i] = cadrantElement;
            }

            center = GetLayer("").CreateSprite("sb/c.png", OsbOrigin.Centre, new Vector2(320, 240));
            center.ScaleVec(0, currentScale*0.0018, currentScale*0.0018);
            center.Fade(0, 0);
        
            background = GetLayer("").CreateSprite("sb/core.png", OsbOrigin.Centre, new Vector2(320, 240));
            background.ScaleVec(0, currentScale*0.0018, currentScale*0.0018);
            background.Fade(0, 0);

            bigHand = GetLayer("").CreateSprite("sb/ch1.png", OsbOrigin.BottomCentre, new Vector2(320, 240));
            bigHand.ScaleVec(0, currentScale*0.0018, currentScale*0.0018);
            bigHand.Fade(0, 0);

            littleHand = GetLayer("").CreateSprite("sb/ch2.png", OsbOrigin.BottomCentre, new Vector2(320, 240));
            littleHand.ScaleVec(0, currentScale*0.0018, currentScale*0.0018);
            littleHand.Fade(0, 0); 
        }
        private void ShowClock(int startFade, int startTime, int endTime, int endFade, float fade)
        {
            for(int i = 0; i < cadrant.Length; i++)
            {
                cadrant[i].Fade(startFade, startTime, 0, fade);
                cadrant[i].Fade(endTime, endFade, fade, 0);
            }

            center.Fade(startFade, startTime, 0, 0.8);
            center.Fade(endTime, endFade, 0.8, 0);

            bigHand.Fade(startFade, startTime, 0, fade);
            bigHand.Fade(endTime, endFade, fade, 0);

            littleHand.Fade(startFade, startTime, 0, fade);
            littleHand.Fade(endTime, endFade, fade, 0);

            background.Fade(startFade, startTime, 0, fade);
            background.Fade(endTime, endFade, fade, 0);
        }
        private void SetClockSpeed(int startTime, int endTime, double speed)
        {
            double currentRotation = bigHand.RotationAt(startTime);
            double littleCurrent = littleHand.RotationAt(startTime);
            for(double i = startTime; i < endTime; i += speed)
            {
                bigHand.Rotate(OsbEasing.OutElastic, i, i + 100, currentRotation, currentRotation + (Math.PI*2)/60);
                currentRotation += (Math.PI*2)/60;
                
                littleHand.Rotate(OsbEasing.OutElastic, i, i + 100, littleCurrent, littleCurrent + (Math.PI*2)/3600);
                littleCurrent += (Math.PI*2)/3600;    
            }
        }
        private void ModifyScale(int startTime, int endTime, int scale)
        {
            double angle = 0.0;
            for(int i = 0; i < 60; i++)
            {
                var newPosition = new Vector2(
                    (float)(320 + Math.Cos(angle) * scale),
                    (float)(240 + Math.Sin(angle) * scale)
                );

                cadrant[i].Move(OsbEasing.OutSine, startTime, endTime, cadrant[i].PositionAt(startTime), newPosition);
                angle += (Math.PI*2)/60;
            }
            littleHand.ScaleVec(OsbEasing.OutSine, startTime, endTime, littleHand.ScaleAt(startTime).X, littleHand.ScaleAt(startTime).Y, scale*0.0018, scale*0.0018);
            bigHand.ScaleVec(OsbEasing.OutSine, startTime, endTime, bigHand.ScaleAt(startTime).X, bigHand.ScaleAt(startTime).Y, scale*0.0018, scale*0.0018);
            center.ScaleVec(OsbEasing.OutSine, startTime, endTime, bigHand.ScaleAt(startTime).X, bigHand.ScaleAt(startTime).Y, scale*0.0018, scale*0.0018);
            background.ScaleVec(OsbEasing.OutSine, startTime, endTime, bigHand.ScaleAt(startTime).X, bigHand.ScaleAt(startTime).Y, scale*0.0018, scale*0.0018);



            currentScale = scale;
        }
    }
}
