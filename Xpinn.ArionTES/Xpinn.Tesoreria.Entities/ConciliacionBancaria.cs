using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ConciliacionBancaria
    {
        //DATOS DE CUENTA BANCARIA

        //[DataMember]
        //public int idctabancaria { get; set; }
        [DataMember]
        public string num_cuenta { get; set; }        
        
        //DATOS DE PLAN CUENTAS
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nombre { get; set; }


        //DATOS DE CONCILIACION
        [DataMember]
        public int idconciliacion { get; set; }
        [DataMember]
        public int idctabancaria { get; set; }
        [DataMember]
        public int cod_banco { get; set; }
        [DataMember]
        public DateTime fecha_inicial { get; set; }
        [DataMember]
        public DateTime fecha_final { get; set; }
        [DataMember]
        public decimal saldo_contable { get; set; }
        [DataMember]
        public decimal saldo_extracto { get; set; }
        [DataMember]
        public int codusuario_elabora { get; set; }
        [DataMember]
        public int codusuario_aprueba { get; set; }
        [DataMember]
        public int estado { get; set; }

        //AGREGADO
        [DataMember]
        public string numcuenta { get; set; }
        [DataMember]
        public string nombrebanco { get; set; } 
        [DataMember]
        public string usuarioelabora { get; set; }
        [DataMember]
        public string nomtipocuenta { get; set; }
        [DataMember]
        public int tipo_cuenta { get; set; }

        [DataMember]
        public string nomestado { get; set; }
       
        //EXTRACTO
        [DataMember]
        public int idextracto { get; set; }

        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public int num_extracto { get; set; }

        [DataMember]
        public List<CONCBANCARIA_RESUMEN> lstResumen { get; set; }
        [DataMember]
        public List<CONCBANCARIA_DETALLE> lstDetalle { get; set; }
        
    }

    [DataContract]
    [Serializable]
    public class CONCBANCARIA_RESUMEN
    {
        [DataMember]
        public int idresumen { get; set; }
        [DataMember]
        public int idconciliacion { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public decimal valor { get; set; }
    }

    [DataContract]
    [Serializable]
    public class CONCBANCARIA_DETALLE
    {
        [DataMember]
        public int iddetalle { get; set; }
        [DataMember]
        public int idconciliacion { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public string referencia { get; set; }
        [DataMember]
        public string beneficiario { get; set; }
        [DataMember]
        public string concepto { get; set; }
        [DataMember]
        public int num_comp { get; set; }
        [DataMember]
        public int tipo_comp { get; set; }
        [DataMember]
        public int dias { get; set; }
        [DataMember]
        public decimal valor { get; set; }


        //AGREGADO
        [DataMember]
        public Int64 tipo { get; set; }
        [DataMember]
        public string nomtipo_comp { get; set; }

        [DataMember]
        public string observacion { get; set; }

    }
}