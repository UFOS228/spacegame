
using monogametest;
using monogametest.Components;
using monogametest.GameObjectPrefabs;
using monogametest.GameObjectPrefabs.Parallaxes;

namespace monogame_test
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public Vector2 cameraPos = new Vector2(0, 0);
        public Vector2 cameraPosCentered
        {
            get
            {
                return new Vector2(cameraPos.X - (_graphics.PreferredBackBufferWidth / 2),
                    cameraPos.Y - (_graphics.PreferredBackBufferHeight / 2));
            }
            set
            {
                cameraPos = new Vector2(value.X + (_graphics.PreferredBackBufferWidth / 2),
                    value.Y + (_graphics.PreferredBackBufferHeight / 2));
            }
        }
        public float updateTimeDelta;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //ballPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            Window.Title = "Space game";
            base.Initialize();
            ObjectManager.game = this;
            cameraPosCentered = Vector2.Zero;
            //Спавн обьектов. TODO: Map system
            ObjectManager.SpawnObject(new PlayerPrefab(), Vector2.Zero);
            ObjectManager.SpawnObject(new WallPrefab(), Vector2.Zero);
            ObjectManager.SpawnObject(new Layer1ParallaxPrefab(), Vector2.Zero);
            ObjectManager.SpawnObject(new AspidParallaxNebPrefab(), Vector2.Zero);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //LoadedContent.ballText = Content.Load<Texture2D>("ball");
            MyContentManager.ContentInit(this);
        }

        protected override void Update(GameTime gameTime)
        {
            //updateTimeDelta = gameTime.ElapsedGameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.BackToFront);
            //_spriteBatch.Draw(LoadedContent.ballText, ballPos, Color.White);
            foreach (var item in ObjectManager.objectsOnMap)
            {
                if (item.TryGetComponent(out RendererComponent renderer))
                {
                    if (ObjectManager.Distance(cameraPosCentered, -item.position) <= _graphics.PreferredBackBufferHeight + _graphics.PreferredBackBufferWidth)
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
            Console.Clear();
            Console.WriteLine("Debug:");
            Console.WriteLine("CamPos:" + cameraPos);
            Console.WriteLine("CamPosCentered:" + cameraPosCentered);
            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                Console.WriteLine("------------Object manifest------------");
                foreach (var item in ObjectManager.objectsOnMap)
                {
                    Console.WriteLine("Object:" + item.name + " Pos:" + item.position);
                }
                Console.WriteLine("---------------------------------------");

            }
#endif
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

