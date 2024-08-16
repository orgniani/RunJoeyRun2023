using Game;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Drawing;

class Ground : Entity
{

    private float speed;
    public int groundHeight = 48;

    public Ground(string fileName, Vector2f position) : base(fileName)
    {
        collisionType = CollisionsHandler.CollisionType.STATIC;
        speed = 100f;
        Position = position;

        isFloor = true;
    }

    public void GroundUpdate(float deltaTime, Ground ground, RenderWindow window)
    {
        base.Update(deltaTime);

        Vector2f movement;
        movement.X = 0;
        movement.Y = 0;

        if (GameManager.GameStart)
        {
            movement.X = -speed * deltaTime;

            if (Position.X + 2400f < 0)
            {
                Reset(ground, window);
            }

            Graphic.TextureRect = new IntRect(0, 0, 2430, 1000);

            Translate(movement);
        }

    }

    public void Reset(Ground ground, RenderWindow window)
    {
        Position = new Vector2f(ground.Position.X + 2400f, window.Size.Y - groundHeight);
    }

}