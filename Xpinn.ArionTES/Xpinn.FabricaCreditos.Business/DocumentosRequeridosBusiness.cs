using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;
 
namespace Xpinn.FabricaCreditos.Business
{
 
        public class DocumentosRequeridosBusiness : GlobalBusiness
        {
 
            private DocumentosRequeridosData DADOCUMENTOSREQUERIDOS;
 
            public DocumentosRequeridosBusiness()
            {
                DADOCUMENTOSREQUERIDOS = new DocumentosRequeridosData();
            }
 
            public documentosrequeridos CrearDocumentosRequeridos(documentosrequeridos pDOCUMENTOSREQUERIDOS, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pDOCUMENTOSREQUERIDOS = DADOCUMENTOSREQUERIDOS.CrearDOCUMENTOSREQUERIDOS(pDOCUMENTOSREQUERIDOS, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pDOCUMENTOSREQUERIDOS;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("DOCUMENTOSREQUERIDOSBusiness", "CrearDOCUMENTOSREQUERIDOS", ex);
                    return null;
                }
            }


        


            public documentosrequeridos ModificarDocumentosRequeridos(documentosrequeridos pDOCUMENTOSREQUERIDOS, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pDOCUMENTOSREQUERIDOS = DADOCUMENTOSREQUERIDOS.ModificarDOCUMENTOSREQUERIDOS(pDOCUMENTOSREQUERIDOS, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pDOCUMENTOSREQUERIDOS;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("DOCUMENTOSREQUERIDOSBusiness", "ModificarDOCUMENTOSREQUERIDOS", ex);
                    return null;
                }
            }
 
 
            public void EliminarDOCUMENTOSREQUERIDOS(Int64 pId, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        DADOCUMENTOSREQUERIDOS.EliminarDOCUMENTOSREQUERIDOS(pId, pusuario);
 
                        ts.Complete();
 
                    }
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("DOCUMENTOSREQUERIDOSBusiness", "EliminarDOCUMENTOSREQUERIDOS", ex);
                }
            }
 
 
            public documentosrequeridos ConsultarDOCUMENTOSREQUERIDOS(Int64 pId, Usuario pusuario)
            {
                try
                {
                    documentosrequeridos documentosrequeridos = new documentosrequeridos();
                    documentosrequeridos = DADOCUMENTOSREQUERIDOS.ConsultarDOCUMENTOSREQUERIDOS(pId, pusuario);
                    return documentosrequeridos;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("DOCUMENTOSREQUERIDOSBusiness", "ConsultarDOCUMENTOSREQUERIDOS", ex);
                    return null;
                }
            }
 
 
            public List<documentosrequeridos> ListarDocumentosRequeridos(documentosrequeridos pDocumentosRequeridos, Usuario pusuario)
            {
                try
                {
                    return DADOCUMENTOSREQUERIDOS.ListarDocumentosRequeridos(pDocumentosRequeridos,pusuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("DOCUMENTOSREQUERIDOSBusiness", "ListarDocumentosRequeridos", ex);
                    return null;
                }
            }
        
            public List<documentosrequeridos> ListarDocumentosCredito(string radicado, Usuario pusuario)
            {
                try
                {
                    return DADOCUMENTOSREQUERIDOS.ListarDocumentosCredito(radicado, pusuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("DOCUMENTOSREQUERIDOSBusiness", "ListarDocumentosCredito", ex);
                    return null;
                }
            }


    }
}