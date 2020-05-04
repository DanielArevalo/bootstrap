using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnpactoWindowService.Clases_Enpacto
{
    public class TransaccionesRespuesta
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Secuencia { get; set; }
        public string Tarjeta { get; set; }
        public string Estado { get; set; }
    }
}
