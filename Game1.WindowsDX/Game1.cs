using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.WindowsDX
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

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
            var model = Content.Load<Model>("Cube");

            foreach (var m in model.Meshes)
                foreach (var e in m.Effects.OfType<BasicEffect>())
                    e.EnableDefaultLighting();

            model.Draw(
                world: Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up),
                view: Matrix.CreateLookAt(Vector3.One * 300f, Vector3.Zero, Vector3.Up),
                projection: Matrix.CreatePerspectiveFieldOfView(
                    fieldOfView: MathHelper.ToRadians(90f),
                    aspectRatio: GraphicsDevice.Viewport.AspectRatio,
                    nearPlaneDistance: 0.01f,
                    farPlaneDistance: 10000f
                    )
                );
            base.Draw(gameTime);
        }

    }
}
