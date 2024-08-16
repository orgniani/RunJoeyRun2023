using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using System.Drawing;

public class AnimatedEntity : Entity
{
    private Dictionary<string, AnimationData> animations = new Dictionary<string, AnimationData>();

    private Vector2i frameSize;
    private Vector2i framePosition;
    private string currentAnimation;
    private float currentFrameTime;
    private float animationTimer;

    public AnimatedEntity(string fileName, Vector2i size) : base(fileName)
    {
        this.frameSize = size;
        Graphic.TextureRect = new IntRect()
        {
            Left = 0,
            Top = 0,
            Height = frameSize.Y,
            Width = frameSize.X,
        };
    }

    public void SetCurrentAnimation(string animationName)
    {
        if (!animations.ContainsKey(animationName))
        {
            Console.WriteLine($"The animation {animationName} doesnt exist");
        }

        if (animationName != currentAnimation)
        {
            currentAnimation = animationName;
            currentFrameTime = 1f / animations[currentAnimation].frameSpeed;

            framePosition = new Vector2i(0, animations[currentAnimation].rowIndex);
        }
    }

    public void AddAnimation(string name, AnimationData animation)
    {
        if (animations.ContainsKey(name))
        {
            Console.WriteLine($"The animation {name} already exists");
        }
        else animations.Add(name, animation);
    }

    public void RemoveAnimation(string name)
    {
        if (!animations.ContainsKey(name))
        {
            Console.WriteLine($"The animation {name} doesnt exist");
        }
        else animations.Remove(name);
    }

    public virtual void Update(float deltaTime)
    {
        if (currentAnimation == null)
            return;

        animationTimer += deltaTime;

        if (animationTimer >= currentFrameTime)
        {
            animationTimer -= currentFrameTime;

            if (framePosition.X < animations[currentAnimation].columnCount - 1)
                framePosition.X++;

            else if (animations[currentAnimation].loops)
                framePosition.X = 0;

            Graphic.TextureRect = new IntRect()
            {
                Left = framePosition.X * frameSize.X,
                Top = framePosition.Y * frameSize.Y,
                Width = frameSize.X,
                Height = frameSize.Y
            };
        }
    }
}