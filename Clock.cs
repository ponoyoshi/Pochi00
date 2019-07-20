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
        int currentScale = 600;
        public override void Generate()
        {
            double beat = Beatmap.GetTimingPointAt(11).BeatDuration;
		    GenerateClock();
            ShowClock(6678, 28095, 92011, 97345, 0.2f);
            SetClockSpeed(6678, 48011, beat*4);
            SetClockSpeed(49345, 70678, beat*2);
            ModifyScale(6761, 28011, 250);
            ModifyScale(28095, 33345, 200);
            ModifyScale(70678, 80011, 150);
            SetClockSpeed(70678, 81345, beat*4);
            ModifyScale(80011, 81345, 200);
            SetClockSpeed(81345, 92011, beat);
            SetClockSpeed(92011, 97345, beat*4);
            SetClockSpeed(168011, 202678, beat);
            


            ShowClock(168011, 169345, 200011, 203345, 0.2f);

            //SONG2///////////////////////////////////////////////////////////////////////////////
            bigHand.Rotate(203419, 0 - 16*((Math.PI*2)/60));
            beat = Beatmap.GetTimingPointAt(203420).BeatDuration;
            ShowClock(203420, 216661, 359006, 374967, 0.2f);
            SetClockSpeed(203420, 216660, beat*2);
            ModifyScale(203420, 216661, 210);
            ChangeHour(216661, 217488, 1, OsbEasing.OutExpo);
            ChangeHour(217488, 217902, -0.5, OsbEasing.OutExpo);
            ChangeHour(217902, 218316, 1, OsbEasing.OutExpo);
            ChangeHour(218316, 219144, 1, OsbEasing.OutElastic);
            ChangeHour(219144, 219557, 0.5, OsbEasing.InExpo);
            ChangeHour(219557, 219971, -0.5, OsbEasing.InExpo);
            ChangeHour(219971, 220799, 1, OsbEasing.OutSine);
            ChangeHour(220799, 221213, 1, OsbEasing.OutExpo);
            ChangeHour(221213, 221626, -1, OsbEasing.OutExpo);
            ChangeHour(221626, 222454, -2, OsbEasing.OutExpo);
            ChangeHour(222454, 222661, 0.2, OsbEasing.OutExpo);
            ChangeHour(222661, 222868, 0.3, OsbEasing.OutExpo);
            ChangeHour(222868, 223075, 0.4, OsbEasing.OutExpo);
            ChangeHour(223075, 223282, 1, OsbEasing.OutExpo);
            ChangeHour(223282, 224109, -1, OsbEasing.InExpo);
            ChangeHour(224109, 224523, 1, OsbEasing.OutElastic);
            ChangeHour(224523, 224937, 1, OsbEasing.In);
            ChangeHour(224937, 225764, 1, OsbEasing.InExpo);
            ChangeHour(225764, 226075, 0.2, OsbEasing.OutExpo);
            ChangeHour(226075, 226385, 0.2, OsbEasing.OutExpo);
            ChangeHour(226385, 226592, 0.2, OsbEasing.OutExpo);
            ChangeHour(226592, 227420, 0.2, OsbEasing.InOutElastic);
            ChangeHour(227420, 227833, 1, OsbEasing.OutExpo);
            ChangeHour(227833, 228247, -1, OsbEasing.OutExpo);
            ChangeHour(228247, 229075, 2, OsbEasing.OutExpo);
            ChangeHour(229075, 229488, 1, OsbEasing.OutExpo);
            ChangeHour(229488, 229902, -1, OsbEasing.InExpo);
            SetClockSpeed(229902, 243144, beat*4);
            SetClockSpeed(243144, 256385, beat*2);

            ChangeHour(256385, 257213, 1, OsbEasing.InExpo);
            ChangeHour(258040, 258868, -1, OsbEasing.InExpo);
            ChangeHour(259695, 260109, 0.2, OsbEasing.OutExpo);
            ChangeHour(260109, 260523, 0.3, OsbEasing.OutExpo);
            ChangeHour(260523, 260937, 0.2, OsbEasing.OutExpo);
            ChangeHour(260937, 261351, 0.1, OsbEasing.OutExpo);
            ChangeHour(261351, 263006, 0.05, OsbEasing.OutExpo);
            ChangeHour(263006, 263833, 1, OsbEasing.InExpo);
            ChangeHour(264661, 265488, -1, OsbEasing.InExpo);
            ChangeHour(266316, 267144, 1, OsbEasing.InExpo);
            ChangeHour(267144, 267557, 1, OsbEasing.OutExpo);
            ChangeHour(267557, 267971, -0.5, OsbEasing.OutExpo);
            ChangeHour(267971, 268799, 0.5, OsbEasing.InExpo);
            ChangeHour(268799, 269213, 0.2, OsbEasing.OutElastic);
            ChangeHour(269213, 269626, 0.4, OsbEasing.OutElastic);
            ChangeHour(269626, 270454, 0.4, OsbEasing.OutSine);
            ChangeHour(270454, 270868, -0.4, OsbEasing.OutSine);
            ChangeHour(270868, 271282, 0.2, OsbEasing.OutSine);
            ChangeHour(271282, 272109, 0.2, OsbEasing.OutExpo);
            ChangeHour(272109, 272523, 0.4, OsbEasing.OutExpo);
            ChangeHour(272523, 272937, 0.1, OsbEasing.OutExpo);
            ChangeHour(272937, 273351, 0.1, OsbEasing.OutExpo);
            ChangeHour(273351, 273764, 0.5, OsbEasing.OutExpo);
            ChangeHour(273764, 274178, 0.3, OsbEasing.OutExpo);
            ChangeHour(274178, 274592, 0.6, OsbEasing.OutExpo);
            ChangeHour(274592, 276247, -1, OsbEasing.InExpo);


            

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
                cadrant[i].ScaleVec(OsbEasing.OutSine, startTime, endTime, cadrant[i].ScaleAt(startTime).X, cadrant[i].ScaleAt(startTime).Y, 1, scale/8);

                angle += (Math.PI*2)/60;
            }
            littleHand.ScaleVec(OsbEasing.OutSine, startTime, endTime, littleHand.ScaleAt(startTime).X, littleHand.ScaleAt(startTime).Y, scale*0.0018, scale*0.0018);
            bigHand.ScaleVec(OsbEasing.OutSine, startTime, endTime, bigHand.ScaleAt(startTime).X, bigHand.ScaleAt(startTime).Y, scale*0.0018, scale*0.0018);
            center.ScaleVec(OsbEasing.OutSine, startTime, endTime, bigHand.ScaleAt(startTime).X, bigHand.ScaleAt(startTime).Y, scale*0.0018, scale*0.0018);
            background.ScaleVec(OsbEasing.OutSine, startTime, endTime, bigHand.ScaleAt(startTime).X, bigHand.ScaleAt(startTime).Y, scale*0.0018, scale*0.0018);



            currentScale = scale;
        }
        private void ChangeHour(int startTime, int endTime, double hour, OsbEasing easing)
        {
            double angle = hour*((Math.PI*2)/12);
            double currentRotation = bigHand.RotationAt(startTime);
            double littleCurrent = littleHand.RotationAt(startTime);
            bigHand.Rotate(easing, startTime, endTime, currentRotation, currentRotation + (angle+(Math.PI*2))*hour);
            littleHand.Rotate(easing, startTime, endTime, littleCurrent, littleCurrent + angle);

        }
    }
}
