using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TheOlian.Collision;

namespace AvoidTheAnimals.src
{
    class Player
    {
        private TheOlian.Collision.BoundingBox dimensions;
        private static Texture2D texture;

        public static void Init(ContentManager Content) {
            texture = Content.Load<Texture2D>("Sad_Sun.png");
        }

        public Player(Vector2 pos)
        {
            this.dimensions = new AIBB(pos, 25 * Game1.sizeScaler);
        }

        public void Update(MouseState ms)
        {
            dimensions.setPosition(ms.Position.ToVector2());   
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, dimensions.getBoundingBox(), Color.White);
        }

        public Vector2 getPosition() {
            return dimensions.getBoundingBox().Location.ToVector2();
        }

        public TheOlian.Collision.BoundingBox getBox() {
            return dimensions;
        }
    }
}
