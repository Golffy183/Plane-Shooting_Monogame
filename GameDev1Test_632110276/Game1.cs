using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameDev1Test_632110276
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Player
        Texture2D p_plane;
        Texture2D p_plane_L;
        Texture2D p_plane_R;
        Vector2 p_PlanePos;
        int hp_player;
        bool died = false;
        bool left = false;
        bool right = false;

        // Enemy
        Texture2D enemy1;
        Vector2[] enemy1_Pos = new Vector2[4];

        // Enemy2
        Texture2D enemy2;
        Texture2D eBullet1;
        int[] eBullet_Count = new int[4];
        Vector2[] eBullet_Pos1 = new Vector2[100];
        Vector2[] eBullet_Pos2 = new Vector2[100];
        Vector2[] eBullet_Pos3 = new Vector2[100];
        Vector2[] eBullet_Pos4 = new Vector2[100];
        Vector2[] enemy2_Pos = new Vector2[4];


        // Enemy3
        Texture2D enemy3;
        int[] e3Bullet_Count = new int[9];
        Vector2[] enemy3_Pos = new Vector2[3];
        Vector2[] e3Bullet_Pos1 = new Vector2[100];
        Vector2[] e3Bullet_Pos2 = new Vector2[100];
        Vector2[] e3Bullet_Pos3 = new Vector2[100];
        Vector2[] e3Bullet_Pos4 = new Vector2[100];
        Vector2[] e3Bullet_Pos5 = new Vector2[100];
        Vector2[] e3Bullet_Pos6 = new Vector2[100];
        Vector2[] e3Bullet_Pos7 = new Vector2[100];
        Vector2[] e3Bullet_Pos8 = new Vector2[100];
        Vector2[] e3Bullet_Pos9 = new Vector2[100];

        // life
        Texture2D life;
        private AnimatedTexture lifeCount;
        Vector2[] life_Pos = new Vector2[4];

        // Font
        SpriteFont score_Font;
        int score;

        Random r = new Random();

        Texture2D bg;
        Texture2D bullet1;
        Texture2D bullet2;
        Texture2D special;
        bool bg_move = false;
        int bullet_Count = 0;
        int bullet_Count2 = 0;
        Vector2[] bg_Pos = new Vector2[2];
        Vector2[] special_Pos = new Vector2[2];
        Vector2[] bullet_Pos = new Vector2[50];
        Vector2[] bullet_Pos2 = new Vector2[2];

        KeyboardState keyboardState;

        private static readonly TimeSpan intervalBetweenAttack = TimeSpan.FromMilliseconds(150);
        private TimeSpan lastTimeAttack;
        private static readonly TimeSpan s_intervalBetweenAttack = TimeSpan.FromMilliseconds(500);
        private TimeSpan s_lastTimeAttack;
        private static readonly TimeSpan eIntervalBetweenAttack = TimeSpan.FromMilliseconds(400);
        private TimeSpan eLastTimeAttack;
        private static readonly TimeSpan e3IntervalBetweenAttack = TimeSpan.FromMilliseconds(800);
        private static readonly TimeSpan s_e3IntervalBetweenAttack = TimeSpan.FromMilliseconds(400);
        private TimeSpan e3LastTimeAttack;
        private static readonly TimeSpan IntervalBetweenHp = TimeSpan.FromMilliseconds(100);
        private TimeSpan LastTimeHp;
        private static readonly TimeSpan e3IntervalBetweenHp = TimeSpan.FromMilliseconds(100);
        private TimeSpan e3LastTimeHp;

        private const float Rotation = 0;
        private const float Scale = 1.0f;
        private const float Depth = 0.5f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 640;
            lifeCount = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
            Content.RootDirectory = "Content";
        }
    
        protected override void Initialize()
        {
            p_PlanePos = new Vector2(380, 580);
            bg_Pos[0] = new Vector2(0, -640);
            bg_Pos[1] = new Vector2(0, -1280);
            hp_player = 8;
            score = 0;

            for (int i = 0; i < 4; i++)
            {
                enemy1_Pos[i].X = r.Next(0, 760);
                enemy1_Pos[i].Y = r.Next(-400, -32);
                enemy2_Pos[i].X = r.Next(0, 760);
                enemy2_Pos[i].Y = r.Next(-400, -32);
                life_Pos[i].X = r.Next(0, 760);
                life_Pos[i].Y = r.Next(-1280, -800) * 3;
                eBullet_Count[i] = 0;
            }
            for (int i = 0; i < 3; i++)
            {
                enemy3_Pos[i].X = r.Next(0, 760);
                enemy3_Pos[i].Y = r.Next(-400, -32);
            }
            for (int i = 0; i < 9; i++)
            {
                e3Bullet_Count[i] = 0;
            }
            for (int i = 0; i < 50; i++)
            {
                bullet_Pos[i].X = -100;
            }
            for (int i = 0; i < 2; i++)
            {
                bullet_Pos2[i].X = -100;
            }

            base.Initialize();
        }

        private Vector2 lifeCount_Pos = new Vector2(5, 60);
        private const int hpFrames = 9;
        private const int hpFramesPerSec = 1;
        private const int hpFramesRow = 1;

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            p_plane = Content.Load<Texture2D>("ship_0001");
            p_plane_L = Content.Load<Texture2D>("ship_L");
            p_plane_R = Content.Load<Texture2D>("ship_R");
            enemy1 = Content.Load<Texture2D>("ship_0000");
            enemy2 = Content.Load<Texture2D>("ship_0002");
            enemy3 = Content.Load<Texture2D>("ship_0003");
            special = Content.Load<Texture2D>("special");
            bg = Content.Load<Texture2D>("MapExam");
            bullet1 = Content.Load<Texture2D>("shoot1");
            bullet2 = Content.Load<Texture2D>("shoot2");
            eBullet1 = Content.Load<Texture2D>("eShoot1");
            life = Content.Load<Texture2D>("life");
            lifeCount.Load(Content, "hp", hpFrames, hpFramesRow, hpFramesPerSec);
            score_Font = Content.Load<SpriteFont>("ArialFont");
        }

        
        protected override void UnloadContent()
        {

        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keyboardState = Keyboard.GetState();
            if (died == false)
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    p_PlanePos.Y -= 2;
                }
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    p_PlanePos.Y += 2;
                }

                if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Right))
                {
                    p_PlanePos.X += 0;
                    left = false;
                    right = false;
                }
                else if (keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyUp(Keys.Right))
                {
                    p_PlanePos.X += 0;
                    left = false;
                    right = false;
                }
                else if (keyboardState.IsKeyDown(Keys.Left))
                {
                    p_PlanePos.X -= 3;
                    left = true;
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    p_PlanePos.X += 3;
                    right = true;
                }

                if (eLastTimeAttack + eIntervalBetweenAttack < gameTime.TotalGameTime)
                {
                    eBullet_Pos1[eBullet_Count[0]] = new Vector2(enemy2_Pos[0].X + 16, enemy2_Pos[0].Y + 17);
                    eBullet_Pos2[eBullet_Count[1]] = new Vector2(enemy2_Pos[1].X + 16, enemy2_Pos[1].Y + 17);
                    eBullet_Pos3[eBullet_Count[2]] = new Vector2(enemy2_Pos[2].X + 16, enemy2_Pos[2].Y + 17);
                    eBullet_Pos4[eBullet_Count[3]] = new Vector2(enemy2_Pos[3].X + 16, enemy2_Pos[3].Y + 17);

                    for (int i = 0; i < 4; i++)
                    {
                        if (eBullet_Count[i] >= 99)
                        {
                            eBullet_Count[i] = 0;
                        }
                        eBullet_Count[i] += 1;
                    }

                    eLastTimeAttack = gameTime.TotalGameTime;
                }

                if (score >= 100)
                {
                    if (e3LastTimeAttack + s_e3IntervalBetweenAttack < gameTime.TotalGameTime)
                    {
                        e3Bullet_Pos1[e3Bullet_Count[0]] = new Vector2(enemy3_Pos[0].X + 16, enemy3_Pos[0].Y + 17);
                        e3Bullet_Pos2[e3Bullet_Count[1]] = new Vector2(enemy3_Pos[0].X + 16, enemy3_Pos[0].Y + 17);
                        e3Bullet_Pos3[e3Bullet_Count[2]] = new Vector2(enemy3_Pos[0].X + 16, enemy3_Pos[0].Y + 17);
                        e3Bullet_Pos4[e3Bullet_Count[3]] = new Vector2(enemy3_Pos[1].X + 16, enemy3_Pos[1].Y + 17);
                        e3Bullet_Pos5[e3Bullet_Count[4]] = new Vector2(enemy3_Pos[1].X + 16, enemy3_Pos[1].Y + 17);
                        e3Bullet_Pos6[e3Bullet_Count[5]] = new Vector2(enemy3_Pos[1].X + 16, enemy3_Pos[1].Y + 17);
                        e3Bullet_Pos7[e3Bullet_Count[6]] = new Vector2(enemy3_Pos[2].X + 16, enemy3_Pos[2].Y + 17);
                        e3Bullet_Pos8[e3Bullet_Count[7]] = new Vector2(enemy3_Pos[2].X + 16, enemy3_Pos[2].Y + 17);
                        e3Bullet_Pos9[e3Bullet_Count[8]] = new Vector2(enemy3_Pos[2].X + 16, enemy3_Pos[2].Y + 17);

                        for (int i = 0; i < 9; i++)
                        {
                            if (e3Bullet_Count[i] >= 99)
                            {
                                e3Bullet_Count[i] = 0;
                            }
                            e3Bullet_Count[i] += 1;
                        }

                        e3LastTimeAttack = gameTime.TotalGameTime;
                    }
                }
                else if (score < 100)
                {
                    if (e3LastTimeAttack + e3IntervalBetweenAttack < gameTime.TotalGameTime)
                    {
                        e3Bullet_Pos1[e3Bullet_Count[0]] = new Vector2(enemy3_Pos[0].X + 16, enemy3_Pos[0].Y + 17);
                        e3Bullet_Pos2[e3Bullet_Count[1]] = new Vector2(enemy3_Pos[0].X + 16, enemy3_Pos[0].Y + 17);
                        e3Bullet_Pos3[e3Bullet_Count[2]] = new Vector2(enemy3_Pos[0].X + 16, enemy3_Pos[0].Y + 17);
                        e3Bullet_Pos4[e3Bullet_Count[3]] = new Vector2(enemy3_Pos[1].X + 16, enemy3_Pos[1].Y + 17);
                        e3Bullet_Pos5[e3Bullet_Count[4]] = new Vector2(enemy3_Pos[1].X + 16, enemy3_Pos[1].Y + 17);
                        e3Bullet_Pos6[e3Bullet_Count[5]] = new Vector2(enemy3_Pos[1].X + 16, enemy3_Pos[1].Y + 17);
                        e3Bullet_Pos7[e3Bullet_Count[6]] = new Vector2(enemy3_Pos[2].X + 16, enemy3_Pos[2].Y + 17);
                        e3Bullet_Pos8[e3Bullet_Count[7]] = new Vector2(enemy3_Pos[2].X + 16, enemy3_Pos[2].Y + 17);
                        e3Bullet_Pos9[e3Bullet_Count[8]] = new Vector2(enemy3_Pos[2].X + 16, enemy3_Pos[2].Y + 17);

                        for (int i = 0; i < 9; i++)
                        {
                            if (e3Bullet_Count[i] >= 99)
                            {
                                e3Bullet_Count[i] = 0;
                            }
                            e3Bullet_Count[i] += 1;
                        }

                        e3LastTimeAttack = gameTime.TotalGameTime;
                    }
                }


                Rectangle charPlayer = new Rectangle((int)p_PlanePos.X, (int)p_PlanePos.Y, 48, 48);
                Rectangle[] charEnemy = new Rectangle[4];
                Rectangle[] charEnemy2 = new Rectangle[4];
                Rectangle[] charEnemy3 = new Rectangle[3];
                Rectangle[] charLife = new Rectangle[4];
                Rectangle[] charBullet = new Rectangle[50];
                Rectangle[] charBullet2 = new Rectangle[2];
                // Enemy 2
                Rectangle[] eCharBullet1 = new Rectangle[100];
                Rectangle[] eCharBullet2 = new Rectangle[100];
                Rectangle[] eCharBullet3 = new Rectangle[100];
                Rectangle[] eCharBullet4 = new Rectangle[100];
                // Enemy 3
                Rectangle[] e3CharBullet1 = new Rectangle[100];
                Rectangle[] e3CharBullet2 = new Rectangle[100];
                Rectangle[] e3CharBullet3 = new Rectangle[100];
                Rectangle[] e3CharBullet4 = new Rectangle[100];
                Rectangle[] e3CharBullet5 = new Rectangle[100];
                Rectangle[] e3CharBullet6 = new Rectangle[100];
                Rectangle[] e3CharBullet7 = new Rectangle[100];
                Rectangle[] e3CharBullet8 = new Rectangle[100];
                Rectangle[] e3CharBullet9 = new Rectangle[100];

                // Attack
                if (lastTimeAttack + intervalBetweenAttack < gameTime.TotalGameTime)
                {
                    if (keyboardState.IsKeyDown(Keys.A))
                    {
                        bullet_Count += 1;
                        bullet_Pos[bullet_Count] = new Vector2(p_PlanePos.X + 17, p_PlanePos.Y);
                        if (bullet_Count == 49)
                        {
                            bullet_Count = 1;
                        }
                        lastTimeAttack = gameTime.TotalGameTime;
                    }
                }
                if (s_lastTimeAttack + s_intervalBetweenAttack < gameTime.TotalGameTime)
                {
                    if (keyboardState.IsKeyDown(Keys.S))
                    {
                        if (bullet_Count2 == 1)
                        {
                            bullet_Pos2[0] = new Vector2(p_PlanePos.X + 7, p_PlanePos.Y);
                            bullet_Count2 -= 1;
                        }
                        else if (bullet_Count2 == 2)
                        {
                            bullet_Pos2[1] = new Vector2(p_PlanePos.X + 7, p_PlanePos.Y);
                            bullet_Count2 -= 1;
                        }
                        s_lastTimeAttack = gameTime.TotalGameTime;
                    }
                }

                for (int j = 0; j < 2; j++)
                {
                    charBullet2[j] = new Rectangle((int)bullet_Pos2[j].X, (int)bullet_Pos2[j].Y, 32, 32);
                    for (int k = 0; k < 4; k++)
                    {
                        charEnemy[k] = new Rectangle((int)enemy1_Pos[k].X, (int)enemy1_Pos[k].Y, 48, 48);
                        charEnemy2[k] = new Rectangle((int)enemy2_Pos[k].X, (int)enemy2_Pos[k].Y, 48, 48);
                        if (charBullet2[j].Intersects(charEnemy[k]))
                        {
                            enemy1_Pos[k].X = r.Next(0, 760);
                            enemy1_Pos[k].Y = r.Next(-1280, -600);
                            score += 2;
                        }
                        if (charBullet2[j].Intersects(charEnemy2[k]))
                        {
                            enemy2_Pos[k].X = r.Next(0, 760);
                            enemy2_Pos[k].Y = r.Next(-1280, -600);
                            score += 2;
                        }
                    }
                    for (int l = 0; l < 3; l++)
                    {
                        charEnemy3[l] = new Rectangle((int)enemy3_Pos[l].X, (int)enemy3_Pos[l].Y, 48, 48);
                        if (charBullet2[j].Intersects(charEnemy3[l]))
                        {
                            enemy3_Pos[l].X = r.Next(0, 760);
                            enemy3_Pos[l].Y = r.Next(-1280, -600);
                            score += 2;
                        }
                    }
                    for (int i = 0; i < 100; i++)
                    {
                        eCharBullet1[i] = new Rectangle((int)eBullet_Pos1[i].X, (int)eBullet_Pos1[i].Y, 16, 16);
                        eCharBullet2[i] = new Rectangle((int)eBullet_Pos2[i].X, (int)eBullet_Pos2[i].Y, 16, 16);
                        eCharBullet3[i] = new Rectangle((int)eBullet_Pos3[i].X, (int)eBullet_Pos3[i].Y, 16, 16);
                        eCharBullet4[i] = new Rectangle((int)eBullet_Pos4[i].X, (int)eBullet_Pos4[i].Y, 16, 16);

                        e3CharBullet1[i] = new Rectangle((int)e3Bullet_Pos1[i].X, (int)e3Bullet_Pos1[i].Y, 16, 16);
                        e3CharBullet2[i] = new Rectangle((int)e3Bullet_Pos2[i].X, (int)e3Bullet_Pos2[i].Y, 16, 16);
                        e3CharBullet3[i] = new Rectangle((int)e3Bullet_Pos3[i].X, (int)e3Bullet_Pos3[i].Y, 16, 16);
                        e3CharBullet4[i] = new Rectangle((int)e3Bullet_Pos4[i].X, (int)e3Bullet_Pos4[i].Y, 16, 16);
                        e3CharBullet5[i] = new Rectangle((int)e3Bullet_Pos5[i].X, (int)e3Bullet_Pos5[i].Y, 16, 16);
                        e3CharBullet6[i] = new Rectangle((int)e3Bullet_Pos6[i].X, (int)e3Bullet_Pos6[i].Y, 16, 16);
                        e3CharBullet7[i] = new Rectangle((int)e3Bullet_Pos7[i].X, (int)e3Bullet_Pos7[i].Y, 16, 16);
                        e3CharBullet8[i] = new Rectangle((int)e3Bullet_Pos8[i].X, (int)e3Bullet_Pos8[i].Y, 16, 16);
                        e3CharBullet9[i] = new Rectangle((int)e3Bullet_Pos9[i].X, (int)e3Bullet_Pos9[i].Y, 16, 16);

                        if (charBullet2[j].Intersects(eCharBullet1[i]))
                        {
                            eBullet_Pos1[i].X = -150;
                        }
                        if (charBullet2[j].Intersects(eCharBullet2[i]))
                        {
                            eBullet_Pos2[i].X = -150;
                        }
                        if (charBullet2[j].Intersects(eCharBullet3[i]))
                        {
                            eBullet_Pos3[i].X = -150;
                        }
                        if (charBullet2[j].Intersects(eCharBullet4[i]))
                        {
                            eBullet_Pos4[i].X = -150;
                        }

                        if (charBullet2[j].Intersects(e3CharBullet1[i]))
                        {
                            e3Bullet_Pos1[i].X = -1280;
                        }
                        if (charBullet2[j].Intersects(e3CharBullet2[i]))
                        {
                            e3Bullet_Pos2[i].X = -1280;
                        }
                        if (charBullet2[j].Intersects(e3CharBullet3[i]))
                        {
                            e3Bullet_Pos3[i].X = -1280;
                        }
                        if (charBullet2[j].Intersects(e3CharBullet4[i]))
                        {
                            e3Bullet_Pos4[i].X = -1280;
                        }
                        if (charBullet2[j].Intersects(e3CharBullet5[i]))
                        {
                            e3Bullet_Pos5[i].X = -1280;
                        }
                        if (charBullet2[j].Intersects(e3CharBullet6[i]))
                        {
                            e3Bullet_Pos6[i].X = -1280;
                        }
                        if (charBullet2[j].Intersects(e3CharBullet7[i]))
                        {
                            e3Bullet_Pos7[i].X = -1280;
                        }
                        if (charBullet2[j].Intersects(e3CharBullet8[i]))
                        {
                            e3Bullet_Pos8[i].X = -1280;
                        }
                        if (charBullet2[j].Intersects(e3CharBullet9[i]))
                        {
                            e3Bullet_Pos9[i].X = -1280;
                        }
                    }
                }

                if (bullet_Pos2[0].Y <= 0)
                {
                    bullet_Pos2[0].Y = 650;
                    bullet_Pos2[0].X = -100;
                }
                if (bullet_Pos2[1].Y <= 0)
                {
                    bullet_Pos2[1].Y = 650;
                    bullet_Pos2[1].X = -100;
                }
                bullet_Pos2[0].Y -= 4;
                bullet_Pos2[1].Y -= 4;

                for (int i = 0; i < bullet_Count2; i++)
                {
                    special_Pos[i] = new Vector2(35 + (i * 20), 60);
                }

                for (int i = 0; i < 100; i++)
                {
                    eBullet_Pos1[i].Y += 4;
                    eBullet_Pos2[i].Y += 4;
                    eBullet_Pos3[i].Y += 4;
                    eBullet_Pos4[i].Y += 4;
                    eCharBullet1[i] = new Rectangle((int)eBullet_Pos1[i].X, (int)eBullet_Pos1[i].Y, 16, 16);
                    eCharBullet2[i] = new Rectangle((int)eBullet_Pos2[i].X, (int)eBullet_Pos2[i].Y, 16, 16);
                    eCharBullet3[i] = new Rectangle((int)eBullet_Pos3[i].X, (int)eBullet_Pos3[i].Y, 16, 16);
                    eCharBullet4[i] = new Rectangle((int)eBullet_Pos4[i].X, (int)eBullet_Pos4[i].Y, 16, 16);


                    if (LastTimeHp + IntervalBetweenHp < gameTime.TotalGameTime)
                    {
                        if (charPlayer.Intersects(eCharBullet1[i]))
                        {
                            hp_player -= 1;
                            eBullet_Pos1[i].X = -150;

                            LastTimeHp = gameTime.TotalGameTime;
                        }
                        if (charPlayer.Intersects(eCharBullet2[i]))
                        {
                            hp_player -= 1;
                            eBullet_Pos2[i].X = -150;

                            LastTimeHp = gameTime.TotalGameTime;
                        }
                        if (charPlayer.Intersects(eCharBullet3[i]))
                        {
                            hp_player -= 1;
                            eBullet_Pos3[i].X = -150;

                            LastTimeHp = gameTime.TotalGameTime;
                        }
                        if (charPlayer.Intersects(eCharBullet4[i]))
                        {
                            hp_player -= 1;
                            eBullet_Pos4[i].X = -150;

                            LastTimeHp = gameTime.TotalGameTime;
                        }
                    }

                    for (int j = 0; j < 50; j++)
                    {
                        charBullet[j] = new Rectangle((int)bullet_Pos[j].X, (int)bullet_Pos[j].Y, 16, 16);
                        if (charBullet[j].Intersects(eCharBullet1[i]))
                        {
                            eBullet_Pos1[i].X = -150;
                            bullet_Pos[j].X = -100;
                        }
                        if (charBullet[j].Intersects(eCharBullet2[i]))
                        {
                            eBullet_Pos2[i].X = -150;
                            bullet_Pos[j].X = -100;
                        }
                        if (charBullet[j].Intersects(eCharBullet3[i]))
                        {
                            eBullet_Pos3[i].X = -150;
                            bullet_Pos[j].X = -100;
                        }
                        if (charBullet[j].Intersects(eCharBullet4[i]))
                        {
                            eBullet_Pos4[i].X = -150;
                            bullet_Pos[j].X = -100;
                        }
                    }
                }

                // Enemy3
                for (int i = 0; i < 100; i++)
                {
                    e3Bullet_Pos1[i].Y += 4;
                    e3Bullet_Pos1[i].X -= 2;
                    e3Bullet_Pos2[i].Y += 4;
                    e3Bullet_Pos3[i].Y += 4;
                    e3Bullet_Pos3[i].X += 2;

                    e3Bullet_Pos4[i].Y += 4;
                    e3Bullet_Pos4[i].X -= 2;
                    e3Bullet_Pos5[i].Y += 4;
                    e3Bullet_Pos6[i].Y += 4;
                    e3Bullet_Pos6[i].X += 2;

                    e3Bullet_Pos7[i].Y += 4;
                    e3Bullet_Pos7[i].X -= 2;
                    e3Bullet_Pos8[i].Y += 4;
                    e3Bullet_Pos9[i].Y += 4;
                    e3Bullet_Pos9[i].X += 2;

                    e3CharBullet1[i] = new Rectangle((int)e3Bullet_Pos1[i].X, (int)e3Bullet_Pos1[i].Y, 16, 16);
                    e3CharBullet2[i] = new Rectangle((int)e3Bullet_Pos2[i].X, (int)e3Bullet_Pos2[i].Y, 16, 16);
                    e3CharBullet3[i] = new Rectangle((int)e3Bullet_Pos3[i].X, (int)e3Bullet_Pos3[i].Y, 16, 16);
                    e3CharBullet4[i] = new Rectangle((int)e3Bullet_Pos4[i].X, (int)e3Bullet_Pos4[i].Y, 16, 16);
                    e3CharBullet5[i] = new Rectangle((int)e3Bullet_Pos5[i].X, (int)e3Bullet_Pos5[i].Y, 16, 16);
                    e3CharBullet6[i] = new Rectangle((int)e3Bullet_Pos6[i].X, (int)e3Bullet_Pos6[i].Y, 16, 16);
                    e3CharBullet7[i] = new Rectangle((int)e3Bullet_Pos7[i].X, (int)e3Bullet_Pos7[i].Y, 16, 16);
                    e3CharBullet8[i] = new Rectangle((int)e3Bullet_Pos8[i].X, (int)e3Bullet_Pos8[i].Y, 16, 16);
                    e3CharBullet9[i] = new Rectangle((int)e3Bullet_Pos9[i].X, (int)e3Bullet_Pos9[i].Y, 16, 16);

                    if (e3LastTimeHp + e3IntervalBetweenHp < gameTime.TotalGameTime)
                    {
                        if (charPlayer.Intersects(e3CharBullet1[i]))
                        {
                            hp_player -= 1;
                            e3Bullet_Pos1[i].X = -1280;

                            e3LastTimeHp = gameTime.TotalGameTime;
                        }
                        if (charPlayer.Intersects(e3CharBullet2[i]))
                        {
                            hp_player -= 1;
                            e3Bullet_Pos2[i].X = -1280;

                            e3LastTimeHp = gameTime.TotalGameTime;
                        }
                        if (charPlayer.Intersects(e3CharBullet3[i]))
                        {
                            hp_player -= 1;
                            e3Bullet_Pos3[i].X = -1280;

                            e3LastTimeHp = gameTime.TotalGameTime;
                        }
                        if (charPlayer.Intersects(e3CharBullet4[i]))
                        {
                            hp_player -= 1;
                            e3Bullet_Pos4[i].X = -1280;

                            e3LastTimeHp = gameTime.TotalGameTime;
                        }
                        if (charPlayer.Intersects(e3CharBullet5[i]))
                        {
                            hp_player -= 1;
                            e3Bullet_Pos5[i].X = -1280;

                            e3LastTimeHp = gameTime.TotalGameTime;
                        }
                        if (charPlayer.Intersects(e3CharBullet6[i]))
                        {
                            hp_player -= 1;
                            e3Bullet_Pos6[i].X = -1280;

                            e3LastTimeHp = gameTime.TotalGameTime;
                        }
                        if (charPlayer.Intersects(e3CharBullet7[i]))
                        {
                            hp_player -= 1;
                            e3Bullet_Pos7[i].X = -1280;

                            e3LastTimeHp = gameTime.TotalGameTime;
                        }
                        if (charPlayer.Intersects(e3CharBullet8[i]))
                        {
                            hp_player -= 1;
                            e3Bullet_Pos8[i].X = -1280;

                            e3LastTimeHp = gameTime.TotalGameTime;
                        }
                        if (charPlayer.Intersects(e3CharBullet9[i]))
                        {
                            hp_player -= 1;
                            e3Bullet_Pos9[i].X = -1280;

                            e3LastTimeHp = gameTime.TotalGameTime;
                        }
                    }

                    for (int j = 0; j < 50; j++)
                    {
                        charBullet[j] = new Rectangle((int)bullet_Pos[j].X, (int)bullet_Pos[j].Y, 16, 16);
                        if (charBullet[j].Intersects(e3CharBullet1[i]))
                        {
                            e3Bullet_Pos1[i].X = -1280;
                            bullet_Pos[j].X = -100;
                        }
                        if (charBullet[j].Intersects(e3CharBullet2[i]))
                        {
                            e3Bullet_Pos2[i].X = -1280;
                            bullet_Pos[j].X = -100;
                        }
                        if (charBullet[j].Intersects(e3CharBullet3[i]))
                        {
                            e3Bullet_Pos3[i].X = -1280;
                            bullet_Pos[j].X = -100;
                        }
                        if (charBullet[j].Intersects(e3CharBullet4[i]))
                        {
                            e3Bullet_Pos4[i].X = -1280;
                            bullet_Pos[j].X = -100;
                        }
                        if (charBullet[j].Intersects(e3CharBullet5[i]))
                        {
                            e3Bullet_Pos5[i].X = -1280;
                            bullet_Pos[j].X = -100;
                        }
                        if (charBullet[j].Intersects(e3CharBullet6[i]))
                        {
                            e3Bullet_Pos6[i].X = -1280;
                            bullet_Pos[j].X = -100;
                        }
                        if (charBullet[j].Intersects(e3CharBullet7[i]))
                        {
                            e3Bullet_Pos7[i].X = -1280;
                            bullet_Pos[j].X = -100;
                        }
                        if (charBullet[j].Intersects(e3CharBullet8[i]))
                        {
                            e3Bullet_Pos8[i].X = -1280;
                            bullet_Pos[j].X = -100;
                        }
                        if (charBullet[j].Intersects(e3CharBullet9[i]))
                        {
                            e3Bullet_Pos9[i].X = -1280;
                            bullet_Pos[j].X = -100;
                        }
                    }
                }

                for (int i = 0; i < 50; i++)
                {
                    bullet_Pos[i].Y -= 4;
                    for (int j = 0; j < 4; j++)
                    {
                        charEnemy[j] = new Rectangle((int)enemy1_Pos[j].X, (int)enemy1_Pos[j].Y, 48, 48);
                        charEnemy2[j] = new Rectangle((int)enemy2_Pos[j].X, (int)enemy2_Pos[j].Y, 48, 48);
                        charLife[j] = new Rectangle((int)life_Pos[j].X, (int)life_Pos[j].Y, 32, 32);
                        // Enemy1
                        if (j != 0)
                        {
                            if (charEnemy[j].Intersects(charEnemy[j - 1]))
                            {
                                enemy1_Pos[j - 1].X = r.Next(0, 760);
                                enemy1_Pos[j - 1].Y = r.Next(-1280, -600);
                            }
                        }
                        if (charBullet[i].Intersects(charEnemy[j]))
                        {
                            enemy1_Pos[j].X = r.Next(0, 760);
                            enemy1_Pos[j].Y = r.Next(-1280, -600);
                            bullet_Pos[i].Y = 650;
                            bullet_Pos[i].X = -100;
                            score += 2;
                        }
                        if (charPlayer.Intersects(charEnemy[j]))
                        {
                            enemy1_Pos[j].X = r.Next(0, 760);
                            enemy1_Pos[j].Y = r.Next(-1280, -600);
                            hp_player -= 1;
                        }
                        // Enemy2
                        if (j != 0)
                        {
                            if (charEnemy2[j].Intersects(charEnemy2[j - 1]))
                            {
                                enemy2_Pos[j - 1].X = r.Next(0, 760);
                                enemy2_Pos[j - 1].Y = r.Next(-1280, -600);
                            }
                        }
                        if (charBullet[i].Intersects(charEnemy2[j]))
                        {
                            enemy2_Pos[j].X = r.Next(0, 760);
                            enemy2_Pos[j].Y = r.Next(-1280, -600);
                            eBullet_Count[j] = 0;
                            bullet_Pos[i].Y = 650;
                            bullet_Pos[i].X = -100;
                            score += 2;
                        }
                        if (charPlayer.Intersects(charEnemy2[j]))
                        {
                            enemy2_Pos[j].X = r.Next(0, 760);
                            enemy2_Pos[j].Y = r.Next(-1280, -600);
                            hp_player -= 1;
                        }

                        if (charPlayer.Intersects(charLife[j]))
                        {
                            life_Pos[j].X = r.Next(0, 760);
                            life_Pos[j].Y = r.Next(-1280, -800) * 3;
                            hp_player += 2;
                            bullet_Count2 += 1;
                            if (hp_player >= 8)
                            {
                                hp_player = 8;
                            }
                            if (bullet_Count2 >= 2)
                            {
                                bullet_Count2 = 2;
                            }
                        }
                    }
                    // Enemy3
                    for (int k = 0; k < 3; k++)
                    {
                        charEnemy3[k] = new Rectangle((int)enemy3_Pos[k].X, (int)enemy3_Pos[k].Y, 48, 48);
                        if (k != 0)
                        {
                            if (charEnemy3[k].Intersects(charEnemy3[k - 1]))
                            {
                                enemy3_Pos[k - 1].X = r.Next(0, 760);
                                enemy3_Pos[k - 1].Y = r.Next(-1280, -600);
                            }
                        }
                        if (charBullet[i].Intersects(charEnemy3[k]))
                        {
                            enemy3_Pos[k].X = r.Next(0, 760);
                            enemy3_Pos[k].Y = r.Next(-1280, -600);
                            bullet_Pos[i].Y = 650;
                            bullet_Pos[i].X = -100;
                            score += 2;
                        }
                        if (charPlayer.Intersects(charEnemy3[k]))
                        {
                            enemy3_Pos[k].X = r.Next(0, 760);
                            enemy3_Pos[k].Y = r.Next(-1280, -600);
                            hp_player -= 1;
                        }
                    }
                    if (bullet_Pos[i].Y <= 0)
                    {
                        bullet_Pos[i].Y = 650;
                        bullet_Pos[i].X = -100;
                    }
                }

                if (p_PlanePos.X <= 0)
                {
                    p_PlanePos.X += 3;
                }
                else if (p_PlanePos.X >= 760)
                {
                    p_PlanePos.X -= 3;
                }
                if (p_PlanePos.Y <= 0)
                {
                    p_PlanePos.Y += 2;
                }
                else if (p_PlanePos.Y >= 600)
                {
                    p_PlanePos.Y -= 2;
                }

                bg_Pos[0].Y += 1;
                if (bg_Pos[0].Y >= 0)
                {
                    bg_move = true;
                }
                if (bg_Pos[0].Y >= 640)
                {
                    bg_Pos[0].Y = -1280;
                }
                if (bg_move == true)
                {
                    bg_Pos[1].Y += 1;
                }
                if (bg_Pos[1].Y >= 640)
                {
                    bg_Pos[1].Y = -1280;
                }

                for (int i = 0; i < 4; i++)
                {
                    enemy1_Pos[i].Y += 5;
                    life_Pos[i].Y += 2;
                    if (enemy1_Pos[i].Y >= 700)
                    {
                        enemy1_Pos[i].X = r.Next(0, 760);
                        enemy1_Pos[i].Y = r.Next(-400, -50);
                    }
                    if (life_Pos[i].Y >= 700)
                    {
                        life_Pos[i].X = r.Next(0, 760);
                        life_Pos[i].Y = r.Next(-1280, -800) * 3;
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    enemy2_Pos[i].Y += 2;
                    if (enemy2_Pos[i].Y >= 700)
                    {
                        enemy2_Pos[i].X = r.Next(0, 760);
                        enemy2_Pos[i].Y = r.Next(-400, -50);
                        eBullet_Count[i] = 0;
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    enemy3_Pos[i].Y += 2;
                    if (enemy3_Pos[i].Y >= 700)
                    {
                        enemy3_Pos[i].X = r.Next(0, 760);
                        enemy3_Pos[i].Y = r.Next(-400, -50);
                        if (i == 0)
                        {
                            e3Bullet_Count[0] = 0;
                            e3Bullet_Count[1] = 0;
                            e3Bullet_Count[2] = 0;
                        }
                        if (i == 1)
                        {
                            e3Bullet_Count[3] = 0;
                            e3Bullet_Count[4] = 0;
                            e3Bullet_Count[5] = 0;
                        }
                        if (i == 2)
                        {
                            e3Bullet_Count[6] = 0;
                            e3Bullet_Count[7] = 0;
                            e3Bullet_Count[8] = 0;
                        }
                    }
                }

                if (hp_player <= 0)
                {
                    died = true;
                }
            }
            if (died == true)
            {
                if (keyboardState.IsKeyDown(Keys.R))
                {
                    p_PlanePos = new Vector2(380, 580);
                    bg_Pos[0] = new Vector2(0, -640);
                    bg_Pos[1] = new Vector2(0, -1280);
                    hp_player = 8;
                    score = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        enemy1_Pos[i].X = r.Next(0, 760);
                        enemy1_Pos[i].Y = r.Next(-400, -32);
                        enemy2_Pos[i].X = r.Next(0, 760);
                        enemy2_Pos[i].Y = r.Next(-400, -32);
                        life_Pos[i].X = r.Next(0, 760);
                        life_Pos[i].Y = r.Next(-1280, -800) * 3;
                        eBullet_Count[i] = 0;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        enemy3_Pos[i].X = r.Next(0, 760);
                        enemy3_Pos[i].Y = r.Next(-400, -32);
                    }
                    for (int i = 0; i < 9; i++)
                    {
                        e3Bullet_Count[i] = 0;
                    }
                    for (int i = 0; i < 50; i++)
                    {
                        bullet_Pos[i].X = -100;
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        bullet_Pos2[i].X = -100;
                    }

                    for (int i = 0; i < 100; i++)
                    {
                        eBullet_Pos1[i].X = -150;
                        eBullet_Pos2[i].X = -150;
                        eBullet_Pos3[i].X = -150;
                        eBullet_Pos4[i].X = -150;
                        e3Bullet_Pos1[i].X = -1280;
                        e3Bullet_Pos2[i].X = -1280;
                        e3Bullet_Pos3[i].X = -1280;
                        e3Bullet_Pos4[i].X = -1280;
                        e3Bullet_Pos5[i].X = -1280;
                        e3Bullet_Pos6[i].X = -1280;
                        e3Bullet_Pos7[i].X = -1280;
                        e3Bullet_Pos8[i].X = -1280;
                        e3Bullet_Pos9[i].X = -1280;
                    }
                    died = false;
                }
            }

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(bg, bg_Pos[0], Color.White);
            if (bg_move == true)
            {
                spriteBatch.Draw(bg, bg_Pos[1], Color.White);
            }
            if (died == false) 
            {
                for (int i = 0; i < 100; i++)
                {
                    spriteBatch.Draw(eBullet1, eBullet_Pos1[i], Color.White);
                    spriteBatch.Draw(eBullet1, eBullet_Pos2[i], Color.White);
                    spriteBatch.Draw(eBullet1, eBullet_Pos3[i], Color.White);
                    spriteBatch.Draw(eBullet1, eBullet_Pos4[i], Color.White);
                    spriteBatch.Draw(eBullet1, e3Bullet_Pos1[i], Color.White);
                    spriteBatch.Draw(eBullet1, e3Bullet_Pos2[i], Color.White);
                    spriteBatch.Draw(eBullet1, e3Bullet_Pos3[i], Color.White);
                    spriteBatch.Draw(eBullet1, e3Bullet_Pos4[i], Color.White);
                    spriteBatch.Draw(eBullet1, e3Bullet_Pos5[i], Color.White);
                    spriteBatch.Draw(eBullet1, e3Bullet_Pos6[i], Color.White);
                    spriteBatch.Draw(eBullet1, e3Bullet_Pos7[i], Color.White);
                    spriteBatch.Draw(eBullet1, e3Bullet_Pos8[i], Color.White);
                    spriteBatch.Draw(eBullet1, e3Bullet_Pos9[i], Color.White);
                }
                for (int i = 0; i < 50; i++)
                {
                    spriteBatch.Draw(bullet1, bullet_Pos[i], Color.White);
                }

                for (int i = 0; i < 4; i++)
                {
                    spriteBatch.Draw(enemy1, enemy1_Pos[i], Color.White);
                    spriteBatch.Draw(enemy2, enemy2_Pos[i], Color.White);
                    spriteBatch.Draw(life, life_Pos[i], Color.White);
                }
                for (int i = 0; i < 3; i++)
                {
                    spriteBatch.Draw(enemy3, enemy3_Pos[i], Color.White);
                }
                for (int i = 0; i < bullet_Count2; i++)
                {
                    spriteBatch.Draw(special, special_Pos[i], Color.White);
                }
                for (int i = 0; i < 2; i++)
                {
                    spriteBatch.Draw(bullet2, bullet_Pos2[i], Color.White);
                }
            }
            if (left == true)
            {
                spriteBatch.Draw(p_plane_L, p_PlanePos, Color.White);
            }
            else if (right == true)
            {
                spriteBatch.Draw(p_plane_R, p_PlanePos, Color.White);
            }
            else
            {
                spriteBatch.Draw(p_plane, p_PlanePos, Color.White);
            }

            switch (hp_player)
            {
                case 0:
                    lifeCount.DrawFrame(spriteBatch, 0, lifeCount_Pos);
                    break;
                case 1:
                    lifeCount.DrawFrame(spriteBatch, 1, lifeCount_Pos);
                    break;
                case 2:
                    lifeCount.DrawFrame(spriteBatch, 2, lifeCount_Pos);
                    break;
                case 3:
                    lifeCount.DrawFrame(spriteBatch, 3, lifeCount_Pos);
                    break;
                case 4:
                    lifeCount.DrawFrame(spriteBatch, 4, lifeCount_Pos);
                    break;
                case 5:
                    lifeCount.DrawFrame(spriteBatch, 5, lifeCount_Pos);
                    break;
                case 6:
                    lifeCount.DrawFrame(spriteBatch, 6, lifeCount_Pos);
                    break;
                case 7:
                    lifeCount.DrawFrame(spriteBatch, 7, lifeCount_Pos);
                    break;
                case 8:
                    lifeCount.DrawFrame(spriteBatch, 8, lifeCount_Pos);
                    break;
            }
            string strScore;
            string strStart;
            strScore = "Score : " + score.ToString();
            strStart = "Press \"R\" to restart.";
            if (died == true)
            {
                spriteBatch.DrawString(score_Font, strScore, new Vector2(365, 280), Color.Black);
                spriteBatch.DrawString(score_Font, strStart, new Vector2(315, 310), Color.Black);
            }
            else
            {
                spriteBatch.DrawString(score_Font, strScore, new Vector2(10, 20), Color.Black);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
