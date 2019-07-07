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
}