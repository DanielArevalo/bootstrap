using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class InformacionAdicional
    {
        #region PERSONA_INFADICIONAL

        [DataMember]
        public int idinfadicional { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public int cod_infadicional { get; set; }
        [DataMember]
        public string valor { get; set; }
        [DataMember]
        public string identificacion { get; set; }

        #endregion

        [DataMember]
        public List<InformacionAdicional> lstInfor { get; set; }

        #region TIPO_INFADICIONAL

        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int? tipo { get; set; }
        [DataMember]
        public string items_lista { get; set; }
        [DataMember]
        public string tipo_persona { get; set; }

        #endregion

      
    }
}