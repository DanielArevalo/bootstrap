using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.ActivosFijos.Business;
using Xpinn.ActivosFijos.Entities;

namespace Xpinn.ActivosFijos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class RequisicionServices
    {
        private RequisicionBusiness BORequisicion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ActivoFijo
        /// </summary>
        public RequisicionServices()
        {
            BORequisicion = new RequisicionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "50301"; } }

        /// <summary>
        /// Servicio para crear ActivoFijo
        /// </summary>
        /// <param name="pEntity">Entidad ActivoFijo</param>
        /// <returns>Entidad ActivoFijo creada</returns>
        public Requisicion CrearRequisicion(Requisicion vRequisicion, List<Detalle_Requisicion> vRequisicionDet, Usuario pUsuario)
        {
            try
            {
                return BORequisicion.CrearRequisicion(vRequisicion,vRequisicionDet , pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "CrearActivoFijo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ActivoFijo
        /// </summary>
        /// <param name="pActivoFijo">Entidad ActivoFijo</param>
        /// <returns>Entidad ActivoFijo modificada</returns>
        public Requisicion ModificarRequisicion(Requisicion vRequisicion, List<Detalle_Requisicion> vRequisicionDet, Usuario pUsuario)
        {
            try
            {
                return BORequisicion.ModificarRequisicion(vRequisicion, vRequisicionDet, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ModificarActivoFijo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ActivoFijo
        /// </summary>
        /// <param name="pId">identificador de ActivoFijo</param>
        public void EliminarRequisicion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BORequisicion.EliminarRequisicion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarActivoFijo", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ActivoFijo
        /// </summary>
        /// <param name="pId">identificador de ActivoFijo</param>
        /// <returns>Entidad ActivoFijo</returns>
        public Requisicion ConsultarRequisicion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BORequisicion.ConsultarRequisicion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ConsultarActivoFijo", ex);
                return null;
            }
        }

        public List<Requisicion> ListarRequisicion(Requisicion pRequisicion, Usuario pUsuario)
        {
            try
            {
                return BORequisicion.ListarRequisicion(pRequisicion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ListarActivoFijo", ex);
                return null;
            }
        }

       

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BORequisicion.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }

     


    }
}