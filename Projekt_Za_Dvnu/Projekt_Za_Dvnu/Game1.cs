using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Projekt_Za_Dvnu
{
    /// <summary>
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
    /// This is the main type for your game
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        public void Dodaj(CvorStabla x, int broj)
        {
            CvorStabla novi = new CvorStabla(broj);
            if (x == null)
                x = novi;
            else if (novi.vrijednost < x.vrijednost)
            {
                if (x.lijevi == null)
                    x.lijevi = novi;
                else
                    Dodaj(x.lijevi, broj);
            }
            else if (novi.vrijednost > x.vrijednost)
            {
                if (x.desni == null)
                    x.desni = novi;
                else
                    Dodaj(x.desni, broj);
            }
        }
        public void Dodaj_u_red_za_crtanje(CvorStabla y)
        {
            red_za_crtanje.Enqueue(y);
            if (y.lijevi != null)
                Dodaj_u_red_za_crtanje(y.lijevi);
            if (y.desni != null)
                Dodaj_u_red_za_crtanje(y.desni);
        }

        public Queue<CvorStabla> red_za_crtanje;
         CvorStabla korijen;
        Random r;
        int slucajan;
        Balon k,k1,k2,k3,k4;
        Crta lajna,lajna2,lajna3,lajna4;
        int razmak;


        
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            // TODO: Add your initialization logic here
            razmak = 50;
            r = new Random();

            red_za_crtanje = new Queue<CvorStabla>();
            korijen = new CvorStabla(r.Next(25, 75));
            slucajan = r.Next(10, 20);
            while (slucajan >= 0)
            {
                Dodaj(korijen, r.Next(1, 100));
                slucajan--;
            }
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            k = new Balon(korijen,new Rectangle(200, 200, 56, 56), Content.Load<Texture2D>("Bubble_Icon"),Content.Load<SpriteFont>("SpriteFont1"));
            k1 = new Balon(korijen, new Rectangle(150, 250, 56, 56), Content.Load<Texture2D>("Bubble_Icon"), Content.Load<SpriteFont>("SpriteFont1"));
            k2 = new Balon(korijen, new Rectangle(200, 300, 56, 56), Content.Load<Texture2D>("Bubble_Icon"), Content.Load<SpriteFont>("SpriteFont1"));
            k3 = new Balon(korijen, new Rectangle(250, 250, 56, 56), Content.Load<Texture2D>("Bubble_Icon"), Content.Load<SpriteFont>("SpriteFont1"));
            k4 = new Balon(korijen, new Rectangle(200, 300, 56, 56), Content.Load<Texture2D>("Bubble_Icon"), Content.Load<SpriteFont>("SpriteFont1"));
            lajna = new Crta(Content.Load<Texture2D>("line_liva"), k, k1, true);
            lajna2 = new Crta(Content.Load<Texture2D>("line_desna"), k1, k2, false);
            lajna3 = new Crta(Content.Load<Texture2D>("line_desna"), k, k3, false);
            lajna4 =  new Crta(Content.Load<Texture2D>("line_liva"), k3, k4, true);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            if (k2.rect.Intersects(k4.rect))
            {
                k1.rect.X -= 1;
                k3.rect.X +=1;
                k2.rect.X -= 1;
                k4.rect.X += 1;
                if (!k2.rect.Intersects(k4.rect))
                {
                    k1.rect.X -= 5;
                    k3.rect.X += 5;
                    k2.rect.X -= 5;
                    k4.rect.X += 5;
                    lajna.PostaviPravokutnik();
                    lajna2.PostaviPravokutnik();
                    lajna3.PostaviPravokutnik();
                    lajna4.PostaviPravokutnik();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            k.Draw(spriteBatch);
            k1.Draw(spriteBatch);
            k2.Draw(spriteBatch);
            k3.Draw(spriteBatch);
            k4.Draw(spriteBatch);
            lajna.Draw(spriteBatch);
            lajna2.Draw(spriteBatch);
            lajna3.Draw(spriteBatch);
            lajna4.Draw(spriteBatch);
            
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
