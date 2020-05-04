using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Aportes.Entities
{
[DataContract]
[Serializable]
public class EscalafonSalarial
{
[DataMember]
public Int64 idescalafon { get; set; }
[DataMember]
public string grado { get; set; }
[DataMember]
public decimal? asignacion_mensual { get; set; }
    }
}