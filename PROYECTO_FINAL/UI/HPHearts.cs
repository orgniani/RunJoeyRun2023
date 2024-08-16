using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace Game
{
    public static class HPHearts
    {
        static private List<Entity> hearts = new List<Entity>();

        static Entity heart1 = new Entity("Resources/Images/UI/HPHeart.png");

        static Entity heart2 = new Entity("Resources/Images/UI/HPHeart.png");

        static Entity heart3 = new Entity("Resources/Images/UI/HPHeart.png");

        static bool needsAdd = false;

        static bool firstAdd = true;

        static public List<Entity> GetHearts()
        {
            return hearts.ToList();
        }

        static public bool needsAdding(bool add)
        {
            needsAdd = add;
            return add;
        }

        static public void AddHeart(Entity heart)
        {
            hearts.Add(heart);
        }

        static public void RemoveHeart(Entity heart)
        {
            hearts.Remove(heart);
        }

        static public void Update(float deltaTime, RenderWindow window, Player player)
        {
            heart1.Position = new Vector2f(30f, 15);
            heart2.Position = new Vector2f(heart1.Position.X, heart1.Position.Y + 15f);
            heart3.Position = new Vector2f(heart2.Position.X, heart2.Position.Y + 15f);

            if (GameManager.GameStart)
            {
                if (firstAdd == true)
                {
                    AddHeart(heart1);
                    AddHeart(heart2);
                    AddHeart(heart3);
                    firstAdd = false;
                }

                else
                {
                    if (needsAdd == false)
                    {
                        LessHearts(player);
                    }

                    if (needsAdd == true)
                    {
                        MoreHearts(player);
                    }
                }
            }

            else
            {
                foreach (Entity heart in HPHearts.GetHearts())
                {
                    RemoveHeart(heart);
                }

                firstAdd = true;
            }
        }

        static public void MoreHearts(Player player)
        {
            if (player.GetHP() == 3)
            {
                if (hearts.Count == 2)
                {
                    AddHeart(heart3);
                }
            }

            if (player.GetHP() == 2)
            {
                if (hearts.Count == 1)
                {
                    AddHeart(heart2);
                }
            }
        }

        static public void LessHearts(Player player)
        {
            if (player.GetHP() == 2)
            {
                RemoveHeart(heart3);
            }

            if (player.GetHP() == 1)
            {
                RemoveHeart(heart2);
            }

            if (player.GetHP() == 0)
            {
                RemoveHeart(heart1);
                firstAdd = true;
            }
        }
    }
}