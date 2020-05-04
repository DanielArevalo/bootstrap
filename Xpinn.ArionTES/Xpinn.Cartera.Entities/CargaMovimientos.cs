using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpinn.Cartera.Entities
{
    public class CargaMovimientos
    {
        public int TipoProducto { get; set; }
        public long NumeroProducto { get; set; }
        public long IdentificacionPer { get; set; }
        public int Valor { get; set; }
        public string TipoMovimiento { get; set; }
        public int CodPersona { get; set; }
        public int CodOperacion { get; set; }
        public int TipoNota { get; set; }
        public int CodUsua { get; set; }
        public string Descripcion { get; set; }
    }

    public class TipoProducto
    {
        public int CodProducto { get; set; }
        public string Descripcion { get; set; }
    }
}

