using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public static class ObstacleManager
    {
        static private List<Obstacles> obstacles = new List<Obstacles>();

        static private float timer;
        static private float time = 3f;
        static private bool makeNew = true;

        static Vector2i size = new Vector2i(0, 0);
        static Vector2f position = new Vector2f(0, 0);
        static string fileName = "";

        static public List<Obstacles> GetObstacles()
        {
            return obstacles.ToList();
        }

        static public void AddObstacle(Obstacles obstacle)
        {
            obstacles.Add(obstacle);
        }

        static public void RemoveObstacle(Obstacles obstacle)
        {
            obstacles.Remove(obstacle);
        }

        static public void Update(float deltaTime, RenderWindow window)
        {
            if (GameManager.GameStart)
            {
                Randomizer(window);

                if (!makeNew)
                {
                    timer += deltaTime;
                    if (timer >= time)
                    {
                        makeNew = true;
                        timer = 0f;
                    }
                }
            }

            else
            {
                foreach (Obstacles obstacle in ObstacleManager.GetObstacles())
                {
                    RemoveObstacle(obstacle);
                    obstacle.Destroy();
                }
            }
        }

        static public void Randomizer(RenderWindow window)
        {
            if (!makeNew) return;

            Random rnd = new Random();
            int random = rnd.Next(1, 4);

            switch (random)
            {
                case 1:
                    fileName = "Resources/Images/Sprites/Bird.png";
                    size = new Vector2i(32, 32);
                    position = new Vector2f(window.Size.X + size.X, window.Size.Y - 70 - size.Y);
                    Obstacles bird = new Obstacles(position, fileName, Obstacles.ObstacleType.BIRD, size);

                    AddObstacle(bird);
                    CollisionsHandler.AddEntity(bird);
                    break;

                case 2:
                    fileName = "Resources/Images/Sprites/Mushroom.png";
                    size = new Vector2i(32, 32);
                    position = new Vector2f(window.Size.X + size.X, window.Size.Y - 48 - size.Y);
                    Obstacles mushroom = new Obstacles(position, fileName, Obstacles.ObstacleType.SHROOM, size);

                    AddObstacle(mushroom);
                    CollisionsHandler.AddEntity(mushroom);
                    break;

                case 3:
                    fileName = "Resources/Images/Sprites/Rock.png";
                    size = new Vector2i(38, 34);
                    position = new Vector2f(window.Size.X + size.X, window.Size.Y - 48 - size.Y);
                    Obstacles rock = new Obstacles(position, fileName, Obstacles.ObstacleType.ROCK, size);

                    AddObstacle(rock);
                    CollisionsHandler.AddEntity(rock);
                    break;
            }
            makeNew = false;

        }
    }
}