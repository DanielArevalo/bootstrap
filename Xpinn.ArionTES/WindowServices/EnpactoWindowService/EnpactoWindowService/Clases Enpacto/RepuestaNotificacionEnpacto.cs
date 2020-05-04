using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnpactoWindowService.Clases_Enpacto
{
    public class RepuestaNotificacionEnpacto
    {
        public List<TransaccionesRespuesta> trans { get; set; }
    }

    public class RepuestaNotificacionCoopcentral
    {
        public string estado { get; set; }
        public string mensaje { get; set; }
    }

}
