using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Cartera.Entities
{
    [DataContract]
    [Serializable]
    public class DebitoAutomatico
    {

        [DataMember]
        public Int64 consecutivo { get; set; }

        [DataMember]
        public Int64 cod_producto { get; set; }

        [DataMember]
        public Int64 cod_tipo_producto { get; set; }
        [DataMember]
        public Int64 cod_linea { get; set; }
        [DataMember]
        public String cod_linea_ahorro { get; set; }
        [DataMember]
        public String descripcion { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }

        [DataMember]
        public String nom_oficina { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }


        [DataMember]
        public Decimal cuota { get; set; }
        [DataMember]
        public Decimal saldo { get; set; }

        [DataMember]
        public Int64 numero_producto { get; set; }


        [DataMember]
        public string numero_cuenta_ahorro { get; set; }


        [DataMember]
        public Int64 numero_cuenta_ahorrochek { get; set; }

        [DataMember]
        public string nombre_linea { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string Apellidos { get; set; }
        [DataMember]
        public string tipoproducto { get; set; }

        [DataMember]
        public string tipoidentificacion { get; set; }


        [DataMember]
        public List<DebitoAutomatico> lstLista { get; set; }
      
    }
}