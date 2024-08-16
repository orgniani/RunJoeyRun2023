using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.ComponentModel.Design;
using static CollisionsHandler;
using SFML.Audio;

namespace Game
{
    public class Player : AnimatedEntity
    {
        private float speed;
        private float jumpSpeed = -500f;
        private float hitSpeed = 300f;

        private float health = 3;

        private Vector2f velocity;
        private const float GRAVITY = 900f;

        private float damageTimer;
        private float damageTime = 0.1f;
        private bool isHit = false;

        private bool endHitAnimation = true;

        private SoundEffect jumpSound;
        private SoundEffect hitSound;
        private SoundEffect gameOverSound;

        public Player(Vector2f position, float speed, string fileName, Vector2i size) : base(fileName, size)
        {
            AnimationData idle = new AnimationData()
            {
                frameSpeed = 20f,
                rowIndex = 0,
                columnCount = 8,
                loops = true
            };

            AnimationData walk = new AnimationData()
            {
                frameSpeed = 20f,
                rowIndex = 2,
                columnCount = 12,
                loops = true
            };

            AnimationData jump = new AnimationData()
            {
                frameSpeed = 20f,
                rowIndex = 4,
                columnCount = 4,
                loops = false
            };

            AnimationData hit = new AnimationData()
            {
                frameSpeed = 20f,
                rowIndex = 7,
                columnCount = 5,
                loops = false
            };

            AddAnimation("Idle", idle);
            AddAnimation("Walk", walk);
            AddAnimation("Jump", jump);
            AddAnimation("Hit", hit);

            Graphic.Position = position;

            type = EntityType.PLAYER;
            collisionType = CollisionsHandler.CollisionType.DYNAMIC;

            SetCurrentAnimation("Idle");

            this.speed = speed;

            //SOUND EFFECTS
            jumpSound = new SoundEffect("Resources/Sounds/jump.wav");
            jumpSound.SetVolume(5f);

            hitSound = new SoundEffect("Resources/Sounds/hit.wav");
            hitSound.SetVolume(5f);

            gameOverSound = new SoundEffect("Resources/Sounds/gameOver.wav");
            gameOverSound.SetVolume(20f);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            velocity.X = 0;

            if (!isOnFloor && Position.Y <= 300) // 504
            {
                velocity.Y += GRAVITY * deltaTime;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && isOnFloor)
            {
                velocity.Y = jumpSpeed;
                isOnFloor = false;
                jumpSound.Play();
            }

            if (isHit)
            {
                endHitAnimation = false;

                velocity.Y = -hitSpeed;
                isOnFloor = false;

                damageTimer += deltaTime;

                if (damageTimer >= damageTime)
                {
                    isHit = false;
                    damageTimer = 0;
                }
            }


            if (isOnFloor)
            {
                endHitAnimation = true;
            }

            if (health == 0)
            {
                StopCollission();
            }

            if(Position.Y > 300) //300
            {
                velocity.Y = 0;
                Vector2f tempVector = Position;
                Position = new Vector2f(tempVector.X, 304); //504
            }

            ProcessAnimation();

            Vector2f movement = velocity * deltaTime;

            Translate(movement);
        }

        private void ProcessAnimation()
        {
            if (isOnFloor)
            {
                if (GameManager.GameStart)
                {
                    SetCurrentAnimation("Walk");
                }

                else
                {
                    SetCurrentAnimation("Idle");
                }
            }

            else if(!isHit && endHitAnimation)
            {
                SetCurrentAnimation("Jump");
            }

            else
            {
                SetCurrentAnimation("Hit");
            }

        }

        public float GetHP()
        {
            return health;
        }

        public void AddHP(float health)
        {
            this.health += health;
        }

        public override void ResolveCollision(Vector2f displacement, Entity otherEntity)
        {
            base.ResolveCollision(displacement, otherEntity);

            switch (otherEntity.type)
            {
                case EntityType.ENEMY:
                    if (isHit) return;

                    health -= 1;
                    hitSound.Play();

                    if (health == 0) gameOverSound.Play();

                    HPHearts.needsAdding(false);

                    isHit = true;
                    break;

                case EntityType.COLLECTABLE:
                    Collectable collectable = otherEntity as Collectable;
                    collectable.OnCollect(this);
                    break;

                default:
                    break;
            }
        }
    }
}