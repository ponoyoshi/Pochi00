using System;
using OpenTK;
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
}