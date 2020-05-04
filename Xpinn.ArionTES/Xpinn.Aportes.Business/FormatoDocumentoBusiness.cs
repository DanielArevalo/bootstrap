using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;




namespace Xpinn.Aportes.Business
{
    /// <summary>
    /// Objeto de negocio para Beneficiario
    /// </summary>
    public class FormatoDocumentoBusiness : GlobalBusiness
    {
        private FormatoDocumentoData DAFormato;


        public FormatoDocumentoBusiness()
        {
            DAFormato = new FormatoDocumentoData();
        }


        public FormatoDocumento CrearFormatoDocumentos(FormatoDocumento pFormatoDocumento, Usuario vUsuario, int pOpcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pFormatoDocumento = DAFormato.CrearFormatoDocumentos(pFormatoDocumento, vUsuario, pOpcion);
                    ts.Complete();
                }
                return pFormatoDocumento;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormatoDocumentoBusiness", "CrearFormatoDocumentos", ex);
                return null;
            }
        }

        public List<FormatoDocumento> ListarFormatoDocumento(FormatoDocumento pFormatoDocumento, Usuario vUsuario)
        {
            try
            {
                return DAFormato.ListarFormatoDocumento(pFormatoDocumento, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormatoDocumentoBusiness", "ListarFormatoDocumento", ex);
                return null;
            }
        }

        public FormatoDocumento ConsultarFormatoDocumento(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DAFormato.ConsultarFormatoDocumento(pId, vUsuario);
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
                return DAFormato.ListarDatosDeDocumento(pVariable, pNombre_pl, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormatoDocumentoBusiness", "ListarDatosDeDocumento", ex);
                return null;
            }
        }


        public List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> ListarDatosDeDocumentoOtros(Int64 pVariable, string pNombre_pl, Usuario vUsuario, string origen)
        {
            try
            {
                return DAFormato.ListarDatosDeDocumentoOtros(pVariable, pNombre_pl, vUsuario, origen);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormatoDocumentoBusiness", "ListarDatosDeDocumento", ex);
                return null;
            }
        }

        public void EliminarFormatoDocumento(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAFormato.EliminarFormatoDocumento(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FormatoDocumentoBusiness", "EliminarFormatoDocumento", ex);
            }
        }


        public Int64 ObtenerSiguienteCodigo(Usuario vUsuario)
        {
            try
            {
                return DAFormato.ObtenerSiguienteCodigo(vUsuario);
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public string ObtenerDocumento(long id_formato, string pVariable, Usuario vUsuario)
        {
            try
            {
                //RECUPERAR NOMBRE DE PL AL QUE EJECUTARA
                Xpinn.Aportes.Entities.FormatoDocumento FormatoDOC = new Xpinn.Aportes.Entities.FormatoDocumento();
                FormatoDOC = DAFormato.ConsultarFormatoDocumento(Convert.ToInt64(id_formato), vUsuario);

                if (FormatoDOC != null)
                {
                    List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> lstDocumentos = new List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento>();
                    lstDocumentos = DAFormato.ListarDatosDeDocumento(Convert.ToInt64(pVariable), FormatoDOC.nombre_pl, vUsuario);

                    if (FormatoDOC.texto != null)
                    {
                        if (FormatoDOC.texto.Trim().Length > 0)
                        {
                            try
                            {
                                // Hacer los reemplazos de los campos
                                foreach (Xpinn.FabricaCreditos.Entities.DatosDeDocumento dFila in lstDocumentos)
                                {
                                    try
                                    {
                                        string cCampo = dFila.Campo.ToString().Trim();
                                        string cValor = "";
                                        if (dFila.Valor != null)
                                            cValor = dFila.Valor.ToString().Trim().Replace("'", "");
                                        else
                                            cValor = "";
                                        FormatoDOC.texto = FormatoDOC.texto.Replace(cCampo, cValor).Replace("'", "");
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                                return FormatoDOC.texto;
                            }
                            catch (Exception ex)
                            {
                                return null;
                            }
                        }
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string ObtenerDocumentootros(long id_formato, string pVariable, Usuario vUsuario, string origen)
        {
            try
            {
                //RECUPERAR NOMBRE DE PL AL QUE EJECUTARA
                Xpinn.Aportes.Entities.FormatoDocumento FormatoDOC = new Xpinn.Aportes.Entities.FormatoDocumento();
                FormatoDOC = DAFormato.ConsultarFormatoDocumento(Convert.ToInt64(id_formato), vUsuario);

                if (FormatoDOC != null)
                {
                    List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> lstDocumentos = new List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento>();
                    lstDocumentos = DAFormato.ListarDatosDeDocumentoOtros(Convert.ToInt64(pVariable), FormatoDOC.nombre_pl, vUsuario, origen);

                    if (FormatoDOC.texto != null)
                    {
                        if (FormatoDOC.texto.Trim().Length > 0)
                        {
                            try
                            {
                                // Hacer los reemplazos de los campos
                                foreach (Xpinn.FabricaCreditos.Entities.DatosDeDocumento dFila in lstDocumentos)
                                {
                                    try
                                    {
                                        string cCampo = dFila.Campo.ToString().Trim();
                                        string cValor = "";
                                        if (dFila.Valor != null)
                                            cValor = dFila.Valor.ToString().Trim().Replace("'", "");
                                        else
                                            cValor = "";
                                        FormatoDOC.texto = FormatoDOC.texto.Replace(cCampo, cValor).Replace("'", "");
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                                return FormatoDOC.texto;
                            }
                            catch (Exception ex)
                            {
                                return null;
                            }
                        }
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public string ObtenerDocumentoCDAT(long id, string apertura, string cierre, Usuario vUsuario)
        {
            try
            {
                // Solicitando la información que debe ser mostrada en el documento
                Xpinn.FabricaCreditos.Data.DatosDeDocumentoData datosDeDocumentoData = new Xpinn.FabricaCreditos.Data.DatosDeDocumentoData();
                List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> lstDatosDeDocumento = new List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento>();

                Xpinn.FabricaCreditos.Data.TiposDocumentoData tiposDocumentoData = new Xpinn.FabricaCreditos.Data.TiposDocumentoData();
                Xpinn.FabricaCreditos.Entities.Documento document = new Xpinn.FabricaCreditos.Entities.Documento();


                //string cRutaLocalDeArchivoPDF = Server.MapPath("~/Page/CDATS/Documentos/CDAT_" + txtNumCDAT.Text.Trim() + ".pdf");
                Xpinn.FabricaCreditos.Entities.TiposDocumento tipoDOC = new Xpinn.FabricaCreditos.Entities.TiposDocumento();
                lstDatosDeDocumento = datosDeDocumentoData.ListarDatosDeDocumentoReporteCDAT(id, vUsuario);
                List<Xpinn.FabricaCreditos.Entities.TiposDocumento> lsDocumentos = tiposDocumentoData.ConsultarTiposDocumento("C", vUsuario);
                if (lsDocumentos.Count > 0)
                {
                    tipoDOC = lsDocumentos[0];
                }
                if (tipoDOC.texto == null)
                    tipoDOC.texto = Encoding.ASCII.GetString(tipoDOC.Textos);
                if (lsDocumentos.Count > 0 && tipoDOC.texto != null)
                {
                    if (tipoDOC.texto != null)
                    {
                        if (tipoDOC.texto.Trim().Length > 0 )
                        {
                            Xpinn.CDATS.Data.AperturaCDATData CData = new CDATS.Data.AperturaCDATData();
                            Xpinn.CDATS.Entities.Cdat vApe = new Xpinn.CDATS.Entities.Cdat();
                            vApe = CData.ConsultarApertu(id, vUsuario);

                            // Validar que exista el texto
                            if (tipoDOC.texto.Trim().Length > 0)
                            {

                                tipoDOC.texto = tipoDOC.texto.Replace("pFechaApertura", apertura).Replace("'", "");
                                tipoDOC.texto = tipoDOC.texto.Replace("pFechaVencimiento", cierre).Replace("'", "");
                                // Hacer los reemplazos de los campos
                                foreach (Xpinn.FabricaCreditos.Entities.DatosDeDocumento dFila in lstDatosDeDocumento)
                                {
                                    try
                                    {
                                        string cCampo = dFila.Campo.ToString().Trim();
                                        string cValor = "";
                                        if (dFila.Valor != null)
                                            cValor = dFila.Valor.ToString().Trim().Replace("'", "");
                                        else
                                            cValor = "";

                                        //Valor en letras
                                        if (cCampo == "pValor")
                                        {
                                            string ValorCDAT = cValor;
                                            CardinalidadNum objCardinalidad = new CardinalidadNum();
                                            string cardinal = " ";
                                            if (ValorCDAT != "0")
                                            {
                                                cardinal = objCardinalidad.enletras(ValorCDAT.Replace(".", ""));
                                                int cont = cardinal.Trim().Length - 1;
                                                int cont2 = cont - 7;
                                                if (cont2 >= 0)
                                                {
                                                    string c = cardinal.Substring(cont2);
                                                    if (cardinal.Trim().Substring(cont2) == "MILLONES" || cardinal.Trim().Substring(cont2 + 2) == "MILLON")
                                                        cardinal = cardinal + " DE PESOS M/CTE";
                                                    else
                                                        cardinal = cardinal + " PESOS M/CTE";
                                                }
                                            }
                                            tipoDOC.texto = tipoDOC.texto.Replace("pValorLetras", cardinal).Replace("'", "");
                                        }

                                        tipoDOC.texto = tipoDOC.texto.Replace(cCampo, cValor).Replace("'", "");


                                        if (vApe.modalidad_int == 1)
                                        {
                                            tipoDOC.texto = tipoDOC.texto.Replace("pModalidadV", "X").Replace("'", "");
                                            tipoDOC.texto = tipoDOC.texto.Replace("pModalidadA", "-").Replace("'", "");
                                        }
                                        else if (vApe.modalidad_int == 2)
                                        {
                                            tipoDOC.texto = tipoDOC.texto.Replace("pModalidadA", "X").Replace("'", "");
                                            tipoDOC.texto = tipoDOC.texto.Replace("pModalidadV", "-").Replace("'", "");
                                        }
                                        else
                                        {
                                            tipoDOC.texto = tipoDOC.texto.Replace("pModalidadA", "-").Replace("'", "");
                                            tipoDOC.texto = tipoDOC.texto.Replace("pModalidadV", "-").Replace("'", "");
                                        }

                                    }
                                    catch
                                    {
                                    }
                                }

                                //Deja los campos beneficiarios vacios en caso de que no los tenga
                                tipoDOC.texto = tipoDOC.texto.Replace("pNomBeneficiario1", " ");
                                tipoDOC.texto = tipoDOC.texto.Replace("pIdenBeneficiario1", " ");
                                tipoDOC.texto = tipoDOC.texto.Replace("pCodBeneficiario1", " ");
                                tipoDOC.texto = tipoDOC.texto.Replace("pNomBeneficiario2", " ");
                                tipoDOC.texto = tipoDOC.texto.Replace("pIdenBeneficiario2", " ");
                                tipoDOC.texto = tipoDOC.texto.Replace("pCodBeneficiario2", " ");
                                tipoDOC.texto = tipoDOC.texto.Replace("pNomBeneficiario3", " ");
                                tipoDOC.texto = tipoDOC.texto.Replace("pIdenBeneficiario3", " ");
                                tipoDOC.texto = tipoDOC.texto.Replace("pCodBeneficiario3", " ");
                            }

                        }
                    }
                }



                return tipoDOC.texto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}