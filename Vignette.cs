using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Linq;

namespace StorybrewScripts
{
    public class Vignette : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            GenerateVignette(124011, 145345);
            GenerateVignette(277902, 289488);
            GenerateVignette(465877, 473877);
        }
        
        private void GenerateVignette(int StartTime, int EndTime)
        {
            var bitmap = GetMapsetBitmap("sb/vig.png");
            var bg = GetLayer("").CreateSprite("sb/vig.png", OsbOrigin.Centre);

            bg.Scale(StartTime, 480.0f / bitmap.Height);
            bg.Fade(StartTime - 200, StartTime, 0, 0.8);
            bg.Fade(EndTime, EndTime + 200, 0.8, 0);
        }
    }
}
