using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Icetex.Data;
using Xpinn.Icetex.Entities;
using System.Data;

namespace Xpinn.Icetex.Business
{
    public class AprobacionBusiness
    {
        private AprobacionData DAAprobacion;
        protected ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();

        public AprobacionBusiness()
        {
            DAAprobacion = new AprobacionData();
        }

        public DataTable ListarCreditosDocumentosIcetex(string pFiltro, ref List<ListadosIcetex> lstDocumentos, ref string pError, Usuario vUsuario)
        {
            try
            {
                lstDocumentos = DAAprobacion.ListarDocumentosIcetex(pFiltro, ref pError, vUsuario);
                return DAAprobacion.ListarCreditosIcetex(pFiltro, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionBusiness", "ListarCreditosDocumentosIcetex", ex);
                return null;
            }
        }


        public Boolean CrearCreditoCheckList(CreditoIcetexAprobacion pAprobacion, List<CreditoIcetexCheckList> lstInformacion, List<ListadosIcetex> lstDocumentos, ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    ConvocatoriaData DataConvocatoria = new ConvocatoriaData();
                    Xpinn.Imagenes.Data.ImagenesORAData BOImagenes = new Imagenes.Data.ImagenesORAData();
                    pAprobacion = BOImagenes.CrearCreditoIcetexAprobacion(pAprobacion, vUsuario, 1);
                    string pFiltro = " where C.numero_Credito = " + pAprobacion.numero_credito;
                    CreditoIcetex pCredIcetex = DataConvocatoria.ConsultarCreditoIcetex(pFiltro, vUsuario);
                    if (pCredIcetex != null)
                    {
                        CreditoIcetex pData = new CreditoIcetex();
                        pCredIcetex.estado = pAprobacion.estado;
                        try
                        {
                            pData = DataConvocatoria.ModificarCreditoIcetex(pCredIcetex, ref pError, vUsuario);
                        }
                        catch (Exception ex)
                        {
                            pError = ex.Message;
                        }
                    }
                    if (pAprobacion != null)
                    {
                        if (lstInformacion.Count > 0)
                        {
                            foreach (CreditoIcetexCheckList nCreditoList in lstInformacion)
                            {
                                nCreditoList.idaprobacion = pAprobacion.idaprobacion;
                                nCreditoList.numero_credito = Convert.ToInt64(pAprobacion.numero_credito);
                                CreditoIcetexCheckList pEntidad = new CreditoIcetexCheckList();
                                pEntidad = DAAprobacion.CrearCreditoCheckList(nCreditoList, vUsuario, 1);
                            }
                        }
                        if (lstDocumentos.Count > 0)
                        {
                            foreach (ListadosIcetex nDocumentos in lstDocumentos)
                            {
                                //Actualizar los archivos del documento
                                CreditoIcetexDocumento pDocu = new CreditoIcetexDocumento();
                                pDocu.cod_credoc = Convert.ToInt32(nDocumentos.codigo);
                                pDocu.imagen = nDocumentos.archivo;
                                pDocu.observacion = nDocumentos.observacion;
                                pDocu = BOImagenes.ModificarCreditoIcetexDocumento(pDocu, vUsuario);
                            }
                        }
                    }
                    ts.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return false;
            }
        }

        public List<ListadosIcetex> ListarDocumentosIcetex(string pFiltro, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DAAprobacion.ListarDocumentosIcetex(pFiltro, ref pError, vUsuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionBusiness", "ListarDocumentosIcetex", ex);
                return null;
            }
        }

        public List<CreditoIcetexCheckList> ListarCreditoIcetexCheckList(string pFiltro, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DAAprobacion.ListarCreditoIcetexCheckList(pFiltro, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionBusiness", "ListarCreditoIcetexCheckList", ex);
                return null;
            }
        }

        public List<CreditoIcetexAprobacion> ListarCreditosAprobacion(string pFiltro, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DAAprobacion.ListarCreditosAprobacion(pFiltro, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionBusiness", "ListarCreditosAprobacion", ex);
                return null;
            }
        }


    }
}
