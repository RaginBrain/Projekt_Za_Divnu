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
                    lista_balona.Add(new Balon(x, prosli.balon, Content.Load<Texture2D>("Bubble_Icon"), Content.Load<Texture2D>("prazan_krug"), Content.Load<Texture2D>("oznaceni_krug"), Content.Load<SpriteFont>("SpriteFont1"), true, razmak_s, razmak_v));
                    x.balon = new Balon(x, prosli.balon, Content.Load<Texture2D>("Bubble_Icon"), Content.Load<Texture2D>("prazan_krug"), Content.Load<Texture2D>("oznaceni_krug"), Content.Load<SpriteFont>("SpriteFont1"), true, razmak_s, razmak_v);
                    lajne.Add(new Crta(Content.Load<Texture2D>("line_liva"),prosli.balon, x.balon, true));
                }
                else if (prosli.desni==x)
                {
                    lista_balona.Add(new Balon(x, prosli.balon, Content.Load<Texture2D>("Bubble_Icon"), Content.Load<Texture2D>("prazan_krug"), Content.Load<Texture2D>("oznaceni_krug"), Content.Load<SpriteFont>("SpriteFont1"), false, razmak_s, razmak_v));
                    x.balon = new Balon(x, prosli.balon, Content.Load<Texture2D>("Bubble_Icon"), Content.Load<Texture2D>("prazan_krug"), Content.Load<Texture2D>("oznaceni_krug"), Content.Load<SpriteFont>("SpriteFont1"), false, razmak_s, razmak_v);
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

        Sprite pozadina;
        SpriteFont spriteFont2;
        Texture2D krug_za_pogadanje;
        Sprite reset;



         CvorStabla korijen;
        Random r;
        int slucajan;
        Balon k;
       public int razmak_s,razmak_v;
        int sirina;
        int visina;
        CvorStabla prazan_cvor;
        List<Balon> lista_balona;
        List<Crta> Lista_crta;
        List<int> lista_brojeva_po_redu;
        

        MouseState previousMouseState;


        
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            // TODO: Add your initialization logic here

            pozadina = new Sprite();
            reset = new Sprite();

            sirina = graphics.PreferredBackBufferWidth;
            visina = graphics.PreferredBackBufferHeight;

            razmak_s = sirina/18;
            razmak_v = visina / 12;
            r = new Random();

            lista_brojeva_po_redu = new List<int>();

            korijen = new CvorStabla(r.Next(40, 60));
            slucajan = r.Next(7,15);
            while (slucajan >= 0)
            {
                int broj_s = r.Next(1, 100);
                Dodaj(korijen, broj_s);
                if(broj_s != korijen.vrijednost && !lista_brojeva_po_redu.Contains(broj_s))
                    lista_brojeva_po_redu.Add(broj_s);
                slucajan--;
            }
            previousMouseState = Mouse.GetState();
            this.IsMouseVisible = true;
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
            pozadina.rect = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            pozadina.textrure = Content.Load<Texture2D>("White_Room_bg");
            spriteFont2 = Content.Load<SpriteFont>("SpriteFont1");
            krug_za_pogadanje = Content.Load<Texture2D>("krug_za_pogadanje");
            reset.rect = new Rectangle(sirina - sirina / 15, visina /50, 48, 48);
            reset.textrure = Content.Load<Texture2D>("reset");



            k = new Balon(korijen, new Rectangle(sirina / 2, 0, 52, 52), Content.Load<Texture2D>("Bubble_Icon"), Content.Load<SpriteFont>("SpriteFont1"));
            korijen.balon = k;
            lista_balona = new List<Balon>();
            lista_balona.Add(k);
            Lista_crta = new List<Crta>();
            Nacrtaj_Cilo_Stablo(korijen, spriteBatch, prazan_cvor, lista_balona, Lista_crta);

            //provjera u sluèaju da ima kolizije balona
            foreach (Balon b in lista_balona)
            {
                bool prikini=false;
                foreach (Balon x in lista_balona)
                {

                    if (b.rect.Intersects(x.rect) && b!=x)
                    {
                        Initialize();
                        prikini = true;
                        break;
                    }
                    
                }
                if (prikini)
                    break;
            }
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

            if (new Rectangle(previousMouseState.X, previousMouseState.Y, 1, 1).Intersects(reset.rect) &&previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Initialize();
            }

            foreach (Balon b in lista_balona)
            {
                if (new Rectangle(previousMouseState.X, previousMouseState.Y, 1, 1).Intersects(b.rect) && b.clicked == false)
                {
                    b.oznacen = true;
                    if (previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (lista_brojeva_po_redu[0].ToString() == b.pisi)
                        {
                            b.clicked = true;
                            lista_brojeva_po_redu.RemoveAt(0);
                        }
                    }
                }
                
                else
                    b.oznacen = false;
                    
                   
            }
            previousMouseState = Mouse.GetState();


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

            spriteBatch.Draw(pozadina.textrure, pozadina.rect, Color.White);
            spriteBatch.Draw(reset.textrure, reset.rect, Color.White);

            if (lista_brojeva_po_redu.Count > 2) 
                spriteBatch.DrawString(spriteFont2, lista_brojeva_po_redu[2].ToString(), new Vector2(sirina/45,50),Color.CadetBlue);
            spriteBatch.Draw(krug_za_pogadanje, new Rectangle(sirina /120, 40, 45, 45), Color.White);

            if (lista_brojeva_po_redu.Count > 1) 
                spriteBatch.DrawString(spriteFont2, lista_brojeva_po_redu[1].ToString(), new Vector2(sirina / 12, 50), Color.CadetBlue);
            spriteBatch.Draw(krug_za_pogadanje, new Rectangle(sirina / 14, 40, 45, 45), Color.White);


            if (lista_brojeva_po_redu.Count > 0) 
                spriteBatch.DrawString(spriteFont2, lista_brojeva_po_redu[0].ToString(), new Vector2(sirina/7,50), Color.IndianRed);
            spriteBatch.Draw(krug_za_pogadanje, new Rectangle(sirina / 8 + sirina / 120, 40, 45, 45),Color.White);

            foreach (Balon b in lista_balona)
                b.Draw(spriteBatch);
            foreach (Crta c in Lista_crta)
                c.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);

            
        }
    }
}
