using UnityEngine;
using ZSerializer;

public class ZSaverStyler
{
    public Texture2D notMadeImage;
    public Texture2D needsRebuildingImage;
    public Texture2D validImage;
    internal Texture2D cogWheel;
    internal Texture2D refreshImage;
    private Font mainFont;
    internal ZSaverSettings settings;

    public ZSaverStyler()
    {
        GetEveryResource();
    }

    public GUIStyle header;

    public static GUIStyle window
    {
        get
        {
            var style = new GUIStyle("window");
            style.overflow = new RectOffset(style.overflow.left, style.overflow.right, style.overflow.top - 19,
                style.overflow.bottom);
            return style;
        }
    }

    public void GetEveryResource()
    {
        notMadeImage = Resources.Load<Texture2D>("not_made");
        validImage = Resources.Load<Texture2D>("valid");
        needsRebuildingImage = Resources.Load<Texture2D>("needs_rebuilding");
        cogWheel = Resources.Load<Texture2D>("cog");
        refreshImage = Resources.Load<Texture2D>("Refresh");

        mainFont = Resources.Load<Font>("FugazOne");
        settings = Resources.Load<ZSaverSettings>("ZSaverSettings");

        header = new GUIStyle()
        {
            // alignment = TextAnchor.MiddleCenter,
            fontSize = 20, // 15 for comfortaa
            richText = true,
            font = mainFont
        };

        header.normal.textColor = Color.white;
    }
}