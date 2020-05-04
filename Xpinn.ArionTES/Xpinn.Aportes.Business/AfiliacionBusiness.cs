using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Riesgo.Data;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Business;

namespace Xpinn.Aportes.Business
{
    /// <summary>
    /// Objeto de negocio para Beneficiario
    /// </summary>
    public class AfiliacionBusiness : GlobalBusiness
    {
        private AfiliacionData DAAfiliacion;

        public AfiliacionBusiness()
        {
            DAAfiliacion = new AfiliacionData();
        }

        public Afiliacion CrearPersonaAfiliacion(Afiliacion pAfiliacion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAfiliacion = DAAfiliacion.CrearPersonaAfiliacion(pAfiliacion, pUsuario);

                    SarlaftAlertaBusiness BOAlerta = new SarlaftAlertaBusiness();
                    SarlaftAlerta saAlerta = new SarlaftAlerta();
                    SarlaftAlertaData DAAlerta = new SarlaftAlertaData();
                    saAlerta.idalerta = 0;
                    saAlerta.fecha_alerta = DateTime.Now;
                    saAlerta.cod_usuario = Convert.ToInt32(pUsuario.codusuario);
                    saAlerta.tipo_alerta = 1;
                    saAlerta.cod_persona = pAfiliacion.cod_persona;
                    saAlerta.descripcion = BOAlerta._alertas[1];
                    saAlerta.fechacrea = DateTime.Now;
                    saAlerta.estado = "P";

                    if (pAfiliacion.Es_PEPS)
                    {
                        saAlerta.tipo_alerta = 1;
                        saAlerta.descripcion = BOAlerta._alertas[1];
                        DAAlerta.CrearSarlaftAlerta(saAlerta, pUsuario);
                    }

                    //Agregado para generar alerta en caso de ser asociado especial
                    if (pAfiliacion.cod_asociado_especial != null && pAfiliacion.cod_asociado_especial != 0)
                    {
                        saAlerta.tipo_alerta = 2;
                        saAlerta.descripcion = BOAlerta._alertas[2];
                        DAAlerta.CrearSarlaftAlerta(saAlerta, pUsuario);
                    }

                    //Agregado para determinar el perfil de riesgo
                    //Determinar perfil de riesgo de la persona
                    Riesgo.Entities.Perfil pPerfil = new Riesgo.Entities.Perfil();
                    Riesgo.Data.PerfilData DAPerfil = new Riesgo.Data.PerfilData();
                    pPerfil.cod_persona = pAfiliacion.cod_persona;
                    DAPerfil.CrearPerfilPersona(null, pAfiliacion, false, pUsuario);

                    ts.Complete();
                }

                return pAfiliacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "CrearPersonaAfiliacion", ex);
                return null;
            }
        }

        public Int64 crearcausacionafiliacion(Afiliacion pAfiliacion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
                    // CREAR OPERACION
                    pOperacion.cod_ope = 0;
                    pOperacion.tipo_ope = 47;
                    pOperacion.cod_caja = 0;
                    pOperacion.cod_cajero = 0;
                    pOperacion.observacion = "Operacion-Causacion";
                    pOperacion.cod_proceso = null;
                    pOperacion.fecha_oper = DateTime.Now;
                    pOperacion.fecha_calc = DateTime.Now;
                    pOperacion.cod_ofi = pUsuario.cod_oficina;
                    
                    pOperacion = DAOperacion.GrabarOperacion(pOperacion, pUsuario);
                    pAfiliacion.cod_ope = pOperacion.cod_ope;
                    pAfiliacion = DAAfiliacion.crearcausacionafiliacion(pOperacion, pAfiliacion, pUsuario);
                    
                    

                    ts.Complete();
                }

                return pAfiliacion.cod_ope;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "crearcausacionafiliacion", ex);
                return 0;
            }
        }

        public Afiliacion ModificarPersonaAfiliacion(Afiliacion pAfiliacion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAfiliacion = DAAfiliacion.ModificarPersonaAfiliacion(pAfiliacion, pUsuario);

                    if (pAfiliacion.Es_PEPS)
                    {
                        SarlaftAlertaBusiness BOAlerta = new SarlaftAlertaBusiness();
                        SarlaftAlertaData DAAlerta = new SarlaftAlertaData();
                        SarlaftAlerta saAlerta = new SarlaftAlerta();
                        saAlerta.idalerta = 0;
                        saAlerta.fecha_alerta = DateTime.Now;
                        saAlerta.cod_usuario = Convert.ToInt32(pUsuario.codusuario);
                        saAlerta.tipo_alerta = 1;
                        saAlerta.cod_persona = pAfiliacion.cod_persona;
                        saAlerta.descripcion = BOAlerta._alertas[1]; 
                        saAlerta.fechacrea = DateTime.Now;
                        saAlerta.estado = "P";
                        saAlerta.consulta = pAfiliacion.consultaRIESGO;
                        DAAlerta.CrearSarlaftAlerta(saAlerta, pUsuario);
                    }

                    //Agregado para generar alerta en caso de ser asociado especial
                    if (pAfiliacion.cod_asociado_especial != null && pAfiliacion.cod_asociado_especial != 0)
                    {
                        SarlaftAlertaBusiness BOAlerta = new SarlaftAlertaBusiness();
                        SarlaftAlertaData DAAlerta = new SarlaftAlertaData();
                        SarlaftAlerta saAlerta = new SarlaftAlerta();
                        saAlerta.idalerta = 0;
                        saAlerta.fecha_alerta = DateTime.Now;
                        saAlerta.cod_usuario = Convert.ToInt32(pUsuario.codusuario);
                        saAlerta.tipo_alerta = 2;
                        saAlerta.cod_persona = pAfiliacion.cod_persona;
                        saAlerta.descripcion = BOAlerta._alertas[2];
                        saAlerta.fechacrea = DateTime.Now;
                        saAlerta.estado = "P";
                        DAAlerta.CrearSarlaftAlerta(saAlerta, pUsuario);
                    }


                    ts.Complete();
                }

                return pAfiliacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ModificarPersonaAfiliacion", ex);
                return null;
            }
        }        

        public Afiliacion ConsultarAfiliacion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                
                Afiliacion afiliacion = DAAfiliacion.ConsultarAfiliacion(pId, vUsuario);
                afiliacion.FechaActualizacion = DAAfiliacion.ConsultarActualziacionDtos(pId, vUsuario);
                return afiliacion;


            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarAfiliacion", ex);
                return null;
            }
        }

        public DataTable ConsultarAfiliados_GarantiasComunitarias(DateTime pFechaCorte , Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.ConsultarAfiliados_GarantiasComunitarias(pFechaCorte, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarAfiliacion", ex);
                return null;
            }
        }

        public DateTime? FechaInicioAfiliacion(DateTime pFechaDesc, Int64 pCodEmpresaDesc, Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.FechaInicioAfiliacion(pFechaDesc, pCodEmpresaDesc, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "FechaInicioAfiliacion", ex);
                return null;
            }
        }

        public List<Afiliacion> listarpersonaafiliacion(Afiliacion pAfiliacion, Usuario vUsuario)
        {
            try
            {
                   return DAAfiliacion.listarpersonaafiliacion(pAfiliacion, vUsuario);
            }
            catch (Exception ex)
            {
                    BOExcepcion.Throw("AfiliacionBusiness", "ModificarPersonaAfiliacion", ex);
                    return null;
            }
        }

        public List<Afiliacion> ConsultarAportes(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.ConsultarAportes(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarAportes", ex);
                return null;
            }
        }

        //consulta para controlar la cantidad de realifialiciones permitidas
        public int ConsultarReafilPerm(Afiliacion pAfiliacion, Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.ConsultarReafilPerm(pAfiliacion, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsultarReafilPerm", "ConsultarReafilPerm", ex);
                return 0;         
            }
        }

        public bool ModificarReafiliacion(Afiliacion pAfiliacion, List<Afiliacion> lstAportes, ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //MODIFICANDO REAFILIACION
                    pAfiliacion = DAAfiliacion.ModificarReafiliacion(pAfiliacion, vUsuario);
                    //ACTUALIZANDO LOS APORTES
                    if (lstAportes.Count > 0)
                    {
                        foreach (Afiliacion nAfili in lstAportes)
                        {
                            Afiliacion pEntidad = new Afiliacion();
                            pEntidad = DAAfiliacion.ModificarAportes(nAfili, vUsuario);
                        }
                    }
                    //INSERTANDO HISTORICO
                    pAfiliacion = DAAfiliacion.InsertarHistoReafili(pAfiliacion, vUsuario);
                    ts.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return false;
            }
        }
        


        public List<Estado_Persona> ListarEstadoPersona(Estado_Persona pEstado, Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.ListarEstadoPersona(pEstado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ListarEstadoPersona", ex);
                return null;
            }
        }

        public List<PersonaActualizacion> ListarDataPersonasXactualizar(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.ListarDataPersonasXactualizar(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ListarDataPersonasXactualizar", ex);
                return null;
            }
        }

        public string ConsultarEstadoAfiliacion(string identificacion, Usuario usuario)
        {
            try
            {
                return DAAfiliacion.ConsultarEstadoAfiliacion(identificacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarEstadoAfiliacion", ex);
                return null;
            }
        }

        public string ConsultarEstadoAfiliacion(Int64 pCodPersona, Usuario usuario)
        {
            try
            {
                return DAAfiliacion.ConsultarEstadoAfiliacion(pCodPersona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarEstadoAfiliacion", ex);
                return null;
            }
        }

        public List<TranAfiliacion> ListarMovAfiliacion(TranAfiliacion pTranAfiliacion, Usuario pUsuario)
        {
            try
            {
                return DAAfiliacion.ListarMovAfiliacion(pTranAfiliacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ListarMovAfiliacion", ex);
                return null;
            }
        }

        public bool Eliminar_PersonaResponsable(PersonaResponsable pPersona, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    bool confirmacion = false;
                    confirmacion = DAAfiliacion.Eliminar_PersonaResponsable(pPersona, vUsuario);
                    ts.Complete();

                    return confirmacion;
                }                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "Eliminar_PersonaResponsable", ex);
                return false;
            }
        }

        public PersonaResponsable ConsultarPersonaResponsable(string pFiltro, Usuario vUsuario)
        {
            try
            {
                PersonaResponsable pEntidad = new PersonaResponsable();                
                pEntidad = DAAfiliacion.ConsultarPersonaResponsable(pFiltro, vUsuario);
                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarPersonaResponsable", ex);
                return null;
            }
        }

        public List<SolicitudPersonaAfi> ListarDataSolicitudAfiliacion(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.ListarDataSolicitudAfiliacion(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ListarDataSolicitudAfiliacion", ex);
                return null;
            }
        }
        public ParametrizacionProcesoAfiliacion consultarTextoCorreo(Int32 cod_proceso, Int64 cod_per, Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.consultarTextoCorreo(cod_proceso, cod_per, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "consultarTextoCorreo", ex);
                return null;
            }
        }
        public ParametrizacionProcesoAfiliacion controlRutaAfiliacion(ParametrizacionProcesoAfiliacion control, Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.controlRutaAfiliacion(control, vUsuario); 
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ListarDataSolicitudAfiliacion", ex);
                return null;
            }
        }
        public Int64 ConfirmacionSolicitudAfiliacion(SolicitudPersonaAfi solicitud, ref string pError, Usuario vUsuario)
        {
            Int64 cod = 0;
            try
            {
                PersonaActDatosData DAPersonaUpdate = new PersonaActDatosData();
                Xpinn.Aportes.Entities.PersonaActDatos pPersonaActualizacion = new Aportes.Entities.PersonaActDatos();
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    cod = DAAfiliacion.ConfirmacionSolicitudAfiliacion(solicitud, ref pError, vUsuario);
                    ts.Complete();
                }
                cod = DAAfiliacion.consultarCodigoPersona(solicitud.id_persona, vUsuario);
                return cod;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return 0;
            }
        }

        public void EliminarSolicitudAfiliacion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAfiliacion.EliminarSolicitudAfiliacion(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "EliminarSolicitudAfiliacion", ex);
            }
        }

        public void EliminarPersonaParentesco(long consecutivoParaBorrar, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAfiliacion.EliminarPersonaParentesco(consecutivoParaBorrar, usuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "EliminarPersonaParentesco", ex);
            }
        }

        public List<PersonaParentescos> ListarParentescoDeUnaPersona(long codigoPersona, Usuario usuario)
        {
            try
            {
                return DAAfiliacion.ListarParentescoDeUnaPersona(codigoPersona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ListarParentescoDeUnaPersona", ex);
                return null;
            }
        }

        public PersonaParentescos ConsultarParentescoDeUnaPersona(long IdPersona, Usuario usuario)
        {
            try
            {
                return DAAfiliacion.ConsultarParentescoDeUnaPersona(IdPersona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarParentescoDeUnaPersona", ex);
                return null;
            }
        }

        public void CrearPersonaParentesco(PersonaParentescos parentesco, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAfiliacion.CrearPersonaParentesco(parentesco, usuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "CrearPersonaParentesco", ex);
            }
        }

        public void ModificarPersonaParentesco(PersonaParentescos parentesco, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAfiliacion.ModificarPersonaParentesco(parentesco, usuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ModificarPersonaParentesco", ex);
            }
        }

        public SolicitudPersonaAfi ConsultarSolicitudAfiliacion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.ConsultarSolicitudAfiliacion(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarSolicitudAfiliacion", ex);
                return null;
            }
        }

        public DateTime? FechaAfiliacion(string pFechaDesc, Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.FechaAfiliacion(pFechaDesc, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "FechaAfiliacion", ex);
                return null;
            }
        }
   
        public List<Afiliacion> ListarReafiliaciones(Afiliacion pReafiliacion, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAAfiliacion.ListarReafiliaciones(pReafiliacion, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ListarReafiliaciones", ex);
                return null;
            }
        }

        public int ConsultarCantidadAfiliados(string pCondicion, Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.ConsultarCantidadAfiliados(pCondicion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroBusiness", "ConsultarCantidadAfiliados", ex);
                return 0;
            }
        }

        public List<ConsultarPersonaBasico> ListarPersonasAfiliadasPaginado(string pCondicion, int pIndicePagina, int pRegistrosPagina, Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.ListarPersonasAfiliadasPaginado(pCondicion, pIndicePagina, pRegistrosPagina, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroBusiness", "ListarPersonasAfiliadasPaginado", ex);
                return null;
            }
        }


        public List<ConsultarPersonaBasico> ListarPersonasOficinaVirtual(Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.ListarPersonasOficinaVirtual(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroBusiness", "ListarPersonasOficinaVirtual", ex);
                return null;
            }
        }

        

        public LineaAporte ConsultarLineaObligatoria(Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.ConsultarLineaObligatoria(vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarLineaObligatoria", ex);
                return null;
            }
        }
        public List<TipoInfAdicional> lstTipoInfAdicional(Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.lstTipoInfAdicional(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "lstTipoInfAdicional", ex);
                return null;
            }
        }
        public DataTable ConsultarAfiliados_GarantiasComunitarias(DateTime pFechaCorte, Usuario vUsuario, string TipInfAdicional)
        {
            try
            {
                return DAAfiliacion.ConsultarAfiliados_GarantiasComunitarias(pFechaCorte, vUsuario, TipInfAdicional);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarAfiliacion", ex);
                return null;
            }
        }

        public Afiliacion ConsultarCierrePersonas(Usuario vUsuario)
        {
            try
            {
                return DAAfiliacion.ConsultarCierrePersonas(vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarCierrePersonas", ex);
                return null;
            }
        }

        /// <summary>
        /// Crea un registro de un proceso de afiliación 
        /// </summary>
        /// <param name="pProceso">Objeto a registrar</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public ProcesoAfiliacion CrearProceso(ProcesoAfiliacion pProceso, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProceso = DAAfiliacion.CrearProceso(pProceso, pUsuario);
                    ts.Complete();
                }

                return pProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "CrearProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un registro de un proceso de afiliación 
        /// </summary>
        /// <param name="pProceso">Objeto a modificar</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public ProcesoAfiliacion ModificarProceso(ProcesoAfiliacion pProceso, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProceso = DAAfiliacion.ModificarProceso(pProceso, pUsuario);
                    ts.Complete();
                }

                return pProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ModificarProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar datos de procesos basado en un filtro
        /// </summary>
        /// <param name="filtro">Filtro del listado</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<ProcesoAfiliacion> ListarProcesos(string filtro, Usuario pUsuario)
        {
            try
            {
                return DAAfiliacion.ListarProcesos(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ListarProcesos", ex);
                return null;
            }
        }

        /// <summary>
        /// Consultar los datos de un proceso en especifico
        /// </summary>
        /// <param name="Proceso">Objeto para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public ProcesoAfiliacion ConsultarProceso(ProcesoAfiliacion Proceso, Usuario pUsuario)
        {
            try
            {
                return DAAfiliacion.ConsultarProceso(Proceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarProceso", ex);
                return null;
            }
        }

    }
}