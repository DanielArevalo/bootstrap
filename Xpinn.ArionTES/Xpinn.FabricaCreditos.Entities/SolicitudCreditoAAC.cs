using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class SolicitudCreditoAAC
    {
        [DataMember]
        public Int64 numerosolicitud { get; set; }
        [DataMember]
        public DateTime fechasolicitud { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public decimal? montosolicitado { get; set; }
        [DataMember]
        public int? plazosolicitado { get; set; }
        [DataMember]
        public decimal? cuotasolicitada { get; set; }
        [DataMember]
        public int? tipocredito { get; set; }
        [DataMember]
        public int? periodicidad { get; set; }
        [DataMember]
        public int? medio { get; set; }
        [DataMember]
        public string reqpoliza { get; set; }
        [DataMember]
        public string otromedio { get; set; }
        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public string concepto { get; set; }
        [DataMember]
        public int? garantia { get; set; }
        [DataMember]
        public int? garantia_comunitaria { get; set; }
        [DataMember]
        public int? tipo_liquidacion { get; set; }
        [DataMember]
        public int? forma_pago { get; set; }
        [DataMember]
        public int? empresa_recaudo { get; set; }
        [DataMember]
        public string idproveedor { get; set; }
        [DataMember]
        public string nomproveedor { get; set; }
        [DataMember]
        public int? destino { get; set; }
        [DataMember]
        public Int64? id_persona { get; set; }
        [DataMember]
        public int? tipo_cuenta { get; set; }
        [DataMember]
        public string num_cuenta { get; set; }
        [DataMember]
        public int? cod_banco { get; set; }
        [DataMember]
        public string tipovivienda { get; set; }
        [DataMember]
        public string arrendatario { get; set; }
        [DataMember]
        public string telef_arrendatario { get; set; }
        [DataMember]
        public string tipo_propiedad { get; set; }
        [DataMember]
        public string otro_propiedad { get; set; }
        [DataMember]
        public string direc_propiedad { get; set; }
        [DataMember]
        public Int64? codciudad_propiedad { get; set; }
        [DataMember]
        public string escritura_propiedad { get; set; }
        [DataMember]
        public string notaria { get; set; }
        [DataMember]
        public int? maneja_hipoteca { get; set; }
        [DataMember]
        public decimal? valor_hipoteca { get; set; }
        [DataMember]
        public string matricula_inmov { get; set; }
        [DataMember]
        public decimal? vr_propiedad_viv { get; set; }
        [DataMember]
        public decimal? vr_arriendo_cuota { get; set; }
        [DataMember]
        public decimal? vr_gastos { get; set; }
        [DataMember]
        public decimal? vr_otrosgastos { get; set; }
        [DataMember]
        public string marca_modelo { get; set; }
        [DataMember]
        public decimal? vr_comercial_vehi { get; set; }
        [DataMember]
        public int? pignorado_vehi { get; set; }
        [DataMember]
        public decimal? vr_pignorado_vehi { get; set; }
        [DataMember]
        public DateTime? fecha_vencimiento { get; set; }
        [DataMember]
        public decimal? saldo { get; set; }
        [DataMember]
        public decimal? vr_cuota { get; set; }
        [DataMember]
        public Int64? empresa_recaudo_seg { get; set; }
        [DataMember]
        public DateTime? fecha_vencimiento_seg { get; set; }
        [DataMember]
        public decimal? saldo_seg { get; set; }
        [DataMember]
        public decimal? vr_cuota_seg { get; set; }
        [DataMember]
        public int estado { get; set; }

        //ADICIONADO
        [DataMember]
        public string nom_linea { get; set; }
        [DataMember]
        public string nom_periodicidad { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public string nom_persona { get; set; }
        //ADICIONADO PARA AFIANCOL 10/05/2019
        [DataMember]
        public int afiancol { get; set; }

    }
}
