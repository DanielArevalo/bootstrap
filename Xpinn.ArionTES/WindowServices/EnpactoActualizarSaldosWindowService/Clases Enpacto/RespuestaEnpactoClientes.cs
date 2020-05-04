using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnpactoActualizarSaldosWindowService.Clases_Enpacto
{
    public class RespuestaEnpactoClientes
    {
        public bool estado { get; set; }
        public string mensaje { get; set; }
        public List<RelacionClienteEnpacto> relaciones { get; set; }

    }
}
