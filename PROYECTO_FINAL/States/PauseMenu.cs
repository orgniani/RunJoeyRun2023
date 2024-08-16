using System;
using SFML.System;
using SFML.Graphics;

namespace Game
{
    public class PauseMenu : LoopState
    {
        Button keepPlaying;
        Button backToMenu;
        Entity background;

        public event Action keepPlayingPressed;
        public event Action backToMenuPressed;

        public PauseMenu(RenderWindow window) : base(window)
        {
            this.window = window;
        }

        protected override void Start()
        {
            base.Start();

            background = new Entity("Resources/Images/UI/backgroundMenu.png");
            background.Position = new Vector2f(0, 0);

            keepPlaying = new Button(window, new Vector2f(window.Size.X / 2 - 59, window.Size.Y / 2 - 22.5f - 10), "CONTINUE", "Resources/Fonts/8-bit.ttf", "Resources/Images/UI/GreenButton.png");
            backToMenu = new Button(window, new Vector2f(window.Size.X / 2 - 59, (window.Size.Y / 2) + 22.5f - 10), "MENU", "Resources/Fonts/8-bit.ttf", "Resources/Images/UI/RedButton.png");

            keepPlaying.Pressed += OnKeepPlaying;
            backToMenu.Pressed += OnBackToMenu;

        }

        private void OnKeepPlaying()
        {
            Stop();
            StateManager.GetScene(StateManager.SceneType.Game).Resume();
        }

        private void OnBackToMenu()
        {
            Stop();
            StateManager.LoadScene(StateManager.SceneType.MainMenu);
        }

        public override void Stop()
        {
            keepPlaying.Pressed -= OnKeepPlaying;
            backToMenu.Pressed -= OnBackToMenu;
            keepPlaying.KillButton();
            backToMenu.KillButton();
            base.Stop();
        }

        protected override void Draw()
        {
            window.Draw(background.Graphic);
            keepPlaying.Draw();
            backToMenu.Draw();

            window.Display();
        }

        protected override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            keepPlaying.Update();
            backToMenu.Update();
        }
    }
}
