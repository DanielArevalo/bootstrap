using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AprobacionComprobantesService
    {
        private ComprobanteBusiness BOComprobante;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para comprobante
        /// </summary>
        public AprobacionComprobantesService()
        {
            BOComprobante = new ComprobanteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public int numero_comp;
        public int tipo_comp;
        public string CodigoPrograma { get { return "30105"; } }

        public Boolean ConsultarComprobante(Int64 pnum_comp, Int64 ptipo_comp, ref Comprobante pComprobante, ref List<DetalleComprobante> pDetalleComprobante, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ConsultarComprobante(pnum_comp, ptipo_comp, ref pComprobante, ref pDetalleComprobante, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionComprobantesService", "ConsultarComprobante", ex);
                return false;
            }
        }
  
        /// <summary>
        /// Servicio para obtener lista de comprobantes a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de comprobantes obtenidos</returns>
        public List<Comprobante> ListarComprobanteParaAprobar(Comprobante pComprobante, Usuario pUsuario, String pfiltro)
        {
            try
            {
                return BOComprobante.ListarComprobanteParaAprobar(pComprobante, pUsuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionComprobantesServices", "ListarComprobanteParaAprobar", ex);
                return null;
            }
        }


        public string Consultafecha(Usuario pUsuario)
        {
            try
            {
                return BOComprobante.Consultafecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionComprobantesServices", "ConsultaUsuario", ex);
                return null;
            }
        }

        public Boolean AprobarAnularComprobante(Comprobante pComprobante, ref string Error, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.AprobarAnularComprobante(pComprobante, ref Error, pUsuario);
            }
            catch 
            {
                //BOExcepcion.Throw("AprobacionComprobantesServices", "AprobarAnularComprobante", ex);
                return false;
            }
        }



        ///AGREGADO

        public List<Comprobante> ListarComprobanteTipoMotivoAnulacion(Comprobante pComprobante, Usuario pusuario)
        {
            try
            {
                return BOComprobante.ListarComprobanteTipoMotivoAnulacion(pComprobante, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ListarComprobante", ex);
                return null;
            }
        }


        public Comprobante ConsultarComprobanteTipoMotivoAnulacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                Comprobante Comprobante = new Comprobante();
                Comprobante = BOComprobante.ConsultarComprobanteTipoMotivoAnulacion(pId, pusuario);
                return Comprobante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultarComprobante", ex);
                return null;
            }
        }


    }
}