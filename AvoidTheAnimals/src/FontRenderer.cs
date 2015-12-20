#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.GamerServices;
using System.Xml.Linq;
#endregion

namespace AvoidTheAnimals.src
{
    /// <summary>
    /// The class to render font
    /// </summary>
    public class FontRenderer
    {

        public FontRenderer()
        { 
        }

        Vector2 GetOffset(string text, Font font)
        {
            Vector2 offset = new Vector2();
            foreach (char c in text)
            {
                int key = (int)c;

                Rectangle rect = font.RectangleDictionary[key];

                offset.X += rect.Width * font.Scale + font.Spacing;
                offset.Y = rect.Height * font.Scale;
            }
            offset.X -= font.Spacing;



            return offset;
        }

        public bool centerText = false;
        public bool centerText_Y = false;
        public int width { private set; get; }
        public Vector2 drawMargin = Vector2.Zero;

        /// <summary>
        /// Draws the given text with the given font.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="position">The upper left position of the text.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="font">The font to draw with.</param>
        /// <param name="color">The color to draw with.</param>
        public void DrawText(SpriteBatch spriteBatch, Vector2 position, string text, Font font, Color color)
        {
            int oldkey = -1;


            Vector2 offset = GetOffset(text, font);
            offset /= 2;
            
            if (centerText)
            {
                position.X -= offset.X;
                position.Y -= offset.Y;
            }
            else if (centerText_Y)
            {
                position.Y -= offset.Y;
            }

            position += drawMargin;

            width = 0;
            for (int i = 0; i < text.Length; i++)
            {
                int key = (int)text[i];

                spriteBatch.Draw(font.Texture, new Rectangle((int)position.X + width, (int)position.Y, (int)(font.RectangleDictionary[key].Width * font.Scale), (int)(font.RectangleDictionary[key].Height * font.Scale)), font.RectangleDictionary[key], color);
                width += (int)(font.RectangleDictionary[key].Width * font.Scale + font.Spacing);

                oldkey = key;
            }

        }
    }

    public class Font
    {
        /// <summary>
        /// Gets the texture.
        /// </summary>
        /// <value>
        /// The texture.
        /// </value>
        public Texture2D Texture { get; private set; }
        /// <summary>
        /// Gets the rectangle dictionary.
        /// </summary>
        /// <value>
        /// The rectangle dictionary.
        /// </value>
        public Dictionary<int, Rectangle> RectangleDictionary { get; private set; }
        public List<Rectangle> RectangleList { get; private set; }
        private XDocument doc;
        private string path;

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        public float Scale { get; set; }
        /// <summary>
        /// Gets or sets the spacing in pixels.
        /// </summary>
        /// <value>
        /// The spacing in pixels.
        /// </value>
        public int Spacing { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> class.
        /// </summary>
        /// <param name="font">The file name of the font.</param>
        /// <param name="Content">Content manager.</param>
        public Font(string font, ContentManager Content)
        {
            Scale = 1f;
            Spacing = 3;
            Texture = Content.Load<Texture2D>(font);
            RectangleDictionary = new Dictionary<int, Rectangle>();
            RectangleList = new List<Rectangle>();

            path = Content.RootDirectory;
            doc = XDocument.Load(path + @"\" + font + ".xml");

            Parse();
        }

        private void Parse()
        {
            XElement element = doc.Element("fontMetrics");
            var characters = element.Elements("character");

            RectangleDictionary.Add(-1, new Rectangle(0, 0, 0, 0));

            foreach (XElement character in characters)
            {
                Rectangle rect = new Rectangle();
                rect.X = int.Parse(character.Element("x").Value);
                rect.Y = int.Parse(character.Element("y").Value);
                rect.Width = int.Parse(character.Element("width").Value);
                rect.Height = int.Parse(character.Element("height").Value);
                RectangleDictionary.Add(int.Parse(character.Attribute("key").Value), rect);
                RectangleList.Add(rect);
            }
        }
    }


}
