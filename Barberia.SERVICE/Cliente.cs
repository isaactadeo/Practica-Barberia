using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia.SERVICE
{
    public class Cliente
    {
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Localidad { get; set;}
        public string Domicilio { get; set; }  
        public int NroDomicilio { get; set; }   
        public string Sexo { get; set; }    
    }
}
