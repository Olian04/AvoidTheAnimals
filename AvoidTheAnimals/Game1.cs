using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

using AvoidTheAnimals.src;
using System;

namespace AvoidTheAnimals
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        float spawnTimer, spawnTimerLimit;
        Random rand = new Random();

        Texture2D background_text;
        Rectangle background_rect;

        StreamReader sr;
        StreamWriter sw;

        int killedAnimals = 0, streak = 0, highestStreak = 0;

        List<Animal> animals;
        Player player;
        static List<Explosion> explosions;

        FontRenderer fontRenderer;
        Font font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsFixedTimeStep = true;
            Window.Position = new Point(200, 100);

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1080;
            graphics.ApplyChanges();

        }

        public static void removeExplosion(Explosion exp) {
            explosions.Remove(exp);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            background_text = Content.Load<Texture2D>("bg.png");
            background_rect = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            Explosion.Init(Content);
            Animal.Init(Content);
            Player.Init(Content);

            fontRenderer = new FontRenderer();
            font = new Font("Font/DS-Digital", Content);
            font.Scale = 0.5f;

            animals = new List<Animal>();
            spawnTimerLimit = 100;

            explosions = new List<Explosion>();

            player = new Player(new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2) );

            sr = new StreamReader("Content/.score.txt");
            highestStreak = Int32.Parse(sr.ReadLine());
            sr.Close();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void endGame() {
            sw = new StreamWriter("Content/.score.txt");
            sw.WriteLine(highestStreak);
            sw.Close();
            Exit();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                endGame();
            }

            MouseState ms = Mouse.GetState();
            if (ms.Position.X < graphics.PreferredBackBufferWidth &&
                ms.Position.X > 0 &&
                ms.Position.Y < graphics.PreferredBackBufferHeight &&
                ms.Position.Y > 0)
            {
                spawnTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (spawnTimer >= spawnTimerLimit) {
                    animals.Add(randomSpawn());
                    spawnTimer = 0;
                }

                player.Update(ms);

                for (int i = 0; i < animals.Count; i++) {
                    if ((animals[i].graceTime > 0 ||
                        animals[i].getPosition().X < graphics.PreferredBackBufferWidth &&
                        animals[i].getPosition().X > 0 &&
                        animals[i].getPosition().Y < graphics.PreferredBackBufferHeight &&
                        animals[i].getPosition().Y > 0) && !player.getBox().Intersects(animals[i].getBox()))
                    {
                        animals[i].Update();
                    }
                    else if (player.getBox().Intersects(animals[i].getBox()))
                    {
                        explosions.Add(new Explosion(animals[i].getBox().getBoundingBox(), 100));
                        animals.Remove(animals[i]);
                        killedAnimals++;
                        if (streak > highestStreak)
                            highestStreak = streak;
                        streak = 0;
                        i--;
                    }
                    else
                    {
                        streak++;
                        animals.Remove(animals[i]);
                        i--;
                    }
                }
                for (int i = 0; i < explosions.Count; i++) {
                    explosions[i].Update();
                }
            }

            base.Update(gameTime);
        }

        private Animal randomSpawn() {
            Vector2 position = new Vector2();
            Vector2 velocity = new Vector2(), diff;

            switch (rand.Next(0, 4)) {
                case 0: //Upp
                    position = new Vector2(rand.Next(0, graphics.PreferredBackBufferWidth), -10);
                    break;
                case 1: //Down
                    position = new Vector2(rand.Next(0, graphics.PreferredBackBufferWidth), graphics.PreferredBackBufferHeight + 10);
                    break;
                case 2: //Left
                    position = new Vector2(-10, rand.Next(0, graphics.PreferredBackBufferHeight));
                    break;
                case 3: //Right
                    position = new Vector2(graphics.PreferredBackBufferWidth + 10, rand.Next(1, graphics.PreferredBackBufferHeight));
                    break;
            }

            diff = player.getPosition() - position;

            diff.X = (float)Math.Round(diff.X / 50, 2);
            diff.Y = (float)Math.Round(diff.Y / 50, 2);

            if (diff.X < 0)
                velocity.X = -1;
            else
                velocity.X = 1;
            if (diff.Y < 0)
                velocity.Y = -1;
            else
                velocity.Y = 1;

            return new Animal(position, diff);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(background_text, background_rect, Color.White);


            foreach (Explosion e in explosions)
                e.Draw(spriteBatch);
            foreach (Animal a in animals)
                a.Draw(spriteBatch);
            player.Draw(spriteBatch);

            fontRenderer.DrawText(spriteBatch, new Vector2(graphics.PreferredBackBufferWidth / 2 - 10,graphics.PreferredBackBufferHeight - 46), "Streak: " + streak, font, Color.Black);
            fontRenderer.DrawText(spriteBatch, new Vector2(graphics.PreferredBackBufferWidth / 2 - 30, graphics.PreferredBackBufferHeight - 34), "Highest Streak: " + highestStreak, font, Color.Black);
            fontRenderer.DrawText(spriteBatch, new Vector2(graphics.PreferredBackBufferWidth / 2 - 50, graphics.PreferredBackBufferHeight - 22), "Animals Slaughtered: " + killedAnimals, font, Color.Black);

            spriteBatch.End();

           base.Draw(gameTime);
        }
    }
}
