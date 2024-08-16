using System;
using SFML.Graphics;
using SFML.System;

public class Entity : Sprite
{
    private Sprite sprite;
    private Texture texture;

    public enum EntityType
    {
        NONE,
        PLAYER,
        ENEMY,
        COLLECTABLE,
        FLOOR
    }

    public Entity(string fileName)
    {
        texture = ResourceManager.CreateTexture(fileName);
        sprite = new Sprite(texture);
    }

    public Vector2f Position { get => sprite.Position; set => sprite.Position = value; }

    public Vector2f Scale { get => sprite.Scale; set => sprite.Scale = value; }

    public bool IsAlive { get => isAlive; }

    public bool StopCollision { get => collision; }

    public bool IsFloor { get => isFloor; }

    public CollisionsHandler.CollisionType collisionType;

    public EntityType type = EntityType.NONE;

    public float Rotation { get => sprite.Rotation; set => sprite.Rotation = value; }

    public Sprite Graphic => sprite;

    private bool isAlive = true;
    private bool collision = true;
    protected bool isOnFloor = false;
    protected bool isFloor = false;

    public void Translate(Vector2f translation) => Position += translation;
    public void Rotate(float rotation) => Rotation += rotation;

    virtual public void Draw(RenderWindow renderWindow)
    {
        if (!isAlive) return;
        renderWindow.Draw(Graphic);
    }

    public bool IsColliding(Entity other)
    {
        if (!isAlive) return false;
        if (!collision) return false;
        FloatRect thisBounds = this.Graphic.GetGlobalBounds();
        FloatRect otherBounds = other.Graphic.GetGlobalBounds();

        return thisBounds.Intersects(otherBounds);
    }

    public virtual void ResolveCollision(Vector2f displacement, Entity otherEntity)
    {
        switch (collisionType)
        {
            case CollisionsHandler.CollisionType.STATIC:
                break;
            case CollisionsHandler.CollisionType.DYNAMIC:
                Translate(displacement);
                isOnFloor = displacement.Y < 0;
                break;
            case CollisionsHandler.CollisionType.TRIGGER:
                break;
            default:
                break;
        }
    }

    public virtual void Update(float deltaTime)
    {

    }


    public void Destroy()
    {
        if (!isAlive) return;
        isAlive = false;
        Dispose();
    }

    public void StopCollission()
    {
        if (!collision) return;
        collision = false;
    }
}