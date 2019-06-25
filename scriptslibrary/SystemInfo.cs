using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.IO;

public class SystemInfo
{
    private static SystemInfo _insance;
    public static SystemInfo Instance
    {
        get
        {
            if(_insance == null)
            {
                _insance = new SystemInfo();
            }
            return _insance;
        }
    }
    public string MAPSETPATH;
}