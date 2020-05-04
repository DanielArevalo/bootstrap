using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Data;
using Xpinn.Util;

namespace Xpinn.Auxilios.Business
{
    public class AprobacionAuxilioBusiness : GlobalBusiness
    {
        protected AprobacionAuxilioData BAAuxilio;

        public AprobacionAuxilioBusiness()
        {
            BAAuxilio = new AprobacionAuxilioData();
        }

        public AprobacionAuxilio AprobarAuxilios(AprobacionAuxilio pAuxilio, int pOpcion, Auxilio_Orden_Servicio pAuxOrden, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAuxilio = BAAuxilio.AprobarAuxilios(pAuxilio, vUsuario);
                    SolicitudAuxilioData DASolicitudAUX = new SolicitudAuxilioData();
                    Auxilio_Orden_Servicio pOrdenAux = new Auxilio_Orden_Servicio();
                    if (pOpcion == 1)
                    { //CREAR
                        pOrdenAux = DASolicitudAUX.CrearAuxilioOdenServicio(pAuxOrden, vUsuario);
                    }
                    else if (pOpcion == 2)
                    { //MODIFICAR
                        pOrdenAux = DASolicitudAUX.ModificarAuxilioOdenServicio(pAuxOrden, vUsuario);
                    }
                    else if (pOpcion == 3)
                    { //ELIMINAR
                        DASolicitudAUX.EliminarAuxilioOrdenServicio(pAuxOrden.idordenservicio, pAuxOrden.numero_auxilio, vUsuario);
                    }
                    ts.Complete();
                }
                return pAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionAuxilioBusiness", "AprobarAuxilios", ex);
                return null;
            }
        }

        public AprobacionAuxilio CrearControlAuxilio(AprobacionAuxilio pAuxilio, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAuxilio = BAAuxilio.CrearControlAuxilio(pAuxilio, vUsuario);
                    ts.Complete();
                }
                return pAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionAuxilioBusiness", "CrearControlAuxilio", ex);
                return null;
            }
        }


        //CARGAR AUXILIOS
        public void RegistrarAuxiliosCargados(ref Int64 vCod_Ope, Xpinn.Tesoreria.Entities.Operacion pOperacion, List<SolicitudAuxilio> lstAuxilios, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    SolicitudAuxilioData SolicitarData = new SolicitudAuxilioData();
                    DesembolsoAuxilioData BADesembolso = new DesembolsoAuxilioData();
                    

                    if (lstAuxilios != null && lstAuxilios.Count > 0)
                    {
                        //OPERACION
                        Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Tesoreria.Data.OperacionData();
                        pOperacion = DAOperacion.GrabarOperacion(pOperacion, vUsuario);
                        vCod_Ope = pOperacion.cod_ope;

                        foreach (SolicitudAuxilio eAuxilio in lstAuxilios)
                        {
                            if (eAuxilio.cod_persona != 0 && eAuxilio.valor_solicitado != 0)
                            {
                                eAuxilio.numero_auxilio = 0;
                                //eAuxilio.fecha_solicitud                      
                                //eAuxilio.cod_persona
                                //eAuxilio.cod_linea_auxilio
                                //eAuxilio.valor_solicitado
                                eAuxilio.fecha_aprobacion = DateTime.Today; 
                                eAuxilio.valor_aprobado = eAuxilio.valor_solicitado; 
                                eAuxilio.fecha_desembolso = DateTime.Today; 
                                eAuxilio.detalle = "Carga Masiva Auxilios";
                                eAuxilio.estado = "D";                                
                                eAuxilio.porc_matricula = 0;
                                eAuxilio.numero_radicacion = null;

                                SolicitudAuxilio nAuxilio = new SolicitudAuxilio();
                                //Se crea el Auxilio e inmediatamente se deja como Desembolsado
                                nAuxilio = SolicitarData.CrearSolicitudAuxilio(eAuxilio, vUsuario);

                                //TRANSACCION
                                DesembolsoAuxilio pDesembolso = new DesembolsoAuxilio();
                                pDesembolso.numero_transaccion = 0;
                                pDesembolso.numero_auxilio = nAuxilio.numero_auxilio;
                                pDesembolso.cod_ope = vCod_Ope;
                                pDesembolso.cod_cliente = nAuxilio.cod_persona;
                                pDesembolso.cod_linea_auxilio = nAuxilio.cod_linea_auxilio;
                                pDesembolso.tipo_tran = 1;
                                pDesembolso.valor= nAuxilio.valor_aprobado;
                                pDesembolso.estado = 1;
                                pDesembolso.num_tran_anula = 0;                             
                                
                                pDesembolso = BADesembolso.CrearTran_Auxilio(pDesembolso, vUsuario);
                               
                            }
                        }
                    }

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "RegistrarAuxiliosCargados", ex);
            }
        }




    }
}
