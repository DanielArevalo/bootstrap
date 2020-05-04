using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using Xpinn.Util;
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Business;


namespace Xpinn.CDATS.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class LiquidacionCDATService
    {
        LiquidacionCDATBusiness BOLiqui ;
        ExcepcionBusiness BOException;

        public LiquidacionCDATService()
        {
            BOLiqui = new LiquidacionCDATBusiness();
            BOException = new ExcepcionBusiness();
        }

        public string CodigoProgramaLiqui { get { return "220306"; } }
        public string CodigoProgramaLiqui2 { get { return "220322"; } }
        public void GENERAR_LiquidacionCDAT(LiquidacionCDAT pLiqui, Usuario vUsuario)
        {
            try
            {
                BOLiqui.GENERAR_LiquidacionCDAT(pLiqui, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("LiquidacionCDATService", "GENERAR_LiquidacionCDAT", ex);                
            }
        }
        

        public LiquidacionCDAT Listartitular(LiquidacionCDAT pLiqui, Usuario pUsuario)
        {
            try
            {
                return BOLiqui.Listartitular(pLiqui, pUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("PersonaService", "Listartitular", ex);
                return null;
            }
        }




        public List<LiquidacionCDAT> ListarTemporal_LiquidacionCDAT(LiquidacionCDAT pTemp, DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return BOLiqui.ListarTemporal_LiquidacionCDAT(pTemp,pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("LiquidacionCDATService", "ListarTemporal_LiquidacionCDAT", ex);
                return null;
            }
        }




        public LiquidacionCDAT CrearLiquidacionCDAT(LiquidacionCDAT pLiqui, Usuario vUsuario)
        {
            try
            {
                return BOLiqui.CrearLiquidacionCDAT(pLiqui, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("LiquidacionCDATService", "CrearLiquidacionCDAT", ex);
                return null;
            }
        }


        public LiquidacionCDAT CalculoLiquidacionCDAT(LiquidacionCDAT pLiqui, Usuario vUsuario)
        {
            try
            {
                return BOLiqui.CalculoLiquidacionCDAT(pLiqui, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("LiquidacionCDATService", "CalculoLiquidacionCDAT", ex);
                return null;
            }
        }

        public void CierreLiquidacionCDAT(ref Int64 COD_OPE, ref string pError, Xpinn.Tesoreria.Entities.Operacion pOperacion, bool opcion, long formadesembolso,Xpinn.FabricaCreditos.Entities.Giro pGiro, LiquidacionCDAT pLiqui, Usuario vUsuario)
        {
            try
            {
                BOLiqui.CierreLiquidacionCDAT(ref COD_OPE, ref pError, pOperacion,  opcion, formadesembolso,pGiro, pLiqui, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("LiquidacionCDATService", "CierreLiquidacionCDAT", ex);
            }
        }

        public void GuardarLiquidacionCDAT(ref Int64 COD_OPE, Xpinn.Tesoreria.Entities.Operacion pOperacion, bool opcion, long formadesembolso, Xpinn.FabricaCreditos.Entities.Giro pGiro,LiquidacionCDAT pLiqui, Usuario vUsuario)
        {
            try
            {
                BOLiqui.GuardarLiquidacionCDAT(ref COD_OPE, pOperacion, opcion, formadesembolso,pGiro, pLiqui, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("LiquidacionCDATService", "CierreLiquidacionCDAT", ex);
            }
        }

        public void GuardarLiquidacionCDATmasivos(ref Int64 COD_OPE, Xpinn.Tesoreria.Entities.Operacion pOperacion, bool opcion, long formadesembolso, Xpinn.FabricaCreditos.Entities.Giro pGiro, LiquidacionCDAT pLiqui, Usuario vUsuario)
        {
            try
            {
                BOLiqui.GuardarLiquidacionCDATMasivos(ref COD_OPE, pOperacion, opcion, formadesembolso, pGiro, pLiqui, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("LiquidacionCDATService", "GuardarLiquidacionCDATmasivos", ex);
            }
        }

        public void GuardarLiquidacionCDATPrroroga(bool opcion, long formadesembolso, Xpinn.FabricaCreditos.Entities.Giro pGiro, ref Int64 COD_OPE, Cdat pCdatModificar, ProrrogaCDAT pProrroga, CDAT_AUDITORIA pAuditoria, Xpinn.Tesoreria.Entities.Operacion pOperacion, LiquidacionCDAT pLiqui,  Usuario vUsuario)
        {
            try
            {
                BOLiqui.GuardarLiquidacionCDATPrroroga(opcion, formadesembolso, pGiro, ref COD_OPE, pCdatModificar, pProrroga, pAuditoria, pOperacion, pLiqui, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("LiquidacionCDATService", "GuardarLiquidacionCDATPrroroga", ex);
            }
        }


        public LiquidacionCDAT ConsultarCierreCdats(Usuario pUsuario)
        {
            try
            {
                return BOLiqui.ConsultarCierreCdats(pUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("LiquidacionCDATService", "ConsultarCierreCdats", ex);
                return null;
            }
        }


    }
}
