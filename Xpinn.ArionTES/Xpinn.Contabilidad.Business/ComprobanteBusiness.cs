using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;
using System.IO;
using System.Web;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para comprobante
    /// </summary>
    public class ComprobanteBusiness : GlobalData
    {
        private ComprobanteData DAComprobante;
        private StreamReader strReader;
        /// <summary>
        /// Constructor del objeto de negocio para Usuarios
        /// </summary>
        public ComprobanteBusiness()
        {
            DAComprobante = new ComprobanteData();
        }

        public List<DetalleComprobante> ConsultarCargaComprobanteDetalle(Int64 operacion, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.ConsultarCargaComprobanteDetalle(operacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultarCargaComprobanteDetalle", ex);
                return null;
            }
        }

        public List<DetalleComprobante> ConsultarCargaComprobanteNiifDetalle(Int64 operacion, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.ConsultarCargaComprobanteNiifDetalle(operacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultarCargaComprobanteNiifDetalle", ex);
                return null;
            }
        }

        public Boolean CargarArchivo(Stream stream, string pTipoNorma, bool pgrabar, ref List<DetalleComprobante> lstcargacomprobante, ref string error, Usuario pUsuario)
        {
            if (lstcargacomprobante == null)
                lstcargacomprobante = new List<DetalleComprobante>();
            else
                lstcargacomprobante.Clear();

            Boolean output = true;
            String readLine;
            int contador = 0;

            try
            {
                if (pgrabar)
                    DAComprobante.EliminarCargaComprobanteDetalle(1, pUsuario);
                using (strReader = new StreamReader(stream))
                {
                    while (strReader.Peek() >= 0)
                    {
                        DetalleComprobante pEntityCargaComprobante = new DetalleComprobante();
                        readLine = strReader.ReadLine();
                        String[] arrayLineas = readLine.Split(Convert.ToChar(13));
                        if (contador > 0)
                        {
                            pEntityCargaComprobante = Cargar(arrayLineas[0].ToString().Replace("	", ";"), contador, pTipoNorma, pgrabar, ref error, pUsuario);
                            if (!pgrabar)
                                lstcargacomprobante.Add(pEntityCargaComprobante);
                        }
                        contador = contador + 1;
                    }
                    if (!pgrabar)
                        lstcargacomprobante = AsignarTercero(lstcargacomprobante, pTipoNorma, pUsuario);
                }
            }
            catch (IOException ex)
            {
                error = ex.Message;
            }
            return output;
        }

        public Boolean CargarTexto(string datos, string pTipoNorma, ref string error, Usuario pUsuario)
        {
            Boolean output = true;
            int contador = 0;

            try
            {
                String[] arrayLineas = datos.Split(Convert.ToChar(13));
                if (arrayLineas.Count() > 0)
                {
                    DAComprobante.EliminarCargaComprobanteDetalle(1, pUsuario);
                    for (int i = 0; i < arrayLineas.Count(); i++)
                    {
                        Cargar(arrayLineas[i].Replace("	", ";"), contador, pTipoNorma, true, ref error, pUsuario);
                        contador = contador + 1;
                    }
                }
            }
            catch (IOException ex)
            {
                output = false;
                error = ex.Message;
            }
            return output;
        }

        public Boolean Validar(List<DetalleComprobante> lstCargaComprobante, Usuario pUsuario, ref string Error)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (DetalleComprobante gCargaComprobante in lstCargaComprobante)
                    {
                        DAComprobante.Validar(gCargaComprobante, pUsuario, ref Error);
                        if (Error.Trim() != "")
                        {
                            return false;
                        }
                    }
                    ts.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "Validar", ex);
                return false;
            }
        }

        private DetalleComprobante Cargar(String lineFile, int contador, string pTipoNorma, bool pgrabar, ref string perror, Usuario pUsuario)
        {
            DetalleComprobante comprobante = new DetalleComprobante();
            perror = "";
            try
            {
                if (lineFile.Trim() != "")
                {
                    String[] arrayline = lineFile.Split(';');
                    if (pTipoNorma == "L")
                    {
                        comprobante.cod_cuenta = Convert.ToString(Convert.ToInt64(arrayline[0].ToString()));
                    }
                    else if (pTipoNorma == "A")
                    {
                        comprobante.cod_cuenta = Convert.ToString(arrayline[0].ToString().Trim());
                        comprobante.cod_cuenta_niif = DAComprobante.HomologarCuentaNIIF(comprobante.cod_cuenta, pUsuario);
                    }
                    else
                    {
                        comprobante.cod_cuenta_niif = Convert.ToString(Convert.ToInt64(arrayline[0].ToString()));
                    }
                    if (arrayline[1].ToString() != "")
                        comprobante.centro_costo = Convert.ToInt64(arrayline[1].ToString());
                    comprobante.detalle = Convert.ToString(arrayline[2].ToString());
                    comprobante.tipo = Convert.ToString(Convert.ToString(arrayline[3].ToString())).Trim();
                    if (comprobante.tipo != null)
                        if (comprobante.tipo.Length > 1)
                            comprobante.tipo = comprobante.tipo.Substring(0, 1);
                    if (arrayline[4].ToString() != "")
                        comprobante.valor = Convert.ToDecimal(arrayline[4].ToString());
                    if (arrayline.Count() > 5)
                    {
                        if (arrayline[5] != null)
                        {
                            if (arrayline[5].ToString().Trim() != "")
                            {
                                string Identificacion = arrayline[5].ToString().Trim();
                                comprobante.identificacion = Identificacion;
                                if (pgrabar)
                                {
                                    DetalleComprobante vData = new DetalleComprobante();
                                    vData = DAComprobante.Identificacion_RETORNA_CodPersona(Identificacion, pUsuario);
                                    if (vData.tercero != 0)
                                    {
                                        try { comprobante.tercero = vData.tercero; }
                                        catch { comprobante.tercero = null; }
                                        if (!pgrabar)
                                            comprobante.nom_tercero = vData.nom_tercero;
                                    }
                                }
                            }
                            else
                                comprobante.tercero = null;
                        }
                    }
                    if (arrayline.Count() > 6)
                        if (arrayline[6] != null)
                            if (arrayline[6].ToString() != "")
                                comprobante.centro_gestion = Convert.ToInt64(arrayline[6].ToString());

                    // Grabar datos del detalle en una tabla
                    if (pgrabar)
                    {
                        using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                        {
                            comprobante.cod_cuenta = pTipoNorma == "N" ? comprobante.cod_cuenta_niif : comprobante.cod_cuenta;
                            DAComprobante.CrearCargaComprobanteDetalle(comprobante, ref perror, pUsuario);
                            ts.Complete();
                        }
                    }
                }
            }
            catch (TransactionException ex)
            {
                perror = ex.Message;
            }
            return comprobante;
        }

        public Comprobante CrearComprobante(List<DetalleComprobante> vDetalleComprobante, Comprobante pComprobante, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pComprobante = DAComprobante.CrearComprobante(pComprobante, vUsuario);

                    DAComprobante.Crearcheque(pComprobante, vUsuario);

                    for (int i = 0; i < vDetalleComprobante.Count; i++)
                    {
                        DetalleComprobante detalle = new DetalleComprobante();
                        detalle = vDetalleComprobante[i];
                        detalle.num_comp = pComprobante.num_comp;
                        detalle.tipo_comp = pComprobante.tipo_comp;
                        detalle = DAComprobante.CrearDetalleComprobante(detalle, vUsuario);
                    }

                    ts.Complete();
                }

                return pComprobante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "CrearComprobante", ex);
                return null;
            }
        }

        /// <summary>
        /// generar metodo para que guarde el maestro de talle se sugiere foreach para guardar el detalle 
        /// </summary>
        /// <param name="pComprobante"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Comprobante ModificarComprobante(List<DetalleComprobante> vDetalleComprobante, Comprobante pComprobante, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pComprobante = DAComprobante.ModificarComprobante(pComprobante, vUsuario);
                    if (pComprobante.tipo_comp == 5 && pComprobante.entidad != null)
                        DAComprobante.Crearcheque(pComprobante, vUsuario);
                    if (pComprobante.tipo_comp == 5)
                        DAComprobante.ModificarGiro(pComprobante, vUsuario);

                    if (DAComprobante.IniciarDetalleComprobante(pComprobante.num_comp, pComprobante.tipo_comp, vUsuario))
                    {
                        DetalleComprobante detalle = new DetalleComprobante();

                        for (int i = 0; i < vDetalleComprobante.Count; i++)
                        {
                            detalle = vDetalleComprobante[i];

                            detalle.num_comp = pComprobante.num_comp;
                            detalle.tipo_comp = pComprobante.tipo_comp;

                            DAComprobante.CrearDetalleComprobante(detalle, vUsuario);
                        }
                        ts.Complete();
                    }
                    else
                    {
                        ts.Dispose();
                    }

                }

                return pComprobante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ModificarComprobante", ex);
                return null;
            }

        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<Comprobante> ListarComprobante(Comprobante pComprobante, Usuario pUsuario, String filtro, String orden)
        {
            try
            {
                return DAComprobante.ListarComprobante(pComprobante, pUsuario, filtro, orden);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ListarComprobante", ex);
                return null;
            }
        }

        public List<Comprobante> ListarComprobanteParaAprobar(Comprobante pComprobante, Usuario pUsuario, String pfiltro)
        {
            try
            {
                return DAComprobante.ListarComprobanteParaAprobar(pComprobante, pUsuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ListarComprobanteParaAprobar", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<DetalleComprobante> ListarComprobantesreporte(DetalleComprobante pComprobante, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.ListarComprobantesreporte(pComprobante, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ListarComprobantesreporte", ex);
                return null;
            }
        }

        public string Consultafecha(Usuario pUsuario, string tipoComprobante = null)
        {
            try
            {
                return DAComprobante.Consultafecha(pUsuario, tipoComprobante);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteServices", "ConsultaUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Método para consultar los datos de un comprobante
        /// </summary>
        /// <param name="pnum_comp"></param>
        /// <param name="ptipo_comp"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public bool ConsultarComprobante(Int64 pnum_comp, Int64 ptipo_comp, ref Comprobante vComprobante, ref List<DetalleComprobante> vDetalleComprobante, Usuario vUsuario)
        {
            try
            {
                Comprobante Comprobante = new Comprobante();

                vComprobante = DAComprobante.ConsultarComprobante(pnum_comp, ptipo_comp, vUsuario);
                Comprobante = DAComprobante.ConsultarGiro(pnum_comp, ptipo_comp, vUsuario);
                if (Comprobante.cuenta != null && vComprobante.cuenta != "")
                    vComprobante.cuenta = Comprobante.cuenta;
                if (Comprobante.cod_ope != 0 && Comprobante.cod_ope != Int64.MinValue)
                    vComprobante.cod_ope = Comprobante.cod_ope;
                vDetalleComprobante = DAComprobante.ConsultarDetalleComprobante(pnum_comp, ptipo_comp, vUsuario);
                // Consultar los datos del cheque
                Comprobante lCheque = new Comprobante();
                lCheque = DAComprobante.ConsultarCheque(pnum_comp, ptipo_comp, vUsuario);
                if (lCheque != null)
                {
                    vComprobante.cheque_id = lCheque.cheque_id;
                    vComprobante.cheque_iden_benef = lCheque.cheque_iden_benef;
                    vComprobante.cheque_nombre = lCheque.cheque_nombre;
                }

                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultarComprobante", ex);
                vComprobante.num_comp = pnum_comp;
                vComprobante.tipo_comp = ptipo_comp;
                return false;
            }
        }


        public bool ConsultarGiroGeneral(Int64 pnum_comp, Int64 ptipo_comp, ref Giro pGiro, Usuario pUsuario)
        {
            try
            {
                pGiro = DAComprobante.ConsultarGiroGeneral(pnum_comp, ptipo_comp, pUsuario);
                if (pGiro == null || pGiro.idgiro == 0)
                    pGiro = DAComprobante.ConsultarGiroRealizadoGeneral(pnum_comp, ptipo_comp, pUsuario);
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultarGiroGeneral", ex);
                return false;
            }
        }


        public Int64 ConsultarOperacion(Int64 pnumComp, Int64 ptipo_comp, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.ConsultarOperacion(pnumComp, ptipo_comp, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultarOperacion", ex);
                return -1;
            }
        }
        public string ConsultarCuenta(Int64 pCodBanco, string pNumCuenta, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.ConsultarCuenta(pCodBanco, pNumCuenta, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultarComprobante", ex);
                return "";
            }
        }

        /// <summary>
        /// Consulta el nombre del usuario que elabora/aprueba un comprobante
        /// </summary>
        /// <param name="cod"></param>
        /// <returns></returns>
        public string ConsultaUsuario(long cod, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.ConsultaUsuario(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultaUsuario", ex);
                return "";
            }
        }


        public Int64 ConsultaCodUsuario(string cod, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.ConsultaCodUsuario(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultaCodUsuario", ex);
                return -1;
            }
        }

        public string CuentaEsAuxiliar(string cod, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.CuentaEsAuxiliar(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "CuentaEsAuxiliar", ex);
                return "";
            }
        }

        public string CuentaNIFEsAuxiliar(string cod, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.CuentaNIFEsAuxiliar(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "CuentaNIFEsAuxiliar", ex);
                return "";
            }
        }


        public Boolean CuentaEsGiro(string cod, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.CuentaEsGiro(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "CuentaEsGiro", ex);
                return false;
            }
        }

        public List<ProcesoContable> ConsultaProceso(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.ConsultaProceso(pcod_ope, ptip_ope, pfecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultaProceso", ex);
                return null;
            }
        }

        public List<ProcesoContable> ConsultaProcesoUlt(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.ConsultaProcesoUlt(pcod_ope, ptip_ope, pfecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultaProcesoUlt", ex);
                return null;
            }
        }


        public Boolean GenerarComprobanteSinCommit(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Int64 pcod_ofi, Int64 pcod_persona, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string pError, Usuario pUsuario)
        {
            try
            {
                Boolean rpta = false;
                rpta = DAComprobante.GenerarComprobante(pcod_ope, ptip_ope, pfecha, pcod_ofi, pcod_persona, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref pError, pUsuario);
                return rpta;
            }
            catch (Exception ex)
            {
                //BOExcepcion.Throw("ComprobanteBusiness", "GenerarComprobanteSinCommit", ex);
                pError += ex.Message + "..-..";
                return false;
            }
        }

        public Boolean GenerarComprobante(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Int64 pcod_ofi, Int64 pcod_persona, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string pError, Usuario pUsuario)
        {
            try
            {
                Boolean rpta = false;
                TransactionOptions tranopc = new TransactionOptions();
                tranopc.Timeout = TimeSpan.MaxValue;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tranopc))
                {
                    rpta = DAComprobante.GenerarComprobante(pcod_ope, ptip_ope, pfecha, pcod_ofi, pcod_persona, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref pError, pUsuario);
                    ts.Complete();
                }
                return rpta;
            }
            catch (Exception ex)
            {
                //BOExcepcion.Throw("ComprobanteBusiness", "GenerarComprobante", ex);
                pError += ex.Message;
                return false;
            }
        }
               

        public Boolean AprobarAnularComprobante(Comprobante pComprobante, ref string Error, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (DAComprobante.AprobarAnularComprobante(pComprobante, ref Error, vUsuario) == true)
                    {
                        ts.Complete();
                        return true;
                    }
                    else
                    {
                        ts.Dispose();
                        return false;
                    }
                }
            }
            catch
            {
                //BOExcepcion.Throw("ComprobanteBusiness", "AprobarAnularComprobante", ex);
                return false;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Int64 pTipoComp, DateTime pFecha, Int64 pOficina, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.ObtenerSiguienteCodigo(pTipoComp, pFecha, pOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }


        public Comprobante ConsultarComprobanteTipoMotivoAnulacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                Comprobante Comprobante = new Comprobante();
                Comprobante = DAComprobante.ConsultarComprobanteTipoMotivoAnulacion(pId, pusuario);
                return Comprobante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultarComprobante", ex);
                return null;
            }
        }


        public List<Comprobante> ListarComprobanteTipoMotivoAnulacion(Comprobante pComprobante, Usuario pusuario)
        {
            try
            {
                return DAComprobante.ListarComprobanteTipoMotivoAnulacion(pComprobante, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ListarComprobante", ex);
                return null;
            }
        }


        public void Anularcomprobante(Comprobante pCOMPROBANTE, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAComprobante.Anularcomprobante(pCOMPROBANTE, pusuario);

                    ts.Complete();

                }

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("COMPROBANTEBusiness", "Anularcomprobante", ex);
            }
        }


        public bool ConsultarAnulaciondetalle(Int64 pnum_comp, Int64 ptipo_comp, ref Comprobante vComprobante, ref List<DetalleComprobante> vDetalleComprobante, Usuario vUsuario)
        {
            try
            {
                Comprobante Comprobante = new Comprobante();

                vComprobante = DAComprobante.ConsultarComprobante(pnum_comp, ptipo_comp, vUsuario);
                Comprobante = DAComprobante.ConsultarGiro(pnum_comp, ptipo_comp, vUsuario);
                if (Comprobante.cuenta != null && vComprobante.cuenta != "")
                    vComprobante.cuenta = Comprobante.cuenta;
                if (Comprobante.cod_ope != 0 && Comprobante.cod_ope != null)
                    vComprobante.cod_ope = Comprobante.cod_ope;
                vDetalleComprobante = DAComprobante.ConsultarAnulaciondetalle(pnum_comp, ptipo_comp, vUsuario);
                // Consultar los datos del cheque
                Comprobante lCheque = new Comprobante();
                lCheque = DAComprobante.ConsultarCheque(pnum_comp, ptipo_comp, vUsuario);
                if (lCheque != null)
                {
                    vComprobante.cheque_id = lCheque.cheque_id;
                    vComprobante.cheque_iden_benef = lCheque.cheque_iden_benef;
                    vComprobante.cheque_nombre = lCheque.cheque_nombre;
                }

                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultarComprobante", ex);
                vComprobante.num_comp = pnum_comp;
                vComprobante.tipo_comp = ptipo_comp;
                return false;
            }
        }


        public Comprobante crearanulacioncomprobante(List<DetalleComprobante> vDetalleComprobante, Comprobante pComprobante, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAComprobante.Anularcomprobante(pComprobante, vUsuario);

                    ts.Complete();
                }

                return pComprobante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "crearanulacioncomprobante", ex);
                return null;
            }
        }

        public Int64 consultacod_persona(string cod, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.consultacod_persona(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultaCodUsuario", ex);
                return -1;
            }
        }
        public bool ConsultarComprobante_Anulacion(Int64 pnum_comp, Int64 ptipo_comp, ref Comprobante vComprobante, ref List<DetalleComprobante> vDetalleComprobante, Usuario vUsuario)
        {
            try
            {
                Comprobante Comprobante = new Comprobante();

                vComprobante = DAComprobante.ConsultarComprobante(pnum_comp, ptipo_comp, vUsuario);
                Comprobante = DAComprobante.ConsultarGiro(pnum_comp, ptipo_comp, vUsuario);
                if (Comprobante.cuenta != null && vComprobante.cuenta != "")
                    vComprobante.cuenta = Comprobante.cuenta;
                if (Comprobante.cod_ope != 0 && Comprobante.cod_ope != null)
                    vComprobante.cod_ope = Comprobante.cod_ope;
                vDetalleComprobante = DAComprobante.ConsultarDetalleCompro_Anulacion(pnum_comp, ptipo_comp, vUsuario);
                // Consultar los datos del cheque
                Comprobante lCheque = new Comprobante();
                lCheque = DAComprobante.ConsultarCheque(pnum_comp, ptipo_comp, vUsuario);
                if (lCheque != null)
                {
                    vComprobante.cheque_id = lCheque.cheque_id;
                    vComprobante.cheque_iden_benef = lCheque.cheque_iden_benef;
                    vComprobante.cheque_nombre = lCheque.cheque_nombre;
                }

                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultarComprobante", ex);
                vComprobante.num_comp = pnum_comp;
                vComprobante.tipo_comp = ptipo_comp;
                return false;
            }
        }

        public Comprobante ConsultarObservacionesAnulacion(Int64 pnum_comp, Int64 ptipo_comp, Usuario vUsuario)
        {
            try
            {
                Comprobante Comprobante = new Comprobante();
                Comprobante = DAComprobante.ConsultarObservacionesAnulacion(pnum_comp, ptipo_comp, vUsuario);
                return Comprobante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultarObservacionesAnulacion", ex);
                return null;
            }
        }

        public Boolean GenerarComprobanteCaja(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Int64 pcod_ofi, Int64 pcod_caja, Int64 pcod_persona, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string pError, Usuario pUsuario)
        {
            try
            {
                Boolean rpta = false;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    rpta = DAComprobante.GenerarComprobanteCaja(pcod_ope, ptip_ope, pfecha, pcod_ofi, pcod_caja, pcod_persona, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref pError, pUsuario);
                    ts.Complete();
                }
                return rpta;
            }
            catch (Exception ex)
            {
                //BOExcepcion.Throw("ComprobanteBusiness", "GenerarComprobanteCaja", ex);
                pError += ex.Message;
                return false;
            }
        }

        public List<DetalleComprobante> AsignarTercero(List<DetalleComprobante> pLstDetalle, string pTipoNorma, Usuario pUsuario)
        {
            try
            {
                pLstDetalle = DAComprobante.AsignarTercero(pLstDetalle, pTipoNorma, pUsuario);
            }
            catch
            {
                return pLstDetalle;
            }
            return pLstDetalle;
        }

        public List<DetalleComprobante> ConsultarDetalleComprobante(Int64 pnum_comp, Int64 ptipo_comp, Usuario pUsuario)
        {
            List<DetalleComprobante> pLstDetalle = new List<DetalleComprobante>();
            try
            {
                pLstDetalle = DAComprobante.ConsultarDetalleComprobante(pnum_comp, ptipo_comp, pUsuario);
            }
            catch
            {
                return pLstDetalle;
            }
            return pLstDetalle;
        }
        //Agregado para verificar cuentas contables que se manejan en productos
        public Int32 ValidarCuentaContable(Int64 nuevo, string cod_cuenta, Usuario pUsuario)
        {
            try
            {
                Int32 validar = DAComprobante.ValidarCuentaContable(nuevo, cod_cuenta, pUsuario);
                return validar;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ValidarCuentaContable", ex);
                return 0;
            }

        }

        public bool GenerarComprobanteTraslado(DateTime fecha_contabilizacion, Int64 cod_traslado, ref Int64 num_comp, ref Int64 tipo_comp, ref string error, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.GenerarComprobanteTraslado(fecha_contabilizacion, cod_traslado, ref num_comp, ref tipo_comp, ref error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "GenerarComprobanteTraslado", ex);
                return false;
            }
        }

        public string HomologarCuentaContable(Int64 ptipo_comp, string pcod_cuenta, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.HomologarCuentaContable(ptipo_comp, pcod_cuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "HomologarCuentaContable", ex);
                return "";
            }

        }

        public List<Comprobante> ContabilizarOperacionSinComp(DateTime pFechaIni, DateTime pFechaFin, Int64 pTipoProducto, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<Comprobante> lstOperacion = new List<Comprobante>();
                    lstOperacion = DAComprobante.ContabilizarOperacionSinComp(pFechaIni, pFechaFin, pTipoProducto, vUsuario);
                    ts.Complete();

                    return lstOperacion;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ContabilizarOperacionSinComp", ex);
                return null;
            }
        }

        public Comprobante ConsultarDatosElaboro(Int64 pelaboro, Usuario pUsuario)
        {
            try
            {
                return DAComprobante.ConsultarDatosElaboro(pelaboro,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteServices", "ConsultarDatosElaboro", ex);
                return null;
            }
        }
    }
}