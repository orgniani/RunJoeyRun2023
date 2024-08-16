using System;
using SFML.System;
using SFML.Graphics;
using SFML.Audio;

namespace Game
{
    class CreditsMenu : LoopState
    {
        Button menu;

        Entity creditsMusic;
        Entity creditsSounds;
        Entity creditsGraphics;

        Entity background;
        Entity background2;

        private float speed = 50f;

        public CreditsMenu(RenderWindow window) : base(window)
        {
            this.window = window;
        }

        protected override void Start()
        {
            base.Start();

            menu = new Button(window, new Vector2f(window.Size.X / 2 - 59, (window.Size.Y / 2) + 68f - 10), "MENU", "Resources/Fonts/8-bit.ttf", "Resources/Images/UI/GreenButton.png");

            background = new Entity("Resources/Images/UI/backgroundMenu.png");
            background.Position = new Vector2f(0, 0);

            background2 = new Entity("Resources/Images/UI/backgroundCredits.png");
            background2.Position = new Vector2f(0, 180);

            creditsMusic = new Entity("Resources/Images/UI/CreditsMusic.png");
            creditsMusic.Position = new Vector2f(window.Size.X / 2 - 158f, window.Size.Y / 2 - 45.5f);

            creditsSounds = new Entity("Resources/Images/UI/CreditsSoundEffects.png");
            creditsSounds.Position = new Vector2f(window.Size.X / 2 - 198f, creditsMusic.Position.Y + 91 + 40f);

            creditsGraphics = new Entity("Resources/Images/UI/CreditsGraphics.png");
            creditsGraphics.Position = new Vector2f(window.Size.X / 2 - 192f, creditsSounds.Position.Y + 332 + 40f);

            menu.Pressed += OnMenuPressed;
        }

        private void OnMenuPressed()
        {
            StateManager.LoadScene(StateManager.SceneType.MainMenu);
        }

        protected override void Update(float deltaTime)
        {
            menu.Update();

            Vector2f input = new Vector2f(0.0f, 0.0f);

            input += new Vector2f(0f, -1f);

            Vector2f movement = input * speed * deltaTime;

            creditsMusic.Translate(movement);
            creditsSounds.Translate(movement);
            creditsGraphics.Translate(movement);

            creditsMusic.Update(deltaTime);
            creditsSounds.Update(deltaTime);
            creditsGraphics.Update(deltaTime);
        }

        protected override void Draw()
        {
            window.Draw(background.Graphic);

            creditsMusic.Draw(window);
            creditsSounds.Draw(window);
            creditsGraphics.Draw(window);

            background2.Draw(window);

            menu.Draw();

            window.Display();
        }

        public override void Stop()
        {
            menu.KillButton();
            menu.Pressed -= OnMenuPressed;
            base.Stop();
        }

        protected override void Finish()
        {
            base.Finish();
        }
    }
}