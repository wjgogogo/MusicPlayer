public class ThemesData
{
    string name;
    Module[] modules;
    
    public struct Module
    {
        string name;
        Position pos;
    }

    struct Position
    {
        double x;
        double y;
        double z;
    }
}
