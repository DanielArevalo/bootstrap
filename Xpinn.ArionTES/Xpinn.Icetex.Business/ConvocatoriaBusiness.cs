using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Icetex.Data;
using Xpinn.Icetex.Entities;

namespace Xpinn.Icetex.Business
{
    public class ConvocatoriaBusiness
    {
        private ConvocatoriaData DAConvocatoria;
        protected ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();

        public ConvocatoriaBusiness()
        {
            DAConvocatoria = new ConvocatoriaData();
        }

        public List<ConvocatoriaRequerido> ValidacionRequisitos(Int64 pCod_Persona, DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return DAConvocatoria.ValidacionRequisitos(pCod_Persona, pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaBusiness", "ValidacionRequisitos", ex);
                return null;
            }
        }

        public ConvocatoriaRequerido CrearConvocatoriaRequerido(ConvocatoriaRequerido pRequerido, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pRequerido = DAConvocatoria.CrearConvocatoriaRequerido(pRequerido, vUsuario);
                    ts.Complete();
                }

                return pRequerido;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaBusiness", "CrearConvocatoriaRequerido", ex);
                return null;
            }
        }



        public CreditoIcetex CrearCreditoIcetex(CreditoIcetex pCreditoIcetex, List<CreditoIcetexDocumento> lstDocumentos, int pOpcion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    string pError = string.Empty;
                    if(pOpcion == 1)
                        pCreditoIcetex = DAConvocatoria.CrearCreditoIcetex(pCreditoIcetex, vUsuario);
                    else //Modificacion
                        pCreditoIcetex = DAConvocatoria.CreditoIcetexInscripcion(pCreditoIcetex, ref pError,  vUsuario);
                    if (!string.IsNullOrEmpty(pError))
                        return null;
                    Xpinn.Imagenes.Data.ImagenesORAData BOImagenes = new Imagenes.Data.ImagenesORAData();
                    if (lstDocumentos.Count > 0)
                    {
                        foreach (CreditoIcetexDocumento nCreditoIcetx in lstDocumentos)
                        {
                            nCreditoIcetx.numero_credito = pCreditoIcetex.numero_credito;
                            CreditoIcetexDocumento pEntidad = new CreditoIcetexDocumento();
                            pEntidad = BOImagenes.CrearCreditoIcetexDocumento(nCreditoIcetx, vUsuario);
                        }
                    }

                    ts.Complete();
                }
                return pCreditoIcetex;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaBusiness", "CrearCreditoIcetex", ex);
                return null;
            }
        }

        public CreditoIcetex ModificarCreditoIcetex(CreditoIcetex pCreditoIcetex, ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCreditoIcetex = DAConvocatoria.ModificarCreditoIcetex(pCreditoIcetex, ref pError, vUsuario);
                    ts.Complete();
                }
                return pCreditoIcetex;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public Boolean CrearCreditoIcetexDocumento(List<CreditoIcetexDocumento> lstDocumentos, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Imagenes.Data.ImagenesORAData BOImagenes = new Imagenes.Data.ImagenesORAData();
                    if (lstDocumentos.Count > 0)
                    {
                        foreach (CreditoIcetexDocumento nCreditoIcetx in lstDocumentos)
                        {
                            CreditoIcetexDocumento pEntidad = new CreditoIcetexDocumento();
                            pEntidad = BOImagenes.CrearCreditoIcetexDocumento(nCreditoIcetx, vUsuario);
                        }
                    }

                    ts.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaBusiness", "CrearCreditoIcetexDocumento", ex);
                return false;
            }
        }


        public List<IcetexDocumentos> ListarConvocatoriaDocumentos(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAConvocatoria.ListarConvocatoriaDocumentos(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaBusiness", "ListarConvocatoriaDocumentos", ex);
                return null;
            }
        }


        public List<CreditoIcetex> ListarCreditosIcetex(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAConvocatoria.ListarCreditosIcetex(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaBusiness", "ListarCreditosIcetex", ex);
                return null;
            }
        }

        public ConvocatoriaIcetex ConsultarConvocatoriaIcetex(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DAConvocatoria.ConsultarConvocatoriaIcetex(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaBusiness", "ConsultarConvocatoriaIcetex", ex);
                return null;
            }
        }

        public List<ConvocatoriaIcetex> ListarConvocatoriaIcetex(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAConvocatoria.ListarConvocatoriaIcetex(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaBusiness", "ListarConvocatoriaIcetex", ex);
                return null;
            }
        }

        public CreditoIcetex ConsultarCreditoIcetex(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAConvocatoria.ConsultarCreditoIcetex(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaBusiness", "ConsultarCreditoIcetex", ex);
                return null;
            }
        }

        public List<Reporte> ListarReporteCreditosIcetex(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAConvocatoria.ListarReporteCreditosIcetex(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaBusiness", "ListarReporteCreditosIcetex", ex);
                return null;
            }
        }

    }
}
