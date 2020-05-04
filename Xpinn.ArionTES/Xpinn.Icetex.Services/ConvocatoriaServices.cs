using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Icetex.Business;
using Xpinn.Icetex.Entities;
using Xpinn.Util;
using System.ServiceModel;

namespace Xpinn.Icetex.Services
{
    public class ConvocatoriaServices
    {
        private ConvocatoriaBusiness BOConvocatoria;
        private ExcepcionBusiness BOExcepcion;


        public ConvocatoriaServices()
        {
            BOConvocatoria = new ConvocatoriaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoProgramaCarga { get { return "170703"; } }

        public List<ConvocatoriaRequerido> ValidacionRequisitos(Int64 pCod_Persona, DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return BOConvocatoria.ValidacionRequisitos(pCod_Persona, pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaServices", "ValidacionRequisitos", ex);
                return null;
            }
        }

        public ConvocatoriaRequerido CrearConvocatoriaRequerido(ConvocatoriaRequerido pRequerido, Usuario vUsuario)
        {
            try
            {
                return BOConvocatoria.CrearConvocatoriaRequerido(pRequerido, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaServices", "CrearConvocatoriaRequerido", ex);
                return null;
            }
        }


        public CreditoIcetex CrearCreditoIcetex(CreditoIcetex pCreditoIcetex, List<CreditoIcetexDocumento> lstDocumentos, int pOpcion, Usuario vUsuario)
        {
            try
            {
                return BOConvocatoria.CrearCreditoIcetex(pCreditoIcetex, lstDocumentos, pOpcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaServices", "CrearCreditoIcetex", ex);
                return null;
            }
        }

        public CreditoIcetex ModificarCreditoIcetex(CreditoIcetex pCreditoIcetex, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOConvocatoria.ModificarCreditoIcetex(pCreditoIcetex, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaServices", "ModificarCreditoIcetex", ex);
                return null;
            }
        }


        public Boolean CrearCreditoIcetexDocumento(List<CreditoIcetexDocumento> lstDocumentos, Usuario vUsuario)
        {
            try
            {
                return BOConvocatoria.CrearCreditoIcetexDocumento(lstDocumentos, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaServices", "CrearCreditoIcetexDocumento", ex);
                return false;
            }
        }


        public List<IcetexDocumentos> ListarConvocatoriaDocumentos(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOConvocatoria.ListarConvocatoriaDocumentos(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaServices", "ListarConvocatoriaDocumentos", ex);
                return null;
            }
        }


        public List<CreditoIcetex> ListarCreditosIcetex(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOConvocatoria.ListarCreditosIcetex(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaServices", "ListarCreditosIcetex", ex);
                return null;
            }
        }

        public ConvocatoriaIcetex ConsultarConvocatoriaIcetex(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOConvocatoria.ConsultarConvocatoriaIcetex(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaServices", "ConsultarConvocatoriaIcetex", ex);
                return null;
            }
        }

        public List<ConvocatoriaIcetex> ListarConvocatoriaIcetex(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOConvocatoria.ListarConvocatoriaIcetex(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaServices", "ListarConvocatoriaIcetex", ex);
                return null;
            }
        }

        public CreditoIcetex ConsultarCreditoIcetex(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOConvocatoria.ConsultarCreditoIcetex(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaServices", "ConsultarCreditoIcetex", ex);
                return null;
            }
        }


        public List<Reporte> ListarReporteCreditosIcetex(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOConvocatoria.ListarReporteCreditosIcetex(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvocatoriaServices", "ListarReporteCreditosIcetex", ex);
                return null;
            }
        }


    }
}
