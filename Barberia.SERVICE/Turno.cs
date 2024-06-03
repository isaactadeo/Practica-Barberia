using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia.SERVICE
{
    public class Turno
    {
        public int NumeroTurno { get; set; }
        public int DniCliente { get; set; }
        public DateTime Fecha { get; set; }  
        public string TipoTrabajo { get; set; }    
        public int Importe { get; set; }    
             
        }
}

