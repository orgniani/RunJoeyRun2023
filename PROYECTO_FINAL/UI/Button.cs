using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

namespace Game
{
    class Button
    {
        private RenderWindow window;
        private Text text;
        private Sprite sprite;
        private bool beingHovered;

        public event Action Pressed;
        public event Action Hover;

        public Button(RenderWindow window, Vector2f position, string buttonText, string fontPath, string imagePath)
        {
            this.window = window;
            text = new Text(buttonText, new Font(fontPath));
            sprite = new Sprite(new Texture(imagePath));
            sprite.Position = position;
            text.Position = position;
            text.CharacterSize = 8;
            SetText(buttonText);

            FloatRect limits = sprite.GetLocalBounds();

            beingHovered = false;
            window.MouseButtonReleased += OnMouseRelease;
            window.MouseMoved += OnMouseHover;
        }
        private void OnMouseRelease(object sender, MouseButtonEventArgs e)
        {
            FloatRect limits = sprite.GetGlobalBounds();
            if (limits.Contains(e.X, e.Y))
            {
                Pressed?.Invoke();
            }
        }

        private void OnMouseHover(object sender, MouseMoveEventArgs e)
        {
            FloatRect limits = sprite.GetGlobalBounds();
            if (limits.Contains(e.X, e.Y))
            {
                beingHovered = true;
                Hover?.Invoke();
            }
            else beingHovered = false;
        }

        private void SetText(string newText)
        {
            text.DisplayedString = newText;
            FloatRect textLimits = text.GetLocalBounds();
            FloatRect imageLimits = sprite.GetGlobalBounds();
            text.Origin = new Vector2f(textLimits.Width / 2f, textLimits.Height / 2f);
            text.Position = new Vector2f(text.Position.X + (imageLimits.Width / 2f), text.Position.Y + (imageLimits.Height / 2f)); //CHANGED 3F TO 2F
        }

        public void SetTextColor(Color color)
        {
            text.FillColor = color;
        }
        public void SetOutlineColor(Color color)
        {
            text.OutlineColor = color;
        }
        public void SetSpriteColor(Color color)
        {
            sprite.Color = color;
        }

        public void Draw()
        {
            window.Draw(sprite);
            window.Draw(text);
        }
        public void KillButton()
        {
            window.MouseButtonReleased -= OnMouseRelease;
            window.MouseMoved -= OnMouseHover;
        }

        public void Update()
        {
            if (!beingHovered)
            {
                SetTextColor(Color.Black);
                SetOutlineColor(Color.White);
            }
            else
            {
                SetTextColor(Color.White);
                SetOutlineColor(Color.Black);
            }
        }
    }
}
