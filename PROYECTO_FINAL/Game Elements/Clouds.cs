using Game;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Drawing;

class Clouds : Entity
{
    private float speed;
    private Vector2i size;
    private Vector2f position;

    public Clouds(string fileName, Vector2f position) : base(fileName)
    {

        speed = 30f;
        size = new Vector2i(74, 24);
        this.position = position;
        Position = position;

    }

    public void CloudUpdate(float deltaTime, RenderWindow window)
    {
        base.Update(deltaTime);

        Vector2f movement;
        movement.X = -speed * deltaTime;
        movement.Y = 0;

        Translate(movement);

        if (Position.X + size.X <= 0)
        {
            Reset(window);
        }
    }

    public void Reset(RenderWindow window)
    {
        Position = position;
    }

}