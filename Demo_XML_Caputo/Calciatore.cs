using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_XML_Caputo
{
    public class Calciatore
    {
        public string Nome { get; set; }

        public string Cognome { get; set; }

        public string Squadra { get; set; }

        public int NumeroMaglia { get; set; }

        public override string ToString()
        {
            return $"{Nome}";
        }
    }
}
