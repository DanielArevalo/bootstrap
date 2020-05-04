namespace Xpinn.Sincronizacion.Entities
{
    public class SyncTopesCaja
    {
        public int cod_caja { get; set; }
        public int cod_moneda { get; set; }
        public int tipo_tope { get; set; }
        public decimal valor_minimo { get; set; }
        public decimal? valor_maximo { get; set; }
    }
}
