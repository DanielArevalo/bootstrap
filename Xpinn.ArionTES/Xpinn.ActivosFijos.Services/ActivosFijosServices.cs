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
    public class ActivosFijoservices
    {
        private ActivosFijosBusiness BOActivoFijo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ActivoFijo
        /// </summary>
        public ActivosFijoservices()
        {
            BOActivoFijo = new ActivosFijosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "50101"; } }
        public string CodigoProgramaDepre { get { return "50102"; } }
        public string CodigoProgramaVenta { get { return "50103"; } }
        public string CodigoProgramaBaja { get { return "50104"; } }
        public string CodigoProgramaMantenimientoNif { get { return "50105"; } }
        public string CodigoProgramaDeterioroNif { get { return "210102"; } }
        public string CodigoProgramaReporteActivos { get { return "50106"; } }

        public string CodigoProgramaReporteCierre { get { return "50108"; } }

        

        /// <summary>
        /// Servicio para crear ActivoFijo
        /// </summary>
        /// <param name="pEntity">Entidad ActivoFijo</param>
        /// <returns>Entidad ActivoFijo creada</returns>
        public ActivoFijo CrearActivoFijo(ActivoFijo vActivoFijo, Usuario pUsuario)
        {
            try
            {
                return BOActivoFijo.CrearActivosFijos(vActivoFijo, pUsuario);
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
        public ActivoFijo ModificarActivoFijo(ActivoFijo vActivoFijo, Usuario pUsuario)
        {
            try
            {
                return BOActivoFijo.ModificarActivosFijos(vActivoFijo, pUsuario);
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
        public void EliminarActivoFijo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOActivoFijo.EliminarActivosFijos(pId, pUsuario);
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
        public ActivoFijo ConsultarActivoFijo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOActivoFijo.ConsultarActivosFijos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ConsultarActivoFijo", ex);
                return null;
            }
        }

        public List<ActivoFijo> ListarActivoFijo(ActivoFijo pActivoFijo, Usuario pUsuario)
        {
            try
            {
                return BOActivoFijo.ListarActivosFijos(pActivoFijo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ListarActivoFijo", ex);
                return null;
            }
        }

        public List<ActivoFijo> ListarTipoActivoFijo( Usuario pUsuario)
        {
            try
            {
                return BOActivoFijo.ListarTipoActivoFijo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ListarTipoActivoFijo", ex);
                return null;
            }
        }
        

        public List<ActivoFijo> ListarActivoFijoDepre(DateTime pFechaDepreciacion, ActivoFijo pActivoFijo, Usuario pUsuario)
        {
            try
            {
                return BOActivoFijo.ListarActivoFijoDepre(pFechaDepreciacion, pActivoFijo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ListarActivoFijoDepre", ex);
                return null;
            }
        }





        public List<ActivoFijo> ListarActivoDeterioroNif(DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return BOActivoFijo.ListarActivoDeterioroNif(pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ListarActivoDeterioroNif", ex);
                return null;
            }
        }

        public List<ActivoFijo> ListarActivoFijoReporteCierre(DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return BOActivoFijo.ListarActivoFijoReporteCierre(pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ListarActivoFijoReporteCierre", ex);
                return null;
            }
        }


        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOActivoFijo.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }

        public Boolean DepreciarActivosFijos(DateTime pFechaDepreciacion, List<ActivoFijo> LstActivosFijos, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string Error, ref Int64 pCodOpe, Usuario pUsuario)
        {
            try
            {
                BOActivoFijo.DepreciarActivosFijos(pFechaDepreciacion, LstActivosFijos, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref Error, ref pCodOpe, pUsuario);
                return true;
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return false;
            }
        }


        public Boolean BajaActivoFijo(ActivoFijo pActivosFijos, ref Int64 pCodOpe, ref String Error, Usuario pUsuario)
        {
            try
            {
                return BOActivoFijo.BajaActivoFijo(pActivosFijos, ref pCodOpe, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "BajaActivoFijo", ex);
                return false;
            }
        }

        public Boolean VentaActivoFijo(ActivoFijo pActivosFijos, ref Int64 pCodOpe, ref String Error, Usuario pUsuario)
        {
            try
            {
                return BOActivoFijo.VentaActivoFijo(pActivosFijos, ref pCodOpe, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "VentaActivoFijo", ex);
                return false;
            }
        }


        public ActivoFijo ModificarMantenimientoNif(ActivoFijo vActivoFijo, Usuario pUsuario)
        {
            try
            {
                return BOActivoFijo.ModificarMantenimientoNif(vActivoFijo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ModificarMantenimientoNif", ex);
                return null;
            }
        }


        public void CrearDeterioroNIF(DateTime pFechaDepre, List<ActivoFijo> plstActivos, ref Int64 pCodOpe, ref String pError, Usuario pUsuario)
        {
            try
            {
                BOActivoFijo.CrearDeterioroNIF(pFechaDepre,plstActivos,ref pCodOpe, ref pError, pUsuario);               
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "CrearDeterioroNIF", ex);
              
            }
        }


        public Int64 CrearCOMPRA_ACTIVO(ActivoFijo pCOMPRA_ACTIVO, Usuario pusuario)
        {
            try
            {
                pCOMPRA_ACTIVO.cod_ope = BOActivoFijo.CrearCOMPRA_ACTIVO(pCOMPRA_ACTIVO, pusuario);
                return pCOMPRA_ACTIVO.cod_ope;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("COMPRA_ACTIVOService", "CrearCOMPRA_ACTIVO", ex);
                return 0;
            }
        }


        public string ValidarDeterioroNiif(DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return BOActivoFijo.ValidarDeterioroNiif(pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijoservice", "ValidarDeterioroNiif", ex);
                return null;
            }
        }


        public ActivoFijo ConsultarCierreActivosFijos(Usuario vUsuario)
        {
            try
            {
                return BOActivoFijo.ConsultarCierreActivosFijos(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ConsultarCierreActivosFijos", ex);
                return null;
            }
        }


    }
}