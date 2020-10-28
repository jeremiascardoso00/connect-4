using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class Paquete
    {
        public string posicion { get; set; }
        

        public Paquete()
        {

        }
        public Paquete(string data)
        {
            posicion = data;
        }

       
        

        public static implicit operator string(Paquete paquete)
        {
            return paquete.posicion;
        }
    }
}