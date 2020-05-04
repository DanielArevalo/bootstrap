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
    /// Objeto de negocio para MatrizRiesgoFactorNIF
    /// </summary>
    public class MatrizRiesgoFactorNIFBusiness : GlobalData
    {
        private MatrizRiesgoFactorNIFData DAMatrizRiesgoFactorNIF;

        /// <summary>
        /// Constructor del objeto de negocio para MatrizRiesgoFactorNIF
        /// </summary>
        public MatrizRiesgoFactorNIFBusiness()
        {
            DAMatrizRiesgoFactorNIF = new MatrizRiesgoFactorNIFData();
        }

        /// <summary>
        /// Crea un MatrizRiesgoFactorNIF
        /// </summary>
        /// <param name="pMatrizRiesgoFactorNIF">Entidad MatrizRiesgoFactorNIF</param>
        /// <returns>Entidad MatrizRiesgoFactorNIF creada</returns>
        public MatrizRiesgoFactorNIF CrearMatrizRiesgoFactorNIF(MatrizRiesgoFactorNIF pMatrizRiesgoFactorNIF, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMatrizRiesgoFactorNIF = DAMatrizRiesgoFactorNIF.CrearMatrizRiesgoFactorNIF(pMatrizRiesgoFactorNIF, pUsuario);

                    ts.Complete();
                }

                return pMatrizRiesgoFactorNIF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoFactorNIFBusiness", "CrearMatrizRiesgoFactorNIF", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un MatrizRiesgoFactorNIF
        /// </summary>
        /// <param name="pMatrizRiesgoFactorNIF">Entidad MatrizRiesgoFactorNIF</param>
        /// <returns>Entidad MatrizRiesgoFactorNIF modificada</returns>
        public MatrizRiesgoFactorNIF ModificarMatrizRiesgoFactorNIF(MatrizRiesgoFactorNIF pMatrizRiesgoFactorNIF, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMatrizRiesgoFactorNIF = DAMatrizRiesgoFactorNIF.ModificarMatrizRiesgoFactorNIF(pMatrizRiesgoFactorNIF, pUsuario);

                    ts.Complete();
                }

                return pMatrizRiesgoFactorNIF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoFactorNIFBusiness", "ModificarMatrizRiesgoFactorNIF", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un MatrizRiesgoFactorNIF
        /// </summary>
        /// <param name="pId">Identificador de MatrizRiesgoFactorNIF</param>
        public void EliminarMatrizRiesgoFactorNIF(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAMatrizRiesgoFactorNIF.EliminarMatrizRiesgoFactorNIF(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoFactorNIFBusiness", "EliminarMatrizRiesgoFactorNIF", ex);
            }
        }

        /// <summary>
        /// Obtiene un MatrizRiesgoFactorNIF
        /// </summary>
        /// <param name="pId">Identificador de MatrizRiesgoFactorNIF</param>
        /// <returns>Entidad MatrizRiesgoFactorNIF</returns>
        public MatrizRiesgoFactorNIF ConsultarMatrizRiesgoFactorNIF(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAMatrizRiesgoFactorNIF.ConsultarMatrizRiesgoFactorNIF(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoFactorNIFBusiness", "ConsultarMatrizRiesgoFactorNIF", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMatrizRiesgoFactorNIF">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MatrizRiesgoFactorNIF obtenidos</returns>
        public List<MatrizRiesgoFactorNIF> ListarMatrizRiesgoFactorNIF(MatrizRiesgoFactorNIF pMatrizRiesgoFactorNIF, Usuario pUsuario)
        {
            try
            {
                return DAMatrizRiesgoFactorNIF.ListarMatrizRiesgoFactorNIF(pMatrizRiesgoFactorNIF, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoFactorNIFBusiness", "ListarMatrizRiesgoFactorNIF", ex);
                return null;
            }
        }

    }

}