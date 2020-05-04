using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class LiquidacionContratoBusiness : GlobalBusiness
    {

        private LiquidacionContratoData DALiquidacionContrato;

        public LiquidacionContratoBusiness()
        {
            DALiquidacionContrato = new LiquidacionContratoData();
        }

        public LiquidacionContrato CrearLiquidacionContrato
        (
            LiquidacionContrato pLiquidacionContrato, 
            List<LiquidacionContratoDetalle> listaLiquidacionContratoDetalle,
            Usuario pusuario,
            Xpinn.Tesoreria.Entities.Operacion pOperacion,
            Xpinn.FabricaCreditos.Entities.Giro pGiro,
            ref Int64 pIdGiro)
          {
            Double valorapagar = 0;
            DateTime fecha;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion entidadop = new Tesoreria.Entities.Operacion();
                    LiquidacionContrato liquidacion = new LiquidacionContrato();
                    entidadop = operacion.GrabarOperacion(pOperacion, pusuario);

                    pLiquidacionContrato.cod_ope = entidadop.cod_ope;
                    pLiquidacionContrato = DALiquidacionContrato.CrearLiquidacionContrato(pLiquidacionContrato, pusuario);
                    liquidacion.cod_ope = entidadop.cod_ope;
                    fecha = liquidacion.fecharetiro;
                    foreach (LiquidacionContratoDetalle detalle in listaLiquidacionContratoDetalle)
                    {

                        detalle.codigoliquidacioncontrato = pLiquidacionContrato.consecutivo;
                        DALiquidacionContrato.CrearLiquidacionContratoDetalle(detalle, pusuario);
                        //interfaz
                        liquidacion.cod_concepto = detalle.codigoconceptonomina;
                        liquidacion.valor = detalle.valor;
                        liquidacion.codigoempleado = pLiquidacionContrato.codigoempleado;
                        liquidacion.codorigen = pLiquidacionContrato.codorigen;
                        DALiquidacionContrato.CrearLiquidContratoNominaInterfaz(liquidacion, pusuario);
                        valorapagar = Convert.ToDouble(detalle.valorPago);

                        //Enviar liquidacion de salud 
                      
                        
                    }


                    // INSERTAR EL GIRO
                    Xpinn.FabricaCreditos.Data.CreditoData DAgiro = new Xpinn.FabricaCreditos.Data.CreditoData();
                    Xpinn.FabricaCreditos.Entities.Credito egiro= new Xpinn.FabricaCreditos.Entities.Credito();
                    fecha = liquidacion.fecharetiro;
                    pGiro.fec_reg= liquidacion.fecharetiro;

                    egiro.fecha_desembolso = pLiquidacionContrato.fecharetiro;
                    DAgiro.GuardarGiro(egiro.numero_radicacion, liquidacion.cod_ope, Convert.ToInt32(pGiro.forma_pago), Convert.ToDateTime(egiro.fecha_desembolso), Convert.ToDouble(pGiro.valor), Convert.ToInt32(pGiro.idctabancaria), pGiro.cod_banco, pGiro.num_cuenta, pGiro.tipo_cuenta, Convert.ToInt64(pGiro.cod_persona), pusuario.nombre, pusuario);


                    ts.Complete();
                }

                return pLiquidacionContrato;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionContratoBusiness", "CrearLiquidacionContrato", ex);
                return null;
            }
        }

        public LiquidacionContrato ModificarLiquidacionContrato(LiquidacionContrato pLiquidacionContrato, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLiquidacionContrato = DALiquidacionContrato.ModificarLiquidacionContrato(pLiquidacionContrato, pusuario);

                    ts.Complete();

                }

                return pLiquidacionContrato;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionContratoBusiness", "ModificarLiquidacionContrato", ex);
                return null;
            }
        }

        public Tuple<List<LiquidacionContratoDetalle>, LiquidacionContrato> GenerarLiquidacionDeContrato(LiquidacionContrato pLiquidacionContrato, Usuario pusuario)
        {
            try
            {
                return DALiquidacionContrato.GenerarLiquidacionDeContrato(pLiquidacionContrato, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionContratoBusiness", "GenerarLiquidacionDeContrato", ex);
                return null;
            }
        }

        public void EliminarLiquidacionContrato(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALiquidacionContrato.EliminarLiquidacionContrato(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionContratoBusiness", "EliminarLiquidacionContrato", ex);
            }
        }


        public LiquidacionContrato ConsultarLiquidacionContrato(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionContrato LiquidacionContrato = new LiquidacionContrato();
                LiquidacionContrato = DALiquidacionContrato.ConsultarLiquidacionContrato(pId, pusuario);
                return LiquidacionContrato;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionContratoBusiness", "ConsultarLiquidacionContrato", ex);
                return null;
            }
        }


        public List<LiquidacionContrato> ListarLiquidacionContrato(string filtro, Usuario pusuario)
        {
            try
            {
                return DALiquidacionContrato.ListarLiquidacionContrato(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionContratoBusiness", "ListarLiquidacionContrato", ex);
                return null;
            }
        }

        public List<LiquidacionContratoDetalle> ListarLiquidacionContratoDetalle(long consecutivoLiquidacion, Usuario usuario)
        {
            try
            {
                return DALiquidacionContrato.ListarLiquidacionContratoDetalle(consecutivoLiquidacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionContratoBusiness", "ListarLiquidacionContratoDetalle", ex);
                return null;
            }
        }
    }
}