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
    public void GenerateFairy(int startTime, Vector2 position)
    {
        for(int i = 0; i < 20; i++)
        {
            double angle = generator.Random(0, Math.PI*2);
            var radius = generator.Random(10, 50);

            var endPosition = new Vector2(
                (float)(position.X + Math.Cos(angle) * radius),
                (float)(position.Y + Math.Sin(angle) * radius)
            );

            var particleDuration = generator.Random(5000, 10000);
            var sprite = generator.GetLayer("PARTICLES").CreateSprite("sb/d.png");
            sprite.Fade(startTime, startTime + particleDuration, 1, 0);
            sprite.Scale(startTime, startTime + particleDuration, radius*0.001, 0);
            sprite.Move(OsbEasing.OutExpo, startTime, startTime + particleDuration, position, endPosition);
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
}