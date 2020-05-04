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
    public class ArticuloBusiness : GlobalBusiness
    {
        private ArticuloData DAArticulo;

        /// <summary>
        /// Constructor del objeto de negocio para ActivosFijos
        /// </summary>
        public ArticuloBusiness()
        {
            DAArticulo = new ArticuloData();
        }

        /// <summary>
        /// Crea un ActivosFijos
        /// </summary>
        /// <param name="pActivosFijos">Entidad ActivosFijos</param>
        /// <returns>Entidad ActivosFijos creada</returns>
        public Articulo CrearArticulo(Articulo pArticulo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pArticulo = DAArticulo.CrearArticulo(pArticulo, pUsuario);

                    ts.Complete();
                }

                return pArticulo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArticuloBusiness", "Areas", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ActivosFijos
        /// </summary>
        /// <param name="pActivosFijos">Entidad ActivosFijos</param>
        /// <returns>Entidad ActivosFijos modificada</returns>
        public Articulo ModificarArticulo(Articulo pArticulo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pArticulo = DAArticulo.ModificarArticulo(pArticulo, pUsuario);


                    ts.Complete();
                }

                return pArticulo;
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
        public void EliminarArticulo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAArticulo.EliminarArticulo(pId, pUsuario);

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
        public Articulo ConsultarArticulo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Articulo Articulo = new Articulo();

                Articulo = DAArticulo.ConsultarArticulo(pId, vUsuario);



                return Articulo;
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
        public List<Articulo> ListarArticulo(Articulo pArticulo, Usuario pUsuario)
        {
            try
            {
                return DAArticulo.ListarArticulo(pArticulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArticuloBusiness", "ListarActivosFijos", ex);
                return null;
            }
        }









        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAArticulo.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }






    }
}