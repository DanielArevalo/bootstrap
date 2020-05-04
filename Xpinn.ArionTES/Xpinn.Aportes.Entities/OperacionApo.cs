using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class OperacionApo
    {
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 tipo_ope { get; set; }
        [DataMember]
        public Int64 cod_cliente { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string nom_tipo_ope { get; set; }
        [DataMember]
        public Int64 cod_usu { get; set; }
        [DataMember]
        public string iden_usuario { get; set; }
        [DataMember]
        public string nom_usuario { get; set; }
        [DataMember]
        public Int64 cod_ofi { get; set; }
        [DataMember]
        public string nom_ofi { get; set; }
        [DataMember]
        public Int64 cod_caja { get; set; }
        [DataMember]
        public Int64 cod_cajero { get; set; }
        [DataMember]
        public Int64 num_recibo { get; set; }
        [DataMember]
        public DateTime fecha_real { get; set; }
        [DataMember]
        public DateTime hora { get; set; }
        [DataMember]
        public DateTime fecha_oper { get; set; }
        [DataMember]
        public DateTime fecha_calc { get; set; }
        [DataMember]
        public Int64 num_comp { get; set; }
        [DataMember]
        public Int64 tipo_comp { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public string nomestado { get; set; }
        [DataMember]
        public Int64 num_lista { get; set; }
        [DataMember]
        public double valor { get; set; }
        [DataMember]
        public Boolean seleccionar { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public Int64? cod_proceso { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string error { get; set; }
    }

    [DataContract]
    [Serializable]
    public class AnulacionOperaciones
    {
        [DataMember]
        public Int64 COD_OPE { get; set; }
        [DataMember]
        public string TIPO_OPE { get; set; }
        [DataMember]
        public string FECHA_OPER { get; set; }
        [DataMember]
        public string FECHA_REAL { get; set; }
        [DataMember]
        public Int64 NUM_COMP { get; set; }
        [DataMember]
        public string TIPO_COMP { get; set; }
        [DataMember]
        public Int64 NUM_LISTA { get; set; }
        [DataMember]
        public string NOMESTADO { get; set; }
        [DataMember]
        public Int64 ESTADO { get; set; }
        [DataMember]
        public string IDEN_USUARIO { get; set; }
        [DataMember]
        public string NOM_USUARIO { get; set; }
        [DataMember]
        public Int64 COD_OFICINA { get; set; }
        [DataMember]
        public string NOM_OFICINA { get; set; }
        [DataMember]
        public string DESCRIPCION { get; set; }
        [DataMember]
        public Int64 COD_REFERENCIA { get; set; }
        [DataMember]
        public string TIPO_TRAN { get; set; }
        [DataMember]
        public string TIPO_MOV { get; set; }
        [DataMember]
        public string ATRIBUTO { get; set; }
        [DataMember]
        public double VALOR { get; set; }
        [DataMember]
        public string COD_OPE2 { get; set; }
        [DataMember]
        public string NUM_TRAN { get; set; }
        [DataMember]
        public string NUMERO_RADICACION { get; set; }
        [DataMember]
        public string COD_LINEA_CREDITO { get; set; }
        [DataMember]
        public string NOMBRE_LINEA { get; set; }
        [DataMember]
        public string CLIENTE { get; set; }
        [DataMember]
        public string MOTIVO_ANULA { get; set; }
        
    }
}


