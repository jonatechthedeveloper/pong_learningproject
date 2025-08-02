using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pongcopy;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D padtexture;

    private Texture2D balltexture;

    private Vector2 ballcoordinates;
    private Rectangle ball;
    private Rectangle padone;
    private Vector2 padonecoordinates;
    private Rectangle padtwo;
    private Vector2 padtwocoordinates;
    private SpriteFont textfont;

    private double score;
    private double score_padtwo;
    private int velocity = 3;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferWidth = 500;
        _graphics.PreferredBackBufferHeight = 455;
    }

    protected override void Initialize()
    {

        score = 0;
        

        ballcoordinates = new Vector2(238,202);
        padonecoordinates = new Vector2(99,206);
        padtwocoordinates = new Vector2(423,206);
        


        padone = new Rectangle((int)padonecoordinates.X,(int)padonecoordinates.Y,20,70);
        padtwo = new Rectangle((int)padtwocoordinates.X,(int)padtwocoordinates.Y,20,70);
        ball = new Rectangle((int)ballcoordinates.X,(int)ballcoordinates.Y,64,64);

        


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        padtexture = Content.Load<Texture2D>("padonetexture.png");
        balltexture = Content.Load<Texture2D>("balltexture.png");

    }

    protected override void Update(GameTime gameTime)
    {


        ball.X -= velocity;
        ball.Y += velocity;
        


        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            padone.Y -= velocity;
            padtwo.Y += velocity;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            padone.Y += velocity;
            padtwo.Y -= velocity;
        }



        if (ball.X < 0 && ball.Y > 0)
        {   
            ball.X = 238;
            ball.Y = 202;
        }

        if (ball.X > 0 && ball.Y < 0)
        {   
            ball.X = 202;
            ball.Y = 238;
        }

        if (ball.Y > padone.Y)
        {
            score_padtwo += 1;
            Console.WriteLine("pad two has scored:" + score_padtwo);
        }
        
        
        if (ball.Y < padone.Y)
        {
            score -= 1;
            Console.WriteLine("pad one has scored:" + score);
        }

        if (ball.X < padone.X)
        {
            score -= 1;
            Console.WriteLine("pad two has scored:" + score_padtwo);
        }
        
        

        if (ball.Y > padtwo.Y)
        {
            score += 1;
            Console.WriteLine("pad one has scored:" + score);
        }

        if (ball.Y < padtwo.Y)
        {
            score -= 1;
            Console.WriteLine("pad one has scored:" + score);
        }

        if (score < 0)
        {
            score = 0;
        }

        if (score_padtwo < 0)
        {
            score_padtwo = 0;
        }


        if (Keyboard.GetState().IsKeyDown(Keys.F1))
        {
            Console.WriteLine("padone scored:"+score);
            Console.WriteLine("pad two scored:" + score_padtwo);

            if (score > score_padtwo)
            {
                Console.WriteLine("\npadone has won with " + score + " points");
                Console.WriteLine("\npadtwo failed with " + score_padtwo + " points");
                Exit();
            }
            else if (score_padtwo > score)
            {
                Console.WriteLine("\npadtwo has won with " + score_padtwo + " points");
                Console.WriteLine("\npadone failed with " + score + " points");
                Exit();
            }
        }
         


         




        if (padtwo.Intersects(ball))
            {
                velocity += 1;
            }


        if (padone.Intersects(ball))
        {
            velocity -= 1;
        }
        

        // Console.WriteLine("X:" + ball.X + "Y:" + ball.Y);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(padtexture,padone,Color.White);
        _spriteBatch.Draw(padtexture,padtwo,Color.White);
        _spriteBatch.Draw(balltexture,ball,Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
