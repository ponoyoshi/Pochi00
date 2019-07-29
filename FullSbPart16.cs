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
    public class FullSbPart16 : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            Squares();
            Squares2();
            Squares2Glitch();
            Squares2Glitch2();
            Squares3();
            Blank();
        }

        public void Squares()
        {
            int sqAmount = 2;

            double angle = 0;
            double radius = 120;
            for (var i = 0; i < sqAmount; i++)
            {
                var Position = new Vector2(320, 240);
                var ConnectionAngle = Math.PI / sqAmount;

                Vector2 position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));


                var layer = GetLayer("");
                var sprite = layer.CreateSprite("sb/p.png", OsbOrigin.Centre);

                Vector2 standardScale = new Vector2(100, 150);
                Vector2 skewedScale = new Vector2(30, -150);
                float standardRotation = MathHelper.DegreesToRadians(0);
                float skewedRotation = MathHelper.DegreesToRadians(60);

                sprite.ScaleVec(OsbEasing.InOutSine, 276247, 277902, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 277902, 283178, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 283178, 286075, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 286075, 287833, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 287833, 289488, standardScale, standardScale.X, 0);
                sprite.Rotate(OsbEasing.InOutSine, 276247, 277902, standardRotation, skewedRotation);
                sprite.Rotate(OsbEasing.InOutSine, 277902, 283178, skewedRotation, skewedRotation * 3);
                sprite.Rotate(OsbEasing.InOutSine, 283178, 289488, skewedRotation * 3, standardRotation);
                sprite.Fade(276247, 0.07);

                var timeStep = 50;
                for (double time = 276247; time < 289488; time += timeStep)
                {
                    angle += 0.03;

                    Vector2 nPosition = new Vector2(
                        (float)(320 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius)
                    );

                    // var Rotation = Math.Atan2((position.Y - nPosition.Y), (position.X - nPosition.X)) - Math.PI / 2f;

                    sprite.Move(time, time + timeStep, Position, nPosition);

                    Position = nPosition;
                }
                angle += ConnectionAngle / (sqAmount / 2);
            }
        }

        public void Squares2()
        {
            int sqAmount = 2;

            double angle = 0;
            double radius = 33;
            for (var i = 0; i < sqAmount; i++)
            {
                var Position = new Vector2(320, 240);
                var ConnectionAngle = Math.PI / sqAmount;

                Vector2 position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));


                var layer = GetLayer("");
                var sprite = layer.CreateSprite("sb/p.png", OsbOrigin.Centre);

                Vector2 standardScale = new Vector2(5, 100);
                Vector2 skewedScale = new Vector2(50, -100);
                float standardRotation = MathHelper.DegreesToRadians(60);
                float skewedRotation = MathHelper.DegreesToRadians(120);

                sprite.ScaleVec(OsbEasing.InOutSine, 276247, 277902, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 277902, 283178, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 283178, 286075, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 286075, 287833, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 287833, 289488, standardScale, standardScale.X, 0);
                sprite.Rotate(OsbEasing.InOutSine, 276247, 277902, standardRotation, skewedRotation);
                sprite.Rotate(OsbEasing.InOutSine, 277902, 283178, skewedRotation, skewedRotation * 3);
                sprite.Rotate(OsbEasing.InOutSine, 283178, 289488, skewedRotation * 3, standardRotation);

                sprite.Fade(276247, 0.6);
                sprite.Color(276247, Color4.White);

                // glitch
                sprite.Fade(277902, 1);
                sprite.Color(277902, Color4.IndianRed);
                sprite.Fade(278316, 0.6);
                sprite.Color(278316, Color4.White);
                // glitch
                sprite.Fade(278730, 1);
                sprite.Color(278730, Color4.IndianRed);
                sprite.Fade(278833, 0.6);
                sprite.Color(278833, Color4.White);
                // glitch
                sprite.Fade(278937, 1);
                sprite.Color(278937, Color4.IndianRed);
                sprite.Fade(279040, 0.6);
                sprite.Color(279040, Color4.White);
                // glitch
                sprite.Fade(280385, 1);
                sprite.Color(280385, Color4.IndianRed);
                sprite.Fade(280799, 0.6);
                sprite.Color(280799, Color4.White);
                // glitch
                sprite.Fade(282868, 1);
                sprite.Color(282868, Color4.IndianRed);
                sprite.Fade(283282, 0.6);
                sprite.Color(283695, Color4.White);
                // glitch
                sprite.Fade(284523, 1);
                sprite.Color(284523, Color4.IndianRed);
                sprite.Fade(284575, 0.6);
                sprite.Color(284575, Color4.White);
                // glitch
                sprite.Fade(284626, 1);
                sprite.Color(284626, Color4.IndianRed);
                sprite.Fade(284678, 0);
                sprite.Color(284678, Color4.White);
                // glitch
                sprite.Fade(284730, 1);
                sprite.Color(284730, Color4.IndianRed);
                sprite.Fade(284782, 0.6);
                sprite.Color(284782, Color4.White);
                // glitch
                sprite.Fade(284833, 1);
                sprite.Color(284833, Color4.IndianRed);
                sprite.Fade(284885, 0.6);
                sprite.Color(284885, Color4.White);
                // glitch
                sprite.Fade(284937, 1);
                sprite.Color(284937, Color4.IndianRed);
                sprite.Fade(284988, 0.6);
                sprite.Color(284988, Color4.White);
                // glitch
                sprite.Fade(285040, 1);
                sprite.Color(285040, Color4.IndianRed);
                sprite.Fade(285092, 0.6);
                sprite.Color(285092, Color4.White);
                // glitch
                sprite.Fade(285144, 1);
                sprite.Color(285144, Color4.IndianRed);
                sprite.Fade(285195, 0.6);
                sprite.Color(285195, Color4.White);
                // glitch
                sprite.Fade(285247, 1);
                sprite.Color(285247, Color4.IndianRed);
                sprite.Fade(285299, 0.6);
                sprite.Color(285299, Color4.White);
                // glitch
                sprite.Fade(286178, 1);
                sprite.Color(286178, Color4.IndianRed);
                sprite.Fade(286488, 0.6);
                sprite.Color(286488, Color4.White);
                // glitch
                sprite.Fade(286592, 1);
                sprite.Color(286592, Color4.IndianRed);
                sprite.Fade(287006, 0.6);
                sprite.Color(287006, Color4.White);
                // glitch
                sprite.Fade(287833, 1);
                sprite.Color(287833, Color4.IndianRed);
                sprite.Fade(288144, 0.6);
                sprite.Color(288144, Color4.White);
                // glitch
                sprite.Fade(288247, 1);
                sprite.Color(288247, Color4.IndianRed);
                sprite.Fade(288661, 0.6);
                sprite.Color(288661, Color4.White);

                sprite.Additive(276247, 289488);

                var timeStep = 50;
                for (double time = 276247; time < 289488; time += timeStep)
                {
                    angle += 0.03;

                    Vector2 nPosition = new Vector2(
                        (float)(320 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius)
                    );

                    // var Rotation = Math.Atan2((position.Y - nPosition.Y), (position.X - nPosition.X)) - Math.PI / 2f;

                    sprite.Move(time, time + timeStep, Position, nPosition);

                    Position = nPosition;
                }
                angle += ConnectionAngle / (sqAmount / 2);
            }
        }

        public void Squares2Glitch()
        {
            int sqAmount = 2;

            double angle = 0;
            double radius = 43;
            for (var i = 0; i < sqAmount; i++)
            {
                var Position = new Vector2(320, 240);
                var ConnectionAngle = Math.PI / sqAmount;

                Vector2 position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));


                var layer = GetLayer("");
                var sprite = layer.CreateSprite("sb/p.png", OsbOrigin.Centre);

                Vector2 standardScale = new Vector2(5, 100);
                Vector2 skewedScale = new Vector2(50, -100);
                float standardRotation = MathHelper.DegreesToRadians(60);
                float skewedRotation = MathHelper.DegreesToRadians(120);

                sprite.ScaleVec(OsbEasing.InOutSine, 276247, 277902, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 277902, 283178, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 283178, 286075, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 286075, 287833, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 287833, 289488, standardScale, standardScale.X, 0);
                sprite.Rotate(OsbEasing.InOutSine, 276247, 277902, standardRotation, skewedRotation);
                sprite.Rotate(OsbEasing.InOutSine, 277902, 283178, skewedRotation, skewedRotation * 3);
                sprite.Rotate(OsbEasing.InOutSine, 283178, 289488, skewedRotation * 3, standardRotation);

                sprite.Fade(276247, 0.6);
                sprite.Additive(276247, 289488);

                var timeStep = 50;
                for (double time = 276247; time < 289488; time += timeStep)
                {
                    angle += 0.03;

                    Vector2 nPosition = new Vector2(
                        (float)(320 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius)
                    );

                    // var Rotation = Math.Atan2((position.Y - nPosition.Y), (position.X - nPosition.X)) - Math.PI / 2f;

                    sprite.Move(time, time + timeStep, Position, nPosition);

                    Position = nPosition;
                }
                angle += ConnectionAngle / (sqAmount / 2);
            }
        }

        public void Squares2Glitch2()
        {
            int sqAmount = 2;

            double angle = 0;
            double radius = 53;
            for (var i = 0; i < sqAmount; i++)
            {
                var Position = new Vector2(320, 240);
                var ConnectionAngle = Math.PI / sqAmount;

                Vector2 position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));


                var layer = GetLayer("");
                var sprite = layer.CreateSprite("sb/p.png", OsbOrigin.Centre);

                Vector2 standardScale = new Vector2(15, 120);
                Vector2 skewedScale = new Vector2(70, 120);
                float standardRotation = MathHelper.DegreesToRadians(60);
                float skewedRotation = MathHelper.DegreesToRadians(120);

                sprite.ScaleVec(OsbEasing.InOutSine, 276247, 277902, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 277902, 283178, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 283178, 286075, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 286075, 287833, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 287833, 289488, standardScale, standardScale.X, 0);
                sprite.Rotate(OsbEasing.InOutSine, 276247, 277902, standardRotation, skewedRotation);
                sprite.Rotate(OsbEasing.InOutSine, 277902, 282868, skewedRotation, skewedRotation * 3);
                sprite.Rotate(OsbEasing.InOutSine, 283695, 289488, skewedRotation * 3, standardRotation);

                sprite.Fade(276247, 0);

                // glitch
                sprite.Fade(277902, 0.6);
                sprite.Fade(278316, 0);
                // glitch
                sprite.Fade(278730, 0.6);
                sprite.Fade(278833, 0);
                // glitch
                sprite.Fade(278937, 0.6);
                sprite.Fade(279040, 0);
                // glitch
                sprite.Fade(279557, 0.6);
                sprite.Fade(279695, 0);
                // glitch
                sprite.Fade(279833, 0.6);
                sprite.Fade(279971, 0);
                // glitch
                sprite.Fade(280109, 0.6);
                sprite.Fade(280247, 0);
                // glitch
                sprite.Fade(280385, 0.6);
                sprite.Fade(280799, 0);
                // glitch
                sprite.Fade(282868, 0.6);
                sprite.Fade(283695, 0);
                sprite.Rotate(OsbEasing.InOutSine, 282868, 283282, skewedRotation, standardRotation);
                sprite.Rotate(OsbEasing.InOutSine, 283282, 283695, standardRotation, skewedRotation);
                // glitch
                sprite.Fade(284523, 0.6);
                sprite.Fade(284575, 0);
                // glitch
                sprite.Fade(284626, 0.6);
                sprite.Fade(284678, 0);
                // glitch
                sprite.Fade(284730, 0.6);
                sprite.Fade(284782, 0);
                // glitch
                sprite.Fade(284833, 0.6);
                sprite.Fade(284885, 0);
                // glitch
                sprite.Fade(284937, 0.6);
                sprite.Fade(284988, 0);
                // glitch
                sprite.Fade(285040, 0.6);
                sprite.Fade(285092, 0);
                // glitch
                sprite.Fade(285144, 0.6);
                sprite.Fade(285195, 0);
                // glitch
                sprite.Fade(285247, 0.6);
                sprite.Fade(285299, 0);
                // glitch
                sprite.Fade(286178, 0.6);
                sprite.Fade(286488, 0);
                // glitch
                sprite.Fade(286592, 0.6);
                sprite.Fade(287006, 0);
                // glitch
                sprite.Fade(287833, 0.6);
                sprite.Fade(288144, 0);
                // glitch
                sprite.Fade(288247, 0.6);
                sprite.Fade(288661, 0);

                sprite.Additive(276247, 289488);
                sprite.Color(276247, Color4.Cyan);

                var timeStep = 50;
                for (double time = 276247; time < 289488; time += timeStep)
                {
                    angle += 0.03;

                    Vector2 nPosition = new Vector2(
                        (float)(320 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius)
                    );

                    // var Rotation = Math.Atan2((position.Y - nPosition.Y), (position.X - nPosition.X)) - Math.PI / 2f;

                    sprite.Move(time, time + timeStep, Position, nPosition);

                    Position = nPosition;
                }
                angle += ConnectionAngle / (sqAmount / 2);
            }
        }

        public void Squares3()
        {
            int sqAmount = 2;

            double angle = 0;
            double radius = 200;
            for (var i = 0; i < sqAmount; i++)
            {
                var Position = new Vector2(320, 240);
                var ConnectionAngle = Math.PI / sqAmount;

                Vector2 position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));


                var layer = GetLayer("");
                var sprite = layer.CreateSprite("sb/c3.png", OsbOrigin.Centre);

                Vector2 standardScale = new Vector2(0.4f, 0.4f);
                Vector2 skewedScale = new Vector2(0.01f, -0.4f);
                float standardRotation = MathHelper.DegreesToRadians(0);
                float skewedRotation = MathHelper.DegreesToRadians(60);

                sprite.ScaleVec(OsbEasing.InOutSine, 276247, 277902, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 277902, 283178, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 283178, 286075, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 286075, 287833, skewedScale, standardScale);
                sprite.Rotate(OsbEasing.InOutSine, 276247, 277902, standardRotation, skewedRotation);
                sprite.Rotate(OsbEasing.InOutSine, 277902, 283178, skewedRotation, skewedRotation * 3);
                sprite.Rotate(OsbEasing.InOutSine, 283178, 289488, skewedRotation * 3, standardRotation);
                sprite.Fade(276247, 0.03);

                var timeStep = 50;
                for (double time = 276247; time < 289488; time += timeStep)
                {
                    angle += 0.03;

                    Vector2 nPosition = new Vector2(
                        (float)(320 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius)
                    );

                    // var Rotation = Math.Atan2((position.Y - nPosition.Y), (position.X - nPosition.X)) - Math.PI / 2f;

                    sprite.Move(time, time + timeStep, Position, nPosition);

                    Position = nPosition;
                }
                angle += ConnectionAngle / (sqAmount / 2);
            }
        }

        public void Blank()
        {
            var hits = new int[]{
                277902, 280385, 134595
            };

            foreach (var hit in hits)
            {
                var bitmap = GetMapsetBitmap("sb/p.png");
                var sprite = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre);

                sprite.Additive(hit, hit + 1000);
                sprite.Fade(hit, hit + 1000, 0.3, 0);
                sprite.ScaleVec(hit, 854.0f / bitmap.Width, 480.0f / bitmap.Height);
            }
        }
    }
}
