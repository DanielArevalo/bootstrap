using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad ModalidadTasa
    /// </summary>
[DataContract]
[Serializable]
public class ModalidadTasa
{
[DataMember]
public string cod_linea_credito { get; set; }
[DataMember]
public string descripcion { get; set; }
[DataMember]
public int valor { get; set; }
[DataMember]
public decimal cod_modalidad { get; set; }
[DataMember]
public Boolean Seleccionar { get; set; }
    }
}

