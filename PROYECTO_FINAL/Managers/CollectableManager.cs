using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Game
{
    public static class CollectableManager
    {
        static private List<Collectable> collectables = new List<Collectable>();

        static private float timer;
        static private float time = 3f;
        static private bool makeNew = true;

        static Vector2i size = new Vector2i(0, 0);
        static Vector2f position = new Vector2f(0, 0);
        static string fileName = "";

        static private bool createdStrawberry;
        static private bool createdHeart;

        static public List<Collectable> GetCollectable()
        {
            return collectables.ToList();
        }

        static public void AddCollectable(Collectable collectable)
        {
            collectables.Add(collectable);
        }

        static public void RemoveCollectable(Collectable collectable)
        {
            collectables.Remove(collectable);
        }

        static public void Update(float deltaTime, RenderWindow window)
        {
            if (GameManager.GameStart)
            {
                createdStrawberry = false;
                createdHeart = false;

                RandomChance(window);

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
                foreach (Collectable collectable in CollectableManager.GetCollectable())
                {
                    RemoveCollectable(collectable);
                    collectable.Destroy();
                }
            }
        }

        static public void RandomChance(RenderWindow window)
        {
            if (!makeNew) return;

            Random rnd = new Random();

            if (rnd.Next(1, 100) <= 50 && createdHeart == false)
            {
                createdStrawberry = true;

                fileName = "Resources/Images/Sprites/Strawberry.png";
                size = new Vector2i(32, 32);
                position = new Vector2f(window.Size.X + size.X + 150, window.Size.Y - 48 - size.Y);
                Collectable strawberry = new Collectable(position, size, fileName, Collectable.CollectableType.STRAWBERRY);
                strawberry.collisionType = CollisionsHandler.CollisionType.TRIGGER;

                AddCollectable(strawberry);
                CollisionsHandler.AddEntity(strawberry);
            }

            if (rnd.Next(1, 100) <= 10 && createdStrawberry == false)
            {
                createdHeart = true;

                fileName = "Resources/Images/Sprites/BigHeart.png";
                size = new Vector2i(18, 14);
                position = new Vector2f(window.Size.X + size.X + 150, window.Size.Y - 48 - size.Y - 10);
                Collectable heart = new Collectable(position, size, fileName, Collectable.CollectableType.HEART);
                heart.collisionType = CollisionsHandler.CollisionType.TRIGGER;

                AddCollectable(heart);
                CollisionsHandler.AddEntity(heart);
            }

            makeNew = false;
        }
    }
}