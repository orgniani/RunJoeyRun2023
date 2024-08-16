using Game;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Obstacles : AnimatedEntity
{
    public enum ObstacleType
    {
        BIRD,
        SHROOM,
        ROCK,
    }

    private float speed = 100f;

    private Vector2i size = new Vector2i(0,0);

    private ObstacleType obstacleType;

    public Obstacles(Vector2f position, string fileName, ObstacleType newObstacleType, Vector2i size) : base(fileName, size)
    {
        Texture = ResourceManager.CreateTexture(fileName);
        obstacleType = newObstacleType;

        AnimationData bird = new AnimationData()
        {
            frameSpeed = 20f,
            rowIndex = 0,
            columnCount = 9,
            loops = true
        };

        AnimationData mushroom = new AnimationData()
        {
            frameSpeed = 20f,
            rowIndex = 0,
            columnCount = 14,
            loops = true
        };

        AnimationData rock = new AnimationData()
        {
            frameSpeed = 20f,
            rowIndex = 0,
            columnCount = 14,
            loops = true
        };

        AddAnimation("Bird", bird);
        AddAnimation("Mushroom", mushroom);
        AddAnimation("Rock", rock);

        if (obstacleType == ObstacleType.BIRD)
        {
            SetCurrentAnimation("Bird");
            speed = 150f;
        }

        if (obstacleType == ObstacleType.SHROOM)
        {
            SetCurrentAnimation("Mushroom");
        }

        if (obstacleType == ObstacleType.ROCK)
        {
            SetCurrentAnimation("Rock");
        }

        Graphic.Position = position;

        this.size = size;

        type = EntityType.ENEMY;
        collisionType = CollisionsHandler.CollisionType.TRIGGER;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        Vector2f input = new Vector2f(0.0f, 0.0f);

        input += new Vector2f(-1f, 0f);

        Vector2f movement = input * speed * deltaTime;

        Translate(movement);

        if (Position.X + size.X <= 0)
        {
            ObstacleManager.RemoveObstacle(this);
            Destroy();
        }
    }

    public override void ResolveCollision(Vector2f displacement, Entity otherEntity)
    {
        base.ResolveCollision(displacement, otherEntity);

        switch (otherEntity.type)
        {
            case EntityType.PLAYER:
                StopCollission();
                break;
            default:
                break;
        }
    }

}