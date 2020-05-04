using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.NIIF.Business;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>


    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EstadosFinancierosNIIFService
    {
        private EstadosFinancierosNIIFBusiness BOEstadosFinancierosNIIF;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para EstadosFinancierosNIIF
        /// </summary>
        public EstadosFinancierosNIIFService()
        {
            BOEstadosFinancierosNIIF = new EstadosFinancierosNIIFBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        public string CodigoPrograma { get { return "210308"; } }        
       public bool CrearConceptosNIF(List<EstadosFinancierosNIIF> lstEstadosFinancierosNIIF, Usuario vUsuario)
        {
            try
            {
                BOEstadosFinancierosNIIF.CrearConceptosNIF(lstEstadosFinancierosNIIF, vUsuario);
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFService", "CrearConceptosNIF", ex);
                return false;
            }
        }
       public EstadosFinancierosNIIF ModificarConceptosNIF(EstadosFinancierosNIIF pEstadosFinancieros, Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancierosNIIF.ModificarConceptosNIF(pEstadosFinancieros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFService", "ModificarConceptosNIF", ex);
                return null;
            }
        }
       public EstadosFinancierosNIIF CrearConceptosNIIF(EstadosFinancierosNIIF pEstadosFinancieros, Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancierosNIIF.CrearConceptosNIIF(pEstadosFinancieros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFService", "CrearConceptosNIIF", ex);
                return null;
            }
        }
       public void EliminarConceptosNIF(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOEstadosFinancierosNIIF.EliminarConceptosNIF(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarConceptosNIF", ex);
            }
        }

        public void EliminarCuentasConceptosNIF(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOEstadosFinancierosNIIF.EliminarCuentasConceptosNIF(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarCuentasConceptosNIF", ex);
            }
        }

        public EstadosFinancierosNIIF ConsultarConceptosNIF(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancierosNIIF.ConsultarConceptosNIF(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFService", "ConsultarConceptosNIF", ex);
                return null;
            }
        }
       public List<EstadosFinancierosNIIF> ConsultarCuentasNIIF(Int32 nivel,Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancierosNIIF.ConsultarCuentasNIIF(nivel, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNominaServices", "ConsultarCuentasNIIF", ex);
                return null;
            }
        }

        public List<EstadosFinancierosNIIF> ConsultarDependeDe(Int32 tipo, Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancierosNIIF.ConsultarDependeDe(tipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNominaServices", "ConsultarDependeDe", ex);
                return null;
            }
        }
        public List<EstadosFinancierosNIIF> ConsultarCuentasLocalNIIF(Int32 nivel,Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancierosNIIF.ConsultarCuentasLocalNIIF(nivel,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoNominaServices", "ConsultarCuentasLocalNIIF", ex);
                return null;
            }
        }
       public List<EstadosFinancierosNIIF> ListarCuentasNIIF(Int64 pcodigo, Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancierosNIIF.ListarCuentasNIIF(pcodigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFService", "ListarCuentasNIIF", ex);
                return null;
            }
        }
       public List<EstadosFinancierosNIIF> ListarCuentasLocalNIIF(Int64 pcodigo, Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancierosNIIF.ListarCuentasLocalNIIF(pcodigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFService", "ListarCuentasLocalNIIF", ex);
                return null;
            }
        }
       public List<EstadosFinancierosNIIF> ListarConceptosNIF(Int64 pestadofinanciero, Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancierosNIIF.ListarConceptosNIF(pestadofinanciero, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFService", "ListarConceptosNIF", ex);
                return null;
            }
        }
       public List<EstadosFinancierosNIIF> ListarTipoEstadoFinancieroNIIF(String filtro, Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancierosNIIF.ListarTipoEstadoFinancieroNIIF(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFService", "ListarTipoEstadoFinancieroNIIF", ex);
                return null;
            }
        }

    }
}