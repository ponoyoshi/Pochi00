using System;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using OpenTK;

public class ShapeManager
{
    private StoryboardObjectGenerator generator;
    public ShapeManager(StoryboardObjectGenerator generator)
    {
        this.generator = generator;
    }
    public void GenerateEmptySquare(Vector2 position, int startTime, int endTime, double startScale, double endScale, bool upScale, OsbEasing easing = OsbEasing.OutExpo)
    {
        double angle = 0;
        for(int i = 0; i < 4; i++)
        {
            var startPosition = new Vector2(
                (float)(position.X + Math.Cos(angle) * startScale),
                (float)(position.Y + Math.Sin(angle) * startScale)
            );

            var endPosition = new Vector2(
                (float)(position.X + Math.Cos(angle) * endScale),
                (float)(position.Y + Math.Sin(angle) * endScale)
            );

            double startBorderScale = Math.Sqrt((startScale * startScale) + (startScale * startScale));
            double endBorderScale = Math.Sqrt((endScale * endScale) + (endScale * endScale));

            var sprite = generator.GetLayer("SHAPES").CreateSprite("sb/p.png", OsbOrigin.BottomCentre, startPosition);
            sprite.ScaleVec(easing, startTime, endTime, upScale ? 0 : 50, startBorderScale + (upScale ? 0 : 25), upScale ? 50 : 0, endBorderScale + (upScale ? 25 : 0));
            sprite.Rotate(startTime, angle - Math.PI/4);
            sprite.Move(easing, startTime, endTime, startPosition, endPosition);
            angle += Math.PI/2;
        }
    }
    public void GenerateGears(int startTime, int endTime, int gearNumber)
    {   
        float colorDark = 0.05f;
        double maxScale = 0.4;
        for(int i = 0; i < gearNumber; i ++)
        {  
            int baseYPos = generator.Random(0, 480);
            int duration = generator.Random(15000, 30000);
            bool isLeft = generator.Random(0,2) == 0 ? true : false;
            var sprite = generator.GetLayer("SHAPES").CreateSprite($"sb/g/g{generator.Random(1, 7)}.png", OsbOrigin.Centre, new Vector2(i % 2 == 0 ? -107 : 747, baseYPos));
            sprite.Fade(startTime + (i * 50), startTime + (i * 50) + 300, 0, 1);
            sprite.Fade(endTime - 1000, endTime, 1, 0);
            sprite.Scale(startTime, generator.Random(0.1, maxScale));
            sprite.Color(startTime, colorDark, colorDark, colorDark);
            sprite.Rotate(startTime, endTime, 0, generator.Random(0, 2) == 0 ? generator.Random(-5, -1) : generator.Random(1, 5));
            sprite.MoveY(startTime, endTime, baseYPos, baseYPos + generator.Random(-100, 100));
            colorDark += 0.5f/gearNumber;
            maxScale-=0.3/gearNumber;
        }
    }
}
