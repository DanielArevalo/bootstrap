using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class PlanTelefonico
    {
        [DataMember]
        public int cod_plan { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal? valor { get; set; }

        //AGREGADO PARA EL MANEJO DE LOS PROVEEDORES DE LAS PLANES PARA LAS LINEAS TELEFONICAS
        [DataMember]
        public Int64? cod_proveedor { get; set; }
        [DataMember]
        public string identificacion_proveedor { get; set; }
        [DataMember]
        public string nombre_proveedor { get; set; }

        //Titular
        [DataMember]
        public string identificacion_titular { get; set; }
        [DataMember]
        public string nombre_titular { get; set; }
        [DataMember]
        public Int64? cod_titular { get; set; }

        //Linea Telefonica
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public string num_linea_telefonica { get; set; }
        [DataMember]
        public Int64? cod_serv_fijo { get; set; }
        [DataMember]
        public Int64? cod_serv_adicional { get; set; }
        [DataMember]
        public DateTime fecha_activacion { get; set; }
        [DataMember]
        public DateTime? fecha_ult_reposicion { get; set; }
        [DataMember]
        public DateTime? fecha_vencimiento { get; set; }
        [DataMember]
        public DateTime? fecha_incativacion { get; set; }
        [DataMember]
        public string estado { get; set; }
        //Complemento servicios
        [DataMember]
        public Int64? cod_linea_servicio { get; set; }
        [DataMember]
        public DateTime fecha_primera_cuota { get; set; }
        [DataMember]
        public decimal valor_cuota { get; set; }
        [DataMember]
        public Int64? cod_periodicidad { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public Int64? cod_empresa { get; set; }
        [DataMember]
        public decimal? valor_compra { get; set; }
        [DataMember]
        public decimal? beneficio { get; set; }
        [DataMember]
        public decimal? valor_mercado { get; set; }
        //Complementos
        [DataMember]
        public string nom_linea { get; set; }
        [DataMember]
        public string nom_plan { get; set; }
        [DataMember]
        public string num_linea_telefonicaR { get; set; }
        //Para las transferencias solidarias 
        [DataMember]
        public decimal? valor_fijo { get; set; }
        [DataMember]
        public decimal? valor_adicional{ get; set; }
        [DataMember]
        public decimal? valor_total { get; set; }
        //Para Traspaso
        [DataMember]
        public string identificacion_nuevo_titular { get; set; }
        [DataMember]
        public string nombre_nuevo_titular { get; set; }
        [DataMember]
        public Int64? cod_nuevo_titular { get; set; }
        [DataMember]
        public string forma_pago_nv_titular { get; set; }
        [DataMember]
        public Int64? cod_empresa_nv_titular { get; set; }
        //Para Reposicion
        [DataMember]
        public decimal? valor_reposicion { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        //Para Cancelacion
        [DataMember]
        public DateTime fecha_cancelacion { get; set; }
        [DataMember]
        public decimal? Saldo_ser_fijo { get; set; }
        [DataMember]
        public decimal? saldo_ser_adicional { get; set; }
        [DataMember]
        public Int64? cod_cacelacion { get; set; }        

    }
}
