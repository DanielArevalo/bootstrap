using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Obligaciones.Data;
using Xpinn.Obligaciones.Entities;
using Xpinn.Comun.Business;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Obligaciones.Business
{
    /// <summary>
    /// Objeto de negocio para ObligacionCredito
    /// </summary>
    public class ObligacionCreditoBusiness : GlobalBusiness
    {
        private ObligacionCreditoData DAObligacionCredito;
        private FechasBusiness BOFechas;

        /// <summary>
        /// Constructor del objeto de negocio para ObligacionCredito
        /// </summary>
        public ObligacionCreditoBusiness()
        {
            DAObligacionCredito = new ObligacionCreditoData();
            BOFechas = new FechasBusiness();
        }

        /// <summary>
        /// Crea un ObligacionCredito
        /// </summary>
        /// <param name="pObligacionCredito">Entidad ObligacionCredito</param>
        /// <returns>Entidad ObligacionCredito creada</returns>
        public ObligacionCredito CrearObligacionCredito(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pObligacionCredito = DAObligacionCredito.CrearObligacionCredito(pObligacionCredito, pUsuario);

                    ts.Complete();
                }

                return pObligacionCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "CrearObligacionCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ObligacionCredito
        /// </summary>
        /// <param name="pObligacionCredito">Entidad ObligacionCredito</param>
        /// <returns>Entidad ObligacionCredito modificada</returns>
        public ObligacionCredito ModificarObligacionCredito(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pObligacionCredito = DAObligacionCredito.ModificarObligacionCredito(pObligacionCredito, pUsuario);

                    ts.Complete();
                }

                return pObligacionCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ModificarObligacionCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ObligacionCredito
        /// </summary>
        /// <param name="pId">Identificador de ObligacionCredito</param>
        public void EliminarObligacionCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAObligacionCredito.EliminarObligacionCredito(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "EliminarObligacionCredito", ex);
            }
        }

        /// <summary>
        /// Obtiene un ObligacionCredito
        /// </summary>
        /// <param name="pId">Identificador de ObligacionCredito</param>
        /// <returns>Entidad ObligacionCredito</returns>
        public ObligacionCredito ConsultarObligacionCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAObligacionCredito.ConsultarObligacionCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ConsultarObligacionCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pObligacionCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito obtenidos</returns>
        public List<ObligacionCredito> ListarObligacionCredito(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return DAObligacionCredito.ListarObligacionCredito(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ListarObligacionCredito", ex);
                return null;
            }
        }

        public List<ObligacionCredito> ListarObligaciones(String filtro, Usuario pUsuario)
        {
            try
            {
                return DAObligacionCredito.ListarObligaciones(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ListarObligaciones", ex);
                return null;
            }
        }

        public List<ObligacionCredito> ListarProvicionCredito(ObligacionCredito pObligacionCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAObligacionCredito.ListarProvicionCredito(pObligacionCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ListarObligacionCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Método para generar lista de fechas de cierres de obligaciones
        /// </summary>
        /// <param name="pObligacionCredito"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<ObligacionCredito> ListarProvisionFechas(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                Configuracion conf = new Configuracion();
                List<ObligacionCredito> LstCierre = new List<ObligacionCredito>();
                // Determinar la fecha del último cierre definitivo realizado
                DateTime FecIni = DAObligacionCredito.FechaUltimoCierre(pObligacionCredito.tipo_cierre, pUsuario);
                if (FecIni == DateTime.MinValue)
                    return null;

                DateTime FecCieIni = DateTime.MinValue;
                DateTime FecCie = DateTime.MinValue;

                FecCieIni = BOFechas.FecUltDia(FecIni).AddDays(1);
                FecIni = FecCieIni;
                FecCieIni = BOFechas.SumarMeses(FecCieIni, 1);
                FecCie = FecCieIni.AddDays(-1);

                while (FecCieIni <= DateTime.Now.AddDays(15))
                {
                    ObligacionCredito CieObl = new ObligacionCredito();
                    FecCieIni = FecCieIni.AddDays(-1);
                    CieObl.sfecha_corte = FecCieIni.ToString(conf.ObtenerFormatoFecha());
                    LstCierre.Add(CieObl);
                    FecCieIni = BOFechas.SumarMeses(FecCieIni.AddDays(1), 1);
                }
                return LstCierre;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ListarProvisionFechas", ex);
                return null;
            }
        }

        public ObligacionCredito ModificarProvision(ObligacionCredito pObligacionCredito, GridView datos, Usuario pUsuario)
        {
            Configuracion conf = new Configuracion();
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (GridViewRow fila in datos.Rows)
                    {
                        pObligacionCredito.codobligacion = Int64.Parse(fila.Cells[0].Text);
                        pObligacionCredito.intereses = Int64.Parse(fila.Cells[12].Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
                        pObligacionCredito.dias_causados = Int64.Parse(fila.Cells[11].Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
                        DAObligacionCredito.ModificarProvision(pObligacionCredito, pUsuario);
                    }
                    DAObligacionCredito.ControlProvision(pObligacionCredito.fecha_corte, "D", pUsuario);
                    ts.Complete();
                }

                return pObligacionCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ModificarProvision", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pObligacionCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito Vencidos obtenidos</returns>
        public List<ObligacionCredito> ListarObligacionCreditoVencido(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return DAObligacionCredito.ListarObligacionCreditoVencido(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ListarObligacionCreditoVencido", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pObligacionCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito Pendientes por Pagar Vencidos obtenidos</returns>
        public List<ObligacionCredito> ListarObligacionPendPagar(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return DAObligacionCredito.ListarObligacionPendPagar(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ListarObligacionPendPagar", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pObligacionCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito Solcitudes obtenidos</returns>
        public List<ObligacionCredito> ListarDatosSolicitud(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return DAObligacionCredito.ListarDatosSolicitud(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ListarDatosSolicitud", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pObligacionCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito Solcitudes obtenidos</returns>
        public List<ObligacionCredito> ListarDatosSolicitudAprobacion(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return DAObligacionCredito.ListarDatosSolicitudAprobacion(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ListarDatosSolicitudAprobacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de OBTranscciones cruzadas con las operaciones 
        /// </summary>
        /// <param name="pObligacionCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de OBTranscciones cruzadas con las operaciones </returns>
        public List<ObligacionCredito> ListarPlanObligacion(ObligacionCredito pObligacionCredito, decimal monto, Usuario pUsuario)
        {
            try
            {
                List<ObligacionCredito> lstObligacionCredito = new List<ObligacionCredito>();

                lstObligacionCredito = DAObligacionCredito.ListarPlanObligacion(pObligacionCredito, pUsuario);
                decimal saldoInicial = 0;
                pObligacionCredito = DAObligacionCredito.ConsultarObligacionCredito(pObligacionCredito.codobligacion, pUsuario);
                
                saldoInicial = pObligacionCredito.saldocapital;

            

                foreach (ObligacionCredito rFila in lstObligacionCredito)
                {
                    rFila.total = rFila.amort_cap + rFila.interes_corriente + rFila.interes_mora + rFila.seguro;
                    saldoInicial = saldoInicial - rFila.amort_cap;
                    rFila.saldo = saldoInicial;
                }
                return lstObligacionCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ListarPlanObligacion", ex);
                return null;
            }
        }

        public List<ObligacionCredito> ListarMovsObligacion(ObligacionCredito pObligacionCredito, decimal monto, Usuario pUsuario)
        {
            try
            {
                List<ObligacionCredito> lstObligacionCredito = new List<ObligacionCredito>();
                lstObligacionCredito = DAObligacionCredito.ListarMovsObligacion(pObligacionCredito, pUsuario);
                decimal saldoInicial = 0;
                foreach (ObligacionCredito rFila in lstObligacionCredito)
                {
                    rFila.total = rFila.amort_cap + rFila.interes_corriente + rFila.interes_mora + rFila.seguro;
                    if (rFila.tipo_mov == "Crédito" && rFila.tipo_tran !=707 && rFila.tipo_tran != 708)
                    {
                        saldoInicial = saldoInicial - rFila.amort_cap;
                    }
                    if (rFila.tipo_mov == "Débito" && rFila.tipo_tran == 708)
                    {
                        saldoInicial = saldoInicial - rFila.amort_cap;
                    }
                    if (rFila.tipo_mov == "Débito" && rFila.tipo_tran != 707 && rFila.tipo_tran != 708) 
                    {
                        saldoInicial = saldoInicial + rFila.amort_cap;
                    }
                    if (rFila.tipo_mov == "Crédito" && rFila.tipo_tran == 707)
                    {
                        saldoInicial = saldoInicial + rFila.amort_cap;
                    }

                    rFila.saldo = saldoInicial;
                }
                return lstObligacionCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ListarMovsObligacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de OBTranscciones cruzadas con las operaciones 
        /// </summary>
        /// <param name="pObligacionCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de OBTranscciones cruzadas con las operaciones </returns>
        public List<ObligacionCredito> ListarDistribPagosPendCuotas(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return DAObligacionCredito.ListarDistribPagosPendCuotas(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ListarDistribPagosPendCuotas", ex);
                return null;
            }
        }


        /// <summary>
        /// Crea un Transaccion Operacion para Una Obligacion
        /// </summary>
        /// <param name="pObligacionCredito">Entidad Transaccion Obligacion</param>
        /// <returns>Entidad Transaccion Operacion Obligacion creada</returns>
        public ObligacionCredito CrearTransacOpePagoOb(ObligacionCredito pObligacionCredito,  Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pObligacionCredito = DAObligacionCredito.CrearTransacOpePagoOb(pObligacionCredito, pUsuario);
                    Xpinn.FabricaCreditos.Data.AvanceData DACredito = new Xpinn.FabricaCreditos.Data.AvanceData();
                    
                    //DACredito.CrearGiro( pUsuario);
                    ts.Complete();
                }

                return pObligacionCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "CrearTransacOpePagoOb", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<ObligacionCredito> ListarOperaciones(ObligacionCredito pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return DAObligacionCredito.ListarOperaciones(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ListarOperaciones", ex);
                return null;
            }
        }


        public ObligacionCredito ProvisionCredito(ObligacionCredito pObligacion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pObligacion = DAObligacionCredito.ProvisionCredito(pObligacion, pUsuario);

                    ts.Complete();
                }

                return pObligacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoBusiness", "ProvisionCredito", ex);
                return null;
            }
        }

    }
}