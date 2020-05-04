using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.UI.WebControls;
using Xpinn.Ahorros.Data;
using Xpinn.Ahorros.Entities;
using Xpinn.Caja.Entities;
using Xpinn.Comun.Data;
using Xpinn.Comun.Entities;
using Xpinn.FabricaCreditos.Data;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;

namespace Xpinn.Ahorros.Business
{
    /// <summary>
    /// Objeto de negocio para AhorroVista
    /// </summary>
    public class AhorroVistaBusiness : GlobalBusiness
    {
        private AhorroVistaData DAAhorroVista;
        private CuentaHabienteData DACtaHabiente;
        private GeneralData DAGeneral;


        /// <summary>
        /// Constructor del objeto de negocio para AhorroVista
        /// </summary>
        public AhorroVistaBusiness()
        {
            DAAhorroVista = new AhorroVistaData();
            DACtaHabiente = new CuentaHabienteData();
            DAGeneral = new GeneralData();
        }

        /// <summary>
        /// Crea un AhorroVista
        /// </summary>
        /// <param name="pAhorroVista">Entidad AhorroVista</param>
        /// <returns>Entidad AhorroVista creada</returns>
        public AhorroVista CrearAhorroVista(AhorroVista pAhorroVista, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAhorroVista = DAAhorroVista.CrearAhorroVista(pAhorroVista, pUsuario);
                    if (pAhorroVista.LstCuentaHabientes != null)
                        foreach (CuentaHabientes pCtaHab in pAhorroVista.LstCuentaHabientes)
                        {
                            if (pCtaHab.cod_persona != null)
                            {
                                CuentaHabientes pCtaH = new CuentaHabientes();
                                pCtaH = DACtaHabiente.CrearCuentaHabientes(pCtaHab, pUsuario, 1);
                            }
                        }

                    if (pAhorroVista.foto != null)
                    {
                        Imagenes pImagenes = new Imagenes();
                        pImagenes.idimagen = 0;
                        pImagenes.Numero_cuenta = Convert.ToInt64(pAhorroVista.numero_cuenta);
                        pImagenes.tipo_imagen = 1;
                        pImagenes.imagen = pAhorroVista.foto;
                        pImagenes.fecha = System.DateTime.Now;
                        DAAhorroVista.CrearImagenesAhorros(pImagenes, pUsuario);
                    }
                    BeneficiarioData DABenef = new BeneficiarioData();
                    if (pAhorroVista.lstBeneficiarios != null)
                        foreach (Xpinn.FabricaCreditos.Entities.Beneficiario eBenef in pAhorroVista.lstBeneficiarios)
                        {
                            eBenef.numero_cuenta = pAhorroVista.numero_cuenta;
                            Xpinn.FabricaCreditos.Entities.Beneficiario nBeneficiario = new Xpinn.FabricaCreditos.Entities.Beneficiario();
                            nBeneficiario = DABenef.CrearBeneficiarioAhorroVista(eBenef, pUsuario);
                        }

                    ts.Complete();
                }

                return pAhorroVista;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "CrearAhorroVista", ex);
                return null;
            }
        }

        public void EliminarBeneficiarioAhorro(long idbeneficiario, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAhorroVista.EliminarBeneficiarioAhorro(idbeneficiario, usuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "EliminarBeneficiarioAhorro", ex);
            }
        }

        /// <summary>
        /// Modifica un AhorroVista
        /// </summary>
        /// <param name="pAhorroVista">Entidad AhorroVista</param>
        /// <returns>Entidad AhorroVista modificada</returns>
        public AhorroVista ModificarAhorroVista(AhorroVista pAhorroVista, Usuario pUsuario)
        {
            try
            { 
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAhorroVista = DAAhorroVista.ModificarAhorroVista(pAhorroVista, pUsuario);
                    String cod = pAhorroVista.numero_cuenta;
                    if (pAhorroVista.LstCuentaHabientes != null && pAhorroVista.LstCuentaHabientes.Count > 0)
                        foreach (CuentaHabientes pCtaHab in pAhorroVista.LstCuentaHabientes)
                        {
                            if (pCtaHab.cod_persona != null)
                            {
                                pCtaHab.numero_cuenta = cod;
                                CuentaHabientes pCtaH = new CuentaHabientes();
                                if (pCtaHab.idcuenta_habiente == 0)
                                    pCtaH = DACtaHabiente.CrearCuentaHabientes(pCtaHab, pUsuario, 1);//Crear

                                if (pCtaHab.idcuenta_habiente > 0)
                                    pCtaH = DACtaHabiente.CrearCuentaHabientes(pCtaHab, pUsuario, 2);//Modificar

                            }
                        }

                    if (pAhorroVista.foto != null)
                    {
                        Imagenes pImagenes = new Imagenes();
                        pImagenes.idimagen = 0;
                        pImagenes.Numero_cuenta = Convert.ToInt64(pAhorroVista.numero_cuenta);
                        pImagenes.tipo_imagen = 1;
                        pImagenes.imagen = pAhorroVista.foto;
                        pImagenes.fecha = System.DateTime.Now;
                        if (DAAhorroVista.ExisteImagenAhorros(Convert.ToInt64(pAhorroVista.numero_cuenta), 1, pUsuario))
                            DAAhorroVista.ModificarImagenesAhorros(pImagenes, pUsuario);
                        else
                            DAAhorroVista.CrearImagenesAhorros(pImagenes, pUsuario);
                    }
                    ts.Complete();
                }


                BeneficiarioData DABenef = new BeneficiarioData();
                if (pAhorroVista.lstBeneficiarios != null && pAhorroVista.lstBeneficiarios.Count > 0)
                {

                    Int64[] num = new Int64[pAhorroVista.lstBeneficiarios.Count];
                    int secuencia = 0;
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        foreach (Xpinn.FabricaCreditos.Entities.Beneficiario eBenef in pAhorroVista.lstBeneficiarios)
                        {
                            eBenef.numero_cuenta = pAhorroVista.numero_cuenta;
                            Xpinn.FabricaCreditos.Entities.Beneficiario nBeneficiario = new Xpinn.FabricaCreditos.Entities.Beneficiario();
                            if (eBenef.idbeneficiario <= 0)
                                nBeneficiario = DABenef.CrearBeneficiarioAhorroVista(eBenef, pUsuario);
                            else
                                nBeneficiario = DABenef.ModificarBeneficiarioAhorroVista(eBenef, pUsuario);

                            num[secuencia] = nBeneficiario.idbeneficiario;
                            secuencia += 1;
                        }
                        ts.Complete();
                    }
                    String filtro;
                    filtro = num[0].ToString();
                    if (num.Count() > 1)
                    {
                        filtro = num[0].ToString();
                        for (int i = 1; i < num.Count(); i++)
                        {
                            filtro = filtro + "," + num[i].ToString();
                        }
                    }
                    //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    //{
                    //    DABenef.EliminarBeneficiarioAhorroVista(filtro, pAhorroVista.numero_cuenta, pUsuario);
                    //    ts.Complete();
                    //}
                }
                //else
                //{
                //    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //    {
                //        DABenef.EliminarBeneficiarioAhorroVista("0", pAhorroVista.numero_cuenta, pUsuario);
                //        ts.Complete();
                //    }
                //}
                return pAhorroVista;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ModificarAhorroVista", ex);
                return null;
            }
        }

        
        public bool ModificarEstadoSolicitud(AhorroVista vista, Usuario usuario)
        {
            try
            {
                return DAAhorroVista.ModificarEstadoSolicitud(vista, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ModificarEstadoSolicitud", ex);
                return false;
            }
        }

        public bool ModificarEstadoSolicitudProducto(AhorroVista pSolicitud, Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.ModificarEstadoSolicitudProducto(pSolicitud, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ModificarEstadoSolicitudProducto", ex);
                return false;
            }
        }

        public List<AhorroVista> ListarAhorrosBeneficiaros(string filtro, Usuario usuario)
        {
            try
            {
                return DAAhorroVista.ListarAhorrosBeneficiaros(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarAhorrosBeneficiaros", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarAprobaciones(Usuario usuario, DateTime Fecha)
        {
            try
            {
                return DAAhorroVista.ListarAprobaciones(usuario, Fecha);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarAprobaciones", ex);
                return null;
            }
        }
        public List<AhorroVista> ListarAprobacionesCuota(Usuario usuario)
        {
            try
            {
                return DAAhorroVista.ListarAprobacionesCuota(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarAprobacionesCuota", ex);
                return null;
            }
        }


        public List<AhorroVista> ListaAhorroExtractos(AhorroVista pAhorroVista, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAAhorroVista.ListaAhorroExtractos(pAhorroVista, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListaAhorroExtractos", ex);
                return null;
            }
        }



        public List<ReporteMovimiento> ListarDetalleExtracto(String cod_cuenta, DateTime pFechaPago, Usuario pUsuario, DateTime? fechaInicio = null, DateTime? fechaFinal = null, decimal saldoInicial = 0)
        {
            try
            {
                return DAAhorroVista.ListarDetalleExtracto(cod_cuenta, pFechaPago, pUsuario, fechaInicio, fechaFinal, saldoInicial);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBusiness", "ListarDetalleExtracto", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarTipoProductoConSolicitud(Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.ListarTipoProductoConSolicitud(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarTipoProductoConSolicitud", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarTipoProductoConSolicitudRetiro(Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.ListarTipoProductoConSolicitudRetiro(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarTipoProductoConSolicitud", ex);
                return null;
            }
        }
        

        public List<AhorroVista> ReporteGMF(AhorroVista pAhorroVista, DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            try
            {

                return DAAhorroVista.ReporteGMF(pAhorroVista, pFechaIni, pFechaFin, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ReporteGmf", ex);
                return null;
            }
        }

        public AhorroVista ModificarCambioEstados(AhorroVista pAhorroVista, Usuario pUsuario, Xpinn.Tesoreria.Entities.Operacion pOperacion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion entidadop = new Tesoreria.Entities.Operacion();
                    
                     entidadop = operacion.GrabarOperacion(pOperacion, pUsuario);
                     pAhorroVista.cod_ope = entidadop.cod_ope;

                    if (pAhorroVista.cod_ope > 0)
                    {
                        pAhorroVista = DAAhorroVista.ModificarCambioEstados(pAhorroVista, pUsuario);
                    }
                    ts.Complete();
                }

                return pAhorroVista;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ModificarCambioEstados", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un AhorroVista
        /// </summary>
        /// <param name="pId">Identificador de AhorroVista</param>
        public void EliminarAhorroVista(String pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAhorroVista.EliminarAhorroVista(pId, pUsuario);

                    ts.Complete();
                }
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BeneficiarioData DABenef = new BeneficiarioData();
                    DABenef.EliminarBeneficiarioAhorroVista("0", pId, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "EliminarAhorroVista", ex);
            }
        }

        /// <summary>
        /// Obtiene un AhorroVista
        /// </summary>
        /// <param name="pId">Identificador de AhorroVista</param>
        /// <returns>Entidad AhorroVista</returns>
        public AhorroVista ConsultarAhorroVista(String pId, Usuario vUsuario)
        {
            try
            {
                AhorroVista AhorroVista = new AhorroVista();
                AhorroVista = DAAhorroVista.ConsultarAhorroVista(pId, vUsuario);
                if (AhorroVista != null)
                {
                    Int64 idImagen = 0;
                    AhorroVista.foto = DAAhorroVista.ConsultarImagenPersona(Convert.ToString(AhorroVista.numero_cuenta), 1, ref idImagen, vUsuario);
                    AhorroVista.idimagen = idImagen;
                }


                return AhorroVista;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ConsultarAhorroVista", ex);
                return null;
            }
        }
        public byte[] ImagenTarjeta(string NumCuenta, ref Int64 IdImagen, Usuario vUsuario)
        {
            try
            {
                return DAAhorroVista.ConsultarImagenPersona(NumCuenta, 1, ref IdImagen, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ImagenTarjeta", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene un AhorroVista
        /// </summary>
        /// <param name="pId">Identificador de AhorroVista</param>
        /// <returns>Entidad AhorroVista</returns>
        public AhorroVista ConsultarAhorroVistatraslado(String pId, Usuario vUsuario)
        {
            try
            {
                AhorroVista AhorroVista = new AhorroVista();
                AhorroVista = DAAhorroVista.ConsultarAhorroVistaTraslado(pId, vUsuario);
                if (AhorroVista != null)
                {
                    Int64 idImagen = 0;
                    AhorroVista.foto = DAAhorroVista.ConsultarImagenPersona(Convert.ToString(AhorroVista.numero_cuenta), 1, ref idImagen, vUsuario);
                    AhorroVista.idimagen = idImagen;
                }


                return AhorroVista;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ConsultarAhorroVista", ex);
                return null;
            }
        }

        public List<AhorroVista> ConsultarMovimientosMasivos(AhorroVista pAhorroVista, Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.ConsultarMovimientosMasivos(pAhorroVista, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ConsultarMovimientosMasivos", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAhorroVista">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AhorroVista obtenidos</returns>
        public List<AhorroVista> ListarAhorroVista(string pFiltro, DateTime pFechaApe, Usuario vUsuario)
        {
            try
            {
                return DAAhorroVista.ListarAhorroVista(pFiltro, pFechaApe, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarAhorroVista", ex);
                return null;
            }
        }

        public List<AhorroVista> FondoLiquidez(AhorroVista pAhorroVista, Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.FondoLiquidez(pAhorroVista, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "FondoLiquidez", ex);
                return null;
            }
        }


        public void enviarfondoliquidez(List<AhorroVista> lstIngreso, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (AhorroVista entidadn in lstIngreso)
                    {
                        AhorroVista Ahorro = new AhorroVista();
                        entidadn.codusuario = vUsuario.codusuario;
                        Ahorro = DAAhorroVista.enviarfondoliquidez(entidadn, vUsuario);
                    }

                    ts.Complete();
                }

            }


            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "FondoLiquidez", ex);

            }
        }


        public Boolean GeneraNumeroCuenta(Usuario pUsuario)
        {
            try
            {
                General eGeneral = new General();
                eGeneral = DAGeneral.ConsultarGeneral(580, pUsuario);
                if (eGeneral.codigo != null && eGeneral.valor == "1")
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }


        public AhorroVista AplicarTraslado(ref string pError, AhorroVista traslado_cuenta, List<AhorroVista> lstIngreso, Xpinn.Tesoreria.Entities.Operacion poperacion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (traslado_cuenta.numero_cuenta != "")
                    {
                        //CREACION DE LA OPERACION
                        Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();

                        poperacion = DAOperacion.GrabarOperacion(poperacion, vUsuario);

                        //HACER EL INGRESO DE LA CUENTA. Cuentas de la misma persona. Esto se hizo para poder descontar este valor para el cálculo del GMF. FerOrt. 12-Ago-2017. COOPERATIVA AVP.
                        int contador = 0;
                        foreach (AhorroVista Ingreso in lstIngreso)
                        {
                            //ACTUALIZAR TABLA 
                            if (Ingreso.numero_cuenta.Trim() != "" && Ingreso.V_Traslado != 0)
                            {
                                Ingreso.cod_ope = poperacion.cod_ope;
                                Ingreso.fecha_cierre = DateTime.Now;
                                Ingreso.codusuario = vUsuario.codusuario;
                                if (Ingreso.cod_persona == traslado_cuenta.cod_persona)
                                {
                                    DAAhorroVista.IngresoCuenta(Ingreso, vUsuario);
                                    //lstIngreso[contador].aplicada = 1;
                                }
                            }
                            contador += 1;
                        }

                        //HACER EL RETIRO DE LA CUENTA. Crear metodo para el retiro llamar USP_XPINN_AHO_RETIRO.

                        if (traslado_cuenta.numero_cuenta.Trim() != "" && traslado_cuenta.V_Traslado != 0)
                        {
                            AhorroVista Traslado = new AhorroVista();
                            Traslado.numero_cuenta = traslado_cuenta.numero_cuenta;
                            Traslado.cod_ope = poperacion.cod_ope;
                            Traslado.fecha_cierre = poperacion.fecha_oper;
                            Traslado.V_Traslado = traslado_cuenta.V_Traslado;
                            Traslado.codusuario = vUsuario.codusuario;
                            Traslado.cod_persona = traslado_cuenta.cod_persona;

                            DAAhorroVista.TrasladoCuentas(Traslado, vUsuario);

                        }

                        //HACER EL INGRESO DE LA CUENTA. Cuentas de diferente persona.
                        foreach (AhorroVista Ingreso in lstIngreso)
                        {
                            //ACTUALIZAR TABLA 
                            if (Ingreso.numero_cuenta.Trim() != "" && Ingreso.V_Traslado != 0)
                            {
                                Ingreso.cod_ope = poperacion.cod_ope;
                                Ingreso.fecha_cierre = DateTime.Now;
                                Ingreso.codusuario = vUsuario.codusuario;
                                if (Ingreso.cod_persona != traslado_cuenta.cod_persona)
                                    DAAhorroVista.IngresoCuenta(Ingreso, vUsuario);
                            }
                        }

                        ts.Complete();

                    }
                    return traslado_cuenta;
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                BOExcepcion.Throw("AhorroVistaBusiness", "AplicarTraslado", ex);
                return null;
            }
        }

        public void IngresoCuenta(AhorroVista ahorro, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAhorroVista.IngresoCuenta(ahorro, pUsuario);

                    ts.Complete();

                    return;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "IngresoCuenta", ex);
                return;
            }
        }

        public void IngresoCuentamasivo(AhorroVista ahorro, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAhorroVista.IngresoCuenta(ahorro, pUsuario);


                    ts.Complete();
                    return;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "IngresoCuenta", ex);
                return;
            }
        }

        public void AplicarRetiroDeposito(AhorroVista traslado_cuenta, List<AhorroVista> lstIngreso, Xpinn.Tesoreria.Entities.Operacion poperacion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (traslado_cuenta.numero_cuenta != "")
                    {
                        //CREACION DE LA OPERACION
                        Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                        poperacion = DAOperacion.GrabarOperacion(poperacion, vUsuario);

                        //HACER EL RETIRO DE LA CUENTA. Crear metodo para el retiro llamar USP_XPINN_AHO_RETIRO.
                        if (traslado_cuenta.cod_rbTipoArchivo != null)
                        {
                            if (traslado_cuenta.numero_cuenta.Trim() != "")
                            {
                                if (traslado_cuenta.cod_rbTipoArchivo == Convert.ToString(0))
                                {
                                    AhorroVista Traslado = new AhorroVista();

                                    Traslado.numero_cuenta = traslado_cuenta.numero_cuenta;
                                    Traslado.cod_ope = poperacion.cod_ope;
                                    Traslado.fecha_cierre = DateTime.Now;
                                    Traslado.V_Traslado = traslado_cuenta.V_Traslado;
                                    Traslado.codusuario = vUsuario.codusuario;
                                    Traslado.cod_persona = poperacion.cod_cliente;

                                    DAAhorroVista.retiroahorros(Traslado, vUsuario);

                                }
                                else
                                {

                                    if (traslado_cuenta.cod_rbTipoArchivo == Convert.ToString(1))
                                    {
                                        //HACER EL INGRESO DE LA CUENTA. 

                                        //ACTUALIZAR TABLA 

                                        AhorroVista Ingreso = new AhorroVista();

                                        Ingreso.numero_cuenta = traslado_cuenta.numero_cuenta;
                                        Ingreso.cod_ope = poperacion.cod_ope;
                                        Ingreso.fecha_cierre = DateTime.Now;
                                        Ingreso.V_Traslado = traslado_cuenta.V_Traslado;
                                        Ingreso.codusuario = vUsuario.codusuario;
                                        Ingreso.cod_persona = poperacion.cod_cliente;

                                        DAAhorroVista.depositoAhorros(Ingreso, vUsuario);

                                    }
                                }

                                ts.Complete();
                            }
                        }
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "AplicarTraslado", ex);
                return;
            }

        }

        public void InsertarDatos(provision_ahorro Insertar_cuenta, List<provision_ahorro> lstInsertar, Xpinn.Tesoreria.Entities.Operacion pinsertar, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (Insertar_cuenta.numero_cuenta != "")
                    {
                        //CREACION DE LA OPERACION
                        Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();

                        pinsertar = DAOperacion.GrabarOperacion(pinsertar, vUsuario);


                        //HACER EL INGRESO DE LA CUENTA. 
                        foreach (provision_ahorro Insertar in lstInsertar)
                        {

                            provision_ahorro pEntidad = new provision_ahorro();

                            Insertar.idprovision = 0;
                            Insertar.cod_ope = pinsertar.cod_ope;


                            pEntidad = DAAhorroVista.InsertarDatos(Insertar, vUsuario);
                        }
                    }

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "IngresoCuenta", ex);
                return;
            }

        }


        public List<AhorroVista> ReportePeriodico(AhorroVista pAhorroVista, Usuario vUsuario)
        {
            try
            {
                return DAAhorroVista.ReportePeriodico(pAhorroVista, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ReportePeriodico", ex);
                return null;
            }
        }

        public void Crearcierea(Xpinn.Comun.Entities.Cierea pcierea, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DAAhorroVista.Crearcierea(pcierea, vUsuario);
                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiereaBusiness", "Crearcierea", ex);
                return;
            }
        }

        public List<provision_ahorro> ListarProvision(DateTime pFechaIni, provision_ahorro pProvision, Usuario vUsuario)
        {
            try
            {


                provision_ahorro Traslado = new provision_ahorro();
                Traslado.codusuario = vUsuario.codusuario;
                Traslado.cod_linea_ahorro = pProvision.cod_linea_ahorro;
                Traslado.cod_oficina = pProvision.cod_oficina;


                return DAAhorroVista.ListarProvision(pFechaIni, pProvision, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarProvision", ex);
                return null;
            }
        }

        public string ConsultarNombreLineaDeAhorroPorCodigo(string linea, Usuario usuario)
        {
            try
            {
                return DAAhorroVista.ConsultarNombreLineaDeAhorroPorCodigo(linea, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ConsultarNombreLineaDeAhorroPorCodigo", ex);
                return null;
            }
        }

        public AhorroVista ConsultarAhorroVistaDatosOficina(string numeroCuenta, Usuario usuario)
        {
            try
            {
                return DAAhorroVista.ConsultarAhorroVistaDatosOficina(numeroCuenta, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ConsultarAhorroVistaDatosOficina", ex);
                return null;
            }
        }

        public ReporteMovimiento ConsultarExtractoAhorroVista(string numeroCuenta, DateTime fechaCorte, DateTime fechaInicio, DateTime fechaFinal, Usuario usuario)
        {
            try
            {
                ReporteMovimiento reporte = DAAhorroVista.ConsultarExtractoAhorroVista(numeroCuenta, fechaCorte, fechaInicio, usuario);

                reporte.SaldoInicio = DAAhorroVista.ConsultarSaldoInicialPeriodoAhorroVista(numeroCuenta, fechaInicio.AddDays(-1), usuario);

                if (reporte.SaldoInicio==0)
                    reporte.SaldoInicio = DAAhorroVista.ConsultarSaldoUtlPeriodoAhorroVista(numeroCuenta, fechaInicio.AddDays(-1), usuario);


                reporte.SaldoInicioTrans = DAAhorroVista.ConsultarSaldoUtlPeriodoAhorroVista(numeroCuenta, fechaInicio, usuario);

                reporte.SaldoFinal = DAAhorroVista.ConsultarSaldoFinalPeriodoAhorroVista(numeroCuenta, reporte.SaldoInicioTrans, fechaInicio,fechaFinal, usuario);
               
                return reporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ConsultarExtractoAhorroVista", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarCuentaAhorroVista(long cod, Usuario usuario)
        {
            try
            {
                return DAAhorroVista.ListarCuentaAhorroVista(cod, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarCuentaAhorroVista", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarCuentaAhorroVistaGiro(long cod, Usuario usuario)
        {
            try
            {
                return DAAhorroVista.ListarCuentaAhorroVistaGiro(cod, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarCuentaAhorroVistaGiro", ex);
                return null;
            }
        }
        public List<AhorroVista> ListarAhorroClubAhorradores(Int64 pcliente, Boolean pResult, string pFiltroAdd, Usuario vUsuario)
        {
            try
            {
                return DAAhorroVista.ListarAhorroClubAhorradores(pcliente, pResult, pFiltroAdd, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarAportesClubAhorradores", ex);
                return null;
            }
        }
        public List<ELibretas> getAllLibretas(String pfiltro, Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.getAllLista(pfiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "getAllLibretas", ex);
                return null;
            }
        }

        public List<ELibretas> llenarListaNuevoBussines(DateTime pfechaAper, String pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.llenarListaNuevo(pfechaAper, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "llenarListaNuevoBussines", ex);
                return null;
            }
        }

        public void eliminarLibretaBusines(Int64 pIdLibreta, Usuario pusuario)
        {
            try
            {
                DAAhorroVista.eliminarLibreta(pIdLibreta, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "eliminarLibretaBusines", ex);
            }
        }

        public String validarBusiness(Usuario pUsuario, Int64 id)
        {
            try
            {
                return DAAhorroVista.validarEliminar(pUsuario, id);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "eliminarLibretaBusines", ex);
                return null;
            }
        }

        public string CrearSolicitudAhorros(AhorroVista pAhorros, Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.CrearSolicitudAhorros(pAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "CrearSolicitudAhorros", ex);
                return null;
            }
        }

        public string CrearSolicitudAhorrosVista(AhorroVista pAhorros, Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.CrearSolicitudAhorrosVista(pAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "CrearSolicitudAhorrosVista", ex);
                return null;
            }
        }

        public ELibretas getLibretaByNumeroCuentaBuss(String codigo, Usuario pusuario)
        {
            try
            {
                return DAAhorroVista.getLibretaByNumeroCuenta(codigo, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "eliminarLibretaBusines", ex);
                return null;
            }
        }

        public void InsertarLibretaBusness(Usuario pUsuario, ELibretas pElibreta, Int64 pidMotivo)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAhorroVista.InsertarLibreta(pUsuario, pElibreta, pidMotivo);
                    if (pElibreta.id_Libreta > 0)
                        DAAhorroVista.actEstadoAnterior(pUsuario, pElibreta.id_Libreta);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "InsertarLibretaBusness", ex);
            }
        }

        public ELibretas getLibretaByIdLibretaBusiness(Int64 idCodigo, Usuario pusuario)
        {
            try
            {
                return DAAhorroVista.getLibretaByIdLibreta(idCodigo, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "getLibretaByIdLibretaBusiness", ex);
                return null;
            }
        }

        public void updateLibretaBusiness(Usuario pUsuario, ELibretas pElibreta, Int64 idMotivo)
        {
            try
            {
                DAAhorroVista.updateLibreta(pUsuario, pElibreta, idMotivo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "getLibretaByIdLibretaBusiness", ex);
            }
        }

        public decimal consultarBusiness(Int64 codigo, String codCuenta, Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.consultar(codigo, codCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "getLibretaByIdLibretaBusiness", ex);
                return -1;
            }
        }

        public Int64 getNumeroDesprendibleBusines(Usuario pUsuario, String numeroCuenta)
        {
            try
            {
                return DAAhorroVista.getNumeroDesprendible(pUsuario, numeroCuenta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "getNumeroDesprendibleBusines", ex);
                return -1;
            }
        }
        public List<Cierea> ListarErrorCierre(string tipo, Usuario pUsuario)
        {
            List<Cierea> lstConsulta = new List<Cierea>();
            // Determinar la fecha del último cierre realizado
            Cierea pCierre = new Cierea();
            pCierre.tipo = tipo;
            pCierre.estado = "P";

            pCierre = DAAhorroVista.FechaUltimoCierre(pCierre, " where to_char(fecrea,'dd/mm/yyyy') = '" + DateTime.Now.ToShortDateString() + "' and tipo='" + tipo + "' and exists (select * from errorescierreshistoricos e where c.fecha=e.fechacierre) ", pUsuario);
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

            return lstConsulta = DAAhorroVista.ListaErrorCierreHistorico(tipo, pUsuario, FecIni.ToShortDateString());

        }

        public AhorroVista ConsultarCuentaBancaria(string pCodPersona, Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.ConsultarCuentaBancaria(pCodPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ConsultarCuentaBancaria", ex);
                return null;
            }
        }

        public List<Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            Xpinn.Comun.Business.FechasBusiness BOFechas = new Comun.Business.FechasBusiness();
            List<Cierea> LstCierre = new List<Cierea>();
            // Determinar la periodicidad de cierre
            int dias_cierre = 0;
            int tipo_calendario = 0;
            DAAhorroVista.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
            // Determinar la fecha del último cierre realizado
            Cierea pCierre = new Cierea();
            pCierre.tipo = "H";
            pCierre.estado = "D";
            pCierre = DAAhorroVista.FechaUltimoCierre(pCierre, "", pUsuario);
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

        public AhorroVista  ciCierreHistorico(AhorroVista pentidad,string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            /* try
             {
                  TransactionOptions topc = new TransactionOptions();
                  topc.Timeout = TimeSpan.MaxValue;
                  using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, topc))
                  {
                     DAAhorroVista.ciCierreHistorico(estado, fecha, cod_usuario, ref serror, pUsuario);
                     ts.Complete();
                  }



              }
              catch (Exception ex)
              {
                  BOExcepcion.Throw("CierreHistoricoBusiness", "CierreHistorico", ex);
              }
              */


            try
          {
                return  DAAhorroVista.ciCierreHistorico(pentidad, estado, fecha, cod_usuario, ref serror, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CierremensualServices", "CrearCierreMensual", ex);
               return null;
            }
        }

        //public string ValidarCierre(string estado, DateTime fecha, int cod_usuario, Usuario pUsuario)
        //{

        //return DAAhorroVista.ValidarCierre(estado, fecha, cod_usuario, pUsuario);

        //}

        public List<Imagenes> Handler(Imagenes pImagenes, Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.Handler(pImagenes, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "Handler", ex);
                return null;
            }
        }
        public DateTime getFechaPosCierreCon(Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.getfechaUltimoCierreConta(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "getFechaPosCierreCon", ex);
                return DateTime.MinValue;
            }
        }
        public DateTime getfechaUltimaCierreAhorros(Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.getfechaUltimaCierreAhorros(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "getfechaUltimaCierreAhorros", ex);
                return DateTime.MinValue;
            }
        }
        public List<ELiquidacionInteres> getCuentasLiquidarBusinnes(DateTime pfechaLiquidacion, String codLinea, Usuario pusuario, String cuenta)
        {
            try
            {
                return DAAhorroVista.getListaCuentasLiquidar(pfechaLiquidacion, codLinea, pusuario, cuenta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "getCuentasLiquidarBusinnes", ex);
                return null;
            }
        }

        //CONSULTAR DETALLE TITULAR
        public List<CuentaHabientes> ListarDetalleTitulares(Int64 pCod, Usuario vUsuario)
        {
            try
            {
                return DAAhorroVista.ListarDetalleTitulares(pCod, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarDetalleTitulares", ex);
                return null;
            }
        }
        public List<AhorroVista> ListarTarjetas(String pId, Usuario vUsuario)
        {
            try
            {
                return DAAhorroVista.ListarTarjetas(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarDetalleTitulares", ex);
                return null;
            }
        }
        public void EliminarCtaHabiente(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAhorroVista.EliminarCtaHabiente(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "EliminarCtaHabiente", ex);
            }
        }

        public AhorroVista ConsultarAfiliacion(String pId, Usuario vUsuario)
        {
            try
            {
                AhorroVista AhorroVista = new AhorroVista();
                AhorroVista = DAAhorroVista.ConsultarAfiliacion(pId, vUsuario);

                return AhorroVista;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ConsultarAfiliacion", ex);
                return null;
            }
        }

        public ELiquidacionInteres CalculoLiquidacionaHORRO(ELiquidacionInteres pLiqui, Usuario vUsuario)
        {
            try
            {
                return DAAhorroVista.CalculoLiquidacionaHORRO(pLiqui, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "CalculoLiquidacionaHORRO", ex);
                return null;
            }
        }
        public ELiquidacionInteres CierreLiquidacionAhorro(ref string pError, ref Int64 COD_OPE, Xpinn.Tesoreria.Entities.Operacion pOperacion, Xpinn.FabricaCreditos.Entities.Giro pGiro, ELiquidacionInteres pLiqui, Usuario vUsuario)
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

                    //GRABAR NUEVO GIRO
                    Xpinn.FabricaCreditos.Data.AvanceData GiroData = new Xpinn.FabricaCreditos.Data.AvanceData();

                    // VALIDANDO SEGUN FORMA DE PAGO
                    TipoFormaDesembolso formaPago = pGiro.forma_pago.ToEnum<TipoFormaDesembolso>();
                    if (formaPago != TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                    {
                        Xpinn.FabricaCreditos.Entities.Giro vGiroEntidad = new Xpinn.FabricaCreditos.Entities.Giro();
                        pGiro.cod_ope = vOpe.cod_ope;
                        vGiroEntidad = GiroData.CrearGiro(pGiro, vUsuario, 1);
                    }
                    else
                    {
                        AhorroVistaBusiness ahorroBusiness = new AhorroVistaBusiness();
                        AhorroVista ahorro = new AhorroVista
                        {
                            numero_cuenta = pGiro.num_cuenta,
                            cod_persona = pGiro.cod_persona,
                            cod_ope = COD_OPE,
                            fecha_cierre = pLiqui.fecha_liquidacion,
                            V_Traslado = pLiqui.valor_pagar,
                            codusuario = vUsuario.codusuario
                        };

                        ahorroBusiness.IngresoCuenta(ahorro, vUsuario);
                    }

                    //GENERANDO CIERRE AHORRO VISTA
                    pLiqui.cod_ope = vOpe.cod_ope;
                    pLiqui = DAAhorroVista.CierreLiquidacionAhorro(pLiqui, vUsuario);
                    ts.Complete();
                }
                return pLiqui;
            }

            catch (Exception ex)
            {
                pError = ex.Message;
                BOExcepcion.Throw("AhorroVistaBusinnes", "CierreLiquidacionAhorro", ex);
                return null;
            }
        }

        public void GuardarLiquidacionAhorro(ref string pError, ref Int64 COD_OPE, Xpinn.Tesoreria.Entities.Operacion pOperacion, ELiquidacionInteres pLiqui, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    //GRABACION DE LA OPERACION
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();

                    vOpe = DAOperacion.GrabarOperacion(pOperacion, vUsuario);
                    COD_OPE = vOpe.cod_ope;

                    if (pLiqui.lstLista != null && pLiqui.lstLista.Count > 0)
                    {

                        foreach (ELiquidacionInteres rOpe in pLiqui.lstLista)
                        {

                            //GENERANDO LIQUIDACION INTERES  AHORROS
                            rOpe.cod_ope = vOpe.cod_ope;
                            DAAhorroVista.GuardarLiquidacionAhorro(rOpe, vUsuario);

                        }
                        ts.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                pError += ex.Message;
                BOExcepcion.Throw("AhorroVistaBusinnes", "GuardarLiquidacionAhorro", ex);
                pError = ex.Message;
            }
        }

        public ELiquidacionInteres CrearLiquidacionAhorro(ELiquidacionInteres pLiqui, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pLiqui.lstLista != null && pLiqui.lstLista.Count > 0)
                    {

                        foreach (ELiquidacionInteres rOpe in pLiqui.lstLista)
                        {
                            //Xpinn.CDATS.Data.AperturaCDATData xApertura = new Xpinn.CDATS.Data.AperturaCDATData();
                            ELiquidacionInteres nLiquidacio = new ELiquidacionInteres();
                            nLiquidacio = DAAhorroVista.CrearLiquidacionAhorro(rOpe, vUsuario);


                        }


                    }
                    ts.Complete();
                }
                return pLiqui;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusinnes", "CrearLiquidacionAhorro", ex);
                return null;
            }
        }

        /// <returns>Conjunto de AhorroVista obtenidos</returns>
        public List<AhorroVista> ListarAhorroVistApPagos(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAAhorroVista.ListarAhorroVistApPagos(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarAhorroVistApPagos", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarAhorroAptPagos(string pFiltro, int p_producto, Usuario vUsuario)
        {
            try
            {
                return DAAhorroVista.ListarAhorroAptPagos(pFiltro, p_producto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarAhorroVistApPagos", ex);
                return null;
            }
        }


        public TransaccionCaja Aplicar(TransaccionCaja pTransaccionCaja, GridView gvTransacciones, List<AhorroVista> lstAhorroVista, string pObservacion, Usuario pUsuario, ref string Error)
        {
            PersonaTransaccion pQuien = new PersonaTransaccion();
            pQuien.titular = true;
            return Aplicar(pTransaccionCaja, pQuien, gvTransacciones, lstAhorroVista, pObservacion, pUsuario, ref Error);
        }


        public TransaccionCaja Aplicar(TransaccionCaja pTransaccionCaja, PersonaTransaccion perTran, GridView gvTransacciones, List<AhorroVista> lstAhorroVista, string pObservacion, Usuario pUsuario, ref string Error)
        {
            PagosVentanillaData DAPagosVentanilla = new PagosVentanillaData();
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    //Validar Fecha Transaciones                    
                    string NumError = "";
                    string NumProducto;
                    int TipoProduc;
                    string FechaAperProd = "";

                    //Validamos fecha Apertura de los productos que entran en la transacción
                    foreach (GridViewRow fila in gvTransacciones.Rows)
                    {                        
                        TipoProduc = Convert.ToInt32(fila.Cells[4].Text);
                        NumProducto = Convert.ToString(fila.Cells[8].Text);

                        FechaAperProd = DAPagosVentanilla.FechasApertura(NumProducto, TipoProduc, pUsuario);

                        if (FechaAperProd != "")
                        {
                            if (pTransaccionCaja.fecha_aplica < Convert.ToDateTime(FechaAperProd))
                            {
                                if (NumError != "")
                                    NumError += ", " + NumProducto;
                                else
                                    NumError += NumProducto;
                            }
                        }
                    }

                    //Validamos fecha de apertura de las cuentas de ahorro vista que realizara la transacción 
                    foreach (AhorroVista item in lstAhorroVista)
                    {
                        TipoProduc = Convert.ToInt32(item.tipo_producto);
                            NumProducto = item.numero_cuenta;

                        FechaAperProd = DAPagosVentanilla.FechasApertura(NumProducto, TipoProduc, pUsuario);

                        if (FechaAperProd != "")
                        {
                            if (pTransaccionCaja.fecha_aplica < Convert.ToDateTime(FechaAperProd))
                            {
                                if (NumError != "")
                                    NumError += ", " + NumProducto;
                                else
                                    NumError += NumProducto;
                            }
                        }
                    }
                                       
                    if (NumError != "")
                    {
                        Error = "La fecha de la operación es menor a la de apertura de los productos: " + NumError;
                        return null;
                    }
                                     
                    pTransaccionCaja = DAPagosVentanilla.AplicarPagoVentanilla(pTransaccionCaja, perTran, gvTransacciones, null, null, pObservacion, pUsuario, ref Error);
                    foreach (AhorroVista eaplicacion in lstAhorroVista)
                    {
                        if (eaplicacion.valor_a_aplicar != 0)
                            DAAhorroVista.Aplicar(eaplicacion, pTransaccionCaja.cod_ope, pUsuario);
                    }
                    ts.Complete();
                }
                return pTransaccionCaja;
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de creditos a aplicar
        /// </summary>
        /// <returns>Conjunto de AhorroVista obtenidos</returns>
        public List<CreditoDebAhorros> ListarCreditoDebAhorros(Usuario vUsuario)
        {
            try
            {
                return DAAhorroVista.ListarCreditoDebAhorros(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarAhorroVista", ex);
                return null;
            }
        }

        public Decimal? Calcular_VrAPagar(Int64 pNumRadicacion, String pFecha, Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.Calcular_VrAPagar(pNumRadicacion, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "Calcular_VrAPagar", ex);
                return null;
            }

        }

        public Boolean AplicarCréditoDebAhorros(List<CreditoDebAhorros> lCredito, DateTime fecha_actual, ref Int64 pcod_ope, Usuario pUsuario)
        {
            try
            {
                Boolean result = false;
                String Error = "";
                Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();

                // Crear la operación
                pOperacion.cod_ope = 0;
                pOperacion.tipo_ope = 50;
                pOperacion.cod_usu = pUsuario.codusuario;
                pOperacion.cod_ofi = pUsuario.cod_oficina;
                pOperacion.fecha_oper = fecha_actual;
                pOperacion.fecha_calc = fecha_actual;
                pOperacion.num_lista = null;
                pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref Error, pUsuario);
                pcod_ope = pOperacion.cod_ope;
                foreach (CreditoDebAhorros credito in lCredito)
                {
                    credito.cod_ope = pOperacion.cod_ope;
                    result = DAAhorroVista.AplicarCréditoDebAhorros(credito, pUsuario);
                }
                return result;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "AplicarCréditoDebAhorros", ex);
                return false;
            }
        }


        public List<Cierea> ListarFechaCierreCausacion(Usuario pUsuario)
        {
            Xpinn.Comun.Business.FechasBusiness BOFechas = new Comun.Business.FechasBusiness();
            List<Cierea> LstCierre = new List<Cierea>();
            // Determinar la periodicidad de cierre
            int dias_cierre = 0;
            int tipo_calendario = 0;
            DAAhorroVista.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
            // Determinar la fecha del último cierre realizado
            Cierea pCierre = new Cierea();
            pCierre.tipo = "I";
            pCierre.estado = "D";
            pCierre = DAAhorroVista.FechaUltimoCierre(pCierre, "", pUsuario);
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



        public bool RetiroDeposito(ref string pError, AhorroVista pCuenta, Xpinn.Tesoreria.Entities.Operacion poperacion, Xpinn.FabricaCreditos.Entities.Giro pGiro, ref Int64 pCodOpe, ref Int64 pIdGiro, Usuario pUsuario)
        {
            pCodOpe = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pCuenta.numero_cuenta != "")
                    {
                        // CREACION DE LA OPERACION
                        Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                        poperacion = DAOperacion.GrabarOperacion(poperacion, pUsuario);
                        pCodOpe = poperacion.cod_ope;

                        // HACER EL RETIRO DE LA CUENTA.
                        pCuenta.cod_ope = poperacion.cod_ope;
                        pCuenta.forma_giro = Convert.ToInt32(pGiro.forma_pago);
                        DAAhorroVista.retiroahorros(pCuenta, pUsuario);

                        // INSERTAR EL GIRO
                        Xpinn.FabricaCreditos.Data.CreditoData DACredito = new Xpinn.FabricaCreditos.Data.CreditoData();
                        Xpinn.FabricaCreditos.Entities.Credito eCredito = new Xpinn.FabricaCreditos.Entities.Credito();
                        DACredito.GuardarGiro(eCredito.numero_radicacion, pCodOpe, Convert.ToInt32(pGiro.forma_pago), Convert.ToDateTime(pCuenta.fecha_cierre), Convert.ToDouble(pCuenta.V_Traslado), Convert.ToInt32(pGiro.idctabancaria), pGiro.cod_banco, pGiro.num_cuenta, pGiro.tipo_cuenta, Convert.ToInt64(pCuenta.cod_persona), pUsuario.nombre, pUsuario);

                        ts.Complete();

                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                BOExcepcion.Throw("AhorroVistaBusiness", "RetiroDeposito", ex);
                return false;
            }

        }

        public List<AhorroVista> ListarAhorroVistaReporteCierre(DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return DAAhorroVista.ListarAhorroVistaReporteCierre(pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarAhorroVistaReporteCierre", ex);
                return null;
            }
        }


        public AhorroVista ConsultarCuentaAhorroVista(String pId, Usuario vUsuario)
        {
            try
            {
                AhorroVista AhorroVista = new AhorroVista();
                AhorroVista = DAAhorroVista.ConsultarCuentaAhorroVista(pId, vUsuario);
                return AhorroVista;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ConsultarCuentaAhorroVista", ex);
                return null;
            }
        }


        public TransaccionCaja AplicarCruce_Ahs_Pro(TransaccionCaja pTransaccionCaja, PersonaTransaccion perTran, GridView gvTransacciones, List<AhorroVista> lstAhorroVista, string pObservacion, Usuario pUsuario, ref string Error)
        {
            PagosVentanillaData DAPagosVentanilla = new PagosVentanillaData();
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTransaccionCaja = DAPagosVentanilla.AplicarPagoVentanilla(pTransaccionCaja, perTran, gvTransacciones, null, null, pObservacion, pUsuario, ref Error);
                    foreach (AhorroVista eaplicacion in lstAhorroVista)
                    {
                        if (eaplicacion.valor_a_aplicar != 0)
                            DAAhorroVista.AplicarCruce_Ahs_Pro(eaplicacion, pTransaccionCaja.cod_ope, pUsuario);
                    }
                    ts.Complete();
                }
                return pTransaccionCaja;
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return null;
            }
        }


        public AhorroVista ConsultarCierreAhorroVista(Usuario vUsuario)
        {
            try
            {
                AhorroVista AhorroVista = new AhorroVista();
                AhorroVista = DAAhorroVista.ConsultarCierreAhorroVista(vUsuario);

                return AhorroVista;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ConsultarCierreAhorroVista", ex);
                return null;
            }
        }
        public AhorroVista CrearNovedadCambio(AhorroVista vAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    vAporte = DAAhorroVista.CrearNovedadCambio(vAporte, pUsuario);

                    ts.Complete();
                }

                return vAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "CrearNovedadCambio", ex);
                return null;
            }
        }
        public List<AhorroVista> ListarAhorroNovedadesCambio(string filtro, Usuario usuario)
        {
            try
            {
                return DAAhorroVista.ListarAhorroNovedadesCambio(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarAhorroNovedadesCambio", ex);
                return null;
            }
        }
        public void ModificarNovedadCuotaAhorro(AhorroVista ahorro, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAhorroVista.ModificarNovedadCuotaAhorro(ahorro, usuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ModificarNovedadCuotaAhorro", ex);
            }
        }
        public bool? ValidarFechaSolicitudCambio(AhorroVista pAhorro, Usuario usuario)
        {
            try
            {
                return DAAhorroVista.ValidarFechaSolicitudCambio(pAhorro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ValidarFechaSolicitudCambio", ex);
                return null;
            }
        }
        public List<SolicitudProductosWeb> ListarSolicitudCreditoAAC(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.ListarSolicitudCreditoAAC(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarSolicitudCreditoAAC", ex);
                return null;
            }
        }
        
        //Devuelve lista de solicitudes de retiro de ahorro a la vista        
        public List<AhorroVista> ListarSolicitudRetiro(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.ListarSolicitudRetiro(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarSolicitudRetiro", ex);
                return null;
            }
        }
                

        public SolicitudProductosWeb CrearAprobacionProducto(SolicitudProductosWeb pProducto, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProducto = DAAhorroVista.CrearConfirmacionProducto(pProducto, pUsuario);


                    ts.Complete();
                }

                return pProducto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "CrearAprobacionProducto", ex);
                return null;
            }
        }
        public string MaxRegistro(string TipoAhorro, Usuario usuario)
        {
            try
            {
                return DAAhorroVista.MaxRegistro(TipoAhorro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ValidarFechaSolicitudCambio", ex);
                return null;
            }
        }

        public int CrearSolicitudRetiroAhorros(AhorroVista pAhorro, Usuario pUsuario)
        {
            try
            {
                int solicitud = 0;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    solicitud = DAAhorroVista.CrearSolicitudRetiroAhorros(pAhorro, pUsuario);
                    ts.Complete();
                }

                return solicitud;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "CrearSolicitudRetiroAhorros", ex);
                return 0;
            }
        }

        //Devuelve lista de solicitudes de productos generadas desde la web
        public List<AhorroVista> ListarSolicitudProducto(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAAhorroVista.ListarSolicitudProducto(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListarSolicitudProducto", ex);
                return null;
            }
        }
        

    }
}

