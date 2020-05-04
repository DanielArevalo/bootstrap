using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Icetex.Business;
using Xpinn.Icetex.Entities;
using Xpinn.Util;
using System.ServiceModel;
using System.Data;

namespace Xpinn.Icetex.Services
{
    public class AprobacionServices
    {
        private AprobacionBusiness BOAprobacion;
        private ExcepcionBusiness BOExcepcion;

        public AprobacionServices()
        {
            BOAprobacion = new AprobacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170701"; } }
        public string CodigoProgramaReporte { get { return "170702"; } }
        public string CodigoProgramaModifi { get { return "170704"; } }
        public string CodigoProgramaConsul { get { return "170705"; } }

        public DataTable ListarCreditosDocumentosIcetex(string pFiltro, ref List<ListadosIcetex> lstDocumentos, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOAprobacion.ListarCreditosDocumentosIcetex(pFiltro, ref lstDocumentos, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServices", "ListarCreditosIcetex", ex);
                return null;
            }
        }


        public Boolean CrearCreditoCheckList(CreditoIcetexAprobacion pAprobacion, List<CreditoIcetexCheckList> lstInformacion, List<ListadosIcetex> lstDocumentos, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOAprobacion.CrearCreditoCheckList(pAprobacion, lstInformacion, lstDocumentos, ref pError, vUsuario);
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
                return BOAprobacion.ListarDocumentosIcetex(pFiltro, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServices", "ListarDocumentosIcetex", ex);
                return null;
            }
        }

        public List<CreditoIcetexCheckList> ListarCreditoIcetexCheckList(string pFiltro, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOAprobacion.ListarCreditoIcetexCheckList(pFiltro, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServices", "ListarCreditoIcetexCheckList", ex);
                return null;
            }
        }

        public List<CreditoIcetexAprobacion> ListarCreditosAprobacion(string pFiltro, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOAprobacion.ListarCreditosAprobacion(pFiltro, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServices", "ListarCreditosAprobacion", ex);
                return null;
            }
        }

    }
}
