using System;
using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

public class ParticleManager
{
    private StoryboardObjectGenerator generator;
    public ParticleManager(StoryboardObjectGenerator generator)
    {
        this.generator = generator;
    }
    public void GenerateFairy(double startTime, Vector2 position, int durationMin = 5000, int durationMax = 10000)
    {
        for(int i = 0; i < 20; i++)
        {
            double angle = generator.Random(0, Math.PI*2);
            var radius = generator.Random(10, 50);

            var endPosition = new Vector2(
                (float)(position.X + Math.Cos(angle) * radius),
                (float)(position.Y + Math.Sin(angle) * radius)
            );

            var particleDuration = generator.Random(durationMin, durationMax);
            var sprite = generator.GetLayer("PARTICLES").CreateSprite("sb/d.png");
            sprite.Fade(startTime, startTime + particleDuration, 1, 0);
            sprite.Scale(startTime, startTime + particleDuration, radius*0.001, 0);
            sprite.Move(OsbEasing.OutExpo, startTime, startTime + particleDuration, position, endPosition);
            sprite.Additive(startTime, startTime + particleDuration);
        }
    }
    public void GenerateFog(int startTime, int endTime, int posY, int stroke, int quantity, string layer = "PARTICLES")
    {
        for(int i = 0; i < quantity; i++)
        {
            int firstTimeDuration = generator.Random(1000, 30000);
            int posX = generator.Random(-107, 830);
            int endX = 800;
            int distance = posX - -150;
            int elementStartTime = startTime;
            double fade = generator.Random(0.1, 0.5);

            for(int p = 0; p < 2; p++)
            {
                var particle = generator.GetLayer(layer).CreateSprite("sb/d.png");
                particle.MoveX(startTime, startTime + firstTimeDuration, posX, endX);
                particle.StartLoopGroup(startTime + firstTimeDuration, 3);
                particle.MoveX(0, 0 + generator.Random(15000, 50000), -107, endX);
                particle.EndGroup();
                particle.MoveY(startTime, generator.Random(posY - stroke, posY + stroke));     
                particle.Fade(startTime, startTime + 1000, 0, 1);
                particle.Fade(endTime, endTime + 1000, 1, 0);
                particle.Scale(startTime, generator.Random(0.008, 0.015));
                particle.Color(startTime, Color4.White);
                particle.Additive(startTime, endTime);
            }

            var sprite = generator.GetLayer(layer).CreateSprite($"sb/s/s{generator.Random(0, 9)}.png");
            sprite.MoveX(startTime, startTime + firstTimeDuration, posX, endX);
            sprite.Fade(startTime, startTime + 1000, 0, fade);
            sprite.Fade(endTime, endTime + 1000, fade, 0);
            sprite.Scale(startTime, generator.Random(0.4, 1));
                
            elementStartTime += firstTimeDuration;
            while(elementStartTime < endTime)
            {          
                int newDuration = generator.Random(15000, 50000);
                sprite.MoveX(elementStartTime, elementStartTime + newDuration, -150, endX);       
                sprite.MoveY(elementStartTime, generator.Random(posY - stroke, posY + stroke));
                elementStartTime += newDuration;
            }
        }
    }
    public void GenerateCircleParticles(int startMove, int startTime, int endTime, int endMove)
    {
        for(int i = 0; i < 200; i++)
        {
            var angle = generator.Random(0, Math.PI*2);
            var radius = generator.Random(200, 600);

            var startPosition = new Vector2(
                (float)(320 + Math.Cos(angle) * 500),
                (float)(240 + Math.Sin(angle) * 500)
            );

            var endPosition = new Vector2(
                (float)(320 + Math.Cos(angle) * radius),
                (float)(240 + Math.Sin(angle) * radius)
            );

            var sprite = generator.GetLayer("PARTICLES").CreateSprite("sb/d.png", OsbOrigin.Centre, startPosition);
            sprite.Move(OsbEasing.OutBack, startMove, startTime, startPosition, endPosition);
            sprite.Move(OsbEasing.InBack, endTime, endMove, endPosition, startPosition);
            sprite.Fade(startMove, startTime, 0, 1);
            sprite.Fade(endTime, endMove, 1, 0);
            sprite.Scale(startMove, radius*0.00005);
        }
    }
    public void GenerateDirectionalCross(int startTime, int endTime, int speed, int spawnDelay)
    {
        Vector2 basePosition = new Vector2(320, 240);
        for(int i = 0; i < 4; i++)
        {
            double angle = (Math.PI/2) * i;
            for(int sTime = startTime; sTime < endTime; sTime += spawnDelay)
            {
                var endPosition = new Vector2(
                    (float)(320 + Math.Cos(angle) * 450),
                    (float)(240 + Math.Sin(angle) * 450)
                );

                var sprite = generator.GetLayer("PARTICLES").CreateSprite("sb/p.png", OsbOrigin.Centre);
                sprite.Move(OsbEasing.OutSine, sTime, sTime + speed, basePosition, endPosition);
                sprite.Fade(sTime + speed/6, sTime + speed/2, 0, 1);
                sprite.ScaleVec(sTime, sTime + speed, 10, 1, 10, 0);
                sprite.Rotate(OsbEasing.InSine, sTime, sTime + speed, angle, angle - 1.5);

                angle += Math.PI/60;
            }
        }
    }
    public void GenerateLinesPlane(int startTime, Vector2 position, bool direction)
    {
        var line = generator.GetLayer("PARTICLES").CreateSprite("sb/pl.png", OsbOrigin.CentreRight);
        line.Fade(startTime, startTime + 2000, 1, 0);
        line.ScaleVec(OsbEasing.OutExpo, startTime, startTime + 2000, 3, 0.2, 0, 0);
        line.MoveY(startTime, position.Y);
        line.MoveX(OsbEasing.OutExpo, startTime, startTime + 500, direction == true ? -107 : 745, direction == true ? 1000 : -300);
        
        if(!direction)
            line.Rotate(startTime, Math.PI);

        line.Additive(startTime, startTime + 2000);

        var hl = generator.GetLayer("PARTICLES").CreateSprite("sb/hl.png", OsbOrigin.Centre, position);
        hl.Fade(startTime, startTime + 1000, 1, 0);
        hl.Scale(OsbEasing.OutExpo, startTime, startTime + 1000, 0.2, 0.25);
        hl.Additive(startTime, startTime + 1000);

        var circle = generator.GetLayer("PARTICLES").CreateSprite("sb/c2.png", OsbOrigin.Centre, position);
        circle.Fade(startTime, startTime + 1000, 1, 0);
        circle.Scale(OsbEasing.OutExpo, startTime, startTime + 1000, 0.3, 0.35);
        circle.Additive(startTime, startTime + 1000);

        GenerateFairy(startTime, position, 1000, 3000);
    }
}