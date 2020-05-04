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
    /// Objeto de negocio para EstacionalidadMensual
    /// </summary>
    public class EstacionalidadMensualBusiness : GlobalData
    {
        private EstacionalidadMensualData DAEstacionalidadMensual;

        /// <summary>
        /// Constructor del objeto de negocio para EstacionalidadMensual
        /// </summary>
        public EstacionalidadMensualBusiness()
        {
            DAEstacionalidadMensual = new EstacionalidadMensualData();
        }

        /// <summary>
        /// Crea un EstacionalidadMensual
        /// </summary>
        /// <param name="pEstacionalidadMensual">Entidad EstacionalidadMensual</param>
        /// <returns>Entidad EstacionalidadMensual creada</returns>
        public EstacionalidadMensual CrearEstacionalidadMensual(EstacionalidadMensual pEstacionalidadMensual, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    CalcularTotal(pEstacionalidadMensual);
                    pEstacionalidadMensual = DAEstacionalidadMensual.CrearEstacionalidadMensual(pEstacionalidadMensual, pUsuario);
                    ts.Complete();
                }

                return pEstacionalidadMensual;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualBusiness", "CrearEstacionalidadMensual", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un EstacionalidadMensual
        /// </summary>
        /// <param name="pEstacionalidadMensual">Entidad EstacionalidadMensual</param>
        /// <returns>Entidad EstacionalidadMensual modificada</returns>
        public EstacionalidadMensual ModificarEstacionalidadMensual(EstacionalidadMensual pEstacionalidadMensual, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    CalcularTotal(pEstacionalidadMensual);
                    pEstacionalidadMensual = DAEstacionalidadMensual.ModificarEstacionalidadMensual(pEstacionalidadMensual, pUsuario);
                    ts.Complete();
                }

                return pEstacionalidadMensual;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualBusiness", "ModificarEstacionalidadMensual", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un EstacionalidadMensual
        /// </summary>
        /// <param name="pId">Identificador de EstacionalidadMensual</param>
        public void EliminarEstacionalidadMensual(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEstacionalidadMensual.EliminarEstacionalidadMensual(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualBusiness", "EliminarEstacionalidadMensual", ex);
            }
        }

        /// <summary>
        /// Obtiene un EstacionalidadMensual
        /// </summary>
        /// <param name="pId">Identificador de EstacionalidadMensual</param>
        /// <returns>Entidad EstacionalidadMensual</returns>
        public EstacionalidadMensual ConsultarEstacionalidadMensual(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAEstacionalidadMensual.ConsultarEstacionalidadMensual(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualBusiness", "ConsultarEstacionalidadMensual", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pEstacionalidadMensual">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EstacionalidadMensual obtenidos</returns>
        public List<EstacionalidadMensual> ListarEstacionalidadMensual(EstacionalidadMensual pEstacionalidadMensual, Usuario pUsuario)
        {
            try
            {
                return DAEstacionalidadMensual.ListarEstacionalidadMensual(pEstacionalidadMensual, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualBusiness", "ListarEstacionalidadMensual", ex);
                return null;
            }
        }




        /// <summary>
        /// Obtiene listas desplegables
        /// </summary>
        /// <param name="pEstacionalidadMensual">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EstacionalidadMensual obtenidos</returns>
        public List<EstacionalidadMensual> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return DAEstacionalidadMensual.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualBusiness", "ListarEstacionalidadMensual", ex);
                return null;
            }
        }




        /// <summary>
        /// Obtiene calculos de los totales
        /// </summary>
        /// <param name="pVentasSemanales">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de VentasSemanales obtenidos</returns>
        public EstacionalidadMensual CalculosEstacionalidadMensual(List<EstacionalidadMensual> lstConsulta)
        {
            try
            {
                EstacionalidadMensual entidad = new EstacionalidadMensual();

                for (int numFila = 0; numFila <= lstConsulta.Count() - 1; numFila++)
                    entidad.totalMensual = entidad.totalMensual + lstConsulta[numFila].total;
                entidad.promedioMensual = entidad.totalMensual / 12;

                return entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualBusiness", "CalculosTotalesSemanales", ex);
                return null;
            }
        }


        private void CalcularTotal(EstacionalidadMensual pEstacionalidadMensual)
        {

            if (pEstacionalidadMensual.enero != 0)
                pEstacionalidadMensual.total = pEstacionalidadMensual.total + pEstacionalidadMensual.valor;
            if (pEstacionalidadMensual.febrero != 0)
                pEstacionalidadMensual.total = pEstacionalidadMensual.total + pEstacionalidadMensual.valor;
            if (pEstacionalidadMensual.marzo != 0)
                pEstacionalidadMensual.total = pEstacionalidadMensual.total + pEstacionalidadMensual.valor;
            if (pEstacionalidadMensual.abril != 0)
                pEstacionalidadMensual.total = pEstacionalidadMensual.total + pEstacionalidadMensual.valor;
            if (pEstacionalidadMensual.mayo != 0)
                pEstacionalidadMensual.total = pEstacionalidadMensual.total + pEstacionalidadMensual.valor;
            if (pEstacionalidadMensual.junio != 0)
                pEstacionalidadMensual.total = pEstacionalidadMensual.total + pEstacionalidadMensual.valor;
            if (pEstacionalidadMensual.julio != 0)
                pEstacionalidadMensual.total = pEstacionalidadMensual.total + pEstacionalidadMensual.valor;
            if (pEstacionalidadMensual.agosto != 0)
                pEstacionalidadMensual.total = pEstacionalidadMensual.total + pEstacionalidadMensual.valor;
            if (pEstacionalidadMensual.septiembre != 0)
                pEstacionalidadMensual.total = pEstacionalidadMensual.total + pEstacionalidadMensual.valor;
            if (pEstacionalidadMensual.octubre != 0)
                pEstacionalidadMensual.total = pEstacionalidadMensual.total + pEstacionalidadMensual.valor;
            if (pEstacionalidadMensual.noviembre != 0)
                pEstacionalidadMensual.total = pEstacionalidadMensual.total + pEstacionalidadMensual.valor;
            if (pEstacionalidadMensual.diciembre != 0)
                pEstacionalidadMensual.total = pEstacionalidadMensual.total + pEstacionalidadMensual.valor;
           
        }


    }
}