using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projekt_Za_Dvnu
{


    public class Broj:Sprite
    {
        public bool clicked;
        string pisi;

    }
    public class Sprite
    {
        public Rectangle rect;
        public Texture2D textrure;

        public Sprite()
        {
        }
    }

    public class Balon : Sprite
    {
        Texture2D oznaceno;
        Texture2D prazno_misto;
        SpriteFont font;
        CvorStabla c;
       public string pisi;
       public float lvl;
       public bool liv;
       public bool clicked;
       public bool oznacen;

        public Balon(CvorStabla cvor,Rectangle r,Texture2D t,SpriteFont a)
        {
            font = a;
            rect = r;
            textrure = t;
            c = cvor;
            pisi = cvor.vrijednost.ToString();
            lvl= 3f;
            clicked = true;
        }
        public Balon(Rectangle r, Texture2D t, SpriteFont a,int broj)
        {
            font = a;
            rect = r;
            textrure = t;
            pisi = broj.ToString();
            lvl = 3f;
            clicked = true;
        }
        public Balon(CvorStabla cvor, Balon roditelj, Texture2D t,Texture2D prazno,Texture2D oz, SpriteFont a,bool livi,int razmak_sirina,int razmak_visina)
        {
            if (roditelj.lvl > 1.01)
                lvl = roditelj.lvl - roditelj.lvl / 3;
            else
                lvl = roditelj.lvl;
            if(livi)
                rect = new Rectangle(roditelj.rect.X - (int)(razmak_sirina * lvl + Math.Pow(lvl+0.4,5)), roditelj.rect.Y + (int)(razmak_visina * (1.8 - lvl / 2) + Math.Pow(lvl,4)), roditelj.rect.Width, roditelj.rect.Height);
            else
                rect = new Rectangle(roditelj.rect.X + (int)(razmak_sirina * lvl + Math.Pow(lvl+0.4,5)), roditelj.rect.Y + (int)(razmak_visina * (1.8 - lvl / 2) + Math.Pow(lvl, 4)), roditelj.rect.Width, roditelj.rect.Height);

            oznacen = false;
            clicked = false;
            textrure = t;
            c = cvor;
            pisi = cvor.vrijednost.ToString();
            prazno_misto = prazno;
            font = a;
            oznaceno = oz;
            
        }

        public void Update(GameTime gameTime)
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (clicked)
            {
                spriteBatch.Draw(textrure, rect, Color.White);
                spriteBatch.DrawString(font, pisi, new Vector2((int)(rect.X + rect.Width/3.5f), (int)(rect.Y + rect.Height / 3.5f)), Color.Green);
            }
            else if (oznacen)
                spriteBatch.Draw(oznaceno, rect, Color.White);
            else
                spriteBatch.Draw(prazno_misto, rect, Color.White);
        }
        public void DrawVece(SpriteBatch spriteBatch)
        {
            if (clicked)
            {
                spriteBatch.Draw(textrure, new Rectangle(rect.X-10,rect.Y-10,rect.Width+20,rect.Height+20), Color.White);
                spriteBatch.DrawString(font, pisi, new Vector2((int)(rect.X + rect.Width / 3.5f), (int)(rect.Y + rect.Height / 3.5f)), Color.Green);
            }
            else if (oznacen)
                spriteBatch.Draw(oznaceno, rect, Color.White);
            else
                spriteBatch.Draw(prazno_misto, rect, Color.White);
        }


    }

    public class Crta : Sprite
    {
        public Balon prvi;
        public Balon drugi;

        public bool livi_smjer;
        public void PostaviPravokutnik()
        {
            int sirina = Math.Abs(prvi.rect.X - drugi.rect.X);
            int visina = Math.Abs(prvi.rect.Y - drugi.rect.Y);
            //bit je odrediti debljnu i poziciju crte, ovo bi trebalo raditi za više različitih pozicija u kutu od (270-0)
            if (livi_smjer)
                rect = new Rectangle((drugi.rect.X+sirina/4), (drugi.rect.Y-visina/4), sirina - prvi.rect.Width / 2, visina - prvi.rect.Width / 2);
            
            else
                rect = new Rectangle((prvi.rect.Center.X + prvi.rect.Width / 4), (prvi.rect.Center.Y + prvi.rect.Height / 4), sirina - prvi.rect.Width / 2, visina - prvi.rect.Width / 2);
        }


        public Crta(Texture2D t, Balon pocetni, Balon zavrsni,bool livo)
        {

            textrure = t;
            prvi = pocetni;
            drugi = zavrsni;
            livi_smjer = livo;

            int sirina=Math.Abs(pocetni.rect.X-zavrsni.rect.X);
            int visina =Math.Abs(pocetni.rect.Y-zavrsni.rect.Y);
            //bit je odrediti debljnu i poziciju crte, ovo bi trebalo raditi za više različitih pozicija u kutu od (270-0)
            if(livo)
             rect = new Rectangle((pocetni.rect.Center.X - sirina + pocetni.rect.Width / 4), (pocetni.rect.Center.Y + pocetni.rect.Width / 4), sirina - pocetni.rect.Width / 2, visina - pocetni.rect.Width / 2);
            else
                rect = new Rectangle((pocetni.rect.Center.X +pocetni.rect.Width/4), (pocetni.rect.Center.Y + pocetni.rect.Height / 4), sirina - pocetni.rect.Width / 2, visina - pocetni.rect.Width / 2);

        }
        public void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(textrure,rect,Color.White);
        }
        
    }
}
