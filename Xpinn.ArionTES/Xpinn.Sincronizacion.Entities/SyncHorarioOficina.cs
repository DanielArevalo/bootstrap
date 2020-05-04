using System;

namespace Xpinn.Sincronizacion.Entities
{
    public class SyncHorarioOficina
    {
        public int cod_horario { get; set; }
        public int? cod_oficina { get; set; }
        public int tipo_horario { get; set; }
        public int dia { get; set; }
        public DateTime hora_inicial { get; set; }
        public DateTime hora_final { get; set; }
    }
}
