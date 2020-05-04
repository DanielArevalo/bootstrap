using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Business;

namespace Xpinn.FabricaCreditos.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DocumentosRequeridosService
    {

        private DocumentosRequeridosBusiness BODocumentosRequeridos;
        private ExcepcionBusiness BOExcepcion;

        public DocumentosRequeridosService()
        {
            BODocumentosRequeridos = new DocumentosRequeridosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100159"; } }        


        public documentosrequeridos CrearDocumentosRequeridos(documentosrequeridos pDocumentosRequeridos, Usuario pusuario)
        {
            try
            {
                pDocumentosRequeridos = BODocumentosRequeridos.CrearDocumentosRequeridos(pDocumentosRequeridos, pusuario);
                return pDocumentosRequeridos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosRequeridosService", "CrearDocumentosRequeridos", ex);
                return null;
            }
        }


        public documentosrequeridos ModificarDocumentosRequeridos(documentosrequeridos pDocumentosRequeridos, Usuario pusuario)
        {
            try
            {
                pDocumentosRequeridos = BODocumentosRequeridos.ModificarDocumentosRequeridos(pDocumentosRequeridos, pusuario);
                return pDocumentosRequeridos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosRequeridosService", "ModificarDocumentosRequeridos", ex);
                return null;
            }
        }


        public void EliminarDocumentosRequeridos(Int64 pId, Usuario pusuario)
        {
            try
            {
                BODocumentosRequeridos.EliminarDOCUMENTOSREQUERIDOS(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosRequeridosService", "EliminarDocumentosRequeridos", ex);
            }
        }


        public documentosrequeridos ConsultarDocumentosRequeridos(Int64 pId, Usuario pusuario)
        {
            try
            {
                documentosrequeridos DocumentosRequeridos = new documentosrequeridos();
                DocumentosRequeridos = BODocumentosRequeridos.ConsultarDOCUMENTOSREQUERIDOS(pId, pusuario);
                return DocumentosRequeridos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosRequeridosService", "ConsultarDocumentosRequeridos", ex);
                return null;
            }
        }


        public List<documentosrequeridos> ListarDocumentosRequeridos(documentosrequeridos pDocumentosRequeridos, Usuario pusuario)
        {
            try
            {
                return BODocumentosRequeridos.ListarDocumentosRequeridos(pDocumentosRequeridos, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosRequeridosService", "ListarDocumentosRequeridos", ex);
                return null;
            }
        }

        public List<documentosrequeridos> ListarDocumentosCredito(string radicado, Usuario pusuario)
        {
            try
            {
                return BODocumentosRequeridos.ListarDocumentosCredito(radicado, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DocumentosRequeridosService", "ListarDocumentosCredito", ex);
                return null;
            }
        }        
    }
}