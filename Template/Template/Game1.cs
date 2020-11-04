using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Tutorial010.Models;

namespace Template
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int bredd;
        public static int hojd;
        private Texture2D background;
        private Vector2 backgroundpos;

        private Texture2D bat;
        private Texture2D ball;

        private List<Sprite> sprites;
        public static Random Random;

        //KOmentar
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this); //skärmstorlek på spelet
            hojd = graphics.PreferredBackBufferHeight = 1080;
            bredd = graphics.PreferredBackBufferWidth = 1920;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            Random = new Random();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("backgroundpicture"); // Bakgrund till spelet
            ball = Content.Load<Texture2D>("Ball");
            bat = Content.Load<Texture2D>("Bat");

            sprites = new List<Sprite>() //lista på mina två sprites // bestämmer position och Input
      {
        new Bat(bat)
        {
          Position = new Vector2(20, (hojd / 2) - (bat.Height / 2)),
          Input = new Input()
          {
            Up = Keys.W,
            Down = Keys.S,
          }
        },
        new Bat(bat)
        {
          Position = new Vector2(bredd - 20 - bat.Width, (hojd / 2) - (bat.Height / 2)),
          Input = new Input()
          {
            Up = Keys.Up,
            Down = Keys.Down,
          }
        },
        new Ball(ball)
        {
          Position = new Vector2((bredd / 2) - (ball.Width / 2), (hojd / 2) - (ball.Height / 2)),
        }
      };
        }
                // TODO: use this.Content to load your game content here 

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime, sprites);
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(); //skriver ut hur stor min bakgrund ska vara och skärmen och hur den ska se ut
            Rectangle backgroundRec = new Rectangle();
            backgroundRec.Location = backgroundpos.ToPoint();
            backgroundRec.Size = new Point(bredd, hojd);
            spriteBatch.Draw(background, backgroundRec, Color.White);

            foreach (var sprite in sprites)
                sprite.Draw(spriteBatch);

            spriteBatch.End();

            // TODO: Add your drawing code here.

            base.Draw(gameTime);
        }
    }
}
