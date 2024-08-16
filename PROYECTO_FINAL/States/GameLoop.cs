using System;
using SFML.System;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;

namespace Game
{
    public class GameLoop : LoopState
    {
        private Player player;

        private Ground ground;
        private Ground groundLoop;

        private Clouds cloud;
        private Clouds cloud2;

        private Background background;

        private HUD screenData;

        private Entity instructions;
        private Entity avoid;

        private Entity gameOver;
        private Button restart;
        private Entity restartPressR;

        private PauseMenu pauseMenu;

        private float gameSpeed;

        public GameLoop(RenderWindow window) : base (window)
        {
            this.window = window;
            pauseMenu = new PauseMenu(window);
        }

        protected override void Start()
        {
            base.Start();

            window.KeyPressed += OnKeyPressed;

            float playerSpeed = 150f;
            int groundHeight = 48;
            isPaused = false;

            //PLAYER

            Vector2i playerSize = new Vector2i(34, 44);
            Vector2f playerPosition = new Vector2f(playerSize.X + 50f, window.Size.Y - groundHeight - playerSize.Y);

            player = new Player(playerPosition, playerSpeed, "Resources/Images/Sprites/BUNNY.png", playerSize);

            //SCORE

            screenData = new HUD(window);

            //FLOOR

            Vector2f groundPosition = new Vector2f(0f, window.Size.Y - groundHeight);
            Vector2f groundLoopPosition = new Vector2f(2400f, window.Size.Y - groundHeight);

            ground = new Ground("Resources/Images/Sprites/Floor.png", groundPosition);
            groundLoop = new Ground("Resources/Images/Sprites/Floor.png", groundLoopPosition);

            //CLOUDS

            Vector2f cloudPosition = new Vector2f(window.Size.X + 74, window.Size.Y / 2 - 20f);
            Vector2f cloud2Position = new Vector2f(window.Size.X * 2 + 74, window.Size.Y / 2 - 60f);

            cloud = new Clouds("Resources/Images/Sprites/Cloud.png", cloudPosition);
            cloud2 = new Clouds("Resources/Images/Sprites/Cloud.png", cloud2Position);

            //BACKGROUND

            Vector2f backgroundPosition = new Vector2f(0f, window.Size.Y - 934f);
            background = new Background("Resources/Images/Sprites/skyBackground.png", backgroundPosition);

            //TEXT

            Vector2f instructionsPosition = new Vector2f(window.Size.X / 2 - 75f, window.Size.Y / 2 - 40f);
            instructions = new Entity("Resources/Images/UI/Instructions.png");
            instructions.Position = instructionsPosition;

            Vector2f avoidPosition = new Vector2f(490f - 96f, window.Size.Y - 15f);
            avoid = new Entity("Resources/Images/UI/avoid.png");
            avoid.Position = avoidPosition;

            Vector2f gameOverPosition = new Vector2f(window.Size.X / 2 - 51f, window.Size.Y / 2 - 10f);
            gameOver = new Entity("Resources/Images/UI/GAMEOVER.png");
            gameOver.Position = gameOverPosition;

            //BUTTON

            restart = new Button(window, new Vector2f(window.Size.X / 2 - 10.5f, gameOverPosition.Y + 15f), "", "Resources/Fonts/8-bit.ttf", "Resources/Images/UI/Restart.png");
            restart.Pressed += OnRestart;

            Vector2f restartPressRPosition = new Vector2f(window.Size.X / 2 - 93f, gameOverPosition.Y + 60f);
            restartPressR = new Entity("Resources/Images/UI/pressRestart.png");
            restartPressR.Position = restartPressRPosition;

            //COLLISSION

            CollisionsHandler.AddEntity(player);
            CollisionsHandler.AddEntity(ground);
            CollisionsHandler.AddEntity(groundLoop);
        }

        private void OnRestart()
        {
            StateManager.LoadScene(StateManager.SceneType.Game);
        }

        private void IncreaseSpeed()
        {
            if (GameManager.Score > 500)
            {
                gameSpeed += 0.0001f;
            }

            if (GameManager.Score > 1000)
            {
                gameSpeed += 0.0001f;
            }

            if (GameManager.Score > 1500)
            {
                gameSpeed += 0.0001f;
            }

            if (GameManager.Score > 3000)
            {
                gameSpeed += 0.0001f;
            }

            if (GameManager.Score > 5000)
            {
                gameSpeed += 0.0001f;
            }
        }

        protected override void Update(float deltaTime)
        {
            gameSpeed = deltaTime;

            IncreaseSpeed();

            //DELTATIME

            background.BGUpdate(deltaTime, window);

            screenData.Update(deltaTime);

            GameManager.Update(deltaTime, player);

            HPHearts.Update(deltaTime, window, player);

            //GAMESPEED

            player.Update(gameSpeed);

            foreach (Obstacles obstacle in ObstacleManager.GetObstacles())
            {
                obstacle.Update(gameSpeed);
            }

            foreach (Collectable collectable in CollectableManager.GetCollectable())
            {
                collectable.Update(gameSpeed);
            }

            ground.GroundUpdate(gameSpeed, groundLoop, window);
            groundLoop.GroundUpdate(gameSpeed, ground, window);

            cloud.CloudUpdate(gameSpeed, window);
            cloud2.CloudUpdate(gameSpeed, window);

            ObstacleManager.Update(gameSpeed, window);

            CollectableManager.Update(gameSpeed, window);

            //OTHERS

            if (player.GetHP() > 0)
            {
                CollisionsHandler.Update();
            }

            restart.Update();

            if (player.GetHP() == 0)
            {
                GameManager.StartGame(false);
            }
        }

        protected override void Draw()
        {
            window.Clear(Color.White);

            background.Draw(window);

            ground.Draw(window);
            groundLoop.Draw(window);

            cloud.Draw(window);
            cloud2.Draw(window);

            player.Draw(window);

            foreach (Obstacles obstacle in ObstacleManager.GetObstacles())
            {
                obstacle.Draw(window);
            }

            foreach (Collectable collectable in CollectableManager.GetCollectable())
            {
                collectable.Draw(window);
            }

            screenData.Draw(window);

            foreach (Entity hearts in HPHearts.GetHearts())
            {
                hearts.Draw(window);
            }

            DrawInstructions();

            DrawGameOver();

            window.Display();
        }

        public void DrawInstructions()
        {
            instructions.Draw(window);
            avoid.Draw(window);

            if (GameManager.GameStart)
            {
                instructions.Destroy();
                avoid.Destroy();
            }
        }

        public void DrawGameOver()
        {
            if (player.GetHP() == 0)
            {
                gameOver.Draw(window);
                restart.Draw();
                restartPressR.Draw(window);
            }
        }

        public override void Stop()
        {
            base.Stop();

            CollisionsHandler.RemoveEntity(player);

            restart.KillButton();
            restart.Pressed -= OnRestart;

            GameManager.StartGame(false);
        }

        protected override void Finish()
        {
            base.Finish();
        }

        public void OnKeyPressed(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                SetPause();
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.R) && player.GetHP() <= 0)
            {
                StateManager.LoadScene(StateManager.SceneType.Game);
            }

        }

        public void SetPause()
        {
            Pause();
            pauseMenu.Play();
        }
    }
}