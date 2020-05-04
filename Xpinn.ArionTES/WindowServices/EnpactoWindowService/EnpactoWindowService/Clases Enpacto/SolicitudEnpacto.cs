using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xpinn.TarjetaDebito.Entities;

namespace EnpactoWindowService.Clases_Enpacto
{
    public class SolicitudEnpacto
    {
        public List<Movimiento> trans { get; set; }
    }

    public class SolicitudCoopcentral
    {
        public string estado { get; set; }
        public string mensaje { get; set; }
        public List<tran> trans { get; set; }
        public class tran : MovimientoCoopcentralEnLinea { }
    }

}
