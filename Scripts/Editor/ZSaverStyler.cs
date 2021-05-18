using UnityEngine;
using ZSave;

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

    public void GetEveryResource()
    {
        notMadeImage = Resources.Load<Texture2D>("not_made");
        validImage = Resources.Load<Texture2D>("valid");
        needsRebuildingImage = Resources.Load<Texture2D>("needs_rebuilding");
        cogWheel = Resources.Load<Texture2D>("cog");
        refreshImage = Resources.Load<Texture2D>("Refresh");

        mainFont = Resources.Load<Font>("Comfortaa");
        settings = Resources.Load<ZSaverSettings>("ZSaverSettings");

        header = new GUIStyle()
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 15,
            richText = true,
            font = mainFont
        };

        header.normal.textColor = Color.white;
    }
}