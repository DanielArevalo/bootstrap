using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para EstadosFinancieros
    /// </summary>
    public class EstadosFinancierosBusiness : GlobalData
    {
        private EstadosFinancierosData DAEstadosFinancieros;

        /// <summary>
        /// Constructor del objeto de negocio para EstadosFinancieros
        /// </summary>
        public EstadosFinancierosBusiness()
        {
            DAEstadosFinancieros = new EstadosFinancierosData();
        }

        /// <summary>
        /// Crea un EstadosFinancieros
        /// </summary>
        /// <param name="pEstadosFinancieros">Entidad EstadosFinancieros</param>
        /// <returns>Entidad EstadosFinancieros creada</returns>
        public EstadosFinancieros CrearEstadosFinancieros(EstadosFinancieros pEstadosFinancieros, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Estado resultados
                    pEstadosFinancieros.UtiBru = pEstadosFinancieros.VenTot - pEstadosFinancieros.CosVen;
                    pEstadosFinancieros.TotGas = pEstadosFinancieros.GasPer + pEstadosFinancieros.SerPub + pEstadosFinancieros.Arr + pEstadosFinancieros.Tra + pEstadosFinancieros.OtrGas;
                    pEstadosFinancieros.UtiOpe = pEstadosFinancieros.UtiBru - pEstadosFinancieros.TotGas;
                    pEstadosFinancieros.TotGasFin = pEstadosFinancieros.Cad + pEstadosFinancieros.Pres + pEstadosFinancieros.OtrGasFin;
                    pEstadosFinancieros.UtiNet = pEstadosFinancieros.UtiOpe - pEstadosFinancieros.TotGasFin;                    
                    //Activos
                    pEstadosFinancieros.TotActCor = pEstadosFinancieros.EfeBan + pEstadosFinancieros.CueCob + pEstadosFinancieros.Merc + pEstadosFinancieros.ProdProc + pEstadosFinancieros.ProTer;
                    pEstadosFinancieros.TotActFij = pEstadosFinancieros.TerrEdi + pEstadosFinancieros.MaqEqu + pEstadosFinancieros.Vehi + pEstadosFinancieros.Otr;
                    pEstadosFinancieros.TotAct = pEstadosFinancieros.TotActFij + pEstadosFinancieros.TotActCor;
                    //Pasivos
                    pEstadosFinancieros.TotPasCor = pEstadosFinancieros.Pro + pEstadosFinancieros.OblFin + pEstadosFinancieros.OtrOblLar;
                    pEstadosFinancieros.TotPas = pEstadosFinancieros.TotPasCor + pEstadosFinancieros.PasLar + pEstadosFinancieros.OtrOblLar;
                    pEstadosFinancieros.TotPasPat = pEstadosFinancieros.TotPas + pEstadosFinancieros.TotPat;
                    pEstadosFinancieros.TotPat = pEstadosFinancieros.TotPasPat - pEstadosFinancieros.TotPas;

                    pEstadosFinancieros = DAEstadosFinancieros.CrearEstadosFinancieros(pEstadosFinancieros, pUsuario);

                    ts.Complete();
                }

                return pEstadosFinancieros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosBusiness", "CrearEstadosFinancieros", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un EstadosFinancieros
        /// </summary>
        /// <param name="pEstadosFinancieros">Entidad EstadosFinancieros</param>
        /// <returns>Entidad EstadosFinancieros modificada</returns>
        public EstadosFinancieros ModificarEstadosFinancieros(EstadosFinancieros pEstadosFinancieros, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEstadosFinancieros = DAEstadosFinancieros.ModificarEstadosFinancieros(pEstadosFinancieros, pUsuario);

                    ts.Complete();
                }

                return pEstadosFinancieros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosBusiness", "ModificarEstadosFinancieros", ex);
                return null;
            }
        }

        public EstadosFinancieros RecalcularEstadosFinancieros(EstadosFinancieros pEstadosFinancieros, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEstadosFinancieros = DAEstadosFinancieros.RecalcularEstadosFinancieros(pEstadosFinancieros, pUsuario);

                    ts.Complete();
                }

                return pEstadosFinancieros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosBusiness", "RecalcularEstadosFinancieros", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un EstadosFinancieros
        /// </summary>
        /// <param name="pId">Identificador de EstadosFinancieros</param>
        public void EliminarEstadosFinancieros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEstadosFinancieros.EliminarEstadosFinancieros(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosBusiness", "EliminarEstadosFinancieros", ex);
            }
        }

        /// <summary>
        /// Obtiene un EstadosFinancieros
        /// </summary>
        /// <param name="pId">Identificador de EstadosFinancieros</param>
        /// <returns>Entidad EstadosFinancieros</returns>
        public EstadosFinancieros ConsultarEstadosFinancieros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAEstadosFinancieros.ConsultarEstadosFinancieros(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosBusiness", "ConsultarEstadosFinancieros", ex);
                return null;
            }
        }

        public EstadosFinancieros listarperosnainfofin(long cod, Usuario pUsuario)
        {
            try
            {
                return DAEstadosFinancieros.listarperosnainfofin(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosService", "ConsultarEstadosFinancieros", ex);
                return null;
            }
        }

        public void guardarIngreEgre(EstadosFinancieros informacionFinanciera, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEstadosFinancieros.guardarIngreEgre(informacionFinanciera, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosService", "ConsultarEstadosFinancieros", ex);
           
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pEstadosFinancieros">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EstadosFinancieros obtenidos</returns>
        public List<EstadosFinancieros> ListarEstadosFinancieros(EstadosFinancieros pEstadosFinancieros, Usuario pUsuario)
        {
            try
            {
                return DAEstadosFinancieros.ListarEstadosFinancieros(pEstadosFinancieros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosBusiness", "ListarEstadosFinancieros", ex);
                return null;
            }
        }


        public EstadosFinancieros UtilidadEstadosFinancieros(EstadosFinancieros entTotalesEstados)
        {
            try
            {
                entTotalesEstados.CosVen = entTotalesEstados.VenTot * (entTotalesEstados.porCostoVenta/100);
                entTotalesEstados.UtiBru = entTotalesEstados.VenTot - entTotalesEstados.CosVen;                

                return entTotalesEstados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualBusiness", "CalculosTotalesSemanales", ex);
                return null;
            }
        }

        public List<EstadosFinancieros> ListarIngresosEgresosRepo(EstadosFinancieros pInformacionfinanciera, Usuario pUsuario)
        {
            try
            {
                return DAEstadosFinancieros.ListarIngresosEgresosRepo(pInformacionfinanciera, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosBusiness", "ListarIngresosEgresosRepo", ex);
                return null;
            }
        }
                
        //Agregado para listar cuentas de moneda extranjera
        public List<EstadosFinancieros> ListarCuentasMonedaExtranjera(Int64 cod_persona, Usuario pUsuario)
        {
            try
            {
                return DAEstadosFinancieros.ListarCuentasMonedaExtranjera(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosBusiness", "ListarCuentasMonedaExtranjera", ex);
                return null;
            }
        }

        //Agregado para eliminara cuentas de moneda extranjera
        public void EliminarCuentasMonedaExtranjera(Int64 pCodMoneda, Usuario vUsuario)
        {
            try
            {
                DAEstadosFinancieros.EliminarCuentasMonedaExtranjera(pCodMoneda, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosBusiness", "EliminarCuentasMonedaExtranjera", ex);
            }
        }

    }
}