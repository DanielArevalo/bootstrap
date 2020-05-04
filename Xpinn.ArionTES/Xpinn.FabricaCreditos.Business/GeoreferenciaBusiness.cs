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
    /// Objeto de negocio para Georeferencia
    /// </summary>
    public class GeoreferenciaBusiness : GlobalData
    {
        private GeoreferenciaData DAGeoreferencia;

        /// <summary>
        /// Constructor del objeto de negocio para Georeferencia
        /// </summary>
        public GeoreferenciaBusiness()
        {
            DAGeoreferencia = new GeoreferenciaData();
        }

        /// <summary>
        /// Crea un Georeferencia
        /// </summary>
        /// <param name="pGeoreferencia">Entidad Georeferencia</param>
        /// <returns>Entidad Georeferencia creada</returns>
        public Georeferencia CrearGeoreferencia(Georeferencia pGeoreferencia, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pGeoreferencia = DAGeoreferencia.CrearGeoreferencia(pGeoreferencia, pUsuario);

                    ts.Complete();
                }

                return pGeoreferencia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeoreferenciaBusiness", "CrearGeoreferencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Georeferencia
        /// </summary>
        /// <param name="pGeoreferencia">Entidad Georeferencia</param>
        /// <returns>Entidad Georeferencia modificada</returns>
        public Georeferencia ModificarGeoreferencia(Georeferencia pGeoreferencia, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pGeoreferencia = DAGeoreferencia.ModificarGeoreferencia(pGeoreferencia, pUsuario);

                    ts.Complete();
                }

                return pGeoreferencia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeoreferenciaBusiness", "ModificarGeoreferencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Georeferencia
        /// </summary>
        /// <param name="pId">Identificador de Georeferencia</param>
        public void EliminarGeoreferencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAGeoreferencia.EliminarGeoreferencia(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeoreferenciaBusiness", "EliminarGeoreferencia", ex);
            }
        }

        /// <summary>
        /// Obtiene un Georeferencia
        /// </summary>
        /// <param name="pId">Identificador de Georeferencia</param>
        /// <returns>Entidad Georeferencia</returns>
        public Georeferencia ConsultarGeoreferencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAGeoreferencia.ConsultarGeoreferencia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeoreferenciaBusiness", "ConsultarGeoreferencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pGeoreferencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Georeferencia obtenidos</returns>
        public List<Georeferencia> ListarGeoreferencia(Georeferencia pGeoreferencia, Usuario pUsuario)
        {
            try
            {
                return DAGeoreferencia.ListarGeoreferencia(pGeoreferencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeoreferenciaBusiness", "ListarGeoreferencia", ex);
                return null;
            }
        }
        public List<Georeferencia> ListarGeoreferenciacion(Georeferencia pGeoreferencia, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAGeoreferencia.ListarGeoreferenciacion(pGeoreferencia, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeoreferenciaBusiness", "ListarGeoreferencia", ex);
                return null;
            }
        }


        public Georeferencia ConsultarGeoreferenciaXPERSONA(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAGeoreferencia.ConsultarGeoreferenciaXPERSONA(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeoreferenciaBusiness", "ConsultarGeoreferenciaXPERSONA", ex);
                return null;
            }
        }

    }
}