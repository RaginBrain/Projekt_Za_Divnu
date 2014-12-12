using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Projekt_Za_Dvnu
{
    public class CvorStabla
    {
        public int vrijednost;
        public CvorStabla lijevi;
        public CvorStabla desni;
        public Balon balon;

        public CvorStabla(int i)
        {
            lijevi = null;
            desni = null;
            vrijednost = i;
        }


    }
}
