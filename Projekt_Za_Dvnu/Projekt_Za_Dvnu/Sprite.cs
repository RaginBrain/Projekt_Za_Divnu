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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textrure,rect,Color.White);
            spriteBatch.DrawString(font,pisi,new Vector2(rect.X+rect.Width/3,rect.Y+rect.Height/3),Color.Green);
        }


    }
}
