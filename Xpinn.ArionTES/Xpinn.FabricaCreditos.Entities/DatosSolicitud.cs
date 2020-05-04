using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities

{
    [DataContract]
    [Serializable]
    public class DatosSolicitud
    {
        [DataMember]
        public Int64 numeroradicado { get; set; }
        [DataMember]
        public Int64 numerosolicitud { get; set; }
        [DataMember]
        public DateTime fechasolicitud { get; set; }

        [DataMember]
        public DateTime fecha_primer_pago { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }        
        [DataMember]
        public Int64 montosolicitado { get; set; }
        [DataMember]
        public Int64 plazosolicitado { get; set; }
        [DataMember]
        public Int64 cuotasolicitada { get; set; }
        [DataMember]
        public string tipocrdito { get; set; }
        [DataMember]
        public Int64 periodicidad { get; set; }
        [DataMember]
        public Int64 medio { get; set; }
        [DataMember]
        public String otro { get; set; }
        [DataMember]
        public String concepto { get; set; }
        [DataMember]
        public String nombre { get; set; }
        [DataMember]
        public Int64 numerocuotas { get; set; }
        [DataMember]
        public Int64 valorcuota { get; set; }
        [DataMember]
        public Int64 cod_asesor_com { get; set; }
        [DataMember]
        public String asesor { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public Int64 cod_usuario { get; set; }
        [DataMember]
        public Int64 garantia { get; set; }
        [DataMember]
        public Int64 poliza { get; set; }
        [DataMember]
        public Int64 garantia_comunitaria { get; set; }
        [DataMember]
        public Int64 forma_pago { get; set; }
        [DataMember]
        public Int64? empresa_recaudo { get; set; }
        [DataMember]
        public Int64 tipo_liquidacion { get; set; }
        [DataMember]
        public String mensaje { get; set; }
        [DataMember]
        public String cod_cliente { get; set; }
        // Auditoria:
        [DataMember]
        public string UsuarioCrea { get; set; }
        [DataMember]
        public DateTime FechaCrea { get; set; }
        [DataMember]
        public string UsuarioEdita { get; set; }
        [DataMember]
        public DateTime FechaEdita { get; set; }
        // Parametro edad
        [DataMember]
        public Int64 edadminima { get; set; }
        [DataMember]
        public Int64 edadmaxima { get; set; }

        // Lineas de credito
        [DataMember]
        public Int64 cod_linea { get; set; }
        [DataMember]
        public String linea { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public Int64 cod_clasifica { get; set; }
        // Proveedor
        [DataMember]
        public string identificacionprov { get; set; }
        [DataMember]
        public string  nombreprov { get; set; }
        [DataMember]
        public Int64? num_preimpreso { get; set; }
        [DataMember]
        public string nomoficina { get; set; }

        // destinación
        [DataMember]
        public Int64 destino { get; set; }
        [DataMember]
        public int cod_banco { get; set; }
        [DataMember]
        public string numero_cuenta { get; set; }
        [DataMember]
        public string tipo_cuenta { get; set; }
        [DataMember]
        public string forma_desembolso { get; set; }
        [DataMember]
        public string pEntidad { get; set; }
        [DataMember]
        public string tipoDePersona { get; set; }
        [DataMember]
        public bool esAsociado { get; set; }
        [DataMember]
        public string nombre_ciudad { get; set; }
        [DataMember]
        public bool esPersonaNatural { get; set; }
        [DataMember]
        public decimal Valor_fondo_garantia { get; set; }

        [DataMember]
        public bool Afiancol { get; set; }
    } 
        
    
}
