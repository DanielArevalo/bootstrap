using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Data;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Ahorros.Business;
using Xpinn.Ahorros.Entities;




namespace Xpinn.Auxilios.Business
{
    public class DesembolsoAuxilioBusiness : GlobalBusiness
    {
        protected DesembolsoAuxilioData BAAuxilio;

        public DesembolsoAuxilioBusiness() 
        {
            BAAuxilio = new DesembolsoAuxilioData();
        }


        public List<SolicitudAuxilio> ListarSolicitudAuxilio(string filtro, DateTime pFechaSol, string orden, Usuario vUsuario)
        {
            try
            {
                return BAAuxilio.ListarSolicitudAuxilio(filtro, pFechaSol,orden, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DesembolsoAuxilioBusiness", "ListarSolicitudAuxilio", ex);
                return null;
            }
        }


        public AprobacionAuxilio DesembolsarAuxilios(AprobacionAuxilio pAuxilio, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAuxilio = BAAuxilio.DesembolsarAuxilios(pAuxilio, vUsuario);
                    ts.Complete();
                }
                return pAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DesembolsoAuxilioBusiness", "DesembolsarAuxilios", ex);
                return null;
            }
        }


        public void CrearTran_Auxilio(long formadesembolso,bool pOpcionGiro,List<Auxilios_Giros> lstAuxGiros,ref Int64 COD_OPE,ref Int32 pIdGiro, DesembolsoAuxilio pDesembolso,Xpinn.Tesoreria.Entities.Operacion pOperacion,Xpinn.FabricaCreditos.Entities.Giro pGiro,bool pOpcion ,Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //GRABACION DE LA OPERACION
                    Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion Opera = new Tesoreria.Entities.Operacion();
                    string Error = "0";
                    Opera.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref Error, vUsuario);
                    COD_OPE = Opera.cod_ope;
                    //GRABACION DATOS GIRO
                    pGiro.cod_ope = Opera.cod_ope;
                    Xpinn.FabricaCreditos.Data.AvanceData AvancData = new Xpinn.FabricaCreditos.Data.AvanceData();
                    Xpinn.FabricaCreditos.Entities.Giro pReturnGiro = new Xpinn.FabricaCreditos.Entities.Giro();
                    if (pOpcion == true)
                        pReturnGiro = AvancData.CrearGiro(pGiro, vUsuario, 1);

                    pIdGiro = pReturnGiro.idgiro;

                    TipoFormaDesembolso tipoFormaDesembolso = formadesembolso.ToEnum<TipoFormaDesembolso>();

                    if (tipoFormaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                    {

                        AhorroVistaBusiness ahorroBusiness = new AhorroVistaBusiness();
                        AhorroVista ahorro = new AhorroVista
                        {

                            numero_cuenta = pDesembolso.numero_cuenta_ahorro_vista.ToString(),
                            cod_persona = pGiro.cod_persona,
                            cod_ope = COD_OPE,
                            fecha_cierre = pGiro.fec_giro,
                            V_Traslado = pDesembolso.valor,
                            codusuario = vUsuario.codusuario
                        };

                        ahorroBusiness.IngresoCuenta(ahorro, vUsuario);
                    }
                        //GRABACION DE LA TRAN_AUXILIO
                        pDesembolso.cod_ope = Opera.cod_ope;
                        pDesembolso = BAAuxilio.CrearTran_Auxilio(pDesembolso, vUsuario);
                        Auxilio_GiroData DAAuxilio = new Auxilio_GiroData();
                        if (pOpcionGiro == true && lstAuxGiros.Count > 0)
                        {
                            foreach (Auxilios_Giros nAuxilios in lstAuxGiros)
                            {
                                Auxilios_Giros pEntidad = new Auxilios_Giros();
                                nAuxilios.numero_auxilio = Convert.ToInt32(pDesembolso.numero_auxilio);
                                pEntidad = DAAuxilio.CrearAuxilio_giro(nAuxilios, vUsuario);
                            }
                        }
                        ts.Complete();
                    }
                }
            
            catch (Exception ex)
            {
                BOExcepcion.Throw("DesembolsoAuxilioBusiness", "CrearTran_Auxilio", ex);
            }
        }



        public SolicitudAuxilio ConsultarAuxilioAprobado(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BAAuxilio.ConsultarAuxilioAprobado(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DesembolsoAuxilioBusiness", "ConsultarAuxilioAprobado", ex);
                return null;
            }
        }



        public CuentasBancarias ConsultarCuentasBancarias(CuentasBancarias pId, string filtro, Usuario vUsuario)
        {
            try
            {
                return BAAuxilio.ConsultarCuentasBancarias(pId, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DesembolsoAuxilioBusiness", "ConsultarCuentasBancarias", ex);
                return null;
            }
        }

    }
}
