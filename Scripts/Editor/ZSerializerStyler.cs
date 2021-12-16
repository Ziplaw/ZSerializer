using System;
using UnityEngine;
using ZSerializer;

namespace ZSerializer.Editor
{

    public sealed class ZSerializerStyler
    {
        //singleton 
        private static ZSerializerStyler _instance;
        public static ZSerializerStyler Instance
        {
            get
            {
                if (_instance == null) _instance = new ZSerializerStyler();
                return _instance;
            }
        }

        public Texture2D notMadeImage;
        public Texture2D needsRebuildingImage;
        public Texture2D validImage;
        public Texture2D offImage;
        internal Texture2D cogWheel;
        internal Texture2D refreshImage;
        internal Texture2D refreshWarningImage;
        internal Texture2D hierarchyOnly;
        internal Texture2D projectOnly;

        private Font mainFont;
        internal ZSerializerSettings settings;
        
        public static string YellowHex
        {
            get
            {
                if (_yellow == default)
                    _yellow = new Color(1f, 0.83f, 0f).ToHexadecimal();
                return  _yellow;
            }
        }
        
        public static string RedHex
        {
            get
            {
                if (_red == default)
                    _red = new Color(1f, 0.38f, 0.35f).ToHexadecimal();
                return  _red;
            }
        }
        
        public static string OffHex
        {
            get
            {
                if (_off == default)
                    _off = new Color(0.6f, 0.6f, 0.6f).ToHexadecimal();
                return  _off;
            }
        }
        
        public static string MainHex
        {
            get
            {
                if (_main == default)
                    _main = _mainColor.ToHexadecimal();
                return  _main;
            }
        }

        public static Color MainColor => _mainColor;

        private static readonly Color _mainColor = new Color(0.49f, 1f, 0.93f);

        private static string _yellow;
        private static string _red;
        private static string _off;
        private static string _main;
        public ZSerializerStyler()
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
            offImage = Resources.Load<Texture2D>("off");
            cogWheel = Resources.Load<Texture2D>("cog");
            refreshImage = Resources.Load<Texture2D>("Refresh");
            refreshWarningImage = Resources.Load<Texture2D>("RefreshWarning");
            projectOnly = Resources.Load<Texture2D>("projectOnly");
            hierarchyOnly = Resources.Load<Texture2D>("hierarchyOnly");

            mainFont = Resources.Load<Font>("FugazOne");
            settings = Resources.Load<ZSerializerSettings>("ZSerializerSettings");

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

    public static class Extensions
    {
        public static string ToHexadecimal(this Color color)
        {
            return $"{((int)(color.r * 255)).DecimalToHexadecimal()}{((int)(color.g*255)).DecimalToHexadecimal()}{((int)(color.b * 255)).DecimalToHexadecimal()}{((int)(color.a*255)).DecimalToHexadecimal()}";
        }
        
        private static string DecimalToHexadecimal(this int dec)
        {
            if (dec <= 0)
                return "00";

            int hex = dec;
            string hexStr = string.Empty;

            while (dec > 0)
            {
                hex = dec % 16;

                if (hex < 10)
                    hexStr = hexStr.Insert(0, Convert.ToChar(hex + 48).ToString());
                else
                    hexStr = hexStr.Insert(0, Convert.ToChar(hex + 55).ToString());

                dec /= 16;
            }

            return hexStr;
        }
    }
}