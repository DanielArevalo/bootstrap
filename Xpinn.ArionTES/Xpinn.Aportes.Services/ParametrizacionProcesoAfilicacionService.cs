using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Aportes.Business;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ParametrizacionProcesoAfilicacionService
    {
        private ParametrizacionProcesoAfiliacionBussines BOprocesoAfiliacion;
        private ExcepcionBusiness BOExcepcion;

        public int Codigoaporte;
        /// <summary>
        /// Constructor del servicio para Aporte
        /// </summary>
        public ParametrizacionProcesoAfilicacionService()
        {
            BOprocesoAfiliacion = new ParametrizacionProcesoAfiliacionBussines();
            BOExcepcion = new ExcepcionBusiness();
        }
        public string CodigoPrograma { get { return "170208"; } }
        public string CodigoProgramaH { get { return "170135"; } }
        public List<ParametrizacionProcesoAfiliacion> ListarParametrosProcesoAfiliacion(Usuario pUsuario)
        {
            try
            {
                return BOprocesoAfiliacion.ListarParametrosProcesoAfiliacion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrizacionProcesoAfilicacionService", "ListarParametrosProcesoAfiliacion", ex);
                return null;
            }
        }
        public List<ParametrizacionProcesoAfiliacion> ListarDetalleRuta(string iden, Usuario pUsuario)
        {
            try
            {
                return BOprocesoAfiliacion.ListarDetalleRuta(iden, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrizacionProcesoAfilicacionService", "ListarDetalleRuta", ex);
                return null;
            }
        }
        public List<ParametrizacionProcesoAfiliacion> ListarHistorialRuta(string iden, Usuario pUsuario)
        {
            try
            {
                return BOprocesoAfiliacion.ListarHistorialRuta(iden, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrizacionProcesoAfilicacionService", "ListarHistorialRuta", ex);
                return null;
            }
        }
        public ParametrizacionProcesoAfiliacion validarProcesoAnterior(string cod_per, Int32 proceso, Usuario pUsuario)
        {
            try
            {
                return BOprocesoAfiliacion.validarProcesoAnterior(cod_per, proceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrizacionProcesoAfilicacionService", "ListarParametrosProcesoAfiliacion", ex);
                return null;
            }
        }
        public bool controlRegistrado(Int32 cod_proceso, Int64 cod_per, Usuario pUsuario)
        {
            try
            {
                return BOprocesoAfiliacion.controlRegistrado(cod_proceso, cod_per, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrizacionProcesoAfilicacionService", "controlRegistrado", ex);
                return false;
            }
        }
        public void cambiarEstadoAsociado(string estado, Int64 cod_per, Usuario pUsuario)
        {
            try
            {
                BOprocesoAfiliacion.cambiarEstadoAsociado(estado, cod_per, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrizacionProcesoAfilicacionService", "cambiarEstadoAsociado", ex);
            }
        }
        public ParametrizacionProcesoAfiliacion ModificarParametros(ParametrizacionProcesoAfiliacion lstParam, Usuario pUsuario)
        {
            try
            {
                return BOprocesoAfiliacion.ModificarParametros(lstParam, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrizacionProcesoAfilicacionService", "ModificarParametros", ex);
                return null;
            }
        }

    }
}
