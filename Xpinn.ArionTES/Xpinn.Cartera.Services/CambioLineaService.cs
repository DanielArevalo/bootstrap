using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Cartera.Business;
using Xpinn.Cartera.Entities;
using Xpinn.Util;
using System.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Cartera.Services
{
    /// <summary>
    /// Servicio para Aprobador
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CambioLineaService
    {
        private CambioLineaBusiness BOCredito;
        private ExcepcionBusiness BOExcepcion;


        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public CambioLineaService()
        {
            BOCredito = new CambioLineaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "60115"; } }


        /// <summary>
        /// Obtiene la lista de creditos para cambio de linea 
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de creditos obtenidos</returns>
        public List<Credito> ListarCreditos(Usuario pUsuario, String filtro)
        {
            try
            {
                return BOCredito.ListarCreditos(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioLineaService", "ListarCreditos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de creditos para cambio de linea 
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de creditos obtenidos</returns>
        public Credito ConsultarCredito(Xpinn.FabricaCreditos.Entities.Credito pEntidad, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCredito(pEntidad,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioLineaService", "ConsultarCredito", ex);
                return null;
            }
        }


        public void CrearCredito(Credito credito, List<Xpinn.FabricaCreditos.Entities.CreditoRecoger> ListRecoge, ref Int64 numero_radicacion, ref string error, Usuario pusuario)
        {
            try
            {
                BOCredito.CrearCredito(credito, ListRecoge, ref numero_radicacion,ref error, pusuario);
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Realizar el desembolso del crèdito
        /// </summary>
        /// <param name="pCredito"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Credito DesembolsarCredito(Credito pCredito, bool opcion,  ref string Error, Usuario pUsuario)
        {
            Error = "";
            try
            {
                pCredito = BOCredito.DesembolsarCredito(pCredito, opcion, ref Error, pUsuario);
                return pCredito;
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de documentos garantia para cambio de linea 
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de documentos obtenidos</returns>
        public List<Documento> ListarDocumentosGarantia(Usuario pUsuario, Int64 credito)
        {
            try
            {
                return BOCredito.ListarDocumentosGarantia(pUsuario,   credito);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioLineaService", "ListarDocumentosGarantia", ex);
                return null;
            }
        }
    }
}
