using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{
    [DataContract]
    [Serializable]
    public class ReporteProducto
    {
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public DateTime? fecha_expedicion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string genero { get; set; }
        [DataMember]
        public string cod_oficina { get; set; }
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public DateTime? fecha_nacimiento { get; set; }
        [DataMember]
        public DateTime? fecha_afiliacion { get; set; }
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string estado{ get; set; }
        [DataMember]
        public List<producto> lstmoviminento { get; set; }
        [DataMember]
        public List<producto> lstsaldo { get; set; }
        [DataMember]
        public string Asesor { get; set; }
        //Saldos
        [DataMember]
        public Int64 saldo_aportes { get; set; }
        [DataMember]
        public Int64 saldo_creditos { get; set; }
        [DataMember]
        public Int64 saldo_servicios { get; set; }
        [DataMember]
        public Int64 saldo_cdat { get; set; }
        [DataMember]
        public Int64 saldo_ahorroV { get; set; }
        [DataMember]
        public Int64 saldo_ahorroP { get; set; }
    }
    [DataContract]
    [Serializable]
    public class producto
    {
        [DataMember]
        public string tipoproducto { get; set; }
        [DataMember]
        public decimal monto { get; set; }
        [DataMember]
        public int cantidad { get; set; }
        [DataMember]
        public Int64 cuota { get; set; }
    }
}