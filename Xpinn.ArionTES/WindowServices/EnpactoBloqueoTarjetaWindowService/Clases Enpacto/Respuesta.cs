using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnpactoBloqueoTarjetaWindowService.Clases_Enpacto
{
    class Respuesta
    {
        public string datos { get; set; }
        public string datos_encriptados { get; set; }
        public string respuesta_encriptada { get; set; }
        public string drespuesta { get; set; }
        public string secuencia { get; set; }
        public string saldo_disponible { get; set; }
        public string saldo_total { get; set; }
        public string estado { get; set; }
        public string mensaje { get; set; }
    }
}
