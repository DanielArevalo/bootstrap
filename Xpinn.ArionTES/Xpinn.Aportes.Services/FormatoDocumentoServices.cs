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
    public class FormatoDocumentoServices
    {

        private FormatoDocumentoBusiness BOFormato;
        private ExcepcionBusiness BOExcepcion;

        public int Codigoaporte;
        /// <summary>
        /// Constructor del servicio para Aporte
        /// </summary>
        public FormatoDocumentoServices()
        {
            BOFormato = new FormatoDocumentoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170207"; } }


        public FormatoDocumento CrearFormatoDocumentos(FormatoDocumento pFormatoDocumento, Usuario vUsuario, int pOpcion)
        {
            try
            {
                return BOFormato.CrearFormatoDocumentos(pFormatoDocumento, vUsuario, pOpcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormatoDocumentoServices", "CrearFormatoDocumentos", ex);
                return null;
            }
        }

        public List<FormatoDocumento> ListarFormatoDocumento(FormatoDocumento pFormatoDocumento, Usuario vUsuario)
        {
            try
            {
                return BOFormato.ListarFormatoDocumento(pFormatoDocumento, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormatoDocumentoServices", "ListarFormatoDocumento", ex);
                return null;
            }
        }

        public FormatoDocumento ConsultarFormatoDocumento(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOFormato.ConsultarFormatoDocumento(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormatoDocumentoBusiness", "ConsultarFormatoDocumento", ex);
                return null;
            }
        }

        public List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> ListarDatosDeDocumento(Int64 pVariable, string pNombre_pl, Usuario vUsuario)
        {
            try
            {
                return BOFormato.ListarDatosDeDocumento(pVariable, pNombre_pl, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormatoDocumentoServices", "ListarDatosDeDocumento", ex);
                return null;
            }
        }

        public List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> ListarDatosDeDocumentoOtros(Int64 pVariable, string pNombre_pl, Usuario vUsuario, string origen)
        {
            try
            {
                return BOFormato.ListarDatosDeDocumentoOtros(pVariable, pNombre_pl, vUsuario, origen);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormatoDocumentoServices", "ListarDatosDeDocumento", ex);
                return null;
            }
        }

        public void EliminarFormatoDocumento(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOFormato.EliminarFormatoDocumento(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormatoDocumentoServices", "EliminarFormatoDocumento", ex);
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario vUsuario)
        {
            try
            {
                return BOFormato.ObtenerSiguienteCodigo(vUsuario);
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public string ObtenerDocumento(Int64 id_formato, string pVariable, Usuario vUsuario)
        {
            try
            {
                return BOFormato.ObtenerDocumento(id_formato, pVariable, vUsuario);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public string ObtenerDocumentootros(Int64 id_formato, string pVariable, Usuario vUsuario,string origen)
        {
            try
            {
                return BOFormato.ObtenerDocumentootros(id_formato, pVariable, vUsuario,origen);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string ObtenerDocumentoCDAT(Int64 id_formato, string apertura, string cierre, Usuario vUsuario)
        {
            try
            {
                return BOFormato.ObtenerDocumentoCDAT(id_formato, apertura, cierre, vUsuario);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}