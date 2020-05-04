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
    /// Objeto de negocio para InventarioMateriaPrima
    /// </summary>
    public class InventarioMateriaPrimaBusiness : GlobalData
    {
        private InventarioMateriaPrimaData DAInventarioMateriaPrima;

        /// <summary>
        /// Constructor del objeto de negocio para InventarioMateriaPrima
        /// </summary>
        public InventarioMateriaPrimaBusiness()
        {
            DAInventarioMateriaPrima = new InventarioMateriaPrimaData();
        }

        /// <summary>
        /// Crea un InventarioMateriaPrima
        /// </summary>
        /// <param name="pInventarioMateriaPrima">Entidad InventarioMateriaPrima</param>
        /// <returns>Entidad InventarioMateriaPrima creada</returns>
        public InventarioMateriaPrima CrearInventarioMateriaPrima(InventarioMateriaPrima pInventarioMateriaPrima, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pInventarioMateriaPrima = DAInventarioMateriaPrima.CrearInventarioMateriaPrima(pInventarioMateriaPrima, pUsuario);

                    ts.Complete();
                }

                return pInventarioMateriaPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioMateriaPrimaBusiness", "CrearInventarioMateriaPrima", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un InventarioMateriaPrima
        /// </summary>
        /// <param name="pInventarioMateriaPrima">Entidad InventarioMateriaPrima</param>
        /// <returns>Entidad InventarioMateriaPrima modificada</returns>
        public InventarioMateriaPrima ModificarInventarioMateriaPrima(InventarioMateriaPrima pInventarioMateriaPrima, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pInventarioMateriaPrima = DAInventarioMateriaPrima.ModificarInventarioMateriaPrima(pInventarioMateriaPrima, pUsuario);

                    ts.Complete();
                }

                return pInventarioMateriaPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioMateriaPrimaBusiness", "ModificarInventarioMateriaPrima", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un InventarioMateriaPrima
        /// </summary>
        /// <param name="pId">Identificador de InventarioMateriaPrima</param>
        public void EliminarInventarioMateriaPrima(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAInventarioMateriaPrima.EliminarInventarioMateriaPrima(pId, pUsuario, Cod_persona, Cod_InfFin);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioMateriaPrimaBusiness", "EliminarInventarioMateriaPrima", ex);
            }
        }

        /// <summary>
        /// Obtiene un InventarioMateriaPrima
        /// </summary>
        /// <param name="pId">Identificador de InventarioMateriaPrima</param>
        /// <returns>Entidad InventarioMateriaPrima</returns>
        public InventarioMateriaPrima ConsultarInventarioMateriaPrima(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAInventarioMateriaPrima.ConsultarInventarioMateriaPrima(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioMateriaPrimaBusiness", "ConsultarInventarioMateriaPrima", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pInventarioMateriaPrima">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InventarioMateriaPrima obtenidos</returns>
        public List<InventarioMateriaPrima> ListarInventarioMateriaPrima(InventarioMateriaPrima pInventarioMateriaPrima, Usuario pUsuario)
        {
            try
            {
                return DAInventarioMateriaPrima.ListarInventarioMateriaPrima(pInventarioMateriaPrima, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioMateriaPrimaBusiness", "ListarInventarioMateriaPrima", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pInventarioMateriaPrima">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InventarioMateriaPrima obtenidos</returns>
        public List<InventarioMateriaPrima> ListarInventarioMateriaPrimaRepo(InventarioMateriaPrima pInventarioMateriaPrima, Usuario pUsuario)
        {
            try
            {
                return DAInventarioMateriaPrima.ListarInventarioMateriaPrimaRepo(pInventarioMateriaPrima, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioMateriaPrimaBusiness", "ListarInventarioMateriaPrimaRepo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene calculos de los totales
        /// </summary>
        /// <param name="pVentasSemanales">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de VentasSemanales obtenidos</returns>
        public InventarioMateriaPrima CalculosInventarioMateriaPrima(List<InventarioMateriaPrima> lstConsulta)
        {
            try
            {
                InventarioMateriaPrima entidad = new InventarioMateriaPrima();

                for (int numFila = 0; numFila <= lstConsulta.Count() - 1; numFila++)
                    entidad.totalMateriaPrima = entidad.totalMateriaPrima + lstConsulta[numFila].valor;
                
                return entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioMateriaPrimaBusiness", "CalculosInventarioMateriaPrima", ex);
                return null;
            }
        }


    }
}