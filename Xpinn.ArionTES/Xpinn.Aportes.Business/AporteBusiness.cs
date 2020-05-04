using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
using System.IO;
using Xpinn.Tesoreria.Entities;
using Xpinn.Comun.Data;
using Xpinn.Comun.Entities;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Aportes.Business
{
    /// <summary>
    /// Objeto de negocio para Aporte
    /// </summary>
    public class AporteBusiness : GlobalBusiness
    {
        private AporteData DAAporte;

        /// <summary>
        /// Constructor del objeto de negocio para Aporte
        /// </summary>
        public AporteBusiness()
        {
            DAAporte = new AporteData();
        }

        public Aporte CrearCruceCuentas(Aporte pAporte, Xpinn.Tesoreria.Entities.Operacion pOperacion, Xpinn.FabricaCreditos.Entities.Giro pGiro, List<DetalleCruce> pDetallesCruce, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    //CREAR LA OPERACION 
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
                    vOpe = DAOperacion.GrabarOperacion(pOperacion, vUsuario);
                    if (vOpe.cod_ope == 0)
                    {
                        ts.Dispose();
                        return null;
                    }

                    //1.GRABAR PERSONA RETIRO
                    pAporte.cod_ope = vOpe.cod_ope;
                    pAporte = DAAporte.CrearCruceCuentas(pAporte, vUsuario);

                    //2.GRABAR NUEVO GIRO
                    Xpinn.FabricaCreditos.Data.AvanceData GiroData = new Xpinn.FabricaCreditos.Data.AvanceData();
                    Xpinn.FabricaCreditos.Entities.Giro vGiroEntidad = new Xpinn.FabricaCreditos.Entities.Giro();
                    pGiro.cod_ope = vOpe.cod_ope;
                    if (pGiro.valor >= 1)
                    {
                        vGiroEntidad = GiroData.CrearGiro(pGiro, vUsuario, 1);
                    }

                    //APLICAR CRUCE                     
                    pAporte = DAAporte.AplicarCruceCuentas(pAporte, vUsuario);

                    //GUARDAR DETALLES
                    DetalleCruceData DADetalleCruce = new DetalleCruceData();
                    foreach (DetalleCruce objDetalle in pDetallesCruce)
                    {
                        objDetalle.Cod_ope = pAporte.cod_ope;
                        DADetalleCruce.CrearDetalleCruce(objDetalle, vUsuario);
                    }

                    ts.Complete();
                }

                return pAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "CrearCruceCuentas" , ex);
                return null;
            }
        }

        public Aporte CrearNovedadCambio(Aporte vAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    vAporte = DAAporte.CrearNovedadCambio(vAporte, pUsuario);

                    ts.Complete();
                }

                return vAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "CrearNovedadCambio", ex);
                return null;
            }
        }
        public string ClasificacionAporte(Int64 idPersona, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    return DAAporte.ClasificacionAporte(idPersona, pUsuario);


                }


            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ClasificacionAporte", ex);
                return null;
            }
        }
        public Aporte ClasificarPorDiasMora(Aporte vAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    vAporte = DAAporte.ClasificarPorDiasMora(vAporte, pUsuario);

                    ts.Complete();
                }

                return vAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ClasificarPorDiasMora", ex);
                return null;
            }
        }

        public List<DetalleCruce> ConsultarDetalleCruce(long pCodPersona, Usuario pUsuario)
        {
            try
            {
                DetalleCruceData DADetalleCruce = new DetalleCruceData();
                return DADetalleCruce.ConsultarDetallesCruce(pCodPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarGrupoAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Crea un Aporte
        /// </summary>
        /// <param name="pAporte">Entidad Aporte</param>
        /// <returns>Entidad Aporte creada</returns>
        public Aporte CrearAporte(Aporte pAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAporte = DAAporte.CrearAporte(pAporte, pUsuario);

                    BeneficiarioData objBeneficiarioDATA = new BeneficiarioData();
                    foreach (Beneficiario objBeneficiario in pAporte.lstBeneficiarios)
                    {
                        if (objBeneficiario.nombre_ben != "" && objBeneficiario.identificacion_ben != "")
                        {
                            objBeneficiario.numero_cuenta = Convert.ToString(pAporte.numero_aporte);
                            if (objBeneficiario.idbeneficiario > 0)
                                objBeneficiarioDATA.ModificarBeneficiarioAporte(objBeneficiario, pUsuario);
                            else
                                objBeneficiarioDATA.CrearBeneficiarioAporte(objBeneficiario, pUsuario);
                        }
                    }

                    ts.Complete();
                }

                return pAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "CrearAporte", ex);
                return null;
            }
        }

        public List<Aporte> ListarAportesNovedadesCambio(string filtro, Usuario usuario)
        {
            try
            {
                return DAAporte.ListarAportesNovedadesCambio(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarAportesNovedadesCambio", ex);
                return null;
            }
        }

        public List<Aporte> ListarTiposConNovedad(Usuario usuario)
        {
            try
            {
                return DAAporte.ListarTiposConNovedad(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarAportesNovedadesCambio", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Aporte
        /// </summary>
        /// <param name="pAporte">Entidad Aporte</param>
        /// <returns>Entidad Aporte modificada</returns>
        public Aporte ModificarAporte(Aporte pAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAporte = DAAporte.ModificarAporte(pAporte, pUsuario);

                    BeneficiarioData objBeneficiarioDATA = new BeneficiarioData();
                    if (pAporte.lstBeneficiarios != null)
                    {
                        if (pAporte.lstBeneficiarios.Count > 0)
                        {
                        foreach (Beneficiario objBeneficiario in pAporte.lstBeneficiarios)
                        {
                            if (objBeneficiario.nombre_ben != "" && objBeneficiario.identificacion_ben != "")
                            {
                                objBeneficiario.numero_cuenta = Convert.ToString(pAporte.numero_aporte);
                                if (objBeneficiario.idbeneficiario > 0)
                                    objBeneficiarioDATA.ModificarBeneficiarioAporte(objBeneficiario, pUsuario);
                                else
                                    objBeneficiarioDATA.CrearBeneficiarioAporte(objBeneficiario, pUsuario);
                            }
                        }
                            }
                    else
                    {
                        objBeneficiarioDATA.EliminarTodosBeneficiariosAporte(pAporte.numero_aporte, pUsuario);
                    }
                    }
                    //DAAporte.CrearNovedadCambio(pAporte, pUsuario); 
                    //Se quito esta opción ya que se debe insertar en esta tabla es cuando necesita confirmación como cuando se solicita el cambio desde atención web 
                    //el derecho del proceso es insertar el cambio directamente 
                    ts.Complete();
                }
                return pAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ModificarAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Aporte
        /// </summary>
        /// <param name="pId">Identificador de Aporte</param>
        public void EliminarAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAporte.EliminarAporte(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "EliminarAporte", ex);
            }
        }

        /// <summary>
        /// Obtiene un Aporte
        /// </summary>
        /// <param name="pId">Identificador de Aporte</param>
        /// <returns>Entidad Aporte</returns>
        public Aporte ConsultarAporte(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Aporte Aporte = new Aporte();

                Aporte = DAAporte.ConsultarAporte(pId, vUsuario);
                BeneficiarioData DABeneficiario = new BeneficiarioData();
                Aporte.lstBeneficiarios = DABeneficiario.ConsultarBeneficiariosAporte(pId, vUsuario);

                return Aporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarAporte", ex);
                return null;
            }
        }

        public void ModificarNovedadCuotaAporte(Aporte aporte, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAporte.ModificarNovedadCuotaAporte(aporte, usuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ModificarNovedadCuotaAporte", ex);
            }
        }


        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarCruceCuentas(Aporte entidad, Usuario pUsuario)
        {
            try
            {
                return DAAporte.ListarCruceCuentas(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarCruceCuentas", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Aporte
        /// </summary>
        /// <param name="pId">Identificador de Aporte</param>
        /// <returns>Entidad GrupoAporte</returns>
        public Aporte ConsultarGrupoAporte(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Aporte Aporte = new Aporte();

                Aporte = DAAporte.ConsultarGrupoAporte(pId, vUsuario);

                return Aporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarGrupoAporte", ex);
                return null;
            }
        }


        public List<Aporte> ConsultarCuentasPorGrupoAporte(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DAAporte.ConsultarCuentasPorGrupoAporte(pId, vUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarCuentasPorGrupoAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Aporte
        /// </summary>
        /// <param name="pId">Identificador de Aporte</param>
        /// <returns>Entidad Aporte</returns>
        public Aporte ConsultarCliente(String pId, Usuario pUsuario)
        {
            try
            {
                Aporte Aporte = new Aporte();

                Aporte = DAAporte.ConsultarCliente(pId, pUsuario);

                return Aporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarCliente", ex);
                return null;
            }
        }


        public decimal ConsultarClienteSalario(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DAAporte.ConsultarClienteSalario(pId, vUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarClienteSalario", ex);
                return 0;
            }
        }


        public decimal Calcular_Cuota(decimal Salario, decimal Porcentaje, decimal Periodicidad, Usuario vUsuario)
        {
            try
            {
                return DAAporte.Calcular_Cuota(Salario, Porcentaje, Periodicidad, vUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarClienteSalario", ex);
                return 0;
            }
        }

        public decimal ConsultarSMLMV(Usuario vUsuario)
        {
            try
            {
                return DAAporte.ConsultarSMLMV(vUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarSMLMV", ex);
                return 0;
            }
        }

        /// Obtiene un Aporte
        /// </summary>
        /// <param name="pId">Identificador de Aporte</param>
        /// <returns>Entidad Aporte</returns>
        public Aporte ConsultarClienteAporte(String pId, Int32 cod_Linea, Usuario vUsuario)
        {
            try
            {
                Aporte Aporte = new Aporte();

                Aporte = DAAporte.ConsultarClienteAporte(pId, cod_Linea, vUsuario);

                return Aporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarClienteAporte", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarAporte(Aporte pAporte, Usuario pUsuario)
        {
            try
            {
                return DAAporte.ListarAporte(pAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarAporte", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<MovimientoAporte> ListarMovAporte(Int64 pNumeroAporte, DateTime? pfechaInicial, DateTime? pfechaFinal, Usuario pUsuario)
        {
            try
            {
                return DAAporte.ListarMovimiento(pNumeroAporte, pfechaInicial, pfechaFinal, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarMovAporte", ex);
                return null;
            }
        }

        public List<Aporte> ListarRetiros(string pFiltro, DateTime pFechaReti, Usuario vUsuario)
        {
            try
            {
                return DAAporte.ListarRetiros(pFiltro, pFechaReti, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarRetiros", ex);
                return null;
            }
        }

        public List<Aporte> ListarSolicitudRetiro(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAAporte.ListarSolicitudRetiro(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarSolicitudRetiro", ex);
                return null;
            }
        }


        public bool ModificarEstadoSolicitud(Aporte pAporte, Usuario pUsuario)
        {
            try
            {
                return DAAporte.ModificarEstadoSolicitud(pAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ModificarEstadoSolicitud", ex);
                return false;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarAperturaAporte(Aporte pAporte, Usuario pUsuario, String Orden)
        {
            try
            {
                return DAAporte.ListarAperturaAporte(pAporte, pUsuario, Orden);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarAperturaAporte", ex);
                return null;
            }
        }
        public List<Aporte> ListarDiasCategoria(int cod_clasifica, Usuario pUsuario)
        {
            try
            {
                return DAAporte.ListarDiasCategoria(cod_clasifica, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarDiasCategoria", ex);
                return null;
            }
        }
        // <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarEstadoCuentaAporte(Int64 pCliente, int? pEstadoAporte, DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return DAAporte.ListarEstadoCuentaAporte(pCliente, pEstadoAporte, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarEstadoCuentaAporte", ex);
                return null;
            }
        }

        public List<Aporte> ListarEstadoCuentaAportestodos(Int64 pCliente, string pEstadoAporte, DateTime pFecha, Usuario pUsuario, int EstadoCuenta = 0)
        {
            try
            {
                return DAAporte.ListarEstadoCuentaAporteTodos(pCliente, pEstadoAporte, pFecha, pUsuario, EstadoCuenta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarEstadoCuentaAportestodos", ex);
                return null;
            }
        }

        public List<Aporte> ListarEstadoCuentaAportePermitePago(Int64 pCliente, DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return DAAporte.ListarEstadoCuentaAportePermitePago(pCliente, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarEstadoCuentaAportePermitePago", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarDistribucionAporte(Usuario pUsuario, Int64 cliente, string pCod_linea_aporte)
        {
            try
            {
                return DAAporte.ListarDistribucionAporte(pUsuario, cliente, pCod_linea_aporte);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarDistribucionAporte", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarDistrAporCambiarCuota(Usuario pUsuario, Int64 cliente)
        {
            try
            {
                return DAAporte.ListarDistrAporCambiarCuota(pUsuario, cliente);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarDistrAporCambiarCuota", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarDistribucionAporteNuevo(Usuario pUsuario, Int64 pGrupo)
        {
            try
            {
                return DAAporte.ListarDistribucionAporteNuevo(pUsuario, pGrupo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarDistribucionAporteNuevo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de LineaAporte obtenidos</returns>
        public List<Aporte> ListarLineaAporte(Aporte pAporte, Usuario pUsuario)
        {
            try
            {
                return DAAporte.ListarLineaAporte(pAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarLineaAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de  obtenidos</returns>
        public List<Aporte> ListarTipoIdentificacion(Aporte pAporte, Usuario pUsuario)
        {
            try
            {
                return DAAporte.ListarTipoIdentificacion(pAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarTipoIdentificacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de  obtenidos</returns>
        public List<Aporte> ListarTipoRetiro(Aporte pAporte, Usuario pUsuario)
        {
            try
            {
                return DAAporte.ListarTipoRetiro(pAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarTipoRetiro", ex);
                return null;
            }
        }

        public List<Aporte> ListarCuentasPersona(Int64 pCod_Persona, Usuario vUsuario)
        {
            try
            {
                return DAAporte.ListarCuentasPersona(pCod_Persona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarCuentasPersona", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Aporte
        /// </summary>
        /// <param name="pId">Identificador de Aporte</param>
        /// <returns>Entidad Aporte</returns>
        public Aporte ConsultarMaxAporte(Usuario vUsuario)
        {
            try
            {
                Aporte Aporte = new Aporte();

                Aporte = DAAporte.ConsultarMaxAporte(vUsuario);

                return Aporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarMaxAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Crea un Aporte
        /// </summary>
        /// <param name="pAporte">Entidad Aporte</param>
        /// <returns>Entidad Aporte creada</returns>
        public Aporte CrearRetiroAporte(Aporte pAporte, Xpinn.Tesoreria.Entities.Operacion pOperacion, long formadesembolso,
            int idCtaBancaria, int cod_banco, string numerocuenta, int tipo_cuenta, ref string Error, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                    pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref Error, pUsuario);
                    if (Error.Trim() == "")
                    {
                        pAporte.cod_ope = pOperacion.cod_ope;
                        pAporte = DAAporte.CrearRetiroAporte(pAporte, pUsuario);
                        DAAporte.GuardarGiro(pAporte.numero_aporte, pAporte.cod_ope, formadesembolso, pAporte.fecha_retiro, pAporte.valor_retiro,
                            idCtaBancaria, cod_banco, numerocuenta, tipo_cuenta, Convert.ToInt64(pAporte.cod_persona), pUsuario.nombre, pUsuario);
                        ts.Complete();
                    }
                    else
                    {
                        ts.Complete();
                        return null;
                    }
                }

                return pAporte;
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return null;
            }
        }
        public String[] getRegistroBusiness(Usuario pUsuario, String pIdentificacion)
        {
            try
            {
                return DAAporte.getRegistro(pUsuario, pIdentificacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarMaxAporte", ex);
                return null;
            }
        }

        public void UpdateInsertEstadoBussines(Int64 idconsecutivo, Int64 idAfilia, Int64 CodPerson, String estadoAn, DateTime fechaDeCambio, String estadoN, Int64 CodMoNue, String Observa, Usuario pUsuario)
        {
            try
            {
                Int64 cod = 0;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAporte.UpdateEstadoPersonaF(ref cod, idAfilia, CodPerson, estadoN, CodMoNue, pUsuario);
                    HistoricoCamEstado historicoEstado = new HistoricoCamEstado();
                    // Actualizar histórico de cambio de estados
                    historicoEstado.idafiliacion = idAfilia;
                    historicoEstado.cod_persona = CodPerson;
                    historicoEstado.estado_anterior = estadoAn;
                    if (cod == 0)
                        historicoEstado.cod_motivo_anterior = null;
                    else
                        historicoEstado.cod_motivo_anterior = cod;
                    historicoEstado.fecha_cambio = fechaDeCambio;
                    historicoEstado.estado_nuevo = estadoN;
                    historicoEstado.cod_motivo_nuevo = CodMoNue;
                    historicoEstado.observaciones = Observa;
                    historicoEstado.fecha = DateTime.Now;
                    historicoEstado.cod_usuario = pUsuario.codusuario;
                    DAAporte.CrearHistorioCamEstado(historicoEstado, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "UpdateInsertEstadoBussines", ex);
            }
        }

        public bool? ValidarFechaSolicitudCambio(Aporte pAporte, Usuario usuario)
        {
            try
            {
                return DAAporte.ValidarFechaSolicitudCambio(pAporte, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ValidarFechaSolicitudCambio", ex);
                return null;
            }
        }

        public string ValidarAporte(Aporte pAporte,Usuario usuario)
        {
            try
            {
                return DAAporte.ValidarAporte(pAporte,usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ValidarAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Credito
        /// </summary>
        /// <param name="pCredito">Entidad Credito</param>
        /// <returns>Entidad Credito modificada</returns>
        /// public void GuardarDatos2 (int codpersona,int numerocuenta,int tipocuenta,int cod_banco)
        public void GuardarGiro(Int64 numero_radicacion, Int64 cod_ope, long formadesembolso, DateTime fecha_desembolso, double monto,
            int idCtaBancaria, int cod_banco, string numerocuenta, int tipo_cuenta, Int64 codperson, string usuario, Usuario pUsuario)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                DAAporte.GuardarGiro(numero_radicacion, cod_ope, formadesembolso, fecha_desembolso, monto,
                    idCtaBancaria, cod_banco, numerocuenta, tipo_cuenta, codperson, usuario, pUsuario);
                ts.Complete();
            }
        }


        public List<Aporte> ListarAportesControl(string filtro, Usuario vUsuario)
        {
            try
            {
                return DAAporte.ListarAportesControl(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarAportesControl", ex);
                return null;
            }
        }
        public List<Aporte> ListarSaldos(Aporte persona, Usuario vUsuario)
        {
            try
            {
                return DAAporte.ListarSaldos(persona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarSaldos", ex);
                return null;
            }
        }

        public Aporte ConsultarPersonaRetiro(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return DAAporte.ConsultarPersonaRetiro(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarPersonaRetiro", ex);
                return null;
            }
        }

        public Aporte ConsultarTotalAportes(Int64 pId, DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return DAAporte.ConsultarTotalAportes(pId, pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarTotalAportes", ex);
                return null;
            }
        }

        private StreamReader strReader;
        public void CargaAportes(ref string pError, string sformato_fecha, Stream pstream, ref List<Aporte> lstAportes, ref List<ErroresCargaAportes> plstErrores, Usuario pUsuario)
        {
            Configuracion conf = new Configuracion();
            string sSeparadorDecimal = conf.ObtenerSeparadorDecimalConfig();

            string readLine;

            // Inicializar control de errores
            RegistrarError(-1, "", "", "", ref plstErrores);

            try
            {
                using (strReader = new StreamReader(pstream))
                {
                    //recorriendo las filas del archivo
                    Aporte pEntidad;
                    while (strReader.Peek() >= 0)
                    {
                        //BAJANDO LA FILA A UNA VARIABLE
                        readLine = strReader.ReadLine();
                        string Separador = "|";

                        //Separando la data a un array
                        string[] arrayline = readLine.Split(Convert.ToChar(Separador));
                        int contadorreg = 0;

                        pEntidad = new Aporte();
                        for (int i = 0; i <= 18; i++)
                        {
                            pEntidad.numero_aporte = 0;
                            if (i == 0) { try { pEntidad.cod_linea_aporte = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 1)
                            {
                                try
                                {
                                    if (arrayline[i].ToString().Trim() != "")
                                        pEntidad.cod_oficina = Convert.ToInt32(arrayline[i].ToString().Trim());
                                }
                                catch (Exception ex)
                                { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                            }
                            if (i == 2) { try { pEntidad.cod_persona = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 3) { try { pEntidad.fecha_apertura = DateTime.ParseExact(arrayline[i].ToString().Trim(), sformato_fecha, null); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 4) { try { pEntidad.cuota = Convert.ToDecimal(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 5) { try { pEntidad.cod_periodicidad = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 6) { try { pEntidad.forma_pago = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 7) { try { pEntidad.fecha_proximo_pago = DateTime.ParseExact(arrayline[i].ToString().Trim(), sformato_fecha, null); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 8)
                            {
                                try
                                {
                                    if (arrayline[i].ToString().Trim() != "")
                                        pEntidad.porcentaje_distribucion = Convert.ToDecimal(arrayline[i].ToString().Trim());
                                }
                                catch (Exception ex)
                                { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                            }
                            if (i == 9) { try { pEntidad.estado = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 10)
                            {
                                try
                                {
                                    if (arrayline[i].ToString().Trim() != "")
                                        pEntidad.cod_empresa = Convert.ToInt64(arrayline[i].ToString().Trim());
                                }
                                catch (Exception ex)
                                { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                            }

                            pEntidad.cod_usuario = Convert.ToInt32(pUsuario.codusuario);
                            pEntidad.fecha_crea = DateTime.Now;

                            contadorreg++;
                        }
                        lstAportes.Add(pEntidad);
                    }
                }
            }
            catch (IOException ex)
            {
                pError = ex.Message;
            }
        }


        public void RegistrarError(int pNumeroLinea, string pRegistro, string pError, string pDato, ref List<ErroresCargaAportes> plstErrores)
        {
            if (pNumeroLinea == -1)
            {
                plstErrores.Clear();
                return;
            }
            ErroresCargaAportes registro = new ErroresCargaAportes();
            registro.numero_registro = pNumeroLinea.ToString();
            registro.datos = pDato;
            registro.error = " Campo No.:" + pRegistro + " Error:" + pError;
            plstErrores.Add(registro);
        }


        public void CrearAporteImportacion(DateTime pFechaCarga, ref string pError, List<Aporte> lstAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstAporte != null && lstAporte.Count > 0)
                    {
                        foreach (Aporte nAporte in lstAporte)
                        {
                            nAporte.numero_aporte = 0;
                            nAporte.cod_usuario = Convert.ToInt32(pUsuario.codusuario);
                            nAporte.fecha_crea = pFechaCarga;
                            Aporte pEntidad = new Aporte();
                            pEntidad = DAAporte.CrearAporte(nAporte, pUsuario);
                        }
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
            }
        }

        public List<LiquidacionInteres> getAportesLiquidarBusinnes(DateTime pfechaLiquidacion, String pcodLinea, Usuario pusuario)
        {
            try
            {
                return DAAporte.getListaAportesLiquidar(pfechaLiquidacion, pcodLinea, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AportesBusiness", "getAportesLiquidarBusinnes", ex);
                return null;
            }
        }

        public void guardarDatosLiquidacion(List<Tran_Aportes> datosIntere, List<Tran_Aportes> datosRetafuentes, Operacion pOperacion, Usuario pUsuario, ref Int64 codigo, DateTime fechaoperacion)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                Xpinn.Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();
                Xpinn.Tesoreria.Entities.Operacion entidadop = new Tesoreria.Entities.Operacion();
                Xpinn.Aportes.Data.AporteData aporte = new Xpinn.Aportes.Data.AporteData();
                Xpinn.Aportes.Data.Pago_IntPermanenteData pago_intpermanente = new Xpinn.Aportes.Data.Pago_IntPermanenteData();
                Xpinn.Aportes.Entities.Pago_IntPermanente entidadPagopermanente = new Xpinn.Aportes.Entities.Pago_IntPermanente();
                Xpinn.Aportes.Entities.Aporte entidadAporte = new Xpinn.Aportes.Entities.Aporte();
                pOperacion.fecha_oper = Convert.ToDateTime(fechaoperacion.ToShortDateString());
                pOperacion.fecha_calc = Convert.ToDateTime(fechaoperacion.ToShortDateString());
                pOperacion.fecha_real = DateTime.Now;
                entidadop = operacion.GrabarOperacion(pOperacion, pUsuario);

                for (int i = 0; i < datosRetafuentes.Count && i < datosIntere.Count; i++)
                {
                    datosIntere[i].COD_OPE = entidadop.cod_ope;
                    datosRetafuentes[i].COD_OPE = entidadop.cod_ope;

                    if (datosIntere[i].VALOR != 0)
                        datosIntere[i].NUMERO_TRANSACCION = DAAporte.InsertLiquidacion(datosIntere[i].COD_OPE, datosIntere[i].NUMERO_APORTE, datosIntere[i].COD_CLIENTE, datosIntere[i].TIPO_TRAN, datosIntere[i].VALOR, pUsuario, datosIntere[i].ESTADO, datosIntere[i].Fecha_Interes);

                    if (datosRetafuentes[i].VALOR != 0)
                        DAAporte.InsertLiquidacion(datosRetafuentes[i].COD_OPE, datosRetafuentes[i].NUMERO_APORTE, datosRetafuentes[i].COD_CLIENTE, datosRetafuentes[i].TIPO_TRAN, datosRetafuentes[i].VALOR, pUsuario, datosRetafuentes[i].ESTADO, datosIntere[i].Fecha_Interes);

                    entidadAporte = aporte.ConsultarCuentaAporte(Convert.ToInt64(datosIntere[i].NUMERO_APORTE), pUsuario);
                    if (entidadAporte.pago_intereses == 2)
                    {
                        entidadPagopermanente.cod_persona = pUsuario.cod_persona;
                        entidadPagopermanente.estado = 0;
                        entidadPagopermanente.numero_aporte = Convert.ToInt64(datosIntere[i].NUMERO_APORTE);
                        entidadPagopermanente.cod_ope = datosIntere[i].COD_OPE;
                        entidadPagopermanente.valor = datosIntere[i].VALOR - datosRetafuentes[i].VALOR;
                        pago_intpermanente.CrearPago_IntPermanente(entidadPagopermanente, pUsuario);
                    }

                }

                ts.Complete();
            }
        }

        public LiquidacionInteres CrearLiquidacionAportes(LiquidacionInteres pLiqui, Usuario vUsuario)
        {
            try
            {
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                if (pLiqui.lstLista != null && pLiqui.lstLista.Count > 0)
                {

                    foreach (LiquidacionInteres rOpe in pLiqui.lstLista)
                    {
                        LiquidacionInteres nLiquidacio = new LiquidacionInteres();
                        nLiquidacio = DAAporte.CrearLiquidacionAportes(rOpe, vUsuario);
                    }

                }
                //    ts.Complete();
                //}
                return pLiqui;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AportesBusiness", "CrearLiquidacionAportes", ex);
                return null;
            }
        }

        public void GuardarLiquidacionAportes(ref Int64 COD_OPE, Xpinn.Tesoreria.Entities.Operacion pOperacion, LiquidacionInteres pLiqui, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //GRABACION DE LA OPERACION
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();

                    vOpe = DAOperacion.GrabarOperacion(pOperacion, vUsuario);
                    COD_OPE = vOpe.cod_ope;

                    if (pLiqui.lstLista != null && pLiqui.lstLista.Count > 0)
                    {

                        foreach (LiquidacionInteres rOpe in pLiqui.lstLista)
                        {

                            //GENERANDO LIQUIDACION INTERES  AHORROS
                            rOpe.cod_ope = vOpe.cod_ope;
                            DAAporte.GuardarLiquidacionAportes(rOpe, vUsuario);

                        }
                        ts.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AportesBusiness", "GuardarLiquidacionAportes", ex);
            }
        }

        public List<Aporte> ListarAporteReporteCierre(DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return DAAporte.ListarAporteReporteCierre(pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarAporteReporteCierre", ex);
                return null;
            }
        }

        public List<Aporte> ListarAportesClubAhorradores(Int64 pcliente, Boolean pResult, string pFiltroAdd, DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return DAAporte.ListarAportesClubAhorradores(pcliente, pResult, pFiltroAdd, pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarAportesClubAhorradores", ex);
                return null;
            }
        }
        public List<Cierea> ListarFechaCierreCausacion(Usuario pUsuario)
        {
            Xpinn.Comun.Business.FechasBusiness BOFechas = new Comun.Business.FechasBusiness();
            List<Cierea> LstCierre = new List<Cierea>();
            // Determinar la periodicidad de cierre
            int dias_cierre = 0;
            int tipo_calendario = 0;
            DAAporte.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
            // Determinar la fecha del último cierre realizado
            Cierea pCierre = new Cierea();
            pCierre.tipo = "W";
            pCierre.estado = "D";
            pCierre = DAAporte.FechaUltimoCierre(pCierre, "", pUsuario);
            if (pCierre == null)
            {
                int año = DateTime.Now.Year;
                int mes = DateTime.Now.Month;
                if (mes <= 1)
                {
                    mes = 12;
                    año = año - 1;
                }
                else
                {
                    mes = mes - 1;
                }
                pCierre = new Cierea();
                pCierre.fecha = new DateTime(año, mes, 1, 0, 0, 0).AddDays(-1);
            }
            DateTime FecIni = pCierre.fecha;
            if (FecIni == DateTime.MinValue)
                return null;
            if (FecIni > DateTime.Now.AddDays(15))
                return null;
            // Calcular fechas de cierre inicial
            DateTime FecFin = DateTime.MinValue;
            FecFin = BOFechas.FecSumDia(FecIni, dias_cierre, 1);

            if (dias_cierre == 30 || (dias_cierre == 15 && FecFin.Day > 15))
            {
                bool control = true;
                do
                {
                    FecFin = FecFin.AddDays(1);
                    if (FecFin.Day == 1)
                    {
                        FecFin = FecFin.AddDays(-1);
                        control = false;
                    }
                } while (control == true);
            }

            // Determinar los periodos pendientes por cerrar
            while (FecFin <= DateTime.Now.AddDays(15))
            {
                Cierea cieRea = new Cierea();
                cieRea.fecha = FecFin;
                LstCierre.Add(cieRea);
                FecIni = FecFin;
                FecFin = BOFechas.FecSumDia(FecIni, dias_cierre, 1);

                if (dias_cierre == 30 || (dias_cierre == 15 && FecFin.Day > 15))
                {
                    bool control = true;
                    do
                    {
                        FecFin = FecFin.AddDays(1);
                        if (FecFin.Day == 1)
                        {
                            FecFin = FecFin.AddDays(-1);
                            control = false;
                        }
                    } while (control == true);
                }
            }
            return LstCierre;
        }

        public List<provision_aportes> ListarProvision(DateTime pFechaIni, provision_aportes pProvision, Usuario vUsuario)
        {
            try
            {


                provision_aportes Traslado = new provision_aportes();
                Traslado.codusuario = vUsuario.codusuario;
                Traslado.cod_linea_aporte = pProvision.cod_linea_aporte;
                Traslado.cod_oficina = pProvision.cod_oficina;


                return DAAporte.ListarProvision(pFechaIni, pProvision, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarProvision", ex);
                return null;
            }
        }


        public void InsertarDatos(provision_aportes Insertar_cuenta, List<provision_aportes> lstInsertar, Xpinn.Tesoreria.Entities.Operacion pinsertar, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (Insertar_cuenta.numero_aporte != "")
                    {
                        //CREACION DE LA OPERACION
                        Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();

                        pinsertar = DAOperacion.GrabarOperacion(pinsertar, vUsuario);


                        //HACER EL INGRESO DE LA CUENTA. 
                        foreach (provision_aportes Insertar in lstInsertar)
                        {

                            provision_aportes pEntidad = new provision_aportes();

                            Insertar.idprovision = 0;
                            Insertar.cod_ope = pinsertar.cod_ope;


                            pEntidad = DAAporte.InsertarDatos(Insertar, vUsuario);
                        }
                    }

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "IngresoCuenta", ex);
                return;
            }

        }

        public void Crearcierea(Xpinn.Comun.Entities.Cierea pcierea, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DAAporte.Crearcierea(pcierea, vUsuario);
                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "Crearcierea", ex);
                return;
            }
        }


        public List<Aporte> RptLibroSocios(string pFecha, bool incluye_retirados, Usuario vUsuario)
        {
            try
            {
                return DAAporte.RptLibroSocios(pFecha, incluye_retirados, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "Crearcierea", ex);
                return null;
            }
        }



        public Aporte ConsultarCierreAportes(Usuario vUsuario)
        {
            try
            {
                Aporte Aporte = new Aporte();

                Aporte = DAAporte.ConsultarCierreAportes(vUsuario);

                return Aporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarCierreAportes", ex);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datosIntere"></param>
        /// <param name="pFecha"></param>
        /// <param name="pOperacion"></param>
        /// <param name="pUsuario"></param>
        public void CrearPagoIntereses(List<Pago_IntPermanente> lstIntereses, DateTime pFecha, ref Operacion pOperacion, Usuario pUsuario)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();
                Operacion entidadop = new Operacion();
                pOperacion.fecha_oper = pFecha;
                pOperacion.fecha_calc = pFecha;
                pOperacion.fecha_real = pFecha;
                pOperacion.tipo_ope = 113;
                entidadop = operacion.GrabarOperacion(pOperacion, pUsuario);

                foreach(Pago_IntPermanente pAporte in lstIntereses)
                {
                    pAporte.cod_ope = entidadop.cod_ope;
                    DAAporte.CrearTransaccionInteres(pAporte, pUsuario);
                }
                ts.Complete();
            }
        }

        public List<Pago_IntPermanente> ListarIntPermanenteRec(Int64 pNumeroRecaudo, Usuario vUsuario)
        {
            try
            {
                Pago_IntPermanenteData DAPagoInteres = new Pago_IntPermanenteData();
                return DAPagoInteres.ListarIntPermanenteRec(pNumeroRecaudo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarIntPermanenteRec", ex);
                return null;
            }
        }

        public bool EsAfiancol(Int64 pCodPersona, Usuario pUsuario)
        {
            return DAAporte.EsAfiancol(pCodPersona, pUsuario);
        }



    }
}


