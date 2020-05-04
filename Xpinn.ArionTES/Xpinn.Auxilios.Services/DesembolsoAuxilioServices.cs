using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;


namespace Xpinn.Auxilios.Services
{
    public class DesembolsoAuxilioServices
    {
        public string CodigoPrograma { get { return "70103"; } }

        private DesembolsoAuxilioBusiness BOAuxilio;
        private ExcepcionBusiness BOExcepcion;


        public DesembolsoAuxilioServices()
        {
            BOAuxilio = new DesembolsoAuxilioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<SolicitudAuxilio> ListarSolicitudAuxilio(string filtro, DateTime pFechaSol, string orden, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.ListarSolicitudAuxilio(filtro, pFechaSol,orden, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DesembolsoAuxilioServices", "ListarSolicitudAuxilio", ex);
                return null;
            }
        }


        public AprobacionAuxilio DesembolsarAuxilios(AprobacionAuxilio pAuxilio, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.DesembolsarAuxilios(pAuxilio, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DesembolsoAuxilioServices", "DesembolsarAuxilios", ex);
                return null;
            }
        }


        public void CrearTran_Auxilio(long formadesembolso,bool pOpcionGiro, List<Auxilios_Giros> lstAuxGiros, ref Int64 COD_OPE, ref Int32 pIdGiro, DesembolsoAuxilio pDesembolso, Xpinn.Tesoreria.Entities.Operacion pOperacion, Xpinn.FabricaCreditos.Entities.Giro pGiro, bool pOpcion, Usuario vUsuario)
        {
            try
            {
                BOAuxilio.CrearTran_Auxilio(formadesembolso,pOpcionGiro, lstAuxGiros,ref COD_OPE, ref pIdGiro,pDesembolso, pOperacion, pGiro, pOpcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DesembolsoAuxilioServices", "CrearTran_Auxilio", ex);
            }
        }


        public SolicitudAuxilio ConsultarAuxilioAprobado(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.ConsultarAuxilioAprobado(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DesembolsoAuxilioServices", "ConsultarAuxilioAprobado", ex);
                return null;
            }
        }

        public CuentasBancarias ConsultarCuentasBancarias(CuentasBancarias pId, string filtro, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.ConsultarCuentasBancarias(pId, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPersonaService", "ConsultarCuentasBancarias", ex);
                return null;
            }
        }


    }
}
