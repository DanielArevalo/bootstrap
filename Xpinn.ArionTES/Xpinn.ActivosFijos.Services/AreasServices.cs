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
    public class Areasservices
    {
        private AreasBusiness BOAreas;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ActivoFijo
        /// </summary>
        public Areasservices()
        {
            BOAreas = new AreasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "50205"; } }

        /// <summary>
        /// Servicio para crear ActivoFijo
        /// </summary>
        /// <param name="pEntity">Entidad ActivoFijo</param>
        /// <returns>Entidad ActivoFijo creada</returns>
        public Areas CrearArea(Areas vAreas, Usuario pUsuario)
        {
            try
            {
                return BOAreas.CrearAreas(vAreas, pUsuario);
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
        public Areas ModificarArea(Areas vAreas, Usuario pUsuario)
        {
            try
            {
                return BOAreas.ModificarAreas(vAreas, pUsuario);
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
        public void EliminarAreas(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOAreas.EliminarAreas(pId, pUsuario);
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
        public Areas ConsultarAreas(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAreas.ConsultarAreas(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ConsultarActivoFijo", ex);
                return null;
            }
        }

        public List<Areas> ListarAreas(Areas pAreas, Usuario pUsuario)
        {
            try
            {
                return BOAreas.ListarAreas(pAreas, pUsuario);
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
                return BOAreas .ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }

     


    }
}