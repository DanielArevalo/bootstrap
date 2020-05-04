using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Integracion.Entities
{
    [DataContract]
    [Serializable]
    public class Monedero
    {
        [DataMember]
        public int id_monedero { get; set; }
        [DataMember]
        public int cod_persona { get; set; }

        [DataMember]
        public decimal saldo { get; set; }

        [DataMember]
        public int estado { get; set; }

        [DataMember]
        public DateTime fecha_apertura { get; set; }        
    }

    public class PersonaMonedero
    {
        [DataMember]
        public long cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public int id_monedero { get; set; }
        [DataMember]
        public int estado { get; set; }
    }

    public class Operaciones
    {
        [DataMember]
        public int id_operacion { get; set; }
        [DataMember]
        public string nombre { get; set; }

        [DataMember]
        public int estado { get; set; }        
    }

    public class ProductoOrigen
    {
        [DataMember]
        public int tipo_producto { get; set; }
        [DataMember]
        public string referencia { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
    }

    public class TranMonedero
    {
        [DataMember]
        public int? num_tran { get; set; }
        public long cod_persona { get; set; }
        public int id_monedero { get; set; }
        public int tipo_tran { get; set; }
        public decimal valor { get; set; }
        public int estado { get; set; }
        public DateTime fecha { get; set; }
        public string  descripcion { get; set; }
        public int id_operacion { get; set; }
        public int tipo_credito { get; set; }
        public int cod_tipo_producto { get; set; }
        public string referencia { get; set; }
    }
}
