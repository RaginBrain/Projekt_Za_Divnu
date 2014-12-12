using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projekt_Za_Dvnu
{
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
        SpriteFont font;
        CvorStabla c;
        string pisi;
        Vector2 tor;
        Vector2 tol;
        Vector2 tod;
        bool dira_li;

        public Balon(CvorStabla cvor,Rectangle r,Texture2D t,SpriteFont a)
        {
            font = a;
            rect = r;
            textrure = t;
            c = cvor;
            pisi = cvor.vrijednost.ToString();
            dira_li = false;
        }
        public Balon(CvorStabla cvor, Balon roditelj, Texture2D t, SpriteFont a,bool livi,int razmak_sirina,int razmak_visina)
        {
            font = a;
            if(livi)
                rect = new Rectangle(roditelj.rect.X-razmak_sirina,roditelj.rect.Y+razmak_visina,roditelj.rect.Width,roditelj.rect.Height);
            else
                rect = new Rectangle(roditelj.rect.X + razmak_sirina, roditelj.rect.Y + razmak_visina, roditelj.rect.Width, roditelj.rect.Height);
            textrure = t;
            c = cvor;
            pisi = cvor.vrijednost.ToString();
            dira_li = false;
        }

        public void Update(GameTime gameTime)
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textrure,rect,Color.White);
            spriteBatch.DrawString(font,pisi,new Vector2(rect.X+rect.Width/3,rect.Y+rect.Height/3),Color.Green);
        }


    }

    public class Crta : Sprite
    {
        public Balon prvi;
        public Balon drugi;

        bool livi_smjer;
        public void PostaviPravokutnik()
        {
            int sirina = Math.Abs(prvi.rect.X - drugi.rect.X);
            int visina = Math.Abs(prvi.rect.Y - drugi.rect.Y);
            //bit je odrediti debljnu i poziciju crte, ovo bi trebalo raditi za više različitih pozicija u kutu od (270-0)
            if (livi_smjer)
                rect = new Rectangle((prvi.rect.Center.X - sirina + prvi.rect.Width / 4), (prvi.rect.Center.Y + prvi.rect.Width / 4), sirina - prvi.rect.Width / 2, visina - prvi.rect.Width / 2);
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
