using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpriteEngine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D t2dTanks;
        Texture2D t2dPrincess;
        MobileSprite myTank;
        MobileSprite mouseTank;
        MobileSprite myPrincess;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            this.IsMouseVisible = true;

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
            t2dTanks = Content.Load<Texture2D>(@"Charset\MulticolorTanks");
            t2dPrincess = Content.Load<Texture2D>(@"Charset\PrincessCharacter");


            myTank = new MobileSprite(t2dTanks);
            myTank.Sprite.AddAnimation("green", 0, 0, 32, 32, 8, 0.1f);
            myTank.Sprite.AutoRotate = true;
            myTank.Position = new Vector2(100, 100);
            myTank.Target = myTank.Position;
            myTank.AddPathNode(new Vector2(200, 200));
            myTank.AddPathNode(new Vector2(400, 200));
            myTank.AddPathNode(new Vector2(400, 400));
            myTank.AddPathNode(new Vector2(200, 400));
            myTank.Speed = 3;
            myTank.LoopPath = true;


            mouseTank = new MobileSprite(t2dTanks);
            mouseTank.Sprite.AddAnimation("red", 0, 32, 32, 32, 8, 0.1f);
            mouseTank.Sprite.AddAnimation("purple", 0, 128, 32, 32, 8, 0.1f, "red");
            mouseTank.Sprite.AddAnimation("yellow", 0, 64, 32, 32, 8, 0.1f);
            mouseTank.Sprite.AutoRotate = true;
            mouseTank.Position = new Vector2(100, 100);
            mouseTank.Target = mouseTank.Position;
            mouseTank.IsPathing = true;
            mouseTank.EndPathAnimation = "yellow";
            mouseTank.LoopPath = false;
            mouseTank.Speed = 2;


            myPrincess = new MobileSprite(t2dPrincess);
            myPrincess.Sprite.AddAnimation("leftstop", 0, 0, 32, 64, 1, 0.1f);
            myPrincess.Sprite.AddAnimation("left", 0, 0, 32, 64, 4, 0.1f);
            myPrincess.Sprite.AddAnimation("rightstop", 0, 64, 32, 64, 1, 0.1f);
            myPrincess.Sprite.AddAnimation("right", 0, 64, 32, 64, 4, 0.1f);
            myPrincess.Sprite.CurrentAnimation = "rightstop";
            myPrincess.Position = new Vector2(100, 300);
            myPrincess.Sprite.AutoRotate = false;
            myPrincess.IsPathing = false;
            myPrincess.IsMoving = false;




        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            MouseState ms = Mouse.GetState();
            KeyboardState ks = Keyboard.GetState();


            myTank.Update(gameTime);


            mouseTank.Target = new Vector2(ms.X, ms.Y);

            /*if (ms.LeftButton == ButtonState.Pressed)
            {
                if (mouseTank.Sprite.CurrentAnimation == "red")
                {
                    mouseTank.Sprite.CurrentAnimation = "purple";
                }
            }*/

            if (ms.LeftButton == ButtonState.Pressed)
            {
                mouseTank.AddPathNode(ms.X, ms.Y);
                if (mouseTank.Sprite.CurrentAnimation != "red")
                    mouseTank.Sprite.CurrentAnimation = "red";
            }

            mouseTank.Update(gameTime);


            bool leftkey = ks.IsKeyDown(Keys.Left);
            bool rightkey = ks.IsKeyDown(Keys.Right);

            if (leftkey)
            {
                if (myPrincess.Sprite.CurrentAnimation != "left")
                {
                    myPrincess.Sprite.CurrentAnimation = "left";
                }
                myPrincess.Sprite.MoveBy(-2, 0);
            }

            if (rightkey)
            {
                if (myPrincess.Sprite.CurrentAnimation != "right")
                {
                    myPrincess.Sprite.CurrentAnimation = "right";
                }
                myPrincess.Sprite.MoveBy(2, 0);
            }

            if (!leftkey && !rightkey)
            {
                if (myPrincess.Sprite.CurrentAnimation == "left")
                {
                    myPrincess.Sprite.CurrentAnimation = "leftstop";
                }
                if (myPrincess.Sprite.CurrentAnimation == "right")
                {
                    myPrincess.Sprite.CurrentAnimation = "rightstop";
                }
            }

            myPrincess.Update(gameTime);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            myTank.Draw(spriteBatch);
            mouseTank.Draw(spriteBatch);
            myPrincess.Draw(spriteBatch);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
