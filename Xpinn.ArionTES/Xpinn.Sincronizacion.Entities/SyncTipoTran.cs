namespace Xpinn.Sincronizacion.Entities
{
    public class SyncTipoTran
    {
        public int tipo_tran { get; set; }
        public string descripcion { get; set; }
        public int? cod_tipo_producto { get; set; }
        public int? tipo_mov { get; set; }
        public int? tipo_caja { get; set; }
        public decimal? porc_imp { get; set; }
    }
}
