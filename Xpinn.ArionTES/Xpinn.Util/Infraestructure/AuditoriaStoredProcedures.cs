using System;
using System.Runtime.Serialization;

namespace Xpinn.Util
{
    [DataContract]
    [Serializable]
    public class AuditoriaStoredProcedures
    {
        public long consecutivo { get; set; }
        public string nombresp { get; set; }
        public DateTime fechaejecucion { get; set; }
        public int codigousuario { get; set; }
        public string nombreusuario { get; set; }
        public bool exitoso { get; set; }
        public string informacionenviada { get; set; }
        public long? codigoOpcion { get; set; }
        public string nombre_opcion { get; set; }
        public string mensaje_error { get; set; }
    }
}
