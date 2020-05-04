using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Confecoop.Entities;
using Xpinn.Confecoop.Business;
using Xpinn.Util;

namespace Xpinn.Confecoop.Services
{
    public class ConfecoopService
    {

        public string CodigoPrograma { get { return "170401"; } }
        public string CodigoProgramaAporte { get { return "170402"; } }
        public string CodigoProgramaInformeDirectivo { get { return "170412"; } }
        public string CodigoProgramaInformeNetaPositiva { get { return "170413"; } }
        public string CodigoProgramaInformeNetaNegativa { get { return "170414"; } }
        public string CodigoProgramaInformeNetaDirectivos { get { return "170415"; } }
        public string CodigoProgramaCartera { get { return "170403"; } }
        public string CodigoProgramaActivos { get { return "170404"; } }
        public string CodigoProgramaAsoci { get { return "170405"; } }
        public string CodigoProgramaCaptaciones { get { return "170406"; } }
        public string CodigoProgramaRLiquidez { get { return "170407"; } }
        public string CodigoProgramaObliFinanc { get { return "170408"; } }
        public string CodigoProgramaRedOficina { get { return "170409"; } }
        public string CodigoProgramaEstadisticas { get { return "170410"; } }
        public string CodigoProgramaOperaciones { get { return "170411"; } }
        public string CodigoProgramaFdoLiquidez { get { return "170416"; } }
        public string CodigoProgramaCotitular { get { return "170417"; } }
        public string CodigoProgramaCXP { get { return "170419"; } }
        public string CodigoProgramaConvenioRecaudo { get { return "170420"; } }
        public string CodigoProgramaPatrimonio { get { return "170421"; } }
        public string CodigoProgramaPatronales { get { return "170422"; } }
        public string CodigoProgramaTerceros { get { return "170420"; } }
        public string CodigoProgramaSaldos { get { return "170423"; } }
        public string CodigoProgramaCastigados { get { return "170424"; } }

        private ConfecoopBusiness BOPuc;
        private ExcepcionBusiness BOExcepcion;

        public ConfecoopService()
        {
            BOPuc = new ConfecoopBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<PUC> ListarFechaCierreGLOBAL(string tipo, string estado, Usuario pUsuario)
        {
            try
            {
                return BOPuc.ListarFechaCierreGLOBAL(tipo,estado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConfecoopService", "ListarFechaCierreGLOBAL", ex);
                return null;
            }
        }



        public List<PUC> ListarTEMP_SUPERSOLIDARIA(PUC pPuc, ref string pError, Usuario vUsuario, bool estado, Int32 pTipoNorma)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERSOLIDARIA(pPuc, ref pError, vUsuario, estado, pTipoNorma);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ConfecoopService", "ListarTEMP_SUPERSOLIDARIA", ex);
                return null;
            }
        }


        public List<PUC> ListarTEMP_SUPERSOLI_APORTES(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERSOLI_APORTES(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERSOLIDARIA", ex);
                return null;
            }
        }

        public List<PUC> ListarTEMP_POSICIONNETADIRECTIVOS(PUC pPuc, ref string pError, Usuario usuario)
        {
            try
            {
                return BOPuc.ListarTEMP_POSICIONNETADIRECTIVOS(pPuc, ref pError, usuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_POSICIONNETANEGATIVA(PUC pPuc, ref string pError, Usuario usuario)
        {
            try
            {
                return BOPuc.ListarTEMP_POSICIONNETANEGATIVA(pPuc, ref pError, usuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_CARTERA(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERSOLI_CARTERA(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERSOLIDARIA", ex);
                return null;
            }
        }

        public List<PUC> ListarTEMP_POSICIONNETAPOSITIVA(PUC pPuc, ref string pError, Usuario usuario)
        {
            try
            {
                return BOPuc.ListarTEMP_POSICIONNETAPOSITIVA(pPuc, ref pError, usuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_ACTIVOSFIJOS(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERSOLI_ACTIVOSFIJOS(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERSOLIDARIA", ex);
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPER_DIRECTIVOS(PUC pPuc, ref string pError, Usuario usuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPER_DIRECTIVOS(pPuc, ref pError, usuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPER_ASOCIADOS(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPER_ASOCIADOS(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPER_ASOCIADOS", ex);
                return null;
            }
        }


        public List<PUC> ListarTEMP_SUPERCAPTACIONESS(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERCAPTACIONESS(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERCAPTACIONESS", ex);
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERiesgoLiquidez(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERiesgoLiquidez(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERCAPTACIONESS", ex);
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERObligacionFinanciera(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERObligacionFinanciera(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERCAPTACIONESS", ex);
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERedOficinas(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERedOficinas(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERCAPTACIONESS", ex);
                return null;
            }
        }


        public List<PUC> ListarTEMP_SUPERSOLI_ESTADISTICAS(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERSOLI_ESTADISTICAS(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarFechaCierre(Usuario pUsuario)
        {
            try
            {
                return BOPuc.ListarFechaCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConfecoopService", "ListarFechaCierre", ex);
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_OPERACIONES(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERSOLI_OPERACIONES(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPER_FDO_LIQUIDEZ(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPER_FDO_LIQUIDEZ(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }


        public List<PUC> ListarTEMP_SUPERCotitulares(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERCotitulares(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_CUENTAXPAGAR(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERSOLI_CUENTAXPAGAR(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                  return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_CONVENIO(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERSOLI_CONVENIO(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_PATRIMONIO(PUC pPuc, ref string pError, Usuario vUsuario, Int32 pTipoNorma)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERSOLI_PATRIMONIO(pPuc, ref pError, vUsuario, pTipoNorma);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }
        public List<PUC> ListarTEMP_SUPERSOLI_TERCEROS(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERSOLI_TERCEROS(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }
        public List<PUC> ListarTEMP_SUPERSOLI_PATRONALES(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERSOLI_PATRONALES(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLIDARIA_SALDOS(PUC pPuc, ref string pError, Usuario vUsuario, bool estado, Int32 pTipoNorma)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERSOLIDARIA_SALDOS(pPuc, ref pError, vUsuario, estado, pTipoNorma);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_CASTIGOS(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPuc.ListarTEMP_SUPERSOLI_CASTIGOS(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERSOLIDARIA", ex);
                return null;
            }
        }



    }
}
