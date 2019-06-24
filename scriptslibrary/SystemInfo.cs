public sealed class SystemInfo
{
    private SystemInfo(){}
    public static SystemInfo _instance = null;
    public static SystemInfo Instance
    {
        get 
        {
            if(_instance == null)
            {
                _instance = new SystemInfo();
            }
            return _instance;
        }
    }

    //GLOBAL VARIABLES
    //##############################

    public string MAPSETPATH {get; set;}

}