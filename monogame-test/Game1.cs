
using monogametest;
using monogametest.Components;
using monogametest.GameObjectPrefabs;

namespace monogame_test
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Vector2 cameraPos = Vector2.Zero;
        public Vector2 cameraPosCentered
        {
            get
            {
                return new Vector2(cameraPos.X + (_graphics.PreferredBackBufferWidth / 2),
                    cameraPos.Y + (_graphics.PreferredBackBufferHeight / 2));
            }
            set
            {
                cameraPos = new Vector2(value.X + (_graphics.PreferredBackBufferWidth / 2),
                    value.Y + (_graphics.PreferredBackBufferHeight / 2));
            }
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //ballPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            Window.Title = "Space game";
            base.Initialize();
            ObjectManager.game = this;
            ObjectManager.objectsOnMap.Add(new GameObject(this, new Component[] {
            new RendererComponent(Content.Load<Texture2D>("ball"), Color.White, SpriteEffects.None, 1),
            new PlayerComponent()},
                Vector2.Zero, Vector2.One));
            ObjectManager.SpawnObject(new WallPrefab(), Vector2.Zero);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //LoadedContent.ballText = Content.Load<Texture2D>("ball");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (var item in ObjectManager.objectsOnMap)
            {
                foreach (var component in item.components)
                {
                    component.Update();
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            //_spriteBatch.Draw(LoadedContent.ballText, ballPos, Color.White);
            foreach (var item in ObjectManager.objectsOnMap)
            {
                if (item.TryGetComponent(out RendererComponent renderer))
                {
                    if (ObjectManager.Distance(cameraPosCentered, item.position) <= 2000)
                    {
                        _spriteBatch.Draw(renderer.texture, item.position + cameraPos, null,
                            renderer.color, item.rotation, new Vector2(renderer.texture.Width / 2, renderer.texture.Height / 2), item.scale, renderer.flipping, renderer.layerDepth);
                    }
                }
                foreach (var component in item.components)
                {
                    component.OnDraw();
                }
            }
#if DEBUG
        if (Keyboard.GetState().IsKeyDown(Keys.I))
        {
                Console.Clear();
                Console.WriteLine("Debug:\n" +
                "Playerpos:" + ObjectManager.objectsOnMap[0].position);
        }
#endif
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

