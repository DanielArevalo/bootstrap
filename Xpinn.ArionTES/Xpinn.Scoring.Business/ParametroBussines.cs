using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Scoring.Data;
using Xpinn.Scoring.Entities;

namespace Xpinn.Scoring.Business
{
    /// <summary>
    /// Objeto de negocio para Parametros
    /// </summary>
    public class ParametroBusiness : GlobalData
    {
        private ParametroData DAParametro;

        /// <summary>
        /// Constructor del objeto de negocio para Parametro
        /// </summary>
        public ParametroBusiness()
        {
            DAParametro = new ParametroData();
        }


        /// <summary>
        /// Crea un Parametro
        /// </summary>
        /// <param name="pParametro">Entidad Crear Parametro</param>
        /// <returns>Entidad Parametro creada</returns>
        public Parametro CrearParametro(Parametro pParametro, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParametro = DAParametro.CrearParametro(pParametro, pUsuario);

                    ts.Complete();
                }

                return pParametro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroBusiness", "CrearParametro", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Parametro
        /// </summary>
        /// <param name="pParametro">Entidad Parametro</param>
        /// <returns>Entidad Parametro modificada</returns>
        public Parametro ModificarParametro(Parametro pParametro, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParametro = DAParametro.ModificarParametro(pParametro, pUsuario);

                    ts.Complete();
                }

                return pParametro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroBusiness", "ModificarParametro", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina Parametro
        /// </summary>
        /// <param name="pId">Identificador de Parametro</param>
        public void EliminarParametro(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAParametro.EliminarParametro(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroBusiness", "EliminarParametro", ex);
            }
        }

        /// <summary>
        /// Obtiene un Parametro
        /// </summary>
        /// <param name="pId">Identificador de Parametro</param>
        /// <returns>Entidad Parametro</returns>
        public Parametro ConsultarParametro(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAParametro.ConsultarParametro(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroBusiness", "ConsultarParametro", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pParametro">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Parametro obtenidos</returns>
        public List<Parametro> ListarParametro(Parametro pParametro, Usuario pUsuario)
        {
            try
            {
                return DAParametro.ListarParametro(pParametro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroBusiness", "ListarParametro", ex);
                return null;
            }
        }

        public List<Modelo> ListarCampos(Modelo pParametro, Usuario pUsuario)
        {
            try
            {
                return DAParametro.ListarCampos(pParametro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroBusiness", "ListarCampos", ex);
                return null;
            }
        }

    }
}