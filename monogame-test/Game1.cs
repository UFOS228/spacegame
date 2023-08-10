
using monogametest;
using monogametest.Components;
using monogametest.Prefabs;
using monogametest.Prefabs.Parallaxes;
using System.Diagnostics;

namespace monogame_test
{

    public class Game1 : Game
    {
        public static Game1 instance;
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public Vector2 cameraPos = new Vector2(0, 0);
        public Color[] playerColors = new Color[]
        {
            Color.Cyan,
            Color.Red,
            Color.Green,
            Color.Violet,
        };
        public float oneTileScale = 64;
        public Vector2 cameraPosLerped
        {
            get;
            private set;
        }
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
        public Vector2 cameraPosCenteredLerped
        {
            get;
            private set;
        }
        public List<GameObject> cameraPointsCenter = new List<GameObject>();
        public float minZoom = 0.3f;
        //public float minZoomDistance = 1100f;
        public float minZoomDistance
        {
            get
            {
                return _graphics.PreferredBackBufferHeight * 1.7f;
            }
        }
        public float zoom = 1;
        public float zoomLerped = 1;
        public float maxZoom = 1f;
        public float updateTimeDelta;
        public Random random;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            instance = this;
            random = new Random();
            //ballPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            Window.Title = "Space game";
            base.Initialize();
            ObjectManager.game = this;
            cameraPosCentered = Vector2.Zero;
            //Спавн обьектов. TODO: Map system
            ObjectManager.SpawnObject(new PlayerPrefab(PlayerIndex.One), Vector2.Zero);
            ObjectManager.SpawnObject(new PlayerPrefab(PlayerIndex.Two), Vector2.Zero);
            ObjectManager.SpawnObject(new PlayerPrefab(PlayerIndex.Three), new Vector2(0, 400));
            ObjectManager.SpawnObject(new WallPrefab(), Vector2.Zero);
            ObjectManager.SpawnObject(new Layer1ParallaxPrefab(), Vector2.Zero);
            ObjectManager.SpawnObject(new AspidParallaxNebPrefab(), Vector2.Zero);
            ObjectManager.SpawnObject(new GameObject("grid", new Component[]{new TilemapComponent()}), Vector2.Zero);
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

            GameManager.Update();
            foreach (var item in ObjectManager.objectsOnMap)
            {
                foreach (var component in item.components)
                {
                    if (item.isActive)
                        ((Component)component).Update();
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GameManager.OnDraw();
            cameraPosCenteredLerped = Vector2.Lerp(cameraPosCenteredLerped, cameraPosCentered, 0.3f);
            cameraPosLerped = Vector2.Lerp(cameraPosLerped, cameraPos, 0.3f);
            if (cameraPointsCenter.Count > 0)
            {
                List<Vector2> poses = new List<Vector2>();
                foreach (var item in cameraPointsCenter)
                {
                    poses.Add(-item.position);
                }
                cameraPosCentered = GameManager.GeometricalCenter(poses.ToArray());

                zoom =
                    /*Distance от 0 до 1*/(MathF.Abs(((Vector2.Distance(poses[0], GameManager.GeometricalCenter(poses.ToArray())) / minZoomDistance) > 1 ? 1 : (Vector2.Distance(poses[0], GameManager.GeometricalCenter(poses.ToArray())) / minZoomDistance))
                    /*Инвертируем*/- 1)
                    * maxZoom);
                if (zoom < minZoom) zoom = minZoom;
                //Console.WriteLine(zoom);
            }
            zoomLerped = Vector2.Lerp(new Vector2(zoomLerped), new Vector2(zoom), 0.3f).X;
            GraphicsDevice.Clear(Color.Black);
            var transform = Matrix.CreateTranslation(new Vector3(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2, 0) / zoom) *
                         Matrix.CreateRotationZ(0) * 
                         Matrix.CreateScale(new Vector3(zoom, zoom, 1));
            _spriteBatch.Begin(SpriteSortMode.BackToFront, transformMatrix:transform, blendState: BlendState.NonPremultiplied);
            //_spriteBatch.Draw(LoadedContent.ballText, ballPos, Color.White);
            for (int i = 0; i < ObjectManager.objectsOnMap.Count; i++)
            {
                for (int i1 = 0; i1 < ObjectManager.objectsOnMap[i].components.Length; i1++)
                {
                    object component = ObjectManager.objectsOnMap[i].components[i1];
                    if (ObjectManager.objectsOnMap[i].isActive)
                        ((Component) component).OnDraw();
                }
            }
#if DEBUG
            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                Debug.WriteLine("Debug:");
                Debug.WriteLine("CamPos:" + cameraPos);
                Debug.WriteLine("CamPosCentered:" + cameraPosCentered);
                Debug.WriteLine("------------Object manifest------------");
                for (int i = 0; i < ObjectManager.objectsOnMap.Count; i++)
                {
                    Debug.WriteLine("Object:" + ObjectManager.objectsOnMap[i].name + " Pos:" + ObjectManager.objectsOnMap[i].position);
                }
                Debug.WriteLine("---------------------------------------");

            }
#endif
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

