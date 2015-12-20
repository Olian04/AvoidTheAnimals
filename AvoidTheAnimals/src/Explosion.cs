using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AvoidTheAnimals.src
{
    public class Explosion
    {
        static List<Texture2D> textures;

        public static void Init(ContentManager Content) {
            textures = new List<Texture2D>();
            for (int i = 0; i<25; i++)
                textures.Add(Content.Load<Texture2D>("White_puff/whitePuff" + i + ".png"));
        }

        Rectangle dimensions;
        int timeToLive;
        Random rand = new Random();

        public Explosion(Rectangle dimensions, int timeToLive) {
            this.dimensions = dimensions;
            this.timeToLive = timeToLive;
        }

        public void Update() {
            timeToLive--;
            if (timeToLive <= 0) {
                Game1.removeExplosion(this);
            }
            if (timeToLive < 10) {
                timeToLive -= 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(textures[rand.Next(0, textures.Count)], dimensions, Color.White);
        }
    }
}
