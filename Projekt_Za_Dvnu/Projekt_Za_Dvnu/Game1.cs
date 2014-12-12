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

        //mogu ispravit pozivanje cvora i cvor.balon
        public void Nacrtaj_Cilo_Stablo(CvorStabla x, SpriteBatch SpriteBatch,CvorStabla prosli, List<Balon> lista_balona,List<Crta>lajne)
        {
            //dodavanje u listu za crtanje
            if (prosli != null)
            {
                if (prosli.lijevi==x)
                {
                    lista_balona.Add(new Balon(prosli, prosli.balon, Content.Load<Texture2D>("Bubble_Icon"), Content.Load<SpriteFont>("SpriteFont1"), true, razmak_s, razmak_v));
                    x.balon = new Balon(prosli, prosli.balon, Content.Load<Texture2D>("Bubble_Icon"), Content.Load<SpriteFont>("SpriteFont1"), true, razmak_s, razmak_v);
                    lajne.Add(new Crta(Content.Load<Texture2D>("line_liva"),prosli.balon, x.balon, true));    
                }
                else if (prosli.desni==x)
                {
                    lista_balona.Add(new Balon(prosli, prosli.balon, Content.Load<Texture2D>("Bubble_Icon"), Content.Load<SpriteFont>("SpriteFont1"), false, razmak_s, razmak_v));
                    x.balon = new Balon(prosli, prosli.balon, Content.Load<Texture2D>("Bubble_Icon"), Content.Load<SpriteFont>("SpriteFont1"), false, razmak_s, razmak_v);
                    lajne.Add(new Crta(Content.Load<Texture2D>("line_desna"),prosli.balon, x.balon, false));
                    
                }
            }

            //zvanje sljedecih funkcija za crtanje
            if (x != null)
            {
                if(x.lijevi!=null)
                    Nacrtaj_Cilo_Stablo(x.lijevi, SpriteBatch, x, lista_balona, lajne);
                if(x.desni!=null)
                    Nacrtaj_Cilo_Stablo(x.desni, SpriteBatch, x, lista_balona, lajne);
            }
        }

        public Queue<CvorStabla> red_za_crtanje;
         CvorStabla korijen;
        Random r;
        int slucajan;
        Balon k;
       // Balon k,k1,k2,k3,k4;
        //Crta lajna,lajna2,lajna3,lajna4;
       public int razmak_s,razmak_v;
        int sirina;
        int visina;
        CvorStabla prazan_cvor;
        List<Balon> lista_balona;
        List<Crta> Lista_crta;


        
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            // TODO: Add your initialization logic here
            sirina = graphics.PreferredBackBufferWidth;
            visina = graphics.PreferredBackBufferHeight;

            razmak_s = sirina/12;
            razmak_v = visina / 8;
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

            k = new Balon(korijen, new Rectangle(sirina / 2, 0, 56, 56), Content.Load<Texture2D>("Bubble_Icon"), Content.Load<SpriteFont>("SpriteFont1"));
            korijen.balon = k;
            lista_balona = new List<Balon>();
            lista_balona.Add(k);
            Lista_crta = new List<Crta>();
            Nacrtaj_Cilo_Stablo(korijen, spriteBatch, prazan_cvor, lista_balona, Lista_crta);





            //TESTNI DIO
            /*
            k = new Balon(korijen,new Rectangle(sirina/2, 0, 56, 56), Content.Load<Texture2D>("Bubble_Icon"),Content.Load<SpriteFont>("SpriteFont1"));
            k1 = new Balon(korijen, k, Content.Load<Texture2D>("Bubble_Icon"), Content.Load<SpriteFont>("SpriteFont1"),true,razmak_s,razmak_v);
            k2 = new Balon(korijen, k1, Content.Load<Texture2D>("Bubble_Icon"), Content.Load<SpriteFont>("SpriteFont1"), false, razmak_s, razmak_v);
            k3 = new Balon(korijen, k, Content.Load<Texture2D>("Bubble_Icon"), Content.Load<SpriteFont>("SpriteFont1"),false,razmak_s,razmak_v);
            k4 = new Balon(korijen, k3, Content.Load<Texture2D>("Bubble_Icon"), Content.Load<SpriteFont>("SpriteFont1"),true,razmak_s,razmak_v);
            lajna = new Crta(Content.Load<Texture2D>("line_liva"), k, k1, true);
            lajna2 = new Crta(Content.Load<Texture2D>("line_desna"), k1, k2, false);
            lajna3 = new Crta(Content.Load<Texture2D>("line_desna"), k, k3, false);
            lajna4 =  new Crta(Content.Load<Texture2D>("line_liva"), k3, k4, true);
            */
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



            /* TESSSSST*************
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
            
            }*/

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



            /*
            k.Draw(spriteBatch);
            k1.Draw(spriteBatch);
            k2.Draw(spriteBatch);
            k3.Draw(spriteBatch);
            k4.Draw(spriteBatch);
            lajna.Draw(spriteBatch);
            lajna2.Draw(spriteBatch);
            lajna3.Draw(spriteBatch);
            lajna4.Draw(spriteBatch);
            */
            foreach (Balon b in lista_balona)
                b.Draw(spriteBatch);
            foreach (Crta c in Lista_crta)
                c.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
