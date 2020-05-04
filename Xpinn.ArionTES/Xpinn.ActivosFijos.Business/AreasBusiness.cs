using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.ActivosFijos.Data;
using Xpinn.ActivosFijos.Entities;



namespace Xpinn.ActivosFijos.Business
{
    /// <summary>
    /// Objeto de negocio para ActivosFijos
    /// </summary>
    public class AreasBusiness : GlobalBusiness
    {
        private AreasData DAAreas;

        /// <summary>
        /// Constructor del objeto de negocio para ActivosFijos
        /// </summary>
        public AreasBusiness()
        {
            DAAreas = new AreasData();
        }

        /// <summary>
        /// Crea un ActivosFijos
        /// </summary>
        /// <param name="pActivosFijos">Entidad ActivosFijos</param>
        /// <returns>Entidad ActivosFijos creada</returns>
        public Areas CrearAreas(Areas pAreas, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAreas = DAAreas.CrearAreas(pAreas, pUsuario);

                    ts.Complete();
                }

                return pAreas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasBusiness", "Areas", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ActivosFijos
        /// </summary>
        /// <param name="pActivosFijos">Entidad ActivosFijos</param>
        /// <returns>Entidad ActivosFijos modificada</returns>
        public Areas ModificarAreas(Areas pAreas, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAreas = DAAreas.ModificarAreas(pAreas, pUsuario);


                    ts.Complete();
                }

                return pAreas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ModificarActivosFijos", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ActivosFijos
        /// </summary>
        /// <param name="pId">Identificador de ActivosFijos</param>
        public void EliminarAreas(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAreas.EliminarAreas(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "EliminarActivosFijos", ex);
            }
        }

        /// <summary>
        /// Obtiene un ActivosFijos
        /// </summary>
        /// <param name="pId">Identificador de ActivosFijos</param>
        /// <returns>Entidad ActivosFijos</returns>
        public Areas ConsultarAreas(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Areas Areas = new Areas();

                Areas = DAAreas.ConsultarAreas(pId, vUsuario);



                return Areas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ConsultarActivosFijos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pActivosFijos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ActivosFijos obtenidos</returns>
        public List<Areas> ListarAreas(Areas pAreas, Usuario pUsuario)
        {
            try
            {
                return DAAreas.ListarAreas(pAreas, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ListarActivosFijos", ex);
                return null;
            }
        }









        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAAreas.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }






    }
}