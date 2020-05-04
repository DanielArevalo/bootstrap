using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
using Xpinn.FabricaCreditos.Entities;
namespace Xpinn.Cartera.Business
{
    public class CambioLineaBusiness : GlobalData
    {
        private CambioLineaData DACredito;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public CambioLineaBusiness()
        {
            DACredito = new CambioLineaData();
        }

        /// <summary>
        /// Obtiene la lista de creditos 
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de creditos obtenidos</returns>
        public List<Credito> ListarCreditos(Usuario pUsuario, String filtro)
        {
            try
            {
                return DACredito.ListarCredito(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioLineaBusiness", "ListarCreditos", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene los datos de un Credito
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Creditos obtenidos</returns>
        public Credito ConsultarCredito(Xpinn.FabricaCreditos.Entities.Credito pEntidad, Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarCredito(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioLineaBusiness", "ConsultarCredito", ex);
                return null;
            }
        }

        public void CrearCredito(Xpinn.FabricaCreditos.Entities.Credito pLineaCredito, List<Xpinn.FabricaCreditos.Entities.CreditoRecoger> ListRecoge, ref Int64 numero_radicacion, ref string error, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    Xpinn.FabricaCreditos.Data.CreditoRecogerData DACreditoRecoger = new Xpinn.FabricaCreditos.Data.CreditoRecogerData();
                    Xpinn.Cartera.Data.CambioLineaData DACartera = new CambioLineaData();


                    // Crear el crédito
                    numero_radicacion = DACredito.InsertSolicitudCredito(pUsuario, pLineaCredito);
                    // Realizar la liquidación del crédito

                    DACartera.LiquidarCredito(numero_radicacion, ref error, pUsuario);
                  
                    // Insertar los créditos que se recogen
                    foreach (Xpinn.FabricaCreditos.Entities.CreditoRecoger cRecoger in ListRecoge)
                    {
                        cRecoger.numero_radicacion = numero_radicacion;
                        DACreditoRecoger.CrearCreditoRecoger(cRecoger, pUsuario);
                    }
                    Xpinn.FabricaCreditos.Data.DocumentoData DADocumentos = new Xpinn.FabricaCreditos.Data.DocumentoData();
                    // Insertar los documentos garantias               
                       
                    if (pLineaCredito.lstDocumentos != null)
                    {
                        foreach (Xpinn.FabricaCreditos.Entities.Documento cDocumentos in pLineaCredito.lstDocumentos)
                        {
                            cDocumentos.numero_radicacion = numero_radicacion;
                            cDocumentos.referencia = "";
                            cDocumentos.ruta = "";                         
                            DADocumentos.CrearDocumentoGenerado(cDocumentos,numero_radicacion, pUsuario);
                        }
                    }
                    // Insertar los codeudores
                    Xpinn.FabricaCreditos.Data.codeudoresData DACodeudores = new Xpinn.FabricaCreditos.Data.codeudoresData();

                    if (pLineaCredito.lstCodeudores != null)
                    {
                        foreach (Xpinn.FabricaCreditos.Entities.codeudores cCodeudores in pLineaCredito.lstCodeudores)
                        {
                            cCodeudores.numero_radicacion = numero_radicacion;
                            DACodeudores.CrearCodeudoresCredito(cCodeudores, pUsuario);
                        }
                    }
                    ts.Complete();


                }
            }
            catch (Exception ex)
            {

                BOExcepcion.Throw("CambioLineaBusiness", "CrearCredito", ex);

            }

        }

        public Credito DesembolsarCredito(Credito pCredito, bool opcion, ref string Error, Usuario pUsuario)
        {
            try
            {
                decimal pMontoDesembolso = 0;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    // Desembolsar el crédito
                    pCredito.cod_ope = DACredito.DesembolsarCredito(pCredito, ref pMontoDesembolso, pUsuario);
                    ts.Complete();
                }

                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioLineaBusiness", "DesembolsarCredito", ex);
                return null;
            }
        }


        // <summary>
        /// Obtiene la lista de documentos 
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de documentos obtenidos</returns>
        public List<Documento> ListarDocumentosGarantia(Usuario pUsuario, Int64 credito)
        {
            try
            {
                return DACredito.ListarDocumentosGarantia(pUsuario, credito);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioLineaBusiness", "ListarDocumentosGarantia", ex);
                return null;
            }
        }


    }
}
