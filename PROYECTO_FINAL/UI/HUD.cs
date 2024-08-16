using Game;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;

public class HUD
{
    Font font = new Font("Resources/Fonts/8-bit.ttf");

    public Text playerHP;
    public Text score;
    public Text highScore;

    public HUD(RenderWindow window)
    {
        score = new Text(Convert.ToString(GameManager.Score), font, 10);
        highScore = new Text(Convert.ToString(GameManager.HighScore), font, 10);

        score.FillColor = Color.Black;
        highScore.FillColor = Color.Black;

        score.Position = new Vector2f(window.Size.X / 2 + window.Size.X / 4 + 60f, 20);
        highScore.Position = new Vector2f(score.Position.X - 120f, score.Position.Y);
    }

    public void Update(float deltaTime)
    {
        score.DisplayedString = Convert.ToString(GameManager.Score);

        highScore.DisplayedString = "HS: " + Convert.ToString(GameManager.HighScore);    
    }

    public void Draw (RenderWindow window)
    {
        window.Draw(score);
        window.Draw(highScore);
    }

}
