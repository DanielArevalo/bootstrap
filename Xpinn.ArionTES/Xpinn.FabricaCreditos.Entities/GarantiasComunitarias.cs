using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class GarantiasComunitarias
    {
        [DataMember]
        public String TIPOPRODUCTO { get; set; }
        [DataMember]
        public String PERIODO_GRACIA { get; set; }
        [DataMember]
        public Double CAPITAL { get; set; }
        [DataMember]
        public String PERIODO { get; set; }
        [DataMember]
        public String ASMODALIDAD { get; set; }
        [DataMember]
        public String TASA { get; set; }
        [DataMember]
        public String PORCENTAJE { get; set; }
        [DataMember]
        public String MODADLIDADCOMISION { get; set; }
        [DataMember]
        public String TIPOCOMISION { get; set; }
        [DataMember]
        public Double VALORCOMISION { get; set; }
        [DataMember]
        public Double VALORIVACOMISION { get; set; }
        [DataMember]
        public Double VALORTOTALCOMISION { get; set; }
        [DataMember]
        public String NOMCODEUDOR { get; set; }
        [DataMember]
        public String APELLIDOCODEUDOR { get; set; }
        [DataMember]
        public String IDENTIFICACIONCODEUDOR { get; set; }
        [DataMember]
        public String SUCURSAL { get; set; }
        [DataMember]
        public String TELEFONOCODEUDOR { get; set; }
        [DataMember]
        public String DIRECCIONCODEUDOR { get; set; }
        [DataMember]
        public String CIUCODEUDOR { get; set; }
        [DataMember]
        public String NUMERO_RADICACION { get; set; }
        [DataMember]
        public String CONVENIO { get; set; }
        [DataMember]
        public String COD_LINEA_CREDITO { get; set; }
        [DataMember]
        public String ENTIDAD { get; set; }
        [DataMember]
        public String NITENTIDAD { get; set; }
        [DataMember]
        public String OFICINA { get; set; }
        [DataMember]
        public String DESEMBOLSOFECHA { get; set; }
        [DataMember]
        public String NOMBRES { get; set; }
        [DataMember]
        public String APELLIDOS { get; set; }
        [DataMember]
        public String IDENTIFICACION { get; set; }
        [DataMember]
        public String SUCURSALOFICINA { get; set; }
        [DataMember]
        public String TELEFONO { get; set; }
        [DataMember]
        public String NOMCIUDAD { get; set; }
        [DataMember]
        public String BARRIO { get; set; }
        [DataMember]
        public String ACTIVIDAD { get; set; }
        [DataMember]
        public String PAGARE { get; set; }
        [DataMember]
        public String PAGARE2 { get; set; }
        [DataMember]
        public Double MONTO_APROBADO { get; set; }
        [DataMember]
        public String DESCRIPCION { get; set; }
        [DataMember]
        public String VALOR_CUOTA { get; set; }
        [DataMember]
        public String NUMERO_CUOTAS { get; set; }
        [DataMember]
        public String FECHA_DESEMBOLSO { get; set; }
        [DataMember]
        public String FECHA_VENCIMIENTO { get; set; }
        [DataMember]
        public String FECHAPROXPAGO { get; set; }
        [DataMember]
        public DateTime FECHARECLAMACION { get; set; }
        [DataMember]
        public String DIASMORA { get; set; }
        [DataMember]
        public String RECLAMACION { get; set; }
        [DataMember]
        public Double INT_CORRIENTES { get; set; }
        [DataMember]
        public Double INT_MORA { get; set; }
        [DataMember]
        public String CUOTAS_RECLAMAR { get; set; }
        [DataMember]
        public Double VALOR_PAGADO { get; set; }
        [DataMember]
        public Int32 COD_IDENT { get; set; }
        [DataMember]
        public Double SOBRANTE { get; set; }

        [DataMember]
        public Int64 numero_reclamacion { get; set; }
        [DataMember]
        public Stream stream { get; set; }
    }
}