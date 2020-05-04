using System;
using System.Collections.Generic;

namespace Xpinn.Sincronizacion.Entities
{
    public class SyncUsuario
    {
        public Int64 codusuario { get; set; }
        public string identificacion { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string login { get; set; }
        public string clave { get; set; }
        public DateTime fechacreacion { get; set; }
        public Int64 estado { get; set; }
        public Int64 codperfil { get; set; }
        public Int64 cod_oficina { get; set; }
        public List<string> lstIpAccesos { get; set; }
        public List<int> lstAtribuciones { get; set; }
    }
}
