using Game;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Drawing;

class Background : Entity
{

    private float speed;
    private bool limit = false;

    public Background(string fileName, Vector2f position) : base(fileName)
    {
        collisionType = CollisionsHandler.CollisionType.STATIC;
        type = Entity.EntityType.NONE;
        speed = 3f;
        Position = position;
    }

    public void BGUpdate(float deltaTime, RenderWindow window)
    {
        base.Update(deltaTime);

        Vector2f movement;

        movement.X = 0;
        movement.Y = 0;

        if (Position.Y > 0)
        {
            limit = true;
        }

        if (Position.Y < window.Size.Y - 934f - 48f)
        {
            limit = false;
        }

        if (limit == true)
        {
            movement.Y = -speed * deltaTime;
        }

        else
        {
            movement.Y = speed * deltaTime;
        }

        Translate(movement);
    }

}