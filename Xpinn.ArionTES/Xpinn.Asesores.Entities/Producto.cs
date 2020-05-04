using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Asesores.Entities.Common;
 
namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class Producto
    {
        public Producto()
        {
            Persona = new Persona();
            Oficina = new Oficina();
            Ejecutivo = new Ejecutivo();
            Codeudores = new List<Codeudor>();
            Comentarios = new List<Comentario>();
            Empresa = new Empresa();
        }

        [DataMember]
        public string CodRadicacion { get; set; }
        [DataMember]
        public Persona Persona { get; set; }
        [DataMember]
        public string Pagare { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Proceso { get; set; }
        [DataMember]
        public string Linea { get; set; }
        [DataMember]
        public string CodLineaCredito { get; set; }
        [DataMember]
        public Int64 Atributos { get; set; }
        [DataMember]
        public DateTime FechaSolicitud { get; set; }
        [DataMember]
        public DateTime FechaDesembolso { get; set; }
        [DataMember]
        public Int64 MontoAprobado { get; set; }
        [DataMember]
        public Int64 SaldoCapital { get; set; }
        [DataMember]
        public Int64 garantiacomunitaria { get; set; }
        [DataMember]
        public Int64 Plazo { get; set; }
        [DataMember]
        public Int64 OtrosSaldos { get; set; }
        [DataMember]
        public Int64 Cuota { get; set; }
        [DataMember]
        public Int64 ValorCuota { get; set; }
        [DataMember]
        public Int64 NumeroCuotas { get; set; }
        [DataMember]
        public Decimal CuotasPagadas { get; set; }
        [DataMember]
        public Decimal CuotasPendientes { get; set; }
        [DataMember]
        public Int64 Garantia { get; set; }
        [DataMember]
        public Int64 Codeudor { get; set; }
        [DataMember]
        public DateTime FechaProximoPago { get; set; }
        [DataMember]
        public DateTime FechaFiltro1 { get; set; }
        [DataMember]
        public DateTime FechaFiltro2 { get; set; }
        [DataMember]
        public Int64 ValorAPagar { get; set; }

        [DataMember]
        public Decimal valorapagarCE { get; set; }

        [DataMember]
        public Int64 ValorTotalAPagar { get; set; }
        [DataMember]
        public Decimal ValorTotalAPagarCE { get; set; }
        [DataMember]
        public Oficina Oficina { get; set; }
        [DataMember]
        public Int64 CalifPromedio { get; set; }
        [DataMember]
        public Int64 TipoLiquidacion { get; set; }
        [DataMember]
        public Int64 PeriodoGracia { get; set; }
        [DataMember]
        public Int64 CodClasifica { get; set; }
        [DataMember]
        public Ejecutivo Ejecutivo { get; set; }
        [DataMember]
        public string TipoCredito { get; set; }
        [DataMember]
        public Int64 TipoLinea { get; set; }
        [DataMember]
        public Decimal VrrEstructurado { get; set; }
        [DataMember]
        public Empresa Empresa { get; set; }
        [DataMember]
        public Int64 CodPagaduria { get; set; }
        [DataMember]
        public Decimal Gradiente { get; set; }
        [DataMember]
        public Int64 NumRadicOrigen { get; set; }
        [DataMember]
        public Int64 NumRadicion { get; set; }
        [DataMember]
        public DateTime FechaPago { get; set; }
        [DataMember]
        public List<Codeudor> Codeudores { get; set; }
        [DataMember]
        public List<Comentario> Comentarios { get; set; }
        [DataMember]
        public Int64 numerocreditos { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public decimal Tasainteres { get; set; }
        [DataMember]
        public long IdPersona { get; set; }
        [DataMember]
        public decimal vr_ult_dscto { get; set; }
        [DataMember]
        public string pagadurias { get; set; }
        [DataMember]
        public DateTime? FechaVencimiento { get; set; }
        [DataMember]
        public Int64 Disponible { get; set; }
        [DataMember]
        public int noconsultaTodo { get; set; }
        [DataMember]
        public string FechaReestructurado { get; set; }
    }

    [DataContract]
    [Serializable]
    public class ProductoResumen
    {
        [DataMember]
        public string Identificacion { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public int tipo_producto { get; set; }
        [DataMember]
        public string numero_producto { get; set; }
        [DataMember]
        public DateTime? fechaapertura { get; set; }
        [DataMember]
        public string cod_linea { get; set; }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public Int64 valorcuota { get; set; }             
        [DataMember]
        public Int64 monto { get; set; }                 
        [DataMember]
        public Int64 saldo { get; set; }        
        [DataMember]
        public DateTime fechaproximopago { get; set; }
        [DataMember]
        public Int64 valorapagar { get; set; }

       
        [DataMember]
        public Int64 valortotalapagar { get; set; }
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public Int64 cod_empresa { get; set; }
        [DataMember]
        public Int64 Plazo { get; set; }
        [DataMember]
        public Int64 CuotasPagadas { get; set; }
        [DataMember]
        public string tipo_registro { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal Tasainteres { get; set; }
        [DataMember]
        public string NombreOficina { get; set; }
        [DataMember]
        public string pagadurias { get; set; }
        [DataMember]
        public DateTime? fecha_vencimiento { get; set; }
        [DataMember]
        public string nom_forma_pago { get; set; }
        [DataMember]
        public string cod_periodicidad { get; set; }
        [DataMember]
        public string nom_periodicidad { get; set; }
        [DataMember]
        public decimal CupoDisponible { get; set; }
        [DataMember]
        public int TipoLinea { get; set; }
    }


    [DataContract]
    [Serializable]
    public class ProductoPersonaAPP
    {
        [DataMember]
        public int cod_tipo_producto { get; set; }
        [DataMember]
        public string tipo_producto { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public Int64 numero_producto { get; set; }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public string nombre_linea { get; set; }
        [DataMember]
        public DateTime fecha_apertura { get; set; }
        [DataMember]
        public string cuota { get; set; }
        [DataMember]
        public string saldo { get; set; }
        [DataMember]
        public DateTime fecha_proximo_pago { get; set; }
        [DataMember]
        public string total_a_pagar { get; set; }
        [DataMember]
        public string fecha_aperturaAPP { get; set; }
        [DataMember]
        public string fecha_proximo_pagoAPP { get; set; }
    }


    [DataContract]
    [Serializable]
    public class MovimientoResumen
    {
        [DataMember]
        public string Identificacion { get; set; }
        [DataMember]
        public Int64 cod_deudor { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public DateTime fecha_oper { get; set; }
        [DataMember]
        public Int64 num_comp { get; set; }
        [DataMember]
        public string tipo_comp { get; set; }
        [DataMember]
        public decimal valor { get; set; }
    }

}
