using System;
using SFML.System;
using SFML.Graphics;
using SFML.Audio;

namespace Game
{
    class MainMenu : LoopState
    {
        Button gameStart;
        Button quit;
        Button credits;

        Entity background;
        Entity title;
        Entity creator;

        Music gameMusic;

        public event Action StartPressed;
        public event Action QuitPressed;

        public MainMenu(RenderWindow window) : base(window)
        {
            this.window = window;
        }

        protected override void Start()
        {
            base.Start();
            gameStart = new Button(window, new Vector2f(window.Size.X / 2 - 59, window.Size.Y / 2 - 22.5f - 10), "START", "Resources/Fonts/8-bit.ttf", "Resources/Images/UI/GreenButton.png");
            quit = new Button(window, new Vector2f(window.Size.X / 2 - 59, (window.Size.Y / 2) + 22.5f - 10), "QUIT", "Resources/Fonts/8-bit.ttf", "Resources/Images/UI/RedButton.png");
            credits = new Button(window, new Vector2f(window.Size.X / 2 - 59, (window.Size.Y / 2) + 68f - 10), "CREDITS", "Resources/Fonts/8-bit.ttf", "Resources/Images/UI/BlueButton.png");

            background = new Entity("Resources/Images/UI/backgroundMenu.png");
            background.Position = new Vector2f(0, 0);

            title = new Entity("Resources/Images/UI/title.png");
            title.Position = new Vector2f((window.Size.X / 2) - 166f, window.Size.Y / 2 - 90);

            creator = new Entity("Resources/Images/UI/creator.png");
            creator.Position = new Vector2f(window.Size.X / 2 - 56.5f, title.Position.Y + 40f);

            if (gameMusic == null)
            {
                gameMusic = new Music("Resources/Sounds/music.wav");
                gameMusic.Loop = true;

                gameMusic.Play();
                gameMusic.Volume = 10f;
            }

            gameStart.Pressed += OnStartPressed;
            quit.Pressed += OnQuitPressed;
            credits.Pressed += OnCreditsPressed;
        }

        private void OnStartPressed()
        {

            StateManager.LoadScene(StateManager.SceneType.Game);
        }

        private void OnQuitPressed()
        {
            Stop();
            return;
        }

        private void OnCreditsPressed()
        {
            StateManager.LoadScene(StateManager.SceneType.Credits);
        }


        protected override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            gameStart.Update();
            quit.Update();
            credits.Update();
        }

        protected override void Draw()
        {
            window.Draw(background.Graphic);
            title.Draw(window);
            creator.Draw(window);
            gameStart.Draw();
            quit.Draw();
            credits.Draw();

            window.Display();
        }

        public override void Stop()
        {
            gameStart.KillButton();
            quit.KillButton();
            credits.KillButton();
            gameStart.Pressed -= OnStartPressed;
            quit.Pressed -= OnQuitPressed;
            credits.Pressed -= OnCreditsPressed;
            base.Stop();
        }

        protected override void Finish()
        {
            base.Finish();
        }
    }
}