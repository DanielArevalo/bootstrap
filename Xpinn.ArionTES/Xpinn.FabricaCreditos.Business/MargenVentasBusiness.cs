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
    /// Objeto de negocio para MargenVentas
    /// </summary>
    public class MargenVentasBusiness : GlobalData
    {
        private MargenVentasData DAMargenVentas;

        /// <summary>
        /// Constructor del objeto de negocio para MargenVentas
        /// </summary>
        public MargenVentasBusiness()
        {
            DAMargenVentas = new MargenVentasData();
        }

        /// <summary>
        /// Crea un MargenVentas
        /// </summary>
        /// <param name="pMargenVentas">Entidad MargenVentas</param>
        /// <returns>Entidad MargenVentas creada</returns>
        public MargenVentas CrearMargenVentas(MargenVentas pMargenVentas, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    CalcularTotal(pMargenVentas);
                    pMargenVentas = DAMargenVentas.CrearMargenVentas(pMargenVentas, pUsuario);
                    ts.Complete();
                }

                return pMargenVentas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MargenVentasBusiness", "CrearMargenVentas", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un MargenVentas
        /// </summary>
        /// <param name="pMargenVentas">Entidad MargenVentas</param>
        /// <returns>Entidad MargenVentas modificada</returns>
        public MargenVentas ModificarMargenVentas(MargenVentas pMargenVentas, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    CalcularTotal(pMargenVentas);
                    pMargenVentas = DAMargenVentas.ModificarMargenVentas(pMargenVentas, pUsuario);
                    ts.Complete();
                }

                return pMargenVentas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MargenVentasBusiness", "ModificarMargenVentas", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un MargenVentas
        /// </summary>
        /// <param name="pId">Identificador de MargenVentas</param>
        public void EliminarMargenVentas(Int64 pId, Int64 persona, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAMargenVentas.EliminarMargenVentas(pId, persona, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MargenVentasBusiness", "EliminarMargenVentas", ex);
            }
        }

        /// <summary>
        /// Obtiene un MargenVentas
        /// </summary>
        /// <param name="pId">Identificador de MargenVentas</param>
        /// <returns>Entidad MargenVentas</returns>
        public MargenVentas ConsultarMargenVentas(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAMargenVentas.ConsultarMargenVentas(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MargenVentasBusiness", "ConsultarMargenVentas", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMargenVentas">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MargenVentas obtenidos</returns>
        public List<MargenVentas> ListarMargenVentas(MargenVentas pMargenVentas, Usuario pUsuario)
        {
            try
            {
                List<MargenVentas> lstMargen = DAMargenVentas.ListarMargenVentas(pMargenVentas, pUsuario);

                //Realiza calculos de utilidad de acuerdo al select realizado:

                for (int numFila = 0; numFila <= lstMargen.Count() - 1; numFila++)
                {
                    lstMargen[numFila].utilidad = lstMargen[numFila].ventatotal - lstMargen[numFila].costoventa;
                }
                return lstMargen;
                       
                //return DAMargenVentas.ListarMargenVentas(pMargenVentas, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MargenVentasBusiness", "ListarMargenVentas", ex);
                return null;
            }
        }


        private void CalcularTotal(MargenVentas pMargenVentas)
        {
            pMargenVentas.costoventa = pMargenVentas.univendida * pMargenVentas.costounidven;
            pMargenVentas.ventatotal = pMargenVentas.univendida * pMargenVentas.preciounidven;
            pMargenVentas.margen = Math.Round(((pMargenVentas.ventatotal - pMargenVentas.costoventa) * 100.00)/ (pMargenVentas.ventatotal), 2);
        }


        /// <summary>
        /// Obtiene calculos de los totales
        /// </summary>
        /// <param name="pVentasSemanales">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de VentasSemanales obtenidos</returns>
        public MargenVentas CalculosMargenVentas(List<MargenVentas> lstConsulta)
        {
            try
            {
                MargenVentas entidad = new MargenVentas();

                for (int numFila = 0; numFila <= lstConsulta.Count() - 1; numFila++)
                {
                    entidad.totalCostoVenta = entidad.totalCostoVenta + lstConsulta[numFila].costoventa;
                    entidad.totalVentaTotal = entidad.totalVentaTotal + lstConsulta[numFila].ventatotal;
                }

                entidad.porcentajeMargen = Math.Round( (((entidad.totalVentaTotal - entidad.totalCostoVenta) * 100.00) / entidad.totalVentaTotal), 2);
              
                entidad.porcentajeCostoVenta = 100 - entidad.porcentajeMargen;

                return entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualBusiness", "CalculosTotalesSemanales", ex);
                return null;
            }
        }


    }
}