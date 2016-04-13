using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TheOlian.Collision;

namespace AvoidTheAnimals.src
{
    class Animal
    {
        private CollisionBox dimensions;
        private Vector2 velocity;
        private Texture2D texture;

        public int graceTime = 100;

        private static Texture2D[] availableTextures;
        public static void Init(ContentManager Content) {
            availableTextures = new Texture2D[10];
            availableTextures[0] = Content.Load<Texture2D>("Animals/elephant.png");
            availableTextures[1] = Content.Load<Texture2D>("Animals/giraffe.png");
            availableTextures[2] = Content.Load<Texture2D>("Animals/hippo.png");
            availableTextures[3] = Content.Load<Texture2D>("Animals/monkey.png");
            availableTextures[4] = Content.Load<Texture2D>("Animals/panda.png");
            availableTextures[5] = Content.Load<Texture2D>("Animals/parrot.png");
            availableTextures[6] = Content.Load<Texture2D>("Animals/penguin.png");
            availableTextures[7] = Content.Load<Texture2D>("Animals/pig.png");
            availableTextures[8] = Content.Load<Texture2D>("Animals/rabbit.png");
            availableTextures[9] = Content.Load<Texture2D>("Animals/snake.png");
        }

        public Animal(Vector2 pos, Vector2 velocity) {
            this.dimensions = new AIBB(pos, 15);
            this.velocity = velocity;
            texture = availableTextures[new Random().Next(0, 10)];
        }

        public void Update() {
            dimensions.alterPositionAdition(velocity);
            graceTime -= 1;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, dimensions.getBoundingBox(), Color.White);
        }

        public Vector2 getPosition()
        {
            return dimensions.getBoundingBox().Location.ToVector2();
        }

        public CollisionBox getBox()
        {
            return dimensions;
        }
    }
}
