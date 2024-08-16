using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public static class GameManager
    {
        static private float score = 00000;
        static private float scoreTimer = 0;
        static private float highScore = 00000;

        static private bool gameStart = false;
        static private bool gameRestart = false;

        static public float Score { get => score; }

        static public float HighScore { get => highScore; }

        static public bool GameStart { get => gameStart; }

        static public bool GameRestart { get => gameRestart; }

        static public void AddScore(float newScore)
        {
            score += newScore;
        }

        static public void Update(float deltaTime, Player player)
        {

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && player.GetHP() > 0)
            {
                gameStart = true;
            }

            if (gameStart)
            {
                scoreTimer += deltaTime * 10;

                if (scoreTimer >= 1.0f)
                {
                    scoreTimer = 0.0f;
                    score += 00001;
                }
            }

        }

        static public void StartGame(bool stop)
        {
            if (score > highScore)
                highScore = score;

            gameStart = stop;
            score = 0;
        }

        static public void RestartGame(bool restart)
        {
            gameRestart = restart;
        }
    }
}