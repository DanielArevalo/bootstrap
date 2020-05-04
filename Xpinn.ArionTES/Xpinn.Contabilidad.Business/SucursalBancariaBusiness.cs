using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para SucursalBancaria
    /// </summary>
    public class SucursalBancariaBusiness : GlobalBusiness
    {
        private SucursalBancariaData DASucursalBancaria;

        /// <summary>
        /// Constructor del objeto de negocio para SucursalBancaria
        /// </summary>
        public SucursalBancariaBusiness()
        {
            DASucursalBancaria = new SucursalBancariaData();
        }

        /// <summary>
        /// Crea un SucursalBancaria
        /// </summary>
        /// <param name="pSucursalBancaria">Entidad SucursalBancaria</param>
        /// <returns>Entidad SucursalBancaria creada</returns>
        public SucursalBancaria CrearSucursalBancaria(SucursalBancaria pSucursalBancaria, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSucursalBancaria = DASucursalBancaria.CrearSucursalBancaria(pSucursalBancaria, vusuario);

                    ts.Complete();
                }

                return pSucursalBancaria;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SucursalBancariaBusiness", "CrearSucursalBancaria", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un SucursalBancaria
        /// </summary>
        /// <param name="pSucursalBancaria">Entidad SucursalBancaria</param>
        /// <returns>Entidad SucursalBancaria modificada</returns>
        public SucursalBancaria ModificarSucursalBancaria(SucursalBancaria pSucursalBancaria, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSucursalBancaria = DASucursalBancaria.ModificarSucursalBancaria(pSucursalBancaria, vusuario);

                    ts.Complete();
                }

                return pSucursalBancaria;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SucursalBancariaBusiness", "ModificarSucursalBancaria", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un SucursalBancaria
        /// </summary>
        /// <param name="pId">Identificador de SucursalBancaria</param>
        public void EliminarSucursalBancaria(Int64 pId, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DASucursalBancaria.EliminarSucursalBancaria(pId, vusuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SucursalBancariaBusiness", "EliminarSucursalBancaria", ex);
            }
        }

        /// <summary>
        /// Obtiene un SucursalBancaria
        /// </summary>
        /// <param name="pId">Identificador de SucursalBancaria</param>
        /// <returns>Entidad SucursalBancaria</returns>
        public SucursalBancaria ConsultarSucursalBancaria(Int64 pId, Usuario vusuario)
        {
            try
            {
                SucursalBancaria SucursalBancaria = new SucursalBancaria();

                SucursalBancaria = DASucursalBancaria.ConsultarSucursalBancaria(pId, vusuario);

                return SucursalBancaria;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SucursalBancariaBusiness", "ConsultarSucursalBancaria", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pSucursalBancaria">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SucursalBancaria obtenidos</returns>
        public List<SucursalBancaria> ListarSucursalBancaria(SucursalBancaria pSucursalBancaria, Usuario vUsuario)
        {
            try
            {
                return DASucursalBancaria.ListarSucursalBancaria(pSucursalBancaria, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SucursalBancariaBusiness", "ListarSucursalBancaria", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario vusuario)
        {
            try
            {
                return DASucursalBancaria.ObtenerSiguienteCodigo(vusuario);
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

    }
}