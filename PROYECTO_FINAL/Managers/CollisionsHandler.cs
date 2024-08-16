using Game;
using SFML.Graphics;
using System;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

public static class CollisionsHandler
{
    public enum CollisionType
    {
        STATIC,
        DYNAMIC,
        TRIGGER,
    }

    public static List<Entity> entities = new List<Entity>();

    static public List<Entity> GetEntities()
    {
        return entities.ToList();
    }

    public static void AddEntity(Entity entity)
    {
        if (entities.Contains(entity))
        {
            Console.WriteLine($"Handler already contains entity");
            return;
        }
        entities.Add(entity);
    }

    public static void RemoveEntity(Entity entity)
    {
        if (!entities.Contains(entity))
        {
            Console.WriteLine($"Handler doesn't contains entity");
            return;
        }
        entities.Remove(entity);
    }

    public static void SolveCollitions(Entity first, Entity second)
    {
        FloatRect firstBounds = first.Graphic.GetGlobalBounds();
        FloatRect secondBounds = second.Graphic.GetGlobalBounds();

        Vector2f firstPos = new Vector2f(firstBounds.Left + firstBounds.Width / 2f,
                                         firstBounds.Top + firstBounds.Height / 2f);

        Vector2f secondPos = new Vector2f(secondBounds.Left + secondBounds.Width / 2f,
                                          secondBounds.Top + secondBounds.Height / 2f);

        float minHorDistance = firstBounds.Width / 2f + secondBounds.Width / 2f;
        float minVerDistance = firstBounds.Height / 2f + secondBounds.Height / 2f;

        Vector2f diff = firstPos - secondPos;

        float horPenetration = minHorDistance - Math.Abs(diff.X);
        float verPenetration = minVerDistance - Math.Abs(diff.Y);

        Vector2f firstSeparation;
        Vector2f secondSeparation;
        float firstDisplacement;
        float secondDisplacement;
        float firstDisplacementSign;
        float secondDisplacementSign;
        bool isPositiveDiff;

        if(horPenetration < verPenetration && !first.IsFloor && !second.IsFloor) //FLOOR DOESNT COLLIDE HORIZONTALLY
        {
            // HORIZONTAL DISPLACEMENT
            isPositiveDiff = (diff.X > 0f);
            firstDisplacementSign = (isPositiveDiff) ? 1f : -1f;
            secondDisplacementSign = (!isPositiveDiff) ? 1f : -1f;

            firstDisplacement = horPenetration * 0.5f * firstDisplacementSign;
            secondDisplacement = horPenetration * 0.5f * secondDisplacementSign;

            firstSeparation = new Vector2f(firstDisplacement, 0f);
            secondSeparation = new Vector2f(secondDisplacement, 0f);
        }

        else
        {
            // VERTICAL DISPLACEMENT
            isPositiveDiff = (diff.Y > 0f);
            firstDisplacementSign = (isPositiveDiff) ? 1f : -1f;
            secondDisplacementSign = (!isPositiveDiff) ? 1f : -1f;

            firstDisplacement = verPenetration * 0.5f * firstDisplacementSign;
            secondDisplacement = verPenetration * 0.5f * secondDisplacementSign;

            firstSeparation = new Vector2f(0f, firstDisplacement);
            secondSeparation = new Vector2f(0f, secondDisplacement);
        }

        first.ResolveCollision(firstSeparation, second);
        second.ResolveCollision(secondSeparation, first);
    }

    public static void Update()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            for (int j = i + 1; j < entities.Count; j++)
            {
                if (entities[i].IsColliding(entities[j]))
                {
                    if (entities[i].IsColliding(entities[j]) && entities[i].IsAlive && entities[j].IsAlive && entities[i].StopCollision && entities[j].StopCollision)
                    {
                        SolveCollitions(entities[i], entities[j]);
                    }
                }
            }
        }
    }
}