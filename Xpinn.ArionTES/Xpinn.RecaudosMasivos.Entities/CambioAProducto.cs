using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class CambioAProducto
    {
        //TABLA PERSONA EMPRESA RECAUDO
        [DataMember]
        public Int64 idempresarecaudo { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public int cod_empresa { get; set; }
        [DataMember]
        public string nom_empresa { get; set; }
        [DataMember]
        public decimal porcentaje { get; set; }
        [DataMember]
        public decimal valor { get; set; }

        [DataMember]
        public decimal? valor_cuota { get; set; }
        [DataMember]
        public decimal? valor_cuota_anterior { get; set; }
        [DataMember]
        public DateTime? fecha_empieza_cambio { get; set; }

        [DataMember]
        public Int64 numero_producto { get; set; }
        [DataMember]
        public int val_forma_pago { get; set; }
        [DataMember]
        public List<CreditoEmpresaRecaudo> lstDetalle { get; set; }
    }

}



