using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.NIIF.Data;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Business
{
    /// <summary>
    /// Objeto de negocio para MatrizRiesgoNIF
    /// </summary>
    public class MatrizRiesgoNIFBusiness : GlobalData
    {
        private MatrizRiesgoNIFData DAMatrizRiesgoNIF;

        /// <summary>
        /// Constructor del objeto de negocio para MatrizRiesgoNIF
        /// </summary>
        public MatrizRiesgoNIFBusiness()
        {
            DAMatrizRiesgoNIF = new MatrizRiesgoNIFData();
        }

        /// <summary>
        /// Crea un MatrizRiesgoNIF
        /// </summary>
        /// <param name="pMatrizRiesgoNIF">Entidad MatrizRiesgoNIF</param>
        /// <returns>Entidad MatrizRiesgoNIF creada</returns>
        public MatrizRiesgoNIF CrearMatrizRiesgoNIF(MatrizRiesgoNIF pMatrizRiesgoNIF, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMatrizRiesgoNIF = DAMatrizRiesgoNIF.CrearMatrizRiesgoNIF(pMatrizRiesgoNIF, pUsuario);

                    ts.Complete();
                }

                return pMatrizRiesgoNIF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoNIFBusiness", "CrearMatrizRiesgoNIF", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un MatrizRiesgoNIF
        /// </summary>
        /// <param name="pMatrizRiesgoNIF">Entidad MatrizRiesgoNIF</param>
        /// <returns>Entidad MatrizRiesgoNIF modificada</returns>
        public MatrizRiesgoNIF ModificarMatrizRiesgoNIF(MatrizRiesgoNIF pMatrizRiesgoNIF, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMatrizRiesgoNIF = DAMatrizRiesgoNIF.ModificarMatrizRiesgoNIF(pMatrizRiesgoNIF, pUsuario);

                    ts.Complete();
                }

                return pMatrizRiesgoNIF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoNIFBusiness", "ModificarMatrizRiesgoNIF", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un MatrizRiesgoNIF
        /// </summary>
        /// <param name="pId">Identificador de MatrizRiesgoNIF</param>
        public void EliminarMatrizRiesgoNIF(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAMatrizRiesgoNIF.EliminarMatrizRiesgoNIF(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoNIFBusiness", "EliminarMatrizRiesgoNIF", ex);
            }
        }

        /// <summary>
        /// Obtiene un MatrizRiesgoNIF
        /// </summary>
        /// <param name="pId">Identificador de MatrizRiesgoNIF</param>
        /// <returns>Entidad MatrizRiesgoNIF</returns>
        public MatrizRiesgoNIF ConsultarMatrizRiesgoNIF(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAMatrizRiesgoNIF.ConsultarMatrizRiesgoNIF(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoNIFBusiness", "ConsultarMatrizRiesgoNIF", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMatrizRiesgoNIF">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MatrizRiesgoNIF obtenidos</returns>
        public List<MatrizRiesgoNIF> ListarMatrizRiesgoNIF(MatrizRiesgoNIF pMatrizRiesgoNIF, Usuario pUsuario)
        {
            try
            {
                return DAMatrizRiesgoNIF.ListarMatrizRiesgoNIF(pMatrizRiesgoNIF, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoNIFBusiness", "ListarMatrizRiesgoNIF", ex);
                return null;
            }
        }

        public List<Clasificacion> ListarClasificacion(Clasificacion clasificacion, Usuario pUsuario)
        {
            try
            {
                return DAMatrizRiesgoNIF.ListarClasificacion(clasificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoNIFService", "ListarClasificacion", ex);
                return null;
            }
        }

    }

}