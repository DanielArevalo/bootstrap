﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class LibroMayor
    {
        #region ParametrosEntrada
            [DataMember]
            public DateTime fecha_corte { get; set; }
            [DataMember]
            public Int64 nivel { get; set; }
            [DataMember]
            public Int64 cenini { get; set; }
            [DataMember]
            public Int64 cenfin { get; set; }
            [DataMember]
            public Int16 excedentes { get; set; }
            [DataMember]
            public Int16 mostrarceros { get; set; }
            [DataMember]
            public Int16 solonivel { get; set; }
            [DataMember]
            public Int16 generarterceros { get; set; }
            [DataMember]
            public Int16? mostrarmovper13 { get; set; }
        #endregion

        #region ParametrosSalida
            [DataMember]
            public DateTime? fecha { get; set; }
            [DataMember]
            public string cod_cuenta { get; set; }
            [DataMember]
            public string nombre_cuenta { get; set; }
            [DataMember]
            public string naturaleza { get; set; }
            [DataMember]
            public Int64? cod_tercero { get; set; }
            [DataMember]
            public string iden_tercero { get; set; }
            [DataMember]
            public string nom_tercero { get; set; }
            [DataMember]
            public Double? saldo_inicial_debito { get; set; }
            [DataMember]
            public Double? saldo_inicial_credito { get; set; }
            [DataMember]
            public Double? debito { get; set; }
            [DataMember]
            public Double? credito { get; set; }
            [DataMember]
            public Double? saldo_final_debito { get; set; }
            [DataMember]
            public Double? saldo_final_credito { get; set; }
            [DataMember]
            public Int64 cod_moneda { get; set; }
        #endregion

    }
}