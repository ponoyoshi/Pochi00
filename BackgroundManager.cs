using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.IO;

namespace StorybrewScripts
{
    public class BackgroundManager : StoryboardObjectGenerator
    {
        private class Background : OsbSprite
        {
            private string backgroundFolderPath;
            private OsbSprite[] layers;
            private OsbSprite sprite;
            private OsbSprite spriteBlur;
            private OsbSprite spriteBW;
            public Background(string backgroundFolderPath)
            {
                this.backgroundFolderPath = backgroundFolderPath;
                GenerateSprites();
            }
            private void GenerateSprites()
            {
                var directoryFiles = Directory.GetFiles(SystemInfo.Instance.MAPSETPATH + "/sb/b/0", "*.jpg");
                foreach(var filePath in directoryFiles)
                {
                    string fileName = Path.GetFileName(filePath);
                    

                }
            }
            private void LayerMove(int startTime, int endTime, MoveType movement)
            {

            }
            private void LayerScale(int startTime, int endTime)
            {

            }
            private enum MoveType
            {
                Left,
                Top,
                Right,
                Down,
                Static
            }
        }
        private void SetBackgroundLibrary()
        {

        }

        //WRITE EVERYTHING DOWN THIS//
        //#########################################################################################################################################//

        public override void Generate()
        {
            SystemInfo.Instance.MAPSETPATH = MapsetPath;
            RemoveBackground();
		    SetBackgroundLibrary();


        }
        private void GenerateBackground(int startFade, int startTime, int endTime, int endFade, string backgroundPath, double fadeValue, bool additive = false)
        {
            var sprite = GetLayer("").CreateSprite(backgroundPath);
            sprite.Fade(startFade, startTime, 0, fadeValue);
            sprite.Fade(endTime, endFade, fadeValue, 0);
            sprite.Scale(startFade, 480.0/1080);
            if(additive)
                sprite.Additive(startFade, endFade);
        }
        private void GenerateLayeredBackground(int startFade, int startTime, int endTime, int endFade, string backgroundFolderID, int layerNumber, double fadeValue, bool additive = false)
        {
            for(int i = 0; i < layerNumber; i++)
            {
                var sprite = GetLayer("").CreateSprite($"sb/b/{backgroundFolderID}/{i}.jpg");
                GenerateBackground(startFade, startTime, endTime, endFade, $"sb/b/{backgroundFolderID}/{i}.jpg", fadeValue, additive);
            }
        }
        private void RemoveBackground()
            => GetLayer("").CreateSprite("bg.jpg").Fade(0, 0);
    }
}
