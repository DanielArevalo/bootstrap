using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para LineasCredito
    /// </summary>
    public class LineasCreditoBusiness : GlobalBusiness
    {
        private LineasCreditoData DALineasCredito;

        /// <summary>
        /// Constructor del objeto de negocio para LineasCredito
        /// </summary>
        public LineasCreditoBusiness()
        {
            DALineasCredito = new LineasCreditoData();
        }

        /// <summary>
        /// Crea un LineasCredito
        /// </summary>
        /// <param name="pLineasCredito">Entidad LineasCredito</param>
        /// <returns>Entidad LineasCredito creada</returns>
        public LineasCredito CrearLineasCredito(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineasCredito = DALineasCredito.CrearLineasCredito(pLineasCredito, pUsuario);
                    string cod = pLineasCredito.cod_linea_credito;

                    if (pLineasCredito.lstDocumentos != null && pLineasCredito.lstDocumentos.Count > 0)
                    {
                        foreach (LineasCredito nLineas in pLineasCredito.lstDocumentos)
                        {
                            LineasCredito nDocument = new LineasCredito();
                            nLineas.cod_linea_credito = cod;
                            nDocument = DALineasCredito.CrearLineasDocumentos(nLineas, pUsuario);
                        }
                    }

                    if (pLineasCredito.lstGarantiaDoc != null && pLineasCredito.lstGarantiaDoc.Count > 0)
                    {
                        foreach (LineasCredito nGaran in pLineasCredito.lstGarantiaDoc)
                        {
                            LineasCredito nDocument = new LineasCredito();
                            nGaran.cod_linea_credito = cod;
                            nDocument = DALineasCredito.CrearGarantiaLineaDocumento(nGaran, pUsuario);
                        }
                    }

                    if (pLineasCredito.lstProcesoLinea != null && pLineasCredito.lstProcesoLinea.Count > 0)
                    {
                        foreach (ProcesoLineaCredito rProc in pLineasCredito.lstProcesoLinea)
                        {
                            ProcesoLineaCredito nProceso = new ProcesoLineaCredito();
                            rProc.cod_lineacredito = cod;
                            nProceso = DALineasCredito.CrearProcesoLinea(rProc, pUsuario);
                        }
                    }

                    if (pLineasCredito.lstPrioridad != null && pLineasCredito.lstPrioridad.Count > 0)
                    {
                        foreach (LineasCredito nPriori in pLineasCredito.lstPrioridad)
                        {
                            LineasCredito nPrioridad = new LineasCredito();
                            nPriori.cod_linea_credito = cod;
                            nPrioridad = DALineasCredito.CrearPrioridad_Linea(nPriori, pUsuario);
                        }
                    }

                    if (pLineasCredito.lstdestinacion != null && pLineasCredito.lstdestinacion.Count > 0)
                    {
                        foreach (LineasCredito nDest in pLineasCredito.lstdestinacion)
                        {
                            LineasCredito nDestino = new LineasCredito();
                            nDest.cod_linea_credito = cod;
                            nDestino = DALineasCredito.CrearDestino_Linea(nDest, pUsuario);
                        }
                    }



                    ts.Complete();
                }

                return pLineasCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "CrearLineasCredito", ex);
                return null;
            }
        }



        public bool ConsultarLineasCreditosActivasPorClasificacion(string cod_clasificacion, Usuario usuario)
        {
            try
            {
                return DALineasCredito.ConsultarLineasCreditosActivasPorClasificacion(cod_clasificacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "ConsultarLineasCreditosActivasPorClasificacion", ex);
                return false;
            }
        }

        public List<LineasCredito> ConsultarGarantiasPorCredito(int pCreditoId, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarGarantiasPorCredito(pCreditoId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "ConsultarGarantiasPorCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un LineasCredito
        /// </summary>
        /// <param name="pLineasCredito">Entidad LineasCredito</param>
        /// <returns>Entidad LineasCredito modificada</returns>
        public LineasCredito ModificarLineasCredito(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineasCredito = DALineasCredito.ModificarLineasCredito(pLineasCredito, pUsuario);
                    string cod = pLineasCredito.cod_linea_credito;
                    int codigo = Convert.ToInt32(pLineasCredito.cod_empresa);

                    if (pLineasCredito.lstDocumentos != null && pLineasCredito.lstDocumentos.Count > 0)
                    {
                        DALineasCredito.EliminarDocumentosRequerido(cod, pUsuario);
                        foreach (LineasCredito nLineas in pLineasCredito.lstDocumentos)
                        {
                            LineasCredito nDocument = new LineasCredito();
                            nLineas.cod_linea_credito = cod;
                            if (nLineas.cod_linea_credito != "")
                                nDocument = DALineasCredito.CrearLineasDocumentos(nLineas, pUsuario);
                        }
                    }

                    if (pLineasCredito.lstGarantiaDoc != null && pLineasCredito.lstGarantiaDoc.Count > 0)
                    {
                        DALineasCredito.EliminarDocumentosGarantia(cod, pUsuario);
                        foreach (LineasCredito nGaran in pLineasCredito.lstGarantiaDoc)
                        {
                            LineasCredito nDocument = new LineasCredito();


                            nGaran.cod_linea_credito = cod;

                            if (nGaran.cod_linea_credito != "")
                                nDocument = DALineasCredito.CrearGarantiaLineaDocumento(nGaran, pUsuario);
                        }
                    }

                    if (pLineasCredito.lstProcesoLinea != null && pLineasCredito.lstProcesoLinea.Count > 0)
                    {
                        DALineasCredito.EliminarProcesoLinea(cod, pUsuario);
                        foreach (ProcesoLineaCredito rProc in pLineasCredito.lstProcesoLinea)
                        {
                            ProcesoLineaCredito nProceso = new ProcesoLineaCredito();
                            rProc.cod_lineacredito = cod;
                            if (rProc.cod_lineacredito != "")
                                nProceso = DALineasCredito.CrearProcesoLinea(rProc, pUsuario);
                        }
                    }

                    if (pLineasCredito.lstPrioridad != null)
                    {
                        DALineasCredito.EliminarPrioridad_Linea(cod, pUsuario);
                        foreach (LineasCredito nPriori in pLineasCredito.lstPrioridad)
                        {
                            LineasCredito nPrioridad = new LineasCredito();
                            nPriori.cod_linea_credito = cod;
                            nPrioridad = DALineasCredito.CrearPrioridad_Linea(nPriori, pUsuario);
                        }
                    }

                    DALineasCredito.EliminarDestinacion_Linea(cod, pUsuario);
                    if (pLineasCredito.lstdestinacion != null && pLineasCredito.lstdestinacion.Count > 0)
                    {
                        foreach (LineasCredito nDest in pLineasCredito.lstdestinacion)
                        {
                            LineasCredito nDestino = new LineasCredito();
                            nDest.cod_linea_credito = cod;
                            nDestino = DALineasCredito.CrearDestino_Linea(nDest, pUsuario);
                        }
                    }

                    if (pLineasCredito.LstParametrosLinea != null && pLineasCredito.LstParametrosLinea.Count > 0)
                    {
                        foreach (ProcesoLineaCredito item in pLineasCredito.LstParametrosLinea)
                        {
                            DALineasCredito.CrearParametroLinea(item, pUsuario);
                        }
                    }

                    ts.Complete();
                }
                return pLineasCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "ModificarLineasCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un LineasCredito
        /// </summary>
        /// <param name="pId">Identificador de LineasCredito</param>
        public void EliminarLineasCredito(string pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALineasCredito.EliminarLineasCredito(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "EliminarLineasCredito", ex);
            }
        }

        /// <summary>
        /// Obtiene un LineasCredito
        /// </summary>
        /// <param name="pId">Identificador de LineasCredito</param>
        /// <returns>Entidad LineasCredito</returns>
        public LineasCredito ConsultarLineasCredito(string pId, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarLineasCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "ConsultarLineasCredito", ex);
                return null;
            }
        }

        public long? ConsultarNumeroCodeudoresXLinea(string pId, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarNumeroCodeudoresXLinea(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "ConsultarNumeroCodeudoresXLinea", ex);
                return null;
            }
        }

        public LineasCredito ConsultarTasaInteresLineaCredito(string pId, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarTasaInteresLineaCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "ConsultarTasaInteresLineaCredito", ex);
                return null;
            }
        }


        public LineasCredito ConsultarTasaInteresLineaCredito(string pCodLinea, Int64 pCodPersona, decimal pMonto, decimal pPlazo, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarTasaInteresLineaCredito(pCodLinea, pCodPersona, pMonto, pPlazo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "ConsultarTasaInteresLineaCredito", ex);
                return null;
            }
        }

        public decimal obtenerTasaInteresEspecifica(string cod_linea_credito, int plazo, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.obtenerTasaInteresEspecifica(cod_linea_credito, plazo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "obtenerTasaInteresEspecifica", ex);
                return 0;
            }
        }

        public List<LineasCredito> ListarLineasCreditoTasaInteres(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ListarLineasCreditoTasaInteres(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "ListarLineasCreditoTasaInteres", ex);
                return null;
            }
        }

        public LineasCredito ConsultarLineasCreditoRotativo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarLineasCreditoRotativo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "ConsultarLineasCreditoRotativo", ex);
                return null;
            }
        }
        public List<LineasCredito> ddlliquidacion(Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ddlliquidacion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }

        public List<LineasCredito> ddlatributo(Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ddlatributo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }

        public List<LineasCredito> ddlimpuestos(Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ddlimpuestos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }

        public List<LineasCredito> ConsultarLineasCreditoatributos(string codigo, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarLineasCreditoatributos(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }

        public LineasCredito ConsultarAtributoGeneral(string codigo, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarAtributoGeneral(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "ConsultarAtributoGeneral", ex);
                return null;
            }
        }

        public LineasCredito ConsultarAtributos(string codigo, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarAtributos(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarAtributos", ex);
                return null;
            }
        }
        public List<LineasCredito> ListarDeducciones(int codigo, int atributo, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ListarDeducciones(codigo, atributo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarDeducciones", ex);
                return null;
            }
        }

        public LineasCredito ConsultarDeducciones(string cod_linea, int atributo, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarDeducciones(cod_linea, atributo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarDeducciones", ex);
                return null;
            }
        }

        public List<LineasCredito> ConsultarLineasCrediatributo(string codigo, int rango, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarLineasCrediatributo(codigo, rango, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }
        public List<LineasCredito> ConsultarLineasCrediatributo2(string codigo, Usuario pUsuario, string numradic)
        {
            try
            {
                return DALineasCredito.ConsultarLineasCrediatributo2(codigo, pUsuario, numradic);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }


        public List<RangosTopes> ConsultarLineasCreditopes(string codigo, string atr, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarLineasCreditopes(codigo, atr, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }
        public RangosTopes ConsultarCreditoTopes(int codigo, int tope, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarCreditoTopes(codigo, tope, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarCreditoTopes", ex);
                return null;
            }
        }

        public RangosTopes ConsultarTopestodos(string codigo, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarTopestodos(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarTopestodos", ex);
                return null;
            }
        }


        public LineasCredito CrearAtributosEXPINN(LineasCredito pAtributos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //GRABAR TABLA RANGOATRIBUTOS
                    pAtributos = DALineasCredito.CrearRangosAtributos(pAtributos, pUsuario);
                    string cod = pAtributos.cod_linea_credito;
                    Int64 rang_atr = pAtributos.cod_rango_atr;

                    //GRABAR GRILLA DE TOPES
                    if (pAtributos.lstTopes != null && pAtributos.lstTopes.Count > 0)
                    {
                        foreach (RangosTopes rRang in pAtributos.lstTopes)
                        {
                            RangosTopes nRangosTopes = new RangosTopes();
                            rRang.cod_linea_credito = cod;
                            rRang.cod_rango_atr = rang_atr;
                            nRangosTopes = DALineasCredito.CrearRangosTopes(rRang, pUsuario);
                        }
                    }

                    //GRABAR GRILLA ATRIBUTOS
                    if (pAtributos.lstAtributos != null && pAtributos.lstAtributos.Count > 0)
                    {
                        foreach (LineasCredito eProg in pAtributos.lstAtributos)
                        {
                            LineasCredito nPrograma = new LineasCredito();
                            eProg.cod_linea_credito = Convert.ToString(cod);
                            eProg.cod_rango_atr = rang_atr;
                            nPrograma = DALineasCredito.CrearAtributos(eProg, pUsuario);
                        }
                    }

                    ts.Complete();
                }
                return pAtributos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "CrearAtributosEXPINN", ex);
                return null;
            }
        }


        public LineasCredito ModificarAtributosEXPINN(LineasCredito pAtributos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAtributos = DALineasCredito.ModificarRangosAtributos(pAtributos, pUsuario);
                    string cod = pAtributos.cod_linea_credito;
                    Int64 rang_atr = pAtributos.cod_rango_atr;

                    //MODIFICAR GRILLA DE RANGOTOPES
                    if (pAtributos.lstTopes != null)
                    {
                        foreach (RangosTopes eProg in pAtributos.lstTopes)
                        {
                            RangosTopes nPrograma1 = new RangosTopes();
                            eProg.cod_linea_credito = cod;
                            eProg.cod_rango_atr = rang_atr;

                            if (eProg.idtope > 0)
                                nPrograma1 = DALineasCredito.ModificarRangosTopes(eProg, pUsuario);
                            else
                                nPrograma1 = DALineasCredito.CrearRangosTopes(eProg, pUsuario);
                        }
                    }

                    //MODIFICAR GRILLA DE ATRIBUTOS
                    if (pAtributos.lstAtributos != null)
                    {
                        DALineasCredito.EliminarALLAtributosXlinea(pAtributos, pUsuario);
                        foreach (LineasCredito eProg in pAtributos.lstAtributos)
                        {
                            LineasCredito nPrograma1 = new LineasCredito();
                            eProg.cod_linea_credito = cod;
                            eProg.cod_rango_atr = rang_atr;

                            nPrograma1 = DALineasCredito.CrearAtributos(eProg, pUsuario);
                        }
                    }

                    ts.Complete();
                }
                return pAtributos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "CrearAtributosEXPINN", ex);
                return null;
            }
        }



        public void EliminarAtributos(LineasCredito pAtributos, Usuario vUsuario)
        {
            try
            {
                DALineasCredito.EliminarAtributos(pAtributos, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "EliminarAtributos", ex);
            }
        }

        public void EliminarTopes(RangosTopes pRangos, Usuario vUsuario)
        {
            try
            {
                DALineasCredito.EliminarTopes(pRangos, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "EliminarTopes", ex);
            }
        }


        public void Creardeducciones(LineasCredito entidad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALineasCredito.Creardeducciones(entidad, pUsuario);

                    ts.Complete();
                }

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);

            }

        }

        public void modificardeducciones(LineasCredito entidad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALineasCredito.modificardeducciones(entidad, pUsuario);

                    ts.Complete();
                }

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "modificardeducciones", ex);

            }
        }




        public List<LineasCredito> ConsultarLineasCreditodeducciones(string codigo, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarLineasCreditodeducciones(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pLineasCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de LineasCredito obtenidos</returns>
        public List<LineasCredito> ListarLineasCredito(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ListarLineasCredito(pLineasCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "ListarLineasCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar todas las lineas de crédito
        /// </summary>
        /// <param name="pLineasCredito"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<LineasCredito> ListarLineasCredito(Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ListarLineasCredito(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "ListarLineasCredito", ex);
                return null;
            }
        }

        public List<LineasCredito> ListarLineasCreditoSinAuxilio(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ListarLineasCreditoSinAuxilio(pLineasCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "ListarLineasCreditoSinAuxilio", ex);
                return null;
            }
        }


        public List<LineasCredito> ListarParentesco(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ListarParentesco(pLineasCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarParentesco", ex);
                return null;
            }
        }


        public List<LineasCredito> MotivoCredito(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.MotivoCredito(pLineasCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "MotivoCredito", ex);
                return null;
            }
        }


        public List<LineasCredito> ddlListarLineaBusines(Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ddlLinea(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinnes", "ddlListarLinea", ex);
                return null;
            }
        }
        public LineasCredito getPorcentajeMatriculaBusines(string CodLineaAuxilio, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.getPorcentajeMatricula(CodLineaAuxilio, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinnes", "ddlListarLinea", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de tipos documento dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos documento obtenidos</returns>
        public List<Persona1> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "ListarTiposDoc", ex);
                return null;
            }
        }


        public List<Atributos> ListarAtributos(Atributos pentidad, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ListarAtributos(pentidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCreditoBusiness", "ListarAtributos", ex);
                return null;
            }
        }


        public List<RangosTopes> ListarTopes(RangosTopes pentidad, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ListarTopes(pentidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCreditoBusiness", "ListarTopes", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Lineas de credito
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Lineas de credito obtenidas</returns>        
        public LineasCredito ConsultaLineaCredito(String cod_linea_credito, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultaLineaCredito(cod_linea_credito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsultaLineaCredito", "ConsultaLineaCredito", ex);
                return null;
            }
        }

        public LineasCredito Calcular_Cupo(String pcod_linea_credito, Int64 pcod_persona, DateTime pfecha, Usuario pUsuario)
        {
            try
            {
                String FormatoFecha = System.Configuration.ConfigurationManager.AppSettings["FormatoFecha"].ToString();
                String sfecha = pfecha.ToString(FormatoFecha);
                return DALineasCredito.Calcular_Cupo(pcod_linea_credito, pcod_persona, sfecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Calcular_Cupo", "Calcular_Cupo", ex);
                return null;
            }
        }

        /// <summary>
        /// Método para listas las líneas de crédito re-estructuradas
        /// </summary>
        /// <param name="pLinea"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<LineasCredito> ListarLineasCreditoRes(LineasCredito pLinea, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ListarLineasCreditoRes(pLinea, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCreditoBusiness", "ListarLineasCreditoRes", ex);
                return null;
            }
        }

        public List<Atributos> ListasAtributosLinea(LineasCredito pLinea, DateTime pfecha_solicitud, Int64 pnumero_cuotas, Double pmonto_solicitado, Usuario pUsuario)
        {
            try
            {
                Boolean bencontro = false;
                List<Int64> lstRangosAtr = new List<Int64>();
                lstRangosAtr = DALineasCredito.ListarRangos(pLinea, pUsuario);
                foreach (Int64 cod_rango in lstRangosAtr)
                {
                    bencontro = false;
                    List<RangosTopes> lstRangosTopes = new List<RangosTopes>();
                    lstRangosTopes = DALineasCredito.ListarRangosTopes(pLinea, cod_rango, pUsuario);
                    foreach (RangosTopes eRangosTopes in lstRangosTopes)
                    {
                        if (eRangosTopes.tipo_tope == 1)
                        {
                            if (pfecha_solicitud < Convert.ToDateTime(eRangosTopes.minimo) || pfecha_solicitud > Convert.ToDateTime(eRangosTopes.maximo))
                            {
                                bencontro = true;
                            }
                        }
                        if (eRangosTopes.tipo_tope == 2)
                        {
                            if (pnumero_cuotas < Convert.ToInt64(eRangosTopes.minimo) || pnumero_cuotas > Convert.ToInt64(eRangosTopes.maximo))
                            {
                                bencontro = true;
                            }
                        }
                        if (eRangosTopes.tipo_tope == 3)
                        {
                            if (pmonto_solicitado < Convert.ToDouble(eRangosTopes.minimo) || pmonto_solicitado > Convert.ToDouble(eRangosTopes.maximo))
                            {
                                bencontro = true;
                            }
                        }
                        if (bencontro == true)
                        {
                            return DALineasCredito.ListarRangosAtributos(pLinea, cod_rango, pUsuario);
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCreditoBusiness", "ListasAtributosLinea", ex);
                return null;
            }
        }


        public Boolean LineaEsFondoGarantiasComunitarias(String pId, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.LineaEsFondoGarantiasComunitarias(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCreditoBusiness", "LineaEsFondoGarantiasComunitarias", ex);
                return false;
            }
        }


        public List<LineasCredito> LineasCastigo(Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.LineasCastigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "LineasCastigo", ex);
                return null;
            }
        }

        public decimal ConsultarParametrosLinea(string cod_linea, string cod_parametro, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarParametrosLinea(cod_linea, cod_parametro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ConsultarParametrosLinea", ex);
                return 0;
            }
        }



        public void EliminarDeducciones(LineasCredito pDeducciones, Usuario vUsuario)
        {
            try
            {
                DALineasCredito.EliminarDeducciones(pDeducciones, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "EliminarDeducciones", ex);
            }
        }

        public void EliminarTodoElAtributo(LineasCredito pAtri, Usuario vUsuario)
        {
            try
            {
                DALineasCredito.EliminarTodoElAtributo(pAtri, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "EliminarTodoElAtributo", ex);
            }
        }

        //Agregado para parametrización de rangos de atributos
        public List<RangosTopes> CrearRanValAtributo(List<RangosTopes> lstRangos, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<RangosTopes> lista = new List<RangosTopes>();
                    //Modificar tabla 
                    if (lstRangos.Count > 0)
                    {
                        DALineasCredito.EliminarRanValAtributo(lstRangos[0].cod_atr, vUsuario);
                        foreach (RangosTopes rango in lstRangos)
                        {
                            RangosTopes rangoTope = new RangosTopes();
                            rangoTope = DALineasCredito.CrearRanValAtributo(rango, vUsuario);
                            lista.Add(rangoTope);
                        }
                        ts.Complete();
                        return lista;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "CrearRanValAtributo", ex);
                return null;
            }
        }

        public void EliminarRanValAtributo(Int64 cod_atributo, Usuario vUsuario)
        {
            try
            {
                DALineasCredito.EliminarRanValAtributo(cod_atributo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "EliminarRanValAtributo", ex);
            }
        }

        public List<RangosTopes> ListarRangosAtributos(Int64 cod_atr, Usuario vUsuario)
        {
            try
            {
                return DALineasCredito.ListarRangosAtributos(cod_atr, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ListarRangosAtributos", ex);
                return null;
            }
        }

        #region Parametros de Componentes (Documentos)

        public List<LineasCredito> ListarDocumentos(LineasCredito pLinea, string filtro, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ListarDocumentos(pLinea, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ListarDocumentos", ex);
                return null;
            }
        }

        public List<LineasCredito> ListarDocumentosXLinea(string pCod_linea_credito, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ListarDocumentosXLinea(pCod_linea_credito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ListarDocumentosXLinea", ex);
                return null;
            }
        }


        public List<LineasCredito> ListarComboTipoDocumentos(LineasCredito pLinea, Usuario pUsuario)
        {
            try
            {
                return DALineasCredito.ListarComboTipoDocumentos(pLinea, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ListarComboTipoDocumentos", ex);
                return null;
            }
        }


        public List<LineasCredito> ConsultarGarantiaDocumento(string pId, Usuario vUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarGarantiaDocumento(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ConsultarGarantiaDocumento", ex);
                return null;
            }
        }
        public List<LineasCredito> ConsultarGarantiacompleta(Usuario vUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarGarantiacompleta(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ConsultarGarantiaDocumento", ex);
                return null;
            }
        }




        public void EliminarDocumentosGarantia(string pId, Usuario vUsuario)
        {
            try
            {
                DALineasCredito.EliminarDocumentosGarantia(pId, vUsuario);
                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ConsultarGarantiaDocumento", ex);
                return;
            }
        }


        public void Eliminardocumentosdegarantia(string pId, string linea, Usuario vUsuario)
        {
            try
            {
                DALineasCredito.Eliminardocumentosdegarantia(pId, linea, vUsuario);
                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ConsultarGarantiaDocumento", ex);
                return;
            }
        }



        #endregion


        #region Procesos

        public List<ProcesoLineaCredito> ListarProcesos(ProcesoLineaCredito pProceso, string filtro, Usuario vUsuario)
        {
            try
            {
                return DALineasCredito.ListarProcesos(pProceso, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ListarProcesos", ex);
                return null;
            }
        }

        #endregion

        #region PRIORIDADES

        public List<LineasCredito> ConsultarPrioridad_Linea(string pId, Usuario vUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarPrioridad_Linea(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ConsultarPrioridad_Linea", ex);
                return null;
            }
        }

        #endregion

        #region destinacion

        public List<LineasCredito> ConsultarDestinacion_Linea(string pId, Usuario vUsuario)
        {
            try
            {
                return DALineasCredito.ConsultarDestinacion_Linea(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ConsultarDestinacion_Linea", ex);
                return null;
            }
        }

        public List<LineaCred_Destinacion> ListaDestinacionCredito(string pId, Usuario vUsuario)
        {
            try
            {
                return DALineasCredito.ListaDestinacionCredito(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListaDestinacionCredito", ex);
                return null;
            }
        }

        #endregion

        public List<ProcesoLineaCredito> ListarParametrosLinea(string codLienaCredito, Usuario vUsuario)
        {
            try
            {
                return DALineasCredito.ListarParametrosLinea(codLienaCredito, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ListarParametrosLinea", ex);
                return null;
            }
        }

    }
}