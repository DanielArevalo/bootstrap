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
    public class SolicitudAuxilioBusiness : GlobalBusiness
    {
        protected SolicitudAuxilioData BAAuxilio;

        public SolicitudAuxilioBusiness() 
        {
            BAAuxilio = new SolicitudAuxilioData();
        }



        public SolicitudAuxilio CrearSolicitudAuxilio(SolicitudAuxilio pAuxilio,Auxilio_Orden_Servicio pAuxOrden, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    int cod;
                    pAuxilio = BAAuxilio.CrearSolicitudAuxilio(pAuxilio, vUsuario);

                    cod = pAuxilio.numero_auxilio;

                    if (pAuxilio.lstValidacion != null)
                    {
                        int num = 0;
                        foreach (Requisitos eAux in pAuxilio.lstValidacion)
                        {
                            Requisitos nAuxilios = new Requisitos();
                            eAux.numero_auxilio = cod;
                            nAuxilios = BAAuxilio.CrearAuxiliosRequisitos(eAux, vUsuario);
                            num += 1;
                        }
                    }

                    if (pAuxilio.lstDetalle != null)
                    {
                        foreach (DetalleSolicitudAuxilio eServ in pAuxilio.lstDetalle)
                        {
                            DetalleSolicitudAuxilio nDetalle = new DetalleSolicitudAuxilio();
                            eServ.numero_auxilio = cod;
                            if (eServ.codbeneficiarioaux <= 0)
                                nDetalle = BAAuxilio.CrearDetalleAuxilio(eServ, vUsuario);
                            else
                                nDetalle = BAAuxilio.ModificarDetalleAuxilio(eServ, vUsuario);
                        }
                    }

                    Auxilio_Orden_Servicio nAuxServicio = new Auxilio_Orden_Servicio();
                    if (pAuxOrden.idproveedor != null && pAuxOrden.nomproveedor != null && pAuxOrden.cod_persona != null)
                    {
                        pAuxOrden.numero_auxilio = cod;
                        nAuxServicio = BAAuxilio.CrearAuxilioOdenServicio(pAuxOrden, vUsuario);
                    }

                    ts.Complete();
                }

                return pAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioBusiness", "CrearSolicitudAuxilio", ex);
                return null;
            }
        }


        public SolicitudAuxilio ModificarSolicitudAuxilio(SolicitudAuxilio pAuxilio, Auxilio_Orden_Servicio pAuxOrden,int pOpcion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAuxilio = BAAuxilio.ModificarSolicitudAuxilio(pAuxilio, vUsuario);

                    int Cod;
                    Cod = pAuxilio.numero_auxilio;

                    if (pAuxilio.lstValidacion != null)
                    {
                        int num = 0;
                        foreach (Requisitos eAux in pAuxilio.lstValidacion)
                        {
                            eAux.numero_auxilio = Cod;
                            Requisitos nAuxilio = new Requisitos();
                            if (eAux.codrequisitoauxilio <= 0 || eAux.codrequisitoauxilio == null)
                                nAuxilio = BAAuxilio.CrearAuxiliosRequisitos(eAux, vUsuario);
                            else
                                nAuxilio = BAAuxilio.ModificarAuxiliosRequisitos(eAux, vUsuario);
                            num += 1;
                        }
                    }

                    if (pAuxilio.lstDetalle != null)
                    {
                        int num = 0;
                        foreach (DetalleSolicitudAuxilio eServi in pAuxilio.lstDetalle)
                        {
                            eServi.numero_auxilio = Cod;
                            DetalleSolicitudAuxilio nProgramacion = new DetalleSolicitudAuxilio();
                            if (eServi.codbeneficiarioaux <= 0 || eServi.codbeneficiarioaux == null)
                                nProgramacion = BAAuxilio.CrearDetalleAuxilio(eServi, vUsuario);
                            else
                                nProgramacion = BAAuxilio.ModificarDetalleAuxilio(eServi, vUsuario);
                            num += 1;
                        }
                    }

                    Auxilio_Orden_Servicio nAuxServicio = new Auxilio_Orden_Servicio();
                    
                    pAuxOrden.numero_auxilio = Cod;
                    Auxilio_Orden_Servicio pEntidad = new Auxilio_Orden_Servicio();

                    if (pOpcion == 1)
                        nAuxServicio = BAAuxilio.CrearAuxilioOdenServicio(pAuxOrden, vUsuario);
                    else if (pOpcion == 2)
                        nAuxServicio = BAAuxilio.ModificarAuxilioOdenServicio(pAuxOrden, vUsuario);
                    else if (pOpcion == 3)
                        BAAuxilio.EliminarAuxilioOrdenServicio(pAuxOrden.idordenservicio, pAuxOrden.numero_auxilio, vUsuario);

                    ts.Complete();
                }

                return pAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioBusiness", "ModificarSolicitudAuxilio", ex);
                return null;
            }
        }



        public List<SolicitudAuxilio> ListarSolicitudAuxilio(SolicitudAuxilio pAuxilio, DateTime pFechaSol, Usuario vUsuario, string filtro)
        {
            try
            {
                return BAAuxilio.ListarSolicitudAuxilio(pAuxilio, pFechaSol, vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioBusiness", "ListarSolicitudAuxilio",ex);
                return null;
            }            
        }


        public void EliminarAuxilio(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BAAuxilio.EliminarAuxilio(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioBusiness", "EliminarAuxilio",ex);
            }
        }

        public SolicitudAuxilio ConsultarAUXILIO(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BAAuxilio.ConsultarAUXILIO(pId,vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioBusiness", "ConsultarAUXILIO", ex);
                return null;
            }   
        }

        public bool ConsultarEstadoPersona(Int64? pCodPersona, string pIdentificacion, string pEstado, Usuario pUsuario)
        {
            try
            {
                return BAAuxilio.ConsultarEstadoPersona(pCodPersona, pIdentificacion, pEstado, pUsuario);
            }
            catch 
            {
                return false;
            }
        }

        //Detalle

        public void EliminarDETALLEAuxilio(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BAAuxilio.EliminarDETALLEAuxilio(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioBusiness", "EliminarDETALLEAuxilio", ex);
            }
        }


        public List<DetalleSolicitudAuxilio> ConsultarDETALLEAuxilio(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BAAuxilio.ConsultarDETALLEAuxilio(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioBusiness", "ConsultarDETALLEAuxilio", ex);
                return null;
            }   
        }



        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BAAuxilio.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;                
            }
        }


        public SolicitudAuxilio ListarLineasDauxilios(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BAAuxilio.ListarLineasDauxilios(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioBusiness", "ListarLineasDauxilios", ex);
                return null;
            }   
        }


        public List<Requisitos> ConsultarValidacionRequisitos(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BAAuxilio.ConsultarValidacionRequisitos(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioBusiness", "ConsultarValidacionRequisitos", ex);
                return null;
            }   
        }

        public List<Requisitos> CargarDatosRequisitos(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BAAuxilio.CargarDatosRequisitos(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioBusiness", "CargarDatosRequisitos", ex);
                return null;
            }   
        }

        public Auxilio_Orden_Servicio ConsultarAUX_OrdenServicio(String pFiltro, Usuario vUsuario)
        {
            try
            {
                return BAAuxilio.ConsultarAUX_OrdenServicio(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioBusiness", "ConsultarAUX_OrdenServicio", ex);
                return null;
            }
        }

        public Int64 ObtenerNumeroPreImpreso(Usuario pUsuario)
        {
            try
            {
                return BAAuxilio.ObtenerNumeroPreImpreso(pUsuario);
            }
            catch
            {
                return 1;
            }
        }

        public void ModificarAuxilio_OrdenServ(Auxilio_Orden_Servicio pAuxi,ref string pError, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BAAuxilio.ModificarAuxilio_OrdenServ(pAuxi,ref pError, pUsuario);  
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioBusiness", "ConsultarAUX_OrdenServicio", ex);
            }
        }

        public SolicitudAuxilio Consultar_Auxilio_Variado(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BAAuxilio.Consultar_Auxilio_Variado(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioBusiness", "Consultar_Auxilio_Variado", ex);
                return null;
            }
        }


        public void Generar_desembolso_auxilio(SolicitudAuxilio pAuxilio,DesembolsoAuxilio pTran_Aux,ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DesembolsoAuxilioData BADesembolso = new DesembolsoAuxilioData();
                    //MODIFICAR AUXILIO
                    pAuxilio = BAAuxilio.Generar_desembolso_auxilio(pAuxilio, vUsuario);
                    //GRABAR TRAN_AUXILIO
                    pTran_Aux = BADesembolso.CrearTran_Auxilio(pTran_Aux, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
            }
        }

    }
}
