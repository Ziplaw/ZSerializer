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
        internal Texture2D refreshErrorImage;
        internal Texture2D hierarchyOnly;
        internal Texture2D projectOnly;

        private static readonly Font MainFont = Resources.Load<Font>("FugazOne");
        internal ZSerializerSettings settings;

        public static string YellowHex => _yellowHex = _yellowHex ?? Yellow.ToHexadecimal();

        public static string RedHex => _redHex = _redHex ?? _red.ToHexadecimal();

        public static string OffHex => _offHex = _offHex ?? Off.ToHexadecimal();

        public static string MainHex
        {
            get
            {
                if (_mainHex == default)
                    _mainHex = _mainColor.ToHexadecimal();
                return _mainHex;
            }
        }

        public static Color MainColor => _mainColor;
        public static Color Yellow => _yellow;
        public static Color Red => _red;
        public static Color Off => _off;


        private static string _yellowHex;
        private static string _redHex;
        private static string _offHex;
        private static string _mainHex;

        private static readonly Color _yellow = new Color(1f, 0.83f, 0f);
        private static readonly Color _red = new Color(1f, 0.38f, 0.35f);
        private static readonly Color _off = new Color(0.6f, 0.6f, 0.6f);
        private static readonly Color _mainColor = new Color(0.49f, 1f, 0.93f);

        public ZSerializerStyler()
        {
            GetEveryResource();
        }

        public GUIStyle header;
        private GUIStyle richText;
        public Texture editIcon;

        private static GUIStyle _bigLabel;

        private static GUIStyle BigLabelStyle
        {
            get => _bigLabel = _bigLabel ?? new GUIStyle("label")
            { 
                font = MainFont, 
                alignment = TextAnchor.MiddleCenter, 
                fontSize = 20,
                richText = true,
            };
        }

        private static GUIStyle _window;

        public static GUIStyle Window
        {
            get
            {
                if (_window == null)
                {
                    _window = new GUIStyle("window");
                    _window.overflow = new RectOffset(_window.overflow.left, _window.overflow.right,
                        _window.overflow.top - 19,
                        _window.overflow.bottom);
                }

                return _window;
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
            refreshErrorImage = Resources.Load<Texture2D>("RefreshError");

            projectOnly = Resources.Load<Texture2D>("projectOnly");
            hierarchyOnly = Resources.Load<Texture2D>("hierarchyOnly");
            editIcon = Resources.Load<Texture2D>("editIcon");
            settings = Resources.Load<ZSerializerSettings>("ZSerializerSettings");

            header = new GUIStyle()
            {
                // alignment = TextAnchor.MiddleCenter,
                fontSize = 20, // 15 for comfortaa
                richText = true,
                font = MainFont
            };

            richText = new GUIStyle()
            {
                richText = true
            };

            richText.normal.textColor = Color.white;
            header.normal.textColor = Color.white;
        }

        public static void BigLabel(string label)
        {
            GUILayout.Space(-18);
            using (new GUILayout.VerticalScope(Window))
            {
                GUILayout.Label(label, BigLabelStyle);
            }
        }

        public static void BigLabel(string label, Color color)
        {
            BigLabel($"<color=#{color.ToHexadecimal()}>{label}</color>");
        }
    }

    public static class Extensions
    {
        public static string ToHexadecimal(this Color color)
        {
            return
                $"{((int)(color.r * 255)).DecimalToHexadecimal()}{((int)(color.g * 255)).DecimalToHexadecimal()}{((int)(color.b * 255)).DecimalToHexadecimal()}{((int)(color.a * 255)).DecimalToHexadecimal()}";
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