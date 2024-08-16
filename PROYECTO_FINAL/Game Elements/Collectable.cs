using SFML.System;
using System;
using System.Collections.Generic;
using Game;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

namespace Game
{
    public class Collectable : AnimatedEntity
    { 
        public enum CollectableType
        {
            STRAWBERRY,
            HEART
        }

        private CollectableType CollectType;

        private float speed = 100f;
        private Vector2i size = new Vector2i(0, 0);
        private SoundEffect collectSound;

        public Collectable(Vector2f position, Vector2i size, string fileName, CollectableType collectableType) : base(fileName, size)
        {
            CollectType = collectableType;
            type = EntityType.COLLECTABLE;

            AnimationData strawberry= new AnimationData()
            {
                frameSpeed = 20f,
                rowIndex = 0,
                columnCount = 17,
                loops = true
            };

            AnimationData heart = new AnimationData()
            {
                frameSpeed = 20f,
                rowIndex = 0,
                columnCount = 8,
                loops = true
            };

            AddAnimation("Strawberry", strawberry);
            AddAnimation("Heart", heart);

            if (collectableType == CollectableType.STRAWBERRY)
            {
                SetCurrentAnimation("Strawberry");
            }

            else
            {
                SetCurrentAnimation("Heart");
            }

            Graphic.Position = position;
            this.size = size;

            collectSound = new SoundEffect("Resources/Sounds/collect.wav");
            collectSound.SetVolume(15f);
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
                CollectableManager.RemoveCollectable(this);
                Destroy();
            }
        }

        public void OnCollect(Player player)
        {
            switch (CollectType)
            {
                case CollectableType.STRAWBERRY:
                    GameManager.AddScore(100);
                    break;

                case CollectableType.HEART:
                    if(player.GetHP() < 3)
                    {
                        player.AddHP(1);
                        HPHearts.needsAdding(true);
                    }
                    break;

                default:
                    break;
            }
            collectSound.Play();
            CollectableManager.RemoveCollectable(this);
            Destroy();
        }
    }
}