using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Business;

namespace Xpinn.Nomina.Services

{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class LiquidacionContratoService
    {

        private LiquidacionContratoBusiness BOLiquidacionContrato;
        private ExcepcionBusiness BOExcepcion;

        public LiquidacionContratoService()
        {
            BOLiquidacionContrato = new LiquidacionContratoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250217"; } }

        public LiquidacionContrato CrearLiquidacionContrato
        (
            LiquidacionContrato pLiquidacionContrato,
            List<LiquidacionContratoDetalle> listaLiquidacionContratoDetalle,
            Usuario pusuario,
            Xpinn.Tesoreria.Entities.Operacion pOperacion,
            Xpinn.FabricaCreditos.Entities.Giro pGiro,
            ref Int64 pIdGiro
        )
        {
            try
            {
                pLiquidacionContrato = BOLiquidacionContrato.CrearLiquidacionContrato(pLiquidacionContrato, listaLiquidacionContratoDetalle, pusuario, pOperacion, pGiro, ref pIdGiro);
                return pLiquidacionContrato;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionContratoService", "CrearLiquidacionContrato", ex);
                return null;
            }
        }

        public Tuple<List<LiquidacionContratoDetalle>, LiquidacionContrato> GenerarLiquidacionDeContrato(LiquidacionContrato pLiquidacionContrato, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionContrato.GenerarLiquidacionDeContrato(pLiquidacionContrato, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionContratoService", "GenerarLiquidacionDeContrato", ex);
                return null;
            }
        }

        public LiquidacionContrato ModificarLiquidacionContrato(LiquidacionContrato pLiquidacionContrato, Usuario pusuario)
        {
            try
            {
                pLiquidacionContrato = BOLiquidacionContrato.ModificarLiquidacionContrato(pLiquidacionContrato, pusuario);
                return pLiquidacionContrato;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionContratoService", "ModificarLiquidacionContrato", ex);
                return null;
            }
        }


        public void EliminarLiquidacionContrato(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOLiquidacionContrato.EliminarLiquidacionContrato(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionContratoService", "EliminarLiquidacionContrato", ex);
            }
        }


        public LiquidacionContrato ConsultarLiquidacionContrato(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionContrato LiquidacionContrato = new LiquidacionContrato();
                LiquidacionContrato = BOLiquidacionContrato.ConsultarLiquidacionContrato(pId, pusuario);
                return LiquidacionContrato;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionContratoService", "ConsultarLiquidacionContrato", ex);
                return null;
            }
        }


        public List<LiquidacionContrato> ListarLiquidacionContrato(string filtro, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionContrato.ListarLiquidacionContrato(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionContratoService", "ListarLiquidacionContrato", ex);
                return null;
            }
        }

        public List<LiquidacionContratoDetalle> ListarLiquidacionContratoDetalle(long consecutivoLiquidacion, Usuario usuario)
        {
            try
            {
                return BOLiquidacionContrato.ListarLiquidacionContratoDetalle(consecutivoLiquidacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionContratoService", "ListarLiquidacionContratoDetalle", ex);
                return null;
            }
        }
    }
}