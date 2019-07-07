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
using System.Drawing;
using System.Net;
using System.IO;

namespace StorybrewScripts
{
    public class DebugTimestamps : StoryboardObjectGenerator
    {
        [Configurable] public bool reload;
        [Configurable] public bool copySprites;
        [Configurable] public bool generateSections;
        int oldTime = 11;
        FontGenerator font;
        public override void Generate()
        {
            FontLibrary fontLibrary = new FontLibrary(this);

            font = fontLibrary.GetFont("Regular");
            ReloadBackgrounds();

            if(generateSections)
		        GenerateSections();
            
            if(copySprites)
            {
                Log("--- COPY FROM PROJECT ---");
                var mapsetDirectory = new DirectoryInfo(MapsetPath + "/sb");
                var projectDirectory = new DirectoryInfo(ProjectPath + "/sprites");
                CopyFiles(projectDirectory, mapsetDirectory);
                Log(" ");
                Log("--- COPY FROM MAPSET ---");
                CopyFiles(mapsetDirectory, projectDirectory);
                Log(" ");
            }
            else 
            {
                Log("--- FILE COPY DISABLED ---");
                Log(" ");
            }
            
        }
        private void ReloadBackgrounds()
        {
            using(var client = new WebClient())
                client.DownloadFile("https://docs.google.com/spreadsheets/d/1KcOHDJRSbK11UI7uwShiVAZtcgbXl5fSJwv1iZSrqWo/export?gid=796801173&format=csv", ProjectPath + "/backgroundData.csv");
        }
        private void GenerateSections()
        {
            int ID = 0;
            foreach(var time in Beatmap.Bookmarks)
            {
                GenerateSection(oldTime, time, ID);
                oldTime = time;
                ID++;
            }
        }
        private void GenerateSection(int startTime, int endTime, int ID)
        {
            var texture = font.GetTexture($"Section - [{ID}]");
            var sprite = GetLayer("DEBUG").CreateSprite(texture.Path, OsbOrigin.CentreLeft, new Vector2(-100, 460));
            sprite.Fade(startTime, endTime, 1, 1);
            sprite.Scale(startTime, 0.2);
        }
        public void CopyFiles(DirectoryInfo source, DirectoryInfo destination)
        {
            if(source == null)
                Directory.CreateDirectory(source.FullName);

            Directory.CreateDirectory(destination.FullName);
            foreach (FileInfo fileInfo in source.GetFiles())
            { 
                if(!File.Exists(Path.Combine(destination.FullName, fileInfo.Name)))
                {
                    Log($"> {fileInfo.Name}");
                    fileInfo.CopyTo(Path.Combine(destination.FullName, fileInfo.Name), true);
                }
            }
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    destination.CreateSubdirectory(diSourceSubDir.Name);
                CopyFiles(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
