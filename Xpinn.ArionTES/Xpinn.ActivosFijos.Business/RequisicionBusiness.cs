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
    public class RequisicionBusiness : GlobalBusiness
    {
        private RequisicionData DARequisicion;

        /// <summary>
        /// Constructor del objeto de negocio para ActivosFijos
        /// </summary>
        public RequisicionBusiness()
        {
            DARequisicion = new RequisicionData();
        }

        /// <summary>
        /// Crea un ActivosFijos
        /// </summary>
        /// <param name="pActivosFijos">Entidad ActivosFijos</param>
        /// <returns>Entidad ActivosFijos creada</returns>
        public Requisicion CrearRequisicion(Requisicion pRequisicion, List<Detalle_Requisicion> vRequisicionDet, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pRequisicion = DARequisicion.CrearRequisicion(pRequisicion, vRequisicionDet , pUsuario);

                    ts.Complete();
                }

                return pRequisicion;
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
        public Requisicion ModificarRequisicion(Requisicion pRequisicion, List<Detalle_Requisicion> vRequisicionDet, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pRequisicion = DARequisicion.ModificarRequisicion(pRequisicion, vRequisicionDet, pUsuario);


                    ts.Complete();
                }

                return pRequisicion;
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
        public void EliminarRequisicion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DARequisicion.EliminarRequisicion(pId, pUsuario);

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
        public Requisicion ConsultarRequisicion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Requisicion Requisicion = new Requisicion();

                Requisicion = DARequisicion.ConsultarRequisicion(pId, vUsuario);



                return Requisicion;
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
        public List<Requisicion> ListarRequisicion(Requisicion pRequisicion, Usuario pUsuario)
        {
            try
            {
                return DARequisicion.ListarRequisicion(pRequisicion, pUsuario);
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
                return DARequisicion.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }






    }
}